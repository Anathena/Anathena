using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Extension of scan constraints, however this keeps track of transitions and their resulting actions for FSM scans
    /// </summary>
    public class FiniteState : ScanConstraintManager, IEnumerable
    {
        /// <summary>
        /// Used for showing the number of elements in this state in the display
        /// </summary>
        public Int64 StateCount;

        // Holds mappings of scan constraints to their next state in the FSM
        private Dictionary<ScanConstraint, FiniteState> Transitions;

        public Point Location { get; set; }

        public enum StateEventEnum
        {
            None,
            MarkValid,
            MarkInvalid,
            EndScan,
        }

        private StateEventEnum StateEvent;

        public FiniteState() : base()
        {
            StateCount = 0;
            StateEvent = StateEventEnum.None;
            Transitions = new Dictionary<ScanConstraint, FiniteState>();
        }

        public FiniteState(Point Location) : base()
        {
            this.Location = Location;
            StateCount = 0;
            StateEvent = StateEventEnum.None;
            Transitions = new Dictionary<ScanConstraint, FiniteState>();
        }

        public StateEventEnum GetStateEvent()
        {
            return this.StateEvent;
        }
        
        public void AddTransition(ScanConstraint Constraint, FiniteState State)
        {
            // Enforce unique outgoing constraints and only one transition to a given state from this one
            foreach (KeyValuePair<ScanConstraint, FiniteState> Transition in Transitions)
                if (Transition.Key.Constraint == Constraint.Constraint)
                    return;

            // Enforce uni-directionality
            if (State.ContainsDestionationState(this))
                return;

            if (!Transitions.ContainsKey(Constraint))
                Transitions.Add(Constraint, State);
        }

        public void SetStateEvent(StateEventEnum StateEvent)
        {
            this.StateEvent = StateEvent;
        }

        public void RemoveTransition(ScanConstraint Constraint, FiniteState State)
        {
            if (Transitions.ContainsKey(Constraint))
                Transitions.Remove(Constraint);
        }

        public void ClearTransitionsToState(FiniteState DestinationState)
        {
            List<ScanConstraint> RemovedItems = new List<ScanConstraint>();
            foreach (KeyValuePair<ScanConstraint, FiniteState> Transition in Transitions)
                if (Transition.Value == DestinationState)
                    RemovedItems.Add(Transition.Key);

            foreach (ScanConstraint Item in RemovedItems)
                Transitions.Remove(Item);
        }

        public Boolean ContainsDestionationState(FiniteState DestinationState)
        {
            foreach (KeyValuePair<ScanConstraint, FiniteState> Transition in Transitions)
                if (Transition.Value == DestinationState)
                    return true;
            return false;
        }

        public Point GetEdgePoint(Point Location, Int32 StateRadius)
        {
            Single Ax = Location.X == 0 ? Single.Epsilon : (Single)Location.X;
            Single Ay = Location.Y == 0 ? Single.Epsilon : (Single)Location.Y;
            Single Bx = this.Location.X == 0 ? Single.Epsilon : (Single)this.Location.X;
            Single By = this.Location.Y == 0 ? Single.Epsilon : (Single)this.Location.Y;

            Single vX = Ax - Bx;
            Single vY = Ay - By;
            Single magV = (Single)Math.Sqrt(vX * vX + vY * vY);
            Single EdgeX = Bx + vX / (magV == 0 ? Single.Epsilon : magV) * (Single)StateRadius;
            Single EdgeY = By + vY / (magV == 0 ? Single.Epsilon : magV) * (Single)StateRadius;

            Point EdgePoint = new Point((Int32)EdgeX, (Int32)EdgeY);

            return EdgePoint;
        }

        public new IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Transitions).GetEnumerator();
        }

    } // End class

} // End namespace