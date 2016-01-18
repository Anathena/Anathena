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
    delegate void ResultsEventHandler(Object Sender, ResultsEventArgs Args);
    class ResultsEventArgs : EventArgs
    {
        public UInt64 MemorySize = 0;
    }

    interface IResultsView : IView
    {
        // Methods invoked by the presenter (upstream)
        void RefreshDisplay();
        void UpdateMemorySizeLabel(String MemorySize);
        void UpdateItemCount(Int32 ItemCount);
    }

    abstract class IResultsModel : RepeatedTask, IModel
    {
        // Events triggered by the model (upstream)
        public event ResultsEventHandler EventRefreshDisplay;
        protected virtual void OnEventRefreshDisplay(ResultsEventArgs E)
        {
            EventRefreshDisplay(this, E);
        }
        public event ResultsEventHandler EventUpdateMemorySize;
        protected virtual void OnEventUpdateMemorySize(ResultsEventArgs E)
        {
            EventUpdateMemorySize(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract void AddSelectionToTable(Int32 Index);

        public abstract IntPtr GetAddressAtIndex(Int32 Index);
        public abstract dynamic GetValueAtIndex(Int32 Index);
        public abstract dynamic GetLabelAtIndex(Int32 Index);
        public abstract Type GetScanType();
        public abstract void SetScanType(Type ScanType);
    }

    class ResultsPresenter : Presenter<IResultsView, IResultsModel>
    {
        new IResultsView View;
        new IResultsModel Model;

        public ResultsPresenter(IResultsView View, IResultsModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventRefreshDisplay += EventRefreshDisplay;
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

        public void AddSelectionToTable(Int32 Index)
        {
            Model.AddSelectionToTable(Index);
        }

        public void UpdateScanType(Type ScanType)
        {
            if (ScanType == typeof(Byte) || ScanType == typeof(UInt16) || ScanType == typeof(UInt32) || ScanType == typeof(UInt64))
                throw new Exception("Invalid type. ScanType parameter assumes signed type.");

            // Apply type change
            Type PreviousScanType = Model.GetScanType();
            Model.SetScanType(ScanType);

            switch (Type.GetTypeCode(ScanType))
            {
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    ChangeSign();
                    break;
                default: return;
            }
        }

        public void ChangeSign()
        {
            Type ScanType = Model.GetScanType();

            switch (Type.GetTypeCode(ScanType))
            {
                case TypeCode.Byte: ScanType = typeof(SByte); break;
                case TypeCode.SByte: ScanType = typeof(Byte); break;
                case TypeCode.Int16: ScanType = typeof(UInt16); break;
                case TypeCode.Int32: ScanType = typeof(UInt32);  break;
                case TypeCode.Int64: ScanType = typeof(UInt64);  break;
                case TypeCode.UInt16: ScanType = typeof(Int16); break;
                case TypeCode.UInt32: ScanType = typeof(Int32); break;
                case TypeCode.UInt64: ScanType = typeof(Int64); break;
                default: return;
            }

            Model.SetScanType(ScanType);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventRefreshDisplay(Object Sender, ResultsEventArgs E)
        {
            View.RefreshDisplay();
        }

        private void EventUpdateMemorySize(Object Sender, ResultsEventArgs E)
        {
            View.UpdateMemorySizeLabel(Conversions.BytesToMetric(E.MemorySize));
            View.UpdateItemCount((Int32)Math.Min(E.MemorySize, Int32.MaxValue));
        }

        #endregion
    }
}