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

        private FiniteStateMachine FiniteStateMachine;
        private FiniteState MousedOverState;
        private Point[] SelectionLine;

        // Drawing Variables:
        private static Font DrawFont = new Font(FontFamily.GenericSerif, 10.0f);
        private static Pen TransitionLine = new Pen(Color.Black, 3);
        private static Int32 StateRadius = Resources.StateHighlighted.Width / 2;
        private const Int32 StateEdgeSize = 8;
        private static Int32 LineOffset = (Int32)TransitionLine.Width / 2;
        private const Int32 LineFloatOffset = 8;
        private const Int32 VariableBorderSize = 4;
        private const Int32 ArrowSize = 4;

        public GUIFilterFSM()
        {
            InitializeComponent();
            FSMBuilderPanel.Paint += new PaintEventHandler(FSMBuilderPanel_Paint);

            FilterFSMPresenter = new FilterFSMPresenter(this, new FilterFSM());
            FilterFSMPresenter.SetStateRadius(StateRadius);
            FilterFSMPresenter.SetStateEdgeSize(StateEdgeSize);

            ScanOptionButtons = new List<ToolStripButton>();

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

        public void UpdateDisplay(FiniteStateMachine FiniteStateMachine, FiniteState MousedOverState, Point[] SelectionLine)
        {
            this.FiniteStateMachine = FiniteStateMachine;
            this.MousedOverState = MousedOverState;
            this.SelectionLine = SelectionLine;
            FSMBuilderPanel.Invalidate();
        }

        private void HandleMouseDown(Point Location)
        {
            FilterFSMPresenter.BeginAction(Location);
        }

        private void HandleMouseMove(Point Location)
        {
            FilterFSMPresenter.UpdateAction(Location);
        }

        private void HandleMouseUp(Point Location)
        {
            FilterFSMPresenter.FinishAction(Location, ValueTextBox.Text.ToString());
        }

        private void HandleRightClick(Point Location)
        {
            FilterFSMPresenter.DeleteAtPoint(Location);
        }

        private void Draw(Graphics Graphics)
        {
            if (FiniteStateMachine == null)
                return;

            foreach (FiniteState State in FiniteStateMachine)
            {
                Image DrawImage;

                DrawImage = Resources.IntermediateState;

                Graphics.DrawImage(DrawImage, State.Location.X - StateRadius, State.Location.Y - StateRadius, DrawImage.Width, DrawImage.Height);
            }

            if (MousedOverState != null)
                Graphics.DrawImage(Resources.StateHighlighted, MousedOverState.Location.X - StateRadius, MousedOverState.Location.Y - StateRadius, Resources.StateHighlighted.Width, Resources.StateHighlighted.Height);

            try
            {
                if (SelectionLine != null && SelectionLine.Length == 2)
                    Graphics.DrawLine(Pens.Red, SelectionLine[0], SelectionLine[1]);
            }
            catch { /* Overflow error. This may lead to a recursive draw call. Not sure how to fix */ }

            foreach (FiniteState State in FiniteStateMachine)
            {
                foreach (KeyValuePair<ScanConstraint, FiniteState> Transition in State)
                {
                    // Calculate start and end points of the transitio line
                    Point StartPoint = State.GetEdgePoint(Transition.Value.Location, StateRadius);
                    Point EndPoint = Transition.Value.GetEdgePoint(State.Location, StateRadius);
                    StartPoint.Y += LineOffset;
                    EndPoint.Y += LineOffset;

                    // Draw transition line
                    Point MidPoint = new Point((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
                    Graphics.DrawLine(TransitionLine, StartPoint, EndPoint);

                    // Draw arrow head
                    //Point[] ArrowHeadPoints = new Point[3];
                    //ArrowHeadPoints[0] = EndPoint;
                    //ArrowHeadPoints[1] = EndPoint;
                    //ArrowHeadPoints[2] = EndPoint;
                    Graphics.FillEllipse(Brushes.Black, EndPoint.X - ArrowSize, EndPoint.Y - ArrowSize, ArrowSize * 2, ArrowSize * 2);

                    // Draw comparison image
                    Point ImageLocation = new Point(MidPoint.X - Resources.Equal.Width / 2, MidPoint.Y - Resources.Equal.Height - LineFloatOffset);
                    switch (Transition.Key.Constraint)
                    {
                        case ConstraintsEnum.Changed:
                            Graphics.DrawImage(Resources.Changed, ImageLocation.X, ImageLocation.Y, Resources.Changed.Width, Resources.Changed.Height);
                            break;
                        case ConstraintsEnum.Unchanged:
                            Graphics.DrawImage(Resources.Unchanged, ImageLocation.X, ImageLocation.Y, Resources.Unchanged.Width, Resources.Unchanged.Height);
                            break;
                        case ConstraintsEnum.Decreased:
                            Graphics.DrawImage(Resources.Decreased, ImageLocation.X, ImageLocation.Y, Resources.Decreased.Width, Resources.Decreased.Height);
                            break;
                        case ConstraintsEnum.Increased:
                            Graphics.DrawImage(Resources.Increased, ImageLocation.X, ImageLocation.Y, Resources.Increased.Width, Resources.Increased.Height);
                            break;
                        case ConstraintsEnum.GreaterThan:
                            Graphics.DrawImage(Resources.GreaterThan, ImageLocation.X, ImageLocation.Y, Resources.GreaterThan.Width, Resources.GreaterThan.Height);
                            break;
                        case ConstraintsEnum.LessThan:
                            Graphics.DrawImage(Resources.LessThan, ImageLocation.X, ImageLocation.Y, Resources.LessThan.Width, Resources.LessThan.Height);
                            break;
                        case ConstraintsEnum.Equal:
                            Graphics.DrawImage(Resources.Equal, ImageLocation.X, ImageLocation.Y, Resources.Equal.Width, Resources.Equal.Height);
                            break;
                        case ConstraintsEnum.NotEqual:
                            Graphics.DrawImage(Resources.NotEqual, ImageLocation.X, ImageLocation.Y, Resources.NotEqual.Width, Resources.NotEqual.Height);
                            break;
                        case ConstraintsEnum.IncreasedByX:
                            Graphics.DrawImage(Resources.PlusX, ImageLocation.X, ImageLocation.Y, Resources.PlusX.Width, Resources.PlusX.Height);
                            break;
                        case ConstraintsEnum.DecreasedByX:
                            Graphics.DrawImage(Resources.MinusX, ImageLocation.X, ImageLocation.Y, Resources.MinusX.Width, Resources.MinusX.Height);
                            break;
                        default:
                        case ConstraintsEnum.Invalid:
                            break;
                    }

                    // Draw transition value if applicable
                    if (Transition.Key.Value != null)
                    {
                        String DrawText = Transition.Key.Value.ToString();
                        SizeF TextSize = Graphics.MeasureString(DrawText, DrawFont);
                        PointF TextLocation = new PointF(MidPoint.X - TextSize.Width / 2, MidPoint.Y + LineFloatOffset);
                        Graphics.FillEllipse(Brushes.Black, TextLocation.X - VariableBorderSize, TextLocation.Y - VariableBorderSize, TextSize.Width + VariableBorderSize * 2, TextSize.Height + VariableBorderSize);
                        Graphics.DrawString(DrawText, DrawFont, Brushes.White, TextLocation);
                    }
                }
            }
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
                ValueConstraint = ConstraintsEnum.Equal;
            else if (Sender == NotEqualButton)
                ValueConstraint = ConstraintsEnum.NotEqual;
            else if (Sender == ChangedButton)
                ValueConstraint = ConstraintsEnum.Changed;
            else if (Sender == UnchangedButton)
                ValueConstraint = ConstraintsEnum.Unchanged;
            else if (Sender == IncreasedButton)
                ValueConstraint = ConstraintsEnum.Increased;
            else if (Sender == DecreasedButton)
                ValueConstraint = ConstraintsEnum.Decreased;
            else if (Sender == GreaterThanButton)
                ValueConstraint = ConstraintsEnum.GreaterThan;
            else if (Sender == LessThanButton)
                ValueConstraint = ConstraintsEnum.LessThan;
            else if (Sender == IncreasedByXButton)
                ValueConstraint = ConstraintsEnum.IncreasedByX;
            else if (Sender == DecreasedByXButton)
                ValueConstraint = ConstraintsEnum.DecreasedByX;

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

            FilterFSMPresenter.SetValueConstraintSelection(ValueConstraint);
        }

        #region Events

        private void FSMBuilderPanel_MouseClick(Object Sender, MouseEventArgs E)
        {
            if (E.Button != MouseButtons.Right)
                return;

            HandleRightClick(E.Location);
        }

        private void FSMBuilderPanel_MouseUp(Object Sender, MouseEventArgs E)
        {
            if (E.Button != MouseButtons.Left)
                return;

            HandleMouseUp(E.Location);
        }

        private void FSMBuilderPanel_MouseMove(Object Sender, MouseEventArgs E)
        {
            HandleMouseMove(E.Location);
        }

        private void FSMBuilderPanel_MouseDown(Object Sender, MouseEventArgs E)
        {
            if (E.Button != MouseButtons.Left)
                return;

            HandleMouseDown(E.Location);
        }

        protected void FSMBuilderPanel_Paint(Object Sender, PaintEventArgs E)
        {
            Draw(E.Graphics);
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

        private void ValueTypeComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {
            FilterFSMPresenter.SetElementType(ValueTypeComboBox.SelectedItem.ToString());
        }

        #endregion

    } // End class

} // End namespace