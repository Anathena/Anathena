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

    interface ITableView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void RefreshDisplay();
        void UpdateAddressTableItemCount(Int32 ItemCount);
        void UpdateScriptTableItemCount(Int32 ItemCount);
        void UpdateFSMTableItemCount(Int32 ItemCount);
    }

    abstract class ITableModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event TableEventHandler EventRefreshDisplay;
        protected virtual void OnEventRefreshDisplay(TableEventArgs E)
        {
            EventRefreshDisplay(this, E);
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

        // Functions invoked by presenter (downstream)
        public abstract AddressItem GetAddressItemAt(Int32 Index);
        public abstract dynamic GetAddressValueAt(Int32 Index);
        public abstract void SetAddressItemAt(Int32 Index, AddressItem AddressItem);
        public abstract void SetAddressValueAt(Int32 Index, dynamic Value);

        public abstract ScriptItem GetScriptItemAt(Int32 Index);
        public abstract void SetScriptItemAt(Int32 Index, ScriptItem ScriptItem);
    }

    class TablePresenter : Presenter<ITableView, ITableModel>
    {
        new ITableView View;
        new ITableModel Model;

        public TablePresenter(ITableView View, ITableModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventRefreshDisplay += EventRefreshDisplay;
            Model.EventUpdateAddressTableItemCount += EventUpdateAddressTableItemCount;
            Model.EventUpdateScriptTableItemCount += EventUpdateScriptTableItemCount;
            Model.EventUpdateFSMTableItemCount += EventUpdateFSMTableItemCount;
        }

        #region Method definitions called by the view (downstream)

        public ListViewItem GetAddressTableItemAt(Int32 Index)
        {
            AddressItem AddressItem = Model.GetAddressItemAt(Index);
            dynamic Value = Model.GetAddressValueAt(Index);

            ListViewItem Result = new ListViewItem(new String[] { String.Empty,
               AddressItem.Description.ToString(),  AddressItem.ElementType.Name.ToString(), AddressItem.Address.ToString(), Value.ToString() });
            Result.Checked = AddressItem.GetActivationState();
            return Result;
        }

        public ListViewItem GetScriptTableItemAt(Int32 Index)
        {
            ScriptItem ScriptItem = Model.GetScriptItemAt(Index);

            ListViewItem Result = new ListViewItem(ScriptItem.Description.ToString());
            Result.Checked = ScriptItem.GetActivationState();

            return Result;
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
            View.UpdateAddressTableItemCount(E.ScriptTableItemCount);
        }

        private void EventUpdateFSMTableItemCount(Object Sender, TableEventArgs E)
        {
            View.UpdateAddressTableItemCount(E.FSMTableItemCount);
        }

        #endregion
    }
}