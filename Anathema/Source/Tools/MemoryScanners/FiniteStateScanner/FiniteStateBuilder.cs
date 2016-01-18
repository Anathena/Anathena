using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Anathema
{
    class FiniteStateBuilder : IFiniteStateBuilderModel
    {
        // User controlled variables
        private FiniteStateMachine FiniteStateMachine;
        public event FiniteStateBuilderEventHandler EventUpdateDisplay;

        private ConstraintsEnum ValueConstraintSelection;
        private FiniteState DraggedState;
        private FiniteState EdgeSelectedState;
        private FiniteState MousedOverState;
        private Point[] SelectionLine;
        private Type ElementType;

        private Int32 StateRadius;
        private Int32 StateEdgeSize;

        private String ValueText = "TEMP"; // swap with a dynamic var or some shit

        public FiniteStateBuilder()
        {
            FiniteStateMachine = new FiniteStateMachine();
        }

        public void SetElementType(Type ElementType)
        {
            FiniteStateMachine.SetElementType(ElementType);
        }
        public ConstraintsEnum GetValueConstraintSelection()
        {
            throw new NotImplementedException();
        }

        public FiniteState GetMousedOverState()
        {
            throw new NotImplementedException();
        }

        public FiniteState GetDraggedState()
        {
            throw new NotImplementedException();
        }

        public FiniteState GetEdgeSelectedState()
        {
            throw new NotImplementedException();
        }

        public Point[] GetSelectionLine()
        {
            throw new NotImplementedException();
        }

        public void SetValueConstraintSelection(ConstraintsEnum ValueConstraintSelection)
        {
            throw new NotImplementedException();
        }

        public void SetFiniteStateMachine(FiniteStateMachine FiniteStateMachine)
        {
            throw new NotImplementedException();
        }

        public void SetMousedOverState(FiniteState FiniteState)
        {
            throw new NotImplementedException();
        }

        public void SetDraggedState(FiniteState FiniteState)
        {
            throw new NotImplementedException();
        }

        public void SetEdgeSelectedState(FiniteState FiniteState)
        {
            throw new NotImplementedException();
        }

        public void SetSelectionLine(Point[] SelectionLIne)
        {
            throw new NotImplementedException();
        }

        public void SetStateRadius(int Radius)
        {
            throw new NotImplementedException();
        }

        public void SetStateEdgeSize(int EdgeSize)
        {
            throw new NotImplementedException();
        }
        public Boolean IsStateAtPoint(Point Location)
        {
            if (FiniteStateMachine.GetStateUnderPoint(Location, StateRadius) != null)
                return true;
            return false;
        }

        public void SetStateEvent(Point Location, FiniteState.StateEventEnum StateEvent)
        {
            FiniteStateMachine.GetStateUnderPoint(Location, StateRadius).SetStateEvent(StateEvent);
        }

        public Boolean IsStateAtPointStartState(Point Location)
        {
            if (FiniteStateMachine.GetStateUnderPoint(Location, StateRadius) == FiniteStateMachine.GetStartState())
                return true;
            return false;
        }

        public void BeginAction(Point Location)
        {
            FiniteState State;

            // Handle edge selection
            State = FiniteStateMachine.GetEdgeUnderPoint(Location, StateRadius, StateEdgeSize);
            if (State != null)
            {
                EdgeSelectedState = State;
                UpdateDisplay();
                return;
            }

            // Handle state selection (drag)
            State = FiniteStateMachine.GetStateUnderPoint(Location, StateRadius);
            if (State != null)
            {
                DraggedState = State;
                UpdateDisplay();
                return;
            }

            // Handle state creation
            FiniteStateMachine.AddNewState(Location);
            UpdateDisplay();
        }

        public void UpdateAction(Point Location)
        {
            // Update transition line dragging
            if (EdgeSelectedState != null)
            {
                SelectionLine = new Point[2];
                SelectionLine[0] = EdgeSelectedState.GetEdgePoint(Location, StateRadius);
                SelectionLine[1] = Location;
                UpdateDisplay();
                return;
            }

            // Update drag
            if (DraggedState != null)
            {
                DraggedState.Location = Location;
                UpdateDisplay();
                return;
            }

            MousedOverState = FiniteStateMachine.GetEdgeUnderPoint(Location, StateRadius, StateEdgeSize);
            UpdateDisplay();
        }

        public void FinishAction(Point Location)
        {
            // Update transition line dragging
            if (EdgeSelectedState != null)
            {
                // Add a transition if possible
                FiniteState DestinationState = FiniteStateMachine.GetStateUnderPoint(Location, StateRadius);
                if (DestinationState != null && DestinationState != EdgeSelectedState)
                {
                    ScanConstraint TransitionConstraint;

                    if (CheckSyntax.CanParseValue(ElementType, ValueText))
                        TransitionConstraint = new ScanConstraint(ValueConstraintSelection, Conversions.ParseValue(ElementType, ValueText));
                    else
                        TransitionConstraint = new ScanConstraint(ValueConstraintSelection);

                    if (!EdgeSelectedState.ContainsDestionationState(DestinationState))
                        EdgeSelectedState.AddTransition(TransitionConstraint, DestinationState);
                }
                SelectionLine = null;
                EdgeSelectedState = null;
                UpdateDisplay();
                return;
            }

            // Update drag
            if (DraggedState != null)
            {
                DraggedState = null;
                UpdateDisplay();
                return;
            }
        }

        public void SetToStartState(Point Location)
        {
            FiniteState StateUnderPoint = FiniteStateMachine.GetStateUnderPoint(Location, StateRadius);
            if (StateUnderPoint == null)
                return;

            FiniteStateMachine.SetStartState(StateUnderPoint);
        }

        public FiniteState.StateEventEnum GetStateEventAtPoint(Point Location)
        {
            FiniteState StateUnderPoint = FiniteStateMachine.GetStateUnderPoint(Location, StateRadius);
            if (StateUnderPoint == null)
                return FiniteState.StateEventEnum.None;

            return StateUnderPoint.GetStateEvent();
        }

        public void DeleteAtPoint(Point Location)
        {
            FiniteState StateUnderPoint = FiniteStateMachine.GetStateUnderPoint(Location, StateRadius);
            if (StateUnderPoint == null)
                return;

            FiniteStateMachine.DeleteState(StateUnderPoint);

            UpdateDisplay();
        }

        public FiniteStateMachine GetFiniteStateMachine()
        {
            return FiniteStateMachine;
        }

        private void UpdateDisplay()
        {
            FiniteStateBuilderEventArgs FilterFSMEventArgs = new FiniteStateBuilderEventArgs();
            EventUpdateDisplay(this, FilterFSMEventArgs);
        }
    }
}
