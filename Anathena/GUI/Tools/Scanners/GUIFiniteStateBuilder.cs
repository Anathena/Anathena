using Ana.Properties;
using Ana.Source.Scanners.FiniteStateScanner;
using Ana.Source.Scanners.ScanConstraints;
using Ana.Source.Utils;
using Ana.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Ana.GUI.Tools.Scanners
{
    public partial class GUIFiniteStateBuilder : UserControl, IFiniteStateBuilderView
    {
        // Drawing Variables:
        private static Font drawFont = new Font(FontFamily.GenericSerif, 10.0f);
        private static Pen transitionLine = new Pen(Color.Black, 3);
        private static Pen pendingLine = new Pen(Color.Red, 2);
        private static Int32 stateRadius = Resources.StateHighlighted.Width / 2;
        private static Int32 lineOffset = (Int32)transitionLine.Width / 2;
        private const Int32 stateEdgeSize = 8;
        private const Int32 lineFloatOffset = 8;
        private const Int32 variableBorderSize = 4;
        private const Int32 arrowSize = 4;

        private Boolean blockNextMouseEvent;

        private FiniteStateBuilderPresenter finiteStateBuilderPresenter;
        private Object accessLock;

        public GUIFiniteStateBuilder()
        {
            InitializeComponent();

            finiteStateBuilderPresenter = new FiniteStateBuilderPresenter(this, new FiniteStateBuilder());
            accessLock = new Object();

            finiteStateBuilderPresenter.SetStateRadius(stateRadius);
            finiteStateBuilderPresenter.SetStateEdgeSize(stateEdgeSize);

            blockNextMouseEvent = false;

            this.Paint += new PaintEventHandler(FSMBuilderPanel_Paint);
        }

        public void UpdateDisplay()
        {
            using (TimedLock.Lock(accessLock))
            {
                ControlThreadingHelper.InvokeControlAction(FSMBuilderPanel, () =>
                {
                    FSMBuilderPanel.Invalidate();
                });
            }
        }

        #region Events

        private void StateContextMenuStrip_Closed(Object sender, ToolStripDropDownClosedEventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                if (e.CloseReason == ToolStripDropDownCloseReason.AppClicked)
                {
                    if (FSMBuilderPanel.Bounds.Contains(FSMBuilderPanel.PointToClient(Cursor.Position)))
                        blockNextMouseEvent = true;
                }
            }
        }

        private void FSMBuilderPanel_MouseDown(Object sender, MouseEventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                if (blockNextMouseEvent)
                {
                    blockNextMouseEvent = false;
                    return;
                }

                if (e.Button != MouseButtons.Left)
                    return;

                finiteStateBuilderPresenter.BeginAction(e.Location);
            }
        }

        private void FSMBuilderPanel_MouseMove(Object sender, MouseEventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                finiteStateBuilderPresenter.UpdateAction(e.Location);
            }
        }

        private void FSMBuilderPanel_MouseUp(Object sender, MouseEventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                if (e.Button != MouseButtons.Left)
                    return;

                finiteStateBuilderPresenter.FinishAction(e.Location);
            }
        }

        private void DragModeButton_Click(Object sender, EventArgs e)
        {

        }

        private void StateContextMenuStrip_Opening(Object sender, CancelEventArgs e)
        {

        }

        protected void FSMBuilderPanel_Paint(Object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        #endregion

        private void Draw(Graphics graphics)
        {
            using (TimedLock.Lock(accessLock))
            {
                if (finiteStateBuilderPresenter.GetFiniteStateMachine() == null)
                    return;

                foreach (FiniteState state in finiteStateBuilderPresenter.GetFiniteStateMachine())
                {
                    Image drawImage;

                    if (state == finiteStateBuilderPresenter.GetFiniteStateMachine().GetStartState())
                        drawImage = Resources.StartState;
                    else
                        drawImage = Resources.IntermediateState;

                    graphics.DrawImage(drawImage, state.Location.X - stateRadius, state.Location.Y - stateRadius, drawImage.Width, drawImage.Height);

                    drawImage = null;
                    switch (state.GetStateEvent())
                    {
                        case FiniteState.StateEventEnum.MarkValid:
                            drawImage = Resources.Valid;
                            break;
                        case FiniteState.StateEventEnum.MarkInvalid:
                            drawImage = Resources.Invalid;
                            break;
                    }

                    if (drawImage != null)
                        graphics.DrawImage(drawImage, state.Location.X - stateRadius, state.Location.Y - stateRadius, drawImage.Width, drawImage.Height);

                }

                if (finiteStateBuilderPresenter.GetMousedOverState() != null)
                    graphics.DrawImage(Resources.StateHighlighted, finiteStateBuilderPresenter.GetMousedOverState().Location.X - stateRadius, finiteStateBuilderPresenter.GetMousedOverState().Location.Y - stateRadius, Resources.StateHighlighted.Width, Resources.StateHighlighted.Height);

                if (finiteStateBuilderPresenter.GetSelectionLine() != null && finiteStateBuilderPresenter.GetSelectionLine().Length == 2)
                    graphics.DrawLine(pendingLine, finiteStateBuilderPresenter.GetSelectionLine()[0], finiteStateBuilderPresenter.GetSelectionLine()[1]);

                foreach (FiniteState state in finiteStateBuilderPresenter.GetFiniteStateMachine())
                {
                    foreach (KeyValuePair<ScanConstraint, FiniteState> transition in state)
                    {
                        // Calculate start and end points of the transitio line
                        Point startPoint = state.GetEdgePoint(transition.Value.Location, stateRadius);
                        Point endPoint = transition.Value.GetEdgePoint(state.Location, stateRadius);
                        startPoint.Y += lineOffset;
                        endPoint.Y += lineOffset;

                        // Draw transition line
                        Point midPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
                        graphics.DrawLine(transitionLine, startPoint, endPoint);

                        // Draw arrow head
                        // Point[] arrowHeadPoints = new Point[3];
                        // arrowHeadPoints[0] = EndPoint;
                        // arrowHeadPoints[1] = EndPoint;
                        // arrowHeadPoints[2] = EndPoint;
                        graphics.FillEllipse(Brushes.Black, endPoint.X - arrowSize, endPoint.Y - arrowSize, arrowSize * 2, arrowSize * 2);

                        // Draw comparison image
                        Point imageLocation = new Point(midPoint.X - Resources.Equal.Width / 2, midPoint.Y - Resources.Equal.Height - lineFloatOffset);
                        switch (transition.Key.Constraint)
                        {
                            case ConstraintsEnum.Changed: graphics.DrawImage(Resources.Changed, imageLocation.X, imageLocation.Y, Resources.Changed.Width, Resources.Changed.Height); break;
                            case ConstraintsEnum.Unchanged: graphics.DrawImage(Resources.Unchanged, imageLocation.X, imageLocation.Y, Resources.Unchanged.Width, Resources.Unchanged.Height); break;
                            case ConstraintsEnum.Decreased: graphics.DrawImage(Resources.Decreased, imageLocation.X, imageLocation.Y, Resources.Decreased.Width, Resources.Decreased.Height); break;
                            case ConstraintsEnum.Increased: graphics.DrawImage(Resources.Increased, imageLocation.X, imageLocation.Y, Resources.Increased.Width, Resources.Increased.Height); break;
                            case ConstraintsEnum.GreaterThan: graphics.DrawImage(Resources.GreaterThan, imageLocation.X, imageLocation.Y, Resources.GreaterThan.Width, Resources.GreaterThan.Height); break;
                            case ConstraintsEnum.GreaterThanOrEqual: graphics.DrawImage(Resources.GreaterThanOrEqual, imageLocation.X, imageLocation.Y, Resources.GreaterThanOrEqual.Width, Resources.GreaterThanOrEqual.Height); break;
                            case ConstraintsEnum.LessThan: graphics.DrawImage(Resources.LessThan, imageLocation.X, imageLocation.Y, Resources.LessThan.Width, Resources.LessThan.Height); break;
                            case ConstraintsEnum.LessThanOrEqual: graphics.DrawImage(Resources.LessThanOrEqual, imageLocation.X, imageLocation.Y, Resources.LessThanOrEqual.Width, Resources.LessThanOrEqual.Height); break;
                            case ConstraintsEnum.Equal: graphics.DrawImage(Resources.Equal, imageLocation.X, imageLocation.Y, Resources.Equal.Width, Resources.Equal.Height); break;
                            case ConstraintsEnum.NotEqual: graphics.DrawImage(Resources.NotEqual, imageLocation.X, imageLocation.Y, Resources.NotEqual.Width, Resources.NotEqual.Height); break;
                            case ConstraintsEnum.IncreasedByX: graphics.DrawImage(Resources.PlusX, imageLocation.X, imageLocation.Y, Resources.PlusX.Width, Resources.PlusX.Height); break;
                            case ConstraintsEnum.DecreasedByX: graphics.DrawImage(Resources.MinusX, imageLocation.X, imageLocation.Y, Resources.MinusX.Width, Resources.MinusX.Height); break;
                            case ConstraintsEnum.NotScientificNotation: graphics.DrawImage(Resources.Intersection, imageLocation.X, imageLocation.Y, Resources.Intersection.Width, Resources.Intersection.Height); break;
                            default: case ConstraintsEnum.Invalid: break;
                        }

                        // Draw transition value if applicable
                        if (transition.Key.Value != null)
                        {
                            String DrawText = transition.Key.Value.ToString();
                            SizeF TextSize = graphics.MeasureString(DrawText, drawFont);
                            PointF TextLocation = new PointF(midPoint.X - TextSize.Width / 2, midPoint.Y + lineFloatOffset);
                            graphics.FillEllipse(Brushes.Black, TextLocation.X - variableBorderSize, TextLocation.Y - variableBorderSize, TextSize.Width + variableBorderSize * 2, TextSize.Height + variableBorderSize);
                            graphics.DrawString(DrawText, drawFont, Brushes.White, TextLocation);
                        }
                    }
                }

                foreach (FiniteState state in finiteStateBuilderPresenter.GetFiniteStateMachine())
                {
                    // Draw transition value if applicable
                    if (state.StateCount != 0)
                    {
                        String drawText = state.StateCount.ToString();
                        SizeF textSize = graphics.MeasureString(drawText, drawFont);
                        PointF textLocation = new PointF(state.Location.X - textSize.Width / 2, state.Location.Y - stateRadius - textSize.Height);
                        graphics.DrawString(drawText, drawFont, Brushes.White, textLocation);
                    }
                }
            }
        } // End draw

    } // End class

} // End namespace