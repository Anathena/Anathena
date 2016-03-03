﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Anathema.Utils.MVP;

namespace Anathema
{
    public partial class GUIResults : DockContent, IResultsView
    {
        private ResultsPresenter ResultsPresenter;

        public GUIResults()
        {
            InitializeComponent();
            ResultsPresenter = new ResultsPresenter(this, Results.GetInstance());
        }

        public void UpdateMemorySizeLabel(String MemorySize, String ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(GUIToolStrip, () =>
            {
                SnapshotSizeValueLabel.Text = MemorySize + " - (" + ItemCount + ")";
            });
        }

        public void UpdateItemCount(Int32 ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                ResultsListView.SetItemCount(ItemCount);
            });
        }

        private void UpdateReadBounds()
        {
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                Tuple<Int32, Int32> ReadBounds = ResultsListView.GetReadBounds();
                ResultsPresenter.UpdateReadBounds(ReadBounds.Item1, ReadBounds.Item2);
            });
        }

        public void EnableResults()
        {
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                ResultsListView.Enabled = true;
            });

            ControlThreadingHelper.InvokeControlAction(GUIToolStrip, () =>
            {
                GUIToolStrip.Enabled = true;
            });
        }

        public void DisableResults()
        {
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                ResultsListView.Enabled = false;
            });
            ControlThreadingHelper.InvokeControlAction(GUIToolStrip, () =>
            {
                GUIToolStrip.Enabled = false;
            });
        }

        public void ReadValues()
        {
            UpdateReadBounds();

            // Force the list view to retrieve items again by signaling an update
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                ResultsListView.BeginUpdate();
                ResultsListView.EndUpdate();
            });
        }

        private void AddSelectedElements()
        {
            if (ResultsListView.SelectedIndices.Count <= 0)
                return;

            ResultsPresenter.AddSelectionToTable(ResultsListView.SelectedIndices[0], ResultsListView.SelectedIndices[ResultsListView.SelectedIndices.Count - 1]);
        }

        #region Events

        private void ByteToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.UpdateScanType(typeof(SByte));
        }

        private void Int16ToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.UpdateScanType(typeof(Int16));
        }

        private void Int32ToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.UpdateScanType(typeof(Int32));
        }

        private void Int64ToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.UpdateScanType(typeof(Int64));
        }

        private void SingleToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.UpdateScanType(typeof(Single));
        }

        private void DoubleToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.UpdateScanType(typeof(Double));
        }

        private void ChangeSignToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ResultsPresenter.ChangeSign();
        }

        private void ResultsListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {
            E.Item = ResultsPresenter.GetItemAt(E.ItemIndex);
        }

        private void AddSelectedResultsButton_Click(Object Sender, EventArgs E)
        {
            AddSelectedElements();
        }

        private void ResultsListView_DoubleClick(Object Sender, EventArgs E)
        {
            AddSelectedElements();
        }

        private void AddToCheatsToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            AddSelectedElements();
        }

        #endregion

    } // End class

} // End mamespace