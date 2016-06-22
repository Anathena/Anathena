// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Diagnostics.Runtime.Desktop;
using Microsoft.Diagnostics.Runtime.Interop;
using Microsoft.Diagnostics.Runtime.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Diagnostics.Runtime.ICorDebug;

namespace Microsoft.Diagnostics.Runtime
{
    /// <summary>
    /// Represents the version of a DLL.
    /// </summary>
    [Serializable]
    public struct VersionInfo
    {
        /// <summary>
        /// In a version 'A.B.C.D', this field represents 'A'.
        /// </summary>
        public int Major;

        /// <summary>
        /// In a version 'A.B.C.D', this field represents 'B'.
        /// </summary>
        public int Minor;

        /// <summary>
        /// In a version 'A.B.C.D', this field represents 'C'.
        /// </summary>
        public int Revision;

        /// <summary>
        /// In a version 'A.B.C.D', this field represents 'D'.
        /// </summary>
        public int Patch;

        internal VersionInfo(int major, int minor, int revision, int patch)
        {
            Major = major;
            Minor = minor;
            Revision = revision;
            Patch = patch;
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>The A.B.C.D version prepended with 'v'.</returns>
        public override string ToString()
        {
            return string.Format("v{0}.{1}.{2}.{3:D2}", Major, Minor, Revision, Patch);
        }
    }

    /// <summary>
    /// Returns the "flavor" of CLR this module represents.
    /// </summary>
    public enum ClrFlavor
    {
        /// <summary>
        /// This is the full version of CLR included with windows.
        /// </summary>
        Desktop = 0,

        /// <summary>
        /// This is a reduced CLR used in other projects.
        /// </summary>
        CoreCLR = 1,
        
        /// <summary>
        /// Used for .Net Native.
        /// </summary>
        Native = 2
    }

    /// <summary>
    /// Represents information about a single Clr runtime in a process.
    /// </summary>
    [Serializable]
    public class ClrInfo : IComparable
    {
        /// <summary>
        /// The version number of this runtime.
        /// </summary>
        public VersionInfo Version { get { return ModuleInfo.Version; } }

        /// <summary>
        /// The type of CLR this module represents.
        /// </summary>
        public ClrFlavor Flavor { get; private set; }

        /// <summary>
        /// Returns module information about the Dac needed create a ClrRuntime instance for this runtime.
        /// </summary>
        public DacInfo DacInfo { get; private set; }

        /// <summary>
        /// Returns module information about the ClrInstance.
        /// </summary>
        public ModuleInfo ModuleInfo { get; private set; }

        /// <summary>
        /// Returns the location of the local dac on your machine which matches this version of Clr, or null
        /// if one could not be found.
        /// </summary>
        public string LocalMatchingDac { get { return _dacLocation; } }
        
        /// <summary>
        /// Creates a runtime from the given Dac file on disk.
        /// </summary>
        public ClrRuntime CreateRuntime()
        {
            string dac = _dacLocation;
            if (dac != null && !File.Exists(dac))
                dac = null;

            if (dac == null)
                dac = _dataTarget.SymbolLocator.FindBinary(DacInfo);

            if (!File.Exists(dac))
                throw new FileNotFoundException(DacInfo.FileName);

            if (IntPtr.Size != (int)_dataTarget.DataReader.GetPointerSize())
                throw new InvalidOperationException("Mismatched architecture between this process and the dac.");

            return ConstructRuntime(dac);
        }

        /// <summary>
        /// Creates a runtime from a given IXClrDataProcess interface.  Used for debugger plugins.
        /// </summary>
        public ClrRuntime CreateRuntime(object clrDataProcess)
        {
            DacLibrary lib = new DacLibrary(_dataTarget, (IXCLRDataProcess)clrDataProcess);

            // Figure out what version we are on.
            if (clrDataProcess is Desktop.ISOSDac)
            {
                return new Desktop.V45Runtime(this, _dataTarget, lib);
            }
            else
            {
                byte[] buffer = new byte[System.Runtime.InteropServices.Marshal.SizeOf(typeof(Desktop.V2HeapDetails))];

                int val = lib.DacInterface.Request(Desktop.DacRequests.GCHEAPDETAILS_STATIC_DATA, 0, null, (uint)buffer.Length, buffer);
                if ((uint)val == (uint)0x80070057)
                    return new Desktop.LegacyRuntime(this, _dataTarget, lib, Desktop.DesktopVersion.v4, 10000);
                else
                    return new Desktop.LegacyRuntime(this, _dataTarget, lib, Desktop.DesktopVersion.v2, 3054);
            }
        }

        /// <summary>
        /// Creates a runtime from the given Dac file on disk.
        /// </summary>
        /// <param name="dacFilename">A full path to the matching mscordacwks for this process.</param>
        /// <param name="ignoreMismatch">Whether or not to ignore mismatches between </param>
        /// <returns></returns>
        public ClrRuntime CreateRuntime(string dacFilename, bool ignoreMismatch = false)
        {
            if (string.IsNullOrEmpty(dacFilename))
                throw new ArgumentNullException("dacFilename");

            if (!File.Exists(dacFilename))
                throw new FileNotFoundException(dacFilename);

            if (!ignoreMismatch)
            {
                int major, minor, revision, patch;
                NativeMethods.GetFileVersion(dacFilename, out major, out minor, out revision, out patch);
                if (major != Version.Major || minor != Version.Minor || revision != Version.Revision || patch != Version.Patch)
                    throw new InvalidOperationException(string.Format("Mismatched dac. Version: {0}.{1}.{2}.{3}", major, minor, revision, patch));
            }

            return ConstructRuntime(dacFilename);
        }

        private ClrRuntime ConstructRuntime(string dac)
        {
            if (IntPtr.Size != (int)_dataTarget.DataReader.GetPointerSize())
                throw new InvalidOperationException("Mismatched architecture between this process and the dac.");

            if (_dataTarget.IsMinidump)
                _dataTarget.SymbolLocator.PrefetchBinary(ModuleInfo.FileName, (int)ModuleInfo.TimeStamp, (int)ModuleInfo.FileSize);

            DacLibrary lib = new DacLibrary(_dataTarget, dac);

            Desktop.DesktopVersion ver;
            if (Flavor == ClrFlavor.CoreCLR)
            {
                return new Desktop.V45Runtime(this, _dataTarget, lib);
            }
            else if (Flavor == ClrFlavor.Native)
            {
                return new Native.NativeRuntime(this, _dataTarget, lib);
            }
            else if (Version.Major == 2)
            {
                ver = Desktop.DesktopVersion.v2;
            }
            else if (Version.Major == 4 && Version.Minor == 0 && Version.Patch < 10000)
            {
                ver = Desktop.DesktopVersion.v4;
            }
            else
            {
                // Assume future versions will all work on the newest runtime version.
                return new Desktop.V45Runtime(this, _dataTarget, lib);
            }

            return new Desktop.LegacyRuntime(this, _dataTarget, lib, ver, Version.Patch);
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>A version string for this Clr runtime.</returns>
        public override string ToString()
        {
            return Version.ToString();
        }

        internal ClrInfo(DataTargetImpl dt, ClrFlavor flavor, ModuleInfo module, DacInfo dacInfo, string dacLocation)
        {
            Debug.Assert(dacInfo != null);

            Flavor = flavor;
            DacInfo = dacInfo;
            ModuleInfo = module;
            module.IsRuntime = true;
            _dataTarget = dt;
            _dacLocation = dacLocation;
        }

        internal ClrInfo()
        {
        }

        private string _dacLocation;
        private DataTargetImpl _dataTarget;

        /// <summary>
        /// IComparable.  Sorts the object by version.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>-1 if less, 0 if equal, 1 if greater.</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            if (!(obj is ClrInfo))
                throw new InvalidOperationException("Object not ClrInfo.");

            ClrFlavor flv = ((ClrInfo)obj).Flavor;
            if (flv != Flavor)
                return flv.CompareTo(Flavor);  // Intentionally reversed.

            VersionInfo rhs = ((ClrInfo)obj).Version;
            if (Version.Major != rhs.Major)
                return Version.Major.CompareTo(rhs.Major);


            if (Version.Minor != rhs.Minor)
                return Version.Minor.CompareTo(rhs.Minor);


            if (Version.Revision != rhs.Revision)
                return Version.Revision.CompareTo(rhs.Revision);

            return Version.Patch.CompareTo(rhs.Patch);
        }
    }

    /// <summary>
    /// Specifies how to attach to a live process.
    /// </summary>
    public enum AttachFlag
    {
        /// <summary>
        /// Performs an invasive debugger attach.  Allows the consumer of this API to control the target
        /// process through normal IDebug function calls.  The process will be paused.
        /// </summary>
        Invasive,

