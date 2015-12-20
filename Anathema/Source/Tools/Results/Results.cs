using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Handles the querying of values for filtered results
    /// </summary>
    class Results : IResultsModel
    {
        private MemorySharp MemoryEditor;

        private CancellationTokenSource CancelRequest;  // Tells the updates to stop
        private Task ValueScanner;                      // Event that constantly checks the target process for changes

        private const Int32 AbortTime = 1000;       // Time to wait (in ms) before giving up when ending scan
        private Int32 WaitTime = 487;               // Time to wait (in ms) for a cancel request between each scan. (I chose a prime # to prevent bad synchronization with target process)

        public event ResultsEventHandler EventUpdateDisplay;

        public Results()
        {
            InitializeObserver();
        }

        ~Results()
        {
            EndQueries();
        }

        public void InitializeObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            EndQueries();
            this.MemoryEditor = MemoryEditor;
            BeginQueries();
        }

        public void BeginQueries()
        {
            CancelRequest = new CancellationTokenSource();
            ValueScanner = Task.Run(async () =>
            {
                while (true)
                {
                    // Query the target process for memory changes
                    QueryValues();

                    // Await with cancellation
                    await Task.Delay(WaitTime, CancelRequest.Token);
                }
            }, CancelRequest.Token);
        }

        private void QueryValues()
        {
            List<RemoteRegion> Regions = SnapshotManager.GetSnapshotManagerInstance().GetActiveSnapshot().GetMemoryRegions();

            List<UInt64> Addresses = new List<UInt64>();
            List<String> AddressStrings = new List<String>();
            List<String> Values = new List<String>();

            // Grab first 1000 addresses
            foreach (RemoteRegion Region in Regions)
            {
                for (UInt64 Address = (UInt64)Region.BaseAddress; Address < (UInt64)Region.EndAddress; Address++)
                {
                    Addresses.Add(Address);
                    AddressStrings.Add(Address.ToString());
                    if (Addresses.Count >= 1000)
                        break;
                }
                if (Addresses.Count >= 1000)
                    break;
            }

            // Read values for each address
            foreach (UInt64 Address in Addresses)
            {
                Boolean Success;
                Single Value = MemoryEditor.Read<Single>((IntPtr)Address, out Success, false);

                if (Success)
                    Values.Add(Value.ToString());
                else
                    Values.Add("??");
            }

            // Send the size of the filtered memory to the GUI
            ResultsEventArgs Args = new ResultsEventArgs();
            Args.Addresses = AddressStrings;
            Args.Values = Values;
        }

        public void EndQueries()
        {
            if (CancelRequest == null)
                return;

            // Wait for the filter to finish
            CancelRequest.Cancel();
            try { ValueScanner.Wait(AbortTime); }
            catch (AggregateException) { }
        }
    }
}
