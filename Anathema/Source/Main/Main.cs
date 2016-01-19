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
        private static Main _Main;

        public event MainEventHandler EventUpdateProcessTitle;
        public event MainEventHandler EventOpenScriptEditor;
        public event MainEventHandler EventOpenLabelThresholder;

        private Main()
        {
            InitializeObserver();
        }
        
        public static Main GetInstance()
        {
            if (_Main == null)
                _Main = new Main();
            return _Main;
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

        public void OpenScriptEditor()
        {
            EventOpenScriptEditor(this, new MainEventArgs());
        }
        
        public void OpenLabelThresholder()
        {
            EventOpenLabelThresholder(this, new MainEventArgs());
        }

    } // End class

} // End namespace