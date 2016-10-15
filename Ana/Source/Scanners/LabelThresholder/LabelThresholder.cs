namespace Ana.Source.Scanners.LabelThresholder
{
    using Snapshots;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Utils;

    internal class LabelThresholder
    {
        private Snapshot Snapshot { get; set; }

        private SortedDictionary<dynamic, Int64> SortedDictionary { get; set; }

        private Boolean Inverted { get; set; }

        private Object ItemLock { get; set; }

        [Obfuscation(Exclude = true)]
        private dynamic MinValue { get; set; }

        [Obfuscation(Exclude = true)]
        private dynamic MaxValue { get; set; }

        public LabelThresholder()
        {
            ItemLock = new Object();
        }

        public void SetInverted(Boolean Inverted)
        {
            this.Inverted = Inverted;
        }

        public void UpdateThreshold(Int32 minimumIndex, Int32 maximumIndex)
        {
            if (SortedDictionary == null)
            {
                return;
            }

            this.MinValue = SortedDictionary.ElementAt(minimumIndex).Key;
            this.MaxValue = SortedDictionary.ElementAt(maximumIndex).Key;
        }

        public Type GetElementType()
        {
            if (Snapshot == null)
            {
                return null;
            }

            return Snapshot.ElementType;
        }

        public void Begin()
        {
            Snapshot = SnapshotManager.GetInstance().GetActiveSnapshot(false);

            if (Snapshot == null)
            {
                return;
            }
        }

        protected void Update()
        {
            ConcurrentDictionary<dynamic, Int64> histogram = new ConcurrentDictionary<dynamic, Int64>();

            Parallel.ForEach(Snapshot.Cast<Object>(), (regionObject) =>
            {
                SnapshotRegion Region = (SnapshotRegion)regionObject;
                foreach (dynamic Element in Region)
                {
                    if (Element.ElementLabel == null)
                    {
                        return;
                    }

                    using (TimedLock.Lock(ItemLock))
                    {
                        if (histogram.ContainsKey(Element.ElementLabel))
                            histogram[((dynamic)Element.ElementLabel)]++;
                        else
                            histogram.TryAdd(Element.ElementLabel, 1);
                    }
                }
                //// End foreach element
            });
            //// End foreach region

            this.SortedDictionary = new SortedDictionary<dynamic, Int64>(histogram);

            //// LabelThresholderEventArgs Args = new LabelThresholderEventArgs();
            //// Args.SortedDictionary = SortedDictionary;
            //// OnEventUpdateHistogram(Args);

            //// CancelFlag = true;
        }

        protected void End()
        {

        }

        public void ApplyThreshold()
        {
            if (Snapshot == null)
            {
                return;
            }

            if (!Inverted)
            {
                Snapshot.MarkAllInvalid();
                foreach (SnapshotRegion region in Snapshot)
                {
                    foreach (dynamic element in region)
                    {
                        if (element.ElementLabel >= MinValue && element.ElementLabel <= MaxValue)
                        {
                            element.Valid = true;
                        }
                    }
                }
            }
            else
            {
                Snapshot.MarkAllValid();
                foreach (SnapshotRegion region in Snapshot)
                {
                    foreach (dynamic element in region)
                    {
                        if (element.ElementLabel >= MinValue && element.ElementLabel <= MaxValue)
                        {
                            element.Valid = false;
                        }
                    }
                }
            }

            Snapshot.DiscardInvalidRegions();
            //// Snapshot.SetScanMethod("Label Thresholder");
            //// SnapshotManager.GetInstance().SaveSnapshot(Snapshot);
        }
    }
    //// End class
}
//// End namespace