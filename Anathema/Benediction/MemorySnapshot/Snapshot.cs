using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class Snapshot
    {
        private List<RemoteRegion> MemoryRegions;
        private List<Object> MemoryLabels;

        public Snapshot()
        {
            MemoryRegions = new List<RemoteRegion>();
            MemoryLabels = new List<object>();
        }

        public Snapshot(List<RemoteRegion> MemoryRegions)
        {
            this.MemoryRegions = MemoryRegions;
            MemoryLabels = new List<object>();
        }

        public Snapshot(List<RemoteRegion> MemoryRegions, List<Object> MemoryLabels)
        {
            this.MemoryRegions = MemoryRegions;
            this.MemoryLabels = MemoryLabels;
        }
        
        public void AssignLabels(List<Object> MemoryLabels)
        {
            this.MemoryLabels = MemoryLabels;
        }

        public List<RemoteRegion> GetMemoryRegions()
        {
            return MemoryRegions;
        }

        public List<Object> GetMemoryLabels()
        {
            return MemoryLabels;
        }
    }
}
