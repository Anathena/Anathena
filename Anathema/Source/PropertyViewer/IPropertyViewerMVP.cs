using Anathema.Source.Project.ProjectItems;
using Anathema.Source.Utils;
using Anathema.Source.Utils.MVP;
using Anathema.Source.Utils.Setting;
using System;
using System.Collections.Generic;

namespace Anathema.Source.PropertyViewer
{
    delegate void PropertyViewerEventHandler(Object Sender, PropertyViewerEventArgs Args);
    class PropertyViewerEventArgs : EventArgs
    {
        public Int32 ItemCount = 0;
        public Int32 ClearCacheIndex = -1;
    }

    interface IPropertyViewerView : IView
    {
        // Methods invoked by the presenter (upstream)
        void RefreshStructure();
    }

    abstract class IPropertyViewerModel : RepeatedTask, IModel
    {
        public virtual void OnGUIOpen() { }

        // Events triggered by the model (upstream)
        public event PropertyViewerEventHandler EventRefreshProjectStructure;
        protected virtual void OnEventRefreshProjectStructure(PropertyViewerEventArgs E)
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

        public abstract void AddProjectItem(ProjectItem ProjectItem, ProjectItem Parent);
        public abstract void DeleteProjectItems(IEnumerable<Int32> Items);

        public abstract void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex);
    }

    class PropertyViewerPresenter : Presenter<IPropertyViewerView, IPropertyViewerModel>
    {
        private new IPropertyViewerView View { get; set; }
        private new IPropertyViewerModel Model { get; set; }

        public PropertyViewerPresenter(IPropertyViewerView View, IPropertyViewerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventRefreshProjectStructure += EventRefreshStructure;

            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public ProjectItem GetProjectRoot()
        {
            return Model.GetProjectRoot();
        }

        public void AddNewAddressItem(ProjectItem Parent = null)
        {
            Model.AddProjectItem(new AddressItem(), Parent);
        }

        public void AddNewFolderItem(ProjectItem Parent = null)
        {
            Model.AddProjectItem(new FolderItem(), Parent);
        }

        public void DeleteProjectItems(IEnumerable<Int32> Indicies)
        {
            Model.DeleteProjectItems(Indicies);
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

        private void EventRefreshStructure(Object Sender, PropertyViewerEventArgs E)
        {
            View.RefreshStructure();
        }

        #endregion

    } // End class

} // End namespace