using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using IStream = System.Runtime.InteropServices.ComTypes.IStream;

namespace Microsoft.Diagnostics.Runtime.ICorDebug
{
    /// <summary>
    /// Wrapper for the ICLRDebugging shim interface. This interface exposes the native pipeline
    /// architecture startup APIs
    /// </summary>
    sealed class CLRDebugging
    {
        private static readonly Guid clsidCLRDebugging = new Guid("BACC578D-FBDD-48a4-969F-02D932B74634");
        private ICLRDebugging _clrDebugging;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>Creates the underlying interface from mscoree!CLRCreateInstance</remarks>
        public CLRDebugging()
        {
            object o;
            Guid ifaceId = typeof(ICLRDebugging).GUID;
            Guid clsid = clsidCLRDebugging;
            NativeMethods.CLRCreateInstance(ref clsid, ref ifaceId, out o);
            _clrDebugging = (ICLRDebugging)o;
        }


        public static ICorDebug GetDebuggerForProcess(int processID, string minimumVersion, DebuggerCallBacks callBacks = null)
        {
            CLRMetaHost mh = new CLRMetaHost();
            CLRRuntimeInfo highestLoadedRuntime = null;
            foreach (var runtime in mh.EnumerateLoadedRuntimes(processID))
            {
                if (highestLoadedRuntime == null ||
                    string.Compare(highestLoadedRuntime.GetVersionString(), runtime.GetVersionString(), StringComparison.OrdinalIgnoreCase) < 0)
                    highestLoadedRuntime = runtime;
            }
            if (highestLoadedRuntime == null)
                throw new ApplicationException("Could not enumerate .NET runtimes on the system.");

            var runtimeVersion = highestLoadedRuntime.GetVersionString();
            if (string.Compare(runtimeVersion, minimumVersion, StringComparison.OrdinalIgnoreCase) < 0)
                throw new ApplicationException("Runtime in process " + runtimeVersion + " below the minimum of " + minimumVersion);

            ICorDebug rawDebuggingAPI = highestLoadedRuntime.GetLegacyICorDebugInterface();
            if (rawDebuggingAPI == null)
                throw new ArgumentException("Cannot be null.", "rawDebugggingAPI");
            rawDebuggingAPI.Initialize();
            if (callBacks == null)
                callBacks = new DebuggerCallBacks();
            rawDebuggingAPI.SetManagedHandler(callBacks);
            return rawDebuggingAPI;
        }

        public static ICorDebugProcess CreateICorDebugProcess(ulong baseAddress, ICorDebugDataTarget dataTarget, ICLRDebuggingLibraryProvider libraryProvider)
        {
            Debug.Assert(baseAddress != 0);
            
            Version version;
            ClrDebuggingProcessFlags flags;
            ICorDebugProcess process;
            int errorCode = new CLRDebugging().TryOpenVirtualProcess(baseAddress, dataTarget, libraryProvider, new Version(4, 6, 0x7fff, 0x7fff), out version, out flags, out process);
            if (errorCode < 0)
            {
                if (((errorCode != -2146231228) && (errorCode != -2146231226)) && (errorCode != -2146231225))
                {
                    Marshal.ThrowExceptionForHR(errorCode);
                }

                process = null;
            }
            
            return process;
        }

        /// <summary>
        /// Detects if a native module represents a CLR and if so provides the debugging interface
        /// and versioning information
        /// </summary>
        /// <param name="moduleBaseAddress">The native base address of a module which might be a CLR</param>
        /// <param name="dataTarget">The process abstraction which can be used for inspection</param>
        /// <param name="libraryProvider">A callback interface for locating version specific debug libraries
        /// such as mscordbi.dll and mscordacwks.dll</param>
        /// <param name="maxDebuggerSupportedVersion">The highest version of the CLR/debugging libraries which
        /// the caller can support</param>
        /// <param name="version">The version of the CLR detected or null if no CLR was detected</param>
        /// <param name="flags">Flags which have additional information about the CLR.
        /// See ClrDebuggingProcessFlags for more details</param>
        /// <returns>The CLR's debugging interface</returns>
        public ICorDebugProcess OpenVirtualProcess(ulong moduleBaseAddress,
            ICorDebugDataTarget dataTarget,
            ICLRDebuggingLibraryProvider libraryProvider,
            Version maxDebuggerSupportedVersion,
            out Version version,
            out ClrDebuggingProcessFlags flags)
        {
            ICorDebugProcess process;
            int hr = TryOpenVirtualProcess(moduleBaseAddress, dataTarget, libraryProvider, maxDebuggerSupportedVersion, out version, out flags, out process);
            if (hr < 0)
                throw new Exception("Failed to OpenVirtualProcess for module at " + moduleBaseAddress + ".  hr = " + hr.ToString("x"));
            return process;
        }

