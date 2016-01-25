/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using FASMSharedInterface;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Windows.Forms;

namespace Binarysharp.MemoryManagement.Assembly.Assembler
{
    /// <summary>
    /// Implement Fasm.NET compiler for 32-bit development.
    /// More info: https://github.com/ZenLulz/Fasm.NET
    /// </summary>
    public class Fasm32Assembler : IAssembler
    {
        private const String FASMHelperExecutable = "FASMHelper.exe";
        private Process FASMHelper;

        private void LoadFASMHelper()
        {
            if (FASMHelper != null)
                return;

            FASMHelper = Process.Start(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), FASMHelperExecutable));
        }

        /// <summary>
        /// Assemble the specified assembly code.
        /// </summary>
        /// <param name="Assembly">The assembly code.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        public Byte[] Assemble(String Assembly)
        {
            // Assemble and return the code
            return Assemble(Assembly, IntPtr.Zero);
        }

        /// <summary>
        /// Assemble the specified assembly code at a base address.
        /// </summary>
        /// <param name="Assembly">The assembly code.</param>
        /// <param name="BaseAddress">The address where the code is rebased.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        public Byte[] Assemble(String Assembly, IntPtr BaseAddress)
        {
            LoadFASMHelper();
            
            IpcChannel IpcChannel = new IpcChannel("Client");
            ChannelServices.RegisterChannel(IpcChannel, true);
            ISharedAssemblyInterface FASMObj = (ISharedAssemblyInterface)Activator.GetObject(typeof(ISharedAssemblyInterface), "ipc://FASMChannel/FASMObj");

            var test = FASMObj.Assemble(false, Assembly, BaseAddress.ToInt64());

            return null;
        }

    } // End class

} // End namespace