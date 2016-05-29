using Anathema.Source.Utils;
using Anathema.Source.Utils.MVP;
using Anathema.Source.Utils.Setting;
using Anathema.Source.Utils.Validation;
using System;

namespace Anathema.Source.Results.ScanResults
{
    delegate void ScanResultsEventHandler(Object Sender, ScanResultsEventArgs Args);
    class ScanResultsEventArgs : EventArgs
    {
        public UInt64 ElementCount = 0;
        public UInt64 MemorySize = 0;
    }

    interface IScanResultsView : IView
    {
        // Methods invoked by the presenter (upstream)
        void ReadValues();
        void SetEnabled(Boolean Enabled);
        void UpdateMemorySizeLabel(String MemorySize, String ItemCount);
        void UpdateItemCount(Int32 ItemCount);
    }

    abstract class IScanResultsModel : RepeatedTask, IModel
    {
        public virtual void OnGUIOpen() { }

        // Events triggered by the model (upstream)
        public event ScanResultsEventHandler EventReadValues;
        protected virtual void OnEventReadValues(ScanResultsEventArgs E)
        {
            EventReadValues?.Invoke(this, E);
        }

        public event ScanResultsEventHandler EventEnableResults;
        protected virtual void OnEventEnableResults(ScanResultsEventArgs E)
        {
            EventEnableResults?.Invoke(this, E);
        }

        public event ScanResultsEventHandler EventDisableResults;
        protected virtual void OnEventDisableResults(ScanResultsEventArgs E)
        {
            EventDisableResults?.Invoke(this, E);
        }

        public event ScanResultsEventHandler EventUpdateItemCounts;
        protected virtual void OnEventUpdateItemCounts(ScanResultsEventArgs E)
        {
            EventUpdateItemCounts?.Invoke(this, E);
        }

        public override void Begin()
        {
            WaitTime = Settings.GetInstance().GetResultReadInterval();
            base.Begin();
        }

        protected override void Update()
        {
            WaitTime = Settings.GetInstance().GetResultReadInterval();
        }

        // Functions invoked by presenter (downstream)
        public abstract void AddSelectionToTable(Int32 MinIndex, Int32 MaxIndex);

        public abstract IntPtr GetAddressAtIndex(Int32 Index);
        public abstract String GetValueAtIndex(Int32 Index);
        public abstract String GetLabelAtIndex(Int32 Index);
        public abstract Type GetScanType();
        public abstract void SetScanType(Type ScanType);

        public abstract void ForceRefresh();

        public abstract void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex);
    }

    class ScanResultsPresenter : Presenter<IScanResultsView, IScanResultsModel>
    {
        private new IScanResultsView View { get; set; }
        private new IScanResultsModel Model { get; set; }

        public ScanResultsPresenter(IScanResultsView View, IScanResultsModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventReadValues += EventReadValues;
            Model.EventEnableResults += EventEnableResults;
            Model.EventDisableResults += EventDisableResults;
            Model.EventUpdateItemCounts += EventUpdateItemCounts;

            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            Model.UpdateReadBounds(StartReadIndex, EndReadIndex);
        }

        public String GetValueAtIndex(Int32 Index)
        {
            return Model.GetValueAtIndex(Index);
        }

        public String GetAddressAtIndex(Int32 Index)
        {
            return Conversions.ToAddress(Model.GetAddressAtIndex(Index));
        }

        public String GetLabelAtIndex(Int32 Index)
        {
            return Model.GetLabelAtIndex(Index);
        }

        public void AddSelectionToTable(Int32 MinIndex, Int32 MaxIndex)
        {
            Model.AddSelectionToTable(MinIndex, MaxIndex);
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
                case TypeCode.Int32: ScanType = typeof(UInt32); break;
                case TypeCode.Int64: ScanType = typeof(UInt64); break;
                case TypeCode.UInt16: ScanType = typeof(Int16); break;
                case TypeCode.UInt32: ScanType = typeof(Int32); break;
                case TypeCode.UInt64: ScanType = typeof(Int64); break;
                default: return;
            }

            Model.SetScanType(ScanType);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventReadValues(Object Sender, ScanResultsEventArgs E)
        {
            View.ReadValues();
        }

        private void EventEnableResults(Object Sender, ScanResultsEventArgs E)
        {
            View.SetEnabled(true);
        }

        private void EventDisableResults(Object Sender, ScanResultsEventArgs E)
        {
            View.SetEnabled(false);
        }

        private void EventUpdateItemCounts(Object Sender, ScanResultsEventArgs E)
        {
            View.UpdateMemorySizeLabel(Conversions.BytesToMetric(E.MemorySize), E.ElementCount.ToString());
            View.UpdateItemCount((Int32)Math.Min(E.ElementCount, (UInt64)Int32.MaxValue));
        }

        #endregion

    } // End class

} // End namespace