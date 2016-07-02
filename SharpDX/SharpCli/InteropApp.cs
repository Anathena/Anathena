// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Pdb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using CallSite = Mono.Cecil.CallSite;
using MethodAttributes = Mono.Cecil.MethodAttributes;

namespace SharpCli
{
    /// <summary>
    /// InteropBuilder is responsible to patch SharpDX assemblies and inject unmanaged interop call.
    /// InteropBuilder is also adding several useful methods:
    /// - memcpy using cpblk
    /// - Read/ReadRange/Write/WriteRange of structured data to a memory location
    /// - SizeOf on generic structures (C# usually doesn't allow this).
    /// </summary>
    public class InteropApp
    {
        private List<TypeDefinition> ClassToRemoveList = new List<TypeDefinition>();
        private AssemblyDefinition AssemblyDefinition;
        private TypeReference VoidType;
        private TypeReference VoidPointerType;
        private TypeReference IntType;
        private Boolean ContainsSharpJit;

        static void Main(String[] Args)
        {
            try
            {
                if (Args.Length != 1)
                {
                    Console.WriteLine("{0} file_path is expecting one file argument", Assembly.GetExecutingAssembly().GetName().Name);
                    Environment.Exit(1);
                }

                String File = Args[0];
                InteropApp Program = new InteropApp();
                Program.PatchFile(File);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Creates a module init for a C# assembly.
        /// </summary>
        /// <param name="Method">The method to add to the module init.</param>
        private void CreateModuleInit(MethodDefinition Method)
        {
            const MethodAttributes ModuleInitAttributes = MethodAttributes.Static |
                MethodAttributes.Assembly | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;

            TypeDefinition ModuleType = AssemblyDefinition.MainModule.GetType("<Module>");

            // Get or create ModuleInit method
            MethodDefinition Cctor = ModuleType.Methods.FirstOrDefault(moduleTypeMethod => moduleTypeMethod.Name == ".cctor");
            if (Cctor == null)
            {
                Cctor = new MethodDefinition(".cctor", ModuleInitAttributes, Method.ReturnType);
                ModuleType.Methods.Add(Cctor);
            }

            Boolean IsCallAlreadyDone = Cctor.Body.Instructions.Any(Instruction => Instruction.OpCode == OpCodes.Call && Instruction.Operand == Method);

            // If the method is not called, we can add it
            if (!IsCallAlreadyDone)
            {
                ILProcessor ILProcessor = Cctor.Body.GetILProcessor();
                Instruction retInstruction = Cctor.Body.Instructions.FirstOrDefault(Instruction => Instruction.OpCode == OpCodes.Ret);
                Instruction CallMethod = ILProcessor.Create(OpCodes.Call, Method);

                if (retInstruction == null)
                {
                    // If a ret instruction is not present, add the method call and ret
                    ILProcessor.Append(CallMethod);
                    ILProcessor.Emit(OpCodes.Ret);
                }
                else
                {
                    // If a ret instruction is already present, just add the method to call before
                    ILProcessor.InsertBefore(retInstruction, CallMethod);
                }
            }
        }

        /// <summary>
        /// Creates the write method with the following signature: 
        /// <code>
        /// public static unsafe void* Write&lt;T&gt;(void* pDest, ref T data) where T : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The method to patch</param>
        private void CreateWriteMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor Gen = Method.Body.GetILProcessor();
            GenericParameter ParamT = Method.GenericParameters[0];

            // Preparing locals
            // local(0) int
            Method.Body.Variables.Add(new VariableDefinition(IntType));
            // local(1) T*
            Method.Body.Variables.Add(new VariableDefinition(new PinnedType(new ByReferenceType(ParamT))));

            // Push (0) pDest for memcpy
            Gen.Emit(OpCodes.Ldarg_0);

            // fixed (void* pinnedData = &data[offset])
            Gen.Emit(OpCodes.Ldarg_1);
            Gen.Emit(OpCodes.Stloc_1);

            // Push (1) pinnedData for memcpy
            Gen.Emit(OpCodes.Ldloc_1);

            // totalSize = sizeof(T)
            Gen.Emit(OpCodes.Sizeof, ParamT);
            Gen.Emit(OpCodes.Conv_I4);
            Gen.Emit(OpCodes.Stloc_0);

            // Push (2) totalSize
            Gen.Emit(OpCodes.Ldloc_0);

            // Emit cpblk
            EmitCpblk(Method, Gen);

            // Return pDest + totalSize
            Gen.Emit(OpCodes.Ldloc_0);
            Gen.Emit(OpCodes.Conv_I);
            Gen.Emit(OpCodes.Ldarg_0);
            Gen.Emit(OpCodes.Add);

            // Ret
            Gen.Emit(OpCodes.Ret);
        }

        private void ReplaceFixedStatement(MethodDefinition Method, ILProcessor ILProcessor, Instruction FixedToPatch)
        {
            TypeReference ParamT = ((GenericInstanceMethod)FixedToPatch.Operand).GenericArguments[0];

            // Preparing locals
            // local(0) T*
            Method.Body.Variables.Add(new VariableDefinition("pin", new PinnedType(new ByReferenceType(ParamT))));

            Int32 Index = Method.Body.Variables.Count - 1;

            Instruction LdlocFixed;
            Instruction StlocFixed;
            switch (Index)
            {
                case 0:
                    StlocFixed = ILProcessor.Create(OpCodes.Stloc_0);
                    LdlocFixed = ILProcessor.Create(OpCodes.Ldloc_0);
                    break;
                case 1:
                    StlocFixed = ILProcessor.Create(OpCodes.Stloc_1);
                    LdlocFixed = ILProcessor.Create(OpCodes.Ldloc_1);
                    break;
                case 2:
                    StlocFixed = ILProcessor.Create(OpCodes.Stloc_2);
                    LdlocFixed = ILProcessor.Create(OpCodes.Ldloc_2);
                    break;
                case 3:
                    StlocFixed = ILProcessor.Create(OpCodes.Stloc_3);
                    LdlocFixed = ILProcessor.Create(OpCodes.Ldloc_3);
                    break;
                default:
                    StlocFixed = ILProcessor.Create(OpCodes.Stloc, Index);
                    LdlocFixed = ILProcessor.Create(OpCodes.Ldloc, Index);
                    break;
            }

            ILProcessor.InsertBefore(FixedToPatch, StlocFixed);
            ILProcessor.Replace(FixedToPatch, LdlocFixed);
        }

        private void ReplaceReadInline(MethodDefinition Method, ILProcessor ILProcessor, Instruction FixedToPatch)
        {
            TypeReference ParamT = ((GenericInstanceMethod)FixedToPatch.Operand).GenericArguments[0];
            Instruction CopyInstruction = ILProcessor.Create(OpCodes.Ldobj, ParamT);
            ILProcessor.Replace(FixedToPatch, CopyInstruction);
        }

        private void ReplaceCopyInline(MethodDefinition method, ILProcessor ilProcessor, Instruction FixedToPatch)
        {
            TypeReference ParamT = ((GenericInstanceMethod)FixedToPatch.Operand).GenericArguments[0];
            Instruction CopyInstruction = ilProcessor.Create(OpCodes.Cpobj, ParamT);
            ilProcessor.Replace(FixedToPatch, CopyInstruction);
        }

        private void ReplaceSizeOfStructGeneric(MethodDefinition method, ILProcessor ILProcessor, Instruction FixedToPatch)
        {
            TypeReference ParamT = ((GenericInstanceMethod)FixedToPatch.Operand).GenericArguments[0];
            Instruction CopyInstruction = ILProcessor.Create(OpCodes.Sizeof, ParamT);
            ILProcessor.Replace(FixedToPatch, CopyInstruction);
        }

        /// <summary>
        /// Creates the cast  method with the following signature:
        /// <code>
        /// public static unsafe void* Cast&lt;T&gt;(ref T data) where T : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The method cast.</param>
        private void CreateCastMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor Gen = Method.Body.GetILProcessor();

            Gen.Emit(OpCodes.Ldarg_0);

            // Ret
            Gen.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates the cast  method with the following signature:
        /// <code>
        /// public static TCAST[] CastArray&lt;TCAST, T&gt;(T[] arrayData) where T : struct where TCAST : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The method cast array.</param>
        private void CreateCastArrayMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor Gen = Method.Body.GetILProcessor();

            Gen.Emit(OpCodes.Ldarg_0);

            // Ret
            Gen.Emit(OpCodes.Ret);
        }

