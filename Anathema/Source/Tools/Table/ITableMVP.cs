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
        public Int32 AddressTableItemCount = 0;
        public Int32 ScriptTableItemCount = 0;
        public Int32 FSMTableItemCount = 0;
    }

    interface ITableView : IView
    {
        // Methods invoked by the presenter (upstream)
        void RefreshDisplay();
        void UpdateAddressTableItemCount(Int32 ItemCount);
        void UpdateScriptTableItemCount(Int32 ItemCount);
        void UpdateFSMTableItemCount(Int32 ItemCount);
    }

    abstract class ITableModel : RepeatedTask, IModel
    {
        // Events triggered by the model (upstream)
        public event TableEventHandler EventRefreshDisplay;
        protected virtual void OnEventRefreshDisplay(TableEventArgs E)
        {
            if (EventRefreshDisplay != null) EventRefreshDisplay(this, E);
        }

        public event TableEventHandler EventUpdateAddressTableItemCount;
        protected virtual void OnEventUpdateAddressTableItemCount(TableEventArgs E)
        {
            EventUpdateAddressTableItemCount(this, E);
        }

        public event TableEventHandler EventUpdateScriptTableItemCount;
        protected virtual void OnEventUpdateScriptTableItemCount(TableEventArgs E)
        {
            EventUpdateScriptTableItemCount(this, E);
        }

        public event TableEventHandler EventUpdateFSMTableItemCount;
        protected virtual void OnEventUpdateFSMTableItemCount(TableEventArgs E)
        {
            EventUpdateFSMTableItemCount(this, E);
        }

        public override void Begin()
        {
            // Temporary workaround until I feel like adding multiple tasks to the RepeatTask class
            WaitTime = Math.Min(Settings.GetInstance().GetTableReadInterval(), Settings.GetInstance().GetFreezeInterval());
            base.Begin();
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
    }

    class TablePresenter : Presenter<ITableView, ITableModel>
    {
        protected new ITableView View { get; set; }
        protected new ITableModel Model { get; set; }
        
        private ListViewCache AddressTableCache;
        private ListViewCache ScriptTableCache;

        private const Int32 A = 0;
        private const Int32 B = 1;
        private const Int32 C = 2;
        private const Int32 D = 3;
        private const Int32 E = 4;

        public TablePresenter(ITableView View, ITableModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            AddressTableCache = new ListViewCache();
            ScriptTableCache = new ListViewCache();

            // Bind events triggered by the model
            Model.EventRefreshDisplay += EventRefreshDisplay;
            Model.EventUpdateAddressTableItemCount += EventUpdateAddressTableItemCount;
            Model.EventUpdateScriptTableItemCount += EventUpdateScriptTableItemCount;
            Model.EventUpdateFSMTableItemCount += EventUpdateFSMTableItemCount;
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

        public ListViewItem GetAddressTableItemAt(Int32 Index)
        {
            ListViewItem Item = AddressTableCache.Get(Index);
            AddressItem AddressItem = Model.GetAddressItemAt(Index);

            // Try to update and return the item if it is a valid item
            if (Item != null && AddressTableCache.TryUpdateSubItem(Index, E, (AddressItem.Value == null ? String.Empty : AddressItem.Value.ToString())))
                return Item;

            // Add the properties to the manager and get the list view item back
            Item = AddressTableCache.Add(Index, new String[] { String.Empty, String.Empty, String.Empty, String.Empty, String.Empty });

            Item.SubItems[A].Text = String.Empty;
            Item.SubItems[B].Text = (AddressItem.Description == null ? String.Empty : AddressItem.Description);
            Item.SubItems[C].Text = Conversions.ToAddress(AddressItem.Address.ToString());
            Item.SubItems[D].Text = AddressItem.ElementType == null ? String.Empty : AddressItem.ElementType.Name;
            Item.SubItems[E].Text = AddressItem.Value == null ? String.Empty : AddressItem.Value.ToString();
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

        private void EventRefreshDisplay(Object Sender, TableEventArgs E)
        {
            View.RefreshDisplay();
        }

        private void EventUpdateAddressTableItemCount(Object Sender, TableEventArgs E)
        {
            View.UpdateAddressTableItemCount(E.AddressTableItemCount);
        }

        private void EventUpdateScriptTableItemCount(Object Sender, TableEventArgs E)
        {
            View.UpdateScriptTableItemCount(E.ScriptTableItemCount);
        }

        private void EventUpdateFSMTableItemCount(Object Sender, TableEventArgs E)
        {
            View.UpdateFSMTableItemCount(E.FSMTableItemCount);
        }

        #endregion
    }
}