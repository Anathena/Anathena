/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Text;
using Binarysharp.MemoryManagement.Internals;

namespace Binarysharp.MemoryManagement.Assembly
{
    /// <summary>
    /// Class representing a transaction where the user can insert mnemonics.
    /// The code is then executed when the object is disposed.
    /// </summary>
    public class AssemblyTransaction : IDisposable
    {
        #region Fields
        /// <summary>
        /// The reference of the <see cref="MemorySharp"/> object.
        /// </summary>
        protected readonly MemorySharp MemorySharp;
        /// <summary>
        /// The builder contains all the mnemonics inserted by the user.
        /// </summary>
        protected StringBuilder Mnemonics;
        /// <summary>
        /// The exit code of the thread created to execute the assembly code.
        /// </summary>
        protected IntPtr ExitCode;
        #endregion

        #region Properties
        #region Address
        /// <summary>
        /// The address where to assembly code is assembled.
        /// </summary>
        public IntPtr Address { get; private set; }
        #endregion
        #region IsAutoExecuted
        /// <summary>
        /// Gets the value indicating whether the assembly code is executed once the object is disposed.
        /// </summary>
        public bool IsAutoExecuted { get; set; }
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyTransaction"/> class.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        /// <param name="address">The address where the assembly code is injected.</param>
        /// <param name="autoExecute">Indicates whether the assembly code is executed once the object is disposed.</param>
        public AssemblyTransaction(MemorySharp memorySharp, IntPtr address, bool autoExecute)
        {
            // Save the parameters
            MemorySharp = memorySharp;
            IsAutoExecuted = autoExecute;
            Address = address;
            // Initialize the string builder
            Mnemonics = new StringBuilder();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyTransaction"/> class.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        /// <param name="autoExecute">Indicates whether the assembly code is executed once the object is disposed.</param>
        public AssemblyTransaction(MemorySharp memorySharp, bool autoExecute) : this(memorySharp, IntPtr.Zero, autoExecute)
        {
        }
        #endregion

        #region Methods
        #region AddLine
        /// <summary>
        /// Adds a mnemonic to the transaction.
        /// </summary>
        /// <param name="asm">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public void AddLine(string asm, params object[] args)
        {
            Mnemonics.AppendLine(String.Format(asm, args));
        }
        #endregion
        #region Assemble
        /// <summary>
        /// Assemble the assembly code of this transaction.
        /// </summary>
        /// <returns>An array of bytes containing the assembly code.</returns>
        public byte[] Assemble()
        {
            return MemorySharp.Assembly.Assembler.Assemble(Mnemonics.ToString());
        }
        #endregion
        #region Clear
        /// <summary>
        /// Removes all mnemonics from the transaction.
        /// </summary>
        public void Clear()
        {
            Mnemonics.Clear();
        }
        #endregion
        #region Dispose (implementation of IDisposable)
        /// <summary>
        /// Releases all resources used by the <see cref="AssemblyTransaction"/> object.
        /// </summary>
        public virtual void Dispose()
        {
            // If a pointer was specified
            if (Address != IntPtr.Zero)
            {
                // If the assembly code must be executed
                if (IsAutoExecuted)
                {
                    ExitCode = MemorySharp.Assembly.InjectAndExecute<IntPtr>(Mnemonics.ToString(), Address);
                }
                // Else the assembly code is just injected
                else
                {
                    MemorySharp.Assembly.Inject(Mnemonics.ToString(), Address);
                }
            }

            // If no pointer was specified and the code assembly code must be executed
            if (Address == IntPtr.Zero && IsAutoExecuted)
            {
                ExitCode = MemorySharp.Assembly.InjectAndExecute<IntPtr>(Mnemonics.ToString());
            }
        }
        #endregion
        #region GetExitCode
        /// <summary>
        /// Gets the termination status of the thread.
        /// </summary>
        public T GetExitCode<T>()
        {
            return MarshalType<T>.PtrToObject(MemorySharp, ExitCode);
        }
        #endregion
        #region InsertLine
        /// <summary>
        /// Inserts a mnemonic to the transaction at a given index.
        /// </summary>
        /// <param name="index">The position in the transaction where insertion begins.</param>
        /// <param name="asm">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public void InsertLine(int index, string asm, params object[] args)
        {
            Mnemonics.Insert(index, String.Format(asm, args));
        }
        #endregion
        #endregion
    }
}
