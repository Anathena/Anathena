// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Diagnostics.Runtime.Utilities;
using Address = System.UInt64;

namespace Microsoft.Diagnostics.Runtime
{
    /// <summary>
    /// The type of frame the ClrStackFrame represents.
    /// </summary>
    public enum ClrStackFrameType
    {
        /// <summary>
        /// Indicates this stack frame is unknown
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Indicates this stack frame is a standard managed method.
        /// </summary>
        ManagedMethod = 0,

        /// <summary>
        /// Indicates this stack frame is a special stack marker that the Clr runtime leaves on the stack.
        /// Note that the ClrStackFrame may still have a ClrMethod associated with the marker.
        /// </summary>
        Runtime = 1
    }

    /// <summary>
    /// A frame in a managed stack trace.  Note you can call ToString on an instance of this object to get the
    /// function name (or clr!Frame name) similar to SOS's !clrstack output.
    /// </summary>
    public abstract class ClrStackFrame
    {
        /// <summary>
        /// Returns the thread this stack frame came from.
        /// </summary>
        public abstract ClrThread Thread { get; }

        /// <summary>
        /// The instruction pointer of this frame.
        /// </summary>
        public abstract Address InstructionPointer { get; }

        /// <summary>
        /// The stack pointer of this frame.
        /// </summary>
        public abstract Address StackPointer { get; }

        /// <summary>
        /// The type of frame (managed or internal).
        /// </summary>
        public abstract ClrStackFrameType Kind { get; }

        /// <summary>
        /// The string to display in a stack trace.  Similar to !clrstack output.
        /// </summary>
        public abstract string DisplayString { get; }

        /// <summary>
        /// Returns the ClrMethod which corresponds to the current stack frame.  This may be null if the
        /// current frame is actually a CLR "Internal Frame" representing a marker on the stack, and that
        /// stack marker does not have a managed method associated with it.
        /// </summary>
        public abstract ClrMethod Method { get; }

        /// <summary>
        /// Returns the module name to use for building the stack trace.
        /// </summary>
        public virtual string ModuleName
        {
            get
            {
                if (Method == null || Method.Type == null || Method.Type.Module == null)
                    return UnknownModuleName;

                string result = Method.Type.Module.Name;
                try
                {
                    return Path.GetFileNameWithoutExtension(result);
                }
                catch
                {
                    return result;
                }
            }
        }

        /// <summary>
        /// The default name used when a module name cannot be calculated.
        /// </summary>
        public static string UnknownModuleName = "UNKNOWN";
    }
    
    /// <summary>
    /// Represents a managed thread in the target process.  Note this does not wrap purely native threads
    /// in the target process (that is, threads which have never run managed code before).
    /// </summary>
    public abstract class ClrThread
    {
        /// <summary>
        /// Gets the runtime associated with this thread.
        /// </summary>
        public abstract ClrRuntime Runtime { get; }

        /// <summary>
        /// The suspension state of the thread according to the runtime.
        /// </summary>
        public abstract GcMode GcMode { get; }

        /// <summary>
        /// Returns true if this is the finalizer thread.
        /// </summary>
        public abstract bool IsFinalizer { get; }

        /// <summary>
        /// The address of the underlying datastructure which makes up the Thread object.  This
        /// serves as a unique identifier.
        /// </summary>
        public abstract Address Address { get; }

        /// <summary>
        /// Returns true if the thread is alive in the process, false if this thread was recently terminated.
        /// </summary>
        public abstract bool IsAlive { get; }

        /// <summary>
        /// The OS thread id for the thread.
        /// </summary>
        public abstract uint OSThreadId { get; }

        /// <summary>
        /// The managed thread ID (this is equivalent to System.Threading.Thread.ManagedThreadId
        /// in the target process).
        /// </summary>
        public abstract int ManagedThreadId { get; }

        /// <summary>
        /// The AppDomain the thread is running in.
        /// </summary>
        public abstract Address AppDomain { get; }

        /// <summary>
        /// The number of managed locks (Monitors) the thread has currently entered but not left.
        /// This will be highly inconsistent unless the process is stopped.
        /// </summary>
        public abstract uint LockCount { get; }

        /// <summary>
        /// The TEB (thread execution block) address in the process.
        /// </summary>
        public abstract Address Teb { get; }

        /// <summary>
        /// The base of the stack for this thread, or 0 if the value could not be obtained.
        /// </summary>
        public abstract Address StackBase { get; }