        private void ReplaceFixedArrayStatement(MethodDefinition Method, ILProcessor ILProcessor, Instruction FixedToPatch)
        {
            TypeReference ParamT = ((GenericInstanceMethod)FixedToPatch.Operand).GenericArguments[0];

            // Preparing locals
            // local(0) T*
            Method.Body.Variables.Add(new VariableDefinition("pin", new PinnedType(new ByReferenceType(ParamT))));

            Int32 Index = Method.Body.Variables.Count - 1;

            Instruction LdlocFixed;
            Instruction StlocFixed;

            switch (Index)
            {
                case 0:
                    StlocFixed = ILProcessor.Create(OpCodes.Stloc_0);
                    LdlocFixed = ILProcessor.Create(OpCodes.Ldloc_0);
                    break;
                case 1:
                    StlocFixed = ILProcessor.Create(OpCodes.Stloc_1);
                    LdlocFixed = ILProcessor.Create(OpCodes.Ldloc_1);
                    break;
                case 2:
                    StlocFixed = ILProcessor.Create(OpCodes.Stloc_2);
                    LdlocFixed = ILProcessor.Create(OpCodes.Ldloc_2);
                    break;
                case 3:
                    StlocFixed = ILProcessor.Create(OpCodes.Stloc_3);
                    LdlocFixed = ILProcessor.Create(OpCodes.Ldloc_3);
                    break;
                default:
                    StlocFixed = ILProcessor.Create(OpCodes.Stloc, Index);
                    LdlocFixed = ILProcessor.Create(OpCodes.Ldloc, Index);
                    break;
            }

            Instruction InstructionLdci40 = ILProcessor.Create(OpCodes.Ldc_I4_0);
            ILProcessor.InsertBefore(FixedToPatch, InstructionLdci40);

            Instruction InstructionLdElema = ILProcessor.Create(OpCodes.Ldelema, ParamT);
            ILProcessor.InsertBefore(FixedToPatch, InstructionLdElema);
            ILProcessor.InsertBefore(FixedToPatch, StlocFixed);
            ILProcessor.Replace(FixedToPatch, LdlocFixed);
        }

