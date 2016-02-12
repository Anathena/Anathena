using System;
using Anathema.MemoryManagement.Memory;

namespace Anathema.MemoryManagement.Native
{
    /// <summary>
    /// Class representing the Process Environment Block of a remote process.
    /// </summary>
    public class ManagedPeb : RemotePointer
    {
        #region Properties
        public Boolean Success;
        public Byte InheritedAddressSpace
        {
            get { return Read<Byte>(PebStructure.InheritedAddressSpace, out Success); }
            set { Write(PebStructure.InheritedAddressSpace, value); }
        }
        public Byte ReadImageFileExecOptions
        {
            get { return Read<Byte>(PebStructure.ReadImageFileExecOptions, out Success); }
            set { Write(PebStructure.ReadImageFileExecOptions, value); }
        }
        public Boolean BeingDebugged
        {
            get { return Read<Boolean>(PebStructure.BeingDebugged, out Success); }
            set { Write(PebStructure.BeingDebugged, value); }
        }
        public Byte SpareBool
        {
            get { return Read<Byte>(PebStructure.SpareBool, out Success); }
            set { Write(PebStructure.SpareBool, value); }
        }
        public IntPtr Mutant
        {
            get { return Read<IntPtr>(PebStructure.Mutant, out Success); }
            set { Write(PebStructure.Mutant, value); }
        }
        public IntPtr Ldr
        {
            get { return Read<IntPtr>(PebStructure.Ldr, out Success); }
            set { Write(PebStructure.Ldr, value); }
        }
        public IntPtr ProcessParameters
        {
            get { return Read<IntPtr>(PebStructure.ProcessParameters, out Success); }
            set { Write(PebStructure.ProcessParameters, value); }
        }
        public IntPtr SubSystemData
        {
            get { return Read<IntPtr>(PebStructure.SubSystemData, out Success); }
            set { Write(PebStructure.SubSystemData, value); }
        }
        public IntPtr ProcessHeap
        {
            get { return Read<IntPtr>(PebStructure.ProcessHeap, out Success); }
            set { Write(PebStructure.ProcessHeap, value); }
        }
        public IntPtr FastPebLock
        {
            get { return Read<IntPtr>(PebStructure.FastPebLock, out Success); }
            set { Write(PebStructure.FastPebLock, value); }
        }
        public IntPtr FastPebLockRoutine
        {
            get { return Read<IntPtr>(PebStructure.FastPebLockRoutine, out Success); }
            set { Write(PebStructure.FastPebLockRoutine, value); }
        }
        public IntPtr FastPebUnlockRoutine
        {
            get { return Read<IntPtr>(PebStructure.FastPebUnlockRoutine, out Success); }
            set { Write(PebStructure.FastPebUnlockRoutine, value); }
        }
        public IntPtr EnvironmentUpdateCount
        {
            get { return Read<IntPtr>(PebStructure.EnvironmentUpdateCount, out Success); }
            set { Write(PebStructure.EnvironmentUpdateCount, value); }
        }
        public IntPtr KernelCallbackTable
        {
            get { return Read<IntPtr>(PebStructure.KernelCallbackTable, out Success); }
            set { Write(PebStructure.KernelCallbackTable, value); }
        }
        public Int32 SystemReserved
        {
            get { return Read<Int32>(PebStructure.SystemReserved, out Success); }
            set { Write(PebStructure.SystemReserved, value); }
        }
        public Int32 AtlThunkSListPtr32
        {
            get { return Read<Int32>(PebStructure.AtlThunkSListPtr32, out Success); }
            set { Write(PebStructure.AtlThunkSListPtr32, value); }
        }
        public IntPtr FreeList
        {
            get { return Read<IntPtr>(PebStructure.FreeList, out Success); }
            set { Write(PebStructure.FreeList, value); }
        }
        public IntPtr TlsExpansionCounter
        {
            get { return Read<IntPtr>(PebStructure.TlsExpansionCounter, out Success); }
            set { Write(PebStructure.TlsExpansionCounter, value); }
        }
        public IntPtr TlsBitmap
        {
            get { return Read<IntPtr>(PebStructure.TlsBitmap, out Success); }
            set { Write(PebStructure.TlsBitmap, value); }
        }
        public Int64 TlsBitmapBits
        {
            get { return Read<Int64>(PebStructure.TlsBitmapBits, out Success); }
            set { Write(PebStructure.TlsBitmapBits, value); }
        }
        public IntPtr ReadOnlySharedMemoryBase
        {
            get { return Read<IntPtr>(PebStructure.ReadOnlySharedMemoryBase, out Success); }
            set { Write(PebStructure.ReadOnlySharedMemoryBase, value); }
        }
        public IntPtr ReadOnlySharedMemoryHeap
        {
            get { return Read<IntPtr>(PebStructure.ReadOnlySharedMemoryHeap, out Success); }
            set { Write(PebStructure.ReadOnlySharedMemoryHeap, value); }
        }
        public IntPtr ReadOnlyStaticServerData
        {
            get { return Read<IntPtr>(PebStructure.ReadOnlyStaticServerData, out Success); }
            set { Write(PebStructure.ReadOnlyStaticServerData, value); }
        }
        public IntPtr AnsiCodePageData
        {
            get { return Read<IntPtr>(PebStructure.AnsiCodePageData, out Success); }
            set { Write(PebStructure.AnsiCodePageData, value); }
        }
        public IntPtr OemCodePageData
        {
            get { return Read<IntPtr>(PebStructure.OemCodePageData, out Success); }
            set { Write(PebStructure.OemCodePageData, value); }
        }
        public IntPtr UnicodeCaseTableData
        {
            get { return Read<IntPtr>(PebStructure.UnicodeCaseTableData, out Success); }
            set { Write(PebStructure.UnicodeCaseTableData, value); }
        }
        public Int32 NumberOfProcessors
        {
            get { return Read<Int32>(PebStructure.NumberOfProcessors, out Success); }
            set { Write(PebStructure.NumberOfProcessors, value); }
        }
        public Int64 NtGlobalFlag
        {
            get { return Read<Int64>(PebStructure.NtGlobalFlag, out Success); }
            set { Write(PebStructure.NtGlobalFlag, value); }
        }
        public Int64 CriticalSectionTimeout
        {
            get { return Read<Int64>(PebStructure.CriticalSectionTimeout, out Success); }
            set { Write(PebStructure.CriticalSectionTimeout, value); }
        }
        public IntPtr HeapSegmentReserve
        {
            get { return Read<IntPtr>(PebStructure.HeapSegmentReserve, out Success); }
            set { Write(PebStructure.HeapSegmentReserve, value); }
        }
        public IntPtr HeapSegmentCommit
        {
            get { return Read<IntPtr>(PebStructure.HeapSegmentCommit, out Success); }
            set { Write(PebStructure.HeapSegmentCommit, value); }
        }
        public IntPtr HeapDeCommitTotalFreeThreshold
        {
            get { return Read<IntPtr>(PebStructure.HeapDeCommitTotalFreeThreshold, out Success); }
            set { Write(PebStructure.HeapDeCommitTotalFreeThreshold, value); }
        }
        public IntPtr HeapDeCommitFreeBlockThreshold
        {
            get { return Read<IntPtr>(PebStructure.HeapDeCommitFreeBlockThreshold, out Success); }
            set { Write(PebStructure.HeapDeCommitFreeBlockThreshold, value); }
        }
        public Int32 NumberOfHeaps
        {
            get { return Read<Int32>(PebStructure.NumberOfHeaps, out Success); }
            set { Write(PebStructure.NumberOfHeaps, value); }
        }
        public Int32 MaximumNumberOfHeaps
        {
            get { return Read<Int32>(PebStructure.MaximumNumberOfHeaps, out Success); }
            set { Write(PebStructure.MaximumNumberOfHeaps, value); }
        }
        public IntPtr ProcessHeaps
        {
            get { return Read<IntPtr>(PebStructure.ProcessHeaps, out Success); }
            set { Write(PebStructure.ProcessHeaps, value); }
        }
        public IntPtr GdiSharedHandleTable
        {
            get { return Read<IntPtr>(PebStructure.GdiSharedHandleTable, out Success); }
            set { Write(PebStructure.GdiSharedHandleTable, value); }
        }
        public IntPtr ProcessStarterHelper
        {
            get { return Read<IntPtr>(PebStructure.ProcessStarterHelper, out Success); }
            set { Write(PebStructure.ProcessStarterHelper, value); }
        }
        public IntPtr GdiDcAttributeList
        {
            get { return Read<IntPtr>(PebStructure.GdiDcAttributeList, out Success); }
            set { Write(PebStructure.GdiDcAttributeList, value); }
        }
        public IntPtr LoaderLock
        {
            get { return Read<IntPtr>(PebStructure.LoaderLock, out Success); }
            set { Write(PebStructure.LoaderLock, value); }
        }
        public Int32 OsMajorVersion
        {
            get { return Read<Int32>(PebStructure.OsMajorVersion, out Success); }
            set { Write(PebStructure.OsMajorVersion, value); }
        }
        public Int32 OsMinorVersion
        {
            get { return Read<Int32>(PebStructure.OsMinorVersion, out Success); }
            set { Write(PebStructure.OsMinorVersion, value); }
        }
        public UInt16 OsBuildNumber
        {
            get { return Read<UInt16>(PebStructure.OsBuildNumber, out Success); }
            set { Write(PebStructure.OsBuildNumber, value); }
        }
        public UInt16 OsCsdVersion
        {
            get { return Read<UInt16>(PebStructure.OsCsdVersion, out Success); }
            set { Write(PebStructure.OsCsdVersion, value); }
        }
        public Int32 OsPlatformId
        {
            get { return Read<Int32>(PebStructure.OsPlatformId, out Success); }
            set { Write(PebStructure.OsPlatformId, value); }
        }
        public Int32 ImageSubsystem
        {
            get { return Read<Int32>(PebStructure.ImageSubsystem, out Success); }
            set { Write(PebStructure.ImageSubsystem, value); }
        }
        public Int32 ImageSubsystemMajorVersion
        {
            get { return Read<Int32>(PebStructure.ImageSubsystemMajorVersion, out Success); }
            set { Write(PebStructure.ImageSubsystemMajorVersion, value); }
        }
        public IntPtr ImageSubsystemMinorVersion
        {
            get { return Read<IntPtr>(PebStructure.ImageSubsystemMinorVersion, out Success); }
            set { Write(PebStructure.ImageSubsystemMinorVersion, value); }
        }
        public IntPtr ImageProcessAffinityMask
        {
            get { return Read<IntPtr>(PebStructure.ImageProcessAffinityMask, out Success); }
            set { Write(PebStructure.ImageProcessAffinityMask, value); }
        }
        public IntPtr[] GdiHandleBuffer
        {
            get { return Read<IntPtr>(PebStructure.GdiHandleBuffer, 0x22, out Success); }
            set { Write(PebStructure.GdiHandleBuffer, value); }
        }
        public IntPtr PostProcessInitRoutine
        {
            get { return Read<IntPtr>(PebStructure.PostProcessInitRoutine, out Success); }
            set { Write(PebStructure.PostProcessInitRoutine, value); }
        }
        public IntPtr TlsExpansionBitmap
        {
            get { return Read<IntPtr>(PebStructure.TlsExpansionBitmap, out Success); }
            set { Write(PebStructure.TlsExpansionBitmap, value); }
        }
        public IntPtr[] TlsExpansionBitmapBits
        {
            get { return Read<IntPtr>(PebStructure.TlsExpansionBitmapBits, 0x20, out Success); }
            set { Write(PebStructure.TlsExpansionBitmapBits, value); }
        }
        public IntPtr SessionId
        {
            get { return Read<IntPtr>(PebStructure.SessionId, out Success); }
            set { Write(PebStructure.SessionId, value); }
        }
        public Int64 AppCompatFlags
        {
            get { return Read<Int64>(PebStructure.AppCompatFlags, out Success); }
            set { Write(PebStructure.AppCompatFlags, value); }
        }
        public Int64 AppCompatFlagsUser
        {
            get { return Read<Int64>(PebStructure.AppCompatFlagsUser, out Success); }
            set { Write(PebStructure.AppCompatFlagsUser, value); }
        }
        public IntPtr ShimData
        {
            get { return Read<IntPtr>(PebStructure.ShimData, out Success); }
            set { Write(PebStructure.ShimData, value); }
        }
        public IntPtr AppCompatInfo
        {
            get { return Read<IntPtr>(PebStructure.AppCompatInfo, out Success); }
            set { Write(PebStructure.AppCompatInfo, value); }
        }
        public Int64 CsdVersion
        {
            get { return Read<Int64>(PebStructure.CsdVersion, out Success); }
            set { Write(PebStructure.CsdVersion, value); }
        }
        public IntPtr ActivationContextData
        {
            get { return Read<IntPtr>(PebStructure.ActivationContextData, out Success); }
            set { Write(PebStructure.ActivationContextData, value); }
        }
        public IntPtr ProcessAssemblyStorageMap
        {
            get { return Read<IntPtr>(PebStructure.ProcessAssemblyStorageMap, out Success); }
            set { Write(PebStructure.ProcessAssemblyStorageMap, value); }
        }
        public IntPtr SystemDefaultActivationContextData
        {
            get { return Read<IntPtr>(PebStructure.SystemDefaultActivationContextData, out Success); }
            set { Write(PebStructure.SystemDefaultActivationContextData, value); }
        }
        public IntPtr SystemAssemblyStorageMap
        {
            get { return Read<IntPtr>(PebStructure.SystemAssemblyStorageMap, out Success); }
            set { Write(PebStructure.SystemAssemblyStorageMap, value); }
        }
        public IntPtr MinimumStackCommit
        {
            get { return Read<IntPtr>(PebStructure.MinimumStackCommit, out Success); }
            set { Write(PebStructure.MinimumStackCommit, value); }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedPeb"/> class.
        /// </summary>
        /// <param name="MemorySharp">The reference of the <see cref="MemoryEditor"/> object.</param>
        /// <param name="Address">The location of the peb.</param>
        internal ManagedPeb(MemoryEditor MemorySharp, IntPtr Address) : base(MemorySharp, Address)
        {

        }

        #endregion

        #region Methods
        /// <summary>
        /// Finds the Process Environment Block address of a specified process.
        /// </summary>
        /// <param name="ProcessHandle">A handle of the process.</param>
        /// <returns>A <see cref="IntPtr"/> pointer of the PEB.</returns>
        public static IntPtr FindPeb(SafeMemoryHandle ProcessHandle)
        {
            return MemoryCore.NtQueryInformationProcess(ProcessHandle).PebBaseAddress;
        }

        #endregion

    } // End class

} // End namespace