using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
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
            if (EventReadValues != null) EventReadValues(this, E);
        }

        public event AddressTableEventHandler EventClearAddressCacheItem;
        protected virtual void OnEventClearAddressCacheItem(AddressTableEventArgs E)
        {
            EventClearAddressCacheItem(this, E);
        }

        public event AddressTableEventHandler EventClearAddressCache;
        protected virtual void OnEventClearAddressCache(AddressTableEventArgs E)
        {
            EventClearAddressCache(this, E);
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
        
        public abstract void ForceRefresh();
        public abstract void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex);
    }

    class AddressTablePresenter : Presenter<IAddressTableView, IAddressTableModel>
    {
        protected new IAddressTableView View { get; set; }
        protected new IAddressTableModel Model { get; set; }

        private ListViewCache AddressTableCache;

        private const Int32 FreezeCheckBoxIndex = 0;
        private const Int32 DescriptionIndex = 1;
        private const Int32 AddressIndex = 2;
        private const Int32 TypeIndex = 3;
        private const Int32 ValueIndex = 4;

        public AddressTablePresenter(IAddressTableView View, IAddressTableModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            AddressTableCache = new ListViewCache();

            // Bind events triggered by the model
            Model.EventReadValues += EventReadValues;

            Model.EventClearAddressCacheItem += EventClearAddressCacheItem;
            Model.EventClearAddressCache += EventClearAddressCache;

            Model.ForceRefresh();
        }

        #region Method definitions called by the view (downstream)

        public void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            Model.UpdateReadBounds(StartReadIndex, EndReadIndex);
        }

        public ListViewItem GetAddressTableItemAt(Int32 Index)
        {
            ListViewItem Item = AddressTableCache.Get((UInt64)Index);
            AddressItem AddressItem = Model.GetAddressItemAt(Index);

            // Try to update and return the item if it is a valid item
            if (Item != null &&
                AddressTableCache.TryUpdateSubItem(Index, ValueIndex, AddressItem.GetValueString()) &&
                AddressTableCache.TryUpdateSubItem(Index, AddressIndex, AddressItem.GetAddressString()))
            {
                Item.Checked = AddressItem.GetActivationState();
                return Item;
            }

            // Add the properties to the manager and get the list view item back
            Item = AddressTableCache.Add(Index, new String[] { String.Empty, String.Empty, String.Empty, String.Empty, String.Empty });

            Item.ForeColor = AddressItem.IsHex ? Color.Green : SystemColors.ControlText;

            Item.SubItems[FreezeCheckBoxIndex].Text = String.Empty;
            Item.SubItems[DescriptionIndex].Text = (AddressItem.Description == null ? String.Empty : AddressItem.Description);
            Item.SubItems[AddressIndex].Text = Conversions.ToAddress(AddressItem.BaseAddress);
            Item.SubItems[TypeIndex].Text = AddressItem.ElementType == null ? String.Empty : AddressItem.ElementType.Name;
            Item.SubItems[ValueIndex].Text = AddressItem.GetValueString();

            Item.Checked = AddressItem.GetActivationState();
            
            return Item;
        }

        public void SetAddressFrozen(Int32 Index, Boolean Activated)
        {
            Model.SetAddressFrozen(Index, Activated);
        }
        
        public ListViewItem GetFSMTableItemAt(Int32 Index)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventReadValues(Object Sender, AddressTableEventArgs E)
        {
            View.ReadValues();
        }

        private void EventClearAddressCacheItem(Object Sender, AddressTableEventArgs E)
        {
            AddressTableCache.Delete((UInt64)E.ClearCacheIndex);
            View.UpdateAddressTableItemCount(E.ItemCount);
        }

        private void EventClearAddressCache(Object Sender, AddressTableEventArgs E)
        {
            AddressTableCache.FlushCache();
            View.UpdateAddressTableItemCount(E.ItemCount);
        }

        #endregion

    } // End class

} // End namespace