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
        public List<String> Addresses = null;
        public List<String> Values = null;
        public List<String> Labels = null;
        public UInt64 MemorySize = 0;
    }

    interface ITableView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void RefreshResults();
        void UpdateMemorySize(String MemorySize);
        void UpdateItemCount(Int32 ItemCount);
    }

    abstract class ITableModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event TableEventHandler EventRefreshDisplay;
        protected virtual void OnEventRefreshDisplay(TableEventArgs E)
        {
            EventRefreshDisplay(this, E);
        }
        public event TableEventHandler EventUpdateMemorySize;
        protected virtual void OnEventUpdateMemorySize(TableEventArgs E)
        {
            EventUpdateMemorySize(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract IntPtr GetAddressAtIndex(Int32 Index);
        public abstract dynamic GetValueAtIndex(Int32 Index);
        public abstract dynamic GetLabelAtIndex(Int32 Index);
        public abstract void UpdateScanType(Type ScanType);
        public abstract Type GetScanType();
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
            Model.EventUpdateMemorySize += EventUpdateMemorySize;
        }

        #region Method definitions called by the view (downstream)

        public ListViewItem GetItemAt(Int32 Index)
        {
            IntPtr Address = Model.GetAddressAtIndex(Index);
            dynamic Value = Model.GetValueAtIndex(Index);
            dynamic Label = Model.GetLabelAtIndex(Index);

            String[] Result = new String[] { Conversions.ToAddress(Address.ToString()), Value.ToString(), Label.ToString() };
            return new ListViewItem(Result);
        }

        public void UpdateScanType(Type ScanType)
        {
            if (ScanType == typeof(Byte) || ScanType == typeof(UInt16) || ScanType == typeof(UInt32) || ScanType == typeof(UInt64))
                throw new Exception("Invalid type. ScanType parameter assumes signed type.");

            // Apply type change
            Type PreviousScanType = Model.GetScanType();
            Model.UpdateScanType(ScanType);

            // Enforce same sign as previous type
            var @switch = new Dictionary<Type, Action> {
                    { typeof(Byte), () => ChangeSign() },
                    { typeof(UInt16), () => ChangeSign() },
                    { typeof(UInt32), () => ChangeSign() },
                    { typeof(UInt64), () => ChangeSign() },
                };

            if (@switch.ContainsKey(PreviousScanType))
                @switch[PreviousScanType]();
        }

        public void ChangeSign()
        {
            Type ScanType = Model.GetScanType();

            var @switch = new Dictionary<Type, Action> {
                    { typeof(Byte), () => ScanType = typeof(SByte) },
                    { typeof(SByte), () => ScanType = typeof(Byte) },
                    { typeof(Int16), () => ScanType = typeof(UInt16) },
                    { typeof(Int32), () => ScanType = typeof(UInt32) },
                    { typeof(Int64), () => ScanType = typeof(UInt64) },
                    { typeof(UInt16), () => ScanType = typeof(Int16) },
                    { typeof(UInt32), () => ScanType = typeof(Int32) },
                    { typeof(UInt64), () => ScanType = typeof(Int64) },
                };

            if (@switch.ContainsKey(ScanType))
                @switch[ScanType]();

            Model.UpdateScanType(ScanType);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateDisplay(Object Sender, TableEventArgs E)
        {
            View.RefreshResults();
        }

        private void EventUpdateMemorySize(Object Sender, TableEventArgs E)
        {
            View.UpdateMemorySize(Conversions.ByteCountToMetricSize(E.MemorySize));
            View.UpdateItemCount((Int32)Math.Min(E.MemorySize, Int32.MaxValue));
        }

        #endregion
    }
}