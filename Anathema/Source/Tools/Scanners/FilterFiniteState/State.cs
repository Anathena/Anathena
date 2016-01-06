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
    /// Extension of scan constraints, however this keeps track of transition events
    /// </summary>
    class State : ScanConstraintManager
    {
        private Dictionary<ScanConstraint, State> TransitionStates;
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