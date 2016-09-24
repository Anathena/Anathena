using Ana.Source.Scanners.ScanConstraints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace Ana.Source.Scanners.FiniteStateScanner
{
    /// <summary>
    /// Class to define a collection of states that represent a finite state machine for FSM scans
    /// </summary>
    public class FiniteStateMachine : IEnumerable
    {
        private ScanConstraintManager UniversalConstraints;
        private List<FiniteState> States;
        private FiniteState StartState;
        private Type ElementType;

        public FiniteStateMachine()
        {
            UniversalConstraints = new ScanConstraintManager();
            States = new List<FiniteState>();
        }

        [Obfuscation(Exclude = true)]
        public FiniteState this[Int32 Index] { get { return States[Index]; } }

        public Byte IndexOf(FiniteState State)
        {
            return (Byte)States.IndexOf(State);
        }

        public FiniteState GetStartState()
        {
            return StartState;
        }

        public void SetStartState(FiniteState TargetState)
        {
            StartState = TargetState;

            if (StartState.GetStateEvent() != FiniteState.StateEventEnum.MarkInvalid || StartState.GetStateEvent() != FiniteState.StateEventEnum.MarkValid)
                StartState.SetStateEvent(FiniteState.StateEventEnum.MarkValid);
        }

        public void AddNewState(Point Location)
        {
            States.Add(new FiniteState(Location));

            if (StartState == null)
                SetStartState(States.First());
        }

        public void DeleteState(FiniteState DeletedState)
        {
            if (DeletedState == null)
                return;

            if (States.Contains(DeletedState))
                States.Remove(DeletedState);

            foreach (FiniteState State in States)
                State.ClearTransitionsToState(DeletedState);

            if (StartState == DeletedState)
            {
                StartState = null;
                SetStartState(States.First());
            }
        }

        public void SetElementType(Type ElementType)
        {
            this.ElementType = ElementType;
            UniversalConstraints.SetElementType(ElementType);
            foreach (FiniteState State in States)
                State.SetElementType(ElementType);
        }

        public Type GetElementType()
        {
            return ElementType;
        }

        public FiniteState GetEdgeUnderPoint(Point Location, Int32 Radius, Int32 EdgeSize)
        {
            foreach (FiniteState State in this)
            {
                Single Distance = (Single)Math.Sqrt((Location.X - State.Location.X) * (Location.X - State.Location.X) + (Location.Y - State.Location.Y) * (Location.Y - State.Location.Y));

                if ((Int32)Distance <= Radius && Distance >= Radius - EdgeSize)
                    return State;
            }
            return null;
        }

        public FiniteState GetStateUnderPoint(Point Location, Int32 Radius)
        {
            foreach (FiniteState State in this)
            {
                Single Distance = (Single)Math.Sqrt((Location.X - State.Location.X) * (Location.X - State.Location.X) + (Location.Y - State.Location.Y) * (Location.Y - State.Location.Y));

                if ((Int32)Distance <= Radius)
                    return State;
            }
            return null;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)States).GetEnumerator();
        }

    } // End class

} // End namespace