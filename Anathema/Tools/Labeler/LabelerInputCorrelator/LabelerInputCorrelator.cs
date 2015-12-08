using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using Gma.System.MouseKeyHook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    class LabelerInputCorrelator : IMemoryLabeler
    {
        private MemorySharp MemoryEditor;
        private Benediction Anathema;

        // Scanning related
        private List<RemoteRegion> MemoryRegions;           // Regions we are scanning (isolated via SearchSpaceAnalyzer)
        private List<IntPtr> Addresses;
        private Dictionary<IntPtr, IntPtr> ScanIndicies;    // Maps literal addresses to indexes
        private List<DateTime> ScanHistoryTime;             // Time stamps for each of the scans
        private Byte[] ScanHistoryValues;                   // Values for each of the scans for each memory page
        private List<BitArray> ChangeHistory;
        private Int32 TotalAddressSize;

        private CancellationTokenSource CancelRequest;      // Tells the scan task to cancel (ie finish)
        private Task ChangeScanner;                         // Event that constantly checks the target process for changes
        private const Int32 WaitTime = 100;                 // Time to wait (in ms) for a cancel request between each scan

        // Input correlation related
        private readonly IKeyboardMouseEvents InputHook;    // Input capturing class
        private Dictionary<Keys, CorrelationValues[]> PhiCorrelationSustain;
        private Dictionary<Keys, CorrelationValues[]> PhiCorrelationDown;
        private Dictionary<Keys, CorrelationValues[]> PhiCorrelationUp;
        
        private Dictionary<Keys, DateTime> KeyBoardPending;         // List of keyboard down events (waiting for an up event)
        private Dictionary<Keys, List<DateTime>> KeyBoardDown;      // List of keyboard down events
        private Dictionary<Keys, List<DateTime>> KeyBoardUp;        // List of keyboard up events
        
        // http://www.ucl.ac.uk/english-usage/staff/sean/resources/phimeasures.pdf
        // https://en.wikipedia.org/wiki/Contingency_table#Measures_of_association
        public struct CorrelationValues
        {
            public Single WeightedImpact;
            public CorrelationValues(Single WeightedImpact)
            {
                this.WeightedImpact = WeightedImpact;
            }
            /*
            public Single WeightedImpact;
            public Single CramerPhi;
            public Single Phi;
            public Single AdjustedC;
            public Single ContingencyC;
            public Single CramerV;
            public Single Tetrachoric;
            public Single Fisher;

            public CorrelationValues(Single WeightedImpact, Single CramerPhi, Single Phi, Single AdjustedC, Single ContingencyC,
                Single CramerV, Single Tetrachoric, Single Fisher)
            {
                this.WeightedImpact = WeightedImpact;
                this.CramerPhi = CramerPhi;
                this.Phi = Phi;
                this.AdjustedC = AdjustedC;
                this.ContingencyC = ContingencyC;
                this.CramerV = CramerV;
                this.Tetrachoric = Tetrachoric;
                this.Fisher = Fisher;
            }*/
        }

        public enum SearchMethodEnum
        {
            Backwards,
            Forward,
            Nearest
        }

        public LabelerInputCorrelator()
        {
            InputHook = Hook.GlobalEvents();

            //Anathema = Benediction.GetBenedictionInstance();
        }

        public void BeginLabeler(MemorySharp MemoryEditor, List<RemoteRegion> MemoryRegions)
        {
            this.MemoryEditor = MemoryEditor;
            this.MemoryRegions = MemoryRegions;

            if (MemoryRegions == null)
                return;

            // Map each base address in the memory regions to a unique sequential index. Idk how to explain better than this.
            IntPtr MappedIndex = IntPtr.Zero;
            ScanIndicies = new Dictionary<IntPtr, IntPtr>();
            Addresses = new List<IntPtr>();
            for (Int32 RegionIndex = 0; RegionIndex < MemoryRegions.Count; RegionIndex++)
            {
                ScanIndicies.Add(MemoryRegions[RegionIndex].BaseAddress, MappedIndex);
                MappedIndex = (IntPtr)((UInt64)MappedIndex + (UInt64)MemoryRegions[RegionIndex].RegionSize);

                for (Int32 ElementIndex = 0; ElementIndex < MemoryRegions[RegionIndex].RegionSize; ElementIndex++)
                    Addresses.Add(MemoryRegions[RegionIndex].BaseAddress + ElementIndex);
            }

            IntPtr AddressCount = MappedIndex;

            if (AddressCount.Equals(IntPtr.Zero))
                return;

            // Initialize objects used in scanning
            ScanHistoryTime = new List<DateTime>();
            ChangeHistory = new List<BitArray>();
            ScanHistoryValues = new Byte[(UInt64)AddressCount];

            // Initialize input dictionaries
            KeyBoardPending = new Dictionary<Keys, DateTime>();
            KeyBoardUp = new Dictionary<Keys, List<DateTime>>();
            KeyBoardDown = new Dictionary<Keys, List<DateTime>>();

            PhiCorrelationUp = new Dictionary<Keys, CorrelationValues[]>();
            PhiCorrelationDown = new Dictionary<Keys, CorrelationValues[]>();
            PhiCorrelationSustain = new Dictionary<Keys, CorrelationValues[]>();

            // Create input hook events
            InputHook.MouseDownExt += GlobalHookMouseDownExt;
            InputHook.KeyUp += GlobalHookKeyUp;
            InputHook.KeyDown += GlobalHookKeyDown;

            // Begin scanning memory until the user cancels
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

        public void EndLabeler()
        {
            CancelRequest.Cancel();
            try
            {
                ChangeScanner.Wait();
            }
            catch (AggregateException) { }

            // Remove the first time step since there is one more time step than there are change logs
            if (ScanHistoryTime.Count >= 1)
                ScanHistoryTime.RemoveAt(0);

            // Cleanup for the input hook
            InputHook.KeyUp -= GlobalHookKeyUp;
            InputHook.MouseDownExt -= GlobalHookMouseDownExt;
            InputHook.KeyDown -= GlobalHookKeyDown;
            InputHook.Dispose();

            CollectCoefficients();
        }

        public void AbortLabeler()
        {

        }

        private void QueryChanges()
        {
            ChangeHistory.Add(new BitArray(ScanHistoryValues.Length));

            // Read memory from all processes
            //for (int RegionIndex = 0; RegionIndex < MemoryRegions.Count; RegionIndex++)
            Parallel.For(0, MemoryRegions.Count, RegionIndex =>
            {
                Boolean Success;
                IntPtr MappedIndex;

                // Read the memory from this page
                Byte[] RegionData = MemoryEditor.ReadBytes(MemoryRegions[RegionIndex].BaseAddress, MemoryRegions[RegionIndex].RegionSize, out Success, false);

                if (!ScanIndicies.TryGetValue(MemoryRegions[RegionIndex].BaseAddress, out MappedIndex))
                    return; // break; // Return is valid for parallel for loops

                if (ScanHistoryTime.Count > 0 && Success)
                {
                    // Calculate changes if memory successfully read (otherwise comparison array is all 0s)
                    for (Int32 CompareIndex = 0; CompareIndex < RegionData.Length; CompareIndex++)
                    {
                        ChangeHistory[ChangeHistory.Count - 1][(Int32)MappedIndex + CompareIndex] = RegionData[CompareIndex] != ScanHistoryValues[(Int32)MappedIndex + CompareIndex];
                    }
                }

                // Copy the read bytes in memory to the saved list
                Array.Copy(RegionData, 0, ScanHistoryValues, (Int32)MappedIndex, RegionData.Length);
            });

            // Get the scan time stamp
            ScanHistoryTime.Add(DateTime.Now);
        }

        private void RegisterKey(Keys Key)
        {
            if (!KeyBoardPending.ContainsKey(Key))
                KeyBoardPending.Add(Key, DateTime.MinValue);

            if (!KeyBoardDown.ContainsKey(Key))
                KeyBoardDown.Add(Key, new List<DateTime>());

            if (!KeyBoardUp.ContainsKey(Key))
                KeyBoardUp.Add(Key, new List<DateTime>());
        }

        private void GlobalHookKeyUp(object sender, KeyEventArgs e)
        {
            RegisterKey(e.KeyCode);

            // Check if there is a pending key that was pressed of the same type
            if (KeyBoardPending[e.KeyCode] != DateTime.MinValue)
            {
                // Add the key to the up and down lists
                KeyBoardDown[e.KeyCode].Add(KeyBoardPending[e.KeyCode]);
                KeyBoardUp[e.KeyCode].Add(DateTime.Now);

                // Clear the pending list
                KeyBoardPending[e.KeyCode] = DateTime.MinValue;
            }
        }

        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            RegisterKey(e.KeyCode);
            KeyBoardPending[e.KeyCode] = DateTime.Now;
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        private void CollectCoefficients()
        {
            TimeSpan MaxDifference = TimeSpan.FromMilliseconds(300);

            // Sustained input
            foreach (var NextItem in KeyBoardDown.Zip(KeyBoardUp, (D, U) => new { Down = D, Up = U }))
            {

            }

            // Pressed input
            foreach (KeyValuePair<Keys, List<DateTime>> NextInput in KeyBoardDown)
            {
                List<Tuple<IntPtr, Object>> Correlations;

                Correlations = CalculateCorrelations(NextInput.Value, MaxDifference);
                //Anathema.UpdateMemoryLabels(Correlations);
                //PhiCorrelationDown.Add(NextInput.Key, Correlations);
            }

            // Released input
            foreach (KeyValuePair<Keys, List<DateTime>> NextItem in KeyBoardDown)
            {

            }
        }

        private List<Tuple<IntPtr, Object>> CalculateCorrelations(List<DateTime> Input, TimeSpan MaxDifference,
            SearchMethodEnum SearchMethod = SearchMethodEnum.Backwards)
        {
            Object[] Correlations = new Object[ScanHistoryValues.Length];

            //if (ScanHistoryTime.Count != ChangeHistory[RegionIndex].Count)
            //    return Correlation;

            Parallel.For(0, Correlations.Length, ElementIndex =>
            //for (Int32 ElementIndex = 0; ElementIndex < Correlations.Length; ElementIndex++)
            {
                TimeSpan TimeDifference;

                // Occurences of each boolean pair of memory change and input engaged (false, false), (true, false) ... etc
                Int32 A00 = 0;
                Int32 B01 = 0;
                Int32 C10 = 0;
                Int32 D11 = 0;

                for (Int32 TimeStepIndex = 0; TimeStepIndex < ScanHistoryTime.Count; TimeStepIndex++)
                {
                    Boolean MemoryChanged = ChangeHistory[TimeStepIndex][(Int32)ElementIndex];
                    Boolean InputChanged = false;

                    // Find the nearest input to this memory change based on the search method
                    for (Int32 InputIndex = 0; InputIndex < Input.Count; InputIndex++)
                    {
                        // Ignore inputs that occurred before previous scans
                        if (TimeStepIndex > 0 && Input[InputIndex] < ScanHistoryTime[TimeStepIndex - 1])
                            continue;

                        // We do not care about looking into the future (for now?)
                        if (Input[InputIndex] > ScanHistoryTime[TimeStepIndex])
                            break;

                        TimeDifference = ScanHistoryTime[TimeStepIndex] - Input[InputIndex];
                        
                        if (TimeDifference <= MaxDifference)
                        {
                            InputChanged = true;
                            break;
                        }
                    }

                    if (!MemoryChanged && !InputChanged)
                        A00++;
                    else if (!MemoryChanged && InputChanged)
                        B01++;
                    else if (MemoryChanged && !InputChanged)
                        C10++;
                    else if (MemoryChanged && InputChanged)
                        D11++;
                }

                // Calculate the X squared statistic from the contingency table values (A, B, C, and D)
                Int32 Total = A00 + B01 + C10 + D11;
                /* Single Denominator = (A00 + B01) * (C10 + D11) * (B01 + D11) * (A00 + C10);
                 Single XSquared = 0;
                 Single MinValue = Math.Min(A00, Math.Min(B01, Math.Min(C10, D11)));

                 if (Denominator != 0)
                     XSquared = (A00 * D11 - B01 * C10) * (A00 * D11 - B01 * C10) * (A00 + B01 + C10 + D11) / Denominator;
                     */
                Single WeightedImpact = 0;  // Unknown if this will work at all. Could potentially outperform all methods.
                                            /*
                                            Single CramerPhi = 0;       // Rates things poorly (worst one) and finds nothing but garbage.
                                            Single Phi = 0;             // Seems to find things that correlate well. Useful stuff doesnt seem to get highly rated.
                                            Single AdjustedC = 0;       // Same as contingency. Seemed to rate useful stuff worse than garbage variables.
                                            Single ContingencyC = 0;    // Doesn't find shit except for a few garbage variables. Might be broken.
                                            Single CramerV = 0;         // High amount of 0s. Rates things quite lowly. Finds useful things though.
                                            Single Tetrachoric = 0;     // Tends to rate bad things quite highly, tends to pick up garbage variables.
                                            Single Fisher = 0;
                                            */
                if (Total > 0)
                    WeightedImpact = WeightedImpact = (Single)(Total - (C10 + B01)) * (Single)D11;

                /*
                if (Total > 0 && MinValue > 1)
                    CramerPhi = XSquared / ((Single)Total * (MinValue - 1));

                if (Denominator != 0)
                    Phi = (A00 * D11 - B01 * C10) / (Single)Math.Sqrt(Denominator);

                if (Total > 0 && MinValue > 1)
                    AdjustedC = (Single)Math.Sqrt(XSquared * MinValue / ((XSquared + (Single)Total) * (MinValue - 1)));

                if (Total > 0)
                    ContingencyC = (Single)Math.Sqrt(XSquared / ((Single)Total + XSquared));

                if (Total > 0 && MinValue > 1)
                    CramerV = (Single)Math.Sqrt(XSquared / ((Single)Total * (MinValue - 1)));

                if (B01 + C10 != 0)
                    Tetrachoric = (Single)(Math.Cos(180.0 / (1.0 + Math.Sqrt(D11 * A00 / (B01 + C10)))));
                    */
                //Single A = NChooseK(A00 + B01, A00);
                //Single B = NChooseK(C10 + D11, C10);
                //Single C = NChooseK(Total, A00 + C10);

                //if (MinValue != 0)
                //Fisher = (A + B) / C;

                //Fisher = (Single)(Fact(A00 + B01) * Fact(C10 + D11) * Fact(A00 + C10) * Fact(B01 + D11)) / (Single)(Fact(A00) * Fact(B01) * Fact(C10) * Fact(D11) * Fact(Total));

                //Correlation[ElementIndex] = new CorrelationValues(WeightedImpact, CramerPhi,
                //    Phi, AdjustedC, ContingencyC, CramerV, Tetrachoric, Fisher);
                Correlations[ElementIndex] = WeightedImpact; // new CorrelationValues(CramerV);
            });

            List<Tuple<IntPtr, Object>> SortedAddresses = Addresses
              .Zip(Correlations, (A, C) => Tuple.Create(A, C))
              .OrderByDescending(x => x.Item2)
              .ToList();
            
            // Correlations = sortedPairs.Select(x => x.Item1).ToList();
            // Addresses = sortedPairs.Select(x => x.Item2).ToList();

            return SortedAddresses;
        }


    } // End class

} // End namespace