        /// <summary>
        /// The limit of the stack for this thread, or 0 if the value could not be obtained.
        /// </summary>
        public abstract Address StackLimit { get; }

        /// <summary>
        /// Enumerates the GC references (objects) on the stack.  This is equivalent to
        /// EnumerateStackObjects(true).
        /// </summary>
        /// <returns>An enumeration of GC references on the stack as the GC sees them.</returns>
        public abstract IEnumerable<ClrRoot> EnumerateStackObjects();

        /// <summary>
        /// Enumerates the GC references (objects) on the stack.
        /// </summary>
        /// <param name="includePossiblyDead">Include all objects found on the stack.  Passing
        /// false attempts to replicate the behavior of the GC, reporting only live objects.</param>
        /// <returns>An enumeration of GC references on the stack as the GC sees them.</returns>
        public abstract IEnumerable<ClrRoot> EnumerateStackObjects(bool includePossiblyDead);

        /// <summary>
        /// Returns the managed stack trace of the thread.  Note that this property may return incomplete
        /// data in the case of a bad stack unwind or if there is a very large number of methods on the stack.
        /// (This is usually caused by a stack overflow on the target thread, stack corruption which leads to
        /// a bad stack unwind, or other inconsistent state in the target debuggee.)
        /// 
        /// Note: This property uses a heuristic to attempt to detect bad unwinds to stop enumerating
        /// frames by inspecting the stack pointer and instruction pointer of each frame to ensure the stack
        /// walk is "making progress".  Additionally we cap the number of frames returned by this method
        /// as another safegaurd.  This means we may not have all frames even if the stack walk was making
        /// progress.
        /// 
        /// If you want to ensure that you receive an un-clipped stack trace, you should use EnumerateStackTrace
        /// instead of this property, and be sure to handle the case of repeating stack frames.
        /// </summary>
        public abstract IList<ClrStackFrame> StackTrace { get; }

        /// <summary>
        /// Enumerates a stack trace for a given thread.  Note this method may loop infinitely in the case of
        /// stack corruption or other stack unwind issues which can happen in practice.  When enumerating frames
        /// out of this method you should be careful to either set a maximum loop count, or to ensure the stack
        /// unwind is making progress by ensuring that ClrStackFrame.StackPointer is making progress (though it
        /// is expected that sometimes two frames may return the same StackPointer in some corner cases).
        /// </summary>
        /// <returns>An enumeration of stack frames.</returns>
        public abstract IEnumerable<ClrStackFrame> EnumerateStackTrace();

        /// <summary>
        /// Returns the exception currently on the thread.  Note that this field may be null.  Also note
        /// that this is basically the "last thrown exception", and may be stale...meaning the thread could
        /// be done processing the exception but a crash dump was taken before the current exception was
        /// cleared off the field.
        /// </summary>
        public abstract ClrException CurrentException { get; }


        /// <summary>
        /// Returns if this thread is a GC thread.  If the runtime is using a server GC, then there will be
        /// dedicated GC threads, which this will indicate.  For a runtime using the workstation GC, this flag
        /// will only be true for a thread which is currently running a GC (and the background GC thread).
        /// </summary>
        public abstract bool IsGC { get; }

        /// <summary>
        /// Returns if this thread is the debugger helper thread.
        /// </summary>
        public abstract bool IsDebuggerHelper { get; }

        /// <summary>
        /// Returns true if this thread is a threadpool timer thread.
        /// </summary>
        public abstract bool IsThreadpoolTimer { get; }

        /// <summary>
        /// Returns true if this thread is a threadpool IO completion port.
        /// </summary>
        public abstract bool IsThreadpoolCompletionPort { get; }

        /// <summary>
        /// Returns true if this is a threadpool worker thread.
        /// </summary>
        public abstract bool IsThreadpoolWorker { get; }

        /// <summary>
        /// Returns true if this is a threadpool wait thread.
        /// </summary>
        public abstract bool IsThreadpoolWait { get; }

        /// <summary>
        /// Returns true if this is the threadpool gate thread.
        /// </summary>
        public abstract bool IsThreadpoolGate { get; }

        /// <summary>
        /// Returns if this thread currently suspending the runtime.
        /// </summary>
        public abstract bool IsSuspendingEE { get; }

        /// <summary>
        /// Returns true if this thread is currently the thread shutting down the runtime.
        /// </summary>
        public abstract bool IsShutdownHelper { get; }

