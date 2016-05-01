using Anathema.User.UserSettings;
using Anathema.Utils;
using Anathema.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anathema.User.UserAddressTable
{
    delegate void AddressTableEventHandler(Object Sender, AddressTableEventArgs Args);
    class AddressTableEventArgs : EventArgs
    {
        public Int32 ItemCount = 0;
        public Int32 ClearCacheIndex = -1;
    }

    interface IAddressTableView : IView
    {
        // Methods invoked by the presenter (upstream)
        void ReadValues();
        void UpdateAddressTableItemCount(Int32 ItemCount);
    }

    abstract class IAddressTableModel : RepeatedTask, IModel
    {
        // Events triggered by the model (upstream)
        public event AddressTableEventHandler EventReadValues;
        protected virtual void OnEventReadValues(AddressTableEventArgs E)
        {
            EventReadValues?.Invoke(this, E);
        }

        public event AddressTableEventHandler EventUpdateAddressTableItemCount;
        protected virtual void OnEventUpdateAddressTableItemCount(AddressTableEventArgs E)
        {
            EventUpdateAddressTableItemCount?.Invoke(this, E);
        }

        public override void Begin()
        {
            // Temporary workaround until I feel like adding multiple tasks to the RepeatTask class
            WaitTime = Math.Min(Settings.GetInstance().GetTableReadInterval(), Settings.GetInstance().GetFreezeInterval());
            base.Begin();
        }

        protected override void Update()
        {
            WaitTime = Math.Min(Settings.GetInstance().GetTableReadInterval(), Settings.GetInstance().GetFreezeInterval());
        }

        // Functions invoked by presenter (downstream)

        public abstract AddressItem GetAddressItemAt(Int32 Index);
        public abstract void SetAddressItemAt(Int32 Index, AddressItem AddressItem);
        public abstract void SetAddressFrozen(Int32 Index, Boolean Activated);

        public abstract void ReorderItem(Int32 SourceIndex, Int32 DestinationIndex);

        public abstract void AddAddressItem(IntPtr BaseAddress, Type ElementType, String Description, IEnumerable<Int32> Offsets = null, Boolean IsHex = false, String Value = null);
        public abstract void AddAddressItem(AddressItem AddressItem);
        public abstract void DeleteTableItems(IEnumerable<Int32> Items);

        public abstract void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex);
    }

    class AddressTablePresenter : Presenter<IAddressTableView, IAddressTableModel>
    {
        protected new IAddressTableView View { get; set; }
        protected new IAddressTableModel Model { get; set; }

        public AddressTablePresenter(IAddressTableView View, IAddressTableModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventReadValues += EventReadValues;
            Model.EventUpdateAddressTableItemCount += EventUpdateAddressTableItemCount;
        }

        #region Method definitions called by the view (downstream)

        public void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            Model.UpdateReadBounds(StartReadIndex, EndReadIndex);
        }

        public AddressItem GetAddressItemAt(Int32 Index)
        {
            return Model.GetAddressItemAt(Index);
        }

        public void AddNewAddressItem()
        {
            Model.AddAddressItem(IntPtr.Zero, typeof(Int32), "No Description");
        }

        public void DeleteTableItems(IEnumerable<Int32> Indicies)
        {
            Model.DeleteTableItems(Indicies);
        }

        public void SetAddressFrozen(Int32 Index, Boolean Activated)
        {
            Model.SetAddressFrozen(Index, Activated);
        }

        public void ReorderItem(Int32 SourceIndex, Int32 DestinationIndex)
        {
            Model.ReorderItem(SourceIndex, DestinationIndex);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventReadValues(Object Sender, AddressTableEventArgs E)
        {
            Task.Run(() => { View.ReadValues(); });
        }

        private void EventUpdateAddressTableItemCount(Object Sender, AddressTableEventArgs E)
        {
            Task.Run(() => { View.UpdateAddressTableItemCount(E.ItemCount); });
        }

        #endregion

    } // End class

} // End namespace