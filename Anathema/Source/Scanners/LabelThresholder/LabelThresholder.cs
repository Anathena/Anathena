using Anathema.Services.ScanResults;
using Anathema.Services.Snapshots;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anathema.Scanners.LabelThresholder
{
    class LabelThresholder : ILabelThresholderModel
    {
        private Snapshot Snapshot;

        private SortedDictionary<dynamic, Int64> SortedDictionary;
        private Boolean Inverted;
        private Object ItemLock;

        private dynamic MinValue;
        private dynamic MaxValue;

        public LabelThresholder()
        {
            ItemLock = new Object();
        }

        public override void SetInverted(Boolean Inverted)
        {
            this.Inverted = Inverted;
        }

        public override void UpdateThreshold(Int32 MinimumIndex, Int32 MaximumIndex)
        {
            if (SortedDictionary == null)
                return;

            this.MinValue = SortedDictionary.ElementAt(MinimumIndex).Key;
            this.MaxValue = SortedDictionary.ElementAt(MaximumIndex).Key;
        }

        public override Type GetElementType()
        {
            if (Snapshot == null)
                return null;

            return Snapshot.GetElementType();
        }

        public override void Begin()
        {
            Snapshot = SnapshotManager.GetInstance().GetActiveSnapshot(false);

            if (Snapshot == null)
                return;

            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            ConcurrentDictionary<dynamic, Int64> Histogram = new ConcurrentDictionary<dynamic, Int64>();

            Parallel.ForEach(Snapshot.Cast<Object>(), (RegionObject) =>
            {
                SnapshotRegion Region = (SnapshotRegion)RegionObject;
                foreach (dynamic Element in Region)
                {
                    if (Element.ElementLabel == null)
                        return;

                    lock (ItemLock)
                    {
                        if (Histogram.ContainsKey(Element.ElementLabel))
                            Histogram[((dynamic)Element.ElementLabel)]++;
                        else
                            Histogram.TryAdd(Element.ElementLabel, 1);
                    }

                } // End foreach element

            }); // End foreach region

            this.SortedDictionary = new SortedDictionary<dynamic, Int64>(Histogram);

            LabelThresholderEventArgs Args = new LabelThresholderEventArgs();
            Args.SortedDictionary = SortedDictionary;
            OnEventUpdateHistogram(Args);

            CancelFlag = true;
        }

        public override void End()
        {
            base.End();
        }

        public override void ApplyThreshold()
        {
            if (Snapshot == null)
                return;

            if (!Inverted)
            {
                Snapshot.MarkAllInvalid();
                foreach (SnapshotRegion Region in Snapshot)
                    foreach (dynamic Element in Region)
                        if (Element.ElementLabel >= MinValue && Element.ElementLabel <= MaxValue)
                            Element.Valid = true;
            }
            else
            {
                Snapshot.MarkAllValid();
                foreach (SnapshotRegion Region in Snapshot)
                    foreach (dynamic Element in Region)
                        if (Element.ElementLabel >= MinValue && Element.ElementLabel <= MaxValue)
                            Element.Valid = false;
            }

            Snapshot.DiscardInvalidRegions();
            // Snapshot.SetScanMethod("Label Thresholder");
            // SnapshotManager.GetInstance().SaveSnapshot(Snapshot);
            SnapshotManager.GetInstance().ForceRefresh();
            Results.GetInstance().ForceRefresh();
        }

    } // End class

} // End namespace