        /// <summary>
        /// Performs a non-invasive debugger attach.  The process will be paused by this attached (and
        /// for the duration of the attach) but the caller cannot control the target process.  This is
        /// useful when there's already a debugger attached to the process.
        /// </summary>
        NonInvasive,

        /// <summary>
        /// Performs a "passive" attach, meaning no debugger is actually attached to the target process.
        /// The process is not paused, so queries for quickly changing data (such as the contents of the
        /// GC heap or callstacks) will be highly inconsistent unless the user pauses the process through
        /// other means.  Useful when attaching with ICorDebug (managed debugger), as you cannot use a
        /// non-invasive attach with ICorDebug.
        /// </summary>
        Passive
    }

    /// <summary>
    /// Information about a specific PDB instance obtained from a PE image.
    /// </summary>
    [Serializable]
    public class PdbInfo
    {
        /// <summary>
        /// The Guid of the PDB.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// The pdb revision.
        /// </summary>
        public int Revision { get; set; }

        /// <summary>
        /// The filename of the pdb.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Creates an instance of the PdbInfo class
        /// </summary>
        public PdbInfo()
        {
        }

        /// <summary>
        /// Creates an instance of the PdbInfo class with the corresponding properties initialized
        /// </summary>
        public PdbInfo(string fileName, Guid guid, int rev)
        {
            FileName = fileName;
            Guid = guid;
            Revision = rev;
        }
    }

    /// <summary>
    /// Provides information about loaded modules in a DataTarget
    /// </summary>
    [Serializable]
    public class ModuleInfo
    {
        /// <summary>
        /// The base address of the object.
        /// </summary>
        public virtual ulong ImageBase { get; set; }

        /// <summary>
        /// The filesize of the image.
        /// </summary>
        public virtual uint FileSize { get; set; }

        /// <summary>
        /// The build timestamp of the image.
        /// </summary>
        public virtual uint TimeStamp { get; set; }

        /// <summary>
        /// The filename of the module on disk.
        /// </summary>
        public virtual string FileName { get; set; }

        /// <summary>
        /// Returns true if this module is a native (non-managed) .Net runtime module.
        /// </summary>
        public bool IsRuntime { get; internal set; }

        /// <summary>
        /// Returns a PEFile from a stream constructed using instance fields of this object.
        /// If the PEFile cannot be constructed correctly, null is returned
        /// </summary>
        /// <returns></returns>
        public PEFile GetPEFile()
        {
            return PEFile.TryLoad(new ReadVirtualStream(_dataReader, (long)ImageBase, (long)FileSize), true);
        }

        /// <summary>
        /// Whether the module is managed or not.
        /// </summary>
        public virtual bool IsManaged
        {
            get
            {
                InitData();
                return _managed ?? false;
            }
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>The filename of the module.</returns>
        public override string ToString()
        {
            return FileName;
        }

        /// <summary>
        /// The PDB associated with this module.
        /// </summary>
        public PdbInfo Pdb
        {
            get
            {
                if (_pdb != null || _dataReader == null)
                    return _pdb;

                InitData();
                return _pdb;
            }

            set
            {
                _pdb = value;
            }
        }

        private void InitData()
        {
            if (_dataReader == null)
                return;

            if (_pdb != null && _managed != null)
                return;

            PdbInfo pdb = null;
            PEFile file = null;
            try
            {
                file = PEFile.TryLoad(new ReadVirtualStream(_dataReader, (long)ImageBase, (long)FileSize), true);
                if (file == null)
                    return;

                _managed = file.Header.ComDescriptorDirectory.VirtualAddress != 0;

                string pdbName;
                Guid guid;
                int age;
                if (file.GetPdbSignature(out pdbName, out guid, out age))
                {
                    pdb = new PdbInfo();
                    pdb.FileName = pdbName;
                    pdb.Guid = guid;
                    pdb.Revision = age;
                    _pdb = pdb;
                }
            }
            catch
            {
            }
            finally
            {
                if (file != null)
                    file.Dispose();
            }
        }

        /// <summary>
        /// The version information for this file.
        /// </summary>
        public VersionInfo Version
        {
            get
            {
                if (_versionInit || _dataReader == null)
                    return _version;

                _dataReader.GetVersionInfo(ImageBase, out _version);
                _versionInit = true;
                return _version;
            }

            set
            {
                _version = value;
                _versionInit = true;
            }
        }



        /// <summary>
        /// Empty constructor for serialization.
        /// </summary>
        public ModuleInfo()
        {
        }

        /// <summary>
        /// Creates a ModuleInfo object with an IDataReader instance.  This is used when
        /// lazily evaluating VersionInfo. 
        /// </summary>
        /// <param name="reader"></param>
        public ModuleInfo(IDataReader reader)
        {
            _dataReader = reader;
        }
        [NonSerialized]

        private IDataReader _dataReader;
        private PdbInfo _pdb;
        private bool? _managed;
        private VersionInfo _version;
        private bool _versionInit;
    }

    /// <summary>
    /// Represents the dac dll
    /// </summary>
    [Serializable]
    public class DacInfo : ModuleInfo
    {
        /// <summary>
        /// Returns the filename of the dac dll according to the specified parameters
        /// </summary>
        public static string GetDacRequestFileName(ClrFlavor flavor, Runtime.Architecture currentArchitecture, Runtime.Architecture targetArchitecture, VersionInfo clrVersion)
        {
            if (flavor == ClrFlavor.Native)
                return targetArchitecture == Runtime.Architecture.Amd64 ? "mrt100dac_winamd64.dll" : "mrt100dac_winx86.dll";

            string dacName = flavor == ClrFlavor.CoreCLR ? "mscordaccore" : "mscordacwks";
            return string.Format("{0}_{1}_{2}_{3}.{4}.{5}.{6:D2}.dll", dacName, currentArchitecture, targetArchitecture, clrVersion.Major, clrVersion.Minor, clrVersion.Revision, clrVersion.Patch);
        }

        /// <summary>
        /// The platform-agnostice filename of the dac dll
        /// </summary>
        public string PlatformAgnosticFileName { get; set; }

        /// <summary>
        /// The architecture (x86 or amd64) being targeted
        /// </summary>
        public Architecture TargetArchitecture { get; set; }

        /// <summary>
        /// Constructs a DacInfo object with the appropriate properties initialized
        /// </summary>
        public DacInfo(IDataReader reader, string agnosticName, Architecture targetArch)
            : base(reader)
        {
            PlatformAgnosticFileName = agnosticName;
            TargetArchitecture = targetArch;
        }
    }


    /// <summary>
    /// The result of a VirtualQuery.
    /// </summary>
    [Serializable]
    public struct VirtualQueryData
    {
        /// <summary>
        /// The base address of the allocation.
        /// </summary>
        public ulong BaseAddress;

        /// <summary>
        ///  The size of the allocation.
        /// </summary>
        public ulong Size;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="addr">Base address of the memory range.</param>
        /// <param name="size">The size of the memory range.</param>
        public VirtualQueryData(ulong addr, ulong size)
        {
            BaseAddress = addr;
            Size = size;
        }
    }

    /// <summary>
    /// An interface for reading data out of the target process.
    /// </summary>
    public interface IDataReader
    {
        /// <summary>
        /// Called when the DataTarget is closing (Disposing).  Used to clean up resources.
        /// </summary>
        void Close();

        /// <summary>
        /// Informs the data reader that the user has requested all data be flushed.
        /// </summary>
        void Flush();

        /// <summary>
        /// Gets the architecture of the target.
        /// </summary>
        /// <returns>The architecture of the target.</returns>
        Architecture GetArchitecture();

        /// <summary>
        /// Gets the size of a pointer in the target process.
        /// </summary>
        /// <returns>The pointer size of the target process.</returns>
        uint GetPointerSize();

        /// <summary>
        /// Enumerates modules in the target process.
        /// </summary>
        /// <returns>A list of the modules in the target process.</returns>
        IList<ModuleInfo> EnumerateModules();

        /// <summary>
        /// Gets the version information for a given module (given by the base address of the module).
        /// </summary>
        /// <param name="baseAddress">The base address of the module to look up.</param>
        /// <param name="version">The version info for the given module.</param>
        void GetVersionInfo(ulong baseAddress, out VersionInfo version);

        /// <summary>
        /// Read memory out of the target process.
        /// </summary>
        /// <param name="address">The address of memory to read.</param>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="bytesRequested">The number of bytes to read.</param>
        /// <param name="bytesRead">The number of bytes actually read out of the target process.</param>
        /// <returns>True if any bytes were read at all, false if the read failed (and no bytes were read).</returns>
        bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead);

