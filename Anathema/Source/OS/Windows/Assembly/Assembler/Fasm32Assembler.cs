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
            //FASMHelper.WaitForInputIdle();
        }

        /// <summary>
        /// Assemble the specified assembly code.
        /// </summary>
        /// <param name="asm">The assembly code.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        public byte[] Assemble(string asm)
        {
            // Assemble and return the code
            return Assemble(asm, IntPtr.Zero);
        }

        /// <summary>
        /// Assemble the specified assembly code at a base address.
        /// </summary>
        /// <param name="asm">The assembly code.</param>
        /// <param name="baseAddress">The address where the code is rebased.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        public byte[] Assemble(string asm, IntPtr baseAddress)
        {
            LoadFASMHelper();
            
            IpcChannel IpcChannel = new IpcChannel("myClient");
            ChannelServices.RegisterChannel(IpcChannel, true);

            ISharedAssemblyInterface obj = (ISharedAssemblyInterface)Activator.GetObject(typeof(ISharedAssemblyInterface), "ipc://IPChannelName/SreeniRemoteObj");
            Int32 temp = obj.Addition(1, 2);

            return null;
            // Rebase the code
            //asm = String.Format("use32\norg 0x{0:X8}\n", baseAddress.ToInt64()) + asm;
            // Assemble and return the code
            //return FasmNet.Assemble(asm);
        }
    }
}