using Anathena.Source.Engine.Processes;
using Anathena.Source.Utils;
using Anathena.Source.Utils.Extensions;
using Anathena.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools
{
    public partial class GUIProcessSelector : DockContent, IProcessSelectorView
    {
        private ProcessSelectorPresenter processSelectorPresenter;
        private Object accessLock;

        // Column text alignment. Without this the column title lines up with the icon rather than text
        private const String alignment = "     ";

        public GUIProcessSelector()
        {
            InitializeComponent();

            // Set custom properties for our process List View
            ProcessListView.Columns.Add(alignment + "Processes");
            ProcessListView.View = View.Details;

            // Initialize presenter
            processSelectorPresenter = new ProcessSelectorPresenter(this, ProcessSelector.GetInstance());
            accessLock = new Object();

            // Initialize process list
            RefreshProcesses();
        }

        public void SelectProcess(Process targetProcess)
        {
            ControlThreadingHelper.InvokeControlAction(this, () =>
            {
                // May potentially use target process in the future if we enable multi-process selection

                this.Close();
            });
        }

        public void DisplayProcesses(IEnumerable<ListViewItem> items, ImageList imageList)
        {
            ControlThreadingHelper.InvokeControlAction(ProcessListView, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    // Clear the old items in the process list
                    ProcessListView.Items.Clear();

                    // Add all of the new items
                    items?.ForEach(x => ProcessListView.Items.Add(x));
                    ProcessListView.SmallImageList = imageList;
                }
            });
        }

        private void TrySelectingProcess()
        {
            ControlThreadingHelper.InvokeControlAction(ProcessListView, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (ProcessListView.SelectedIndices.Count <= 0)
                        return;

                    try
                    {
                        processSelectorPresenter.SelectProcess(ProcessListView.SelectedIndices[0]);
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
            processSelectorPresenter.RefreshProcesses(this.Handle);
        }

        #region Events

        private void SelectProcessToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            TrySelectingProcess();
        }

        private void ProcessListView_DoubleClick(Object sender, EventArgs e)
        {
            TrySelectingProcess();
        }

        private void RefreshToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            RefreshProcesses();
        }

        private void RefreshButton_Click(Object sender, EventArgs e)
        {
            RefreshProcesses();
        }

        private void GUIProcessSelector_Resize(Object sender, EventArgs e)
        {
            HandleResize();
        }

        #endregion

    } // End class

} // End namespace