using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    delegate void TableEventHandler(Object Sender, TableEventArgs Args);
    class TableEventArgs : EventArgs
    {
        public Int32 ItemCount = 0;
        public Int32 ClearCacheIndex = -1;
    }

    interface ITableView : IView
    {
        // Methods invoked by the presenter (upstream)
        void ReadValues();
        void UpdateAddressTableItemCount(Int32 ItemCount);
        void UpdateScriptTableItemCount(Int32 ItemCount);
        void UpdateFSMTableItemCount(Int32 ItemCount);
    }

    abstract class ITableModel : RepeatedTask, IModel
    {
        // Events triggered by the model (upstream)
        public event TableEventHandler EventReadValues;
        protected virtual void OnEventReadValues(TableEventArgs E)
        {
            if (EventReadValues != null) EventReadValues(this, E);
        }

        public event TableEventHandler EventClearAddressCacheItem;
        protected virtual void OnEventClearAddressCacheItem(TableEventArgs E)
        {
            EventClearAddressCacheItem(this, E);
        }

        public event TableEventHandler EventClearScriptCacheItem;
        protected virtual void OnEventClearScriptCacheItem(TableEventArgs E)
        {
            EventClearScriptCacheItem(this, E);
        }

        public event TableEventHandler EventClearAddressCache;
        protected virtual void OnEventClearAddressCache(TableEventArgs E)
        {
            EventClearAddressCache(this, E);
        }

        public event TableEventHandler EventClearScriptCache;
        protected virtual void OnEventClearScriptCache(TableEventArgs E)
        {
            EventClearScriptCache(this, E);
        }

        public event TableEventHandler EventClearFSMCache;
        protected virtual void OnEventClearFSMCache(TableEventArgs E)
        {
            EventClearFSMCache(this, E);
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
        public abstract Boolean SaveTable(String Path);
        public abstract Boolean LoadTable(String Path);

        public abstract void OpenScript(Int32 Index);

        public abstract AddressItem GetAddressItemAt(Int32 Index);
        public abstract void SetAddressItemAt(Int32 Index, AddressItem AddressItem);
        public abstract void SetFrozenAt(Int32 Index, Boolean Activated);

        public abstract ScriptItem GetScriptItemAt(Int32 Index);
        public abstract void SetScriptItemAt(Int32 Index, ScriptItem ScriptItem);

        public abstract void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex);
    }

    class TablePresenter : Presenter<ITableView, ITableModel>
    {
        protected new ITableView View { get; set; }
        protected new ITableModel Model { get; set; }

        private ListViewCache AddressTableCache;
        private ListViewCache ScriptTableCache;

        private const Int32 CheckBoxIndex = 0;
        private const Int32 DescriptionIndex = 1;
        private const Int32 AddressIndex = 2;
        private const Int32 TypeIndex = 3;
        private const Int32 ValueIndex = 4;

        public TablePresenter(ITableView View, ITableModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            AddressTableCache = new ListViewCache();
            ScriptTableCache = new ListViewCache();

            // Bind events triggered by the model
            Model.EventReadValues += EventReadValues;

            Model.EventClearAddressCacheItem += EventClearAddressCacheItem;
            Model.EventClearScriptCacheItem += EventClearScriptCacheItem;

            Model.EventClearAddressCache += EventClearAddressCache;
            Model.EventClearScriptCache += EventClearScriptCache;
            Model.EventClearFSMCache += EventClearFSMCache;
        }

        #region Method definitions called by the view (downstream)

        public Boolean SaveTable(String Path)
        {
            if (Path == String.Empty)
                return false;

            return Model.SaveTable(Path);
        }

        public Boolean LoadTable(String Path)
        {
            if (Path == String.Empty)
                return false;

            return Model.LoadTable(Path);
        }

        public void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            Model.UpdateReadBounds(StartReadIndex, EndReadIndex);
        }

        public ListViewItem GetAddressTableItemAt(Int32 Index)
        {
            ListViewItem Item = AddressTableCache.Get(Index);
            AddressItem AddressItem = Model.GetAddressItemAt(Index);

            // Try to update and return the item if it is a valid item
            if (Item != null && AddressTableCache.TryUpdateSubItem(Index, ValueIndex, (AddressItem.Value == null ? "-" : AddressItem.Value.ToString())))
            {
                Item.Checked = AddressItem.GetActivationState();
                return Item;
            }

            // Add the properties to the manager and get the list view item back
            Item = AddressTableCache.Add(Index, new String[] { String.Empty, String.Empty, String.Empty, String.Empty, String.Empty });

            Item.SubItems[CheckBoxIndex].Text = String.Empty;
            Item.SubItems[DescriptionIndex].Text = (AddressItem.Description == null ? String.Empty : AddressItem.Description);
            Item.SubItems[AddressIndex].Text = Conversions.ToAddress(AddressItem.Address.ToString());
            Item.SubItems[TypeIndex].Text = AddressItem.ElementType == null ? String.Empty : AddressItem.ElementType.Name;
            Item.SubItems[ValueIndex].Text = "-";
            Item.Checked = AddressItem.GetActivationState();

            return Item;
        }

        public ListViewItem GetScriptTableItemAt(Int32 Index)
        {
            ListViewItem Item = AddressTableCache.Get(Index);
            ScriptItem ScriptItem = Model.GetScriptItemAt(Index);

            // Try to update and return the item if it is a valid item
            if (Item != null)
                return Item;

            // Add the properties to the manager and get the list view item back
            Item = AddressTableCache.Add(Index, new String[] { String.Empty, String.Empty });

            Item.SubItems[0].Text = String.Empty;
            Item.SubItems[1].Text = (ScriptItem.Description == null ? String.Empty : ScriptItem.Description);
            Item.Checked = ScriptItem.GetActivationState();

            return Item;
        }

        public void OpenScript(Int32 Index)
        {
            Model.OpenScript(Index);
        }

        public void SetFrozenAt(Int32 Index, Boolean Activated)
        {
            Model.SetFrozenAt(Index, Activated);
        }

        public String GetScriptTableScriptAt(Int32 Index)
        {
            return Model.GetScriptItemAt(Index).Script;
        }

        public ListViewItem GetFSMTableItemAt(Int32 Index)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventReadValues(Object Sender, TableEventArgs E)
        {
            View.ReadValues();
        }

        private void EventClearAddressCacheItem(Object Sender, TableEventArgs E)
        {
            AddressTableCache.Delete(E.ClearCacheIndex);
        }

        private void EventClearScriptCacheItem(Object Sender, TableEventArgs E)
        {
            ScriptTableCache.Delete(E.ClearCacheIndex);
        }

        private void EventClearAddressCache(Object Sender, TableEventArgs E)
        {
            AddressTableCache.FlushCache();
            View.UpdateAddressTableItemCount(E.ItemCount);
        }

        private void EventClearScriptCache(Object Sender, TableEventArgs E)
        {
            ScriptTableCache.FlushCache();
            View.UpdateScriptTableItemCount(E.ItemCount);
        }

        private void EventClearFSMCache(Object Sender, TableEventArgs E)
        {
            View.UpdateFSMTableItemCount(E.ItemCount);
        }

        #endregion
    }
}