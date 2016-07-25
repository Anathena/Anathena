using Anathema.Source.Controller;
using Anathema.Source.Engine;
using Anathema.Source.Engine.Processes;
using Anathema.Source.Project.ProjectItems;
using Anathema.Source.PropertyEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace Anathema.Source.Project
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    [Obfuscation(ApplyToMembers = true)]
    [Obfuscation(Exclude = true)]
    class ProjectExplorer : IProjectExplorerModel, IProcessObserver
    {
        // Singleton instance of Project Explorer
        private static Lazy<ProjectExplorer> ProjectExplorerInstance = new Lazy<ProjectExplorer>(() => { return new ProjectExplorer(); }, LazyThreadSafetyMode.PublicationOnly);

        private EngineCore EngineCore;

        private FolderItem ProjectRoot;
        private IEnumerable<ProjectItem> UpdateSet;
        private Boolean Changed;

        private ProjectExplorer()
        {
            InitializeProcessObserver();
            ProjectRoot = new FolderItem(String.Empty);

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

        public override void ActivateProjectItem(ProjectItem ProjectItem)
        {

        }

        public override void UpdateSelection(IEnumerable<ProjectItem> ProjectItems)
        {
            PropertyViewer.GetInstance().SetTargetObjects(ProjectItems.ToArray());
        }

        public override void SetUpdateSet(IEnumerable<ProjectItem> UpdateSet)
        {
            this.UpdateSet = UpdateSet;
        }

        public override void DeleteItems(IEnumerable<ProjectItem> ProjectItems)
        {
            RefreshProjectStructure();
        }

        public override void AddProjectItem(ProjectItem ProjectItem, ProjectItem Parent = null)
        {
            while (Parent != null && !(Parent is FolderItem))
                Parent = Parent.Parent;

            if (Parent == null)
                Parent = ProjectRoot;

            ProjectItem.Parent = Parent;
            Parent.AddChild(ProjectItem);

            RefreshProjectStructure();

            ProjectExplorer.GetInstance().ProjectChanged();
        }

        public override void UpdateSelectedIndicies(IEnumerable<Int32> Indicies)
        {
            // TODO: Smart logic for identifying the most common set of properties from the collection of trees
            PropertyViewer.GetInstance().SetTargetObjects(null);
        }

        public override ProjectItem GetProjectRoot()
        {
            return ProjectRoot;
        }

        public void SetProjectItems(FolderItem ProjectRoot)
        {
            this.ProjectRoot = ProjectRoot;
            RefreshProjectStructure();

            ProjectExplorer.GetInstance().ProjectChanged();
        }

        private void ImportProjectItems(FolderItem ImportedProjectRoot)
        {
            foreach (ProjectItem Item in ImportedProjectRoot)
                ProjectRoot.AddChild(Item);

            RefreshProjectStructure();

            ProjectExplorer.GetInstance().ProjectChanged();
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

        public void RefreshProjectStructure()
        {
            ProjectExplorerEventArgs ProjectExplorerEventArgs = new ProjectExplorerEventArgs();
            ProjectExplorerEventArgs.ProjectRoot = ProjectRoot;
            OnEventRefreshProjectStructure(ProjectExplorerEventArgs);
        }

        public override void Begin()
        {
            base.Begin();
        }

        private IEnumerable<ProjectItem> CreateUpdateSetDEPRECATED(ProjectItem ProjectItem, List<ProjectItem> CurrentSet = null)
        {
            if (ProjectItem == null)
                return CurrentSet;

            if (CurrentSet == null)
                CurrentSet = new List<ProjectItem>();

            foreach (ProjectItem Child in ProjectItem)
            {
                CurrentSet.Add(Child);
                CreateUpdateSetDEPRECATED(Child, CurrentSet);
            }

            return CurrentSet;
        }

        protected override void Update()
        {
            UpdateSet = CreateUpdateSetDEPRECATED(ProjectRoot);

            if (UpdateSet == null)
                return;

            foreach (ProjectItem ProjectItem in UpdateSet)
                ProjectItem.Update(EngineCore);
        }

        protected override void End() { }

        public void ProjectChanged()
        {
            Changed = true;
            Main.GetInstance().UpdateHasChanges(Changed);
        }

        public void ProjectSaved()
        {
            Changed = false;
            Main.GetInstance().UpdateHasChanges(Changed);
        }

        public Boolean HasChanges()
        {
            return Changed;
        }

        public Boolean SaveProject(String Path)
        {
            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Create, FileAccess.Write))
                {
                    DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(FolderItem));

                    Serializer.WriteObject(FileStream, ProjectExplorer.GetInstance().GetProjectRoot());
                }
            }
            catch
            {
                return false;
            }

            ProjectSaved();
            return true;
        }

        public Boolean OpenProject(String Path)
        {
            if (Path == null || Path == String.Empty)
                return false;

            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(FolderItem));
                    FolderItem ProjectRoot = Serializer.ReadObject(FileStream) as FolderItem;

                    SetProjectItems(ProjectRoot);
                }
            }
            catch
            {
                return false;
            }

            ProjectSaved();
            return true;
        }

        public Boolean ImportProject(String Path)
        {
            if (Path == null || Path == String.Empty)
                return false;

            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(FolderItem));
                    FolderItem ImportedProjectRoot = Serializer.ReadObject(FileStream) as FolderItem;

                    ImportProjectItems(ImportedProjectRoot);
                }
            }
            catch
            {
                return false;
            }

            ProjectChanged();
            return true;
        }

    } // End class

} // End namespace