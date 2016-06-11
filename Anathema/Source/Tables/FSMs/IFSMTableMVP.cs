using Anathema.Source.SystemInternals.Processes;
using Anathema.Source.Scanners.FiniteStateScanner;
using Anathema.Source.Utils.MVP;
using System;
using System.Windows.Forms;

namespace Anathema.Source.Tables.FSMs
{
    delegate void FSMTableEventHandler(Object Sender, FSMTableEventArgs Args);
    class FSMTableEventArgs : EventArgs
    {
        public Int32 ItemCount = 0;
        public Int32 ClearCacheIndex = -1;
    }

    interface IFSMTableView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateFSMTableItemCount(Int32 ItemCount);
    }

    interface IFSMTableModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
        Boolean SaveTable(String Path);
        Boolean LoadTable(String Path);

        void OpenFSM(Int32 Index);

        FiniteStateMachine GetFSMItemAt(Int32 Index);
    }

    class FSMTablePresenter : Presenter<IFSMTableView, IFSMTableModel>
    {
        private new IFSMTableView View { get; set; }
        private new IFSMTableModel Model { get; set; }

        public FSMTablePresenter(IFSMTableView View, IFSMTableModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model


            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public Boolean SaveTable(String Path)
        {
            if (Path == String.Empty)
                return false;

            return Model.SaveTable(Path);
        }

        public Boolean LoadTable(String Path)
        {
            if (Path == String.Empty)
                return false;

            return Model.LoadTable(Path);
        }

        public ListViewItem GetFSMTableItemAt(Int32 Index)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion

    } // End class

} // End namespace