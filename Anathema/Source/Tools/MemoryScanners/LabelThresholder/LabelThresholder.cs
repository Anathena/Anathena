using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Modules;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema
{
    class LabelThresholder : ILabelThresholderModel
    {
        private MemorySharp MemoryEditor;
        private Snapshot Snapshot;

        public event LabelThresholderEventHandler EventUpdateHistogram;

        private SortedDictionary<dynamic, Int64> SortedDictionary;
        private Boolean Inverted;

        public LabelThresholder()
        {
            InitializeObserver();

        }

        public void InitializeObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public void SetInverted(Boolean Inverted)
        {
            this.Inverted = Inverted;
        }

        public void GatherData()
        {
            Snapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            if (Snapshot == null)
                return;

            ConcurrentDictionary<dynamic, Int64> Histogram = new ConcurrentDictionary<dynamic, Int64>();
            
            Parallel.ForEach(Snapshot.Cast<Object>(), (RegionObject) =>
            {
                SnapshotRegion Region = (SnapshotRegion)RegionObject;
                foreach (dynamic Element in Region)
                {
                    if (Element.ElementLabel == null)
                        return;

                    if (Histogram.ContainsKey(Element.ElementLabel))
                        Histogram[((dynamic)Element.ElementLabel)]++;
                    else
                        Histogram.TryAdd(Element.ElementLabel, 0);

                } // End foreach element

            }); // End foreach region

            this.SortedDictionary = new SortedDictionary<dynamic, Int64>(Histogram);

            LabelThresholderEventArgs Args = new LabelThresholderEventArgs();
            Args.SortedDictionary = SortedDictionary;
            EventUpdateHistogram(this, Args);
        }

        public void ApplyThreshold(Int32 MinimumIndex, Int32 MaximumIndex)
        {
            dynamic MinValue = SortedDictionary.ElementAt(MinimumIndex).Key;
            dynamic MaxValue = SortedDictionary.ElementAt(MaximumIndex).Key;

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
        }

    } // End class

} // End namespace