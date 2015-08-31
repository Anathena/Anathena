/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Threading;

namespace Binarysharp.MemoryManagement.Native
{
    /// <summary>
    /// Class representing the Thread Environment Block of a remote thread.
    /// </summary>
    public class ManagedTeb : RemotePointer
    {
        #region Properties
        /// <summary>
        /// Current Structured Exception Handling (SEH) frame.
        /// </summary>
        public IntPtr CurrentSehFrame
        {
            get { return Read<IntPtr>(TebStructure.CurrentSehFrame); }
            set { Write(TebStructure.CurrentSehFrame, value); }
        }
        /// <summary>
        /// The top of stack.
        /// </summary>
        public IntPtr TopOfStack
        {
            get { return Read<IntPtr>(TebStructure.TopOfStack); }
            set { Write(TebStructure.TopOfStack, value); }
        }
        /// <summary>
        /// The current bottom of stack.
        /// </summary>
        public IntPtr BottomOfStack
        {
            get { return Read<IntPtr>(TebStructure.BottomOfStack); }
            set { Write(TebStructure.BottomOfStack, value); }
        }
        /// <summary>
        /// The TEB sub system.
        /// </summary>
        public IntPtr SubSystemTeb
        {
            get { return Read<IntPtr>(TebStructure.SubSystemTeb); }
            set { Write(TebStructure.SubSystemTeb, value); }
        }
        /// <summary>
        /// The fiber data.
        /// </summary>
        public IntPtr FiberData
        {
            get { return Read<IntPtr>(TebStructure.FiberData); }
            set { Write(TebStructure.FiberData, value); }
        }
        /// <summary>
        /// The arbitrary data slot.
        /// </summary>
        public IntPtr ArbitraryDataSlot
        {
            get { return Read<IntPtr>(TebStructure.ArbitraryDataSlot); }
            set { Write(TebStructure.ArbitraryDataSlot, value); }
        }
        /// <summary>
        /// The linear address of Thread Environment Block (TEB).
        /// </summary>
        public IntPtr Teb
        {
            get { return Read<IntPtr>(TebStructure.Teb); }
            set { Write(TebStructure.Teb, value); }
        }
        /// <summary>
        /// The environment pointer.
        /// </summary>
        public IntPtr EnvironmentPointer
        {
            get { return Read<IntPtr>(TebStructure.EnvironmentPointer); }
            set { Write(TebStructure.EnvironmentPointer, value); }
        }
        /// <summary>
        /// The process Id.
        /// </summary>
        public int ProcessId
        {
            get { return Read<int>(TebStructure.ProcessId); }
            set { Write(TebStructure.ProcessId, value); }
        }
        /// <summary>
        /// The current thread Id.
        /// </summary>
        public int ThreadId
        {
            get { return Read<int>(TebStructure.ThreadId); }
            set { Write(TebStructure.ThreadId, value); }
        }
        /// <summary>
        /// The active RPC handle.
        /// </summary>
        public IntPtr RpcHandle
        {
            get { return Read<IntPtr>(TebStructure.RpcHandle); }
            set { Write(TebStructure.RpcHandle, value); }
        }
        /// <summary>
        /// The linear address of the thread-local storage (TLS) array.
        /// </summary>
        public IntPtr Tls
        {
            get { return Read<IntPtr>(TebStructure.Tls); }
            set { Write(TebStructure.Tls, value); }
        }
        /// <summary>
        /// The linear address of Process Environment Block (PEB).
        /// </summary>
        public IntPtr Peb
        {
            get { return Read<IntPtr>(TebStructure.Peb); }
            set { Write(TebStructure.Peb, value); }
        }
        /// <summary>
        /// The last error number.
        /// </summary>
        public int LastErrorNumber
        {
            get { return Read<int>(TebStructure.LastErrorNumber); }
            set { Write(TebStructure.LastErrorNumber, value); }
        }
        /// <summary>
        /// The count of owned critical sections.
        /// </summary>
        public int CriticalSectionsCount
        {
            get { return Read<int>(TebStructure.CriticalSectionsCount); }
            set { Write(TebStructure.CriticalSectionsCount, value); }
        }
        /// <summary>
        /// The address of CSR Client Thread.
        /// </summary>
        public IntPtr CsrClientThread
        {
            get { return Read<IntPtr>(TebStructure.CsrClientThread); }
            set { Write(TebStructure.CsrClientThread, value); }
        }
        /// <summary>
        /// Win32 Thread Information.
        /// </summary>
        public IntPtr Win32ThreadInfo
        {
            get { return Read<IntPtr>(TebStructure.Win32ThreadInfo); }
            set { Write(TebStructure.Win32ThreadInfo, value); }
        }
        /// <summary>
        /// Win32 client information (NT), user32 private data (Wine), 0x60 = LastError (Win95), 0x74 = LastError (WinME).
        /// </summary>
        public byte[] Win32ClientInfo
        {
            get { return Read<byte>(TebStructure.Win32ClientInfo, 124); }
            set { Write(TebStructure.Win32ClientInfo, value); }
        }
        /// <summary>
        /// Reserved for Wow64. Contains a pointer to FastSysCall in Wow64.
        /// </summary>
        public IntPtr WoW64Reserved
        {
            get { return Read<IntPtr>(TebStructure.WoW64Reserved); }
            set { Write(TebStructure.WoW64Reserved, value); }
        }
        /// <summary>
        /// The current locale
        /// </summary>
        public IntPtr CurrentLocale
        {
            get { return Read<IntPtr>(TebStructure.CurrentLocale); }
            set { Write(TebStructure.CurrentLocale, value); }
        }
        /// <summary>
        /// The FP Software Status Register.
        /// </summary>
        public IntPtr FpSoftwareStatusRegister
        {
            get { return Read<IntPtr>(TebStructure.FpSoftwareStatusRegister); }
            set { Write(TebStructure.FpSoftwareStatusRegister, value); }
        }
        /// <summary>
        /// Reserved for OS (NT), kernel32 private data (Wine).
        /// herein: FS:[0x124] 4 NT Pointer to KTHREAD (ETHREAD) structure.
        /// </summary>
        public byte[] SystemReserved1
        {
            get { return Read<byte>(TebStructure.SystemReserved1, 216); }
            set { Write(TebStructure.SystemReserved1, value); }
        }
        /// <summary>
        /// The exception code.
        /// </summary>
        public IntPtr ExceptionCode
        {
            get { return Read<IntPtr>(TebStructure.ExceptionCode); }
            set { Write(TebStructure.ExceptionCode, value); }
        }
        /// <summary>
        /// The activation context stack.
        /// </summary>
        public byte[] ActivationContextStack
        {
            get { return Read<byte>(TebStructure.ActivationContextStack, 18); }
            set { Write(TebStructure.ActivationContextStack, value); }
        }
        /// <summary>
        /// The spare bytes (NT), ntdll private data (Wine).
        /// </summary>
        public byte[] SpareBytes
        {
            get { return Read<byte>(TebStructure.SpareBytes, 26); }
            set { Write(TebStructure.SpareBytes, value); }
        }
        /// <summary>
        /// Reserved for OS (NT), ntdll private data (Wine).
        /// </summary>
        public byte[] SystemReserved2
        {
            get { return Read<byte>(TebStructure.SystemReserved2, 40); }
            set { Write(TebStructure.SystemReserved2, value); }
        }
        /// <summary>
        /// The GDI TEB Batch (OS), vm86 private data (Wine).
        /// </summary>
        public byte[] GdiTebBatch
        {
            get { return Read<byte>(TebStructure.GdiTebBatch, 1248); }
            set { Write(TebStructure.GdiTebBatch, value); }
        }
        /// <summary>
        /// The GDI Region.
        /// </summary>
        public IntPtr GdiRegion
        {
            get { return Read<IntPtr>(TebStructure.GdiRegion); }
            set { Write(TebStructure.GdiRegion, value); }
        }
        /// <summary>
        /// The GDI Pen.
        /// </summary>
        public IntPtr GdiPen
        {
            get { return Read<IntPtr>(TebStructure.GdiPen); }
            set { Write(TebStructure.GdiPen, value); }
        }
        /// <summary>
        /// The GDI Brush.
        /// </summary>
        public IntPtr GdiBrush
        {
            get { return Read<IntPtr>(TebStructure.GdiBrush); }
            set { Write(TebStructure.GdiBrush, value); }
        }
        /// <summary>
        /// The real process Id.
        /// </summary>
        public int RealProcessId
        {
            get { return Read<int>(TebStructure.RealProcessId); }
            set { Write(TebStructure.RealProcessId, value); }
        }
        /// <summary>
        /// The real thread Id.
        /// </summary>
        public int RealThreadId
        {
            get { return Read<int>(TebStructure.RealThreadId); }
            set { Write(TebStructure.RealThreadId, value); }
        }
        /// <summary>
        /// The GDI cached process handle.
        /// </summary>
        public IntPtr GdiCachedProcessHandle
        {
            get { return Read<IntPtr>(TebStructure.GdiCachedProcessHandle); }
            set { Write(TebStructure.GdiCachedProcessHandle, value); }
        }
        /// <summary>
        /// The GDI client process Id (PID).
        /// </summary>
        public IntPtr GdiClientProcessId
        {
            get { return Read<IntPtr>(TebStructure.GdiClientProcessId); }
            set { Write(TebStructure.GdiClientProcessId, value); }
        }
        /// <summary>
        /// The GDI client thread Id (TID).
        /// </summary>
        public IntPtr GdiClientThreadId
        {
            get { return Read<IntPtr>(TebStructure.GdiClientThreadId); }
            set { Write(TebStructure.GdiClientThreadId, value); }
        }
        /// <summary>
        /// The GDI thread locale information.
        /// </summary>
        public IntPtr GdiThreadLocalInfo
        {
            get { return Read<IntPtr>(TebStructure.GdiThreadLocalInfo); }
            set { Write(TebStructure.GdiThreadLocalInfo, value); }
        }
        /// <summary>
        /// Reserved for user application.
        /// </summary>
        public byte[] UserReserved1
        {
            get { return Read<byte>(TebStructure.UserReserved1, 20); }
            set { Write(TebStructure.UserReserved1, value); }
        }
        /// <summary>
        /// Reserved for GL.
        /// </summary>
        public byte[] GlReserved1
        {
            get { return Read<byte>(TebStructure.GlReserved1, 1248); }
            set { Write(TebStructure.GlReserved1, value); }
        }
        /// <summary>
        /// The last value status value.
        /// </summary>
        public int LastStatusValue
        {
            get { return Read<int>(TebStructure.LastStatusValue); }
            set { Write(TebStructure.LastStatusValue, value); }
        }
        /// <summary>
        /// The static UNICODE_STRING buffer.
        /// </summary>
        public byte[] StaticUnicodeString
        {
            get { return Read<byte>(TebStructure.StaticUnicodeString, 532); }
            set { Write(TebStructure.StaticUnicodeString, value); }
        }
        /// <summary>
        /// The pointer to deallocation stack.
        /// </summary>
        public IntPtr DeallocationStack
        {
            get { return Read<IntPtr>(TebStructure.DeallocationStack); }
            set { Write(TebStructure.DeallocationStack, value); }
        }
        /// <summary>
        /// The TLS slots, 4 byte per slot.
        /// </summary>
        public IntPtr[] TlsSlots
        {
            get { return Read<IntPtr>(TebStructure.TlsSlots, 64); }
            set { Write(TebStructure.TlsSlots, value); }
        }
        /// <summary>
        /// The TLS links (LIST_ENTRY structure).
        /// </summary>
        public long TlsLinks
        {
            get { return Read<long>(TebStructure.TlsLinks); }
            set { Write(TebStructure.TlsLinks, value); }
        }
        /// <summary>
        /// Virtual DOS Machine.
        /// </summary>
        public IntPtr Vdm
        {
            get { return Read<IntPtr>(TebStructure.Vdm); }
            set { Write(TebStructure.Vdm, value); }
        }
        /// <summary>
        /// Reserved for RPC.
        /// </summary>
        public IntPtr RpcReserved
        {
            get { return Read<IntPtr>(TebStructure.RpcReserved); }
            set { Write(TebStructure.RpcReserved, value); }
        }
        /// <summary>
        /// The thread error mode (RtlSetThreadErrorMode).
        /// </summary>
        public IntPtr ThreadErrorMode
        {
            get { return Read<IntPtr>(TebStructure.ThreadErrorMode); }
            set { Write(TebStructure.ThreadErrorMode, value); }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedTeb"/> class.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        /// <param name="address">The location of the teb.</param>
        internal ManagedTeb(MemorySharp memorySharp, IntPtr address) : base(memorySharp, address)
        {}
        #endregion

        #region Methods
        /// <summary>
        /// Finds the Thread Environment Block address of a specified thread.
        /// </summary>
        /// <param name="threadHandle">A handle of the thread.</param>
        /// <returns>A <see cref="IntPtr"/> pointer of the TEB.</returns>
        public static IntPtr FindTeb(SafeMemoryHandle threadHandle)
        {
            return ThreadCore.NtQueryInformationThread(threadHandle).TebBaseAdress;
        }
        #endregion
    }
}