        /// <summary>
        /// Read memory out of the target process.
        /// </summary>
        /// <param name="address">The address of memory to read.</param>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="bytesRequested">The number of bytes to read.</param>
        /// <param name="bytesRead">The number of bytes actually read out of the target process.</param>
        /// <returns>True if any bytes were read at all, false if the read failed (and no bytes were read).</returns>
        bool ReadMemory(ulong address, IntPtr buffer, int bytesRequested, out int bytesRead);
        
        /// <summary>
        /// Returns true if the data target is a minidump (or otherwise may not contain full heap data).
        /// </summary>
        /// <returns>True if the data target is a minidump (or otherwise may not contain full heap data).</returns>
        bool IsMinidump { get; }

        /// <summary>
        /// Gets the TEB of the specified thread.
        /// </summary>
        /// <param name="thread">The OS thread ID to get the TEB for.</param>
        /// <returns>The address of the thread's teb.</returns>
        ulong GetThreadTeb(uint thread);

        /// <summary>
        /// Enumerates the OS thread ID of all threads in the process.
        /// </summary>
        /// <returns>An enumeration of all threads in the target process.</returns>
        IEnumerable<uint> EnumerateAllThreads();

        /// <summary>
        /// Gets information about the given memory range.
        /// </summary>
        /// <param name="addr">An arbitrary address in the target process.</param>
        /// <param name="vq">The base address and size of the allocation.</param>
        /// <returns>True if the address was found and vq was filled, false if the address is not valid memory.</returns>
        bool VirtualQuery(ulong addr, out VirtualQueryData vq);

        /// <summary>
        /// Gets the thread context for the given thread.
        /// </summary>
        /// <param name="threadID">The OS thread ID to read the context from.</param>
        /// <param name="contextFlags">The requested context flags, or 0 for default flags.</param>
        /// <param name="contextSize">The size (in bytes) of the context parameter.</param>
        /// <param name="context">A pointer to the buffer to write to.</param>
        bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, IntPtr context);

        /// <summary>
        /// Gets the thread context for the given thread.
        /// </summary>
        /// <param name="threadID">The OS thread ID to read the context from.</param>
        /// <param name="contextFlags">The requested context flags, or 0 for default flags.</param>
        /// <param name="contextSize">The size (in bytes) of the context parameter.</param>
        /// <param name="context">A pointer to the buffer to write to.</param>
        bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, byte[] context);

        /// <summary>
        /// Read a pointer out of the target process.
        /// </summary>
        /// <returns>The pointer at the give address, or 0 if that pointer doesn't exist in
        /// the data target.</returns>
        ulong ReadPointerUnsafe(ulong addr);

