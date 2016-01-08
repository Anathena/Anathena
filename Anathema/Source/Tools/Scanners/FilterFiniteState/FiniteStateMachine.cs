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

        public void AddNewState(Point Location)
        {
            States.Add(new FiniteState(Location));
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