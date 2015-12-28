using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;

namespace Anathema
{
    class LabelerChangeCounter : ILabelerChangeCounterModel
    {
        private Snapshot<UInt16> CurrentSnapshot;
        private CancellationTokenSource CancelRequest;      // Tells the scan task to cancel (ie finish)
        private Task ChangeScanner;                         // Event that constantly checks the target process for changes

        // Variables
        private const Int32 AbortTime = 3000;       // Time to wait (in ms) before giving up when ending scan
        private Int32 WaitTime = 200;               // Time to wait (in ms) for a cancel request between each scan

        private Int32 MinChanges;
        private Int32 MaxChanges;
        private Int32 VariableSize;

        // Event stubs
        public event EventHandler EventLabelerFinished;

        public LabelerChangeCounter()
        {

        }

        public void SetMinChanges(Int32 MinChanges)
        {
            this.MinChanges = MinChanges;
        }

        public void SetMaxChanges(Int32 MaxChanges)
        {
            this.MaxChanges = MaxChanges;
        }

        public void SetVariableSize(Int32 VariableSize)
        {
            this.VariableSize = VariableSize;
        }

        public void BeginFilter()
        {
            // Grab the current snapshot and assign counts of 0 to all addresses
            Snapshot InitialSnapshot = SnapshotManager.GetSnapshotManagerInstance().GetActiveSnapshot();
            List<UInt16> Counts = new List<UInt16>(new UInt16[InitialSnapshot.GetSize()]);
            CurrentSnapshot = new Snapshot<UInt16>(InitialSnapshot.GetMemoryRegions());
            CurrentSnapshot.AssignLabels(Counts);

            CancelRequest = new CancellationTokenSource();
            ChangeScanner = Task.Run(async () =>
            {
                while (true)
                {
                    // Query the target process for memory changes
                    ApplyFilter();

                    // Await with cancellation
                    await Task.Delay(WaitTime, CancelRequest.Token);
                }
            }, CancelRequest.Token);
        }

        private void ApplyFilter()
        {
            List<Int32> IndexMapping = CurrentSnapshot.GetLabelMapping();
            List<UInt16> Labels = CurrentSnapshot.GetMemoryLabels();

            if (!CurrentSnapshot.HasLabels())
                throw new Exception("Count labels missing");



            //CurrentSnapshot.AssignLabels(PageData);
        }

        public Snapshot<Object> EndFilter()
        {
            // Wait for the filter to finish
            CancelRequest.Cancel();
            try { ChangeScanner.Wait(AbortTime); }
            catch (AggregateException) { }

            return null;
        }

        public void BeginLabeler()
        {
            throw new NotImplementedException();
        }

        public Snapshot<Object> EndLabeler()
        {
            throw new NotImplementedException();
        }

    } // End class

} // End namespace