using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Anathema.Properties;

namespace Anathema
{
    public partial class GUIFilterFSM : DockContent, IFilterFSMView
    {
        private FilterFSMPresenter FilterFSMPresenter;

        private List<ToolStripButton> ScanOptionButtons;

        private List<GraphicalState> States;
        private GraphicalState PendingState;

        public GUIFilterFSM()
        {
            InitializeComponent();

            FSMBuilderPanel.Paint += new PaintEventHandler(FSMBuilderPanel_Paint);

            FilterFSMPresenter = new FilterFSMPresenter(this, new FilterFSM());

            ScanOptionButtons = new List<ToolStripButton>();
            States = new List<GraphicalState>();

            InitializeValueTypeComboBox();
            InitializeScanOptionButtons();
            EvaluateScanOptions(EqualButton);
        }
        private void InitializeValueTypeComboBox()
        {
            foreach (Type Primitive in PrimitiveTypes.GetPrimitiveTypes())
                ValueTypeComboBox.Items.Add(Primitive.Name);

            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(typeof(Int32).Name);
        }

        private void InitializeScanOptionButtons()
        {
            ScanOptionButtons.Add(UnchangedButton);
            ScanOptionButtons.Add(ChangedButton);
            ScanOptionButtons.Add(IncreasedButton);
            ScanOptionButtons.Add(DecreasedButton);
            ScanOptionButtons.Add(NotEqualButton);
            ScanOptionButtons.Add(EqualButton);
            ScanOptionButtons.Add(GreaterThanButton);
            ScanOptionButtons.Add(LessThanButton);
            ScanOptionButtons.Add(IncreasedByXButton);
            ScanOptionButtons.Add(DecreasedByXButton);
        }

        public void UpdateDisplay(List<String[]> ScanConstraintItems)
        {
            //ConstraintsListView.Items.Clear();
            //foreach (String[] Item in ScanConstraintItems)
            // ConstraintsListView.Items.Add(new ListViewItem(Item));
        }

        private void EvaluateScanOptions(ToolStripButton Sender)
        {
            ConstraintsEnum ValueConstraint = ConstraintsEnum.Invalid;

            foreach (ToolStripButton Button in ScanOptionButtons)
                if (Button != Sender)
                    Button.Checked = false;
                else
                    Button.Checked = true;

            if (Sender == EqualButton)
            {
                ValueConstraint = ConstraintsEnum.Equal;
            }
            else if (Sender == NotEqualButton)
            {
                ValueConstraint = ConstraintsEnum.NotEqual;
            }
            else if (Sender == ChangedButton)
            {
                ValueConstraint = ConstraintsEnum.Changed;
            }
            else if (Sender == UnchangedButton)
            {
                ValueConstraint = ConstraintsEnum.Unchanged;
            }
            else if (Sender == IncreasedButton)
            {
                ValueConstraint = ConstraintsEnum.Increased;
            }
            else if (Sender == DecreasedButton)
            {
                ValueConstraint = ConstraintsEnum.Decreased;
            }
            else if (Sender == GreaterThanButton)
            {
                ValueConstraint = ConstraintsEnum.GreaterThan;
            }
            else if (Sender == LessThanButton)
            {
                ValueConstraint = ConstraintsEnum.LessThan;
            }
            else if (Sender == IncreasedByXButton)
            {
                ValueConstraint = ConstraintsEnum.IncreasedByX;
            }
            else if (Sender == DecreasedByXButton)
            {
                ValueConstraint = ConstraintsEnum.DecreasedByX;
            }

            switch (ValueConstraint)
            {
                case ConstraintsEnum.Changed:
                case ConstraintsEnum.Unchanged:
                case ConstraintsEnum.Decreased:
                case ConstraintsEnum.Increased:
                    ValueTextBox.Enabled = false;
                    ValueTextBox.Text = "";
                    break;
                case ConstraintsEnum.Invalid:
                case ConstraintsEnum.GreaterThan:
                case ConstraintsEnum.LessThan:
                case ConstraintsEnum.Equal:
                case ConstraintsEnum.NotEqual:
                case ConstraintsEnum.IncreasedByX:
                case ConstraintsEnum.DecreasedByX:
                    ValueTextBox.Enabled = true;
                    break;
            }

            if (Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString()) == typeof(Single) ||
                Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString()) == typeof(Double))
            {
                //FilterScientificNotationCheckBox.Visible = true;
            }
            else
            {
                //FilterScientificNotationCheckBox.Visible = false;
            }

