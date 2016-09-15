using Anathena.Source.Scanners.ScanConstraints;
using Anathena.Source.Utils.Extensions;
using Anathena.Source.Utils.Validation;
using System;
using System.Collections.Generic;

namespace Anathena.Source.Scanners.PointerScanner
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
        private new IPointerScannerView view { get; set; }
        private new IPointerScannerModel model { get; set; }

        public PointerScannerPresenter(IPointerScannerView view, IPointerScannerModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model
            model.EventReadValues += EventReadValues;
            model.EventUpdateItemCount += EventScanFinished;

            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            model.UpdateReadBounds(StartReadIndex, EndReadIndex);
        }

        public void BeginPointerScan()
        {
            model.BeginPointerScan();
        }

        public void BeginPointerRescan()
        {
            model.BeginPointerRescan();
        }

        public void SetElementType(Type ElementType)
        {
            model.SetElementType(ElementType);
        }

        public void SetRescanMode(Boolean IsAddressMode)
        {
            model.SetRescanMode(IsAddressMode);
        }

        public String GetValueAtIndex(Int32 Index)
        {
            return model.GetValueAtIndex(Index);
        }

        public String GetAddressAtIndex(Int32 Index)
        {
            return model.GetAddressAtIndex(Index);
        }

        public IEnumerable<String> GetOffsetsAtIndex(Int32 Index)
        {
            return model.GetOffsetsAtIndex(Index);
        }

        public Int32 GetMaxPointerLevel()
        {
            return model.GetMaxPointerLevel();
        }

        public Int32 GetMaxPointerOffset()
        {
            return model.GetMaxPointerOffset();
        }

        public void AddSelectionToTable(Int32 MinIndex, Int32 MaxIndex)
        {
            model.AddSelectionToTable(MinIndex, MaxIndex);
        }

        public void SetTargetAddress(String TargetAddress)
        {
            if (!CheckSyntax.CanParseAddress(TargetAddress))
                return;

            model.SetTargetAddress(Conversions.AddressToValue(TargetAddress).ToIntPtr());
        }

        public void SetScanConstraintManager(ScanConstraintManager ScanConstraintManager)
        {
            model.SetScanConstraintManager(ScanConstraintManager);
        }

        public void SetMaxPointerLevel(String MaxPointerLevel)
        {
            if (!CheckSyntax.CanParseValue(typeof(Int32), MaxPointerLevel))
                return;

            model.SetMaxPointerLevel(Conversions.ParseDecStringAsValue(typeof(Int32), MaxPointerLevel));
        }

        public void SetMaxPointerOffset(String MaxPointerOffset)
        {
            if (!CheckSyntax.CanParseValue(typeof(Int32), MaxPointerOffset))
                return;

            model.SetMaxPointerOffset(Conversions.ParseDecStringAsValue(typeof(Int32), MaxPointerOffset));
            return;
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)        

        private void EventReadValues(Object Sender, PointerScannerEventArgs E)
        {
            view.ReadValues();
        }

        private void EventScanFinished(Object Sender, PointerScannerEventArgs E)
        {
            view.ScanFinished(E.ItemCount, E.MaxPointerLevel);
        }

        #endregion

    } // End class

} // End namespace