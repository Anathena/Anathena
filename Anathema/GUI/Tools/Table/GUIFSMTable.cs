using System;
using System.Windows.Forms;
using Anathema.Utils.MVP;
using Anathema.User.UserFSMTable;

namespace Anathema
{
    public partial class GUIFSMTable : UserControl, IFSMTableView
    {
        private FSMTablePresenter FSMTablePresenter;

        public GUIFSMTable()
        {
            InitializeComponent();

            FSMTablePresenter = new FSMTablePresenter(this, FSMTable.GetInstance());
        }

        public void UpdateFSMTableItemCount(Int32 ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(FSMTableListView, () =>
            {
                FSMTableListView.BeginUpdate();
                FSMTableListView.SetItemCount(ItemCount);
                FSMTableListView.EndUpdate();
            });
        }

        #region Events

        private void FSMTableListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {

        }

        #endregion

    } // End class

} // End namespace