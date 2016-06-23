using Anathema.Source.Engine.Processes;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUIProcessSelector : DockContent, IProcessSelectorView
    {
        private ProcessSelectorPresenter ProcessSelectorPresenter;
        private Object AccessLock;

        // Column text alignment. Without this the column title lines up with the icon rather than text
        private const string Alignment = "     ";

        public GUIProcessSelector()
        {
            InitializeComponent();

            // Set custom properties for our process List View
            ProcessListView.Columns.Add(Alignment + "Processes");
            ProcessListView.View = View.Details;

            // Initialize presenter
            ProcessSelectorPresenter = new ProcessSelectorPresenter(this, ProcessSelector.GetInstance());
            AccessLock = new Object();

            // Initialize process list
            RefreshProcesses();
        }

        public void SelectProcess(Process TargetProcess)
        {
            ControlThreadingHelper.InvokeControlAction(this, () =>
            {
                // May potentially use target process in the future if we enable multi-process selection

                this.Close();
            });
        }

        public void DisplayProcesses(IEnumerable<ListViewItem> Items, ImageList ImageList)
        {
            ControlThreadingHelper.InvokeControlAction(ProcessListView, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    // Clear the old items in the process list
                    ProcessListView.Items.Clear();

                    // Add all of the new items
                    Items?.ForEach(X => ProcessListView.Items.Add(X));
                    ProcessListView.SmallImageList = ImageList;
                }
            });
        }

        private void TrySelectingProcess()
        {
            ControlThreadingHelper.InvokeControlAction(ProcessListView, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (ProcessListView.SelectedIndices.Count <= 0)
                        return;

                    try
                    {
                        ProcessSelectorPresenter.SelectProcess(ProcessListView.SelectedIndices[0]);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message, "Error making selection.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            });
        }

        private void HandleResize()
        {
            ProcessListView.Columns[0].Width = ProcessListView.Width - 24;
        }

        private void RefreshProcesses()
        {
            ProcessSelectorPresenter.RefreshProcesses(this.Handle);
        }

        #region Events

        private void SelectProcessToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            TrySelectingProcess();
        }

        private void ProcessListView_DoubleClick(Object Sender, EventArgs E)
        {
            TrySelectingProcess();
        }

        private void RefreshToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            RefreshProcesses();
        }

        private void GUIProcessSelector_Resize(Object Sender, EventArgs E)
        {
            HandleResize();
        }

        #endregion

    } // End class

} // End namespace