            FilterFSMPresenter.SetValueConstraints(ValueConstraint);
            //this.CompareTypeLabel.Text = ValueConstraint.ToString();
        }

        private Boolean IsCurrentScanSetting(params ToolStripButton[] Controls)
        {
            List<ToolStripButton> CandidateControls = new List<ToolStripButton>(Controls);

            // Controls in consideration must be checked
            foreach (ToolStripButton Button in CandidateControls)
                if (!Button.Checked)
                    return false;

            // Controls not in consideration must not be checked.
            foreach (ToolStripButton Button in ScanOptionButtons)
            {
                if (CandidateControls.Contains(Button))
                    continue;

                if (Button.Checked)
                    return false;
            }

            return true;
        }

        #region Events

        protected void FSMBuilderPanel_Paint(Object Sender, PaintEventArgs E)
        {
            foreach (GraphicalState State in States)
            {
                GraphicalState.StyleEnum Style;
                if (States.IndexOf(State) == 0)
                    Style = GraphicalState.StyleEnum.StartState;
                else if (States.IndexOf(State) == States.Count - 1)
                    Style = GraphicalState.StyleEnum.EndState;
                else
                    Style = GraphicalState.StyleEnum.IntermediateState;

                State.Draw(E.Graphics, Style);

            }

            if (PendingState != null)
                PendingState.Draw(E.Graphics, States.Count == 0 ? GraphicalState.StyleEnum.StartState : GraphicalState.StyleEnum.IntermediateState);


        }

        private void FSMBuilderPanel_MouseUp(Object Sender, MouseEventArgs E)
        {
            if (PendingState != null)
            {
                PendingState.SetLocation(E.Location);
                States.Add(PendingState);
                PendingState = null;

                FSMBuilderPanel.Invalidate();
            }
        }

        private void FSMBuilderPanel_MouseMove(Object Sender, MouseEventArgs E)
        {
            if (PendingState != null)
            {
                PendingState.SetLocation(E.Location);
                FSMBuilderPanel.Invalidate();
            }
            foreach (GraphicalState State in States)
            {
                if (State.IsEdgeMousedOver(E.Location))
                {
                    FSMBuilderPanel.Invalidate();
                }
            }
        }

        private void FSMBuilderPanel_MouseDown(Object Sender, MouseEventArgs E)
        {

            if (PendingState == null)
            {
                foreach (GraphicalState State in States)
                {
                    if (State.IsMousedOver(E.Location))
                    {
                        PendingState = State;
                        States.Remove(State);
                        break;
                    }
                }
            }

            if (PendingState == null)
                PendingState = new GraphicalState(E.Location);
        }

        private void FSMBuilderPanel_MouseClick(Object Sender, MouseEventArgs E)
        {

        }

        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            FilterFSMPresenter.BeginScan();
        }

        private void ChangedButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void NotEqualButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void UnchangedButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void IncreasedButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void DecreasedButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void EqualButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void GreaterThanButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void LessThanButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void IncreasedByXButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void DecreasedByXButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void AddConstraintButton_Click(Object Sender, EventArgs E)
        {
            FilterFSMPresenter.AddConstraint(ValueTextBox.Text.ToString());
        }

        private void ClearConstraintsButton_Click(Object Sender, EventArgs E)
        {
            FilterFSMPresenter.ClearConstraints();
        }

        private void RemoveConstraintButton_Click(Object Sender, EventArgs E)
        {
            //FilterFSMPresenter.RemoveConstraints(ConstraintsListView.SelectedIndices.Cast<Int32>().ToArray());
        }

        private void ValueTypeComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {
            FilterFSMPresenter.SetElementType(ValueTypeComboBox.SelectedItem.ToString());
        }

        #endregion

    } // End class

    class GraphicalState
    {
        public enum StyleEnum
        {
            StartState,
            IntermediateState,
            EndState
        }

        private const Int32 SelectionWidth = 24;
        private Point Location;
        private Boolean MousedOver;

        public GraphicalState(Point Location)
        {
            this.Location = Location;
        }

        public void SetLocation(Point Location)
        {
            this.Location = Location;
        }

        public Boolean IsMousedOver(Point MouseLocation)
        {
            Single Distance = (Single)Math.Sqrt((MouseLocation.X - Location.X) * (MouseLocation.X - Location.X) + (MouseLocation.Y - Location.Y) * (MouseLocation.Y - Location.Y));

            if (Distance < Resources.StateHighlighted.Width - SelectionWidth)
                return true;
            return false;
        }

        public Boolean IsEdgeMousedOver(Point MouseLocation)
        {
            Boolean NewMouseOverState = false;

            Single Distance = (Single)Math.Sqrt((MouseLocation.X - Location.X) * (MouseLocation.X - Location.X) + (MouseLocation.Y - Location.Y) * (MouseLocation.Y - Location.Y));

            if (Distance <= Resources.StateHighlighted.Width && Distance >= Resources.StateHighlighted.Width - SelectionWidth)
                NewMouseOverState = true;

            if (NewMouseOverState != MousedOver)
            {
                MousedOver = NewMouseOverState;
                return true;
            }

            MousedOver = NewMouseOverState;
            return false;
        }

        // Paint ourselves with the specified Graphics object
        public void Draw(Graphics Graphics, StyleEnum Style)
        {
            Image DrawImage;
            switch (Style)
            {
                case StyleEnum.StartState:
                    DrawImage = Resources.StartState;
                    break;
                case StyleEnum.EndState:
                    DrawImage = Resources.EndState;
                    break;
                default:
                case StyleEnum.IntermediateState:
                    DrawImage = Resources.IntermediateState;
                    break;
            }
            Graphics.DrawImage(DrawImage, Location.X - Resources.StartState.Width / 2, Location.Y - Resources.StartState.Height / 2);
            if (MousedOver)
                Graphics.DrawImage(Resources.StateHighlighted, Location.X - Resources.StartState.Width / 2, Location.Y - Resources.StartState.Height / 2);
        }
    }
} // End namespace