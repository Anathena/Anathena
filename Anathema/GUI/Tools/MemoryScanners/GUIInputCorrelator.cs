using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Gma.System.MouseKeyHook;

namespace Anathema
{
    public partial class GUIInputCorrelator : DockContent, IInputCorrelatorView
    {
        private InputCorrelatorPresenter InputCorrelatorPresenter;

        private readonly IKeyboardMouseEvents InputHook;    // Input capturing class
        private Boolean UpdatingInputTextBox;

        public GUIInputCorrelator()
        {
            InitializeComponent();

            InputCorrelatorPresenter = new InputCorrelatorPresenter(this, new InputCorrelator());

            InputHook = Hook.GlobalEvents();

            UpdatingInputTextBox = false;

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

        public void UpdateDisplay(TreeNode Root)
        {
            ControlThreadingHelper.InvokeControlAction(InputTreeView, () =>
            {
                // Save selection
                TreeNode SelectedNode = InputTreeView.SelectedNode;

                this.InputTreeView.Nodes.Clear();
                if (Root != null)
                    this.InputTreeView.Nodes.Add(Root);

                if (ContainsNode(InputTreeView.Nodes, SelectedNode))
                    InputTreeView.SelectedNode = SelectedNode;
            });
        }

        private Boolean ContainsNode(TreeNodeCollection Nodes, TreeNode TargetNode)
        {
            foreach (TreeNode Node in Nodes)
            {
                if (Node == TargetNode)
                    return true;

                if (ContainsNode(Node.Nodes, TargetNode))
                    return true;
            }
            return false;
        }

        private Stack<Int32> GetSelectionIndicies()
        {
            Stack<Int32> Indicies = new Stack<Int32>();
            TreeNode CurrentNode = InputTreeView.SelectedNode;
            while (CurrentNode != null)
            {
                Indicies.Push(CurrentNode.Index);
                CurrentNode = CurrentNode.Parent;
            }

            return Indicies;
        }

        private void GlobalHookKeyUp(Object Sender, KeyEventArgs E)
        {
            InputCorrelatorPresenter.SetCurrentKey(E.KeyCode);
            UpdatingInputTextBox = true;
            InputTextBox.Text = E.KeyCode.ToString();
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

        private void InputTextBox_Enter(Object Sender, EventArgs E)
        {
            InputHook.KeyUp += GlobalHookKeyUp;
        }

        private void InputTextBox_Leave(object sender, EventArgs e)
        {
            InputHook.KeyUp -= GlobalHookKeyUp;
        }

        private void InputTextBox_TextChanged(Object Sender, EventArgs E)
        {
            if (!UpdatingInputTextBox)
                InputTextBox.Text = String.Empty;
            else
                UpdatingInputTextBox = false;
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
            InputCorrelatorPresenter.AddInput(GetSelectionIndicies());
        }

        private void DeleteNodeButton_Click(object sender, EventArgs e)
        {
            InputCorrelatorPresenter.DeleteNode(GetSelectionIndicies());
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

        private void InputTreeView_Leave(object sender, EventArgs e)
        {
            InputTreeView.SelectedNode.BackColor = SystemColors.Highlight;
            InputTreeView.SelectedNode.ForeColor = SystemColors.HighlightText;
        }

        private void InputTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (InputTreeView.SelectedNode == null)
                return;

            InputTreeView.SelectedNode.BackColor = InputTreeView.BackColor;
            InputTreeView.SelectedNode.ForeColor = SystemColors.ControlText;
        }

        private void GUILabelerInputCorrelator_Resize(Object Sender, EventArgs E)
        {
            HandleResize();
        }

        #endregion

    } // End class

} // End namespace