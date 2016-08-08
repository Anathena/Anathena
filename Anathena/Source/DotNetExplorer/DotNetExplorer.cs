using Anathena.Source.Engine;
using Anathena.Source.Engine.AddressResolver;
using Anathena.Source.Engine.AddressResolver.DotNet;
using Anathena.Source.Engine.Processes;
using Anathena.Source.Project;
using Anathena.Source.Project.ProjectItems;
using Anathena.Source.PropertyView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anathena.Source.DotNetExplorer
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
            AddressItem AddressItem = new AddressItem(IntPtr.Zero, DotNetObject.GetElementType(),
                DotNetObject.GetName(), AddressResolver.ResolveTypeEnum.DotNet, DotNetObject.GetFullName());
            ProjectExplorer.GetInstance().AddProjectItem(AddressItem);
        }

        public void UpdateSelection(IEnumerable<DotNetObject> DotNetObjects)
        {
            PropertyViewer.GetInstance().SetTargetObjects(DotNetObjects.ToArray());
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