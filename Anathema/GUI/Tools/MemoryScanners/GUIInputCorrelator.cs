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
    public partial class GUIInputCorrelator : DockContent, IInputCorrelatorView
    {
        InputCorrelatorPresenter InputCorrelatorPresenter;

        public GUIInputCorrelator()
        {
            InitializeComponent();

            InputCorrelatorPresenter = new InputCorrelatorPresenter(this, new InputCorrelator());

            SetVariableSize();
            EnableGUI();
        }

        private void SetVariableSize()
        {
            Int32 VariableSize = (Int32)Math.Pow(2, VariableSizeTrackBar.Value);
            VariableSizeValueLabel.Text = Conversions.BytesToMetric((UInt64)VariableSize).ToString();

            InputCorrelatorPresenter.SetVariableSize(VariableSize);
        }

        private void EnableGUI()
        {
            StartScanButton.Enabled = true;
            StopScanButton.Enabled = false;
            VariableSizeTrackBar.Enabled = true;
        }

        private void DisableGUI()
        {
            StartScanButton.Enabled = false;
            StopScanButton.Enabled = true;
            VariableSizeTrackBar.Enabled = false;
        }

        private Stack<Int32> GetSelectionIndicies()
        {
            Stack<Int32> Indicies = new Stack<Int32>();
            TreeNode ParentNode;
            TreeNode CurrentNode = InputTreeView.SelectedNode;
            while (CurrentNode != null)
            {
                ParentNode = CurrentNode.Parent;
                Indicies.Push(ParentNode.Nodes.IndexOf(CurrentNode));
                CurrentNode = ParentNode;
            }

            return Indicies;
        }

        #region Events

        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            DisableGUI();
            InputCorrelatorPresenter.BeginScan();
        }

        private void StopScanButton_Click(Object Sender, EventArgs E)
        {
            EnableGUI();
            InputCorrelatorPresenter.EndScan();
        }

        private void HandleResize()
        {
            VariableSizeTrackBar.Width = this.Width / 2 - VariableSizeTrackBar.Location.X;
        }

        private void VariableSizeTrackBar_Scroll(Object Sender, EventArgs E)
        {
            SetVariableSize();
        }

        private void VariableToUserRadioButton_CheckedChanged(Object Sender, EventArgs E)
        {

        }

        private void UserToVariableRadioButton_CheckedChanged(Object Sender, EventArgs E)
        {

        }

        private void EitherRadioButton_CheckedChanged(Object Sender, EventArgs E)
        {

        }

        private void AddInputButton_Click(Object Sender, EventArgs E)
        {

        }

        private void AddANDButton_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.AddAND(GetSelectionIndicies());
        }

        private void AddORButton_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.AddOR(GetSelectionIndicies());
        }

        private void AddNOTButton_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.AddNOT(GetSelectionIndicies());
        }

        private void InputTextBox_TextChanged(Object Sender, EventArgs E)
        {
            InputTextBox.Text = String.Empty;
        }

        private void GUILabelerInputCorrelator_Resize(Object Sender, EventArgs E)
        {
            HandleResize();
        }

        #endregion

    }
}
