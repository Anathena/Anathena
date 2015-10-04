using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /*
    TODO:
    - Speedhack
    - Manual Scan
    - Batch read/write function (automatic API call minimization)
    - Multiprocess Scan
    - Plugin Support
    - File sharing

    */
    /// <summary>
    /// Singleton class to controls the main memory editor. Individual tools subscribe to this tool to recieve updates to
    /// changes in the target process.
    /// </summary>
    class Anathema
    {
        private static Anathema AnathemaInstance;
        private MemorySharp MemoryEditor;

        private IMemoryFilter MemoryFilter;
        private IMemoryLabeler MemoryLabeler;

        // These do not need to be here. These are managed by snapshots
        private List<RemoteRegion> MemoryRegions;
        private List<Tuple<IntPtr, Object>> MemoryLabels;

        private Anathema()
        {
            MemoryRegions = new List<RemoteRegion>();
            MemoryLabels = new List<Tuple<IntPtr, Object>>();
        }

        public void BeginFilter(IMemoryFilter MemoryFilter)
        {
            this.MemoryFilter = MemoryFilter;

            if (MemoryRegions == null || MemoryRegions.Count == 0)
                GatherMemoryRegions();

            if (MemoryFilter != null)
                MemoryFilter.BeginFilter(MemoryEditor, MemoryRegions);
        }

        public void EndFilter()
        {
            if (MemoryFilter != null)
                MemoryRegions = MemoryFilter.EndFilter();
            MemoryFilter = null;
        }

        public void AbortFilter()
        {
            if (MemoryFilter != null)
                MemoryFilter.AbortFilter();
        }

        public void BeginLabeler(IMemoryLabeler MemoryLabeler)
        {
            this.MemoryLabeler = MemoryLabeler;

            if (MemoryRegions == null || MemoryRegions.Count == 0)
                GatherMemoryRegions();

            if (MemoryLabeler != null)
                MemoryLabeler.BeginLabeler(MemoryEditor, MemoryRegions);
        }

        public void EndLabeler()
        {
            if (MemoryLabeler != null)
                MemoryLabeler.EndLabeler();
            MemoryLabeler = null;
        }

        public void AbortLabeler()
        {
            if (MemoryLabeler != null)
                MemoryLabeler.AbortLabeler();
        }

        public List<Tuple<IntPtr, Object>> GetMemoryLabels()
        {
            return MemoryLabels;
        }

        /// <summary>
        /// Returns the instance of the singleton anathema object
        /// </summary>
        public static Anathema GetAnathemaInstance()
        {
            if (AnathemaInstance == null)
                AnathemaInstance = new Anathema();
            return AnathemaInstance;
        }

        /// <summary>
        /// Discards the old memory regions and gathers all regions from the target process
        /// </summary>
        public void GatherMemoryRegions()
        {
            List<RemoteVirtualPage> VirtualPages = new List<RemoteVirtualPage>();

            foreach (RemoteVirtualPage Page in MemoryEditor.Memory.VirtualPages)
                VirtualPages.Add(Page);

            List<RemoteRegion> MemoryRegions = new List<RemoteRegion>();
            for (int PageIndex = 0; PageIndex < VirtualPages.Count; PageIndex++)
                MemoryRegions.Add(new RemoteRegion(MemoryEditor, VirtualPages[PageIndex].Information.BaseAddress, VirtualPages[PageIndex].Information.RegionSize));
            UpdateMemoryRegions(MemoryRegions);
        }

        /// <summary>
        /// Update the target process 
        /// </summary>
        /// <param name="TargetProcess"></param>
        public void UpdateTargetProcess(Process TargetProcess)
        {
            // Instantiate a new memory editor with the new target process
            MemoryEditor = new MemorySharp(TargetProcess);
        }

        /// <summary>
        /// Updates the remote memory regions
        /// </summary>
        /// <param name="MemoryRegions"></param>
        public void UpdateMemoryRegions(List<RemoteRegion> MemoryRegions)
        {
            // Instantiate a new memory editor with the new target process
            this.MemoryRegions = MemoryRegions;
        }

        /// <summary>
        /// Updates the remote memory labels
        /// </summary>
        /// <param name="MemoryLabels"></param>
        public void UpdateMemoryLabels(List<Tuple<IntPtr, Object>> MemoryLabels)
        {
            // Instantiate a new memory editor with the new target process
            this.MemoryLabels = MemoryLabels;
        }
    }
}
