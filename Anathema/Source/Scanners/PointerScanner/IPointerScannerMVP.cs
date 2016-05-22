using Anathema.Scanners.ScanConstraints;
using Anathema.Utils.Extensions;
using Anathema.Utils.Validation;
using System;
using System.Collections.Generic;

namespace Anathema.Scanners.PointerScanner
{
    delegate void PointerScannerEventHandler(Object Sender, PointerScannerEventArgs Args);
    class PointerScannerEventArgs : EventArgs
    {
        public Int32 ItemCount;
        public Int32 MaxPointerLevel;
    }

    interface IPointerScannerView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void ReadValues();
        void ScanFinished(Int32 ItemCount, Int32 MaxPointerLevel);
    }

    abstract class IPointerScannerModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event PointerScannerEventHandler EventReadValues;
        protected virtual void OnEventReadValues(PointerScannerEventArgs E)
        {
            EventReadValues?.Invoke(this, E);
        }

        public event PointerScannerEventHandler EventUpdateItemCount;
        protected virtual void OnEventScanFinished(PointerScannerEventArgs E)
        {
            EventUpdateItemCount?.Invoke(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract void BeginPointerScan();
        public abstract void BeginPointerRescan();

        public abstract void AddSelectionToTable(Int32 MinIndex, Int32 MaxIndex);

        public abstract String GetValueAtIndex(Int32 Index);
        public abstract String GetAddressAtIndex(Int32 Index);
        public abstract IEnumerable<String> GetOffsetsAtIndex(Int32 Index);
        public abstract Int32 GetMaxPointerLevel();
        public abstract Int32 GetMaxPointerOffset();

        public abstract void SetElementType(Type ElementType);
        public abstract void SetRescanMode(Boolean IsAddressMode);
        public abstract void SetTargetAddress(IntPtr Address);
        public abstract void SetMaxPointerLevel(Int32 MaxPointerLevel);
        public abstract void SetMaxPointerOffset(Int32 MaxOffset);

        public abstract void SetScanConstraintManager(ScanConstraintManager ScanConstraintManager);

        public abstract void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex);
    }

    class PointerScannerPresenter : ScannerPresenter
    {
        private new IPointerScannerView View { get; set; }
        private new IPointerScannerModel Model { get; set; }

        public PointerScannerPresenter(IPointerScannerView View, IPointerScannerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventReadValues += EventReadValues;
            Model.EventUpdateItemCount += EventScanFinished;

            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            Model.UpdateReadBounds(StartReadIndex, EndReadIndex);
        }

        public void BeginPointerScan()
        {
            Model.BeginPointerScan();
        }

        public void BeginPointerRescan()
        {
            Model.BeginPointerRescan();
        }

        public void SetElementType(Type ElementType)
        {
            Model.SetElementType(ElementType);
        }

        public void SetRescanMode(Boolean IsAddressMode)
        {
            Model.SetRescanMode(IsAddressMode);
        }

        public String GetValueAtIndex(Int32 Index)
        {
            return Model.GetValueAtIndex(Index);
        }

        public String GetAddressAtIndex(Int32 Index)
        {
            return Model.GetAddressAtIndex(Index);
        }

        public IEnumerable<String> GetOffsetsAtIndex(Int32 Index)
        {
            return Model.GetOffsetsAtIndex(Index);
        }

        public Int32 GetMaxPointerLevel()
        {
            return Model.GetMaxPointerLevel();
        }

        public Int32 GetMaxPointerOffset()
        {
            return Model.GetMaxPointerOffset();
        }

        public void AddSelectionToTable(Int32 MinIndex, Int32 MaxIndex)
        {
            Model.AddSelectionToTable(MinIndex, MaxIndex);
        }

        public void SetTargetAddress(String TargetAddress)
        {
            if (!CheckSyntax.CanParseAddress(TargetAddress))
                return;

            Model.SetTargetAddress(Conversions.AddressToValue(TargetAddress).ToIntPtr());
        }

        public void SetScanConstraintManager(ScanConstraintManager ScanConstraintManager)
        {
            Model.SetScanConstraintManager(ScanConstraintManager);
        }

        public void SetMaxPointerLevel(String MaxPointerLevel)
        {
            if (!CheckSyntax.CanParseValue(typeof(Int32), MaxPointerLevel))
                return;

            Model.SetMaxPointerLevel(Conversions.ParseValue(typeof(Int32), MaxPointerLevel));
        }

        public void SetMaxPointerOffset(String MaxPointerOffset)
        {
            if (!CheckSyntax.CanParseValue(typeof(Int32), MaxPointerOffset))
                return;

            Model.SetMaxPointerOffset(Conversions.ParseValue(typeof(Int32), MaxPointerOffset));
            return;
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)        

        private void EventReadValues(Object Sender, PointerScannerEventArgs E)
        {
            View.ReadValues();
        }

        private void EventScanFinished(Object Sender, PointerScannerEventArgs E)
        {
            View.ScanFinished(E.ItemCount, E.MaxPointerLevel);
        }

        #endregion

    } // End class

} // End namespace