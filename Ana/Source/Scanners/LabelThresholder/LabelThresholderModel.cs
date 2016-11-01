namespace Ana.Source.Scanners.LabelThresholder
{
    using Snapshots;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using UserSettings;
    using Utils;

    internal class LabelThresholderModel : ScannerBase, ISnapshotObserver
    {
        public LabelThresholderModel(Action onUpdateHistogram) : base("Label Thresholder")
        {
            this.ItemLock = new Object();
            this.OnUpdateHistogram = onUpdateHistogram;
            Task.Run(() => SnapshotManager.GetInstance().Subscribe(this));
        }

        private Action OnUpdateHistogram { get; set; }

        private Snapshot Snapshot { get; set; }

        private Boolean Inverted { get; set; }

        private Object ItemLock { get; set; }

        [Obfuscation(Exclude = true)]
        public Double UpperThreshold { get; set; }

        [Obfuscation(Exclude = true)]
        public Double LowerThreshold { get; set; }

        [Obfuscation(Exclude = true)]
        public SortedList<dynamic, Int64> Histogram { get; set; }

        public void ToggleInverted()
        {
            this.Inverted = !this.Inverted;
        }

        public void Update(Snapshot snapshot)
        {
            this.Snapshot = snapshot;
            this.Begin();
        }

        public void ApplyThreshold()
        {
            if (this.Snapshot == null)
            {
                return;
            }

            dynamic lowerValue = Histogram.Keys[(Int32)(((Double)this.LowerThreshold / 100.0) * (this.Histogram.Count - 1))];
            dynamic upperValue = Histogram.Keys[(Int32)(((Double)this.UpperThreshold / 100.0) * (this.Histogram.Count - 1))];

            if (!this.Inverted)
            {
                this.Snapshot.MarkAllInvalid();

                foreach (SnapshotRegion region in this.Snapshot)
                {
                    foreach (dynamic element in region)
                    {
                        if (element.ElementLabel >= lowerValue && element.ElementLabel <= upperValue)
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
                        if (element.ElementLabel >= lowerValue && element.ElementLabel <= upperValue)
                        {
                            element.Valid = false;
                        }
                    }
                }
            }

            this.Snapshot.DiscardInvalidRegions();
            SnapshotManager.GetInstance().SaveSnapshot(this.Snapshot);
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void OnUpdate()
        {
            ConcurrentDictionary<dynamic, Int64> histogram = new ConcurrentDictionary<dynamic, Int64>();

            if (this.Snapshot == null)
            {
                this.End();
            }

            Parallel.ForEach(
                this.Snapshot.Cast<Object>(),
                SettingsViewModel.GetInstance().ParallelSettings,
                (regionObject) =>
            {
                SnapshotRegion region = regionObject as SnapshotRegion;

                foreach (dynamic element in region)
                {
                    if (element.ElementLabel == null)
                    {
                        return;
                    }

                    using (TimedLock.Lock(this.ItemLock))
                    {
                        if (histogram.ContainsKey(element.ElementLabel))
                        {
                            histogram[((dynamic)element.ElementLabel)]++;
                        }
                        else
                        {
                            histogram.TryAdd(element.ElementLabel, 1);
                        }
                    }
                }
                //// End foreach element
            });
            //// End foreach region

            this.Histogram = new SortedList<dynamic, Int64>(histogram);
            this.OnUpdateHistogram();
            this.End();
        }

        protected override void OnEnd()
        {
            this.Snapshot = null;
            base.OnEnd();
        }
    }
    //// End class
}
//// End namespace