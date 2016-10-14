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

        public void PerformDefaultAction(ProjectItem projectItem)
        {
            if (projectItem is ScriptItem)
            {
                // ScriptEditor ScriptEditor = new ScriptEditor();
                // ScriptEditor.EditValue(null, (projectItem as ScriptItem).LuaScript);
            }
        }

        public void DeleteItems(IEnumerable<ProjectItem> projectItems)
        {
            ProjectRoot.Delete(projectItems);

            RefreshProjectStructure();
        }

        public void AddProjectItem(ProjectItem projectItem, ProjectItem parent = null)
        {
            while (parent != null && !(parent is FolderItem))
            {
                parent = parent.Parent;
            }

            if (parent == null)
            {
                parent = ProjectRoot;
            }

            projectItem.Parent = parent;
            parent.AddChild(projectItem);

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
            {
                ProjectRoot.AddChild(Item);
            }

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
        /// <param name="projectItem"></param>
        /// <param name="currentSet"></param>
        /// <returns></returns>
        private IEnumerable<ProjectItem> CreateUpdateSet_TODO_REPLACE_ME(ProjectItem projectItem, List<ProjectItem> currentSet = null)
        {
            if (projectItem == null)
            {
                return currentSet;
            }

            if (currentSet == null)
            {
                currentSet = new List<ProjectItem>();
            }

            foreach (ProjectItem Child in projectItem.Children)
            {
                currentSet.Add(Child);
                CreateUpdateSet_TODO_REPLACE_ME(Child, currentSet);
            }

            return currentSet;
        }

        protected void Update()
        {
            UpdateSet = CreateUpdateSet_TODO_REPLACE_ME(ProjectRoot);

            if (UpdateSet == null)
            {
                return;
            }

            foreach (ProjectItem ProjectItem in UpdateSet)
            {
                ProjectItem.Update();
            }
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