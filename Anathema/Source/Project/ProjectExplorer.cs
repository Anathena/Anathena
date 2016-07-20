using Anathema.Source.Engine;
using Anathema.Source.Engine.Processes;
using Anathema.Source.Project.Deprecating;
using Anathema.Source.Project.ProjectItems;
using Anathema.Source.PropertyEditor;
using System;
using System.Collections.Generic;

namespace Anathema.Source.Project
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class ProjectExplorer : IProjectExplorerModel, IProcessObserver
    {
        // Singleton instance of project explorer
        private static Lazy<ProjectExplorer> ProjectExplorerInstance = new Lazy<ProjectExplorer>(() => { return new ProjectExplorer(); });

        private EngineCore EngineCore;
        private ProjectItem ProjectRoot;

        private Int32 VisibleIndexStart;
        private Int32 VisibleIndexEnd;

        private ProjectExplorer()
        {
            InitializeProcessObserver();
            ProjectRoot = new ProjectItem();

            Begin();
        }

        public override void OnGUIOpen()
        {
            RefreshProjectStructure();
        }

        public static ProjectExplorer GetInstance()
        {
            return ProjectExplorerInstance.Value;
        }

        ~ProjectExplorer()
        {
            TriggerEnd();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
        }

        public override void UpdateReadBounds(Int32 VisibleIndexStart, Int32 VisibleIndexEnd)
        {
            this.VisibleIndexStart = VisibleIndexStart;
            this.VisibleIndexEnd = VisibleIndexEnd;
        }

        public override void SetItemActivation(Int32 Index, Boolean Activated)
        {
            /*
            if (EngineCore == null)
            {
                // Allow disabling even if there is no valid process
                if (!Activated)
                    ProjectItems[Index].SetActivationState(Activated);

                return;
            }

            if (Activated)
            {
                ProjectItem ProjectItem = ProjectItems[Index];

                if (ProjectItem is AddressItem)
                {
                    AddressItem AddressItem = (AddressItem)ProjectItem;
                    Boolean ReadSuccess;
                    AddressItem.ResolveAddress(EngineCore);
                    AddressItem.Value = EngineCore.Memory.Read(AddressItem.ElementType, AddressItem.EffectiveAddress, out ReadSuccess);
                }
            }
            ProjectItems[Index].SetActivationState(Activated);
            */
        }

        public override void AddProjectItem(ProjectItem ProjectItem, ProjectItem Parent = null)
        {
            if (Parent == null)
                Parent = ProjectRoot;

            ProjectItem.Parent = Parent;
            Parent.AddChild(ProjectItem);

            RefreshProjectStructure();

            TableManager.GetInstance().TableChanged();
        }

        public override void DeleteProjectIncicies(IEnumerable<Int32> Indicies)
        {
            // foreach (Int32 Index in Indicies.OrderByDescending(X => X))
            //    ProjectItems.RemoveAt(Index);

            RefreshProjectStructure();

            TableManager.GetInstance().TableChanged();
        }

        public override void UpdateSelectedIndicies(IEnumerable<Int32> Indicies)
        {
            // TODO: Smart logic for identifying the most common set of properties from the collection of trees
            PropertyViewer.GetInstance().SetProperties(null);
        }

        public override ProjectItem GetProjectRoot()
        {
            return ProjectRoot;
        }

        public void SetProjectItems(ProjectItem ProjectRoot)
        {
            this.ProjectRoot = ProjectRoot;
            RefreshProjectStructure();

            TableManager.GetInstance().TableChanged();
        }

        public override void SetAddressItemAt(Int32 Index, AddressItem AddressItem)
        {
            // TODO: FIX
            // ProjectItems[Index] = AddressItem;

            // Force update of value, regardless if frozen or not
            AddressItem.ForceUpdateValue(AddressItem.Value);

            // Write change to memory
            if (AddressItem.Value != null)
            {
                AddressItem.ResolveAddress(EngineCore);
                if (EngineCore != null)
                    EngineCore.Memory.Write(AddressItem.ElementType, AddressItem.EffectiveAddress, AddressItem.Value);
            }

            RefreshProjectStructure();

            TableManager.GetInstance().TableChanged();
        }

        public override void ReorderItem(Int32 SourceIndex, Int32 DestinationIndex)
        {
            /*
            // Bounds checking
            if (SourceIndex < 0 || SourceIndex > ProjectItems.Count)
                return;

            // If an item is being removed before the destination, the destination must be shifted
            if (DestinationIndex > SourceIndex)
                DestinationIndex--;

            // Bounds checking
            if (DestinationIndex < 0 || DestinationIndex > ProjectItems.Count)
                return;

            ProjectItem Item = ProjectItems[SourceIndex];
            ProjectItems.RemoveAt(SourceIndex);
            ProjectItems.Insert(DestinationIndex, Item);
            UpdateItemCount();

            TableManager.GetInstance().TableChanged();
            */
        }

        private void RefreshProjectStructure()
        {
            ProjectExplorerEventArgs ProjectExplorerEventArgs = new ProjectExplorerEventArgs();
            OnEventRefreshProjectStructure(ProjectExplorerEventArgs);
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            // TODO: Offloading this shit elsewhere
            /*
            // Freeze addresses
            foreach (ProjectItem ProjectItem in ProjectItems)
            {
                if (ProjectItem is AddressItem)
                {
                    AddressItem AddressItem = (AddressItem)ProjectItem;
                    if (AddressItem.GetActivationState())
                    {
                        AddressItem.ResolveAddress(EngineCore);

                        if (EngineCore != null && AddressItem.Value != null)
                            EngineCore.Memory.Write(AddressItem.ElementType, AddressItem.EffectiveAddress, AddressItem.Value);
                    }
                }
            }

            for (Int32 Index = VisibleIndexStart; Index < VisibleIndexEnd; Index++)
            {
                if (Index < 0 || Index >= ProjectItems.Count)
                    continue;

                ProjectItem ProjectItem = ProjectItems[Index];

                if (ProjectItem is AddressItem)
                {
                    AddressItem AddressItem = (AddressItem)ProjectItem;
                    Boolean ReadSuccess;
                    AddressItem.ResolveAddress(EngineCore);

                    if (EngineCore != null)
                        AddressItem.Value = EngineCore.Memory.Read(AddressItem.ElementType, AddressItem.EffectiveAddress, out ReadSuccess);
                }
            }

            if (ProjectItems.Count != 0)
                OnEventReadValues(new ProjectExplorerEventArgs());
                */
        }

        protected override void End() { }

    } // End class

} // End namespace