using Anathema.Source.Engine;
using Anathema.Source.Engine.DotNetObjectCollector;
using Anathema.Source.Engine.Processes;
using Anathema.Source.Project;
using Anathema.Source.Project.ProjectItems;
using System.Collections.Generic;

namespace Anathema.Source.Utils.DotNetExplorer
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class DotNetExplorer : IDotNetExplorerModel, IProcessObserver
    {
        private EngineCore EngineCore;

        // Event stubs
        public event DotNetExplorerEventHandler EventRefreshObjectTrees;

        public DotNetExplorer()
        {
            InitializeProcessObserver();
        }

        public void OnGUIOpen() { }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;

            RefreshObjectTrees();
        }
        public void AddToTable(DotNetObject DotNetObject)
        {
            AddressItem AddressItem = new AddressItem(DotNetObject.GetFullName(), DotNetObject.GetElementType(), DotNetObject.GetName());
            ProjectExplorer.GetInstance().AddProjectItem(AddressItem);
        }

        public void RefreshObjectTrees()
        {
            if (EngineCore == null)
                return;

            List<DotNetObject> ObjectTrees = DotNetObjectCollector.GetInstance().GetObjectTrees();

            DotNetExplorerEventArgs Args = new DotNetExplorerEventArgs();
            Args.ObjectTrees = ObjectTrees;
            EventRefreshObjectTrees?.Invoke(this, Args);
        }

    } // End class

} // End namespace