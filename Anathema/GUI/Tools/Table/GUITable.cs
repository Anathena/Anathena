using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Anathema
{
    public partial class GUITable : DockContent, ITableView
    {
        private TablePresenter TablePresenter;

        public GUITable()
        {
            InitializeComponent();
            TablePresenter = new TablePresenter(this, Table.GetInstance());

            ViewCheatTable();
        }
        
        private void ViewCheatTable()
        {
            CheatTableButton.Checked = true;
            FSMTableButton.Checked = false;
            CheatTableSplitContainer.Visible = true;
            GUIFSMTable.Visible = false;
        }

        private void ViewFSMTable()
        {
            CheatTableButton.Checked = false;
            FSMTableButton.Checked = true;
            CheatTableSplitContainer.Visible = false;
            GUIFSMTable.Visible = true;
        }

        public void BeginSaveTable()
        {
            SaveFileDialog SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.Filter = "Anathema Table | *.ana";
            SaveFileDialog.Title = "Save Cheat Table";
            SaveFileDialog.ShowDialog();

            TablePresenter.SaveTable(SaveFileDialog.FileName);
        }

        public void BeginOpenTable()
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Filter = "Anathema Table | *.ana";
            OpenFileDialog.Title = "Open Cheat Table";
            OpenFileDialog.ShowDialog();

            TablePresenter.LoadTable(OpenFileDialog.FileName);
        }

        #region Events

        private Point LastRightClickLocation = Point.Empty;

        private void SaveTableButton_Click(Object Sender, EventArgs E)
        {
            BeginSaveTable();
        }

        private void LoadTableButton_Click(Object Sender, EventArgs E)
        {
            BeginOpenTable();
        }
        
        private void CheatTableButton_Click(Object Sender, EventArgs E)
        {
            ViewCheatTable();
        }

        private void FSMTableButton_Click(Object Sender, EventArgs E)
        {
            ViewFSMTable();
        }

        private void AddAddressButton_Click(Object Sender, EventArgs E)
        {
            
        }
        
        #endregion

    } // End class

} // End namespace