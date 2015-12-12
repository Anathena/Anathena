using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Defines the data contained in a single snapshot
    /// </summary>
    class Snapshot
    {
        private DateTime TimeStamp;
        private List<RemoteRegion> MemoryRegions;
        private List<Object> MemoryLabels;

        public Snapshot()
        {
            MemoryRegions = new List<RemoteRegion>();
            MemoryLabels = new List<Object>();
        }

        public Snapshot(List<RemoteRegion> MemoryRegions)
        {
            this.MemoryRegions = MemoryRegions;
            MemoryLabels = new List<Object>();
        }

        public Snapshot(List<RemoteRegion> MemoryRegions, List<Object> MemoryLabels)
        {
            this.MemoryRegions = MemoryRegions;
            this.MemoryLabels = MemoryLabels;
        }

        public void SetTimeStampToNow()
        {
            TimeStamp = DateTime.Now;
        }

        public DateTime GetTimeStamp()
        {
            return TimeStamp;
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