        /// <summary>
        /// Returns true if an abort was requested for this thread (such as Thread.Abort, or AppDomain unload).
        /// </summary>
        public abstract bool IsAbortRequested { get; }

        /// <summary>
        /// Returns true if this thread was aborted.
        /// </summary>
        public abstract bool IsAborted { get; }

        /// <summary>
        /// Returns true if the GC is attempting to suspend this thread.
        /// </summary>
        public abstract bool IsGCSuspendPending { get; }

        /// <summary>
        /// Returns true if the user has suspended the thread (using Thread.Suspend).
        /// </summary>
        public abstract bool IsUserSuspended { get; }

        /// <summary>
        /// Returns true if the debugger has suspended the thread.
        /// </summary>
        public abstract bool IsDebugSuspended { get; }

        /// <summary>
        /// Returns true if this thread is a background thread.  (That is, if the thread does not keep the
        /// managed execution environment alive and running.)
        /// </summary>
        public abstract bool IsBackground { get; }

        /// <summary>
        /// Returns true if this thread was created, but not started.
        /// </summary>
        public abstract bool IsUnstarted { get; }

        /// <summary>
        /// Returns true if the Clr runtime called CoIntialize for this thread.
        /// </summary>
        public abstract bool IsCoInitialized { get; }

        /// <summary>
        /// Returns true if this thread is in a COM single threaded apartment.
        /// </summary>
        public abstract bool IsSTA { get; }

        /// <summary>
        /// Returns true if the thread is a COM multithreaded apartment.
        /// </summary>
        public abstract bool IsMTA { get; }

        /// <summary>
        /// Returns the object this thread is blocked waiting on, or null if the thread is not blocked.
        /// </summary>
        public abstract IList<BlockingObject> BlockingObjects { get; }
    }


    internal abstract class ThreadBase : ClrThread
    {
        public override Address Address
        {
            get { return _address; }
        }

        public override bool IsFinalizer
        {
            get { return _finalizer; }
        }

        public override bool IsGC
        {
            get { return (ThreadType & (int)TlsThreadType.ThreadType_GC) == (int)TlsThreadType.ThreadType_GC; }
        }

        public override bool IsDebuggerHelper
        {
            get { return (ThreadType & (int)TlsThreadType.ThreadType_DbgHelper) == (int)TlsThreadType.ThreadType_DbgHelper; }
        }

        public override bool IsThreadpoolTimer
        {
            get { return (ThreadType & (int)TlsThreadType.ThreadType_Timer) == (int)TlsThreadType.ThreadType_Timer; }
        }

        public override bool IsThreadpoolCompletionPort
        {
            get
            {
                return (ThreadType & (int)TlsThreadType.ThreadType_Threadpool_IOCompletion) == (int)TlsThreadType.ThreadType_Threadpool_IOCompletion
                    || (_threadState & (int)ThreadState.TS_CompletionPortThread) == (int)ThreadState.TS_CompletionPortThread;
            }
        }

        public override bool IsThreadpoolWorker
        {
            get
            {
                return (ThreadType & (int)TlsThreadType.ThreadType_Threadpool_Worker) == (int)TlsThreadType.ThreadType_Threadpool_Worker
                    || (_threadState & (int)ThreadState.TS_TPWorkerThread) == (int)ThreadState.TS_TPWorkerThread;
            }
        }

        public override bool IsThreadpoolWait
        {
            get { return (ThreadType & (int)TlsThreadType.ThreadType_Wait) == (int)TlsThreadType.ThreadType_Wait; }
        }

        public override bool IsThreadpoolGate
        {
            get { return (ThreadType & (int)TlsThreadType.ThreadType_Gate) == (int)TlsThreadType.ThreadType_Gate; }
        }

        public override bool IsSuspendingEE
        {
            get { return (ThreadType & (int)TlsThreadType.ThreadType_DynamicSuspendEE) == (int)TlsThreadType.ThreadType_DynamicSuspendEE; }
        }

        public override bool IsShutdownHelper
        {
            get { return (ThreadType & (int)TlsThreadType.ThreadType_ShutdownHelper) == (int)TlsThreadType.ThreadType_ShutdownHelper; }
        }



        public override bool IsAborted
        {
            get { return (_threadState & (int)ThreadState.TS_Aborted) == (int)ThreadState.TS_Aborted; }
        }

        public override bool IsGCSuspendPending
        {
            get { return (_threadState & (int)ThreadState.TS_GCSuspendPending) == (int)ThreadState.TS_GCSuspendPending; }
        }

