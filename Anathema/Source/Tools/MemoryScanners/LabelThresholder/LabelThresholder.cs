using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class LabelThresholder : ILabelThresholderModel
    {
        private MemorySharp MemoryEditor;

        public event LabelThresholderEventHandler EventDisplayScript;

        public LabelThresholder()
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
        }
    }
}