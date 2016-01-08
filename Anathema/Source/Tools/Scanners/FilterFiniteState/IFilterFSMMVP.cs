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
    delegate void FilterFSMEventHandler(Object Sender, FilterFSMEventArgs Args);
    class FilterFSMEventArgs : EventArgs
    {

    }

    interface IFilterFSMView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateDisplay(FiniteStateMachine FiniteStateMachine, FiniteState MousedOverState, Point[] SelectionLine);
    }

    abstract class IFilterFSMModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event FilterFSMEventHandler EventUpdateDisplay;
        protected virtual void OnEventUpdateDisplay(FilterFSMEventArgs E)
        {
            EventUpdateDisplay(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract FiniteStateMachine GetFiniteStateMachine();

        public abstract void SetElementType(Type ElementType);
        public abstract Type GetElementType();
    }

    class FilterFSMPresenter : ScannerPresenter
    {
        new IFilterFSMView View;
        new IFilterFSMModel Model;

        private ConstraintsEnum ValueConstraintSelection;
        private FiniteState DraggedState;
        private FiniteState EdgeSelectedState;
        private FiniteState MousedOverState;
        private Point[] SelectionLine;

        private Int32 StateRadius;
        private Int32 StateEdgeSize;

        public FilterFSMPresenter(IFilterFSMView View, IFilterFSMModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventUpdateDisplay += EventUpdateDisplay;
        }

        #region Method definitions called by the view (downstream)

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

        public void DeleteAtPoint(Point Location)
        {
            //Model.GetFiniteStateMachine().DeleteAtPoint(Location);
        }

        private void UpdateDisplay()
        {
            View.UpdateDisplay(Model.GetFiniteStateMachine(), MousedOverState, SelectionLine);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        public void EventUpdateDisplay(Object Sender, FilterFSMEventArgs E)
        {
            UpdateDisplay();
        }

        #endregion
    }
}