        /// <summary>
        /// Version of the above that doesn't throw exceptions on failure
        /// </summary>        
        public int TryOpenVirtualProcess(ulong moduleBaseAddress,
            ICorDebugDataTarget dataTarget,
            ICLRDebuggingLibraryProvider libraryProvider,
            Version maxDebuggerSupportedVersion,
            out Version version,
            out ClrDebuggingProcessFlags flags,
            out ICorDebugProcess process)
        {
            ClrDebuggingVersion maxSupport = new ClrDebuggingVersion();
            ClrDebuggingVersion clrVersion = new ClrDebuggingVersion();
            maxSupport.StructVersion = 0;
            maxSupport.Major = (short)maxDebuggerSupportedVersion.Major;
            maxSupport.Minor = (short)maxDebuggerSupportedVersion.Minor;
            maxSupport.Build = (short)maxDebuggerSupportedVersion.Build;
            maxSupport.Revision = (short)maxDebuggerSupportedVersion.Revision;
            object processIface = null;
            clrVersion.StructVersion = 0;
            Guid iid = typeof(ICorDebugProcess).GUID;

            int result = _clrDebugging.OpenVirtualProcess(moduleBaseAddress, dataTarget, libraryProvider,
                ref maxSupport, ref iid, out processIface, ref clrVersion, out flags);

            // This may be set regardless of success/failure
            version = new Version(clrVersion.Major, clrVersion.Minor, clrVersion.Build, clrVersion.Revision);

            if (result < 0)
            {
                // OpenVirtualProcess failed
                process = null;
                return result;
            }

            // Success
            process = (ICorDebugProcess)processIface;
            return 0;
        }

        /// <summary>
        /// Determines if the module is no longer in use
        /// </summary>
        /// <param name="moduleHandle">A module handle that was provided via the ILibraryProvider</param>
        /// <returns>True if the module can be unloaded, False otherwise</returns>
        public bool CanUnloadNow(IntPtr moduleHandle)
        {
            int ret = _clrDebugging.CanUnloadNow(moduleHandle);
            if (ret == 0)   // S_OK
                return true;
            else if (ret == (int)1) // S_FALSE
                return false;
            else
                Marshal.ThrowExceptionForHR(ret);

            //unreachable
            throw new Exception();
        }

    }

