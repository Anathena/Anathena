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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace Anathema.Source.Project
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    [Obfuscation(ApplyToMembers = false)]
    [Obfuscation(Exclude = true)]
    class ProjectExplorer : IProjectExplorerModel, IProcessObserver
    {
        // Singleton instance of Project Explorer
        private static Lazy<ProjectExplorer> ProjectExplorerInstance = new Lazy<ProjectExplorer>(() => { return new ProjectExplorer(); }, LazyThreadSafetyMode.PublicationOnly);

        private FolderItem ProjectRoot;

        [Obfuscation(Exclude = true)]
        private EngineCore EngineCore;

        [Obfuscation(Exclude = true)]
        private IEnumerable<ProjectItem> UpdateSet;

        [Obfuscation(Exclude = true)]
        private Boolean Changed;

        private ProjectExplorer()
        {
            InitializeProcessObserver();
            ProjectRoot = new FolderItem(String.Empty);

            Begin();
        }

        [Obfuscation(Exclude = true)]
        public override void OnGUIOpen()
        {
            RefreshProjectStructure();
        }

        [Obfuscation(Exclude = true)]
        public static ProjectExplorer GetInstance()
        {
            return ProjectExplorerInstance.Value;
        }

        [Obfuscation(Exclude = true)]
        ~ProjectExplorer()
        {
            TriggerEnd();
        }

        [Obfuscation(Exclude = true)]
        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        [Obfuscation(Exclude = true)]
        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
        }

        [Obfuscation(Exclude = true)]
        public override void ActivateProjectItem(ProjectItem ProjectItem)
        {

        }

        [Obfuscation(Exclude = true)]
        public override void UpdateSelection(IEnumerable<ProjectItem> ProjectItems)
        {
            PropertyViewer.GetInstance().SetTargetObjects(ProjectItems.ToArray());
        }

        [Obfuscation(Exclude = true)]
        public override void SetUpdateSet(IEnumerable<ProjectItem> UpdateSet)
        {
            this.UpdateSet = UpdateSet;
        }

        [Obfuscation(Exclude = true)]
        public override void DeleteItems(IEnumerable<ProjectItem> ProjectItems)
        {
            RefreshProjectStructure();
        }

        [Obfuscation(Exclude = true)]
        public override void AddProjectItem(ProjectItem ProjectItem, ProjectItem Parent = null)
        {
            while (Parent != null && !(Parent is FolderItem))
                Parent = Parent.Parent;

            if (Parent == null)
                Parent = ProjectRoot;

            ProjectItem.Parent = Parent;
            Parent.AddChild(ProjectItem);

            RefreshProjectStructure();

            ProjectExplorer.GetInstance().TableChanged();
        }

        [Obfuscation(Exclude = true)]
        public override void UpdateSelectedIndicies(IEnumerable<Int32> Indicies)
        {
            // TODO: Smart logic for identifying the most common set of properties from the collection of trees
            PropertyViewer.GetInstance().SetTargetObjects(null);
        }

        [Obfuscation(Exclude = true)]
        public override ProjectItem GetProjectRoot()
        {
            return ProjectRoot;
        }

        [Obfuscation(Exclude = true)]
        public void SetProjectItems(FolderItem ProjectRoot)
        {
            this.ProjectRoot = ProjectRoot;
            RefreshProjectStructure();

            ProjectExplorer.GetInstance().TableChanged();
        }

        [Obfuscation(Exclude = true)]
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

        [Obfuscation(Exclude = true)]
        public void RefreshProjectStructure()
        {
            ProjectExplorerEventArgs ProjectExplorerEventArgs = new ProjectExplorerEventArgs();
            ProjectExplorerEventArgs.ProjectRoot = ProjectRoot;
            OnEventRefreshProjectStructure(ProjectExplorerEventArgs);
        }

        [Obfuscation(Exclude = true)]
        public override void Begin()
        {
            base.Begin();
        }

        [Obfuscation(Exclude = true)]
        protected override void Update()
        {

        }

        [Obfuscation(Exclude = true)]
        protected override void End() { }

        [Obfuscation(Exclude = true)]
        public void TableChanged()
        {
            Changed = true;
            Main.GetInstance().UpdateHasChanges(Changed);
        }

        [Obfuscation(Exclude = true)]
        public void TableSaved()
        {
            Changed = false;
            Main.GetInstance().UpdateHasChanges(Changed);
        }

        [Obfuscation(Exclude = true)]
        public Boolean HasChanges()
        {
            return Changed;
        }

        [Obfuscation(Exclude = true)]
        public Boolean SaveTable(String Path)
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

            TableSaved();
            return true;
        }

        [Obfuscation(Exclude = true)]
        public Boolean OpenTable(String Path)
        {
            if (Path == null || Path == String.Empty)
                return false;

            // DELETE XML SERIALIZER EVENTUALLY (Legacy loading scheme, switched to JSON)
            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    DataContractSerializer Serializer = new DataContractSerializer(typeof(FolderItem));
                    FolderItem ProjectRoot = (FolderItem)Serializer.ReadObject(FileStream);

                    SetProjectItems(ProjectRoot);
                }
            }
            catch
            {
                try
                {
                    using (FileStream FileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                    {
                        DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(FolderItem));
                        FolderItem ProjectRoot = (FolderItem)Serializer.ReadObject(FileStream);

                        SetProjectItems(ProjectRoot);
                    }
                }
                catch
                {
                    return false;
                }
            }

            TableSaved();
            return true;
        }

        [Obfuscation(Exclude = true)]
        public Boolean MergeTable(String Path)
        {
            // TODO: Re-implement this shit
            throw new NotImplementedException();

            if (Path == null || Path == String.Empty)
                return false;

            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    DataContractSerializer Serializer = new DataContractSerializer(typeof(FolderItem));
                    FolderItem ProjectRoot = (FolderItem)Serializer.ReadObject(FileStream);

                    SetProjectItems(ProjectRoot);
                }
            }
            catch
            {
                return false;
            }

            TableChanged();
            return true;
        }

    } // End class

} // End namespace