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
        public Boolean Success;

        /// <summary>
        /// Current Structured Exception Handling (SEH) frame.
        /// </summary>
        public IntPtr CurrentSehFrame
        {
            get { return Read<IntPtr>(TebStructure.CurrentSehFrame, out Success); }
            set { Write(TebStructure.CurrentSehFrame, value); }
        }

        /// <summary>
        /// The top of stack.
        /// </summary>
        public IntPtr TopOfStack
        {
            get { return Read<IntPtr>(TebStructure.TopOfStack, out Success); }
            set { Write(TebStructure.TopOfStack, value); }
        }

        /// <summary>
        /// The current bottom of stack.
        /// </summary>
        public IntPtr BottomOfStack
        {
            get { return Read<IntPtr>(TebStructure.BottomOfStack, out Success); }
            set { Write(TebStructure.BottomOfStack, value); }
        }

        /// <summary>
        /// The TEB sub system.
        /// </summary>
        public IntPtr SubSystemTeb
        {
            get { return Read<IntPtr>(TebStructure.SubSystemTeb, out Success); }
            set { Write(TebStructure.SubSystemTeb, value); }
        }

        /// <summary>
        /// The fiber data.
        /// </summary>
        public IntPtr FiberData
        {
            get { return Read<IntPtr>(TebStructure.FiberData, out Success); }
            set { Write(TebStructure.FiberData, value); }
        }

        /// <summary>
        /// The arbitrary data slot.
        /// </summary>
        public IntPtr ArbitraryDataSlot
        {
            get { return Read<IntPtr>(TebStructure.ArbitraryDataSlot, out Success); }
            set { Write(TebStructure.ArbitraryDataSlot, value); }
        }

        /// <summary>
        /// The linear address of Thread Environment Block (TEB).
        /// </summary>
        public IntPtr Teb
        {
            get { return Read<IntPtr>(TebStructure.Teb, out Success); }
            set { Write(TebStructure.Teb, value); }
        }

        /// <summary>
        /// The environment pointer.
        /// </summary>
        public IntPtr EnvironmentPointer
        {
            get { return Read<IntPtr>(TebStructure.EnvironmentPointer, out Success); }
            set { Write(TebStructure.EnvironmentPointer, value); }
        }

        /// <summary>
        /// The process Id.
        /// </summary>
        public Int32 ProcessId
        {
            get { return Read<Int32>(TebStructure.ProcessId, out Success); }
            set { Write(TebStructure.ProcessId, value); }
        }

        /// <summary>
        /// The current thread Id.
        /// </summary>
        public int ThreadId
        {
            get { return Read<int>(TebStructure.ThreadId, out Success); }
            set { Write(TebStructure.ThreadId, value); }
        }

        /// <summary>
        /// The active RPC handle.
        /// </summary>
        public IntPtr RpcHandle
        {
            get { return Read<IntPtr>(TebStructure.RpcHandle, out Success); }
            set { Write(TebStructure.RpcHandle, value); }
        }

        /// <summary>
        /// The linear address of the thread-local storage (TLS) array.
        /// </summary>
        public IntPtr Tls
        {
            get { return Read<IntPtr>(TebStructure.Tls, out Success); }
            set { Write(TebStructure.Tls, value); }
        }

        /// <summary>
        /// The linear address of Process Environment Block (PEB).
        /// </summary>
        public IntPtr Peb
        {
            get { return Read<IntPtr>(TebStructure.Peb, out Success); }
            set { Write(TebStructure.Peb, value); }
        }

        /// <summary>
        /// The last error number.
        /// </summary>
        public Int32 LastErrorNumber
        {
            get { return Read<Int32>(TebStructure.LastErrorNumber, out Success); }
            set { Write(TebStructure.LastErrorNumber, value); }
        }

        /// <summary>
        /// The count of owned critical sections.
        /// </summary>
        public Int32 CriticalSectionsCount
        {
            get { return Read<Int32>(TebStructure.CriticalSectionsCount, out Success); }
            set { Write(TebStructure.CriticalSectionsCount, value); }
        }

        /// <summary>
        /// The address of CSR Client Thread.
        /// </summary>
        public IntPtr CsrClientThread
        {
            get { return Read<IntPtr>(TebStructure.CsrClientThread, out Success); }
            set { Write(TebStructure.CsrClientThread, value); }
        }

        /// <summary>
        /// Win32 Thread Information.
        /// </summary>
        public IntPtr Win32ThreadInfo
        {
            get { return Read<IntPtr>(TebStructure.Win32ThreadInfo, out Success); }
            set { Write(TebStructure.Win32ThreadInfo, value); }
        }

        /// <summary>
        /// Win32 client information (NT), user32 private data (Wine), 0x60 = LastError (Win95), 0x74 = LastError (WinME).
        /// </summary>
        public Byte[] Win32ClientInfo
        {
            get { return Read<Byte>(TebStructure.Win32ClientInfo, 124, out Success); }
            set { Write(TebStructure.Win32ClientInfo, value); }
        }

        /// <summary>
        /// Reserved for Wow64. Contains a pointer to FastSysCall in Wow64.
        /// </summary>
        public IntPtr WoW64Reserved
        {
            get { return Read<IntPtr>(TebStructure.WoW64Reserved, out Success); }
            set { Write(TebStructure.WoW64Reserved, value); }
        }

        /// <summary>
        /// The current locale
        /// </summary>
        public IntPtr CurrentLocale
        {
            get { return Read<IntPtr>(TebStructure.CurrentLocale, out Success); }
            set { Write(TebStructure.CurrentLocale, value); }
        }

        /// <summary>
        /// The FP Software Status Register.
        /// </summary>
        public IntPtr FpSoftwareStatusRegister
        {
            get { return Read<IntPtr>(TebStructure.FpSoftwareStatusRegister, out Success); }
            set { Write(TebStructure.FpSoftwareStatusRegister, value); }
        }

        /// <summary>
        /// Reserved for OS (NT), kernel32 private data (Wine).
        /// herein: FS:[0x124] 4 NT Pointer to KTHREAD (ETHREAD) structure.
        /// </summary>
        public Byte[] SystemReserved1
        {
            get { return Read<Byte>(TebStructure.SystemReserved1, 216, out Success); }
            set { Write(TebStructure.SystemReserved1, value); }
        }

        /// <summary>
        /// The exception code.
        /// </summary>
        public IntPtr ExceptionCode
        {
            get { return Read<IntPtr>(TebStructure.ExceptionCode, out Success); }
            set { Write(TebStructure.ExceptionCode, value); }
        }

        /// <summary>
        /// The activation context stack.
        /// </summary>
        public Byte[] ActivationContextStack
        {
            get { return Read<Byte>(TebStructure.ActivationContextStack, 18, out Success); }
            set { Write(TebStructure.ActivationContextStack, value); }
        }

        /// <summary>
        /// The spare bytes (NT), ntdll private data (Wine).
        /// </summary>
        public Byte[] SpareBytes
        {
            get { return Read<Byte>(TebStructure.SpareBytes, 26, out Success); }
            set { Write(TebStructure.SpareBytes, value); }
        }

        /// <summary>
        /// Reserved for OS (NT), ntdll private data (Wine).
        /// </summary>
        public Byte[] SystemReserved2
        {
            get { return Read<Byte>(TebStructure.SystemReserved2, 40, out Success); }
            set { Write(TebStructure.SystemReserved2, value); }
        }

        /// <summary>
        /// The GDI TEB Batch (OS), vm86 private data (Wine).
        /// </summary>
        public Byte[] GdiTebBatch
        {
            get { return Read<Byte>(TebStructure.GdiTebBatch, 1248, out Success); }
            set { Write(TebStructure.GdiTebBatch, value); }
        }

        /// <summary>
        /// The GDI Region.
        /// </summary>
        public IntPtr GdiRegion
        {
            get { return Read<IntPtr>(TebStructure.GdiRegion, out Success); }
            set { Write(TebStructure.GdiRegion, value); }
        }

        /// <summary>
        /// The GDI Pen.
        /// </summary>
        public IntPtr GdiPen
        {
            get { return Read<IntPtr>(TebStructure.GdiPen, out Success); }
            set { Write(TebStructure.GdiPen, value); }
        }

        /// <summary>
        /// The GDI Brush.
        /// </summary>
        public IntPtr GdiBrush
        {
            get { return Read<IntPtr>(TebStructure.GdiBrush, out Success); }
            set { Write(TebStructure.GdiBrush, value); }
        }

        /// <summary>
        /// The real process Id.
        /// </summary>
        public Int32 RealProcessId
        {
            get { return Read<Int32>(TebStructure.RealProcessId, out Success); }
            set { Write(TebStructure.RealProcessId, value); }
        }

        /// <summary>
        /// The real thread Id.
        /// </summary>
        public Int32 RealThreadId
        {
            get { return Read<Int32>(TebStructure.RealThreadId, out Success); }
            set { Write(TebStructure.RealThreadId, value); }
        }

        /// <summary>
        /// The GDI cached process handle.
        /// </summary>
        public IntPtr GdiCachedProcessHandle
        {
            get { return Read<IntPtr>(TebStructure.GdiCachedProcessHandle, out Success); }
            set { Write(TebStructure.GdiCachedProcessHandle, value); }
        }

        /// <summary>
        /// The GDI client process Id (PID).
        /// </summary>
        public IntPtr GdiClientProcessId
        {
            get { return Read<IntPtr>(TebStructure.GdiClientProcessId, out Success); }
            set { Write(TebStructure.GdiClientProcessId, value); }
        }

        /// <summary>
        /// The GDI client thread Id (TID).
        /// </summary>
        public IntPtr GdiClientThreadId
        {
            get { return Read<IntPtr>(TebStructure.GdiClientThreadId, out Success); }
            set { Write(TebStructure.GdiClientThreadId, value); }
        }

        /// <summary>
        /// The GDI thread locale information.
        /// </summary>
        public IntPtr GdiThreadLocalInfo
        {
            get { return Read<IntPtr>(TebStructure.GdiThreadLocalInfo, out Success); }
            set { Write(TebStructure.GdiThreadLocalInfo, value); }
        }

        /// <summary>
        /// Reserved for user application.
        /// </summary>
        public Byte[] UserReserved1
        {
            get { return Read<Byte>(TebStructure.UserReserved1, 20, out Success); }
            set { Write(TebStructure.UserReserved1, value); }
        }

        /// <summary>
        /// Reserved for GL.
        /// </summary>
        public Byte[] GlReserved1
        {
            get { return Read<Byte>(TebStructure.GlReserved1, 1248, out Success); }
            set { Write(TebStructure.GlReserved1, value); }
        }

        /// <summary>
        /// The last value status value.
        /// </summary>
        public Int32 LastStatusValue
        {
            get { return Read<Int32>(TebStructure.LastStatusValue, out Success); }
            set { Write(TebStructure.LastStatusValue, value); }
        }

        /// <summary>
        /// The static UNICODE_STRING buffer.
        /// </summary>
        public Byte[] StaticUnicodeString
        {
            get { return Read<Byte>(TebStructure.StaticUnicodeString, 532, out Success); }
            set { Write(TebStructure.StaticUnicodeString, value); }
        }

        /// <summary>
        /// The pointer to deallocation stack.
        /// </summary>
        public IntPtr DeallocationStack
        {
            get { return Read<IntPtr>(TebStructure.DeallocationStack, out Success); }
            set { Write(TebStructure.DeallocationStack, value); }
        }

        /// <summary>
        /// The TLS slots, 4 byte per slot.
        /// </summary>
        public IntPtr[] TlsSlots
        {
            get { return Read<IntPtr>(TebStructure.TlsSlots, 64, out Success); }
            set { Write(TebStructure.TlsSlots, value); }
        }

        /// <summary>
        /// The TLS links (LIST_ENTRY structure).
        /// </summary>
        public Int64 TlsLinks
        {
            get { return Read<Int64>(TebStructure.TlsLinks, out Success); }
            set { Write(TebStructure.TlsLinks, value); }
        }

        /// <summary>
        /// Virtual DOS Machine.
        /// </summary>
        public IntPtr Vdm
        {
            get { return Read<IntPtr>(TebStructure.Vdm, out Success); }
            set { Write(TebStructure.Vdm, value); }
        }

        /// <summary>
        /// Reserved for RPC.
        /// </summary>
        public IntPtr RpcReserved
        {
            get { return Read<IntPtr>(TebStructure.RpcReserved, out Success); }
            set { Write(TebStructure.RpcReserved, value); }
        }

        /// <summary>
        /// The thread error mode (RtlSetThreadErrorMode).
        /// </summary>
        public IntPtr ThreadErrorMode
        {
            get { return Read<IntPtr>(TebStructure.ThreadErrorMode, out Success); }
            set { Write(TebStructure.ThreadErrorMode, value); }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedTeb"/> class.
        /// </summary>
        /// <param name="MemorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        /// <param name="Address">The location of the teb.</param>
        internal ManagedTeb(MemorySharp MemorySharp, IntPtr Address) : base(MemorySharp, Address)
        {

        }

        #endregion

        #region Methods
        /// <summary>
        /// Finds the Thread Environment Block address of a specified thread.
        /// </summary>
        /// <param name="ThreadHandle">A handle of the thread.</param>
        /// <returns>A <see cref="IntPtr"/> pointer of the TEB.</returns>
        public static IntPtr FindTeb(SafeMemoryHandle ThreadHandle)
        {
            return ThreadCore.NtQueryInformationThread(ThreadHandle).TebBaseAdress;
        }

        #endregion

    } // End class

} // End namespace