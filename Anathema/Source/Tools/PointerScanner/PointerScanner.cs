using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class PointerScanner : IPointerScannerModel
    {
        private MemorySharp MemoryEditor;

        public event PointerScannerEventHandler EventUpdateProcessTitle;

        public PointerScanner()
        {
            InitializeProcessObserver();
        }
        
        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public void BeginPointerScanner()
        {
            RemoteModule k = MemoryEditor.Modules.MainModule;
        }
    }
}