        public override bool IsUserSuspended
        {
            get { return (_threadState & (int)ThreadState.TS_UserSuspendPending) == (int)ThreadState.TS_UserSuspendPending; }
        }

        public override bool IsDebugSuspended
        {
            get { return (_threadState & (int)ThreadState.TS_DebugSuspendPending) == (int)ThreadState.TS_DebugSuspendPending; }
        }

        public override bool IsBackground
        {
            get { return (_threadState & (int)ThreadState.TS_Background) == (int)ThreadState.TS_Background; }
        }

        public override bool IsUnstarted
        {
            get { return (_threadState & (int)ThreadState.TS_Unstarted) == (int)ThreadState.TS_Unstarted; }
        }

        public override bool IsCoInitialized
        {
            get { return (_threadState & (int)ThreadState.TS_CoInitialized) == (int)ThreadState.TS_CoInitialized; }
        }

        public override GcMode GcMode
        {
            get { return _preemptive ? GcMode.Preemptive : GcMode.Cooperative; }
        }

        public override bool IsSTA
        {
            get { return (_threadState & (int)ThreadState.TS_InSTA) == (int)ThreadState.TS_InSTA; }
        }

        public override bool IsMTA
        {
            get { return (_threadState & (int)ThreadState.TS_InMTA) == (int)ThreadState.TS_InMTA; }
        }

        public override bool IsAbortRequested
        {
            get
            {
                return (_threadState & (int)ThreadState.TS_AbortRequested) == (int)ThreadState.TS_AbortRequested
                    || (_threadState & (int)ThreadState.TS_AbortInitiated) == (int)ThreadState.TS_AbortInitiated;
            }
        }


        public override bool IsAlive { get { return _osThreadId != 0 && (_threadState & ((int)ThreadState.TS_Unstarted | (int)ThreadState.TS_Dead)) == 0; } }
        public override uint OSThreadId { get { return _osThreadId; } }
        public override int ManagedThreadId { get { return (int)_managedThreadId; } }
        public override ulong AppDomain { get { return _appDomain; } }
        public override uint LockCount { get { return _lockCount; } }
        public override ulong Teb { get { return _teb; } }

        internal void SetBlockingObjects(BlockingObject[] blobjs)
        {
            _blockingObjs = blobjs;
        }

        #region Helper Enums
        internal enum TlsThreadType
        {
            ThreadType_GC = 0x00000001,
            ThreadType_Timer = 0x00000002,
            ThreadType_Gate = 0x00000004,
            ThreadType_DbgHelper = 0x00000008,
            //ThreadType_Shutdown = 0x00000010,
            ThreadType_DynamicSuspendEE = 0x00000020,
            //ThreadType_Finalizer = 0x00000040,
            //ThreadType_ADUnloadHelper = 0x00000200,
            ThreadType_ShutdownHelper = 0x00000400,
            ThreadType_Threadpool_IOCompletion = 0x00000800,
            ThreadType_Threadpool_Worker = 0x00001000,
            ThreadType_Wait = 0x00002000,
        }

        private enum ThreadState
        {
            //TS_Unknown                = 0x00000000,    // threads are initialized this way

            TS_AbortRequested = 0x00000001,    // Abort the thread
            TS_GCSuspendPending = 0x00000002,    // waiting to get to safe spot for GC
            TS_UserSuspendPending = 0x00000004,    // user suspension at next opportunity
            TS_DebugSuspendPending = 0x00000008,    // Is the debugger suspending threads?
                                                    //TS_GCOnTransitions        = 0x00000010,    // Force a GC on stub transitions (GCStress only)

            //TS_LegalToJoin            = 0x00000020,    // Is it now legal to attempt a Join()
            //TS_YieldRequested         = 0x00000040,    // The task should yield
            //TS_Hijacked               = 0x00000080,    // Return address has been hijacked
            //TS_BlockGCForSO           = 0x00000100,    // If a thread does not have enough stack, WaitUntilGCComplete may fail.
            // Either GC suspension will wait until the thread has cleared this bit,
            // Or the current thread is going to spin if GC has suspended all threads.
            TS_Background = 0x00000200,    // Thread is a background thread
            TS_Unstarted = 0x00000400,    // Thread has never been started
            TS_Dead = 0x00000800,    // Thread is dead

            //TS_WeOwn                  = 0x00001000,    // Exposed object initiated this thread
            TS_CoInitialized = 0x00002000,    // CoInitialize has been called for this thread

