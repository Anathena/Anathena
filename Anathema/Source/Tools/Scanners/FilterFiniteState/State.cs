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
    class State : ScanConstraintManager
    {
        private enum StateEventsEnum
        {
            MarkInvalid,
            MarkValid,
        }

        private Dictionary<ScanConstraint, State> TransitionStates; // Maps constraints to states
        private Point DisplayLocation;

        public State() : base()
        {
            TransitionStates = new Dictionary<ScanConstraint, State>();
        }

        public void AddTransition(ScanConstraint Constraint, State State)
        {
            TransitionStates.Add(Constraint, State);
        }

        public void RemoveTransition(ScanConstraint Constraint, State State)
        {
            if (TransitionStates.ContainsKey(Constraint))
                TransitionStates.Remove(Constraint);
        }


    } // End class

} // End namespace