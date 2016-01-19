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

        public event LabelThresholderEventHandler EventUpdateHistogram;

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

        public void GatherData()
        {
            if (!SnapshotManager.GetInstance().HasActiveSnapshot())
                return;

            Snapshot Snapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

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
            
            LabelThresholderEventArgs Args = new LabelThresholderEventArgs();
            Args.SortedDictionary = new SortedDictionary<dynamic, Int64>(Histogram);
            EventUpdateHistogram(this, Args);
        }

    } // End class

} // End namespace