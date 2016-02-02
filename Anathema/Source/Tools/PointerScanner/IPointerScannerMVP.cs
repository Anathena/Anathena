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
    }

    interface IPointerScannerView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void ReadValues();
        void UpdateItemCount(Int32 ItemCount);
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
        protected virtual void OnEventUpdateItemCount(PointerScannerEventArgs E)
        {
            EventUpdateItemCount(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract void SetTargetAddress(UInt64 Address);
        public abstract void SetMaxPointerLevel(Int32 MaxPointerLevel);
        public abstract void SetMaxPointerOffset(UInt64 MaxOffset);
    }

    class PointerScannerPresenter : ScannerPresenter
    {
        new IPointerScannerView View;
        new IPointerScannerModel Model;

        private ListViewCache ListViewCache;
        private const Int32 BaseIndex = 0;

        public PointerScannerPresenter(IPointerScannerView View, IPointerScannerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
        }

        #region Method definitions called by the view (downstream)
        
        public ListViewItem GetItemAt(Int32 Index)
        {
            ListViewItem Item = ListViewCache.Get((UInt64)Index);

            // Try to update and return the item if it is a valid item
            //if (Item != null && ListViewCache.TryUpdateSubItem(Index, ValueIndex, Model.GetValueAtIndex(Index)))
            //    return Item;

            // Add the properties to the cache and get the list view item back
            Item = ListViewCache.Add(Index, Enumerable.Repeat(String.Empty, 3).ToArray());

            Item.SubItems[BaseIndex].Text = "module/address here";

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

        private void EventUpdateItemCount(Object Sender, PointerScannerEventArgs E)
        {
            View.UpdateItemCount(E.ItemCount);
        }

        #endregion

    } // End class

} // End namespace