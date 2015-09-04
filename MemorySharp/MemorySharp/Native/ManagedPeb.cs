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

namespace Binarysharp.MemoryManagement.Native
{
    /// <summary>
    /// Class representing the Process Environment Block of a remote process.
    /// </summary>
    public class ManagedPeb : RemotePointer
    {
        #region Properties
        public bool success;
        public byte InheritedAddressSpace
        {
            get { return Read<byte>(PebStructure.InheritedAddressSpace, out success); }
            set { Write(PebStructure.InheritedAddressSpace, value); }
        }
        public byte ReadImageFileExecOptions
        {
            get { return Read<byte>(PebStructure.ReadImageFileExecOptions, out success); }
            set { Write(PebStructure.ReadImageFileExecOptions, value); }
        }
        public bool BeingDebugged
        {
            get { return Read<bool>(PebStructure.BeingDebugged, out success); }
            set { Write(PebStructure.BeingDebugged, value); }
        }
        public byte SpareBool
        {
            get { return Read<byte>(PebStructure.SpareBool, out success); }
            set { Write(PebStructure.SpareBool, value); }
        }
        public IntPtr Mutant
        {
            get { return Read<IntPtr>(PebStructure.Mutant, out success); }
            set { Write(PebStructure.Mutant, value); }
        }
        public IntPtr Ldr
        {
            get { return Read<IntPtr>(PebStructure.Ldr, out success); }
            set { Write(PebStructure.Ldr, value); }
        }
        public IntPtr ProcessParameters
        {
            get { return Read<IntPtr>(PebStructure.ProcessParameters, out success); }
            set { Write(PebStructure.ProcessParameters, value); }
        }
        public IntPtr SubSystemData
        {
            get { return Read<IntPtr>(PebStructure.SubSystemData, out success); }
            set { Write(PebStructure.SubSystemData, value); }
        }
        public IntPtr ProcessHeap
        {
            get { return Read<IntPtr>(PebStructure.ProcessHeap, out success); }
            set { Write(PebStructure.ProcessHeap, value); }
        }
        public IntPtr FastPebLock
        {
            get { return Read<IntPtr>(PebStructure.FastPebLock, out success); }
            set { Write(PebStructure.FastPebLock, value); }
        }
        public IntPtr FastPebLockRoutine
        {
            get { return Read<IntPtr>(PebStructure.FastPebLockRoutine, out success); }
            set { Write(PebStructure.FastPebLockRoutine, value); }
        }
        public IntPtr FastPebUnlockRoutine
        {
            get { return Read<IntPtr>(PebStructure.FastPebUnlockRoutine, out success); }
            set { Write(PebStructure.FastPebUnlockRoutine, value); }
        }
        public IntPtr EnvironmentUpdateCount
        {
            get { return Read<IntPtr>(PebStructure.EnvironmentUpdateCount, out success); }
            set { Write(PebStructure.EnvironmentUpdateCount, value); }
        }
        public IntPtr KernelCallbackTable
        {
            get { return Read<IntPtr>(PebStructure.KernelCallbackTable, out success); }
            set { Write(PebStructure.KernelCallbackTable, value); }
        }
        public int SystemReserved
        {
            get { return Read<int>(PebStructure.SystemReserved, out success); }
            set { Write(PebStructure.SystemReserved, value); }
        }
        public int AtlThunkSListPtr32
        {
            get { return Read<int>(PebStructure.AtlThunkSListPtr32, out success); }
            set { Write(PebStructure.AtlThunkSListPtr32, value); }
        }
        public IntPtr FreeList
        {
            get { return Read<IntPtr>(PebStructure.FreeList, out success); }
            set { Write(PebStructure.FreeList, value); }
        }
        public IntPtr TlsExpansionCounter
        {
            get { return Read<IntPtr>(PebStructure.TlsExpansionCounter, out success); }
            set { Write(PebStructure.TlsExpansionCounter, value); }
        }
        public IntPtr TlsBitmap
        {
            get { return Read<IntPtr>(PebStructure.TlsBitmap, out success); }
            set { Write(PebStructure.TlsBitmap, value); }
        }
        public long TlsBitmapBits
        {
            get { return Read<long>(PebStructure.TlsBitmapBits, out success); }
            set { Write(PebStructure.TlsBitmapBits, value); }
        }
        public IntPtr ReadOnlySharedMemoryBase
        {
            get { return Read<IntPtr>(PebStructure.ReadOnlySharedMemoryBase, out success); }
            set { Write(PebStructure.ReadOnlySharedMemoryBase, value); }
        }
        public IntPtr ReadOnlySharedMemoryHeap
        {
            get { return Read<IntPtr>(PebStructure.ReadOnlySharedMemoryHeap, out success); }
            set { Write(PebStructure.ReadOnlySharedMemoryHeap, value); }
        }
        public IntPtr ReadOnlyStaticServerData
        {
            get { return Read<IntPtr>(PebStructure.ReadOnlyStaticServerData, out success); }
            set { Write(PebStructure.ReadOnlyStaticServerData, value); }
        }
        public IntPtr AnsiCodePageData
        {
            get { return Read<IntPtr>(PebStructure.AnsiCodePageData, out success); }
            set { Write(PebStructure.AnsiCodePageData, value); }
        }
        public IntPtr OemCodePageData
        {
            get { return Read<IntPtr>(PebStructure.OemCodePageData, out success); }
            set { Write(PebStructure.OemCodePageData, value); }
        }
        public IntPtr UnicodeCaseTableData
        {
            get { return Read<IntPtr>(PebStructure.UnicodeCaseTableData, out success); }
            set { Write(PebStructure.UnicodeCaseTableData, value); }
        }
        public int NumberOfProcessors
        {
            get { return Read<int>(PebStructure.NumberOfProcessors, out success); }
            set { Write(PebStructure.NumberOfProcessors, value); }
        }
        public long NtGlobalFlag
        {
            get { return Read<long>(PebStructure.NtGlobalFlag, out success); }
            set { Write(PebStructure.NtGlobalFlag, value); }
        }
        public long CriticalSectionTimeout
        {
            get { return Read<long>(PebStructure.CriticalSectionTimeout, out success); }
            set { Write(PebStructure.CriticalSectionTimeout, value); }
        }
        public IntPtr HeapSegmentReserve
        {
            get { return Read<IntPtr>(PebStructure.HeapSegmentReserve, out success); }
            set { Write(PebStructure.HeapSegmentReserve, value); }
        }
        public IntPtr HeapSegmentCommit
        {
            get { return Read<IntPtr>(PebStructure.HeapSegmentCommit, out success); }
            set { Write(PebStructure.HeapSegmentCommit, value); }
        }
        public IntPtr HeapDeCommitTotalFreeThreshold
        {
            get { return Read<IntPtr>(PebStructure.HeapDeCommitTotalFreeThreshold, out success); }
            set { Write(PebStructure.HeapDeCommitTotalFreeThreshold, value); }
        }
        public IntPtr HeapDeCommitFreeBlockThreshold
        {
            get { return Read<IntPtr>(PebStructure.HeapDeCommitFreeBlockThreshold, out success); }
            set { Write(PebStructure.HeapDeCommitFreeBlockThreshold, value); }
        }
        public int NumberOfHeaps
        {
            get { return Read<int>(PebStructure.NumberOfHeaps, out success); }
            set { Write(PebStructure.NumberOfHeaps, value); }
        }
        public int MaximumNumberOfHeaps
        {
            get { return Read<int>(PebStructure.MaximumNumberOfHeaps, out success); }
            set { Write(PebStructure.MaximumNumberOfHeaps, value); }
        }
        public IntPtr ProcessHeaps
        {
            get { return Read<IntPtr>(PebStructure.ProcessHeaps, out success); }
            set { Write(PebStructure.ProcessHeaps, value); }
        }
        public IntPtr GdiSharedHandleTable
        {
            get { return Read<IntPtr>(PebStructure.GdiSharedHandleTable, out success); }
            set { Write(PebStructure.GdiSharedHandleTable, value); }
        }
        public IntPtr ProcessStarterHelper
        {
            get { return Read<IntPtr>(PebStructure.ProcessStarterHelper, out success); }
            set { Write(PebStructure.ProcessStarterHelper, value); }
        }
        public IntPtr GdiDcAttributeList
        {
            get { return Read<IntPtr>(PebStructure.GdiDcAttributeList, out success); }
            set { Write(PebStructure.GdiDcAttributeList, value); }
        }
        public IntPtr LoaderLock
        {
            get { return Read<IntPtr>(PebStructure.LoaderLock, out success); }
            set { Write(PebStructure.LoaderLock, value); }
        }
        public int OsMajorVersion
        {
            get { return Read<int>(PebStructure.OsMajorVersion, out success); }
            set { Write(PebStructure.OsMajorVersion, value); }
        }
        public int OsMinorVersion
        {
            get { return Read<int>(PebStructure.OsMinorVersion, out success); }
            set { Write(PebStructure.OsMinorVersion, value); }
        }
        public ushort OsBuildNumber
        {
            get { return Read<ushort>(PebStructure.OsBuildNumber, out success); }
            set { Write(PebStructure.OsBuildNumber, value); }
        }
        public ushort OsCsdVersion
        {
            get { return Read<ushort>(PebStructure.OsCsdVersion, out success); }
            set { Write(PebStructure.OsCsdVersion, value); }
        }
        public int OsPlatformId
        {
            get { return Read<int>(PebStructure.OsPlatformId, out success); }
            set { Write(PebStructure.OsPlatformId, value); }
        }
        public int ImageSubsystem
        {
            get { return Read<int>(PebStructure.ImageSubsystem, out success); }
            set { Write(PebStructure.ImageSubsystem, value); }
        }
        public int ImageSubsystemMajorVersion
        {
            get { return Read<int>(PebStructure.ImageSubsystemMajorVersion, out success); }
            set { Write(PebStructure.ImageSubsystemMajorVersion, value); }
        }
        public IntPtr ImageSubsystemMinorVersion
        {
            get { return Read<IntPtr>(PebStructure.ImageSubsystemMinorVersion, out success); }
            set { Write(PebStructure.ImageSubsystemMinorVersion, value); }
        }
        public IntPtr ImageProcessAffinityMask
        {
            get { return Read<IntPtr>(PebStructure.ImageProcessAffinityMask, out success); }
            set { Write(PebStructure.ImageProcessAffinityMask, value); }
        }
        public IntPtr[] GdiHandleBuffer
        {
            get { return Read<IntPtr>(PebStructure.GdiHandleBuffer, 0x22, out success); }
            set { Write(PebStructure.GdiHandleBuffer, value); }
        }
        public IntPtr PostProcessInitRoutine
        {
            get { return Read<IntPtr>(PebStructure.PostProcessInitRoutine, out success); }
            set { Write(PebStructure.PostProcessInitRoutine, value); }
        }
        public IntPtr TlsExpansionBitmap
        {
            get { return Read<IntPtr>(PebStructure.TlsExpansionBitmap, out success); }
            set { Write(PebStructure.TlsExpansionBitmap, value); }
        }
        public IntPtr[] TlsExpansionBitmapBits
        {
            get { return Read<IntPtr>(PebStructure.TlsExpansionBitmapBits, 0x20, out success); }
            set { Write(PebStructure.TlsExpansionBitmapBits, value); }
        }
        public IntPtr SessionId
        {
            get { return Read<IntPtr>(PebStructure.SessionId, out success); }
            set { Write(PebStructure.SessionId, value); }
        }
        public long AppCompatFlags
        {
            get { return Read<long>(PebStructure.AppCompatFlags, out success); }
            set { Write(PebStructure.AppCompatFlags, value); }
        }
        public long AppCompatFlagsUser
        {
            get { return Read<long>(PebStructure.AppCompatFlagsUser, out success); }
            set { Write(PebStructure.AppCompatFlagsUser, value); }
        }
        public IntPtr ShimData
        {
            get { return Read<IntPtr>(PebStructure.ShimData, out success); }
            set { Write(PebStructure.ShimData, value); }
        }
        public IntPtr AppCompatInfo
        {
            get { return Read<IntPtr>(PebStructure.AppCompatInfo, out success); }
            set { Write(PebStructure.AppCompatInfo, value); }
        }
        public long CsdVersion
        {
            get { return Read<long>(PebStructure.CsdVersion, out success); }
            set { Write(PebStructure.CsdVersion, value); }
        }
        public IntPtr ActivationContextData
        {
            get { return Read<IntPtr>(PebStructure.ActivationContextData, out success); }
            set { Write(PebStructure.ActivationContextData, value); }
        }
        public IntPtr ProcessAssemblyStorageMap
        {
            get { return Read<IntPtr>(PebStructure.ProcessAssemblyStorageMap, out success); }
            set { Write(PebStructure.ProcessAssemblyStorageMap, value); }
        }
        public IntPtr SystemDefaultActivationContextData
        {
            get { return Read<IntPtr>(PebStructure.SystemDefaultActivationContextData, out success); }
            set { Write(PebStructure.SystemDefaultActivationContextData, value); }
        }
        public IntPtr SystemAssemblyStorageMap
        {
            get { return Read<IntPtr>(PebStructure.SystemAssemblyStorageMap, out success); }
            set { Write(PebStructure.SystemAssemblyStorageMap, value); }
        }
        public IntPtr MinimumStackCommit
        {
            get { return Read<IntPtr>(PebStructure.MinimumStackCommit, out success); }
            set { Write(PebStructure.MinimumStackCommit, value); }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedPeb"/> class.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        /// <param name="address">The location of the peb.</param>
        internal ManagedPeb(MemorySharp memorySharp, IntPtr address) : base(memorySharp, address)
        {}
        #endregion

        #region Methods
        /// <summary>
        /// Finds the Process Environment Block address of a specified process.
        /// </summary>
        /// <param name="processHandle">A handle of the process.</param>
        /// <returns>A <see cref="IntPtr"/> pointer of the PEB.</returns>
        public static IntPtr FindPeb(SafeMemoryHandle processHandle)
        {
            return MemoryCore.NtQueryInformationProcess(processHandle).PebBaseAddress;
        }
        #endregion
    }
}
