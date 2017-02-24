namespace Ana.Source.Scanners.LabelThresholder
{
    using Snapshots;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UserSettings;

    internal class LabelThresholderModel : ScannerBase, ISnapshotObserver
    {
        private Double upperThreshold;

        private Double lowerThreshold;

        public LabelThresholderModel(Action onUpdateHistogram) : base(
            scannerName: "Label Thresholder",
            isRepeated: false,
            dependencyBehavior: null)
        {
            this.ItemLock = new Object();
            this.SnapshotLock = new Object();
            this.ProgressLock = new Object();
            this.OnUpdateHistogram = onUpdateHistogram;
            Task.Run(() => SnapshotManager.GetInstance().Subscribe(this));
        }

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

        public SortedList<dynamic, Int64> Histogram { get; set; }

        public SortedList<dynamic, Int64> HistogramFiltered { get; set; }

        public SortedList<dynamic, Int64> HistogramKept { get; set; }

        private Int32 LowerIndex { get; set; }

        private Int32 UpperIndex { get; set; }

        private Action OnUpdateHistogram { get; set; }

        private Snapshot Snapshot { get; set; }

        private Boolean Inverted { get; set; }

        private Object ItemLock { get; set; }

        private Object SnapshotLock { get; set; }

        private Object ProgressLock { get; set; }

        public void ToggleInverted()
        {
            this.Inverted = !this.Inverted;
            this.UpdateHistogram(forceUpdate: true);
        }

        /// <summary>
        /// Recieves an update of the active snapshot.
        /// </summary>
        /// <param name="snapshot">The active snapshot.</param>
        public void Update(Snapshot snapshot)
        {
            lock (this.SnapshotLock)
            {
                this.Snapshot = snapshot;

                if (this.Snapshot == null)
                {
                    return;
                }
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
                    this.Snapshot.SetAllValidBits(false);

                    foreach (SnapshotRegion region in this.Snapshot)
                    {
                        foreach (SnapshotElementRef element in region)
                        {
                            if (((dynamic)element).GetElementLabel() >= lowerValue && ((dynamic)element).GetElementLabel() <= upperValue)
                            {
                                element.SetValid(true);
                            }
                        }
                    }
                }
                else
                {
                    this.Snapshot.SetAllValidBits(true);

                    foreach (SnapshotRegion region in this.Snapshot)
                    {
                        foreach (SnapshotElementRef element in region)
                        {
                            if (((dynamic)element).GetElementLabel() >= lowerValue && ((dynamic)element).GetElementLabel() <= upperValue)
                            {
                                element.SetValid(false);
                            }
                        }
                    }
                }

                this.Snapshot.DiscardInvalidRegions();
            }

            SnapshotManager.GetInstance().SaveSnapshot(this.Snapshot);
            this.UpdateHistogram(forceUpdate: true);
        }

        protected override void OnBegin()
        {
            base.OnBegin();
        }

        protected override void OnUpdate()
        {
            ConcurrentDictionary<dynamic, Int64> histogram = new ConcurrentDictionary<dynamic, Int64>();
            Int32 processedPages = 0;

            lock (this.SnapshotLock)
            {
                if (this.Snapshot == null)
                {
                    this.Cancel();
                    return;
                }

                Parallel.ForEach(
                    this.Snapshot.Cast<Object>(),
                    SettingsViewModel.GetInstance().ParallelSettings,
                    (regionObject) =>
                {
                    SnapshotRegion region = regionObject as SnapshotRegion;

                    if (((dynamic)region).GetElementLabels() == null || region.GetElementCount() <= 0)
                    {
                        return;
                    }

                    foreach (SnapshotElementRef element in region)
                    {
                        lock (this.ItemLock)
                        {
                            if (histogram.ContainsKey(((dynamic)element).GetElementLabel()))
                            {
                                histogram[((dynamic)element).GetElementLabel()]++;
                            }
                            else
                            {
                                histogram.TryAdd(((dynamic)element).GetElementLabel(), 1);
                            }
                        }
                    }

                    lock (this.ProgressLock)
                    {
                        processedPages++;
                        this.UpdateProgress(processedPages, this.Snapshot.GetRegionCount());
                    }
                    //// End foreach element
                });
                //// End foreach region
            }

            this.Histogram = new SortedList<dynamic, Int64>(histogram);
            this.UpdateHistogram();
            this.Cancel();

            base.OnUpdate();
        }

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