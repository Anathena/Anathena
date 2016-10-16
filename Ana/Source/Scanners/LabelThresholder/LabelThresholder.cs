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
        public LabelThresholder()
        {
            this.ItemLock = new Object();
        }

        private Snapshot Snapshot { get; set; }

        private SortedDictionary<dynamic, Int64> SortedDictionary { get; set; }

        private Boolean Inverted { get; set; }

        private Object ItemLock { get; set; }

        [Obfuscation(Exclude = true)]
        private dynamic MinValue { get; set; }

        [Obfuscation(Exclude = true)]
        private dynamic MaxValue { get; set; }

        public void SetInverted(Boolean inverted)
        {
            this.Inverted = inverted;
        }

        public void UpdateThreshold(Int32 minimumIndex, Int32 maximumIndex)
        {
            if (this.SortedDictionary == null)
            {
                return;
            }

            this.MinValue = this.SortedDictionary.ElementAt(minimumIndex).Key;
            this.MaxValue = this.SortedDictionary.ElementAt(maximumIndex).Key;
        }

        public Type GetElementType()
        {
            if (Snapshot == null)
            {
                return null;
            }

            return Snapshot.ElementType;
        }

        public void ApplyThreshold()
        {
            if (this.Snapshot == null)
            {
                return;
            }

            if (!this.Inverted)
            {
                this.Snapshot.MarkAllInvalid();
                foreach (SnapshotRegion region in this.Snapshot)
                {
                    foreach (dynamic element in region)
                    {
                        if (element.ElementLabel >= this.MinValue && element.ElementLabel <= this.MaxValue)
                        {
                            element.Valid = true;
                        }
                    }
                }
            }
            else
            {
                this.Snapshot.MarkAllValid();
                foreach (SnapshotRegion region in this.Snapshot)
                {
                    foreach (dynamic element in region)
                    {
                        if (element.ElementLabel >= this.MinValue && element.ElementLabel <= this.MaxValue)
                        {
                            element.Valid = false;
                        }
                    }
                }
            }

            this.Snapshot.DiscardInvalidRegions();
        }

        public void Begin()
        {
            this.Snapshot = SnapshotManager.GetInstance().GetActiveSnapshot(false);

            if (this.Snapshot == null)
            {
                return;
            }
        }

        protected void Update()
        {
            ConcurrentDictionary<dynamic, Int64> histogram = new ConcurrentDictionary<dynamic, Int64>();

            Parallel.ForEach(
                this.Snapshot.Cast<Object>(),
                (regionObject) =>
            {
                SnapshotRegion Region = (SnapshotRegion)regionObject;
                foreach (dynamic Element in Region)
                {
                    if (Element.ElementLabel == null)
                    {
                        return;
                    }

                    using (TimedLock.Lock(this.ItemLock))
                    {
                        if (histogram.ContainsKey(Element.ElementLabel))
                        {
                            histogram[((dynamic)Element.ElementLabel)]++;
                        }
                        else
                        {
                            histogram.TryAdd(Element.ElementLabel, 1);
                        }
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
    }
    //// End class
}
//// End namespace