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
    /// Controls the main memory editor. Individual tools subscribe to this tool to recieve updates to
    /// changes in the target process.
    /// </summary>
    class Anathema
    {
        private MemorySharp MemoryEditor;

        private List<RemoteRegion> MemoryRegions;
        private List<IMemoryLabeler> MemoryLabels;

        public Anathema()
        {
            MemoryRegions = new List<RemoteRegion>();
            MemoryLabels = new List<IMemoryLabeler>();
        }

        /// <summary>
        /// Discards the old memory regions and gathers all regions from the target process
        /// </summary>
        public void GatherMemoryRegions()
        {
            List<RemoteVirtualPage> VirtualPages = MemoryEditor.Memory.VirtualPages.ToList();
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
