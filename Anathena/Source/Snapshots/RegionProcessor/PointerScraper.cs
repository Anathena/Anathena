using Anathena.Source.Engine;
using Anathena.Source.Engine.Processes;
using System;
using System.Collections.Concurrent;

namespace Anathena.Source.Snapshots.RegionProcessor
{
    /// <summary>
    /// Class to continuously collect a list of pointers in the target process. The intent is to make a list of all pointers,
    /// and replace the prefilters we have. The way this is accomplished is by taking these 'radial pointers', and keeping all memory
    /// within some 'radius' of the pointer. Ie a pointer to 0x0040000 with a radius of 2kb will keep the chunk at that location.
    /// 
    /// The idea is simple -- most, if not all memory we give a shit about is possible to pointer scan for. Which means things point to those things.
    /// Which means we can use this same logic as a search space reduction technique
    /// 
    /// TODO:
    /// The region passing model I came up with seems like it might be complete shit in this situation
    /// Instead we probably need to base this on prefilters
    /// 
    /// If this works as well as I hope, all prefilters will be uneeded, and this will completely shit on any method I've come up with so far
    /// </summary>
    class PointerScraper : IProcessObserver, IRegionProcessor
    {
        private EngineCore EngineCore;
        private Snapshot Snapshot;
        private ConcurrentDictionary<IntPtr, IntPtr> PointerPool;

        public PointerScraper()
        {
            InitializeProcessObserver();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
        }

        public void ProcessRegion(SnapshotRegion Region)
        {
            // As far as I can tell, no valid pointers will end up being less than 0x10000 (UInt16.MaxValue), nor higher than usermode space.
            dynamic InvalidPointerMin = EngineCore.Memory.IsProcess32Bit() ? (Int32)UInt16.MaxValue : (Int64)UInt16.MaxValue;
            dynamic InvalidPointerMax = EngineCore.Memory.IsProcess32Bit() ? Int32.MaxValue : Int64.MaxValue;

            if (!Region.HasValues())
                return;

            foreach (SnapshotElement Element in Region)
            {
                if (Element.LessThanValue(InvalidPointerMin))
                    continue;

                if (Element.GreaterThanValue(InvalidPointerMax))
                    continue;

                // Enforce 4-byte alignment of destination
                if (Element.GetValue() % 4 != 0)
                    continue;

                IntPtr Value = new IntPtr(Element.GetValue());

                // Check if it is possible that this pointer is valid, if so keep it
                if (Snapshot.ContainsAddress(Value))
                    PointerPool[Element.BaseAddress] = Value;
            }
        }

        public void FinishedAllRegions()
        {
            throw new NotImplementedException();
        }

    } // End interface

} // End namespace