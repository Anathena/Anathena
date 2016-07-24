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
        public abstract void SetAddressItemAt(Int32 Index, AddressItem AddressItem);
        public abstract void SetItemActivation(Int32 Index, Boolean Activated);

        public abstract void ReorderItem(Int32 SourceIndex, Int32 DestinationIndex);
        public abstract void UpdateSelection(IEnumerable<ProjectItem> ProjectItems);

        public abstract void AddProjectItem(ProjectItem ProjectItem, ProjectItem Parent);
        public abstract void DeleteProjectIncicies(IEnumerable<Int32> Indicies);
        public abstract void UpdateSelectedIndicies(IEnumerable<Int32> Indicies);

        public abstract void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex);
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

        public void AddNewFolderItem(ProjectItem Parent = null)
        {
            Model.AddProjectItem(new FolderItem(), Parent);
        }

        public void AddNewDotNetItem(ProjectItem Parent = null)
        {
            Model.AddProjectItem(new DotNetItem(), Parent);
        }

        public void AddNewJavaItem(ProjectItem Parent = null)
        {
            Model.AddProjectItem(new JavaItem(), Parent);
        }

        public void DeleteProjectItems(IEnumerable<Int32> Indicies)
        {
            Model.DeleteProjectIncicies(Indicies);
        }

        public void UpdateSelection(IEnumerable<ProjectItem> ProjectItems)
        {
            Model.UpdateSelection(ProjectItems);
        }

        public void SetAddressFrozen(Int32 Index, Boolean Activated)
        {
            Model.SetItemActivation(Index, Activated);
        }

        public void ReorderItem(Int32 SourceIndex, Int32 DestinationIndex)
        {
            Model.ReorderItem(SourceIndex, DestinationIndex);
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