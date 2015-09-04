using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IMemoryLabeler
    {
        void BeginLabeler(MemorySharp MemoryEditor, List<RemoteRegion> MemoryRegions);
        List<RemoteRegion> EndLabeler();
        void AbortLabeler();
    }
}
