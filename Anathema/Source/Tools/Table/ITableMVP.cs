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
        public UInt64 AddressTableItemCount = 0;
        public UInt64 ScriptTableItemCount = 0;
    }

    interface ITableView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void RefreshResults();
    }

    abstract class ITableModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event TableEventHandler EventRefreshDisplay;
        protected virtual void OnEventRefreshDisplay(TableEventArgs E)
        {
            EventRefreshDisplay(this, E);
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
            Model.EventRefreshDisplay += EventUpdateDisplay;
        }

        #region Method definitions called by the view (downstream)

        public ListViewItem GetAddressTableItemAt(Int32 Index)
        {
            AddressItem AddressItem = Model.GetAddressItemAt(Index);
            dynamic Value = Model.GetAddressValueAt(Index);

            ListViewItem Result = new ListViewItem(new String[] {
                AddressItem.Address.ToString(), Value.ToString(), AddressItem.Description.ToString(), AddressItem.ElementType.ToString() });
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

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateDisplay(Object Sender, TableEventArgs E)
        {
            View.RefreshResults();
        }

        #endregion
    }
}