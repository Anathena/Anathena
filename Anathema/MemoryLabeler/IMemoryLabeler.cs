using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IMemoryLabeler : IMemoryEditTool
    {
        void BeginFilter(MemorySharp MemorySharp, List<RemoteRegion> Regions);
        List<RemoteRegion> EndFilter();
    }
}
