using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void PointerScannerEventHandler(Object Sender, PointerScannerEventArgs Args);
    class PointerScannerEventArgs : EventArgs
    {
        public String ProcessTitle = String.Empty;
    }

    interface IPointerScannerView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
    }

    abstract class IPointerScannerModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        event PointerScannerEventHandler EventUpdateProcessTitle;

        // Functions invoked by presenter (downstream)
        public abstract void SetTargetAddress(UInt64 Address);
        public abstract void SetMaxPointerLevel(Int32 MaxPointerLevel);
        public abstract void SetMaxPointerOffset(UInt64 MaxOffset);
    }

    class PointerScannerPresenter : ScannerPresenter
    {
        new IPointerScannerView View;
        new IPointerScannerModel Model;

        public PointerScannerPresenter(IPointerScannerView View, IPointerScannerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
        }

        #region Method definitions called by the view (downstream)

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

        #endregion

    } // End class

} // End namespace