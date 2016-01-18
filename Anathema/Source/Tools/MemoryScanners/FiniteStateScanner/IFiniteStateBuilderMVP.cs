using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void FiniteStateBuilderEventHandler(Object Sender, FiniteStateBuilderEventArgs Args);
    class FiniteStateBuilderEventArgs : EventArgs
    {

    }

    interface IFiniteStateBuilderView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateDisplay(FiniteStateMachine FiniteStateMachine, FiniteState MousedOverState, Point[] SelectionLine);
    }

    abstract class IFiniteStateBuilderModel : IModel
    {
        // Events triggered by the model (upstream)
        public event FiniteStateBuilderEventHandler EventUpdateDisplay;
        protected virtual void OnEventUpdateDisplay(FiniteStateBuilderEventArgs E)
        {
            EventUpdateDisplay(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract FiniteStateMachine GetFiniteStateMachine();

        public abstract void SetElementType(Type ElementType);
        public abstract Type GetElementType();
    }

    class FiniteStateBuilderPresenter : Presenter<IFiniteStateBuilderView, IFiniteStateBuilderModel>
    {
        new IFiniteStateBuilderView View;
        new IFiniteStateBuilderModel Model;

        private ConstraintsEnum ValueConstraintSelection;
        private FiniteState DraggedState;
        private FiniteState EdgeSelectedState;
        private FiniteState MousedOverState;
        private Point[] SelectionLine;

        private Int32 StateRadius;
        private Int32 StateEdgeSize;

        public FiniteStateBuilderPresenter(IFiniteStateBuilderView View, IFiniteStateBuilderModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventUpdateDisplay += EventUpdateDisplay;
        }

        #region Method definitions called by the view (downstream)

        public void SetStateEvent(Point Location, FiniteState.StateEventEnum StateEvent)
        {
            Model.GetFiniteStateMachine().GetStateUnderPoint(Location, StateRadius).SetStateEvent(StateEvent);
        }

        public void SetStateRadius(Int32 StateRadius)
        {
            this.StateRadius = StateRadius;
        }

        public void SetStateEdgeSize(Int32 StateEdgeSize)
        {
            this.StateEdgeSize = StateEdgeSize;
        }

        public void SetValueConstraintSelection(ConstraintsEnum ValueConstraintSelection)
        {
            this.ValueConstraintSelection = ValueConstraintSelection;
        }

        public ConstraintsEnum GetValueConstraintSelection()
        {
            return ValueConstraintSelection;
        }

        public void SetElementType(String ElementType)
        {
            Model.SetElementType(Conversions.StringToPrimitiveType(ElementType));
        }

        public Type GetElementType()
        {
            return Model.GetElementType();
        }

        public Boolean IsStateAtPoint(Point Location)
        {
            if (Model.GetFiniteStateMachine().GetStateUnderPoint(Location, StateRadius) != null)
                return true;
            return false;
        }

        public Boolean IsStateAtPointStartState(Point Location)
        {
            if (Model.GetFiniteStateMachine().GetStateUnderPoint(Location, StateRadius) == Model.GetFiniteStateMachine().GetStartState())
                return true;
            return false;
        }

        public void BeginAction(Point Location)
        {
            FiniteState State;

            // Handle edge selection
            State = Model.GetFiniteStateMachine().GetEdgeUnderPoint(Location, StateRadius, StateEdgeSize);
            if (State != null)
            {
                EdgeSelectedState = State;
                UpdateDisplay();
                return;
            }

            // Handle state selection (drag)
            State = Model.GetFiniteStateMachine().GetStateUnderPoint(Location, StateRadius);
            if (State != null)
            {
                DraggedState = State;
                UpdateDisplay();
                return;
            }

            // Handle state creation
            Model.GetFiniteStateMachine().AddNewState(Location);
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

            MousedOverState = Model.GetFiniteStateMachine().GetEdgeUnderPoint(Location, StateRadius, StateEdgeSize);
            UpdateDisplay();
        }

        public void FinishAction(Point Location, String ValueText)
        {
            // Update transition line dragging
            if (EdgeSelectedState != null)
            {
                // Add a transition if possible
                FiniteState DestinationState = Model.GetFiniteStateMachine().GetStateUnderPoint(Location, StateRadius);
                if (DestinationState != null && DestinationState != EdgeSelectedState)
                {
                    ScanConstraint TransitionConstraint;

                    if (CheckSyntax.CanParseValue(Model.GetElementType(), ValueText))
                        TransitionConstraint = new ScanConstraint(ValueConstraintSelection, Conversions.ParseValue(Model.GetElementType(), ValueText));
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
            FiniteState StateUnderPoint = Model.GetFiniteStateMachine().GetStateUnderPoint(Location, StateRadius);
            if (StateUnderPoint == null)
                return;

            Model.GetFiniteStateMachine().SetStartState(StateUnderPoint);
        }

        public FiniteState.StateEventEnum GetStateEventAtPoint(Point Location)
        {
            FiniteState StateUnderPoint = Model.GetFiniteStateMachine().GetStateUnderPoint(Location, StateRadius);
            if (StateUnderPoint == null)
                return FiniteState.StateEventEnum.None;

            return StateUnderPoint.GetStateEvent();
        }

        public void DeleteAtPoint(Point Location)
        {
            FiniteState StateUnderPoint = Model.GetFiniteStateMachine().GetStateUnderPoint(Location, StateRadius);
            if (StateUnderPoint == null)
                return;

            Model.GetFiniteStateMachine().DeleteState(StateUnderPoint);

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            View.UpdateDisplay(Model.GetFiniteStateMachine(), MousedOverState, SelectionLine);
        }

        private void ScanFinished()
        {

        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        public void EventUpdateDisplay(Object Sender, FiniteStateBuilderEventArgs E)
        {
            UpdateDisplay();
        }

        #endregion
    }
}
