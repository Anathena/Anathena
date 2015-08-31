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
    class FiniteStateFilter : MemorySharp
    {
        private List<RemoteVirtualPage> RemoteRegions;

        public FiniteStateFilter(Process Process) : base(Process)
        {

        }

        public void Begin(List<RemoteVirtualPage> RemoteRegions)
        {
            this.RemoteRegions = RemoteRegions;
        }
    }
}
