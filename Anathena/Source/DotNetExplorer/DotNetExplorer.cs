namespace Anathena.Source.DotNetExplorer
{
    using Engine;
    using Engine.AddressResolver;
    using Engine.AddressResolver.DotNet;
    using Engine.Processes;
    using Project;
    using Project.ProjectItems;
    using PropertyView;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    public class DotNetExplorer : IDotNetExplorerModel, IProcessObserver
    {
        public DotNetExplorer()
        {
            this.InitializeProcessObserver();
        }

        public event DotNetExplorerEventHandler EventRefreshObjectTrees;

        private EngineCore EngineCore { get; set; }

        public void OnGUIOpen()
        {
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore engineCore)
        {
            this.EngineCore = engineCore;

            this.RefreshObjectTrees();
        }

        public void AddToTable(DotNetObject dotNetObject)
        {
            AddressItem addressItem = new AddressItem(
                IntPtr.Zero,
                dotNetObject.GetElementType(),
                dotNetObject.GetName(),
                AddressResolver.ResolveTypeEnum.DotNet,
                dotNetObject.GetFullName());
            ProjectExplorer.GetInstance().AddProjectItem(addressItem);
        }

        public void UpdateSelection(IEnumerable<DotNetObject> dotNetObjects)
        {
            PropertyViewer.GetInstance().SetTargetObjects(dotNetObjects.ToArray());
        }

        public void RefreshObjectTrees()
        {
            if (this.EngineCore == null)
            {
                return;
            }

            List<DotNetObject> objectTrees = DotNetObjectCollector.GetInstance().GetObjectTrees();

            DotNetExplorerEventArgs args = new DotNetExplorerEventArgs();
            args.objectTrees = objectTrees;
            this.EventRefreshObjectTrees?.Invoke(this, args);
        }
    }
    //// End class
}
//// End namespace