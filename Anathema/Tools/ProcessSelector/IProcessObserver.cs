using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IProcessSubject
    {
        void Subscribe(IProcessObserver Observer);
        void Unsubscribe(IProcessObserver Observer);
        void Notify();
    }
}