        /// <summary>
        /// Creates the write range method with the following signature:
        /// <code>
        /// public static unsafe void* Write&lt;T&gt;(void* pDest, T[] data, int offset, int count) where T : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The method copy struct.</param>
        private void CreateWriteRangeMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor Gen = Method.Body.GetILProcessor();
            GenericParameter ParamT = Method.GenericParameters[0];

            // Preparing locals
            // local(0) int
            Method.Body.Variables.Add(new VariableDefinition(IntType));
            // local(1) T*
            Method.Body.Variables.Add(new VariableDefinition(new PinnedType(new ByReferenceType(ParamT))));

            // Push (0) pDest for memcpy
            Gen.Emit(OpCodes.Ldarg_0);

            // fixed (void* pinnedData = &data[offset])
            Gen.Emit(OpCodes.Ldarg_1);
            Gen.Emit(OpCodes.Ldarg_2);
            Gen.Emit(OpCodes.Ldelema, ParamT);
            Gen.Emit(OpCodes.Stloc_1);

            // Push (1) pinnedData for memcpy
            Gen.Emit(OpCodes.Ldloc_1);

            // totalSize = sizeof(T) * count
            Gen.Emit(OpCodes.Sizeof, ParamT);
            Gen.Emit(OpCodes.Conv_I4);
            Gen.Emit(OpCodes.Ldarg_3);
            Gen.Emit(OpCodes.Mul);
            Gen.Emit(OpCodes.Stloc_0);

