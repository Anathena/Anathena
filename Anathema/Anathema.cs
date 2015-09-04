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

        private List<RemoteRegion> MemoryRegions;
        private List<IMemoryLabeler> MemoryLabels;

        private Anathema()
        {
            MemoryRegions = new List<RemoteRegion>();
            MemoryLabels = new List<IMemoryLabeler>();
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
                MemoryRegions = MemoryLabeler.EndLabeler();
            MemoryLabeler = null;
        }

        public void AbortLabeler()
        {
            if (MemoryLabeler != null)
                MemoryLabeler.AbortLabeler();
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
        /// Update the remote memory regions and notify all dependent tools of the change
        /// </summary>
        /// <param name="MemoryRegions"></param>
        public void UpdateMemoryRegions(List<RemoteRegion> MemoryRegions)
        {
            // Instantiate a new memory editor with the new target process
            this.MemoryRegions = MemoryRegions;
        }
    }
}
