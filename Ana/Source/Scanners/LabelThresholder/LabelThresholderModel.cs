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
        private Double upperThreshold;

        private Double lowerThreshold;

        public LabelThresholderModel(Action onUpdateHistogram) : base("Label Thresholder")
        {
            this.ItemLock = new Object();
            this.SnapshotLock = new Object();
            this.OnUpdateHistogram = onUpdateHistogram;
            Task.Run(() => SnapshotManager.GetInstance().Subscribe(this));
        }

        [Obfuscation(Exclude = true)]
        public Double LowerThreshold
        {
            get
            {
                return this.lowerThreshold;
            }

            set
            {
                this.lowerThreshold = value;
                this.UpdateHistogram();
            }
        }

        [Obfuscation(Exclude = true)]
        public Double UpperThreshold
        {
            get
            {
                return this.upperThreshold;
            }

            set
            {
                this.upperThreshold = value;
                this.UpdateHistogram();
            }
        }

        [Obfuscation(Exclude = true)]
        public SortedList<dynamic, Int64> Histogram { get; set; }

        [Obfuscation(Exclude = true)]
        public SortedList<dynamic, Int64> HistogramFiltered { get; set; }

        [Obfuscation(Exclude = true)]
        public SortedList<dynamic, Int64> HistogramKept { get; set; }

        private Int32 LowerIndex { get; set; }

        private Int32 UpperIndex { get; set; }

        private Action OnUpdateHistogram { get; set; }

        private Snapshot Snapshot { get; set; }

        private Boolean Inverted { get; set; }

        private Object ItemLock { get; set; }

        private Object SnapshotLock { get; set; }

        public void ToggleInverted()
        {
            this.Inverted = !this.Inverted;
            this.UpdateHistogram(forceUpdate: true);
        }

        public void Update(Snapshot snapshot)
        {
            lock (this.SnapshotLock)
            {
                this.Snapshot = snapshot;
            }

            this.Begin();
        }

        public void ApplyThreshold()
        {
            lock (this.SnapshotLock)
            {
                if (this.Snapshot == null)
                {
                    return;
                }
            }

            dynamic lowerValue = this.Histogram.Keys[this.LowerIndex];
            dynamic upperValue = this.Histogram.Keys[this.UpperIndex];

            lock (this.SnapshotLock)
            {
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
            }

            SnapshotManager.GetInstance().SaveSnapshot(this.Snapshot);
            this.UpdateHistogram(forceUpdate: true);
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void OnUpdate()
        {
            ConcurrentDictionary<dynamic, Int64> histogram = new ConcurrentDictionary<dynamic, Int64>();

            lock (this.SnapshotLock)
            {
                if (this.Snapshot == null)
                {
                    this.End();
                    return;
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
            }

            this.Histogram = new SortedList<dynamic, Int64>(histogram);
            this.UpdateHistogram();
            this.End();
        }

        /// <summary>
        /// Called when the repeated task completes
        /// </summary>
        protected override void OnEnd()
        {
            base.OnEnd();
        }

        private void UpdateHistogram(Boolean forceUpdate = false)
        {
            if (this.Histogram == null || this.Histogram.Count <= 0)
            {
                return;
            }

            Int32 lowerIndex = (Int32)Math.Round(((Double)this.LowerThreshold / 100.0) * (Double)(this.Histogram.Count - 1));
            Int32 upperIndex = (Int32)Math.Round(((Double)this.UpperThreshold / 100.0) * (Double)(this.Histogram.Count - 1));

            // Prevent updating the histograms again if we have already computed them
            if (!forceUpdate && lowerIndex == this.LowerIndex && upperIndex == this.UpperIndex)
            {
                return;
            }

            this.LowerIndex = lowerIndex;
            this.UpperIndex = upperIndex;

            SortedList<dynamic, Int64> histogramKept = new SortedList<dynamic, Int64>();
            SortedList<dynamic, Int64> histogramFiltered = new SortedList<dynamic, Int64>();

            foreach (KeyValuePair<dynamic, Int64> bar in this.Histogram)
            {
                Int32 index = this.Histogram.IndexOfKey(bar.Key);

                if (this.Inverted ^ (index >= lowerIndex && index <= upperIndex))
                {
                    // Keep items within bounds (unless inverted)
                    histogramKept.Add(bar.Key, bar.Value);
                    histogramFiltered.Add(bar.Key, 0);
                }
                else
                {
                    // Filter items outside bounds (unless inverted)
                    histogramKept.Add(bar.Key, 0);
                    histogramFiltered.Add(bar.Key, bar.Value);
                }
            }

            this.HistogramKept = histogramKept;
            this.HistogramFiltered = histogramFiltered;
            this.OnUpdateHistogram();
        }
    }
    //// End class
}
//// End namespace