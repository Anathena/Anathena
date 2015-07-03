using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Anathema
{
    /// <summary>
    /// Consantly queries the OS for all of the memory of the target program to determine if any of it has changed.
    /// This information is stored in an extremely compact way, as a series of bit arrays (0 = unchanged, 1 = changed)
    /// </summary>
    class SearchSpaceAnalyzer : MemoryReader
    {
        // List of the initial virtual pages for which we are determining memory changes
        private List<VirtualPageData> InitialMemoryPages = new List<VirtualPageData>();
        private List<UInt64[]> CheckSums = new List<UInt64[]>();
        private List<List<BitArray>> ChangeHistory = new List<List<BitArray>>();
        public const Int32 BitArraySize = 64;

        // Instead of saving an entire DateTime struture along with each bit, we can save the start and end times
        // and sample the target memory at a constant rate (ie 500ms), allowing us to determine the in-between times
        private DateTime StartTime;
        private DateTime EndTime;

        // Current reduction cycle (ie 0, 1, 2...)
        private Int32 CurrentCycle;

        private Timer UpdateTimer;

        private bool FinalCycle = false;


        public SearchSpaceAnalyzer()
        {
            UpdateTimer = new Timer(10000.0);
            UpdateTimer.Elapsed += new ElapsedEventHandler(Tick);
        }

        public List<List<BitArray>> GetHistory()
        {
            return ChangeHistory;
        }

        public void Begin()
        {

            // Get the initial pages
            InitialMemoryPages = GetPages();

            // Add the checksum history for each page (only keep record of the last two)
            for (int Index = 0; Index < InitialMemoryPages.Count; Index++)
            {
                CheckSums.Add(new UInt64[2]);
                ChangeHistory.Add(new List<BitArray>());
            }


            if (InitialMemoryPages.Count <= 0)
            {
                throw new Exception("No memory pages found");
            }

            // Initialize SSA parameters
            CurrentCycle = 0;
            StartTime = InitialMemoryPages[0].TimeStamp;
            UpdateTimer.Enabled = true;
        }

        private void Tick(object Sender, ElapsedEventArgs Event)
        {
            List<VirtualPageData> Pages = GetPages();

            for (int Index = 0; Index < InitialMemoryPages.Count; Index++)
            {
                int PageIndex = Pages.FindIndex(
                    Page => Page.BaseAddress == InitialMemoryPages[Index].BaseAddress &&
                    Page.RegionSize == InitialMemoryPages[Index].RegionSize);

                // Check if the page could not be found (deallocated)
                if (PageIndex < 0 || PageIndex >= InitialMemoryPages.Count)
                {
                    // Delete this page from the initial page list as well as its checksum data
                    //CheckSums.RemoveAt(InitialMemoryPages.IndexOf(InitialPage));
                    //InitialMemoryPages.Remove(InitialPage);
                    // TODO remove bit arrays

                    continue;
                }

                // Add to the bit array
                if (CurrentCycle % BitArraySize == 0)
                {
                    ChangeHistory[Index].Add(new BitArray(BitArraySize));
                }

                Byte[] Data = ReadArrayOfBytes((IntPtr)InitialMemoryPages[Index].BaseAddress, (UInt32)InitialMemoryPages[Index].RegionSize);

                // Calculate the checksum for this iteration
                CheckSums[Index][CurrentCycle % 2] = CalculateCheckSum(Data);

                // Compare the checksum to this iteration and update the bit array accordingly
                if (CheckSums[Index][CurrentCycle % 2] != CheckSums[Index][(CurrentCycle + 1) % 2])
                    ChangeHistory[Index][CurrentCycle / BitArraySize][CurrentCycle % BitArraySize] = true;
                else
                    ChangeHistory[Index][CurrentCycle / BitArraySize][CurrentCycle % BitArraySize] = false;
            }

            if (FinalCycle)
            {
                UpdateTimer.Enabled = false;
                UpdateTimer.Stop();
                EndTime = Pages[0].TimeStamp; // TODO can check to make sure page exists, if not use the last known one

                // Truncate the final bit array to the correct size
                int FinishSize = CurrentCycle % BitArraySize;
                for (int Index = 0; Index < InitialMemoryPages.Count; Index++)
                {
                    ChangeHistory[Index][CurrentCycle / BitArraySize].Length = CurrentCycle % BitArraySize;
                }

            }

            CurrentCycle++;

        }

        public void End()
        {
            // Set a flag indicating the update timer should only do one more final sample
            FinalCycle = true;
        }

        static unsafe UInt64 CalculateCheckSum(byte[] MemoryValues)
        {
            if (MemoryValues == null || MemoryValues.Length <= 0)
                return 0;

            unchecked
            {
                UInt64 CheckSum = 0;
                fixed (byte* ValuePointer = MemoryValues)
                {
                    // Add each 8-byte chunk at a time to the checksum
                    for (var Index = 0; Index < MemoryValues.Length / sizeof(UInt64); Index++)
                    {
                        var val = ((UInt64*)ValuePointer)[Index];
                        CheckSum += val;
                    }

                    Int32 RemainderIterations = MemoryValues.Length % sizeof(UInt64);

                    // Add remaining values 1 byte at a time
                    while (RemainderIterations >= 0)
                    {
                        CheckSum += ValuePointer[MemoryValues.Length - RemainderIterations];
                        RemainderIterations--;
                    }

                    return CheckSum;
                }
            }
        }


    }
}
