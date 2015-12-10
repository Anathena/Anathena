using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IMainView : IView
    {
        // Methods invoked by the presenter (upstream)

    }

    interface IMainModel : IModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
        void UpdateProcess(MemorySharp MemoryEditor);
    }

    class MainPresenter : Presenter<IMainView, IMainModel>
    {
        public MainPresenter(IMainView View, IMainModel Model) : base(View, Model)
        {
            // Bind events triggered by the model

        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion
    }
}
