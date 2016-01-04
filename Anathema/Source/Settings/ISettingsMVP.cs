using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    delegate void SettingsEventHandler(Object Sender, SettingsEventArgs Args);
    class SettingsEventArgs : EventArgs
    {

    }

    interface ISettingsView : IView
    {
        // Methods invoked by the presenter (upstream)
        void RefreshDisplay();
        void UpdateAddressTableItemCount(Int32 ItemCount);
        void UpdateScriptTableItemCount(Int32 ItemCount);
        void UpdateFSMTableItemCount(Int32 ItemCount);
    }

    interface ISettingsModel : IModel
    {
        // Events triggered by the model (upstream)
        event SettingsEventHandler EventRefreshDisplay;

        // Functions invoked by presenter (downstream)
        void Whatever();
    }

    class SettingsPresenter : Presenter<ISettingsView, ISettingsModel>
    {
        protected new ISettingsView View { get; set; }
        protected new ISettingsModel Model { get; set; }

        public SettingsPresenter(ISettingsView View, ISettingsModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventRefreshDisplay += EventRefreshDisplay;
        }

        #region Method definitions called by the view (downstream)
        

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventRefreshDisplay(Object Sender, SettingsEventArgs E)
        {
            View.RefreshDisplay();
        }

        #endregion
    }
}