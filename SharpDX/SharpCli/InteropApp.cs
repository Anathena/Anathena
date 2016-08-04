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
using Mono.Collections.Generic;
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
    /// InteropBuilder is also adding several useful Methods:
    /// - memcpy using cpblk
    /// - Read/ReadRange/Write/WriteRange of structured data to a memory location
    /// - SizeOf on IlProcessoreric structures (C# usually doesn't allow this).
    /// </summary>
    public class InteropApp
    {
        private List<TypeDefinition> ClassToRemoveList = new List<TypeDefinition>();
        private AssemblyDefinition Assembly;
        private TypeReference VoidType;
        private TypeReference VoidPointerType;
        private TypeReference IntType;
        private Boolean ContainsSharpJit;
        private String ExecutingDirectory;

        /// <summary>
        /// Main of this program.
        /// </summary>
        /// <param name="args">The args.</param>
        public InteropApp(String ExecutingDirectory)
        {
            this.ExecutingDirectory = ExecutingDirectory;
        }

        public void PatchAll()
        {
            String[] Libraries = new String[]
            {
                "SharpDX.dll",
                "SharpDX.Animation.dll",
                "SharpDX.D3DCompiler.dll",
                "SharpDX.Desktop.dll",
                "SharpDX.Direct2D1.dll",
                "SharpDX.Direct3D9.dll",
                "SharpDX.Direct3D11.dll",
                "SharpDX.Direct3D11.Effects.dll",
                "SharpDX.Direct3D12.dll",
                "SharpDX.DirectComposition.dll",
                "SharpDX.DirectInput.dll",
                "SharpDX.DirectManipulation.dll",
                "SharpDX.DirectSound.dll",
                "SharpDX.DXGI.dll",
                "SharpDX.Mathematics.dll",
                "SharpDX.MediaFoundation.dll",
                "SharpDX.RawInput.dll",
                "SharpDX.XAudio2.dll",
                "SharpDX.XInput.dll"
            };

            try
            {
                foreach (String Library in Libraries)
                    PatchFile(Path.Combine(ExecutingDirectory, Library));
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Creates a module init for a C# assembly.
        /// </summary>
        /// <param name="Method">The Method to add to the module init.</param>
        private void CreateModuleInit(MethodDefinition Method)
        {
            MethodAttributes ModuleInitAttributes = MethodAttributes.Static | MethodAttributes.Assembly | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
            TypeDefinition ModuleType = Assembly.MainModule.GetType("<Module>");

            // Get or create ModuleInit Method
            MethodDefinition Cctor = ModuleType.Methods.FirstOrDefault(X => X.Name == ".cctor");

            if (Cctor == null)
            {
                Cctor = new MethodDefinition(".cctor", ModuleInitAttributes, Method.ReturnType);
                ModuleType.Methods.Add(Cctor);
            }

            Boolean IsCallAlreadyDone = Cctor.Body.Instructions.Any(X => X.OpCode == OpCodes.Call && X.Operand == Method);

            // If the Method is not called, we can add it
            if (IsCallAlreadyDone)
                return;

            ILProcessor IlProcessor = Cctor.Body.GetILProcessor();
            var RetInstruction = Cctor.Body.Instructions.FirstOrDefault(X => X.OpCode == OpCodes.Ret);
            var CallMethod = IlProcessor.Create(OpCodes.Call, Method);

            if (RetInstruction == null)
            {
                // If a ret instruction is not present, add the Method call and ret
                IlProcessor.Append(CallMethod);
                IlProcessor.Emit(OpCodes.Ret);
            }
            else
            {
                // If a ret instruction is already present, just add the Method to call before
                IlProcessor.InsertBefore(RetInstruction, CallMethod);
            }
        }

        /// <summary>
        /// Creates the write Method with the following signature: 
        /// <code>
        /// public static unsafe void* Write&lt;T&gt;(void* pDest, ref T data) where T : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The Method to patch</param>
        private void CreateWriteMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor IlProcessor = Method.Body.GetILProcessor();
            GenericParameter ParamT = Method.GenericParameters[0];

            // Preparing locals
            // local(0) int
            Method.Body.Variables.Add(new VariableDefinition(IntType));
            // local(1) T*
            Method.Body.Variables.Add(new VariableDefinition(new PinnedType(new ByReferenceType(ParamT))));

            // Push (0) pDest for memcpy
            IlProcessor.Emit(OpCodes.Ldarg_0);

            // fixed (void* pinnedData = &data[offset])
            IlProcessor.Emit(OpCodes.Ldarg_1);
            IlProcessor.Emit(OpCodes.Stloc_1);

            // Push (1) pinnedData for memcpy
            IlProcessor.Emit(OpCodes.Ldloc_1);

            // totalSize = sizeof(T)
            IlProcessor.Emit(OpCodes.Sizeof, ParamT);
            IlProcessor.Emit(OpCodes.Conv_I4);
            IlProcessor.Emit(OpCodes.Stloc_0);

            // Push (2) totalSize
            IlProcessor.Emit(OpCodes.Ldloc_0);

            // Emit cpblk
            EmitCpblk(Method, IlProcessor);

            // Return pDest + totalSize
            IlProcessor.Emit(OpCodes.Ldloc_0);
            IlProcessor.Emit(OpCodes.Conv_I);
            IlProcessor.Emit(OpCodes.Ldarg_0);
            IlProcessor.Emit(OpCodes.Add);

            // Ret
            IlProcessor.Emit(OpCodes.Ret);
        }

        private void ReplaceFixedStatement(MethodDefinition Method, ILProcessor IlProcessor, Instruction FixedtoPatch)
        {
            TypeReference ParamT = ((GenericInstanceMethod)FixedtoPatch.Operand).GenericArguments[0];

            // Preparing locals
            // local(0) T*
            Method.Body.Variables.Add(new VariableDefinition("pin", new PinnedType(new ByReferenceType(ParamT))));

            Int32 Index = Method.Body.Variables.Count - 1;

            Instruction LdlocFixed;
            Instruction StlocFixed;
            switch (Index)
            {
                case 0:
                    StlocFixed = IlProcessor.Create(OpCodes.Stloc_0);
                    LdlocFixed = IlProcessor.Create(OpCodes.Ldloc_0);
                    break;
                case 1:
                    StlocFixed = IlProcessor.Create(OpCodes.Stloc_1);
                    LdlocFixed = IlProcessor.Create(OpCodes.Ldloc_1);
                    break;
                case 2:
                    StlocFixed = IlProcessor.Create(OpCodes.Stloc_2);
                    LdlocFixed = IlProcessor.Create(OpCodes.Ldloc_2);
                    break;
                case 3:
                    StlocFixed = IlProcessor.Create(OpCodes.Stloc_3);
                    LdlocFixed = IlProcessor.Create(OpCodes.Ldloc_3);
                    break;
                default:
                    StlocFixed = IlProcessor.Create(OpCodes.Stloc, Index);
                    LdlocFixed = IlProcessor.Create(OpCodes.Ldloc, Index);
                    break;
            }

            IlProcessor.InsertBefore(FixedtoPatch, StlocFixed);
            IlProcessor.Replace(FixedtoPatch, LdlocFixed);
        }

        private void ReplaceReadInline(MethodDefinition Method, ILProcessor IlProcessor, Instruction FixedtoPatch)
        {
            TypeReference ParamT = ((GenericInstanceMethod)FixedtoPatch.Operand).GenericArguments[0];
            Instruction CopyInstruction = IlProcessor.Create(OpCodes.Ldobj, ParamT);

            IlProcessor.Replace(FixedtoPatch, CopyInstruction);
        }

        private void ReplaceCopyInline(MethodDefinition Method, ILProcessor IlProcessor, Instruction FixedtoPatch)
        {
            TypeReference ParamT = ((GenericInstanceMethod)FixedtoPatch.Operand).GenericArguments[0];
            Instruction CopyInstruction = IlProcessor.Create(OpCodes.Cpobj, ParamT);

            IlProcessor.Replace(FixedtoPatch, CopyInstruction);
        }

        private void ReplaceSizeOfStructGeneric(MethodDefinition Method, ILProcessor IlProcessor, Instruction FixedtoPatch)
        {
            TypeReference ParamT = ((GenericInstanceMethod)FixedtoPatch.Operand).GenericArguments[0];
            Instruction CopyInstruction = IlProcessor.Create(OpCodes.Sizeof, ParamT);

            IlProcessor.Replace(FixedtoPatch, CopyInstruction);
        }

        /// <summary>
        /// Creates the cast  Method with the following signature:
        /// <code>
        /// public static unsafe void* Cast&lt;T&gt;(ref T data) where T : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The Method cast.</param>
        private void CreateCastMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor IlProcessor = Method.Body.GetILProcessor();

            IlProcessor.Emit(OpCodes.Ldarg_0);
            IlProcessor.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates the cast  Method with the following signature:
        /// <code>
        /// public static TCAST[] CastArray&lt;TCAST, T&gt;(T[] arrayData) where T : struct where TCAST : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The Method cast array.</param>
        private void CreateCastArrayMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor IlProcessor = Method.Body.GetILProcessor();

            IlProcessor.Emit(OpCodes.Ldarg_0);
            IlProcessor.Emit(OpCodes.Ret);
        }

        private void ReplaceFixedArrayStatement(MethodDefinition Method, ILProcessor IlProcessor, Instruction fixedtoPatch)
        {
            TypeReference ParamT = ((GenericInstanceMethod)fixedtoPatch.Operand).GenericArguments[0];

            // Preparing locals
            // local(0) T*
            Method.Body.Variables.Add(new VariableDefinition("pin", new PinnedType(new ByReferenceType(ParamT))));

            Int32 Index = Method.Body.Variables.Count - 1;

            Instruction LdlocFixed;
            Instruction StlocFixed;

            switch (Index)
            {
                case 0:
                    StlocFixed = IlProcessor.Create(OpCodes.Stloc_0);
                    LdlocFixed = IlProcessor.Create(OpCodes.Ldloc_0);
                    break;
                case 1:
                    StlocFixed = IlProcessor.Create(OpCodes.Stloc_1);
                    LdlocFixed = IlProcessor.Create(OpCodes.Ldloc_1);
                    break;
                case 2:
                    StlocFixed = IlProcessor.Create(OpCodes.Stloc_2);
                    LdlocFixed = IlProcessor.Create(OpCodes.Ldloc_2);
                    break;
                case 3:
                    StlocFixed = IlProcessor.Create(OpCodes.Stloc_3);
                    LdlocFixed = IlProcessor.Create(OpCodes.Ldloc_3);
                    break;
                default:
                    StlocFixed = IlProcessor.Create(OpCodes.Stloc, Index);
                    LdlocFixed = IlProcessor.Create(OpCodes.Ldloc, Index);
                    break;
            }

            Instruction InstructionLdci40 = IlProcessor.Create(OpCodes.Ldc_I4_0);
            IlProcessor.InsertBefore(fixedtoPatch, InstructionLdci40);

            Instruction InstructionLdElema = IlProcessor.Create(OpCodes.Ldelema, ParamT);
            IlProcessor.InsertBefore(fixedtoPatch, InstructionLdElema);
            IlProcessor.InsertBefore(fixedtoPatch, StlocFixed);
            IlProcessor.Replace(fixedtoPatch, LdlocFixed);
        }

        /// <summary>
        /// Creates the write range Method with the following signature:
        /// <code>
        /// public static unsafe void* Write&lt;T&gt;(void* pDest, T[] data, Int32offset, Int32count) where T : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The Method copy struct.</param>
        private void CreateWriteRangeMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor IlProcessor = Method.Body.GetILProcessor();
            GenericParameter ParamT = Method.GenericParameters[0];

            // Preparing locals
            // local(0) int
            Method.Body.Variables.Add(new VariableDefinition(IntType));
            // local(1) T*
            Method.Body.Variables.Add(new VariableDefinition(new PinnedType(new ByReferenceType(ParamT))));

            // Push (0) pDest for memcpy
            IlProcessor.Emit(OpCodes.Ldarg_0);

            // fixed (void* pinnedData = &data[offset])
            IlProcessor.Emit(OpCodes.Ldarg_1);
            IlProcessor.Emit(OpCodes.Ldarg_2);
            IlProcessor.Emit(OpCodes.Ldelema, ParamT);
            IlProcessor.Emit(OpCodes.Stloc_1);

            // Push (1) pinnedData for memcpy
            IlProcessor.Emit(OpCodes.Ldloc_1);

            // totalSize = sizeof(T) * count
            IlProcessor.Emit(OpCodes.Sizeof, ParamT);
            IlProcessor.Emit(OpCodes.Conv_I4);
            IlProcessor.Emit(OpCodes.Ldarg_3);
            IlProcessor.Emit(OpCodes.Mul);
            IlProcessor.Emit(OpCodes.Stloc_0);

            // Push (2) totalSize
            IlProcessor.Emit(OpCodes.Ldloc_0);

            // Emit cpblk
            EmitCpblk(Method, IlProcessor);

            // Return pDest + totalSize
            IlProcessor.Emit(OpCodes.Ldloc_0);
            IlProcessor.Emit(OpCodes.Conv_I);
            IlProcessor.Emit(OpCodes.Ldarg_0);
            IlProcessor.Emit(OpCodes.Add);
            IlProcessor.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates the read Method with the following signature:
        /// <code>
        /// public static unsafe void* Read&lt;T&gt;(void* pSrc, ref T data) where T : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The Method copy struct.</param>
        private void CreateReadMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor IlProcessor = Method.Body.GetILProcessor();
            GenericParameter ParamT = Method.GenericParameters[0];

            // Preparing locals
            // local(0) int
            Method.Body.Variables.Add(new VariableDefinition(IntType));
            // local(1) T*

            Method.Body.Variables.Add(new VariableDefinition(new PinnedType(new ByReferenceType(ParamT))));

            // fixed (void* pinnedData = &data[offset])
            IlProcessor.Emit(OpCodes.Ldarg_1);
            IlProcessor.Emit(OpCodes.Stloc_1);

            // Push (0) pinnedData for memcpy
            IlProcessor.Emit(OpCodes.Ldloc_1);

            // Push (1) pSrc for memcpy
            IlProcessor.Emit(OpCodes.Ldarg_0);

            // totalSize = sizeof(T)
            IlProcessor.Emit(OpCodes.Sizeof, ParamT);
            IlProcessor.Emit(OpCodes.Conv_I4);
            IlProcessor.Emit(OpCodes.Stloc_0);

            // Push (2) totalSize
            IlProcessor.Emit(OpCodes.Ldloc_0);

            // Emit cpblk
            EmitCpblk(Method, IlProcessor);

            // Return pDest + totalSize
            IlProcessor.Emit(OpCodes.Ldloc_0);
            IlProcessor.Emit(OpCodes.Conv_I);
            IlProcessor.Emit(OpCodes.Ldarg_0);
            IlProcessor.Emit(OpCodes.Add);
            IlProcessor.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates the read Method with the following signature:
        /// <code>
        /// public static unsafe void Read&lt;T&gt;(void* pSrc, ref T data) where T : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The Method copy struct.</param>
        private void CreateReadRawMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor IlProcessor = Method.Body.GetILProcessor();
            var ParamT = Method.GenericParameters[0];

            // Push (1) pSrc for memcpy
            IlProcessor.Emit(OpCodes.Cpobj);
        }

        /// <summary>
        /// Creates the read range Method with the following signature:
        /// <code>
        /// public static unsafe void* Read&lt;T&gt;(void* pSrc, T[] data, Int32offset, Int32count) where T : struct
        /// </code>
        /// </summary>
        /// <param name="Method">The Method copy struct.</param>
        private void CreateReadRangeMethod(MethodDefinition Method)
        {
            Method.Body.Instructions.Clear();
            Method.Body.InitLocals = true;

            ILProcessor IlProcessor = Method.Body.GetILProcessor();
            GenericParameter ParamT = Method.GenericParameters[0];

            // Preparing locals
            // local(0) int
            Method.Body.Variables.Add(new VariableDefinition(IntType));
            // local(1) T*
            Method.Body.Variables.Add(new VariableDefinition(new PinnedType(new ByReferenceType(ParamT))));

            // fixed (void* pinnedData = &data[offset])
            IlProcessor.Emit(OpCodes.Ldarg_1);
            IlProcessor.Emit(OpCodes.Ldarg_2);
            IlProcessor.Emit(OpCodes.Ldelema, ParamT);
            IlProcessor.Emit(OpCodes.Stloc_1);

            // Push (0) pinnedData for memcpy
            IlProcessor.Emit(OpCodes.Ldloc_1);

            // Push (1) pDest for memcpy
            IlProcessor.Emit(OpCodes.Ldarg_0);

            // totalSize = sizeof(T) * count
            IlProcessor.Emit(OpCodes.Sizeof, ParamT);
            IlProcessor.Emit(OpCodes.Conv_I4);
            IlProcessor.Emit(OpCodes.Ldarg_3);
            IlProcessor.Emit(OpCodes.Conv_I4);
            IlProcessor.Emit(OpCodes.Mul);
            IlProcessor.Emit(OpCodes.Stloc_0);

            // Push (2) totalSize
            IlProcessor.Emit(OpCodes.Ldloc_0);

            // Emit cpblk
            EmitCpblk(Method, IlProcessor);

            // Return pDest + totalSize
            IlProcessor.Emit(OpCodes.Ldloc_0);
            IlProcessor.Emit(OpCodes.Conv_I);
            IlProcessor.Emit(OpCodes.Ldarg_0);
            IlProcessor.Emit(OpCodes.Add);
            IlProcessor.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates the memcpy Method with the following signature:
        /// <code>
        /// public static unsafe void memcpy(void* pDest, void* pSrc, Int32count)
        /// </code>
        /// </summary>
        /// <param name="MethodCopyStruct">The Method copy struct.</param>
        private void CreateMemcpy(MethodDefinition MethodCopyStruct)
        {
            MethodCopyStruct.Body.Instructions.Clear();

            ILProcessor IlProcessor = MethodCopyStruct.Body.GetILProcessor();

            IlProcessor.Emit(OpCodes.Ldarg_0);
            IlProcessor.Emit(OpCodes.Ldarg_1);
            IlProcessor.Emit(OpCodes.Ldarg_2);
            IlProcessor.Emit(OpCodes.Unaligned, (Byte)1);
            IlProcessor.Emit(OpCodes.Cpblk);
            IlProcessor.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Creates the memset Method with the following signature:
        /// <code>
        /// public static unsafe void memset(void* pDest, Byte value, Int32count)
        /// </code>
        /// </summary>
        /// <param name="MethodSetStruct">The Method set struct.</param>
        private void CreateMemset(MethodDefinition MethodSetStruct)
        {
            MethodSetStruct.Body.Instructions.Clear();

            ILProcessor IlProcessor = MethodSetStruct.Body.GetILProcessor();

            IlProcessor.Emit(OpCodes.Ldarg_0);
            IlProcessor.Emit(OpCodes.Ldarg_1);
            IlProcessor.Emit(OpCodes.Ldarg_2);
            IlProcessor.Emit(OpCodes.Unaligned, (Byte)1);
            IlProcessor.Emit(OpCodes.Initblk);
            IlProcessor.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Emits the cpblk Method, supporting x86 and x64 platform.
        /// </summary>
        /// <param name="Method">The Method.</param>
        /// <param name="IlProcessor">The IlProcessor.</param>
        private void EmitCpblk(MethodDefinition Method, ILProcessor IlProcessor)
        {
            Instruction Cpblk = IlProcessor.Create(OpCodes.Cpblk);
            // IlProcessor.Emit(OpCodes.Sizeof, voidPointerType);
            // IlProcessor.Emit(OpCodes.Ldc_I4_8);
            // IlProcessor.Emit(OpCodes.Bne_Un_S, cpblk);
            IlProcessor.Emit(OpCodes.Unaligned, (Byte)1);
            IlProcessor.Append(Cpblk);

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
        /// Patches the Method.
        /// </summary>
        /// <param name="Method">The Method.</param>
        Boolean PatchMethod(MethodDefinition Method)
        {
            Boolean IsSharpJit = false;
            List<String> Attributes = this.GetSharpDXAttributes(Method);

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
            }
            else if (Method.HasBody)
            {
                ILProcessor IlProcessor = Method.Body.GetILProcessor();
                Collection<Instruction> instructions = Method.Body.Instructions;
                Instruction Instruction = null;
                Instruction previousInstruction;

                for (Int32 Index = 0; Index < instructions.Count; Index++)
                {
                    previousInstruction = Instruction;
                    Instruction = instructions[Index];

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
                                    if (Arg.Name == "Feature" && Arg.Argument.Value != null)
                                    {
                                        var customValue = Arg.Argument.Value.ToString();
                                        if (customValue.StartsWith("SharpJit."))
                                        {
                                            IsSharpJit = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (IsSharpJit) break;
                        }
                    }

                    if (!IsSharpJit)
                    {
                        if (MethodDescription.Name.StartsWith("Calli") && MethodDescription.DeclaringType.Name == "LocalInterop")
                        {
                            CallSite CallSite = new CallSite(MethodDescription.ReturnType) { CallingConvention = MethodCallingConvention.StdCall };

                            // Last parameter is the function ptr, so we don't add it as a parameter for calli
                            // as it is already an implicit parameter for calli
                            for (Int32 ParameterIndex = 0; ParameterIndex < MethodDescription.Parameters.Count - 1; ParameterIndex++)
                            {
                                ParameterDefinition ParameterDefinition = MethodDescription.Parameters[ParameterIndex];
                                CallSite.Parameters.Add(ParameterDefinition);
                            }

                            // Create calli Instruction
                            Instruction CallIInstruction = IlProcessor.Create(OpCodes.Calli, CallSite);

                            // Replace instruction
                            IlProcessor.Replace(Instruction, CallIInstruction);
                        }
                        else if (MethodDescription.DeclaringType.Name == "Interop")
                        {
                            if (MethodDescription.FullName.Contains("Fixed"))
                            {
                                if (MethodDescription.Parameters[0].ParameterType.IsArray)
                                {
                                    ReplaceFixedArrayStatement(Method, IlProcessor, Instruction);
                                }
                                else
                                {
                                    ReplaceFixedStatement(Method, IlProcessor, Instruction);
                                }
                            }
                            else if (MethodDescription.Name.StartsWith("ReadInline"))
                            {
                                this.ReplaceReadInline(Method, IlProcessor, Instruction);
                            }
                            else if (MethodDescription.Name.StartsWith("CopyInline") || MethodDescription.Name.StartsWith("WriteInline"))
                            {
                                this.ReplaceCopyInline(Method, IlProcessor, Instruction);
                            }
                            else if (MethodDescription.Name.StartsWith("SizeOf"))
                            {
                                this.ReplaceSizeOfStructGeneric(Method, IlProcessor, Instruction);
                            }
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
            // Patch Methods
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
        /// <param name="ToFile">The file.</param>
        /// <param name="FromFile">From file.</param>
        /// <returns>
        /// 	<c>true</c> if [is file check updated] [the specified file]; otherwise, <c>false</c>.
        /// </returns>
        static Boolean IsFileCheckUpdated(String ToFile, String FromFile)
        {
            return File.Exists(ToFile) && File.GetLastWriteTime(ToFile) == File.GetLastWriteTime(FromFile);
        }

        /// <summary>
        /// Get Program Files x86
        /// </summary>
        /// <returns></returns>
        static String ProgramFilesx86()
        {
            if (IntPtr.Size == 8
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432")))
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
        public Boolean PatchFile(String TargetFile)
        {
            TargetFile = Path.Combine(Environment.CurrentDirectory, TargetFile);

            FileTime FileTime = new FileTime(TargetFile);
            // var FileTimeInteropBuilder = new FileTime(Assembly.GetExecutingAssembly().Location);
            String CheckFile = Path.GetFullPath(TargetFile) + ".check";
            // String checkInteropBuilderFile = "InteropBuild.check";

            // If checkFile and checkInteropBuilderFile up-to-date, then nothing to do
            if (FileTime.CheckFileUpToDate(CheckFile))
            {
                Log("Nothing to do. SharpDX patch was already applied for assembly [{0}]", TargetFile);
                return false;
            }

            // Copy PDB from input assembly to output assembly if any
            ReaderParameters ReaderParameters = new ReaderParameters();
            DefaultAssemblyResolver Resolver = new DefaultAssemblyResolver();
            ReaderParameters.AssemblyResolver = Resolver;
            WriterParameters WriterParameters = new WriterParameters();
            String PdbName = Path.ChangeExtension(TargetFile, "pdb");

            if (File.Exists(PdbName))
            {
                PdbReaderProvider SymbolReaderProvider = new PdbReaderProvider();
                ReaderParameters.SymbolReaderProvider = SymbolReaderProvider;
                ReaderParameters.ReadSymbols = true;
                WriterParameters.WriteSymbols = true;
            }

            // Read Assembly
            Assembly = AssemblyDefinition.ReadAssembly(TargetFile, ReaderParameters);
            Resolver.AddSearchDirectory(Path.GetDirectoryName(TargetFile));

            // Query the target framework in order to resolve correct assemblies and type forwarding
            CustomAttribute TargetFrameworkAttr = Assembly.CustomAttributes.FirstOrDefault(
                X => X.Constructor.FullName.Contains("System.Runtime.Versioning.TargetFrameworkAttribute"));

            if (TargetFrameworkAttr != null && TargetFrameworkAttr.ConstructorArguments.Count > 0 && TargetFrameworkAttr.ConstructorArguments[0].Value != null)
            {
                FrameworkName TargetFramework = new FrameworkName(TargetFrameworkAttr.ConstructorArguments[0].Value.ToString());

                String NetcoreAssemblyPath = String.Format(@"Reference Assemblies\Microsoft\Framework\{0}\v{1}",
                    TargetFramework.Identifier,
                    TargetFramework.Version);

                NetcoreAssemblyPath = Path.Combine(ProgramFilesx86(), NetcoreAssemblyPath);
                if (Directory.Exists(NetcoreAssemblyPath))
                {
                    Resolver.AddSearchDirectory(NetcoreAssemblyPath);
                }
            }

            // Import void* and int32 
            VoidType = Assembly.MainModule.TypeSystem.Void.Resolve();
            VoidPointerType = new PointerType(Assembly.MainModule.Import(VoidType));
            IntType = Assembly.MainModule.Import(Assembly.MainModule.TypeSystem.Int32.Resolve());

            // Remove CompilationRelaxationsAttribute
            for (Int32 Index = 0; Index < Assembly.CustomAttributes.Count; Index++)
            {
                CustomAttribute CustomAttribute = Assembly.CustomAttributes[Index];
                if (CustomAttribute.AttributeType.FullName == typeof(CompilationRelaxationsAttribute).FullName)
                {
                    Assembly.CustomAttributes.RemoveAt(Index);
                    Index--;
                }
            }

            Log("SharpDX interop patch for assembly [{0}]", TargetFile);
            foreach (TypeDefinition Type in Assembly.MainModule.Types)
                PatchType(Type);

            // Remove All Interop classes
            foreach (TypeDefinition Type in ClassToRemoveList)
                Assembly.MainModule.Types.Remove(Type);

            String OutputFilePath = TargetFile;
            Assembly.Write(OutputFilePath, WriterParameters);

            FileTime = new FileTime(TargetFile);

            // Update Check file
            FileTime.UpdateCheckFile(CheckFile);
            // FileTimeInteropBuilder.UpdateCheckFile(CheckInteropBuilderFile);

            Log("SharpDX patch done for assembly [{0}]", TargetFile);
            return true;
        }

        public void Log(String message, params Object[] Parameters)
        {
            Console.WriteLine(message, Parameters);
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

            public FileTime(String TargetFile)
            {
                CreateTime = File.GetCreationTime(TargetFile);
                LastAccessTime = File.GetLastAccessTime(TargetFile);
                LastWriteTime = File.GetLastWriteTime(TargetFile);
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

            public void UpdateFile(String TargetFile)
            {
                File.SetCreationTime(TargetFile, CreateTime);
                File.SetLastWriteTime(TargetFile, LastWriteTime);
                File.SetLastAccessTime(TargetFile, LastAccessTime);
            }
        }

    } // End class

} // End namespace