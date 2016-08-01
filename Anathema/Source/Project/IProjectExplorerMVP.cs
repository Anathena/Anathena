using Anathema.Source.Project.ProjectItems;
using Anathema.Source.Utils;
using Anathema.Source.Utils.MVP;
using Anathema.Source.Utils.Setting;
using System;
using System.Collections.Generic;

namespace Anathema.Source.Project
{
    delegate void ProjectExplorerEventHandler(Object Sender, ProjectExplorerEventArgs Args);
    class ProjectExplorerEventArgs : EventArgs
    {
        public ProjectItem ProjectRoot = null;
    }

    interface IProjectExplorerView : IView
    {
        // Methods invoked by the presenter (upstream)
        void RefreshStructure(ProjectItem ProjectRoot);
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
        public abstract void SetUpdateSet(IEnumerable<ProjectItem> UpdateSet);

        public abstract void UpdateSelectedIndicies(IEnumerable<Int32> Indicies);
    }

    class ProjectExplorerPresenter : Presenter<IProjectExplorerView, IProjectExplorerModel>
    {
        private new IProjectExplorerView View { get; set; }
        private new IProjectExplorerModel Model { get; set; }

        public ProjectExplorerPresenter(IProjectExplorerView View, IProjectExplorerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventRefreshProjectStructure += EventRefreshStructure;

            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void AddNewAddressItem(ProjectItem Parent = null)
        {
            Model.AddProjectItem(new AddressItem(), Parent);
        }

        public void AddNewScriptItem(ProjectItem Parent = null)
        {
            Model.AddProjectItem(new ScriptItem(), Parent);
        }

        public void AddNewFolderItem(ProjectItem Parent = null)
        {
            Model.AddProjectItem(new FolderItem(), Parent);
        }

        public void UpdateSelection(IEnumerable<ProjectItem> ProjectItems)
        {
            Model.UpdateSelection(ProjectItems);
        }

        public void DeleteProjectItems(IEnumerable<ProjectItem> ProjectItems)
        {
            Model.DeleteItems(ProjectItems);
        }

        public void ActivateProjectItem(ProjectItem ProjectItem, Boolean ActivationState)
        {
            Model.ActivateProjectItems(new ProjectItem[] { ProjectItem }, ActivationState);
        }

        public void ActivateProjectItems(IEnumerable<ProjectItem> ProjectItems, Boolean ActivationState)
        {
            Model.ActivateProjectItems(ProjectItems, ActivationState);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventRefreshStructure(Object Sender, ProjectExplorerEventArgs E)
        {
            View.RefreshStructure(E.ProjectRoot);
        }

        #endregion

    } // End class

} // End namespace