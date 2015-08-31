using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUIFiniteStateMachinePanel : UserControl
    {
        private List<FiniteState> States;
        private FiniteState SelectedState;

        public GUIFiniteStateMachinePanel()
        {
            InitializeComponent();
        }

        private void GUIFiniteStateMachine_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.DoubleBuffer, true);

            InitializeDefaultStates();
        }

        private void InitializeDefaultStates()
        {
            States = new List<FiniteState>();
            States.Add(new FiniteState(this.ClientRectangle.Width / 3, this.ClientRectangle.Height / 2));
            States.Add(new FiniteState(this.ClientRectangle.Width / 2, this.ClientRectangle.Height / 2));
            States.Add(new FiniteState(this.ClientRectangle.Width * 2 / 3, this.ClientRectangle.Height / 2));
        }

        private void TryDeleteStatesAtPoint(Point DeleteLocation)
        {
            for (Int32 Index = Enum.GetNames(typeof(FiniteState.RequiredStates)).Length; Index < States.Count; Index++)
            {
                if (States[Index].IsPointInCircle(DeleteLocation))
                {
                    States.RemoveAt(Index);
                    this.Invalidate();
                    break;
                }
            }
        }

        private void TrySelectState(Point ClickLocation)
        {
            // Reset the selected state object
            SelectedState = null;

            // Determine if we are selecting an existing state
            for (Int32 Index = 0; Index < States.Count; Index++)
            {
                Int32 GripPort = States[Index].GetGripAtLocation(ClickLocation);
                // Clicking on a grip -- do grip things
                if (GripPort != FiniteState.InvalidGrip)
                {
                    SelectedState = States[Index];
                    SelectedState.StartCreateTransition(GripPort, FiniteStateTransition.TransitionEventEnum.Changed);
                    SelectedState.BeginConnecting();
                    break;
                }

                // Clicking inside the state -- drag it
                if (States[Index].IsPointInCircle(ClickLocation))
                {
                    SelectedState = States[Index];
                    SelectedState.SetLocation(ClickLocation);
                    SelectedState.BeginPlace();
                    this.Invalidate();
                    break;
                }
            }

            // Do not allow placing of more than the maximum number of states
            if (States.Count >= FiniteState.MaximumStates)
                return;

            // Create a new state if we aren't clicking on an existing one
            if (SelectedState == null)
            {
                SelectedState = new FiniteState(ClickLocation);
                SelectedState.BeginPlace();
                States.Add(SelectedState);
                this.Invalidate();
            }
        }

        private void GUIFiniteStateMachine_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
                return;

            // Right click delete -- ignoring requried states which cannot be deleted
            if (e.Button == MouseButtons.Right)
                TryDeleteStatesAtPoint(e.Location);

            // Left click drag or connect
            if (e.Button == MouseButtons.Left)
                TrySelectState(e.Location);

        } // end mouse down

        private void GUIFiniteStateMachine_MouseMove(object sender, MouseEventArgs e)
        {
            // Handle mouse over events
            for (Int32 Index = 0; Index < States.Count; Index++)
            {
                if (States[Index].UpdateMouseOver(e.Location))
                    Invalidate();
            }

            if (SelectedState == null)
                return;

            if (SelectedState.IsPlacing())
            {
                SelectedState.SetLocation(e.Location);
                this.Invalidate();
            }
            else if (SelectedState.IsConnecting())
            {
                this.Invalidate();
            }
        }

        private void GUIFiniteStateMachine_MouseUp(object sender, MouseEventArgs e)
        {
            if (SelectedState == null)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (SelectedState.IsPlacing())
                {
                    SelectedState.EndPlace();
                    SelectedState = null;
                    this.Invalidate();
                }

                else if (SelectedState.IsConnecting())
                {
                    for (Int32 Index = 0; Index < States.Count; Index++)
                    {
                        Int32 GripPort = States[Index].GetGripAtLocation(e.Location);

                        if (States[Index] == SelectedState || GripPort == FiniteState.InvalidGrip)
                            continue;

                        SelectedState.EndCreateTransition(GripPort, States[Index]);
                        break;
                    }

                    SelectedState.EndConnecting();
                    SelectedState = null;
                    this.Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw all states
            for (Int32 Index = 0; Index < States.Count; Index++)
                States[Index].Draw(e.Graphics, Index, this.PointToClient(Cursor.Position));
        }
    }
}
