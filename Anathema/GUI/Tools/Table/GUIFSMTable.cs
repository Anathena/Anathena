using Anathema.User.UserFSMTable;
using Anathema.Utils.Cache;
using Anathema.Utils.MVP;
using System;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUIFSMTable : UserControl, IFSMTableView
    {
        private FSMTablePresenter FSMTablePresenter;
        private ListViewCache FSMTableCache;

        public GUIFSMTable()
        {
            InitializeComponent();

            FSMTablePresenter = new FSMTablePresenter(this, FSMTable.GetInstance());
            FSMTableCache = new ListViewCache();
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