    class DebuggerCallBacks : ICorDebugManagedCallback3, ICorDebugManagedCallback2, ICorDebugManagedCallback
    {
        public virtual void CustomNotification(ICorDebugThread pThread, ICorDebugAppDomain pAppDomain) { pAppDomain.Continue(0); }
        public virtual void FunctionRemapOpportunity(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread, ICorDebugFunction pOldFunction, ICorDebugFunction pNewFunction, uint oldILOffset) { pAppDomain.Continue(0); }
        public virtual void CreateConnection(ICorDebugProcess pProcess, uint dwConnectionId, ref ushort pConnName) { pProcess.Continue(0); }
        public virtual void ChangeConnection(ICorDebugProcess pProcess, uint dwConnectionId) { pProcess.Continue(0); }
        public virtual void DestroyConnection(ICorDebugProcess pProcess, uint dwConnectionId) { pProcess.Continue(0); }
        public virtual void Exception(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread, ICorDebugFrame pFrame, uint nOffset, CorDebugExceptionCallbackType dwEventType, uint dwFlags) { pAppDomain.Continue(0); }
        public virtual void ExceptionUnwind(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread, CorDebugExceptionUnwindCallbackType dwEventType, uint dwFlags) { pAppDomain.Continue(0); }
        public virtual void FunctionRemapComplete(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread, ICorDebugFunction pFunction) { pAppDomain.Continue(0); }
        public virtual void MDANotification(ICorDebugController pController, ICorDebugThread pThread, ICorDebugMDA pMDA) { pController.Continue(0); }
        public virtual void Breakpoint(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread, ICorDebugBreakpoint pBreakpoint) { pAppDomain.Continue(0); }
        public virtual void StepComplete(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread, ICorDebugStepper pStepper, CorDebugStepReason reason) { pAppDomain.Continue(0); }
        public virtual void Break(ICorDebugAppDomain pAppDomain, ICorDebugThread thread) { pAppDomain.Continue(0); }
        public virtual void Exception(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread, int unhandled) { pAppDomain.Continue(0); }
        public virtual void EvalComplete(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread, ICorDebugEval pEval) { pAppDomain.Continue(0); }
        public virtual void EvalException(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread, ICorDebugEval pEval) { pAppDomain.Continue(0); }
        public virtual void CreateProcess(ICorDebugProcess pProcess) { pProcess.Continue(0); }
        public virtual void ExitProcess(ICorDebugProcess pProcess) { pProcess.Continue(0); }
        public virtual void CreateThread(ICorDebugAppDomain pAppDomain, ICorDebugThread thread) { pAppDomain.Continue(0); }
        public virtual void ExitThread(ICorDebugAppDomain pAppDomain, ICorDebugThread thread) { pAppDomain.Continue(0); }
        public virtual void LoadModule(ICorDebugAppDomain pAppDomain, ICorDebugModule pModule) { pAppDomain.Continue(0); }
        public virtual void UnloadModule(ICorDebugAppDomain pAppDomain, ICorDebugModule pModule) { pAppDomain.Continue(0); }
        public virtual void LoadClass(ICorDebugAppDomain pAppDomain, ICorDebugClass c) { pAppDomain.Continue(0); }
        public virtual void UnloadClass(ICorDebugAppDomain pAppDomain, ICorDebugClass c) { pAppDomain.Continue(0); }
        public virtual void DebuggerError(ICorDebugProcess pProcess, int errorHR, uint errorCode) { pProcess.Continue(0); }
        public virtual void LogMessage(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread, int lLevel, string pLogSwitchName, string pMessage) { pAppDomain.Continue(0); }
        public virtual void LogSwitch(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread, int lLevel, uint ulReason, string pLogSwitchName, string pParentName) { pAppDomain.Continue(0); }
        public virtual void CreateAppDomain(ICorDebugProcess pProcess, ICorDebugAppDomain pAppDomain) { pAppDomain.Continue(0); }
        public virtual void ExitAppDomain(ICorDebugProcess pProcess, ICorDebugAppDomain pAppDomain) { pAppDomain.Continue(0); }
        public virtual void LoadAssembly(ICorDebugAppDomain pAppDomain, ICorDebugAssembly pAssembly) { pAppDomain.Continue(0); }
        public virtual void UnloadAssembly(ICorDebugAppDomain pAppDomain, ICorDebugAssembly pAssembly) { pAppDomain.Continue(0); }
        public virtual void ControlCTrap(ICorDebugProcess pProcess) { pProcess.Continue(0); }
        public virtual void NameChange(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread) { pAppDomain.Continue(0); }
        public virtual void UpdateModuleSymbols(ICorDebugAppDomain pAppDomain, ICorDebugModule pModule, IStream pSymbolStream) { pAppDomain.Continue(0); }
        public virtual void EditAndContinueRemap(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread, ICorDebugFunction pFunction, int fAccurate) { pAppDomain.Continue(0); }
        public virtual void BreakpointSetError(ICorDebugAppDomain pAppDomain, ICorDebugThread pThread, ICorDebugBreakpoint pBreakpoint, uint dwError) { pAppDomain.Continue(0); }
    }

}