            // Push (2) totalSize
            Gen.Emit(OpCodes.Ldloc_0);

            // Emit cpblk
            EmitCpblk(Method, Gen);

            // Return pDest + totalSize
            Gen.Emit(OpCodes.Ldloc_0);
            Gen.Emit(OpCodes.Conv_I);
            Gen.Emit(OpCodes.Ldarg_0);
            Gen.Emit(OpCodes.Add);

            // Ret
            Gen.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates the read method with the following signature:
        /// <code>
        /// public static unsafe void* Read&lt;T&gt;(void* pSrc, ref T data) where T : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The method copy struct.</param>
        private void CreateReadMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor Gen = Method.Body.GetILProcessor();
            GenericParameter ParamT = Method.GenericParameters[0];

            // Preparing locals
            // local(0) int
            Method.Body.Variables.Add(new VariableDefinition(IntType));
            // local(1) T*

            Method.Body.Variables.Add(new VariableDefinition(new PinnedType(new ByReferenceType(ParamT))));

            // fixed (void* pinnedData = &data[offset])
            Gen.Emit(OpCodes.Ldarg_1);
            Gen.Emit(OpCodes.Stloc_1);

            // Push (0) pinnedData for memcpy
            Gen.Emit(OpCodes.Ldloc_1);

            // Push (1) pSrc for memcpy
            Gen.Emit(OpCodes.Ldarg_0);

            // totalSize = sizeof(T)
            Gen.Emit(OpCodes.Sizeof, ParamT);
            Gen.Emit(OpCodes.Conv_I4);
            Gen.Emit(OpCodes.Stloc_0);

            // Push (2) totalSize
            Gen.Emit(OpCodes.Ldloc_0);

            // Emit cpblk
            EmitCpblk(Method, Gen);

            // Return pDest + totalSize
            Gen.Emit(OpCodes.Ldloc_0);
            Gen.Emit(OpCodes.Conv_I);
            Gen.Emit(OpCodes.Ldarg_0);
            Gen.Emit(OpCodes.Add);

            // Ret
            Gen.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates the read method with the following signature:
        /// <code>
        /// public static unsafe void Read&lt;T&gt;(void* pSrc, ref T data) where T : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The method copy struct.</param>
        private void CreateReadRawMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor Gen = Method.Body.GetILProcessor();
            GenericParameter ParamT = Method.GenericParameters[0];

            // Push (1) pSrc for memcpy
            Gen.Emit(OpCodes.Cpobj);
        }

        /// <summary>
        /// Creates the read range method with the following signature:
        /// <code>
        /// public static unsafe void* Read&lt;T&gt;(void* pSrc, T[] data, int offset, int count) where T : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The method copy struct.</param>
        private void CreateReadRangeMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor Gen = Method.Body.GetILProcessor();
            GenericParameter ParamT = Method.GenericParameters[0];

            // Preparing locals
            // local(0) int
            Method.Body.Variables.Add(new VariableDefinition(IntType));
            // local(1) T*
            Method.Body.Variables.Add(new VariableDefinition(new PinnedType(new ByReferenceType(ParamT))));

            // fixed (void* pinnedData = &data[offset])
            Gen.Emit(OpCodes.Ldarg_1);
            Gen.Emit(OpCodes.Ldarg_2);
            Gen.Emit(OpCodes.Ldelema, ParamT);
            Gen.Emit(OpCodes.Stloc_1);

            // Push (0) pinnedData for memcpy
            Gen.Emit(OpCodes.Ldloc_1);

            // Push (1) pDest for memcpy
            Gen.Emit(OpCodes.Ldarg_0);

