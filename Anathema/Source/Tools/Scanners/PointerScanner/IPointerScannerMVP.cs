using Anathema.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
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
            EventReadValues(this, E);
        }

        public event PointerScannerEventHandler EventUpdateItemCount;
        protected virtual void OnEventScanFinished(PointerScannerEventArgs E)
        {
            EventUpdateItemCount(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract void BeginPointerScan();
        public abstract void BeginPointerRescan();

        public abstract void AddSelectionToTable(Int32 MinIndex, Int32 MaxIndex);

        public abstract String GetValueAtIndex(Int32 Index);
        public abstract String GetBaseAddress(Int32 Index);
        public abstract String[] GetOffsets(Int32 Index);

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
        protected new IPointerScannerView View;
        protected new IPointerScannerModel Model;

        private const Int32 ValueIndex = 0;
        private const Int32 BaseIndex = 1;
        private const Int32 OffsetStartIndex = 2;

        private ListViewCache ListViewCache;
        private Int32 MaxPointerLevel;

        public PointerScannerPresenter(IPointerScannerView View, IPointerScannerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            ListViewCache = new ListViewCache();

            // Bind events triggered by the model
            Model.EventReadValues += EventReadValues;
            Model.EventUpdateItemCount += EventScanFinished;
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

        public ListViewItem GetItemAt(Int32 Index)
        {
            ListViewItem Item = ListViewCache.Get((UInt64)Index);

            // Try to update and return the item if it is a valid item
            if (Item != null && ListViewCache.TryUpdateSubItem(Index, ValueIndex, Model.GetValueAtIndex(Index)))
                return Item;

            // Add the properties to the cache and get the list view item back
            Item = ListViewCache.Add(Index, Enumerable.Repeat(String.Empty, OffsetStartIndex + MaxPointerLevel).ToArray());

            Item.SubItems[ValueIndex].Text = "-";
            Item.SubItems[BaseIndex].Text = Model.GetBaseAddress(Index);

            String[] Offsets = Model.GetOffsets(Index);
            for (Int32 OffsetIndex = OffsetStartIndex; OffsetIndex < OffsetStartIndex + MaxPointerLevel; OffsetIndex++)
                Item.SubItems[OffsetIndex].Text = (OffsetIndex - OffsetStartIndex) < Offsets.Length ? Offsets[OffsetIndex - OffsetStartIndex] : String.Empty;


            return Item;
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

            this.MaxPointerLevel = Conversions.ParseValue(typeof(Int32), MaxPointerLevel);

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