        /// <summary>
        /// Read an int out of the target process.
        /// </summary>
        /// <returns>The int at the give address, or 0 if that pointer doesn't exist in
        /// the data target.</returns>
        uint ReadDwordUnsafe(ulong addr);
    }

    /// <summary>
    /// The type of crash dump reader to use.
    /// </summary>
    public enum CrashDumpReader
    {
        /// <summary>
        /// Use DbgEng.  This allows the user to obtain an instance of IDebugClient through the
        /// DataTarget.DebuggerInterface property, at the cost of strict threading requirements.
        /// </summary>
        DbgEng,

        /// <summary>
        /// Use a simple dump reader to read data out of the crash dump.  This allows processing
        /// multiple dumps (using separate DataTargets) on multiple threads, but the
        /// DataTarget.DebuggerInterface property will return null.
        /// </summary>
        ClrMD
    }


    /// <summary>
    /// A crash dump or live process to read out of.
    /// </summary>
    public abstract class DataTarget : IDisposable
    {
        /// <summary>
        /// Creates a DataTarget from a crash dump.
        /// </summary>
        /// <param name="fileName">The crash dump's filename.</param>
        /// <returns>A DataTarget instance.</returns>
        public static DataTarget LoadCrashDump(string fileName)
        {
            DbgEngDataReader reader = new DbgEngDataReader(fileName);
            return CreateFromReader(reader, reader.DebuggerInterface);
        }


        /// <summary>
        /// Creates a DataTarget from a crash dump, specifying the dump reader to use.
        /// </summary>
        /// <param name="fileName">The crash dump's filename.</param>
        /// <param name="dumpReader">The type of dump reader to use.</param>
        /// <returns>A DataTarget instance.</returns>
        public static DataTarget LoadCrashDump(string fileName, CrashDumpReader dumpReader)
        {
            if (dumpReader == CrashDumpReader.DbgEng)
            {
                DbgEngDataReader reader = new DbgEngDataReader(fileName);
                return CreateFromReader(reader, reader.DebuggerInterface);
            }
            else
            {
                DumpDataReader reader = new DumpDataReader(fileName);
                return CreateFromReader(reader, null);
            }
        }

        /// <summary>
        /// Create an instance of DataTarget from a user defined DataReader
        /// </summary>
        /// <param name="reader">A user defined DataReader.</param>
        /// <returns>A new DataTarget instance.</returns>
        public static DataTarget CreateFromDataReader(IDataReader reader)
        {
            return CreateFromReader(reader, null);
        }

        private static DataTarget CreateFromReader(IDataReader reader, Interop.IDebugClient client)
        {
#if _TRACING
            reader = new TraceDataReader(reader);
#endif
            return new DataTargetImpl(reader, client);
        }

        /// <summary>
        /// Creates a data target from an existing IDebugClient interface.  If you created and attached
        /// a dbgeng based debugger to a process you may pass the IDebugClient RCW object to this function
        /// to create the DataTarget.
        /// </summary>
        /// <param name="client">The dbgeng IDebugClient object.  We will query interface on this for IDebugClient.</param>
        /// <returns>A DataTarget instance.</returns>
        public static DataTarget CreateFromDebuggerInterface(Microsoft.Diagnostics.Runtime.Interop.IDebugClient client)
        {
            DbgEngDataReader reader = new DbgEngDataReader(client);
            DataTargetImpl dataTarget = new DataTargetImpl(reader, reader.DebuggerInterface);

            return dataTarget;
        }

        /// <summary>
        /// Invasively attaches to a live process.
        /// </summary>
        /// <param name="pid">The process ID of the process to attach to.</param>
        /// <param name="msecTimeout">Timeout in milliseconds.</param>
        /// <returns>A DataTarget instance.</returns>
        public static DataTarget AttachToProcess(int pid, uint msecTimeout)
        {
            return AttachToProcess(pid, msecTimeout, AttachFlag.Invasive);
        }

        /// <summary>
        /// Attaches to a live process.
        /// </summary>
        /// <param name="pid">The process ID of the process to attach to.</param>
        /// <param name="msecTimeout">Timeout in milliseconds.</param>
        /// <param name="attachFlag">The type of attach requested for the target process.</param>
        /// <returns>A DataTarget instance.</returns>
        public static DataTarget AttachToProcess(int pid, uint msecTimeout, AttachFlag attachFlag)
        {
            Microsoft.Diagnostics.Runtime.Interop.IDebugClient client = null;
            IDataReader reader;
            if (attachFlag == AttachFlag.Passive)
            {
                reader = new LiveDataReader(pid);
            }
            else
            {
                var dbgeng = new DbgEngDataReader(pid, attachFlag, msecTimeout);
                reader = dbgeng;
                client = dbgeng.DebuggerInterface;
            }

            DataTargetImpl dataTarget = new DataTargetImpl(reader, client);
            return dataTarget;
        }

        /// <summary>
        /// The data reader for this instance.
        /// </summary>
        public abstract IDataReader DataReader { get; }

        private SymbolLocator _symbolLocator;
        /// <summary>
        /// Instance to manage the symbol path(s)
        /// </summary>
        public SymbolLocator SymbolLocator
        {
            get
            {
                if (_symbolLocator == null)
                    _symbolLocator = new DefaultSymbolLocator();

                return _symbolLocator;
            }
            set
            {
                _symbolLocator = value;
            }
        }

        /// <summary>
        /// A symbol provider which loads PDBs on behalf of ClrMD.  This should be set so that when ClrMD needs to
        /// resolve names which can only come from PDBs.  If this is not set, you may have a degraded experience.
        /// </summary>
        public ISymbolProvider SymbolProvider { get; set; }

        FileLoader _fileLoader;
        internal FileLoader FileLoader
        {
            get
            {
                if (_fileLoader == null)
                    _fileLoader = new FileLoader(this);

                return _fileLoader;
            }
        }

        /// <summary>
        /// Returns true if the target process is a minidump, or otherwise might have limited memory.  If IsMinidump
        /// returns true, a greater range of functions may fail to return data due to the data not being present in
        /// the application/crash dump you are debugging.
        /// </summary>
        public abstract bool IsMinidump { get; }

        /// <summary>
        /// Returns the architecture of the target process or crash dump.
        /// </summary>
        public abstract Architecture Architecture { get; }

        /// <summary>
        /// Returns the list of Clr versions loaded into the process.
        /// </summary>
        public abstract IList<ClrInfo> ClrVersions { get; }

        /// <summary>
        /// Returns the pointer size for the target process.
        /// </summary>
        public abstract uint PointerSize { get; }

        /// <summary>
        /// Reads memory from the target.
        /// </summary>
        /// <param name="address">The address to read from.</param>
        /// <param name="buffer">The buffer to store the data in.  Size must be greator or equal to
        /// bytesRequested.</param>
        /// <param name="bytesRequested">The amount of bytes to read from the target process.</param>
        /// <param name="bytesRead">The actual number of bytes read.</param>
        /// <returns>True if any bytes were read out of the process (including a partial read).  False
        /// if no bytes could be read from the address.</returns>
        public abstract bool ReadProcessMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead);

        /// <summary>
        /// Returns the IDebugClient interface associated with this datatarget.  (Will return null if the
        /// user attached passively.)
        /// </summary>
        public abstract Microsoft.Diagnostics.Runtime.Interop.IDebugClient DebuggerInterface { get; }

        /// <summary>
        /// Enumerates information about the loaded modules in the process (both managed and unmanaged).
        /// </summary>
        public abstract IEnumerable<ModuleInfo> EnumerateModules();

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public abstract void Dispose();
    }

    internal class DataTargetImpl : DataTarget
    {
        private IDataReader _dataReader;
        private IDebugClient _client;
        private ClrInfo[] _versions;
        private Architecture _architecture;
        private ModuleInfo[] _modules;

        public DataTargetImpl(IDataReader dataReader, IDebugClient client)
        {
            if (dataReader == null)
                throw new ArgumentNullException("dataReader");

            _dataReader = dataReader;
            _client = client;
            _architecture = _dataReader.GetArchitecture();
        }

        public override IDataReader DataReader
        {
            get
            {
                return _dataReader;
            }
        }

        public override bool IsMinidump
        {
            get { return _dataReader.IsMinidump; }
        }

        public override Architecture Architecture
        {
            get { return _architecture; }
        }

        public override uint PointerSize
        {
            get { return _dataReader.GetPointerSize(); }
        }

        public override IList<ClrInfo> ClrVersions
        {
            get
            {
                if (_versions != null)
                    return _versions;

                List<ClrInfo> versions = new List<ClrInfo>();
                foreach (ModuleInfo module in EnumerateModules())
                {
                    string clrName = Path.GetFileNameWithoutExtension(module.FileName).ToLower();

                    if (clrName != "clr" && clrName != "mscorwks" && clrName != "coreclr" && clrName != "mrt100_app")
                        continue;

                    string dacLocation = Path.Combine(Path.GetDirectoryName(module.FileName), "mscordacwks.dll");
                    if (!File.Exists(dacLocation) || !NativeMethods.IsEqualFileVersion(dacLocation, module.Version))
                        dacLocation = null;

                    ClrFlavor flavor;
                    switch (clrName)
                    {
                        case "mrt100_app":
                            flavor = ClrFlavor.Native;
                            break;

                        case "coreclr":
                            flavor = ClrFlavor.CoreCLR;
                            break;

                        default:
                            flavor = ClrFlavor.Desktop;
                            break;
                    }

                    VersionInfo version = module.Version;
                    string dacAgnosticName = DacInfo.GetDacRequestFileName(flavor, Architecture, Architecture, version);
                    string dacFileName = DacInfo.GetDacRequestFileName(flavor, IntPtr.Size == 4 ? Architecture.X86 : Architecture.Amd64, Architecture, version);

                    DacInfo dacInfo = new DacInfo(_dataReader, dacAgnosticName, Architecture);
                    dacInfo.FileSize = module.FileSize;
                    dacInfo.TimeStamp = module.TimeStamp;
                    dacInfo.FileName = dacFileName;
                    dacInfo.Version = module.Version;

                    versions.Add(new ClrInfo(this, flavor, module, dacInfo, dacLocation));
                }

                _versions = versions.ToArray();

                Array.Sort(_versions);
                return _versions;
            }
        }

        public override bool ReadProcessMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead)
        {
            return _dataReader.ReadMemory(address, buffer, bytesRequested, out bytesRead);
        }

        public override IDebugClient DebuggerInterface
        {
            get { return _client; }
        }

        public override IEnumerable<ModuleInfo> EnumerateModules()
        {
            if (_modules == null)
                InitModules();

            return _modules;
        }

        private ModuleInfo FindModule(ulong addr)
        {
            if (_modules == null)
                InitModules();

            // TODO: Make binary search.
            foreach (var module in _modules)
                if (module.ImageBase <= addr && addr < module.ImageBase + module.FileSize)
                    return module;

            return null;
        }

        private void InitModules()
        {
            if (_modules == null)
            {
                var sortedModules = new List<ModuleInfo>(_dataReader.EnumerateModules());
                sortedModules.Sort((a, b) => a.ImageBase.CompareTo(b.ImageBase));
                _modules = sortedModules.ToArray();
            }
        }

        public override void Dispose()
        {
            _dataReader.Close();
        }
    }


    internal class DacLibrary
    {
        #region Variables
        private IntPtr _library;
        private DacDataTarget _dacDataTarget;
        private IXCLRDataProcess _dac;
        private ISOSDac _sos;
        private HashSet<object> _release = new HashSet<object>();
        #endregion

        public DacDataTarget DacDataTarget { get { return _dacDataTarget; } }

        public IXCLRDataProcess DacInterface { get { return _dac; } }

        public ISOSDac SOSInterface
        {
            get
            {
                if (_sos == null)
                    _sos = (ISOSDac)_dac;

                return _sos;
            }
        }

        public DacLibrary(DataTargetImpl dataTarget, object ix)
        {
            _dac = ix as IXCLRDataProcess;
            if (_dac == null)
                throw new ArgumentException("clrDataProcess not an instance of IXCLRDataProcess");
        }

        public DacLibrary(DataTargetImpl dataTarget, string dacDll)
        {
            if (dataTarget.ClrVersions.Count == 0)
                throw new ClrDiagnosticsException(String.Format("Process is not a CLR process!"));

            _library = NativeMethods.LoadLibrary(dacDll);
            if (_library == IntPtr.Zero)
                throw new ClrDiagnosticsException("Failed to load dac: " + dacDll);

            IntPtr addr = NativeMethods.GetProcAddress(_library, "CLRDataCreateInstance");
            _dacDataTarget = new DacDataTarget(dataTarget);

            object obj;
            NativeMethods.CreateDacInstance func = (NativeMethods.CreateDacInstance)Marshal.GetDelegateForFunctionPointer(addr, typeof(NativeMethods.CreateDacInstance));
            Guid guid = new Guid("5c552ab6-fc09-4cb3-8e36-22fa03c798b7");
            int res = func(ref guid, _dacDataTarget, out obj);

            if (res == 0)
                _dac = obj as IXCLRDataProcess;

            if (_dac == null)
                throw new ClrDiagnosticsException("Failure loading DAC: CreateDacInstance failed 0x" + res.ToString("x"), ClrDiagnosticsException.HR.DacError);
        }

        ~DacLibrary()
        {
            foreach (object obj in _release)
                Marshal.FinalReleaseComObject(obj);

            if (_dac != null)
                Marshal.FinalReleaseComObject(_dac);

            if (_library != IntPtr.Zero)
                NativeMethods.FreeLibrary(_library);
        }

        internal void AddToReleaseList(object obj)
        {
            Debug.Assert(Marshal.IsComObject(obj));
            _release.Add(obj);
        }
    }

    internal class DacDataTarget : IDacDataTarget, IMetadataLocator, ICorDebug.ICorDebugDataTarget
    {
        private DataTargetImpl _dataTarget;
        private IDataReader _dataReader;
        private ModuleInfo[] _modules;

        public DacDataTarget(DataTargetImpl dataTarget)
        {
            _dataTarget = dataTarget;
            _dataReader = _dataTarget.DataReader;
            _modules = dataTarget.EnumerateModules().ToArray();
            Array.Sort(_modules, delegate (ModuleInfo a, ModuleInfo b) { return a.ImageBase.CompareTo(b.ImageBase); });
        }


        public CorDebugPlatform GetPlatform()
        {
            var arch = _dataReader.GetArchitecture();

            switch (arch)
            {
                case Architecture.Amd64:
                    return CorDebugPlatform.CORDB_PLATFORM_WINDOWS_AMD64;

                case Architecture.X86:
                    return CorDebugPlatform.CORDB_PLATFORM_WINDOWS_X86;

                case Architecture.Arm:
                    return CorDebugPlatform.CORDB_PLATFORM_WINDOWS_ARM;

                default:
                    throw new Exception();
            }
        }

        public uint ReadVirtual(ulong address, IntPtr buffer, uint bytesRequested)
        {
            int read;
            if (ReadVirtual(address, buffer, (int)bytesRequested, out read) >= 0)
                return (uint)read;

            throw new Exception();
        }

        void ICorDebugDataTarget.GetThreadContext(uint threadId, uint contextFlags, uint contextSize, IntPtr context)
        {
            if (!_dataReader.GetThreadContext(threadId, contextFlags, contextSize, context))
                throw new Exception();
        }

        public void GetMachineType(out IMAGE_FILE_MACHINE machineType)
        {
            var arch = _dataReader.GetArchitecture();

            switch (arch)
            {
                case Architecture.Amd64:
                    machineType = IMAGE_FILE_MACHINE.AMD64;
                    break;

                case Architecture.X86:
                    machineType = IMAGE_FILE_MACHINE.I386;
                    break;

                case Architecture.Arm:
                    machineType = IMAGE_FILE_MACHINE.THUMB2;
                    break;

                default:
                    machineType = IMAGE_FILE_MACHINE.UNKNOWN;
                    break;
            }
        }

        private ModuleInfo GetModule(ulong address)
        {
            int min = 0, max = _modules.Length - 1;

            while (min <= max)
            {
                int i = (min + max) / 2;
                ModuleInfo curr = _modules[i];

                if (curr.ImageBase <= address && address < curr.ImageBase + curr.FileSize)
                    return curr;
                else if (curr.ImageBase < address)
                    min = i + 1;
                else
                    max = i - 1;
            }

            return null;
        }

        public void GetPointerSize(out uint pointerSize)
        {
            pointerSize = _dataReader.GetPointerSize();
        }

        public void GetImageBase(string imagePath, out ulong baseAddress)
        {
            imagePath = Path.GetFileNameWithoutExtension(imagePath);

            foreach (ModuleInfo module in _modules)
            {
                string moduleName = Path.GetFileNameWithoutExtension(module.FileName);
                if (imagePath.Equals(moduleName, StringComparison.CurrentCultureIgnoreCase))
                {
                    baseAddress = module.ImageBase;
                    return;
                }
            }

            throw new Exception();
        }

        public unsafe int ReadVirtual(ulong address, IntPtr buffer, int bytesRequested, out int bytesRead)
        {
            int read = 0;
            if (_dataReader.ReadMemory(address, buffer, bytesRequested, out read))
            {
                bytesRead = read;
                return 0;
            }

            ModuleInfo info = GetModule(address);
            if (info != null)
            {
                string filePath = _dataTarget.SymbolLocator.FindBinary(info.FileName, info.TimeStamp, info.FileSize, true);
                if (filePath == null)
                {
                    bytesRead = 0;
                    return -1;
                }

                // We do not put a using statement here to prevent needing to load/unload the binary over and over.
                PEFile file = _dataTarget.FileLoader.LoadPEFile(filePath);
                if (file != null)
                {
                    PEBuffer peBuffer = file.AllocBuff();

                    int rva = checked((int)(address - info.ImageBase));

                    if (file.Header.TryGetFileOffsetFromRva(rva, out rva))
                    {
                        byte* dst = (byte*)buffer.ToPointer();
                        byte* src = peBuffer.Fetch(rva, bytesRequested);

                        for (int i = 0; i < bytesRequested; i++)
                            dst[i] = src[i];

                        bytesRead = bytesRequested;
                        return 0;
                    }

                    file.FreeBuff(peBuffer);
                }
            }

            bytesRead = 0;
            return -1;
        }

        public int ReadMemory(ulong address, byte[] buffer, uint bytesRequested, out uint bytesRead)
        {
            int read = 0;
            if (_dataReader.ReadMemory(address, buffer, (int)bytesRequested, out read))
            {
                bytesRead = (uint)read;
                return 0;
            }

            bytesRead = 0;
            return -1;
        }

        public int ReadVirtual(ulong address, byte[] buffer, uint bytesRequested, out uint bytesRead)
        {
            return ReadMemory(address, buffer, bytesRequested, out bytesRead);
        }

        public void WriteVirtual(ulong address, byte[] buffer, uint bytesRequested, out uint bytesWritten)
        {
            // This gets used by MemoryBarrier() calls in the dac, which really shouldn't matter what we do here.
            bytesWritten = bytesRequested;
        }

        public void GetTLSValue(uint threadID, uint index, out ulong value)
        {
            // TODO:  Validate this is not used?
            value = 0;
        }

        public void SetTLSValue(uint threadID, uint index, ulong value)
        {
            throw new NotImplementedException();
        }

        public void GetCurrentThreadID(out uint threadID)
        {
            threadID = 0;
        }

        public void GetThreadContext(uint threadID, uint contextFlags, uint contextSize, IntPtr context)
        {
            _dataReader.GetThreadContext(threadID, contextFlags, contextSize, context);
        }

        public void SetThreadContext(uint threadID, uint contextSize, IntPtr context)
        {
            throw new NotImplementedException();
        }

        public void Request(uint reqCode, uint inBufferSize, IntPtr inBuffer, IntPtr outBufferSize, out IntPtr outBuffer)
        {
            throw new NotImplementedException();
        }

        public int GetMetadata(string filename, uint imageTimestamp, uint imageSize, IntPtr mvid, uint mdRva, uint flags, uint bufferSize, byte[] buffer, IntPtr dataSize)
        {
            string filePath = _dataTarget.SymbolLocator.FindBinary(filename, imageTimestamp, imageSize, true);
            if (filePath == null)
                return -1;

            // We do not put a using statement here to prevent needing to load/unload the binary over and over.
            PEFile file = _dataTarget.FileLoader.LoadPEFile(filePath);
            if (file == null)
                return -1;

            var comDescriptor = file.Header.ComDescriptorDirectory;
            if (comDescriptor.VirtualAddress == 0)
                return -1;

            PEBuffer peBuffer = file.AllocBuff();
            if (mdRva == 0)
            {
                IntPtr hdr = file.SafeFetchRVA((int)comDescriptor.VirtualAddress, (int)comDescriptor.Size, peBuffer);

                IMAGE_COR20_HEADER corhdr = (IMAGE_COR20_HEADER)Marshal.PtrToStructure(hdr, typeof(IMAGE_COR20_HEADER));
                if (bufferSize < corhdr.MetaData.Size)
                {
                    file.FreeBuff(peBuffer);
                    return -1;
                }

                mdRva = corhdr.MetaData.VirtualAddress;
                bufferSize = corhdr.MetaData.Size;
            }

            IntPtr ptr = file.SafeFetchRVA((int)mdRva, (int)bufferSize, peBuffer);
            Marshal.Copy(ptr, buffer, 0, (int)bufferSize);

            file.FreeBuff(peBuffer);
            return 0;
        }
    }

    internal unsafe class DbgEngDataReader : IDisposable, IDataReader
    {
        private static int s_totalInstanceCount = 0;
        private static bool s_needRelease = true;

        private IDebugClient _client;
        private IDebugDataSpaces _spaces;
        private IDebugDataSpaces2 _spaces2;
        private IDebugDataSpacesPtr _spacesPtr;
        private IDebugSymbols _symbols;
        private IDebugSymbols3 _symbols3;
        private IDebugControl2 _control;
        private IDebugAdvanced _advanced;
        private IDebugSystemObjects _systemObjects;
        private IDebugSystemObjects3 _systemObjects3;

        private uint _instance = 0;
        private bool _disposed;

        private byte[] _ptrBuffer = new byte[IntPtr.Size];
        private List<ModuleInfo> _modules;
        private bool? _minidump = null;

        ~DbgEngDataReader()
        {
            Dispose(false);
        }

        private void SetClientInstance()
        {
            Debug.Assert(s_totalInstanceCount > 0);

            if (_systemObjects3 != null && s_totalInstanceCount > 1)
                _systemObjects3.SetCurrentSystemId(_instance);
        }


        public DbgEngDataReader(string dumpFile)
        {
            if (!File.Exists(dumpFile))
                throw new FileNotFoundException(dumpFile);

            IDebugClient client = CreateIDebugClient();
            int hr = client.OpenDumpFile(dumpFile);

            if (hr != 0)
                throw new ClrDiagnosticsException(String.Format("Could not load crash dump '{0}', HRESULT: 0x{1:x8}", dumpFile, hr), ClrDiagnosticsException.HR.DebuggerError);

            CreateClient(client);

            // This actually "attaches" to the crash dump.
            _control.WaitForEvent(0, 0xffffffff);
        }

        public DbgEngDataReader(IDebugClient client)
        {
            //* We need to be very careful to not cleanup the IDebugClient interfaces
            // * (that is, detach from the target process) if we created this wrapper
            // * from a pre-existing IDebugClient interface.  Setting s_needRelease to
            // * false will keep us from *ever* explicitly detaching from any IDebug
            // * interface (even ones legitimately attached with other constructors),
            // * but this is the best we can do with DbgEng's design.  Better to leak
            // * a small amount of memory (and file locks) than detatch someone else's
            // * IDebug interface unexpectedly.
            // 
            CreateClient(client);
            s_needRelease = false;
        }

        public DbgEngDataReader(int pid, AttachFlag flags, uint msecTimeout)
        {
            IDebugClient client = CreateIDebugClient();
            CreateClient(client);

            DEBUG_ATTACH attach = (flags == AttachFlag.Invasive) ? DEBUG_ATTACH.DEFAULT : DEBUG_ATTACH.NONINVASIVE;
            int hr = _control.AddEngineOptions(DEBUG_ENGOPT.INITIAL_BREAK);

            if (hr == 0)
                hr = client.AttachProcess(0, (uint)pid, attach);

            if (hr == 0)
                hr = _control.WaitForEvent(0, msecTimeout);

            if (hr == 1)
            {
                throw new TimeoutException("Break in did not occur within the allotted timeout.");
            }
            else if (hr != 0)
            {
                if ((uint)hr == 0xd00000bb)
                    throw new InvalidOperationException("Mismatched architecture between this process and the target process.");

                throw new ClrDiagnosticsException(String.Format("Could not attach to pid {0:X}, HRESULT: 0x{1:x8}", pid, hr), ClrDiagnosticsException.HR.DebuggerError);
            }
        }



        public bool IsMinidump
        {
            get
            {
                if (_minidump != null)
                    return (bool)_minidump;

                SetClientInstance();

                DEBUG_CLASS cls;
                DEBUG_CLASS_QUALIFIER qual;
                _control.GetDebuggeeType(out cls, out qual);

                if (qual == DEBUG_CLASS_QUALIFIER.USER_WINDOWS_SMALL_DUMP)
                {
                    DEBUG_FORMAT flags;
                    _control.GetDumpFormatFlags(out flags);
                    _minidump = (flags & DEBUG_FORMAT.USER_SMALL_FULL_MEMORY) == 0;
                    return (bool)_minidump;
                }

                _minidump = false;
                return false;
            }
        }

        public Architecture GetArchitecture()
        {
            SetClientInstance();

            IMAGE_FILE_MACHINE machineType;
            int hr = _control.GetExecutingProcessorType(out machineType);
            if (0 != hr)
                throw new ClrDiagnosticsException(String.Format("Failed to get proessor type, HRESULT: {0:x8}", hr), ClrDiagnosticsException.HR.DebuggerError);

            switch (machineType)
            {
                case IMAGE_FILE_MACHINE.I386:
                    return Architecture.X86;

                case IMAGE_FILE_MACHINE.AMD64:
                    return Architecture.Amd64;

                case IMAGE_FILE_MACHINE.ARM:
                case IMAGE_FILE_MACHINE.THUMB:
                case IMAGE_FILE_MACHINE.THUMB2:
                    return Architecture.Arm;

                default:
                    return Architecture.Unknown;
            }
        }

        private static IDebugClient CreateIDebugClient()
        {
            Guid guid = new Guid("27fe5639-8407-4f47-8364-ee118fb08ac8");
            object obj;
            NativeMethods.DebugCreate(ref guid, out obj);

            IDebugClient client = (IDebugClient)obj;
            return client;
        }

        public void Close()
        {
            Dispose();
        }


        internal IDebugClient DebuggerInterface
        {
            get { return _client; }
        }

        public uint GetPointerSize()
        {
            SetClientInstance();
            int hr = _control.IsPointer64Bit();
            if (hr == 0)
                return 8;
            else if (hr == 1)
                return 4;

            throw new ClrDiagnosticsException(String.Format("IsPointer64Bit failed: {0:x8}", hr), ClrDiagnosticsException.HR.DebuggerError);
        }

        public void Flush()
        {
            _modules = null;
        }

        public bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, IntPtr context)
        {
            uint id = 0;
            GetThreadIdBySystemId(threadID, out id);

            SetCurrentThreadId(id);
            GetThreadContext(context, contextSize);

            return true;
        }

        private void GetThreadContext(IntPtr context, uint contextSize)
        {
            SetClientInstance();
            _advanced.GetThreadContext(context, contextSize);
        }

        internal int ReadVirtual(ulong address, byte[] buffer, int bytesRequested, out int bytesRead)
        {
            SetClientInstance();
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            if (buffer.Length < bytesRequested)
                bytesRequested = buffer.Length;

            uint read = 0;
            int res = _spaces.ReadVirtual(address, buffer, (uint)bytesRequested, out read);
            bytesRead = (int)read;
            return res;
        }


        private ulong[] GetImageBases()
        {
            List<ulong> bases = null;
            uint count, unloadedCount;
            if (GetNumberModules(out count, out unloadedCount) < 0)
                return null;

            bases = new List<ulong>((int)count);
            for (uint i = 0; i < count + unloadedCount; ++i)
            {
                ulong image;
                if (GetModuleByIndex(i, out image) < 0)
                    continue;

                bases.Add(image);
            }

            return bases.ToArray();
        }

        public IList<ModuleInfo> EnumerateModules()
        {
            if (_modules != null)
                return _modules;

            ulong[] bases = GetImageBases();
            DEBUG_MODULE_PARAMETERS[] mods = new DEBUG_MODULE_PARAMETERS[bases.Length];
            List<ModuleInfo> modules = new List<ModuleInfo>();

            if (bases != null && CanEnumerateModules)
            {
                int hr = GetModuleParameters(bases.Length, bases, 0, mods);
                if (hr >= 0)
                {
                    for (int i = 0; i < bases.Length; ++i)
                    {
                        ModuleInfo info = new ModuleInfo(this);
                        info.TimeStamp = mods[i].TimeDateStamp;
                        info.FileSize = mods[i].Size;
                        info.ImageBase = bases[i];

                        uint needed;
                        StringBuilder sbpath = new StringBuilder();
                        if (GetModuleNameString(DEBUG_MODNAME.IMAGE, i, bases[i], null, 0, out needed) >= 0 && needed > 1)
                        {
                            sbpath.EnsureCapacity((int)needed);
                            if (GetModuleNameString(DEBUG_MODNAME.IMAGE, i, bases[i], sbpath, needed, out needed) >= 0)
                                info.FileName = sbpath.ToString();
                        }

                        modules.Add(info);
                    }
                }
            }

            _modules = modules;
            return modules;
        }


        public bool CanEnumerateModules { get { return _symbols3 != null; } }

        internal int GetModuleParameters(int count, ulong[] bases, int start, DEBUG_MODULE_PARAMETERS[] mods)
        {
            SetClientInstance();
            return _symbols.GetModuleParameters((uint)count, bases, (uint)start, mods);
        }

        private void CreateClient(IDebugClient client)
        {
            _client = client;

            _spaces = (IDebugDataSpaces)_client;
            _spacesPtr = (IDebugDataSpacesPtr)_client;
            _symbols = (IDebugSymbols)_client;
            _control = (IDebugControl2)_client;

            // These interfaces may not be present in older DbgEng dlls.
            _spaces2 = _client as IDebugDataSpaces2;
            _symbols3 = _client as IDebugSymbols3;
            _advanced = _client as IDebugAdvanced;
            _systemObjects = _client as IDebugSystemObjects;
            _systemObjects3 = _client as IDebugSystemObjects3;

            Interlocked.Increment(ref s_totalInstanceCount);

            if (_systemObjects3 == null && s_totalInstanceCount > 1)
                throw new ClrDiagnosticsException("This version of DbgEng is too old to create multiple instances of DataTarget.", ClrDiagnosticsException.HR.DebuggerError);

            if (_systemObjects3 != null)
                _systemObjects3.GetCurrentSystemId(out _instance);
        }



        internal int GetModuleNameString(DEBUG_MODNAME Which, int Index, UInt64 Base, StringBuilder Buffer, UInt32 BufferSize, out UInt32 NameSize)
        {
            if (_symbols3 == null)
            {
                NameSize = 0;
                return -1;
            }

            SetClientInstance();
            return _symbols3.GetModuleNameString(Which, (uint)Index, Base, Buffer, BufferSize, out NameSize);
        }

        internal int GetNumberModules(out uint count, out uint unloadedCount)
        {
            if (_symbols3 == null)
            {
                count = 0;
                unloadedCount = 0;
                return -1;
            }

            SetClientInstance();
            return _symbols3.GetNumberModules(out count, out unloadedCount);
        }

        internal int GetModuleByIndex(uint i, out ulong image)
        {
            if (_symbols3 == null)
            {
                image = 0;
                return -1;
            }

            SetClientInstance();
            return _symbols3.GetModuleByIndex(i, out image);
        }

        internal int GetNameByOffsetWide(ulong offset, StringBuilder sb, int p, out uint size, out ulong disp)
        {
            SetClientInstance();
            return _symbols3.GetNameByOffsetWide(offset, sb, p, out size, out disp);
        }

        public bool VirtualQuery(ulong addr, out VirtualQueryData vq)
        {
            vq = new VirtualQueryData();
            if (_spaces2 == null)
                return false;

            MEMORY_BASIC_INFORMATION64 mem;
            SetClientInstance();
            int hr = _spaces2.QueryVirtual(addr, out mem);
            vq.BaseAddress = mem.BaseAddress;
            vq.Size = mem.RegionSize;

            return hr == 0;
        }


        public bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead)
        {
            return ReadVirtual(address, buffer, bytesRequested, out bytesRead) >= 0;
        }


        public ulong ReadPointerUnsafe(ulong addr)
        {
            int read;
            if (ReadVirtual(addr, _ptrBuffer, IntPtr.Size, out read) != 0)
                return 0;

            fixed (byte* r = _ptrBuffer)
            {
                if (IntPtr.Size == 4)
                    return *(((uint*)r));

                return *(((ulong*)r));
            }
        }

        public uint ReadDwordUnsafe(ulong addr)
        {
            int read;
            if (ReadVirtual(addr, _ptrBuffer, 4, out read) != 0)
                return 0;

            fixed (byte* r = _ptrBuffer)
                return *(((uint*)r));
        }

        internal void SetSymbolPath(string path)
        {
            SetClientInstance();
            _symbols.SetSymbolPath(path);
            _control.Execute(DEBUG_OUTCTL.NOT_LOGGED, ".reload", DEBUG_EXECUTE.NOT_LOGGED);
        }

        internal int QueryVirtual(ulong addr, out MEMORY_BASIC_INFORMATION64 mem)
        {
            if (_spaces2 == null)
            {
                mem = new MEMORY_BASIC_INFORMATION64();
                return -1;
            }

            SetClientInstance();
            return _spaces2.QueryVirtual(addr, out mem);
        }

        internal int GetModuleByModuleName(string image, int start, out uint index, out ulong baseAddress)
        {
            SetClientInstance();
            return _symbols.GetModuleByModuleName(image, (uint)start, out index, out baseAddress);
        }

        public void GetVersionInfo(ulong addr, out VersionInfo version)
        {
            version = new VersionInfo();

            uint index;
            ulong baseAddr;
            int hr = _symbols.GetModuleByOffset(addr, 0, out index, out baseAddr);
            if (hr != 0)
                return;

            uint needed = 0;
            hr = GetModuleVersionInformation(index, baseAddr, "\\", null, 0, out needed);
            if (hr != 0)
                return;

            byte[] buffer = new byte[needed];
            hr = GetModuleVersionInformation(index, baseAddr, "\\", buffer, needed, out needed);
            if (hr != 0)
                return;

            version.Minor = (ushort)Marshal.ReadInt16(buffer, 8);
            version.Major = (ushort)Marshal.ReadInt16(buffer, 10);
            version.Patch = (ushort)Marshal.ReadInt16(buffer, 12);
            version.Revision = (ushort)Marshal.ReadInt16(buffer, 14);

            return;
        }

        internal int GetModuleVersionInformation(uint index, ulong baseAddress, string p, byte[] buffer, uint needed1, out uint needed2)
        {
            if (_symbols3 == null)
            {
                needed2 = 0;
                return -1;
            }

            SetClientInstance();
            return _symbols3.GetModuleVersionInformation(index, baseAddress, "\\", buffer, needed1, out needed2);
        }

        internal int GetModuleNameString(DEBUG_MODNAME requestType, uint index, ulong baseAddress, StringBuilder sbpath, uint needed1, out uint needed2)
        {
            if (_symbols3 == null)
            {
                needed2 = 0;
                return -1;
            }

            SetClientInstance();
            return _symbols3.GetModuleNameString(requestType, index, baseAddress, sbpath, needed1, out needed2);
        }

        internal int GetModuleParameters(UInt32 Count, UInt64[] Bases, UInt32 Start, DEBUG_MODULE_PARAMETERS[] Params)
        {
            SetClientInstance();
            return _symbols.GetModuleParameters(Count, Bases, Start, Params);
        }

        internal void GetThreadIdBySystemId(uint threadID, out uint id)
        {
            SetClientInstance();
            _systemObjects.GetThreadIdBySystemId(threadID, out id);
        }

        internal void SetCurrentThreadId(uint id)
        {
            SetClientInstance();
            _systemObjects.SetCurrentThreadId(id);
        }

        internal void GetExecutingProcessorType(out IMAGE_FILE_MACHINE machineType)
        {
            SetClientInstance();
            _control.GetEffectiveProcessorType(out machineType);
        }

        public bool ReadMemory(ulong address, IntPtr buffer, int bytesRequested, out int bytesRead)
        {
            SetClientInstance();

            uint read;
            bool res = _spacesPtr.ReadVirtual(address, buffer, (uint)bytesRequested, out read) >= 0;
            bytesRead = res ? (int)read : 0;
            return res;
        }

        public int ReadVirtual(ulong address, byte[] buffer, uint bytesRequested, out uint bytesRead)
        {
            SetClientInstance();
            return _spaces.ReadVirtual(address, buffer, bytesRequested, out bytesRead);
        }

        public IEnumerable<uint> EnumerateAllThreads()
        {
            SetClientInstance();

            uint count = 0;
            int hr = _systemObjects.GetNumberThreads(out count);
            if (hr == 0)
            {
                uint[] sysIds = new uint[count];

                hr = _systemObjects.GetThreadIdsByIndex(0, count, null, sysIds);
                if (hr == 0)
                    return sysIds;
            }

            return new uint[0];
        }

        public ulong GetThreadTeb(uint thread)
        {
            SetClientInstance();

            ulong teb = 0;
            uint id = 0;
            int hr = _systemObjects.GetCurrentThreadId(out id);
            bool haveId = hr == 0;

            if (_systemObjects.GetThreadIdBySystemId(thread, out id) == 0 && _systemObjects.SetCurrentThreadId(id) == 0)
                _systemObjects.GetCurrentThreadTeb(out teb);

            if (haveId)
                _systemObjects.SetCurrentThreadId(id);

            return teb;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _disposed = true;

            int count = Interlocked.Decrement(ref s_totalInstanceCount);
            if (count == 0 && s_needRelease && disposing)
            {
                if (_systemObjects3 != null)
                    _systemObjects3.SetCurrentSystemId(_instance);

                _client.EndSession(DEBUG_END.ACTIVE_DETACH);
                _client.DetachProcesses();
            }

            // If there are no more debug instances, we can safely reset this variable
            // and start releasing newly created IDebug objects.
            if (count == 0)
                s_needRelease = true;
        }

        public unsafe bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, byte[] context)
        {
            uint id = 0;
            GetThreadIdBySystemId(threadID, out id);

            SetCurrentThreadId(id);

            fixed (byte* pContext = &context[0])
                GetThreadContext(new IntPtr(pContext), contextSize);

            return true;
        }
    }

    internal unsafe class LiveDataReader : IDataReader
    {
        #region Variables
        private IntPtr _process;
        private int _pid;
        #endregion

        private const int PROCESS_VM_READ = 0x10;
        private const int PROCESS_QUERY_INFORMATION = 0x0400;
        public LiveDataReader(int pid)
        {
            _pid = pid;
            _process = OpenProcess(PROCESS_VM_READ | PROCESS_QUERY_INFORMATION, false, pid);

            if (_process == IntPtr.Zero)
                throw new ClrDiagnosticsException(String.Format("Could not attach to process. Error {0}.", Marshal.GetLastWin32Error()));

            bool wow64, targetWow64;
            using (Process p = Process.GetCurrentProcess())
                if (NativeMethods.TryGetWow64(p.Handle, out wow64) &&
                    NativeMethods.TryGetWow64(_process, out targetWow64) &&
                    wow64 != targetWow64)
                {
                    throw new ClrDiagnosticsException("Dac architecture mismatch!");
                }
        }

        public bool IsMinidump
        {
            get
            {
                return false;
            }
        }

        public void Close()
        {
            if (_process != IntPtr.Zero)
            {
                CloseHandle(_process);
                _process = IntPtr.Zero;
            }
        }

        public void Flush()
        {
        }

        public Architecture GetArchitecture()
        {
            if (IntPtr.Size == 4)
                return Architecture.X86;

            return Architecture.Amd64;
        }

        public uint GetPointerSize()
        {
            return (uint)IntPtr.Size;
        }

        public IList<ModuleInfo> EnumerateModules()
        {
            List<ModuleInfo> result = new List<ModuleInfo>();

            uint needed;
            EnumProcessModules(_process, null, 0, out needed);

            IntPtr[] modules = new IntPtr[needed / 4];
            uint size = (uint)modules.Length * sizeof(uint);

            if (!EnumProcessModules(_process, modules, size, out needed))
                throw new ClrDiagnosticsException("Unable to get process modules.", ClrDiagnosticsException.HR.DataRequestError);

            for (int i = 0; i < modules.Length; i++)
            {
                IntPtr ptr = modules[i];

                if (ptr == IntPtr.Zero)
                {
                    break;
                }

                StringBuilder sb = new StringBuilder(1024);
                GetModuleFileNameExA(_process, ptr, sb, sb.Capacity);

                string filename = sb.ToString();
                ModuleInfo module = new ModuleInfo(this);

                module.ImageBase = (ulong)ptr.ToInt64();
                module.FileName = filename;

                uint filesize, timestamp;
                GetFileProperties(module.ImageBase, out filesize, out timestamp);

                module.FileSize = filesize;
                module.TimeStamp = timestamp;

                result.Add(module);
            }

            return result;
        }

        public void GetVersionInfo(ulong addr, out VersionInfo version)
        {
            StringBuilder filename = new StringBuilder(1024);
            GetModuleFileNameExA(_process, new IntPtr((long)addr), filename, filename.Capacity);

            int major, minor, revision, patch;
            if (NativeMethods.GetFileVersion(filename.ToString(), out major, out minor, out revision, out patch))
                version = new VersionInfo(major, minor, revision, patch);
            else
                version = new VersionInfo();
        }

        public bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead)
        {
            try
            {
                int res = ReadProcessMemory(_process, new IntPtr((long)address), buffer, bytesRequested, out bytesRead);
                return res != 0;
            }
            catch
            {
                bytesRead = 0;
                return false;
            }
        }

        public bool ReadMemory(ulong address, IntPtr buffer, int bytesRequested, out int bytesRead)
        {
            try
            {
                int res = RawPinvokes.ReadProcessMemory(_process, new IntPtr((long)address), buffer, bytesRequested, out bytesRead);
                return res != 0;
            }
            catch
            {
                bytesRead = 0;
                return false;
            }
        }


        private byte[] _ptrBuffer = new byte[IntPtr.Size];
        public ulong ReadPointerUnsafe(ulong addr)
        {
            int read;
            if (!ReadMemory(addr, _ptrBuffer, IntPtr.Size, out read))
                return 0;

            fixed (byte* r = _ptrBuffer)
            {
                if (IntPtr.Size == 4)
                    return *(((uint*)r));

                return *(((ulong*)r));
            }
        }


        public uint ReadDwordUnsafe(ulong addr)
        {
            int read;
            if (!ReadMemory(addr, _ptrBuffer, 4, out read))
                return 0;

            fixed (byte* r = _ptrBuffer)
                return *(((uint*)r));
        }


        public ulong GetThreadTeb(uint thread)
        {
            // todo
            throw new NotImplementedException();
        }

        public IEnumerable<uint> EnumerateAllThreads()
        {
            Process p = Process.GetProcessById(_pid);
            foreach (ProcessThread thread in p.Threads)
                yield return (uint)thread.Id;
        }

        public bool VirtualQuery(ulong addr, out VirtualQueryData vq)
        {
            vq = new VirtualQueryData();

            MEMORY_BASIC_INFORMATION mem = new MEMORY_BASIC_INFORMATION();
            IntPtr ptr = new IntPtr((long)addr);

            int res = VirtualQueryEx(_process, ptr, ref mem, new IntPtr(Marshal.SizeOf(mem)));
            if (res == 0)
                return false;

            vq.BaseAddress = mem.BaseAddress;
            vq.Size = mem.Size;
            return true;
        }

        public bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, IntPtr context)
        {
            using (SafeWin32Handle thread = OpenThread(ThreadAccess.THREAD_ALL_ACCESS, true, threadID))
            {
                if (thread.IsInvalid)
                    return false;

                bool res = GetThreadContext(thread.DangerousGetHandle(), context);
                return res;
            }
        }

        public unsafe bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, byte[] context)
        {
            using (SafeWin32Handle thread = OpenThread(ThreadAccess.THREAD_ALL_ACCESS, true, threadID))
            {
                if (thread.IsInvalid)
                    return false;

                fixed (byte* b = context)
                {
                    bool res = GetThreadContext(thread.DangerousGetHandle(), new IntPtr(b));
                    return res;
                }
            }
        }


        private void GetFileProperties(ulong moduleBase, out uint filesize, out uint timestamp)
        {
            filesize = 0;
            timestamp = 0;
            byte[] buffer = new byte[4];

            int read;
            if (ReadMemory(moduleBase + 0x3c, buffer, buffer.Length, out read) && read == buffer.Length)
            {
                uint sigOffset = (uint)BitConverter.ToInt32(buffer, 0);
                int sigLength = 4;

                if (ReadMemory(moduleBase + (ulong)sigOffset, buffer, buffer.Length, out read) && read == buffer.Length)
                {
                    uint header = (uint)BitConverter.ToInt32(buffer, 0);

                    // Ensure the module contains the magic "PE" value at the offset it says it does.  This check should
                    // never fail unless we have the wrong base address for CLR.
                    Debug.Assert(header == 0x4550);
                    if (header == 0x4550)
                    {
                        const int timeDataOffset = 4;
                        const int imageSizeOffset = 0x4c;
                        if (ReadMemory(moduleBase + (ulong)sigOffset + (ulong)sigLength + (ulong)timeDataOffset, buffer, buffer.Length, out read) && read == buffer.Length)
                            timestamp = (uint)BitConverter.ToInt32(buffer, 0);

                        if (ReadMemory(moduleBase + (ulong)sigOffset + (ulong)sigLength + (ulong)imageSizeOffset, buffer, buffer.Length, out read) && read == buffer.Length)
                            filesize = (uint)BitConverter.ToInt32(buffer, 0);
                    }
                }
            }
        }

        #region PInvoke Structs
        [StructLayout(LayoutKind.Sequential)]
        internal struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr Address;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;

            public ulong BaseAddress
            {
                get { return (ulong)Address; }
            }

            public ulong Size
            {
                get { return (ulong)RegionSize; }
            }
        }
        #endregion

        #region PInvokes
        [DllImportAttribute("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("psapi.dll", SetLastError = true)]
        public static extern bool EnumProcessModules(IntPtr hProcess, [Out] IntPtr[] lphModule, uint cb, [MarshalAs(UnmanagedType.U4)] out uint lpcbNeeded);

        [DllImport("psapi.dll", SetLastError = true)]
        [PreserveSig]
        public static extern uint GetModuleFileNameExA([In]IntPtr hProcess, [In]IntPtr hModule, [Out]StringBuilder lpFilename, [In][MarshalAs(UnmanagedType.U4)]int nSize);

        [DllImport("kernel32.dll")]
        private static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, ref MEMORY_BASIC_INFORMATION lpBuffer, IntPtr dwLength);

        [DllImport("kernel32.dll")]
        private static extern bool GetThreadContext(IntPtr hThread, IntPtr lpContext);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern SafeWin32Handle OpenThread(ThreadAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwThreadId);
        #endregion
        
        private enum ThreadAccess : int
        {
            THREAD_ALL_ACCESS = (0x1F03FF),
        }
    }

    internal class RawPinvokes
    {
        [DllImport("kernel32.dll")]
        internal static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int dwSize, out int lpNumberOfBytesRead);
    }
}
