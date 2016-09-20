using Ana.Source.Project.ProjectItems;
using Ana.Source.UserSettings;
using Ana.Source.Utils;
using Ana.Source.Utils.MVP;
using System;
using System.Collections.Generic;

namespace Ana.Source.Project
{
    delegate void ProjectExplorerEventHandler(Object Sender, ProjectExplorerEventArgs Args);
    class ProjectExplorerEventArgs : EventArgs
    {
        public ProjectItem ProjectRoot = null;
    }

    interface IProjectExplorerView : IView
    {
        // Methods invoked by the presenter (upstream)
        void EventRefreshProjectStructure(ProjectItem ProjectRoot);
    }

    abstract class IProjectExplorerModel : RepeatedTask, IModel
    {
        public virtual void OnGUIOpen() { }

        // Events triggered by the model (upstream)
        public event ProjectExplorerEventHandler EventRefreshProjectStructure;
        protected virtual void OnEventRefreshProjectStructure(ProjectExplorerEventArgs E)
        {
            EventRefreshProjectStructure?.Invoke(this, E);
        }

        public override void Begin()
        {
            // Temporary workaround until I feel like adding multiple tasks to the RepeatTask class
            UpdateInterval = Math.Min(Settings.GetInstance().GetTableReadInterval(), Settings.GetInstance().GetFreezeInterval());
            base.Begin();
        }

        protected override void Update()
        {
            UpdateInterval = Math.Min(Settings.GetInstance().GetTableReadInterval(), Settings.GetInstance().GetFreezeInterval());
        }

        // Functions invoked by presenter (downstream)
        public abstract ProjectItem GetProjectRoot();
        public abstract void UpdateSelection(IEnumerable<ProjectItem> ProjectItems);
        public abstract void DeleteItems(IEnumerable<ProjectItem> ProjectItems);
        public abstract void AddProjectItem(ProjectItem ProjectItem, ProjectItem Parent);
        public abstract void ActivateProjectItems(IEnumerable<ProjectItem> ProjectItem, Boolean ActivationState);
        public abstract void PerformDefaultAction(ProjectItem ProjectItem);
        public abstract void SetUpdateSet(IEnumerable<ProjectItem> UpdateSet);

        public abstract void UpdateSelectedIndicies(IEnumerable<Int32> Indicies);
    }

    class ProjectExplorerPresenter : Presenter<IProjectExplorerView, IProjectExplorerModel>
    {
        private new IProjectExplorerView view { get; set; }
        private new IProjectExplorerModel model { get; set; }

        public ProjectExplorerPresenter(IProjectExplorerView view, IProjectExplorerModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model
            model.EventRefreshProjectStructure += EventRefreshProjectStructure;

            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void AddNewAddressItem(ProjectItem Parent = null)
        {
            model.AddProjectItem(new AddressItem(), Parent);
        }

        public void AddNewScriptItem(ProjectItem Parent = null)
        {
            model.AddProjectItem(new ScriptItem(), Parent);
        }

        public void AddNewFolderItem(ProjectItem Parent = null)
        {
            model.AddProjectItem(new FolderItem(), Parent);
        }

        public void UpdateSelection(IEnumerable<ProjectItem> ProjectItems)
        {
            model.UpdateSelection(ProjectItems);
        }

        public void DeleteProjectItems(IEnumerable<ProjectItem> ProjectItems)
        {
            model.DeleteItems(ProjectItems);
        }

        public void ActivateProjectItem(ProjectItem ProjectItem, Boolean ActivationState)
        {
            model.ActivateProjectItems(new ProjectItem[] { ProjectItem }, ActivationState);
        }

        public void ActivateProjectItems(IEnumerable<ProjectItem> ProjectItems, Boolean ActivationState)
        {
            model.ActivateProjectItems(ProjectItems, ActivationState);
        }

        public void PerformDefaultAction(ProjectItem ProjectItem)
        {
            if (ProjectItem == null)
                return;

            model.PerformDefaultAction(ProjectItem);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventRefreshProjectStructure(Object Sender, ProjectExplorerEventArgs E)
        {
            view.EventRefreshProjectStructure(E.ProjectRoot);
        }

        #endregion

    } // End class

} // End namespace