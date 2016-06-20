using Anathema.Source.Engine.Architecture.Assembler;
using Anathema.Source.Engine.Architecture.Disassembler;
using Anathema.Source.Engine.Hook.Client;
using Anathema.Source.Engine.InputCapture;
using Anathema.Source.Engine.OperatingSystems;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Anathema.Source.Engine
{
    /// <summary>
    /// Abstraction of the OS, providing access to assembly functions and target process functions
    /// </summary>
    public class EngineCore
    {
        public IOperatingSystem Memory { get; private set; }

        public IAssembler Assembler { get; private set; }

        public IDisassembler Disassembler { get; private set; }

        public IInputManager InputManager { get; private set; }

        /// <summary>
        /// TODO: Replace this with IGraphicsInterface, ISpeedHack, IUnrandomizer, etc instead of the
        /// hook creator that initializes them. Instead the hook creator can be lazy initialized when any
        /// of the child processes are accessed
        /// </summary>
        public IHookCreator HookCreator { get; private set; }

        public EngineCore(Process TargetProcess)
        {
            Memory = OperatingSystemFactory.GetOperatingSystem(TargetProcess);
            Assembler = AssemblerFactory.GetAssembler();
            Disassembler = DisassemblerFactory.GetDisassembler();
            InputManager = new InputManager();
            HookCreator = new HookCreator();
        }

        #region TODO: Move to Memory

        /// <summary>
        /// Determines if the OS is a 32 bit OS
        /// </summary>
        /// <returns></returns>
        public static Boolean IsOS32Bit()
        {
            return !Environment.Is64BitOperatingSystem;
        }

        /// <summary>
        /// Determines if the OS is a 64 bit OS
        /// </summary>
        /// <returns></returns>
        public static Boolean IsOS64Bit()
        {
            return Environment.Is64BitOperatingSystem;
        }

        /// <summary>
        /// Determines if Anathema is running as 32 bit
        /// </summary>
        /// <returns></returns>
        public static Boolean IsAnathema32Bit()
        {
            return !Environment.Is64BitProcess;
        }

        /// <summary>
        /// Determines if Anathema is running as 64 bit
        /// </summary>
        /// <returns></returns>
        public static Boolean IsAnathema64Bit()
        {
            return Environment.Is64BitProcess;
        }

        /// <summary>
        /// Determines if Anathema is running as 32 bit or 64 bit
        /// </summary>
        /// <returns></returns>
        public static Boolean IsAnthema64Bit()
        {
            return Environment.Is64BitProcess;
        }

        /// <summary>
        /// Determines if a specified process is 32 bit
        /// </summary>
        /// <param name="ProcessHandle"></param>
        /// <returns></returns>
        public static Boolean IsProcess32Bit(IntPtr ProcessHandle)
        {
            // First do the simple check if seeing if the OS is 32 bit, in which case the process wont be 64 bit
            if (!Environment.Is64BitOperatingSystem)
                return true;

            Boolean IsWow64;
            if (!IsWow64Process(ProcessHandle, out IsWow64))
                return false; // Error

            return IsWow64;
        }

        /// <summary>
        /// Determines if a specified process is 64 bit
        /// </summary>
        /// <param name="ProcessHandle"></param>
        /// <returns></returns>
        public static Boolean IsProcess64bit(IntPtr ProcessHandle)
        {
            return !IsProcess32Bit(ProcessHandle);
        }

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr ProcessHandle, [Out, MarshalAs(UnmanagedType.Bool)] out bool Wow64Process);

        #endregion

    } // End interface

} // End namespace