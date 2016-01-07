using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Class to define a collection of states that represent a finite state machine for FSM scans
    /// </summary>
    class FiniteStateMachine : IEnumerable
    {
        private List<FiniteState> States;
        private Type ElementType;

        public FiniteStateMachine()
        {
            States = new List<FiniteState>();
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

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)States).GetEnumerator();
        }

    } // End class

} // End namespace