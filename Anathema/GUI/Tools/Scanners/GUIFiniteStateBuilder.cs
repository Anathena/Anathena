using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Anathema.Properties;
using Anathema.Utils.MVP;
using Anathema.Scanners.ScanConstraints;
using Anathema.Scanners.FiniteStateScanner;

namespace Anathema.GUI
{
    public partial class GUIFiniteStateBuilder : UserControl, IFiniteStateBuilderView
    {
        // Drawing Variables:
        private static Font DrawFont = new Font(FontFamily.GenericSerif, 10.0f);
        private static Pen TransitionLine = new Pen(Color.Black, 3);
        private static Pen PendingLine = new Pen(Color.Red, 2);
        private static Int32 StateRadius = Resources.StateHighlighted.Width / 2;
        private static Int32 LineOffset = (Int32)TransitionLine.Width / 2;
        private const Int32 StateEdgeSize = 8;
        private const Int32 LineFloatOffset = 8;
        private const Int32 VariableBorderSize = 4;
        private const Int32 ArrowSize = 4;

        private Boolean BlockNextMouseEvent;

        private FiniteStateBuilderPresenter FiniteStateBuilderPresenter;

        public GUIFiniteStateBuilder()
        {
            InitializeComponent();

            FiniteStateBuilderPresenter = new FiniteStateBuilderPresenter(this, new FiniteStateBuilder());
            
            FiniteStateBuilderPresenter.SetStateRadius(StateRadius);
            FiniteStateBuilderPresenter.SetStateEdgeSize(StateEdgeSize);
            
            BlockNextMouseEvent = false;

            this.Paint += new PaintEventHandler(FSMBuilderPanel_Paint);
        }

        public void UpdateDisplay()
        {
            ControlThreadingHelper.InvokeControlAction(FSMBuilderPanel, () =>
            {
                FSMBuilderPanel.Invalidate();
            });
        }

        #region Events

        private void StateContextMenuStrip_Closed(Object Sender, ToolStripDropDownClosedEventArgs E)
        {
            if (E.CloseReason == ToolStripDropDownCloseReason.AppClicked)
            {
                if (FSMBuilderPanel.Bounds.Contains(FSMBuilderPanel.PointToClient(Cursor.Position)))
                    BlockNextMouseEvent = true;
            }
        }

        private void FSMBuilderPanel_MouseDown(Object Sender, MouseEventArgs E)
        {
            if (BlockNextMouseEvent)
            {
                BlockNextMouseEvent = false;
                return;
            }

            if (E.Button != MouseButtons.Left)
                return;

            FiniteStateBuilderPresenter.BeginAction(E.Location);
        }

        private void FSMBuilderPanel_MouseMove(Object Sender, MouseEventArgs E)
        {
            FiniteStateBuilderPresenter.UpdateAction(E.Location);
        }

        private void FSMBuilderPanel_MouseUp(Object Sender, MouseEventArgs E)
        {
            if (E.Button != MouseButtons.Left)
                return;

            FiniteStateBuilderPresenter.FinishAction(E.Location);
        }
        
        private void DragModeButton_Click(Object Sender, EventArgs E)
        {

        }

        private void StateContextMenuStrip_Opening(Object Sender, CancelEventArgs E)
        {
            /*
            RightClickLocation = FSMBuilderPanel.PointToClient(Cursor.Position);
            if (!FiniteStateBuilderPresenter.IsStateAtPoint(RightClickLocation))
                E.Cancel = true;

            foreach (ToolStripMenuItem Item in StateContextMenuStrip.Items)
            {
                Item.Enabled = true;
                Item.Checked = false;
            }

            if (FiniteStateBuilderPresenter.IsStateAtPointStartState(RightClickLocation))
            {
                foreach (ToolStripMenuItem Item in StateContextMenuStrip.Items)
                    Item.Enabled = false;

                ((ToolStripMenuItem)StateContextMenuStrip.Items[StateContextMenuStrip.Items.IndexOf(MarkValidToolStripMenuItem)]).Enabled = true;
                ((ToolStripMenuItem)StateContextMenuStrip.Items[StateContextMenuStrip.Items.IndexOf(MarkInvalidToolStripMenuItem)]).Enabled = true;
            }

            switch (FiniteStateBuilderPresenter.GetStateEventAtPoint(RightClickLocation))
            {
                case FiniteState.StateEventEnum.None:
                    ((ToolStripMenuItem)StateContextMenuStrip.Items[StateContextMenuStrip.Items.IndexOf(NoEventToolStripMenuItem)]).Checked = true;
                    break;
                case FiniteState.StateEventEnum.MarkValid:
                    ((ToolStripMenuItem)StateContextMenuStrip.Items[StateContextMenuStrip.Items.IndexOf(MarkValidToolStripMenuItem)]).Checked = true;
                    break;
                case FiniteState.StateEventEnum.MarkInvalid:
                    ((ToolStripMenuItem)StateContextMenuStrip.Items[StateContextMenuStrip.Items.IndexOf(MarkInvalidToolStripMenuItem)]).Checked = true;
                    break;
                case FiniteState.StateEventEnum.EndScan:
                    ((ToolStripMenuItem)StateContextMenuStrip.Items[StateContextMenuStrip.Items.IndexOf(EndScanToolStripMenuItem)]).Checked = true;
                    break;
            }*/
        }

        protected void FSMBuilderPanel_Paint(Object Sender, PaintEventArgs E)
        {
            Draw(E.Graphics);
        }

        #endregion

