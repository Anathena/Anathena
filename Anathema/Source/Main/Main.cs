using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class Main : IMainModel
    {
        private MemorySharp MemoryEditor;

        public event MainEventHandler EventUpdateProcessTitle;

        public Main()
        {
            InitializeObserver();
        }
        
        public void InitializeObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;

            MainEventArgs MainEventArgs = new MainEventArgs();
            MainEventArgs.ProcessTitle = MemoryEditor.Native.ProcessName;
            EventUpdateProcessTitle(this, MainEventArgs);
        }
    }
}