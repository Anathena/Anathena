using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class MemoryFilter : IMemoryFilter
    {
        protected MemorySharp MemoryEditor;
        protected List<RemoteRegion> MemoryRegions;
        
        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public void UpdateMemoryRegions(List<RemoteRegion> MemoryRegions)
        {
            this.MemoryRegions = MemoryRegions;
        }
        
        public virtual void BeginFilter()
        {
            throw new NotImplementedException();
        }

        public virtual List<RemoteRegion> EndFilter()
        {
            throw new NotImplementedException();
        }
    }
}
