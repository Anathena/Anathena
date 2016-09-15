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
        private EngineCore engineCore;

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

        public void UpdateEngineCore(EngineCore engineCore)
        {
            this.engineCore = engineCore;

            RefreshObjectTrees();
        }

        public void AddToTable(DotNetObject dotNetObject)
        {
            AddressItem addressItem = new AddressItem(IntPtr.Zero, dotNetObject.GetElementType(),
                dotNetObject.GetName(), AddressResolver.ResolveTypeEnum.DotNet, dotNetObject.GetFullName());
            ProjectExplorer.GetInstance().AddProjectItem(addressItem);
        }

        public void UpdateSelection(IEnumerable<DotNetObject> dotNetObjects)
        {
            PropertyViewer.GetInstance().SetTargetObjects(dotNetObjects.ToArray());
        }

        public void RefreshObjectTrees()
        {
            if (engineCore == null)
                return;

            List<DotNetObject> objectTrees = DotNetObjectCollector.GetInstance().GetObjectTrees();

            DotNetExplorerEventArgs args = new DotNetExplorerEventArgs();
            args.objectTrees = objectTrees;
            EventRefreshObjectTrees?.Invoke(this, args);
        }

    } // End class

} // End namespace