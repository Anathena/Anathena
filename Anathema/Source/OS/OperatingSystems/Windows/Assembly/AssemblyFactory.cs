/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Linq;
using System.Threading.Tasks;
using Binarysharp.MemoryManagement.Assembly.Assembler;
using Binarysharp.MemoryManagement.Assembly.CallingConvention;
using Binarysharp.MemoryManagement.Internals;
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Threading;
using Binarysharp.MemoryManagement.Assembly.Disassembler;

namespace Binarysharp.MemoryManagement.Assembly
{
    /// <summary>
    /// Class providing tools for manipulating assembly code.
    /// </summary>
    public class AssemblyFactory : IFactory
    {
        /// <summary>
        /// The reference of the <see cref="MemorySharp"/> object.
        /// </summary>
        protected readonly MemoryEditor MemorySharp;

        /// <summary>
        /// The assembler used by the factory.
        /// </summary>
        public IAssembler Assembler { get; set; }

        /// <summary>
        /// The assembler used by the factory.
        /// </summary>
        public IDisassembler Disassembler { get; set; }

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyFactory"/> class.
        /// </summary>
        /// <param name="MemorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        internal AssemblyFactory(MemoryEditor MemorySharp)
        {
            // Save the parameter
            this.MemorySharp = MemorySharp;

            // Create the tool
            Assembler = new Fasm32Assembler();
            Disassembler = new SharpDisassembler();
        }
        #endregion

        #region Methods
        #region BeginTransaction
        /// <summary>
        /// Begins a new transaction to inject and execute assembly code into the process at the specified address.
        /// </summary>
        /// <param name="Address">The address where the assembly code is injected.</param>
        /// <param name="AutoExecute">Indicates whether the assembly code is executed once the object is disposed.</param>
        /// <returns>The return value is a new transaction.</returns>
        public AssemblyTransaction BeginTransaction(IntPtr Address, Boolean AutoExecute = true)
        {
            return new AssemblyTransaction(MemorySharp, Address, AutoExecute);
        }

        /// <summary>
        /// Begins a new transaction to inject and execute assembly code into the process.
        /// </summary>
        /// <param name="AutoExecute">Indicates whether the assembly code is executed once the object is disposed.</param>
        /// <returns>The return value is a new transaction.</returns>
        public AssemblyTransaction BeginTransaction(Boolean AutoExecute = true)
        {
            return new AssemblyTransaction(MemorySharp, AutoExecute);
        }

        #endregion
        #region Dispose (implementation of IFactory)
        /// <summary>
        /// Releases all resources used by the <see cref="AssemblyFactory"/> object.
        /// </summary>
        public void Dispose()
        {
            // Nothing to dispose... yet
        }

        #endregion
        #region Execute
        /// <summary>
        /// Executes the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="Address">The address where the assembly code is located.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public T Execute<T>(IntPtr Address)
        {
            // Execute and join the code in a new thread
            var thread = MemorySharp.Threads.CreateAndJoin(Address);
            // Return the exit code of the thread
            return thread.GetExitCode<T>();
        }

        /// <summary>
        /// Executes the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="Address">The address where the assembly code is located.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public IntPtr Execute(IntPtr Address)
        {
            return Execute<IntPtr>(Address);
        }

        /// <summary>
        /// Executes the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="Address">The address where the assembly code is located.</param>
        /// <param name="Parameter">The parameter used to execute the assembly code.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public T Execute<T>(IntPtr Address, dynamic Parameter)
        {
            // Execute and join the code in a new thread
            RemoteThread Thread = MemorySharp.Threads.CreateAndJoin(Address, Parameter);
            // Return the exit code of the thread
            return Thread.GetExitCode<T>();
        }

        /// <summary>
        /// Executes the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="Address">The address where the assembly code is located.</param>
        /// <param name="Parameter">The parameter used to execute the assembly code.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public IntPtr Execute(IntPtr Address, dynamic Parameter)
        {
            return Execute<IntPtr>(Address, Parameter);
        }

