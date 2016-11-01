namespace Ana.Source.Project
{
    using ProjectItems;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization.Json;
    using System.Threading;
    using Utils;

    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    internal class ProjectExplorerDeprecated : RepeatedTask
    {
        /// <summary>
        /// Singleton instance of the <see cref="ProjectExplorerDeprecated"/> class
        /// </summary>
        private static Lazy<ProjectExplorerDeprecated> projectExplorerInstance = new Lazy<ProjectExplorerDeprecated>(
                () => { return new ProjectExplorerDeprecated(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        private ProjectExplorerDeprecated()
        {
            this.ProjectRoot = new FolderItem(String.Empty);
            this.Begin();
        }

        ~ProjectExplorerDeprecated()
        {
            this.End();
        }

        private FolderItem ProjectRoot { get; set; }

        private IEnumerable<ProjectItem> UpdateSet { get; set; }

        private Boolean Changed { get; set; }

        public static ProjectExplorerDeprecated GetInstance()
        {
            return ProjectExplorerDeprecated.projectExplorerInstance.Value;
        }

        public void SetUpdateSet(IEnumerable<ProjectItem> updateSet)
        {
            this.UpdateSet = updateSet;
        }

        public void ActivateProjectItems(IEnumerable<ProjectItem> projectItems, Boolean isActivated)
        {
            foreach (ProjectItem projectItem in projectItems)
            {
                projectItem.IsActivated = isActivated;
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

            ProjectExplorerDeprecated.GetInstance().ProjectChanged();
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
            ProjectExplorerDeprecated.GetInstance().ProjectChanged();
        }

        public void RefreshProjectStructure()
        {
            this.ProjectRoot.BuildParents();
        }

        public override void Begin()
        {
            base.Begin();
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
                    serializer.WriteObject(fileStream, ProjectExplorerDeprecated.GetInstance().GetProjectRoot());
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

        protected override void OnUpdate()
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

        protected override void OnEnd()
        {
            base.OnEnd();
        }

        private void ImportProjectItems(FolderItem importedProjectRoot)
        {
            foreach (ProjectItem item in importedProjectRoot.Children)
            {
                this.ProjectRoot.AddChild(item);
            }

            this.RefreshProjectStructure();

            ProjectExplorerDeprecated.GetInstance().ProjectChanged();
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