using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IProcessObserver
    {
        void UpdateProcess(MemorySharp MemoryEditor);
    }
}