        /// <summary>
        /// Executes the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="Address">The address where the assembly code is located.</param>
        /// <param name="CallingConvention">The calling convention used to execute the assembly code with the parameters.</param>
        /// <param name="Parameters">An array of parameters used to execute the assembly code.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public T Execute<T>(IntPtr Address, CallingConventions CallingConvention, params dynamic[] Parameters)
        {
            // Marshal the parameters
            IMarshalledValue[] MarshalledParameters = Parameters.Select(x => MarshalValue.Marshal(MemorySharp, x)).Cast<IMarshalledValue>().ToArray();
            
            // Start a transaction
            AssemblyTransaction AssemblyTransaction;
            using (AssemblyTransaction = BeginTransaction())
            {
                // Get the object dedicated to create mnemonics for the given calling convention
                ICallingConvention Calling = CallingConventionSelector.Get(CallingConvention);

                // Push the parameters
                AssemblyTransaction.AddLine(Calling.FormatParameters(MarshalledParameters.Select(x => x.Reference).ToArray()));

                // Call the function
                AssemblyTransaction.AddLine(Calling.FormatCalling(Address));

                // Clean the parameters
                if(Calling.Cleanup == CleanupTypes.Caller)
                    AssemblyTransaction.AddLine(Calling.FormatCleaning(MarshalledParameters.Length));

                // Add the return mnemonic
                AssemblyTransaction.AddLine("retn");
            }

            // Clean the marshalled parameters
            foreach (IMarshalledValue Parameter in MarshalledParameters)
            {
                Parameter.Dispose();
            }

            // Return the exit code
            return AssemblyTransaction.GetExitCode<T>();
        }
        /// <summary>
        /// Executes the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="Address">The address where the assembly code is located.</param>
        /// <param name="CallingConvention">The calling convention used to execute the assembly code with the parameters.</param>
        /// <param name="Parameters">An array of parameters used to execute the assembly code.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public IntPtr Execute(IntPtr Address, CallingConventions CallingConvention, params dynamic[] Parameters)
        {
            return Execute<IntPtr>(Address, CallingConvention, Parameters);
        }

        #endregion
        #region ExecuteAsync
        /// <summary>
        /// Executes asynchronously the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="Address">The address where the assembly code is located.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<T> ExecuteAsync<T>(IntPtr Address)
        {
            return Task.Run(() => Execute<T>(Address));
        }

        /// <summary>
        /// Executes asynchronously the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="Address">The address where the assembly code is located.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<IntPtr> ExecuteAsync(IntPtr Address)
        {
            return ExecuteAsync<IntPtr>(Address);
        }

        /// <summary>
        /// Executes asynchronously the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="Address">The address where the assembly code is located.</param>
        /// <param name="Parameter">The parameter used to execute the assembly code.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<T> ExecuteAsync<T>(IntPtr Address, dynamic Parameter)
        {
            return Task.Run(() => (Task<T>)Execute<T>(Address, Parameter));
        }

        /// <summary>
        /// Executes asynchronously the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="Address">The address where the assembly code is located.</param>
        /// <param name="Parameter">The parameter used to execute the assembly code.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<IntPtr> ExecuteAsync(IntPtr Address, dynamic Parameter)
        {
            return ExecuteAsync<IntPtr>(Address, Parameter);
        }

        /// <summary>
        /// Executes asynchronously the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="Address">The address where the assembly code is located.</param>
        /// <param name="CallingConvention">The calling convention used to execute the assembly code with the parameters.</param>
        /// <param name="Parameters">An array of parameters used to execute the assembly code.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<T> ExecuteAsync<T>(IntPtr Address, CallingConventions CallingConvention, params dynamic[] Parameters)
        {
            return Task.Run(() => Execute<T>(Address, CallingConvention, Parameters));
        }

        /// <summary>
        /// Executes asynchronously the assembly code located in the remote process at the specified address.
        /// </summary>
        /// <param name="Address">The address where the assembly code is located.</param>
        /// <param name="CallingConvention">The calling convention used to execute the assembly code with the parameters.</param>
        /// <param name="Parameters">An array of parameters used to execute the assembly code.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<IntPtr> ExecuteAsync(IntPtr Address, CallingConventions CallingConvention, params dynamic[] Parameters)
        {
            return ExecuteAsync<IntPtr>(Address, CallingConvention, Parameters);
        }

