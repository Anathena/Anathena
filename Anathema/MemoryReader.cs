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

        public Byte[] ReadArrayOfBytes(IntPtr MemoryAddress, uint BytesToRead)
        {
            byte[] ByteBuffer = new byte[BytesToRead];
            IntPtr PtrBytesRead;
            ReadProcessMemory(TargetProcess.Handle, MemoryAddress, ByteBuffer, BytesToRead, out PtrBytesRead);
            return ByteBuffer;
        }

        public List<VirtualPageData> GetPages()
        {
            const Int32 MaxIterations = 5000; // TODO Make this configurable and find a good default

            List<VirtualPageData> PageDataList = new List<VirtualPageData>();

            UIntPtr CurrentAddress = UIntPtr.Zero;
            UIntPtr PreviousAddress = UIntPtr.Zero;
            UIntPtr MaximumAddress = (UIntPtr)(UInt32.MaxValue / 2); // TODO make accurate reflection of max address instead of defaulting to 2GB
            DateTime StartTime = DateTime.Now;

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
                PageDataList.Add(new VirtualPageData(StartTime, MemoryInformation));
            }

            return PageDataList;
        }

        /// <summary>
        /// Structure defining the important details of a memory page
        /// </summary>
        public struct VirtualPageData
        {
            public DateTime TimeStamp;
            public UInt64 BaseAddress;
            public UInt64 RegionSize;
            public MEMORY_STATE State;
            public MEMORY_PROTECTION Protection;
            public MEMORY_TYPE LType;

            public VirtualPageData(DateTime TimeStamp, MEMORY_BASIC_INFORMATION64 MemoryInfo)
            {
                this.TimeStamp = TimeStamp;
                this.BaseAddress = MemoryInfo.BaseAddress;
                this.RegionSize = MemoryInfo.RegionSize;
                this.State = (MEMORY_STATE)MemoryInfo.State;
                this.Protection = (MEMORY_PROTECTION)MemoryInfo.AllocationProtect;
                this.LType = (MEMORY_TYPE)MemoryInfo.lType;
            }
        }

    }
}
