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

                AddContextMenuToNodes(InputTreeView.Nodes);

                if (ContainsNode(InputTreeView.Nodes, SelectedNode))
                    InputTreeView.SelectedNode = SelectedNode;
            });
        }

        private void AddContextMenuToNodes(TreeNodeCollection Nodes)
        {
            foreach (TreeNode Node in Nodes)
            {
                Node.ContextMenuStrip = InputContextMenuStrip;
                AddContextMenuToNodes(Node.Nodes);
            }
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
            //VariableSizeTrackBar.Width = this.Width / 2 - VariableSizeTrackBar.Location.X;
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

        private void AddInputButton_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.AddInput(GetSelectionIndicies());
        }

        private void DeleteNodeButton_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.DeleteNode(GetSelectionIndicies());
        }

        private void ClearInputsButton_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.ClearNodes();
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

        private void InputContextMenuStrip_Opening(Object Sender, CancelEventArgs E)
        {
            if (InputTreeView.SelectedNode == null)
                E.Cancel = true;
        }

        private void DeleteToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.DeleteNode(GetSelectionIndicies());
        }

        private void GUILabelerInputCorrelator_Resize(Object Sender, EventArgs E)
        {
            HandleResize();
        }

        #endregion

    } // End class

} // End namespace