        #endregion
        #region Inject
        /// <summary>
        /// Assembles mnemonics and injects the corresponding assembly code into the remote process at the specified address.
        /// </summary>
        /// <param name="Asm">The mnemonics to inject.</param>
        /// <param name="Address">The address where the assembly code is injected.</param>
        public void Inject(String Asm, IntPtr Address)
        {
            MemorySharp.Write(Address, Assembler.Assemble(Asm, Address));
        }

        /// <summary>
        /// Assembles mnemonics and injects the corresponding assembly code into the remote process at the specified address.
        /// </summary>
        /// <param name="Asm">An array containing the mnemonics to inject.</param>
        /// <param name="Address">The address where the assembly code is injected.</param>
        public void Inject(String[] Asm, IntPtr Address)
        {
            Inject(String.Join("\n", Asm), Address);
        }

        /// <summary>
        /// Assembles mnemonics and injects the corresponding assembly code into the remote process.
        /// </summary>
        /// <param name="Asm">The mnemonics to inject.</param>
        /// <returns>The address where the assembly code is injected.</returns>
        public RemoteAllocation Inject(String Asm)
        {
            // Assemble the assembly code
            Byte[] Code = Assembler.Assemble(Asm);

            // Allocate a chunk of memory to store the assembly code
            RemoteAllocation Memory = MemorySharp.Memory.Allocate(Code.Length);

            // Inject the code
            Inject(Asm, Memory.BaseAddress);

            // Return the memory allocated
            return Memory;
        }

        /// <summary>
        /// Assembles mnemonics and injects the corresponding assembly code into the remote process.
        /// </summary>
        /// <param name="Asm">An array containing the mnemonics to inject.</param>
        /// <returns>The address where the assembly code is injected.</returns>
        public RemoteAllocation Inject(String[] Asm)
        {
            return Inject(String.Join("\n", Asm));
        }

        #endregion
        #region InjectAndExecute
        /// <summary>
        /// Assembles, injects and executes the mnemonics into the remote process at the specified address.
        /// </summary>
        /// <param name="Asm">The mnemonics to inject.</param>
        /// <param name="Address">The address where the assembly code is injected.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public T InjectAndExecute<T>(String Asm, IntPtr Address)
        {
            // Inject the assembly code
            Inject(Asm, Address);

            // Execute the code
            return Execute<T>(Address);
        }

        /// <summary>
        /// Assembles, injects and executes the mnemonics into the remote process at the specified address.
        /// </summary>
        /// <param name="Asm">The mnemonics to inject.</param>
        /// <param name="Address">The address where the assembly code is injected.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public IntPtr InjectAndExecute(String Asm, IntPtr Address)
        {
            return InjectAndExecute<IntPtr>(Asm, Address);
        }

        /// <summary>
        /// Assembles, injects and executes the mnemonics into the remote process at the specified address.
        /// </summary>
        /// <param name="Asm">An array containing the mnemonics to inject.</param>
        /// <param name="Address">The address where the assembly code is injected.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public T InjectAndExecute<T>(String[] Asm, IntPtr Address)
        {
            return InjectAndExecute<T>(String.Join("\n", Asm), Address);
        }

        /// <summary>
        /// Assembles, injects and executes the mnemonics into the remote process at the specified address.
        /// </summary>
        /// <param name="Asm">An array containing the mnemonics to inject.</param>
        /// <param name="Address">The address where the assembly code is injected.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public IntPtr InjectAndExecute(String[] Asm, IntPtr Address)
        {
            return InjectAndExecute<IntPtr>(Asm, Address);
        }

        /// <summary>
        /// Assembles, injects and executes the mnemonics into the remote process.
        /// </summary>
        /// <param name="Asm">The mnemonics to inject.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public T InjectAndExecute<T>(String Asm)
        {
            // Inject the assembly code
            using (RemoteAllocation Memory = Inject(Asm))
            {
                // Execute the code
                return Execute<T>(Memory.BaseAddress);
            }
        }

