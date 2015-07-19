using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

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
    /// REPEAT:
    /// 2) Perform a 64 bit hash on each memory page. A total of 2 will be kept for each page (for comparison purposes)
    /// 3) Once 2 have been collected, we can compare to determine if the pages have changed and store this in the history.
    /// 4) If a page has changed, we can then split that page into two (if it is larger than the given threshold), allowing us
    /// to determine which half is of interest with subsequent loop iterations
    /// END ON USER REQUEST. Preferably after enough time to split the pages all the way down to the threshold size.
    /// 
    /// </summary>
    class SearchSpaceAnalyzer : MemoryReader
    {
        // Search space reduction related
        private List<MemoryChangeLogger> MemoryPages;   // List of memory pages and their change history
        public const Int32 BitArraySize = 64;           // Size of the number of change bits to keep before allocating more
        private CancellationTokenSource CancelRequest;  // Tells the scan task to cancel (ie finish)
        private Task ChangeScanner;                     // Event that constantly checks the target process for changes
        private const Int32 WaitTime = 100;             // Time to wait (in ms) for a cancel request between each scan

        // Input correlation related
        private readonly IKeyboardMouseEvents InputHook;            // Input Capturing instance
        private Dictionary<Keys, Double> PhiCoefficientCorrelation; // Correlation of input and memory changes
        private Dictionary<Keys, List<DateTime>> KeyBoardDown;      // List of keyboard down events
        private Dictionary<Keys, List<DateTime>> KeyBoardUp;        // List of keyboard up events

        #region Public Methods
        public SearchSpaceAnalyzer()
        {
            MemoryPages = new List<MemoryChangeLogger>();

            KeyBoardUp = new Dictionary<Keys, List<DateTime>>();
            KeyBoardDown = new Dictionary<Keys, List<DateTime>>();

            // TODO: App hook option? From author: "Note: for the application hook, use the Hook.AppEvents() instead"
            InputHook = Hook.GlobalEvents();
        }

        public List<MemoryChangeLogger> GetHistory()
        {
            return MemoryPages;
        }

        public void Reset()
        {
            MemoryPages.Clear();
        }

        public void Begin(UInt32 PageSplitThreshold = UInt32.MaxValue)
        {
            MemoryChangeLogger.PageSplitThreshold = PageSplitThreshold;

            // Create input hook events
            //InputHook.MouseDownExt += GlobalHookMouseDownExt;
            //InputHook.KeyUp += GlobalHookKeyUp;
            //InputHook.KeyDown += GlobalHookKeyDown;

            // Get the active pages from the target
            List<VirtualPageData> Pages = GetPages();
            for (int Index = 0; Index < Pages.Count; Index++)
                MemoryPages.Add(new MemoryChangeLogger(Pages[Index]));

            // Start recording changes in the process memory
            BeginScan();
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
                /*
                // Test every recorded memory change (or lack thereof) against the input change logs
                for (int PageIndex = 0; PageIndex < MemoryPages.Count; PageIndex++)
                {
                    // https://en.wikipedia.org/wiki/Point-biserial_correlation_coefficient

                    Single Count0 = 0;
                    Single Count1 = 0;
                    Single M0 = 0;
                    Single M1 = 0;
                    Single Sn = 0;

                    Single ActivationRatio = 0;
                    Single Duration = 0;
                    Single InputActivatedDuration = 0;

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

                            // Add to the total time activated
                            InputActivatedDuration += (Single)(KeyUpHistory[InputIndex].Ticks - KeyDownHistory[InputIndex].Ticks);

                            // Compute the difference between the start and end to get the total time
                            if (InputIndex == KeyDownHistory.Count - 1)
                                Duration = (Single)(KeyUpHistory[InputIndex].Ticks - KeyDownHistory[0].Ticks);
                        }

                        if (InputActive)
                            M1++;
                        else
                            M0++;


                        if (PageChanged || InputActive)
                            Count1++;
                        else
                            Count0++;
                    }

                    M0 /= Count0;
                    M1 /= Count1;

                    ActivationRatio = InputActivatedDuration / Duration;
                    Sn = (Single)Math.Sqrt(ActivationRatio * (1.0f - ActivationRatio));

                    MemoryPages[PageIndex].PhiCoefficient = (M1 - M0) / Sn *
                        (Single)Math.Sqrt(Count0 * Count1 / (Count0 + Count1) * (Count0 + Count1));

                    //MemoryPages[PageIndex].PhiCoefficient = (Count11 * Count00 - Count10 * Count01) /
                    //    (Single)Math.Sqrt(Count0 * Count0 * Count1 * Count1);
                }*/
            }
        }
        #endregion

        public void BeginScan()
        {
            CancelRequest = new CancellationTokenSource();

            ChangeScanner = Task.Run(async () =>
            {
                while (true)
                {
                    // Query the target process for memory changes
                    QueryChanges();
                    await Task.Delay(WaitTime, CancelRequest.Token); // Await with cancellation
                }
            }, CancelRequest.Token);
        }
        
        private void QueryChanges()
        {
            Stopwatch StopWatch = new Stopwatch();
            StopWatch.Start();

            //for (int PageIndex = 0; PageIndex < MemoryPages.Count; PageIndex++)
            Parallel.For(0, MemoryPages.Count, PageIndex => // Upwards of a x16 increase in speed
            {
                Boolean Success = false;
                Byte[] PageData = ReadArrayOfBytes((IntPtr)MemoryPages[PageIndex].ActiveMemoryPage.BaseAddress,
                    (UInt32)MemoryPages[PageIndex].ActiveMemoryPage.RegionSize, out Success);

                // Process the changes that have occurred since the last sampling for this memory page
                if (Success)
                    MemoryPages[PageIndex].ProcessChanges(PageData);
                // Error reading this page -- delete it (may have been deallocated)
                else
                    MemoryPages[PageIndex].IsDead = true;
            });

            // Remove dead (deallocated) pages
            for (int Index = 0; Index < MemoryPages.Count; Index++)
            {
                if (MemoryPages[Index].IsDead)
                    MemoryPages.RemoveAt(Index--);
            }

            // Get the elapsed time as a TimeSpan value.
            StopWatch.Stop();
            Int32 ts = StopWatch.Elapsed.Milliseconds;
        }

        public void EndScan()
        {
            CancelRequest.Cancel();
            try
            {
                ChangeScanner.Wait();

                // Remove pages that have not changed at all through the entire scan
                for (int PageIndex = 0; PageIndex < MemoryPages.Count; PageIndex++)
                    MemoryPages[PageIndex].Finalization();
            }
            catch (AggregateException) { }
        }

        static UInt64 CalculateCheckSum(byte[] MemoryValues, int Start, int Length)
        {
            if (MemoryValues == null)
                return 0;

            return FastHash.ComputeHash(MemoryValues, Start, Start + Length);
        }

        public class MemoryChangeLogger
        {
            public VirtualPageData ActiveMemoryPage;    // Virtual memory page for which this class is tracking changes
            public List<DateTime> DateHistory;          // History of timestamps for queried memory
            public MemoryChangeTree Child;              // Root for data structure that processes changes

            public static UInt32 PageSplitThreshold;    // User specified minimum page size for dynamic pages

            public bool IsDead = false;

            public MemoryChangeLogger(VirtualPageData ActiveMemoryPage)
            {
                this.ActiveMemoryPage = ActiveMemoryPage;
                Child = new MemoryChangeTree();
                DateHistory = new List<DateTime>();
            }

            public void Finalization()
            {
                Child.TruncateChanges();
            }

            public List<Int32> GetValidPages()
            {
                List<Int32> Pages = new List<Int32>();
                Child.AddPages(Pages);
                return Pages;
            }

            public void ProcessChanges(Byte[] PageData)
            {
                // Add the time stamp
                DateHistory.Add(DateTime.Now);

                // Have the change tree root process the changes in the data
                Child.ProcessChanges(PageData, 0, PageData.Length - 1);
            }
        }

        public class MemoryChangeTree
        {
            public UInt64[] CheckSums;      // Can look into UInt32 if memory is a large concern for these trees
            public BitArray ChangeHistory;
            public Int32 ChangeCount = 0;

            public MemoryChangeTree ChildLeft = null;
            public MemoryChangeTree ChildRight = null;

            public bool LastChangeState = false;
            public bool StateUnknown = true;

            //public float PhiCoefficient; // stupid TODO: perhaps its not terrible for each tree to keep track of a Dict<> of this

            public MemoryChangeTree()
            {
                CheckSums = new UInt64[2];
                ChangeHistory = new BitArray(BitArraySize);
            }

            /// <summary>
            /// Extra space is allocated to keep track of changes in the form of a bit array (ie 10100xxxxxxxx)
            /// where x represents an allocated bit that was not used yet. If the user stops analysis prior to filling the array,
            /// the extra bits are chopped off.
            /// </summary>
            public void TruncateChanges()
            {
                // Propagate the call to children nodes
                if (ChildLeft != null)
                    ChildLeft.TruncateChanges();
                if (ChildRight != null)
                    ChildRight.TruncateChanges();

                // Truncate the history if we have history
                if (ChangeHistory != null)
                    ChangeHistory.Length = ChangeCount;
            }

            public void AddPages(List<Int32> Pages)
            {
                if (ChangeHistory != null)
                {
                    bool ContainsChange = true;

                    for (int ChangeIndex = 0; ChangeIndex < ChangeHistory.Count; ChangeIndex++)
                    {
                        if (ChangeHistory[ChangeIndex] == true)
                        {
                            ContainsChange = true;
                            break;
                        }
                    }

                    if (ContainsChange)
                    {

                    }

                    return;
                }

                if (ChildLeft != null)
                    ChildLeft.AddPages(Pages);
                if (ChildRight != null)
                    ChildRight.AddPages(Pages);
            }

            public void ProcessChanges(Byte[] Data, Int32 Start, Int32 Length)
            {
                // Process pages if they are small enough
                if (Length <= MemoryChangeLogger.PageSplitThreshold)
                {
                    // Calculate changes for this page
                    CheckSums[ChangeCount % 2] = CalculateCheckSum(Data, Start, Length);

                    // Expand our change history if we go over the size limit
                    if (ChangeCount >= ChangeHistory.Length)
                        ChangeHistory.Length = ChangeHistory.Length + BitArraySize;

                    // Update the history, given that 2 checksums have been collected
                    if (ChangeCount > 0)
                        ChangeHistory[ChangeCount++] = CheckSums[0] == CheckSums[1];

                    // Update state variables
                    StateUnknown = false;
                    ChangeCount++;
                    return;
                }

                // Create child nodes if they do not exist
                if (ChildLeft == null && ChildRight == null)
                {
                    ChildLeft = new MemoryChangeTree();
                    ChildRight = new MemoryChangeTree();

                    // We no longer need the history for this page since we split it (and thus can no longer use the history)
                    ChangeHistory = null;
                    CheckSums = null;
                }

                // For pages that are too large, pass them on to the children, splitting the page in half
                ChildLeft.ProcessChanges(Data, Start, Length / 2);
                ChildLeft.ProcessChanges(Data, Start + Length / 2, Length / 2);
            }

        } // End class

    } // End class

} // End namespace