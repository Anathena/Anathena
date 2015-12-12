using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void MainEventHandler(Object Sender, MainEventArgs Args);
    class MainEventArgs : EventArgs
    {
        public String ProcessTitle = String.Empty;
    }

    interface IMainView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateProcessTitle(String ProcessTitle);
    }

    interface IMainModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event MainEventHandler EventUpdateProcessTitle;

        // Functions invoked by presenter (downstream)
    }

    class MainPresenter : Presenter<IMainView, IMainModel>
    {
        public MainPresenter(IMainView View, IMainModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.EventUpdateProcessTitle += UpdateProcessTitle;
        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void UpdateProcessTitle(Object Sender, MainEventArgs E)
        {
            View.UpdateProcessTitle(E.ProcessTitle);
        }

        #endregion
    }
}
