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
    /// TODO: Grow regions by 7 bytes (max for any data type - 1) after re-merging
    /// 
    /// </summary>
    class SearchSpaceAnalyzer : MemoryReader
    {
        // Search space reduction related
        private List<MemoryChangeRoot> CandidateTree;       // List of memory pages that we may be interested in
        private List<MemoryRegion> CurrentScanResults;      // List of memory regions after scanning
        private CancellationTokenSource CancelRequest;      // Tells the scan task to cancel (ie finish)
        private Task ChangeScanner;                         // Event that constantly checks the target process for changes
        private const Int32 BitArraySize = 64;              // Size of the number of change bits to keep before allocating more
        private const Int32 WaitTime = 500;                 // Time to wait (in ms) for a cancel request between each scan

        private InputCorrelator InputCorrelator;
        private FiniteStateScanner FiniteStateScanner;

        // Scan statistics
        private UInt64 InitialSize = 0;
        private UInt64 EndSize = 0;

        private AnalysisModeEnum AnalysisMode;


        public enum AnalysisModeEnum
        {
            SearchSpaceReduction,
            InputCorrelator,
        }

        public SearchSpaceAnalyzer()
        {
            CandidateTree = new List<MemoryChangeRoot>();
            InputCorrelator = new InputCorrelator();
            FiniteStateScanner = new FiniteStateScanner();
        }

        public List<MemoryChangeRoot> GetHistory()
        {
            return CandidateTree;
        }

        public void Reset()
        {
            CandidateTree.Clear();
        }

        public void Begin(AnalysisModeEnum AnalysisMode, UInt32 PageSplitThreshold = UInt32.MaxValue)
        {
            this.AnalysisMode = AnalysisMode;

            switch (AnalysisMode)
            {
                case AnalysisModeEnum.SearchSpaceReduction:

                    // Update the theshold for splitting pages during hash scans
                    MemoryChangeTree.PageSplitThreshold = PageSplitThreshold;

                    // Get the active pages from the target
                    List<MemoryRegion> Regions = GetPages().ConvertAll(Page => (MemoryRegion)Page);

                    for (int Index = 0; Index < Regions.Count; Index++)
                        CandidateTree.Add(new MemoryChangeRoot(Regions[Index]));

                    // Start recording changes in the process memory
                    BeginScan();
                    break;

                case AnalysisModeEnum.InputCorrelator:
                    InputCorrelator.Begin(CurrentScanResults);
                    break;
            }
        }

        private void BeginScan()
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
            Parallel.For(0, CandidateTree.Count, PageIndex => // Upwards of a x2 increase in speed
            {
                Boolean Success = false;
                Byte[] PageData = ReadArrayOfBytes((IntPtr)CandidateTree[PageIndex].BaseAddress,
                    (UInt32)CandidateTree[PageIndex].RegionSize, out Success);

                // Process the changes that have occurred since the last sampling for this memory page
                if (Success)
                {
                    CandidateTree[PageIndex].ProcessChanges(PageData);
                }
                // Error reading this page -- delete it (may have been deallocated)
                else
                {
                    CandidateTree[PageIndex].Dead = true;
                }
            });

            // Remove dead (deallocated) pages
            for (int Index = 0; Index < CandidateTree.Count; Index++)
            {
                if (CandidateTree[Index].Dead)
                    CandidateTree.RemoveAt(Index--);
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
            }
            catch (AggregateException) { }


            switch (AnalysisMode)
            {
                case AnalysisModeEnum.SearchSpaceReduction:

                    // Collect the pages that have changed
                    List<MemoryChangeRoot> AcceptedPages = new List<MemoryChangeRoot>();
                    for (int Index = 0; Index < CandidateTree.Count; Index++)
                        CandidateTree[Index].GetPageList(AcceptedPages);

                    InitialSize = GetSize(CandidateTree);

                    Single DebugTest = GetSize(AcceptedPages);

                    CandidateTree = null;

                    // Merge and collect any adjacent regions from the accepted list of memory pages
                    CurrentScanResults = CombineRegions(AcceptedPages);

                    EndSize = GetSize(CurrentScanResults);
                    break;

                case AnalysisModeEnum.InputCorrelator:
                    InputCorrelator.End();
                    break;
            }

        }

        // Merging regions the naïve way is O(n^2) and can take upwards of 15 seconds. A faster approach is a stack based algorithm (<20 ms)
        private List<MemoryRegion> CombineRegions(List<MemoryChangeRoot> AcceptedPages)
        {
            // Collect memory pages from the filtered results
            List<MemoryRegion> Regions = AcceptedPages.ConvertAll(Page => (MemoryRegion)Page);

            if (Regions.Count == 0)
                return Regions;

            // First, sort by start address
            Regions.OrderBy(x => x.BaseAddress);

            // Prepare the stack
            Stack<MemoryRegion> MergedRegions = new Stack<MemoryRegion>();
            MergedRegions.Push(Regions[0]);

            // Build the regions
            for (int Index = 1; Index < Regions.Count; Index++)
            {
                MemoryRegion Top = MergedRegions.Peek();

                // If the interval does not overlap, put it on the top of the stack
                if (Top.EndAddress < Regions[Index].BaseAddress)
                {
                    MergedRegions.Push(Regions[Index]);
                }
                // The interval overlaps; just merge it with the current top of the stack
                else if (Top.EndAddress < Regions[Index].EndAddress)
                {
                    Top.RegionSize = Regions[Index].EndAddress - Top.BaseAddress;
                    MergedRegions.Pop();
                    MergedRegions.Push(Top);
                }
            }

            return MergedRegions.ToList();
        }

        public override void SetTargetProcess(Process TargetProcess)
        {
            // Update target process for all analysis components
            base.SetTargetProcess(TargetProcess);
            InputCorrelator.SetTargetProcess(TargetProcess);
            FiniteStateScanner.SetTargetProcess(TargetProcess);
        }

        private UInt64 GetSize(List<MemoryChangeRoot> Regions)
        {
            UInt64 Value = 0;
            for (int Index = 0; Index < Regions.Count; Index++)
                Value += Regions[Index].RegionSize;
            return Value;
        }

        private UInt64 GetSize(List<MemoryRegion> Regions)
        {
            UInt64 Value = 0;
            for (int Index = 0; Index < Regions.Count; Index++)
                Value += Regions[Index].RegionSize;
            return Value;
        }

        public class MemoryChangeRoot : MemoryRegion
        {
            public MemoryChangeTree Child = null;
            public Boolean Dead = false;
            public Boolean HasChanged = false;

            public MemoryChangeRoot(MemoryRegion MemoryRegion) : base(MemoryRegion.BaseAddress, MemoryRegion.RegionSize)
            {
                Child = new MemoryChangeTree();
            }

            public MemoryChangeRoot CopyRange(UInt64 Start, UInt64 Length)
            {
                MemoryRegion NewPage = base.Clone();

                NewPage.BaseAddress += Start;  // Start is an offset from 0, so we can simply add it
                NewPage.RegionSize = Length;   // The length must be assigned

                return new MemoryChangeRoot(NewPage);
            }

            public void GetPageList(List<MemoryChangeRoot> AcceptedPages)
            {
                Child.GetPageList(this, AcceptedPages, 0, RegionSize);
            }

            public void ProcessChanges(Byte[] Data)
            {
                HasChanged = Child.ProcessChanges(Data, 0, (UInt64)Data.Length);
            }
        }

        public class MemoryChangeTree
        {
            public static UInt64 PageSplitThreshold;    // User specified minimum page size for dynamic pages

            public UInt64[] Hashes;         // Can look into UInt32 if memory is a large concern for these trees
            public Int32 QueryCount = 0;    // Number of times changes have been queried

            public MemoryChangeTree ChildLeft = null;
            public MemoryChangeTree ChildRight = null;

            public Boolean HasChanged = false;
            public Boolean StateUnknown = true;

            public MemoryChangeTree()
            {
                Hashes = new UInt64[2];
            }

            public void GetPageList(MemoryChangeRoot Root, List<MemoryChangeRoot> AcceptedPages, UInt64 Start, UInt64 Length)
            {
                // Add this page to the accepted list if we are a leaf on the tree structure
                if (ChildLeft == null && ChildRight == null)
                {
                    if (HasChanged || StateUnknown)
                    {
                        AcceptedPages.Add(Root.CopyRange(Start, Length));
                    }
                }
                // This node has children; propagate the request (in equal halves) downwards
                else
                {
                    ChildLeft.GetPageList(Root, AcceptedPages, Start, Length / 2);
                    ChildRight.GetPageList(Root, AcceptedPages, Start + Length / 2, Length - Length / 2);
                }
            }

            public Boolean ProcessChanges(Byte[] Data, UInt64 Start, UInt64 Length)
            {
                // No need to process a page that has already changed
                if (Length <= PageSplitThreshold && HasChanged)
                    return HasChanged;

                // If this node has no children, this node is a leaf and thus does the processing
                if (ChildLeft == null && ChildRight == null)
                {
                    // Calculate changes for this page
                    Hashes[QueryCount % 2] = FastHash.ComputeHash(Data, Start, Start + Length);

                    // Update the history, given that 2 checksums have been collected
                    if (QueryCount > 0)
                    {
                        HasChanged = Hashes[0] != Hashes[1];
                        StateUnknown = false;
                    }

                    // This page needs to be split if a change was detected and the size is above the threshold
                    if (HasChanged && Length > PageSplitThreshold)
                    {
                        ChildLeft = new MemoryChangeTree();
                        ChildRight = new MemoryChangeTree();

                        // We no longer need the history for this page since we split it (and thus can no longer use the history)
                        Hashes = null;
                    }

                    QueryCount++;
                }

                // Pass the data down to the children if they exist
                if (ChildLeft != null && ChildRight != null)
                {
                    return (
                        ChildLeft.ProcessChanges(Data, Start, Length / 2) &
                        ChildRight.ProcessChanges(Data, Start + Length / 2, Length - Length / 2)
                    );
                }

                return HasChanged;
            }

        } // End class

    } // End class

} // End namespace