using Anathena.Source.Utils;
using System;
using System.Collections.Generic;

namespace Anathena.Source.Snapshots.RegionProcessor
{
    class RegionProcessorManager : RepeatedTask
    {
        // Enumeration of all processors
        private LinkedList<IRegionProcessor> RegionProcessors;

        // Individual processors
        private ByteFrequencyProcessor ByteFrequencyProcessor;
        private PointerScraper PointerScraper;

        private const Int32 ChunkLimit = 32768;
        private const Int32 ChunkSize = 4096;
        private const Int32 RescanTime = 400;

        public RegionProcessorManager()
        {
            RegionProcessors = new LinkedList<IRegionProcessor>();
            ByteFrequencyProcessor = new ByteFrequencyProcessor();
            PointerScraper = new PointerScraper();

            RegionProcessors.AddLast(ByteFrequencyProcessor);
            RegionProcessors.AddLast(PointerScraper);
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            foreach (IRegionProcessor RegionProcessor in RegionProcessors)
                RegionProcessor.ProcessRegion(null);
        }

        protected override void End()
        {
            base.End();
        }

    } // End class

} // End namespace