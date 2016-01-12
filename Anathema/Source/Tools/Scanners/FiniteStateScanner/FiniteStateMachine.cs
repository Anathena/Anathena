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
    /// Class to define a collection of states that represent a finite state machine for FSM scans
    /// </summary>
    public class FiniteStateMachine : IEnumerable
    {
        private List<FiniteState> States;
        private FiniteState StartState;
        private FiniteState EndState;
        private Type ElementType;

        public FiniteStateMachine()
        {
            States = new List<FiniteState>();
        }

        public FiniteState this[Int32 Index] { get { return States[Index]; } }

        public Byte IndexOf(FiniteState State)
        {
            return (Byte)States.IndexOf(State);
        }

        public Boolean IsFinalState(FiniteState State)
        {
            if (State == EndState)
                return true;
            return false;
        }

        public Byte GetFinalStateIndex()
        {
            if (EndState == null)
                return 0;

            return (Byte)States.IndexOf(EndState);
        }

        public FiniteState GetStartState()
        {
            return StartState;
        }

        public FiniteState GetEndState()
        {
            return EndState;
        }

        private void SetStartState()
        {
            foreach (FiniteState State in States)
                if (State != EndState)
                    StartState = State;
        }

        private void SetEndState()
        {
            foreach (FiniteState State in States.Select(x => x).Reverse())
                if (State != StartState)
                    EndState = State;
        }

        public void AddNewState(Point Location)
        {
            States.Add(new FiniteState(Location));

            if (StartState == null)
                SetStartState();
            else if (EndState == null)
                SetEndState();
        }

        public void DeleteState(FiniteState State)
        {
            if (State == null)
                return;

            if (States.Contains(State))
                States.Remove(State);

            if (StartState == State)
            {
                StartState = null;
                SetStartState();
            }
            if (EndState == State)
            {
                EndState = null;
                SetEndState();
            }
        }

        public void SetElementType(Type ElementType)
        {
            this.ElementType = ElementType;
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