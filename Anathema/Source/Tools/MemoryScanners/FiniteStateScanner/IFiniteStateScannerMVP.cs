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
    delegate void FiniteStateScannerEventHandler(Object Sender, FiniteStateScannerEventArgs Args);
    class FiniteStateScannerEventArgs : EventArgs
    {

    }

    interface IFiniteStateScannerView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateDisplay(FiniteStateMachine FiniteStateMachine, FiniteState MousedOverState, Point[] SelectionLine);
        void ScanFinished();
    }

    abstract class IFiniteStateScannerModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event FiniteStateScannerEventHandler EventScanFinished;
        protected virtual void OnEventScanFinished(FiniteStateScannerEventArgs E)
        {
            EventScanFinished(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract FiniteStateMachine GetFiniteStateMachine();

        public abstract void SetElementType(Type ElementType);
        public abstract Type GetElementType();
    }

    class FiniteStateScannerPresenter : ScannerPresenter
    {
        new IFiniteStateScannerView View;
        new IFiniteStateScannerModel Model;

        private ConstraintsEnum ValueConstraintSelection;
        private FiniteState DraggedState;
        private FiniteState EdgeSelectedState;
        private FiniteState MousedOverState;
        private Point[] SelectionLine;

        private Int32 StateRadius;
        private Int32 StateEdgeSize;

        public FiniteStateScannerPresenter(IFiniteStateScannerView View, IFiniteStateScannerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventScanFinished += EventScanFinished;
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

        public void EventScanFinished(Object Sender, FiniteStateScannerEventArgs E)
        {
            ScanFinished();
        }

        #endregion
    }
}
