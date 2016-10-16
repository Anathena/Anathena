namespace Ana.Source.Project
{
    using ProjectItems;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization.Json;
    using System.Threading;

    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    internal class ProjectExplorer
    {
        /// <summary>
        /// Singleton instance of the <see cref="ProjectExplorer"/> class
        /// </summary>
        private static Lazy<ProjectExplorer> projectExplorerInstance = new Lazy<ProjectExplorer>(
                () => { return new ProjectExplorer(); },
                LazyThreadSafetyMode.PublicationOnly);

        private ProjectExplorer()
        {
            this.ProjectRoot = new FolderItem(String.Empty);
            this.Begin();
        }

        ~ProjectExplorer()
        {
            //// TriggerEnd();
        }

        private FolderItem ProjectRoot { get; set; }

        private IEnumerable<ProjectItem> UpdateSet { get; set; }

        private Boolean Changed { get; set; }

        public static ProjectExplorer GetInstance()
        {
            return ProjectExplorer.projectExplorerInstance.Value;
        }

        public void OnGUIOpen()
        {
            this.RefreshProjectStructure();
        }

        public void UpdateSelection(IEnumerable<ProjectItem> projectItems)
        {
            //// PropertyViewer.GetInstance().SetTargetObjects(projectItems.ToArray());
        }

        public void SetUpdateSet(IEnumerable<ProjectItem> updateSet)
        {
            this.UpdateSet = updateSet;
        }

        public void ActivateProjectItems(IEnumerable<ProjectItem> projectItems, Boolean activationState)
        {
            foreach (ProjectItem projectItem in projectItems)
            {
                projectItem.SetActivationState(activationState);
            }
        }

        public void PerformDefaultAction(ProjectItem projectItem)
        {
            if (projectItem is ScriptItem)
            {
                //// ScriptEditor ScriptEditor = new ScriptEditor();
                //// ScriptEditor.EditValue(null, (projectItem as ScriptItem).LuaScript);
            }
        }

        public void DeleteItems(IEnumerable<ProjectItem> projectItems)
        {
            this.ProjectRoot.Delete(projectItems);
            this.RefreshProjectStructure();
        }

        public void AddProjectItem(ProjectItem projectItem, ProjectItem parent = null)
        {
            while (parent != null && !(parent is FolderItem))
            {
                parent = parent.Parent;
            }

            if (parent == null)
            {
                parent = this.ProjectRoot;
            }

            projectItem.Parent = parent;
            parent.AddChild(projectItem);

            this.RefreshProjectStructure();

            ProjectExplorer.GetInstance().ProjectChanged();
        }

        public void UpdateSelectedIndicies(IEnumerable<Int32> indicies)
        {
            //// PropertyViewer.GetInstance().SetTargetObjects(null);
        }

        public ProjectItem GetProjectRoot()
        {
            return this.ProjectRoot;
        }

        public void SetProjectItems(FolderItem projectRoot)
        {
            this.ProjectRoot = projectRoot;
            this.RefreshProjectStructure();
            ProjectExplorer.GetInstance().ProjectChanged();
        }

        public void RefreshProjectStructure()
        {
            this.ProjectRoot.BuildParents();
        }

        public void Begin()
        {
        }

        public void ProjectChanged()
        {
            this.Changed = true;
            //// Main.GetInstance().UpdateHasChanges(this.Changed);
        }

        public void ProjectSaved()
        {
            this.Changed = false;
            //// Main.GetInstance().UpdateHasChanges(this.Changed);
        }

        public Boolean HasChanges()
        {
            return this.Changed;
        }

        public Boolean SaveProject(String path)
        {
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FolderItem));
                    serializer.WriteObject(fileStream, ProjectExplorer.GetInstance().GetProjectRoot());
                }
            }
            catch
            {
                return false;
            }

            this.ProjectSaved();
            return true;
        }

        public Boolean OpenProject(String path)
        {
            if (path == null || path == String.Empty)
            {
                return false;
            }

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FolderItem));
                    FolderItem projectRoot = serializer.ReadObject(fileStream) as FolderItem;
                    this.SetProjectItems(projectRoot);
                }
            }
            catch
            {
                return false;
            }

            this.ProjectSaved();
            return true;
        }

        public Boolean ImportProject(String path)
        {
            if (path == null || path == String.Empty)
            {
                return false;
            }

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FolderItem));
                    FolderItem importedProjectRoot = serializer.ReadObject(fileStream) as FolderItem;

                    this.ImportProjectItems(importedProjectRoot);
                }
            }
            catch
            {
                return false;
            }

            this.ProjectChanged();
            return true;
        }

        protected void Update()
        {
            this.UpdateSet = this.CreateUpdateSet_TODO_REPLACE_ME(this.ProjectRoot);

            if (this.UpdateSet == null)
            {
                return;
            }

            foreach (ProjectItem projectItem in this.UpdateSet)
            {
                projectItem.Update();
            }
        }

        protected void End()
        {
        }

        private void ImportProjectItems(FolderItem importedProjectRoot)
        {
            foreach (ProjectItem item in importedProjectRoot.Children)
            {
                this.ProjectRoot.AddChild(item);
            }

            this.RefreshProjectStructure();

            ProjectExplorer.GetInstance().ProjectChanged();
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

            foreach (ProjectItem child in projectItem.Children)
            {
                currentSet.Add(child);
                this.CreateUpdateSet_TODO_REPLACE_ME(child, currentSet);
            }

            return currentSet;
        }
    }
    //// End class
}
//// End namespace