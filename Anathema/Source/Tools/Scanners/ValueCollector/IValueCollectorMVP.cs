using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IValueCollectorView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
    }

    abstract class IValueCollectorModel : IScannerModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
        public abstract void SetElementType(Type ElementType);
    }

    class ValueCollectorPresenter : ScannerPresenter
    {
        protected new IValueCollectorView View;
        protected new IValueCollectorModel Model;

        public ValueCollectorPresenter(IValueCollectorView View, IValueCollectorModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model

        }

        #region Method definitions called by the view (downstream)

        public void SetElementType(Type ElementType)
        {
            Model.SetElementType(ElementType);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion

    } // End class

} // End namespace