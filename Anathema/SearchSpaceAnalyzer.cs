using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace Anathema
{
    /// <summary>
    /// Consantly queries the OS for all of the memory of the target program to determine if any of it has changed.
    /// This information is stored in an extremely compact way, as a series of bit arrays (0 = unchanged, 1 = changed)
    /// This information can then be used to drastically reduce the search space of a target process (95% is a reasonable amount)
    /// 
    /// Ideally this class will be used as follows
    /// 0) An instance of this class is made, and the Begin function is called with a specified page threshold.
    /// 1) Query all active memory pages running on the target process and save their metadata (page properties, address, size, etc)
    /// 
    /// On a timer:
    /// 2) Perform a MD5 hash on each memory page. A total of 2 will be kept for each page (for comparison purposes)
    /// 3) Once 2 have been collected, we can compare to determine if the pages have changed and store this in the history.
    /// 4) If a page has changed, we can then split that page into two (if it is larger than the given threshold), allowing us
    /// to determine which half is of interest with subsequent loop iterations
    /// Repeat for a decent amount of time. Preferably enough time to split the pages all the way down to the threshold size.
    /// 
    /// // TODO
    /// After initial reduction, determine which pages are changing in correspondence to the input(s) of interest.
    /// Allow the user to specify certain constraints (data types, allignment, float E format filter...)
    /// </summary>
    class SearchSpaceAnalyzer : MemoryReader
    {
        // TODO: dysync the currentCycle shit and keep that internal to each page.
        //  Also just add a fucking flag to determine if the page is safe to kill

        private List<MemoryChangeData> MemoryPages;
        public class MemoryChangeData
        {
            public VirtualPageData ActiveMemoryPages;
            public byte[][] CheckSums;
            public BitArray ChangeHistory;
            public bool StateUnknown;

            public MemoryChangeData(VirtualPageData ActiveMemoryPages, byte[][] CheckSums = null, BitArray ChangeHistory = null)
            {
                this.ActiveMemoryPages = ActiveMemoryPages;
                this.CheckSums = CheckSums;
                this.ChangeHistory = ChangeHistory;
                this.StateUnknown = false;
            }

            public void InitializeCheckSums()
            {
                CheckSums = new byte[2][];
            }

            public void InitializeChangeHistory()
            {
                ChangeHistory = new BitArray(BitArraySize);
            }

            // Called to create a copy of the memory page data before splitting it in half
            public MemoryChangeData Clone()
            {
                // Clone all of the important data (not including checksums)
                VirtualPageData NewActiveMemoryPages = ActiveMemoryPages.Clone();
                BitArray NewChangeHistory = new BitArray(ChangeHistory);

                return new MemoryChangeData(NewActiveMemoryPages, null, NewChangeHistory);
            }

            public void SetStateUnknown()
            {
                StateUnknown = true;
            }

            public void SetToLowerHalf()
            {
                // Lower half is easily split -- simply cut the region size in half
                ActiveMemoryPages.RegionSize /= 2;
            }

            public void SetToUpperHalf()
            {
                // Upper half needs a bit more work -- must shift the base address and subtract the lower half size
                ActiveMemoryPages.BaseAddress = ActiveMemoryPages.BaseAddress + ActiveMemoryPages.RegionSize / 2;
                ActiveMemoryPages.RegionSize -= ActiveMemoryPages.RegionSize / 2;
            }
        }

        public const Int32 BitArraySize = 64;           // Size of the number of change bits to keep before allocating more
        private const Int32 CachedPageHashCount = 2;    // Number of cached checksums for each page
        private const Int32 RoundRobinGroupSize = 4;    // TODO do I want this still?

        // Instead of saving an entire DateTime struture along with each bit, we can save the start and end times
        // and sample the target memory at a constant rate (ie 500ms), allowing us to determine the in-between times
        // TODO: Determine if we still want this method or what
        private DateTime StartTime;
        private DateTime EndTime;

        // Current reduction cycle (ie 0, 1, 2...)
        private Int32 CurrentCycle;
        private bool FinalCycle = false;
        private Timer UpdateTimer;
        private float TimerDelay = 250f;
        private UInt64 PageSplitThreshold;

        #region Public Methods
        public SearchSpaceAnalyzer()
        {
            MemoryPages = new List<MemoryChangeData>();
        }

        public List<MemoryChangeData> GetHistory()
        {
            return MemoryPages;
        }

        public void Reset()
        {
            MemoryPages.Clear();
        }

        public void Begin(UInt64 PageSplitThreshold = UInt64.MaxValue)
        {
            Initialize();

            this.PageSplitThreshold = PageSplitThreshold;

            // Get the active pages
            List<VirtualPageData> Pages = GetPages();
            for (int Index = 0; Index < Pages.Count; Index++)
                MemoryPages.Add(new MemoryChangeData(Pages[Index], null, null));

            // Create timer event
            UpdateTimer = new Timer(TimerDelay);
            UpdateTimer.Elapsed += new ElapsedEventHandler(Tick);
            UpdateTimer.Start();
        }

        #endregion

        // Resets and clears variables to prepare for a new run
        private void Initialize()
        {
            CurrentCycle = 0;
            FinalCycle = false;
        }

        private void Finalization()
        {
            UpdateTimer.Enabled = false;
            UpdateTimer.Stop();
            EndTime = DateTime.Now;

            // Calculate the index for the change history, offset by how many hashes we have cached
            int HistoryIndex = CurrentCycle - (CachedPageHashCount - 1);

            // Truncate the final bit array to the correct size
            for (int Index = 0; Index < MemoryPages.Count; Index++)
            {
                MemoryPages[Index].ChangeHistory.Length = HistoryIndex;
            }


            for (int PageIndex = 0; PageIndex < MemoryPages.Count; PageIndex++)
            {
                bool IsConstant = true;

                for (int ChangeIndex = 0; ChangeIndex < MemoryPages[PageIndex].ChangeHistory.Count; ChangeIndex++)
                {
                    if (MemoryPages[PageIndex].ChangeHistory[ChangeIndex] == true)
                        IsConstant = false;
                }

                // Remove the page if there have been no (known) memory changes
                if (!MemoryPages[PageIndex].StateUnknown && IsConstant)
                    MemoryPages.RemoveAt(PageIndex--);
            }

            FinalizationDebugging();
        }

        UInt64 TotalSize = 0;
        private void FinalizationDebugging()
        {
            TotalSize = 0;
            for (int Index = 0; Index < MemoryPages.Count; Index++)
            {
                TotalSize += MemoryPages[Index].ActiveMemoryPages.RegionSize;
            }

            TotalSize++;
        }

        // Determine which memory pages change
        private void QueryChanges()
        {
            // Get a list of current pages
            List<VirtualPageData> Pages = GetPages();

            if (CurrentCycle == 0)
                StartTime = DateTime.Now;

            // Calculate the index for the change history, offset by how many hashes we have cached
            int HistoryIndex = CurrentCycle - (CachedPageHashCount - 1);

            for (int Index = 0; Index < MemoryPages.Count; Index++)
            {
                //int PageIndex = Pages.FindIndex(Page => Page.BaseAddress == ActiveMemoryPages[Index].BaseAddress &&
                //    Page.RegionSize == ActiveMemoryPages[Index].RegionSize);

                // Determine which current page corresponds to the active page under consideration
                int PageIndex;
                for (PageIndex = 0; PageIndex < Pages.Count; PageIndex++)
                {
                    if (MemoryPages[Index].ActiveMemoryPages.BaseAddress >= Pages[PageIndex].BaseAddress &&
                        MemoryPages[Index].ActiveMemoryPages.BaseAddress + MemoryPages[Index].ActiveMemoryPages.RegionSize <=
                        Pages[PageIndex].BaseAddress + Pages[PageIndex].RegionSize)
                    {
                        break;
                    }
                }

                // Create the list of change history if not already created
                if (MemoryPages[Index].ChangeHistory == null)
                    MemoryPages[Index].InitializeChangeHistory();

                // Check if the page could not be found (deallocated)
                if (PageIndex < 0 || PageIndex >= MemoryPages.Count)
                {
                    // Delete this page from the active list
                    MemoryPages.RemoveAt(Index--);
                    continue;
                }

                // Allocate more space for history if we go over
                if (HistoryIndex >= MemoryPages[Index].ChangeHistory.Length)
                    MemoryPages[Index].ChangeHistory.Length = MemoryPages[Index].ChangeHistory.Length + BitArraySize;

                // Read the memory from the next page
                Byte[] PageData = ReadArrayOfBytes((IntPtr)MemoryPages[Index].ActiveMemoryPages.BaseAddress,
                    (UInt32)MemoryPages[Index].ActiveMemoryPages.RegionSize);

                // Create the list of checksums if not already created
                bool CheckSumInitialized = false;
                if (MemoryPages[Index].CheckSums == null)
                {
                    CheckSumInitialized = true;
                    MemoryPages[Index].InitializeCheckSums();
                }

                // Calculate the checksum for the page for this iteration (only two kept at any given time)
                MemoryPages[Index].CheckSums[CurrentCycle % 2] = CalculateCheckSum(PageData);

                // Do not calculate the change history for the first cycle, as we can't know for sure what memory has changed
                if (CheckSumInitialized)
                    continue;

                // Compare the checksum to this iteration and update the bit array accordingly
                MemoryPages[Index].ChangeHistory[HistoryIndex] =
                    CompareCheckSums(MemoryPages[Index].CheckSums[CurrentCycle % 2], MemoryPages[Index].CheckSums[(CurrentCycle + 1) % 2]);
            }
        }

        private void SplitPages()
        {
            // Do not waste processing time when the threshold is at its max (ie splitting disabled)
            if (PageSplitThreshold == UInt64.MaxValue)
                return;

            // Calculate the index for the change history, offset by how many hashes we have cached
            int HistoryIndex = CurrentCycle - (CachedPageHashCount - 1);

            if (HistoryIndex < 0)
                return;

            // List of new pages that come from splitting an old one
            List<MemoryChangeData> SplitPages = new List<MemoryChangeData>();

            for (int Index = 0; Index < MemoryPages.Count; Index++)
            {
                // Ignore pages below/equal to the threshold size
                if (MemoryPages[Index].ActiveMemoryPages.RegionSize <= PageSplitThreshold)
                    continue;

                // Ignore pages that are constant (ie no need to split them)
                if (MemoryPages[Index].ChangeHistory[HistoryIndex] == false)
                    continue;

                // Reset the flag indicating the page has changed (important!)
                MemoryPages[Index].ChangeHistory[HistoryIndex] = false;

                // Page has changed and is above the threshold, so we must split it into two pages
                MemoryChangeData SplitPage = MemoryPages[Index].Clone();

                // Adjust split page to be the upper half
                SplitPage.SetToUpperHalf();
                SplitPage.SetStateUnknown();
                SplitPages.Add(SplitPage);

                // Adjust this page to be the lower half
                MemoryPages[Index].SetToLowerHalf();
                MemoryPages[Index].SetStateUnknown();
            }

            // Add the new pages to the main list
            MemoryPages.AddRange(SplitPages);
        }

        private void Tick(object Sender, ElapsedEventArgs Event)
        {
            UpdateTimer.Stop();

            QueryChanges();
            SplitPages();

            if (FinalCycle)
            {
                Finalization();
                return;
            }

            CurrentCycle++;
            UpdateTimer.Start();
        }

        public void End()
        {
            // Set a flag indicating the update timer should only do one more final sample
            FinalCycle = true;
        }

        private static MD5 MD5Hash = MD5.Create();
        static byte[] CalculateCheckSum(byte[] MemoryValues)
        {
            if (MemoryValues == null || MemoryValues.Length <= 0)
                return null;

            return MD5Hash.ComputeHash(MemoryValues);
        }

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int memcmp(byte[] b1, byte[] b2, long count);
        static bool CompareCheckSums(byte[] ArrayA, byte[] ArrayB)
        {
            // Determine if the two byte arrays are identical 
            return !(ArrayA.Length == ArrayB.Length && memcmp(ArrayA, ArrayB, ArrayA.Length) == 0);
        }


    }
}
