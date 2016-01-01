using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace Anathema
{
    public partial class GUITable : DockContent, ITableView
    {
        private TablePresenter TablePresenter;

        public GUITable()
        {
            InitializeComponent();

            TablePresenter = new TablePresenter(this, new Table());
        }

        public void RefreshResults()
        {
            AddressTableListView.BeginUpdate();
            AddressTableListView.EndUpdate();

            ScriptTableListView.BeginUpdate();
            ScriptTableListView.EndUpdate();
        }

        private void SaveTableButton_Click(Object Sender, EventArgs E)
        {

        }

        private void LoadTableButton_Click(Object Sender, EventArgs E)
        {

        }

        private void AddressTableListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {

        }

        private void ScriptTableListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {

        }
    }
}