        private void Draw(Graphics Graphics)
        {
            if (FiniteStateBuilderPresenter.GetFiniteStateMachine() == null)
                return;

            foreach (FiniteState State in FiniteStateBuilderPresenter.GetFiniteStateMachine())
            {
                Image DrawImage;

                if (State == FiniteStateBuilderPresenter.GetFiniteStateMachine().GetStartState())
                    DrawImage = Resources.StartState;
                else
                    DrawImage = Resources.IntermediateState;

                Graphics.DrawImage(DrawImage, State.Location.X - StateRadius, State.Location.Y - StateRadius, DrawImage.Width, DrawImage.Height);

                DrawImage = null;
                switch (State.GetStateEvent())
                {
                    case FiniteState.StateEventEnum.MarkValid:
                        DrawImage = Resources.Valid;
                        break;
                    case FiniteState.StateEventEnum.MarkInvalid:
                        DrawImage = Resources.Invalid;
                        break;
                }

                if (DrawImage != null)
                    Graphics.DrawImage(DrawImage, State.Location.X - StateRadius, State.Location.Y - StateRadius, DrawImage.Width, DrawImage.Height);

            }

            if (FiniteStateBuilderPresenter.GetMousedOverState() != null)
                Graphics.DrawImage(Resources.StateHighlighted, FiniteStateBuilderPresenter.GetMousedOverState().Location.X - StateRadius, FiniteStateBuilderPresenter.GetMousedOverState().Location.Y - StateRadius, Resources.StateHighlighted.Width, Resources.StateHighlighted.Height);

            if (FiniteStateBuilderPresenter.GetSelectionLine() != null && FiniteStateBuilderPresenter.GetSelectionLine().Length == 2)
                Graphics.DrawLine(PendingLine, FiniteStateBuilderPresenter.GetSelectionLine()[0], FiniteStateBuilderPresenter.GetSelectionLine()[1]);

            foreach (FiniteState State in FiniteStateBuilderPresenter.GetFiniteStateMachine())
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
                        case ConstraintsEnum.Changed: Graphics.DrawImage(Resources.Changed, ImageLocation.X, ImageLocation.Y, Resources.Changed.Width, Resources.Changed.Height); break;
                        case ConstraintsEnum.Unchanged: Graphics.DrawImage(Resources.Unchanged, ImageLocation.X, ImageLocation.Y, Resources.Unchanged.Width, Resources.Unchanged.Height); break;
                        case ConstraintsEnum.Decreased: Graphics.DrawImage(Resources.Decreased, ImageLocation.X, ImageLocation.Y, Resources.Decreased.Width, Resources.Decreased.Height); break;
                        case ConstraintsEnum.Increased: Graphics.DrawImage(Resources.Increased, ImageLocation.X, ImageLocation.Y, Resources.Increased.Width, Resources.Increased.Height); break;
                        case ConstraintsEnum.GreaterThan: Graphics.DrawImage(Resources.GreaterThan, ImageLocation.X, ImageLocation.Y, Resources.GreaterThan.Width, Resources.GreaterThan.Height); break;
                        case ConstraintsEnum.GreaterThanOrEqual: Graphics.DrawImage(Resources.GreaterThanOrEqual, ImageLocation.X, ImageLocation.Y, Resources.GreaterThanOrEqual.Width, Resources.GreaterThanOrEqual.Height); break;
                        case ConstraintsEnum.LessThan: Graphics.DrawImage(Resources.LessThan, ImageLocation.X, ImageLocation.Y, Resources.LessThan.Width, Resources.LessThan.Height); break;
                        case ConstraintsEnum.LessThanOrEqual: Graphics.DrawImage(Resources.LessThanOrEqual, ImageLocation.X, ImageLocation.Y, Resources.LessThanOrEqual.Width, Resources.LessThanOrEqual.Height); break;
                        case ConstraintsEnum.Equal: Graphics.DrawImage(Resources.Equal, ImageLocation.X, ImageLocation.Y, Resources.Equal.Width, Resources.Equal.Height); break;
                        case ConstraintsEnum.NotEqual: Graphics.DrawImage(Resources.NotEqual, ImageLocation.X, ImageLocation.Y, Resources.NotEqual.Width, Resources.NotEqual.Height); break;
                        case ConstraintsEnum.IncreasedByX: Graphics.DrawImage(Resources.PlusX, ImageLocation.X, ImageLocation.Y, Resources.PlusX.Width, Resources.PlusX.Height); break;
                        case ConstraintsEnum.DecreasedByX: Graphics.DrawImage(Resources.MinusX, ImageLocation.X, ImageLocation.Y, Resources.MinusX.Width, Resources.MinusX.Height); break;
                        case ConstraintsEnum.NotScientificNotation: Graphics.DrawImage(Resources.Intersection, ImageLocation.X, ImageLocation.Y, Resources.Intersection.Width, Resources.Intersection.Height); break;
                        default: case ConstraintsEnum.Invalid: break;
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

            foreach (FiniteState State in FiniteStateBuilderPresenter.GetFiniteStateMachine())
            {
                // Draw transition value if applicable
                if (State.StateCount != 0)
                {
                    String DrawText = State.StateCount.ToString();
                    SizeF TextSize = Graphics.MeasureString(DrawText, DrawFont);
                    PointF TextLocation = new PointF(State.Location.X - TextSize.Width / 2, State.Location.Y - StateRadius - TextSize.Height);
                    Graphics.DrawString(DrawText, DrawFont, Brushes.White, TextLocation);
                }
            }

        } // End draw

    } // End class

} // End namespace