            TS_InSTA = 0x00004000,    // Thread hosts an STA
            TS_InMTA = 0x00008000,    // Thread is part of the MTA

            // Some bits that only have meaning for reporting the state to clients.
            //TS_ReportDead             = 0x00010000,    // in WaitForOtherThreads()

            //TS_TaskReset              = 0x00040000,    // The task is reset

            //TS_SyncSuspended          = 0x00080000,    // Suspended via WaitSuspendEvent
            //TS_DebugWillSync          = 0x00100000,    // Debugger will wait for this thread to sync

            //TS_StackCrawlNeeded       = 0x00200000,    // A stackcrawl is needed on this thread, such as for thread abort
            // See comment for s_pWaitForStackCrawlEvent for reason.

            //TS_SuspendUnstarted       = 0x00400000,    // latch a user suspension on an unstarted thread

            TS_Aborted = 0x00800000,    // is the thread aborted?
            TS_TPWorkerThread = 0x01000000,    // is this a threadpool worker thread?

            //TS_Interruptible          = 0x02000000,    // sitting in a Sleep(), Wait(), Join()
            //TS_Interrupted            = 0x04000000,    // was awakened by an interrupt APC. !!! This can be moved to TSNC

            TS_CompletionPortThread = 0x08000000,    // Completion port thread

            TS_AbortInitiated = 0x10000000,    // set when abort is begun

            //TS_Finalized              = 0x20000000,    // The associated managed Thread object has been finalized.
            // We can clean up the unmanaged part now.

            //TS_FailStarted            = 0x40000000,    // The thread fails during startup.
            //TS_Detached               = 0x80000000,    // Thread was detached by DllMain
        }
        #endregion

        #region Internal Methods
        private void InitTls()
        {
            if (_tlsInit)
                return;

            _tlsInit = true;

            _threadType = GetTlsSlotForThread((RuntimeBase)Runtime, Teb);
        }

        internal static int GetTlsSlotForThread(RuntimeBase runtime, ulong teb)
        {
            const int maxTlsSlot = 64;
            const int tlsSlotOffset = 0x1480; // Same on x86 and amd64
            const int tlsExpansionSlotsOffset = 0x1780;
            uint ptrSize = (uint)runtime.PointerSize;

            ulong lowerTlsSlots = teb + tlsSlotOffset;
            uint clrTlsSlot = runtime.GetTlsSlot();
            if (clrTlsSlot == uint.MaxValue)
                return 0;

            ulong tlsSlot = 0;
            if (clrTlsSlot < maxTlsSlot)
            {
                tlsSlot = lowerTlsSlots + ptrSize * clrTlsSlot;
            }
            else
            {
                if (!runtime.ReadPointer(teb + tlsExpansionSlotsOffset, out tlsSlot) || tlsSlot == 0)
                    return 0;

                tlsSlot += ptrSize * (clrTlsSlot - maxTlsSlot);
            }

            ulong clrTls = 0;
            if (!runtime.ReadPointer(tlsSlot, out clrTls))
                return 0;

            // Get thread data;

            uint tlsThreadTypeIndex = runtime.GetThreadTypeIndex();
            if (tlsThreadTypeIndex == uint.MaxValue)
                return 0;

            ulong threadType = 0;
            if (!runtime.ReadPointer(clrTls + ptrSize * tlsThreadTypeIndex, out threadType))
                return 0;

            return (int)threadType;
        }

        internal ThreadBase(Desktop.IThreadData thread, ulong address, bool finalizer)
        {
            _address = address;
            _finalizer = finalizer;

            Debug.Assert(thread != null);
            if (thread != null)
            {
                _osThreadId = thread.OSThreadID;
                _managedThreadId = thread.ManagedThreadID;
                _appDomain = thread.AppDomain;
                _lockCount = thread.LockCount;
                _teb = thread.Teb;
                _threadState = thread.State;
                _exception = thread.ExceptionPtr;
                _preemptive = thread.Preemptive;
            }
        }


        protected uint _osThreadId;
        protected IList<ClrStackFrame> _stackTrace;
        protected bool _finalizer;

        protected bool _tlsInit;
        protected int _threadType;
        protected int _threadState;
        protected uint _managedThreadId;
        protected uint _lockCount;
        protected ulong _address;
        protected ulong _appDomain;
        protected ulong _teb;
        protected ulong _exception;
        protected BlockingObject[] _blockingObjs;
        protected bool _preemptive;
        protected int ThreadType { get { InitTls(); return _threadType; } }
        #endregion
    }
}