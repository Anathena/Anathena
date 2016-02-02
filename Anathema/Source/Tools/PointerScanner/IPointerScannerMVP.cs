using Binarysharp.MemoryManagement;
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

        public abstract String GetValueAtIndex(Int32 Index);

        public abstract void SetTargetAddress(UInt64 Address);
        public abstract void SetMaxPointerLevel(Int32 MaxPointerLevel);
        public abstract void SetMaxPointerOffset(UInt64 MaxOffset);
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

        public void BeginPointerScan()
        {
            Model.BeginPointerScan();
        }

        public void BeginPointerRescan()
        {
            Model.BeginPointerRescan();
        }

        public ListViewItem GetItemAt(Int32 Index)
        {
            ListViewItem Item = ListViewCache.Get((UInt64)Index);

            // Try to update and return the item if it is a valid item
            if (Item != null && ListViewCache.TryUpdateSubItem(Index, ValueIndex, Model.GetValueAtIndex(Index)))
                return Item;

            // Add the properties to the cache and get the list view item back
            Item = ListViewCache.Add(Index, Enumerable.Repeat(String.Empty, OffsetStartIndex + MaxPointerLevel).ToArray());

            Item.SubItems[ValueIndex].Text = "value";
            Item.SubItems[BaseIndex].Text = "address";

            for (Int32 OffsetIndex = OffsetStartIndex; OffsetIndex < OffsetStartIndex + MaxPointerLevel; OffsetIndex++)
                Item.SubItems[OffsetIndex].Text = "offset";


            return Item;
        }

        public Boolean TrySetTargetAddress(String TargetAddress)
        {
            if (!CheckSyntax.Address(TargetAddress))
                return false;

            Model.SetTargetAddress(Conversions.AddressToValue(TargetAddress));
            return true;
        }

        public Boolean TrySetMaxPointerLevel(String MaxPointerLevel)
        {
            if (!CheckSyntax.CanParseValue(typeof(Int32), MaxPointerLevel))
                return false;

            this.MaxPointerLevel = Conversions.ParseValue(typeof(Int32), MaxPointerLevel);

            Model.SetMaxPointerLevel(Conversions.ParseValue(typeof(Int32), MaxPointerLevel));
            return true;
        }

        public Boolean TrySetMaxPointerOffset(String MaxPointerOffset)
        {
            if (!CheckSyntax.CanParseValue(typeof(UInt64), MaxPointerOffset))
                return false;

            Model.SetMaxPointerOffset(Conversions.ParseValue(typeof(UInt64), MaxPointerOffset));
            return true;
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