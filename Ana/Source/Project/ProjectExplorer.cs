using Ana.Source.Engine;
using Ana.Source.Project.ProjectItems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace Ana.Source.Project
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    internal class ProjectExplorer
    {
        // Singleton instance of Project Explorer
        private static Lazy<ProjectExplorer> ProjectExplorerInstance = new Lazy<ProjectExplorer>(() => { return new ProjectExplorer(); }, LazyThreadSafetyMode.PublicationOnly);

        private EngineCore EngineCore;

        private FolderItem ProjectRoot;
        private IEnumerable<ProjectItem> UpdateSet;
        private Boolean Changed;

        private ProjectExplorer()
        {
            ProjectRoot = new FolderItem(String.Empty);

            Begin();
        }

        public void OnGUIOpen()
        {
            RefreshProjectStructure();
        }

        public static ProjectExplorer GetInstance()
        {
            return ProjectExplorerInstance.Value;
        }

        ~ProjectExplorer()
        {
            // TriggerEnd();
        }

        public void UpdateSelection(IEnumerable<ProjectItem> ProjectItems)
        {
            // PropertyViewer.GetInstance().SetTargetObjects(ProjectItems.ToArray());
        }

        public void SetUpdateSet(IEnumerable<ProjectItem> UpdateSet)
        {
            this.UpdateSet = UpdateSet;
        }

        public void ActivateProjectItems(IEnumerable<ProjectItem> ProjectItems, Boolean ActivationState)
        {
            foreach (ProjectItem ProjectItem in ProjectItems)
                ProjectItem.SetActivationState(ActivationState);
        }

        public void PerformDefaultAction(ProjectItem ProjectItem)
        {
            if (ProjectItem is ScriptItem)
            {
                // ScriptEditor ScriptEditor = new ScriptEditor();
                // ScriptEditor.EditValue(null, (ProjectItem as ScriptItem).LuaScript);
            }
        }

        public void DeleteItems(IEnumerable<ProjectItem> ProjectItems)
        {
            ProjectRoot.Delete(ProjectItems);

            RefreshProjectStructure();
        }

        public void AddProjectItem(ProjectItem ProjectItem, ProjectItem Parent = null)
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

        public void UpdateSelectedIndicies(IEnumerable<Int32> Indicies)
        {
            // PropertyViewer.GetInstance().SetTargetObjects(null);
        }

        public ProjectItem GetProjectRoot()
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
            foreach (ProjectItem Item in ImportedProjectRoot.Children)
                ProjectRoot.AddChild(Item);

            RefreshProjectStructure();

            ProjectExplorer.GetInstance().ProjectChanged();
        }

        public void RefreshProjectStructure()
        {
            ProjectRoot.BuildParents();
        }

        public void Begin()
        {

        }

        /// <summary>
        /// Eventually we want the update set just to be the visible nodes in the display, not every single node
        /// </summary>
        /// <param name="ProjectItem"></param>
        /// <param name="CurrentSet"></param>
        /// <returns></returns>
        private IEnumerable<ProjectItem> CreateUpdateSet_TODO_REPLACE_ME(ProjectItem ProjectItem, List<ProjectItem> CurrentSet = null)
        {
            if (ProjectItem == null)
                return CurrentSet;

            if (CurrentSet == null)
                CurrentSet = new List<ProjectItem>();

            foreach (ProjectItem Child in ProjectItem.Children)
            {
                CurrentSet.Add(Child);
                CreateUpdateSet_TODO_REPLACE_ME(Child, CurrentSet);
            }

            return CurrentSet;
        }

        protected void Update()
        {
            UpdateSet = CreateUpdateSet_TODO_REPLACE_ME(ProjectRoot);

            if (UpdateSet == null)
                return;

            foreach (ProjectItem ProjectItem in UpdateSet)
                ProjectItem.Update();
        }

        protected void End()
        {

        }

        public void ProjectChanged()
        {
            Changed = true;
            // Main.GetInstance().UpdateHasChanges(Changed);
        }

        public void ProjectSaved()
        {
            Changed = false;
            // Main.GetInstance().UpdateHasChanges(Changed);
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