        /// <summary>
        /// Assembles, injects and executes the mnemonics into the remote process.
        /// </summary>
        /// <param name="Asm">The mnemonics to inject.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public IntPtr InjectAndExecute(String Asm)
        {
            return InjectAndExecute<IntPtr>(Asm);
        }

        /// <summary>
        /// Assembles, injects and executes the mnemonics into the remote process.
        /// </summary>
        /// <param name="Asm">An array containing the mnemonics to inject.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public T InjectAndExecute<T>(String[] Asm)
        {
            return InjectAndExecute<T>(String.Join("\n", Asm));
        }

        /// <summary>
        /// Assembles, injects and executes the mnemonics into the remote process.
        /// </summary>
        /// <param name="Asm">An array containing the mnemonics to inject.</param>
        /// <returns>The return value is the exit code of the thread created to execute the assembly code.</returns>
        public IntPtr InjectAndExecute(String[] Asm)
        {
            return InjectAndExecute<IntPtr>(Asm);
        }

        #endregion
        #region InjectAndExecuteAsync
        /// <summary>
        /// Assembles, injects and executes asynchronously the mnemonics into the remote process at the specified address.
        /// </summary>
        /// <param name="Asm">The mnemonics to inject.</param>
        /// <param name="Address">The address where the assembly code is injected.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<T> InjectAndExecuteAsync<T>(String Asm, IntPtr Address)
        {
            return Task.Run(() => InjectAndExecute<T>(Asm, Address));
        }

        /// <summary>
        /// Assembles, injects and executes asynchronously the mnemonics into the remote process at the specified address.
        /// </summary>
        /// <param name="Asm">The mnemonics to inject.</param>
        /// <param name="Address">The address where the assembly code is injected.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<IntPtr> InjectAndExecuteAsync(String Asm, IntPtr Address)
        {
            return InjectAndExecuteAsync<IntPtr>(Asm, Address);
        }

        /// <summary>
        /// Assembles, injects and executes asynchronously the mnemonics into the remote process at the specified address.
        /// </summary>
        /// <param name="Asm">An array containing the mnemonics to inject.</param>
        /// <param name="Address">The address where the assembly code is injected.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<T> InjectAndExecuteAsync<T>(String[] Asm, IntPtr Address)
        {
            return Task.Run(() => InjectAndExecute<T>(Asm, Address));
        }

        /// <summary>
        /// Assembles, injects and executes asynchronously the mnemonics into the remote process at the specified address.
        /// </summary>
        /// <param name="Asm">An array containing the mnemonics to inject.</param>
        /// <param name="Address">The address where the assembly code is injected.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<IntPtr> InjectAndExecuteAsync(String[] Asm, IntPtr Address)
        {
            return InjectAndExecuteAsync<IntPtr>(Asm, Address);
        }

        /// <summary>
        /// Assembles, injects and executes asynchronously the mnemonics into the remote process.
        /// </summary>
        /// <param name="Asm">The mnemonics to inject.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<T> InjectAndExecuteAsync<T>(String Asm)
        {
            return Task.Run(() => InjectAndExecute<T>(Asm));
        }

        /// <summary>
        /// Assembles, injects and executes asynchronously the mnemonics into the remote process.
        /// </summary>
        /// <param name="Asm">The mnemonics to inject.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<IntPtr> InjectAndExecuteAsync(String Asm)
        {
            return InjectAndExecuteAsync<IntPtr>(Asm);
        }

        /// <summary>
        /// Assembles, injects and executes asynchronously the mnemonics into the remote process.
        /// </summary>
        /// <param name="Asm">An array containing the mnemonics to inject.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<T> InjectAndExecuteAsync<T>(String[] Asm)
        {
            return Task.Run(() => InjectAndExecute<T>(Asm));
        }

        /// <summary>
        /// Assembles, injects and executes asynchronously the mnemonics into the remote process.
        /// </summary>
        /// <param name="Asm">An array containing the mnemonics to inject.</param>
        /// <returns>The return value is an asynchronous operation that return the exit code of the thread created to execute the assembly code.</returns>
        public Task<IntPtr> InjectAndExecuteAsync(String[] Asm)
        {
            return InjectAndExecuteAsync<IntPtr>(Asm);
        }

        #endregion
        #endregion

    } // End class

} // End namespace