            // totalSize = sizeof(T) * count
            Gen.Emit(OpCodes.Sizeof, ParamT);
            Gen.Emit(OpCodes.Conv_I4);
            Gen.Emit(OpCodes.Ldarg_3);
            Gen.Emit(OpCodes.Conv_I4);
            Gen.Emit(OpCodes.Mul);
            Gen.Emit(OpCodes.Stloc_0);

            // Push (2) totalSize
            Gen.Emit(OpCodes.Ldloc_0);

            // Emit cpblk
            EmitCpblk(Method, Gen);

            // Return pDest + totalSize
            Gen.Emit(OpCodes.Ldloc_0);
            Gen.Emit(OpCodes.Conv_I);
            Gen.Emit(OpCodes.Ldarg_0);
            Gen.Emit(OpCodes.Add);

            // Ret
            Gen.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates the memcpy method with the following signature:
        /// <code>
        /// public static unsafe void memcpy(void* pDest, void* pSrc, int count)
        /// </code>
        /// </summary>
        /// <param name="MethodCopyStruct">The method copy struct.</param>
        private void CreateMemcpy(MethodDefinition MethodCopyStruct)
        {
            MethodCopyStruct.Body.Instructions.Clear();

            ILProcessor Gen = MethodCopyStruct.Body.GetILProcessor();

            Gen.Emit(OpCodes.Ldarg_0);
            Gen.Emit(OpCodes.Ldarg_1);
            Gen.Emit(OpCodes.Ldarg_2);
            Gen.Emit(OpCodes.Unaligned, (Byte)1);
            Gen.Emit(OpCodes.Cpblk);

            // Ret
            Gen.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates the memset method with the following signature:
        /// <code>
        /// public static unsafe void memset(void* pDest, byte value, int count)
        /// </code>
        /// </summary>
        /// <param name="MethodSetStruct">The method set struct.</param>
        private void CreateMemset(MethodDefinition MethodSetStruct)
        {
            MethodSetStruct.Body.Instructions.Clear();

            ILProcessor Gen = MethodSetStruct.Body.GetILProcessor();

            Gen.Emit(OpCodes.Ldarg_0);
            Gen.Emit(OpCodes.Ldarg_1);
            Gen.Emit(OpCodes.Ldarg_2);
            Gen.Emit(OpCodes.Unaligned, (Byte)1);
            Gen.Emit(OpCodes.Initblk);

            // Ret
            Gen.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Emits the cpblk method, supporting x86 and x64 platform.
        /// </summary>
        /// <param name="Method">The method.</param>
        /// <param name="Gen">The gen.</param>
        private void EmitCpblk(MethodDefinition Method, ILProcessor Gen)
        {
            Instruction Cpblk = Gen.Create(OpCodes.Cpblk);
            // Gen.Emit(OpCodes.Sizeof, VoidPointerType);
            // Gen.Emit(OpCodes.Ldc_I4_8);
            // Gen.Emit(OpCodes.Bne_Un_S, Cpblk);
            Gen.Emit(OpCodes.Unaligned, (Byte)1);
            Gen.Append(Cpblk);

        }

        private List<String> GetSharpDXAttributes(MethodDefinition Method)
        {
            List<String> Attributes = new List<String>();
            foreach (CustomAttribute CustomAttribute in Method.CustomAttributes)
            {
                if (CustomAttribute.AttributeType.FullName == "SharpDX.TagAttribute")
                {
                    Object Value = CustomAttribute.ConstructorArguments[0].Value;
                    Attributes.Add(Value == null ? String.Empty : Value.ToString());
                }
            }

            return Attributes;
        }

        /// <summary>
        /// Patches the method.
        /// </summary>
        /// <param name="Method">The method.</param>
        bool PatchMethod(MethodDefinition Method)
        {
            Boolean IsSharpJit = false;

            List<String> Attributes = GetSharpDXAttributes(Method);
            if (Attributes.Contains("SharpDX.ModuleInit"))
            {
                CreateModuleInit(Method);
            }

            if (Method.DeclaringType.Name == "Interop")
            {
                if (Method.Name == "memcpy")
                {
                    CreateMemcpy(Method);
                }
                else if (Method.Name == "memset")
                {
                    CreateMemset(Method);
                }
                else if ((Method.Name == "Cast") || (Method.Name == "CastOut"))
                {
                    CreateCastMethod(Method);
                }
                else if (Method.Name == "CastArray")
                {
                    CreateCastArrayMethod(Method);
                }
                else if (Method.Name == "Read" || (Method.Name == "ReadOut") || (Method.Name == "Read2D"))
                {
                    if (Method.Parameters.Count == 2)
                        CreateReadMethod(Method);
                    else
                        CreateReadRangeMethod(Method);
                }
                else if (Method.Name == "Write" || (Method.Name == "Write2D"))
                {
                    if (Method.Parameters.Count == 2)
                        CreateWriteMethod(Method);
                    else
                        CreateWriteRangeMethod(Method);
                }

                return IsSharpJit;
            }

            if (!Method.HasBody)
                return IsSharpJit;

            ILProcessor ILProcessor = Method.Body.GetILProcessor();
            ICollection<Instruction> Instructions = Method.Body.Instructions;
            Instruction Instruction = null;
            Instruction PreviousInstruction;

            for (Int32 Index = 0; Index < Instructions.Count; Index++)
            {
                PreviousInstruction = Instruction;
                Instruction = Instructions.ElementAt(Index);

                if (Instruction.OpCode != OpCodes.Call || !(Instruction.Operand is MethodReference))
                    continue;

                MethodReference MethodDescription = (MethodReference)Instruction.Operand;

                if (MethodDescription is MethodDefinition)
                {
                    foreach (CustomAttribute CustomAttribute in ((MethodDefinition)MethodDescription).CustomAttributes)
                    {
                        if (CustomAttribute.AttributeType.FullName == typeof(ObfuscationAttribute).FullName)
                        {
                            foreach (Mono.Cecil.CustomAttributeNamedArgument Arg in CustomAttribute.Properties)
                            {
                                if (Arg.Name != "Feature" || Arg.Argument.Value == null)
                                    continue;

                                String CustomValue = Arg.Argument.Value.ToString();
                                if (CustomValue.StartsWith("SharpJit."))
                                {
                                    IsSharpJit = true;
                                    break;
                                }
                            }
                        }
                        if (IsSharpJit)
                            break;
                    }
                }

                if (!IsSharpJit)
                {
                    if (MethodDescription.Name.StartsWith("Calli") && MethodDescription.DeclaringType.Name == "LocalInterop")
                    {
                        CallSite CallSite = new CallSite(MethodDescription.ReturnType) { CallingConvention = MethodCallingConvention.StdCall };

                        // Last parameter is the function ptr, so we don't add it as a parameter for calli
                        // as it is already an implicit parameter for calli
                        for (Int32 MethodParamIndex = 0; MethodParamIndex < MethodDescription.Parameters.Count - 1; MethodParamIndex++)
                        {
                            ParameterDefinition ParameterDefinition = MethodDescription.Parameters[MethodParamIndex];
                            CallSite.Parameters.Add(ParameterDefinition);
                        }

                        // Create calli Instruction
                        Instruction CallIInstruction = ILProcessor.Create(OpCodes.Calli, CallSite);

                        // Replace instruction
                        ILProcessor.Replace(Instruction, CallIInstruction);
                    }
                    else if (MethodDescription.DeclaringType.Name == "Interop")
                    {
                        if (MethodDescription.FullName.Contains("Fixed"))
                        {
                            if (MethodDescription.Parameters[0].ParameterType.IsArray)
                            {
                                ReplaceFixedArrayStatement(Method, ILProcessor, Instruction);
                            }
                            else
                            {
                                ReplaceFixedStatement(Method, ILProcessor, Instruction);
                            }
                        }
                        else if (MethodDescription.Name.StartsWith("ReadInline"))
                        {
                            ReplaceReadInline(Method, ILProcessor, Instruction);
                        }
                        else if (MethodDescription.Name.StartsWith("CopyInline") || MethodDescription.Name.StartsWith("WriteInline"))
                        {
                            ReplaceCopyInline(Method, ILProcessor, Instruction);
                        }
                        else if (MethodDescription.Name.StartsWith("SizeOf"))
                        {
                            ReplaceSizeOfStructGeneric(Method, ILProcessor, Instruction);
                        }
                    }
                }
            }

            return IsSharpJit;
        }


        /// <summary>
        /// Patches the type.
        /// </summary>
        /// <param name="Type">The type.</param>
        void PatchType(TypeDefinition Type)
        {
            // Patch methods
            foreach (MethodDefinition Method in Type.Methods)
                if (PatchMethod(Method))
                    ContainsSharpJit = true;

            // LocalInterop will be removed after the patch only for non SharpJit code
            if (!ContainsSharpJit && Type.Name == "LocalInterop")
                ClassToRemoveList.Add(Type);

            // Patch nested types
            foreach (TypeDefinition TypeDefinition in Type.NestedTypes)
                PatchType(TypeDefinition);
        }

        /// <summary>
        /// Determines whether [is file check updated] [the specified file].
        /// </summary>
        /// <param name="TargetFile">The file.</param>
        /// <param name="FromFile">From file.</param>
        /// <returns>
        /// 	<c>true</c> if [is file check updated] [the specified file]; otherwise, <c>false</c>.
        /// </returns>
        static bool IsFileCheckUpdated(String TargetFile, String FromFile)
        {
            return File.Exists(TargetFile) && File.GetLastWriteTime(TargetFile) == File.GetLastWriteTime(FromFile);
        }

        /// <summary>
        /// Get Program Files x86
        /// </summary>
        /// <returns></returns>
        static String ProgramFilesx86()
        {
            if (IntPtr.Size == 8 || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432")))
                || Environment.GetEnvironmentVariable("ProgramFiles(x86)") != null)
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        /// <summary>
        /// Patches the file.
        /// </summary>
        /// <param name="TargetFile">The file.</param>
        public bool PatchFile(String TargetFile)
        {
            TargetFile = Path.Combine(Environment.CurrentDirectory, TargetFile);

            FileTime FileTime = new FileTime(TargetFile);
            // FileTime FileTimeInteropBuilder = new FileTime(Assembly.GetExecutingAssembly().Location);
            String checkFile = Path.GetFullPath(TargetFile) + ".check";
            // String checkInteropBuilderFile = "InteropBuild.check";

            // If checkFile and checkInteropBuilderFile up-to-date, then nothing to do
            if (FileTime.CheckFileUpToDate(checkFile))
            {
                Log("Nothing to do. SharpDX patch was already applied for assembly [{0}]", TargetFile);
                return false;
            }

            // Copy PDB from input assembly to output assembly if any
            ReaderParameters ReaderParameters = new ReaderParameters();
            DefaultAssemblyResolver Resolver = new DefaultAssemblyResolver();
            WriterParameters WriterParameters = new WriterParameters();
            String PDBName = Path.ChangeExtension(TargetFile, "pdb");
            ReaderParameters.AssemblyResolver = Resolver;

            if (File.Exists(PDBName))
            {
                PdbReaderProvider SymbolReaderProvider = new PdbReaderProvider();
                ReaderParameters.SymbolReaderProvider = SymbolReaderProvider;
                ReaderParameters.ReadSymbols = true;
                WriterParameters.WriteSymbols = true;
            }

            // Read Assembly
            AssemblyDefinition = AssemblyDefinition.ReadAssembly(TargetFile, ReaderParameters);
            Resolver.AddSearchDirectory(Path.GetDirectoryName(TargetFile));

            // Query the target framework in order to resolve correct assemblies and type forwarding
            CustomAttribute TargetFrameworkAttr = AssemblyDefinition.CustomAttributes.FirstOrDefault(
                Attribute => Attribute.Constructor.FullName.Contains("System.Runtime.Versioning.TargetFrameworkAttribute"));

            if (TargetFrameworkAttr != null && TargetFrameworkAttr.ConstructorArguments.Count > 0 &&
                TargetFrameworkAttr.ConstructorArguments[0].Value != null)
            {
                FrameworkName targetFramework = new FrameworkName(TargetFrameworkAttr.ConstructorArguments[0].Value.ToString());

                String NetcoreAssemblyPath = String.Format(@"Reference Assemblies\Microsoft\Framework\{0}\v{1}",
                    targetFramework.Identifier, targetFramework.Version);

                NetcoreAssemblyPath = Path.Combine(ProgramFilesx86(), NetcoreAssemblyPath);

                if (Directory.Exists(NetcoreAssemblyPath))
                    Resolver.AddSearchDirectory(NetcoreAssemblyPath);
            }

            // Import void* and Int32 
            VoidType = AssemblyDefinition.MainModule.TypeSystem.Void.Resolve();
            VoidPointerType = new PointerType(AssemblyDefinition.MainModule.Import(VoidType));
            IntType = AssemblyDefinition.MainModule.Import(AssemblyDefinition.MainModule.TypeSystem.Int32.Resolve());

            // Remove CompilationRelaxationsAttribute
            for (Int32 Index = 0; Index < AssemblyDefinition.CustomAttributes.Count; Index++)
            {
                CustomAttribute CustomAttribute = AssemblyDefinition.CustomAttributes[Index];
                if (CustomAttribute.AttributeType.FullName == typeof(CompilationRelaxationsAttribute).FullName)
                {
                    AssemblyDefinition.CustomAttributes.RemoveAt(Index);
                    Index--;
                }
            }

            Log("SharpDX interop patch for assembly [{0}]", TargetFile);
            foreach (TypeDefinition Type in AssemblyDefinition.MainModule.Types)
                PatchType(Type);

            // Remove All Interop classes
            foreach (TypeDefinition Type in ClassToRemoveList)
                AssemblyDefinition.MainModule.Types.Remove(Type);

            String OutputFilePath = TargetFile;
            AssemblyDefinition.Write(OutputFilePath, WriterParameters);

            FileTime = new FileTime(TargetFile);

            // Update Check file
            FileTime.UpdateCheckFile(checkFile);

            // FileTimeInteropBuilder.UpdateCheckFile(CheckInteropBuilderFile);

            Log("SharpDX patch done for assembly [{0}]", TargetFile);

            return true;
        }

        public void Log(String Message, params Object[] Parameters)
        {
            Console.WriteLine(Message, Parameters);
        }

        public void LogError(String Message, params Object[] Parameters)
        {
            Console.WriteLine(Message, Parameters);
        }

        public void LogError(Exception Ex)
        {
            Console.WriteLine(Ex.ToString());
        }

        /// <summary>
        /// FileTime.
        /// </summary>
        class FileTime
        {
            private DateTime CreateTime;
            private DateTime LastAccessTime;
            private DateTime LastWriteTime;

            public FileTime(String FileName)
            {
                CreateTime = File.GetCreationTime(FileName);
                LastAccessTime = File.GetLastAccessTime(FileName);
                LastWriteTime = File.GetLastWriteTime(FileName);
            }

            public void UpdateCheckFile(String CheckFile)
            {
                File.WriteAllText(CheckFile, "");
                UpdateFile(CheckFile);
            }

            /// <summary>
            /// Checks the file.
            /// </summary>
            /// <param name="Checkfile">The file to check.</param>
            /// <returns>true if the file exist and has the same LastWriteTime </returns>
            public Boolean CheckFileUpToDate(String Checkfile)
            {
                return File.Exists(Checkfile) && File.GetLastWriteTime(Checkfile) == LastWriteTime;
            }

            public void UpdateFile(String FileName)
            {
                File.SetCreationTime(FileName, CreateTime);
                File.SetLastWriteTime(FileName, LastWriteTime);
                File.SetLastAccessTime(FileName, LastAccessTime);
            }
        }

    } // End class

} // End namespace