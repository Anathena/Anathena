using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

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
        private List<MemoryChangeData> MemoryPages;     // List of memory pages and their boolean change history

        // Thread related TODO
        //private List<Thread> Threads = new List<Thread>();
        //private const Int32 ThreadCount = 4;

        // Search space reduction related
        public const Int32 BitArraySize = 64;           // Size of the number of change bits to keep before allocating more
        private bool FinishedAnalyzing = false;         // Flag indicating the user wishes for the analysis to stop
        private System.Timers.Timer UpdateTimer;        // Timer to query for memory and process changes
        private float TimerDelay = 250f;                // Delay between update cycles
        private UInt64 PageSplitThreshold;              // User specified minimum page size for dynamic pages

        // Reduction Statistics
        private UInt64 QueriedMemorySize;               // Size of the initial memory query
        private UInt64 FilteredMemorySize;              // Size of the memory after search space reduction
        private Double ReductionPercentage;             // Percent reduction of search space

        // Input correlation related
        private readonly IKeyboardMouseEvents InputHook;            // Input Capturing instance
        private const Double DelayBuffer = 250.0;
        private Dictionary<Keys, Double> PhiCoefficientCorrelation; // Correlation of input and memory changes
        private Dictionary<Keys, List<DateTime>> KeyBoardDown;      // List of keyboard down events
        private Dictionary<Keys, List<DateTime>> KeyBoardUp;        // List of keyboard up events
        // private List<>                                           // List of mouse events


        #region Public Methods
        public SearchSpaceAnalyzer()
        {
            MemoryPages = new List<MemoryChangeData>();

            KeyBoardUp = new Dictionary<Keys, List<DateTime>>();
            KeyBoardDown = new Dictionary<Keys, List<DateTime>>();

            // TODO: App hook option? From author: "Note: for the application hook, use the Hook.AppEvents() instead"
            InputHook = Hook.GlobalEvents();
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
            FinishedAnalyzing = false;

            this.PageSplitThreshold = PageSplitThreshold;

            // Create input hook events
            InputHook.MouseDownExt += GlobalHookMouseDownExt;
            InputHook.KeyUp += GlobalHookKeyUp;
            InputHook.KeyDown += GlobalHookKeyDown;

            // Get the active pages from the target
            List<VirtualPageData> Pages = GetPages();
            for (int Index = 0; Index < Pages.Count; Index++)
                MemoryPages.Add(new MemoryChangeData(Pages[Index]));
            QueriedMemorySize = QueryMemorySize();

            // Create timer event
            UpdateTimer = new System.Timers.Timer(TimerDelay);
            UpdateTimer.Elapsed += new ElapsedEventHandler(Tick);
            UpdateTimer.Start();
        }

        #endregion

        #region Input recording

        private void GlobalHookKeyUp(object sender, KeyEventArgs e)
        {
            // Releasing a key that we have no record of ever pressing -- just ignore it
            if (!KeyBoardDown.ContainsKey(e.KeyCode))
                return;

            if (!KeyBoardUp.ContainsKey(e.KeyCode))
                KeyBoardUp.Add(e.KeyCode, new List<DateTime>());

            KeyBoardUp[e.KeyCode].Add(DateTime.Now);

            EnforceSameInputListSizes(e.KeyCode);
        }

        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            // Console.WriteLine("KeyPress: \t{0} \t{1}", e.KeyChar, DateTime.Now);
            if (!KeyBoardDown.ContainsKey(e.KeyCode))
                KeyBoardDown.Add(e.KeyCode, new List<DateTime>());

            KeyBoardDown[e.KeyCode].Add(DateTime.Now);
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        private void EnforceSameInputListSizes(Keys KeyCode, bool EnforceTime = false)
        {
            // Ensure that the sizes of up and down key records match (should not happen often, but it should be handled)
            if (KeyBoardUp[KeyCode].Count > KeyBoardDown[KeyCode].Count)
                KeyBoardUp[KeyCode].RemoveRange(KeyBoardDown[KeyCode].Count, KeyBoardUp[KeyCode].Count - KeyBoardDown[KeyCode].Count);

            if (KeyBoardDown[KeyCode].Count > KeyBoardUp[KeyCode].Count)
                KeyBoardDown[KeyCode].RemoveRange(KeyBoardUp[KeyCode].Count, KeyBoardDown[KeyCode].Count - KeyBoardUp[KeyCode].Count);

            if (!EnforceTime)
                return;

            // TODO: Ensure that the release time isnt > start time, etc
        }


        public void EndInputRecording()
        {
            InputHook.KeyUp -= GlobalHookKeyUp;
            InputHook.MouseDownExt -= GlobalHookMouseDownExt;
            InputHook.KeyDown -= GlobalHookKeyDown;

            //It is recommened to dispose it
            InputHook.Dispose();
        }

        private void MeasurePhiCoefficients()
        {
            // Ensure that the down and up lists are in sync
            foreach (KeyValuePair<Keys, List<DateTime>> NextItem in KeyBoardUp)
                EnforceSameInputListSizes(NextItem.Key, true);

            // Create a single list which combines the list of key presses (down) and releases (up)
            var KeyBoardDurations = KeyBoardDown.Zip(KeyBoardUp, (D, U) => new { Down = D, Up = U });
            
            foreach (var NextItem in KeyBoardDurations)
            {
                List<DateTime> KeyDownHistory = NextItem.Down.Value;
                List<DateTime> KeyUpHistory = NextItem.Up.Value;

                // Test every recorded memory change (or lack thereof) against the input change logs
                for (int PageIndex = 0; PageIndex < MemoryPages.Count; PageIndex++)
                {
                    Single Count00 = 0;
                    Single Count01 = 0;
                    Single Count10 = 0;
                    Single Count11 = 0;

                    Single Count1 = 0;
                    Single Count0 = 0;

                    for (int ChangeIndex = 0; ChangeIndex < MemoryPages[PageIndex].ChangeHistory.Count; ChangeIndex++)
                    {
                        bool PageChanged = MemoryPages[PageIndex].ChangeHistory[ChangeIndex];
                        bool InputActive = false;

                        for (int InputIndex = 0; InputIndex < KeyDownHistory.Count; InputIndex++)
                        {
                            // Search for the most temporally close page index that matches the input change
                            if (!(MemoryPages[PageIndex].DateHistory[ChangeIndex] > KeyDownHistory[InputIndex] &&
                                MemoryPages[PageIndex].DateHistory[ChangeIndex] < KeyUpHistory[InputIndex].AddMilliseconds(DelayBuffer)))
                                continue;

                            // We found a match indicating the input was active at the same time (or at least within DelayBuffer ms)
                            InputActive = true;
                            break;
                        }

                        if (PageChanged || InputActive)
                            Count1++;
                        else
                            Count0++;

                        // Use the change booleans to count the occurence of each binary relationship
                        if (!PageChanged && !InputActive)
                            Count00++;
                        else if (!PageChanged && InputActive)
                            Count01++;
                        else if (PageChanged && !InputActive)
                            Count10++;
                        else if (PageChanged && InputActive)
                            Count11++;
                    }

                    MemoryPages[PageIndex].PhiCoefficient = (Count11 * Count00 - Count10 * Count01) /
                        (Single)Math.Sqrt(Count0 * Count0 * Count1 * Count1);
                }
            }
        }
        #endregion

        private void Finalization()
        {
            // Disable the timer that queries for memory changes
            UpdateTimer.Enabled = false;
            UpdateTimer.Stop();

            // Truncate the final bit array to the correct size (future space is allocated, not all of it is used)
            for (int Index = 0; Index < MemoryPages.Count; Index++)
                MemoryPages[Index].TruncateHistory();

            // Remove pages that have not changed at all through the entire scan
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

            // Measure the input correlation
            MeasurePhiCoefficients();
            
            FilteredMemorySize = QueryMemorySize();
            if (QueriedMemorySize > 0)
                ReductionPercentage = 1.0 - (Double)FilteredMemorySize / (Double)QueriedMemorySize;
            else
                ReductionPercentage = 0;
        }

        private UInt64 QueryMemorySize()
        {
            UInt64 TotalSize = 0;
            for (int Index = 0; Index < MemoryPages.Count; Index++)
                TotalSize += MemoryPages[Index].ActiveMemoryPages.RegionSize;

            return TotalSize;
        }

        // Determine which memory pages change
        private void QueryChanges()
        {
            // Get a list of current pages
            List<VirtualPageData> Pages = GetPages();

            for (int Index = 0; Index < MemoryPages.Count; Index++)
            {
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

                // Check if the page could not be found (deallocated)
                if (PageIndex < 0 || PageIndex >= MemoryPages.Count)
                {
                    // Delete this page from the active list
                    MemoryPages.RemoveAt(Index--);
                    continue;
                }

                // Read the memory from the next page
                Byte[] PageData = ReadArrayOfBytes((IntPtr)MemoryPages[Index].ActiveMemoryPages.BaseAddress,
                    (UInt32)MemoryPages[Index].ActiveMemoryPages.RegionSize);

                // Calculate the checksum for the page for this iteration (only two kept at any given time)
                MemoryPages[Index].AddCheckSum(CalculateCheckSum(PageData));

                // Use the checksum data to determine if there were any changes in the memory page
                MemoryPages[Index].AddHistory();
            }
        }

        private void SplitPages()
        {
            // Do not waste processing time when the threshold is at its max (ie splitting disabled)
            if (PageSplitThreshold == UInt64.MaxValue)
                return;

            // List of new pages that come from splitting an old one
            List<MemoryChangeData> SplitPages = new List<MemoryChangeData>();

            for (int Index = 0; Index < MemoryPages.Count; Index++)
            {
                // Ignore pages below/equal to the threshold size
                if (MemoryPages[Index].ActiveMemoryPages.RegionSize <= PageSplitThreshold)
                    continue;

                // Ignore pages that are constant (ie no need to split them)
                if (!MemoryPages[Index].HasChanged())
                    continue;

                // Page has changed and is above the threshold, so we must split it into two pages
                MemoryChangeData SplitPage = MemoryPages[Index].Clone();

                // Adjust split page to be the upper half
                SplitPage.SetToUpperHalf();
                SplitPage.ClearState();
                SplitPages.Add(SplitPage);

                // Adjust this page to be the lower half
                MemoryPages[Index].SetToLowerHalf();
                MemoryPages[Index].ClearState();
            }

            // Add the new pages to the main list
            MemoryPages.AddRange(SplitPages);
        }

        private void Tick(object Sender, ElapsedEventArgs Event)
        {
            // Disable the timer while we do all of the heavy lifting
            UpdateTimer.Stop();

            QueryChanges();
            SplitPages();

            // If the user has specified that we are finished, then finalize and do not restart the timer
            if (FinishedAnalyzing)
            {
                Finalization();
                return;
            }

            // Enable the timer and repeat the process
            UpdateTimer.Start();
        }

        public void End()
        {
            // Set a flag indicating the update timer should only do one more final sample
            FinishedAnalyzing = true;
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

        public class MemoryChangeData
        {
            public VirtualPageData ActiveMemoryPages;
            public BitArray ChangeHistory;
            public List<DateTime> DateHistory;
            public byte[][] CheckSums;
            public float PhiCoefficient;

            public int CurrentDepth;

            public bool LogHistory;
            public bool LastChangeState = false;
            public bool StateUnknown;

            public MemoryChangeData(VirtualPageData ActiveMemoryPages, BitArray ChangeHistory = null, List<DateTime> DateHistory = null,
                byte[][] CheckSums = null, bool LogHistory = false, int CurrentDepth = 0, float PhiCoefficient = 0.0f)
            {
                this.ActiveMemoryPages = ActiveMemoryPages;
                this.ChangeHistory = ChangeHistory;
                this.DateHistory = DateHistory;
                this.CheckSums = CheckSums;

                this.LogHistory = LogHistory;
                this.CurrentDepth = CurrentDepth;
                this.StateUnknown = false;

                if (this.CheckSums == null)
                    this.CheckSums = new byte[2][];

                if (this.ChangeHistory == null)
                    this.ChangeHistory = new BitArray(BitArraySize);

                if (this.DateHistory == null)
                    this.DateHistory = new List<DateTime>();
            }

            public void AddCheckSum(byte[] CheckSum)
            {
                // Handle null case first
                if (CheckSums[CurrentDepth % 2] == null)
                {
                    CheckSums[CurrentDepth % 2] = CheckSum;
                }
                // The idea is to always be setting CurrentDepth % 2, so we are gonna have to switch things around
                else if (CheckSums[(CurrentDepth + 1) % 2] == null)
                {
                    CheckSums[(CurrentDepth + 1) % 2] = CheckSums[CurrentDepth % 2];
                    CheckSums[CurrentDepth % 2] = CheckSum;
                }
                // Standard case
                else
                {
                    CheckSums[CurrentDepth % 2] = CheckSum;
                }
            }

            public void AddHistory()
            {
                // Expand our change history if we go over the cap
                if (CurrentDepth >= ChangeHistory.Length)
                    ChangeHistory.Length = ChangeHistory.Length + BitArraySize;

                // Return if there is not enough information to determine changes yet
                if (CheckSums[0] == null || CheckSums[1] == null)
                    return;

                // Determine if there was a change in the checksums
                LastChangeState = CompareCheckSums(CheckSums[0], CheckSums[1]);

                // Add the current time stamp
                if (CurrentDepth < DateHistory.Count)
                    DateHistory[CurrentDepth] = DateTime.Now;
                else
                    DateHistory.Add(DateTime.Now);

                ChangeHistory[CurrentDepth++] = LastChangeState;
            }

            public bool HasChanged()
            {
                // Return if there is not enough information to determine changes yet
                if (CheckSums[0] == null || CheckSums[1] == null)
                    return false;

                return LastChangeState;
            }

            public void TruncateHistory()
            {
                // More space is generally allocated than is used in the end and must be removed
                ChangeHistory.Length = CurrentDepth - 1;
            }

            // Called to create a copy of the memory page data before splitting it in half
            public MemoryChangeData Clone()
            {
                // Clone all of the important data (not including checksums)
                VirtualPageData NewActiveMemoryPages = ActiveMemoryPages.Clone();
                BitArray NewChangeHistory = new BitArray(ChangeHistory);
                List<DateTime> NewDateHistory = new List<DateTime>(DateHistory);

                return new MemoryChangeData(NewActiveMemoryPages, NewChangeHistory,
                    NewDateHistory, null, LogHistory, CurrentDepth, PhiCoefficient);
            }

            public void ClearState()
            {
                // Erase the most recent history
                CurrentDepth--;
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
    }
}