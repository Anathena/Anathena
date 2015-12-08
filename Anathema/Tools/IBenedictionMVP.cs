using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IBenedictionView : IView
    {
        // Methods invoked by the presenter (upstream)

    }

    interface IBenedictionModel : IModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
        void UpdateProcess(MemorySharp MemoryEditor);
    }

    class BenedictionPresenter : Presenter<IBenedictionView, IBenedictionModel>
    {
        public BenedictionPresenter(IBenedictionView View, IBenedictionModel Model) : base(View, Model)
        {
            // Bind events triggered by the model

        }

        #region Method definitions called by the view (downstream)

        public void UpdateProcess(MemorySharp MemoryEditor)
        {
            Model.UpdateProcess(MemoryEditor);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion
    }
}
