using Anathema.Source.Project.ProjectItems;
using Anathema.Source.Utils;
using Anathema.Source.Utils.MVP;
using Anathema.Source.Utils.Setting;
using Anathema.Source.Utils.Validation;
using System;
using System.Collections.Generic;

namespace Anathema.Source.Project
{
    delegate void ScratchPadEventHandler(Object Sender, ScratchPadEventArgs Args);
    class ScratchPadEventArgs : EventArgs
    {
        public Int32 ItemCount = 0;
        public Int32 ClearCacheIndex = -1;
    }

    interface IScratchPadView : IView
    {
        // Methods invoked by the presenter (upstream)
        void ReadValues();
        void RefreshStructure();
    }

    abstract class IScratchPadModel : RepeatedTask, IModel
    {
        public virtual void OnGUIOpen() { }

        // Events triggered by the model (upstream)
        public event ScratchPadEventHandler EventReadValues;
        protected virtual void OnEventReadValues(ScratchPadEventArgs E)
        {
            EventReadValues?.Invoke(this, E);
        }

        public event ScratchPadEventHandler EventRefreshStructure;
        protected virtual void OnEventRefreshStructure(ScratchPadEventArgs E)
        {
            EventRefreshStructure?.Invoke(this, E);
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
        public abstract ProjectItem GetProjectItemAt(Int32 Index);
        public abstract Int32 GetItemCount();
        public abstract void SetAddressItemAt(Int32 Index, AddressItem AddressItem);
        public abstract void SetItemActivation(Int32 Index, Boolean Activated);

        public abstract void ReorderItem(Int32 SourceIndex, Int32 DestinationIndex);

        public abstract void AddProjectItem(ProjectItem ProjectItem);
        public abstract void AddAddressItem(String BaseAddress, Type ElementType, String Description, IEnumerable<Int32> Offsets = null, Boolean IsHex = false, String Value = null);
        public abstract void AddFolderItem(String Description);
        public abstract void DeleteTableItems(IEnumerable<Int32> Items);

        public abstract void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex);
    }

    class ScratchPadPresenter : Presenter<IScratchPadView, IScratchPadModel>
    {
        private new IScratchPadView View { get; set; }
        private new IScratchPadModel Model { get; set; }

        public ScratchPadPresenter(IScratchPadView View, IScratchPadModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventReadValues += EventReadValues;
            Model.EventRefreshStructure += EventRefreshStructure;

            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            Model.UpdateReadBounds(StartReadIndex, EndReadIndex);
        }

        public ProjectItem GetProjectItemAt(Int32 Index)
        {
            return Model.GetProjectItemAt(Index);
        }

        public Int32 GetItemCount()
        {
            return Model.GetItemCount();
        }

        public void AddNewAddressItem()
        {
            Model.AddAddressItem(Conversions.ToAddress(IntPtr.Zero), typeof(Int32), "New Address");
        }

        public void AddNewFolderItem()
        {
            Model.AddFolderItem("New Folder");
        }

        public void DeleteTableItems(IEnumerable<Int32> Indicies)
        {
            Model.DeleteTableItems(Indicies);
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

        private void EventReadValues(Object Sender, ScratchPadEventArgs E)
        {
            View.ReadValues();
        }

        private void EventRefreshStructure(Object Sender, ScratchPadEventArgs E)
        {
            View.RefreshStructure();
        }

        #endregion

    } // End class

} // End namespace