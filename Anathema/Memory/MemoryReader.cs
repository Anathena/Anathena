using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class MemoryReader : OSInterface
    {
        public MemoryReader() : base()
        {

        }

        public Byte[] ReadArrayOfBytes(IntPtr MemoryAddress, UInt32 BytesToRead, out Boolean Success)
        {
            byte[] ByteBuffer = new byte[BytesToRead];
            IntPtr PtrBytesRead;
            ReadProcessMemory(TargetProcess.Handle, MemoryAddress, ByteBuffer, BytesToRead, out PtrBytesRead);

            if (PtrBytesRead == IntPtr.Zero)
                Success = false;
            else
                Success = true;

            return ByteBuffer;
        }

        public List<VirtualPageData> GetPages()
        {
            const Int32 MaxIterations = 9000; // TODO Make this configurable and find a good default

            List<VirtualPageData> MemoryPageList = new List<VirtualPageData>();

            UIntPtr CurrentAddress = UIntPtr.Zero;
            UIntPtr PreviousAddress = UIntPtr.Zero;
            UIntPtr MaximumAddress = (UIntPtr)(UInt32.MaxValue / 2); // TODO make accurate reflection of max address instead of defaulting to 2GB

            // Query pages with VirtualQueryEx until we hit a proper stopping condition, or we have too many iterations
            for (int Count = 0; Count < MaxIterations; Count++)
            {
                // Gather the memory page information with virtualqieruEX
                MEMORY_BASIC_INFORMATION64 MemoryInformation = new MEMORY_BASIC_INFORMATION64();
                QueryMemoryPages(Conversions.UIntPtrToIntPtr(CurrentAddress), out MemoryInformation, (uint)Marshal.SizeOf(MemoryInformation));

                PreviousAddress = CurrentAddress;
                CurrentAddress = (UIntPtr)(MemoryInformation.BaseAddress + MemoryInformation.RegionSize);

                // Exit when finished
                if ((UInt64)PreviousAddress >= (UInt64)CurrentAddress || (UInt64)CurrentAddress >= (UInt64)MaximumAddress)
                    break;

                // Check page state settings (Enforcing commit only is standard)
                if (!(MemoryInformation.State * Settings.StateSettings[0] == (uint)MEMORY_STATE.COMMIT
                    || MemoryInformation.State * Settings.StateSettings[1] == (uint)MEMORY_STATE.RESERVE
                    || MemoryInformation.State * Settings.StateSettings[2] == (uint)MEMORY_STATE.FREE
                    || MemoryInformation.State * Settings.StateSettings[3] == (uint)MEMORY_STATE.RESET_UNDO))
                {
                    continue;
                }

                // Check page type settings
                if (!(MemoryInformation.lType * Settings.TypeSettings[0] == (uint)MEMORY_TYPE.PRIVATE
                    || MemoryInformation.lType * Settings.TypeSettings[1] == (uint)MEMORY_TYPE.MAPPED
                    || MemoryInformation.lType * Settings.TypeSettings[2] == (uint)MEMORY_TYPE.IMAGE))
                {
                    continue;
                }

                // Check page protection settings
                if (!(MemoryInformation.Protect * Settings.ProtectionSettings[0] == (uint)MEMORY_PROTECTION.NO_ACCESS
                    || MemoryInformation.Protect * Settings.ProtectionSettings[1] == (uint)MEMORY_PROTECTION.READ_ONLY
                    || MemoryInformation.Protect * Settings.ProtectionSettings[2] == (uint)MEMORY_PROTECTION.READ_WRITE
                    || MemoryInformation.Protect * Settings.ProtectionSettings[3] == (uint)MEMORY_PROTECTION.WRITE_COPY
                    || MemoryInformation.Protect * Settings.ProtectionSettings[4] == (uint)MEMORY_PROTECTION.EXECUTE
                    || MemoryInformation.Protect * Settings.ProtectionSettings[5] == (uint)MEMORY_PROTECTION.EXECUTE_READ
                    || MemoryInformation.Protect * Settings.ProtectionSettings[6] == (uint)MEMORY_PROTECTION.EXECUTE_READ_WRITE
                    || MemoryInformation.Protect * Settings.ProtectionSettings[7] == (uint)MEMORY_PROTECTION.EXECUTE_WRITE_COPY
                    || MemoryInformation.Protect * Settings.ProtectionSettings[8] == (uint)MEMORY_PROTECTION.GUARD
                    || MemoryInformation.Protect * Settings.ProtectionSettings[9] == (uint)MEMORY_PROTECTION.NO_CACHE
                    || MemoryInformation.Protect * Settings.ProtectionSettings[10] == (uint)MEMORY_PROTECTION.WRITE_COMBINE))
                {
                    continue;
                }

                // This page passed all of the setting constraints, add it to the list
                MemoryPageList.Add(new VirtualPageData(MemoryInformation));
            }

            return MemoryPageList;
        }

        /// <summary>
        /// Defines a mapping of addresses to values. This could be scan results, correlation coefficients, etc
        /// </summary>
        public class MemoryRegionMapping<MappedType> : MemoryRegion
        {
            public const Byte AllBytesMapped = 0;   // Indicates all bytes in the region should be mapped to a single value
            public const Byte DefaultStride = 0;    // Indicates the stride should be equal to the BytesPerMap value

            public MappedType[] MappedValues;

            public Byte BytesPerMap;
            public Byte Stride;

            public MemoryRegionMapping(UInt64 BaseAddress, UInt64 RegionSize,
                Byte BytesPerMap = AllBytesMapped, Byte Stride = DefaultStride) : base(BaseAddress, RegionSize)
            {
                this.BytesPerMap = BytesPerMap;
                this.Stride = Stride;
            }

            public MemoryRegionMapping(MemoryRegion MemoryRegion,
                Byte BytesPerMap = AllBytesMapped, Byte Stride = DefaultStride) : base(MemoryRegion)
            {
                this.BytesPerMap = BytesPerMap;
                this.Stride = Stride;
            }

            public void SetMappedValues(MappedType[] MappedValues)
            {
                this.MappedValues = MappedValues;
            }
        }

        /// <summary>
        /// Defines the start and end of a region of memory, potentially ignoring page boundaries
        /// </summary>
        public class MemoryRegion
        {
            public UInt64 BaseAddress;
            public UInt64 RegionSize;

            public MemoryRegion(UInt64 BaseAddress, UInt64 RegionSize)
            {
                this.BaseAddress = BaseAddress;
                this.RegionSize = RegionSize;
            }

            public MemoryRegion(MemoryRegion MemoryRegion)
            {
                this.BaseAddress = MemoryRegion.BaseAddress;
                this.RegionSize = MemoryRegion.RegionSize;
            }

            public UInt64 EndAddress
            {
                get
                {
                    return BaseAddress + RegionSize;
                }
            }

            // Break a region up into a list of addresses
            public List<UInt64> Shatter(Int32 Stride = 1, UInt64 Alignment = 0)
            {
                List<UInt64> NewList = new List<UInt64>();

                for (Int32 Index = 0; Index < (Int32)RegionSize; Index += Stride)
                {
                    if (!(Alignment == 0 || (BaseAddress + (UInt64)Index) % Alignment == 0))
                        continue;

                    NewList.Add((BaseAddress + (UInt64)Index));
                }

                return NewList;
            }

            public MemoryRegion Clone()
            {
                return new MemoryRegion(BaseAddress, RegionSize);
            }
        }

        /// <summary>
        /// Class defining the important details of a memory page
        /// </summary>
        public class VirtualPageData : MemoryRegion
        {
            public MEMORY_STATE State;
            public MEMORY_PROTECTION Protection;
            public MEMORY_TYPE LType;

            public VirtualPageData(MEMORY_BASIC_INFORMATION64 MemoryInfo) : base(MemoryInfo.BaseAddress, MemoryInfo.RegionSize)
            {
                this.State = (MEMORY_STATE)MemoryInfo.State;
                this.Protection = (MEMORY_PROTECTION)MemoryInfo.AllocationProtect;
                this.LType = (MEMORY_TYPE)MemoryInfo.lType;
            }

            /*public VirtualPageData(UInt64 BaseAddress, UInt64 RegionSize, MEMORY_STATE State, MEMORY_PROTECTION Protection, MEMORY_TYPE LType) : base(BaseAddress, RegionSize)
            {
                this.State = State;
                this.Protection = Protection;
                this.LType = LType;
            }

            public VirtualPageData Clone()
            {
                return new VirtualPageData(State, Protection, LType);
            }*/
        }

    }
}
