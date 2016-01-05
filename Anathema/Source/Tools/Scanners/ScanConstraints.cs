using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Class for storing a collection of constraints to be used in a scan that applies more than one constraint per update
    /// </summary>
    class ScanConstraints : IEnumerable
    {
        private List<ScanConstraintItem> ValueConstraints;
        private Boolean FilterScientificNotation;

        public ScanConstraints()
        {
            FilterScientificNotation = false;
            ValueConstraints = new List<ScanConstraintItem>();
        }

        public void AddConstraint(ScanConstraintItem ValueConstraintsItem)
        {
            this.ValueConstraints.Add(ValueConstraintsItem);
        }

        public void RemoveConstraints(Int32[] ConstraintIndicies)
        {
            foreach (Int32 Index in ConstraintIndicies.ToList().Select(x => x).Reverse())
                this.ValueConstraints.RemoveAt(Index);
        }

        public void ClearConstraints()
        {
            this.ValueConstraints.Clear();
        }

        public void SetFilterScientificNotation(Boolean FilterScientificNotation)
        {
            this.FilterScientificNotation = FilterScientificNotation;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)ValueConstraints).GetEnumerator();
        }

    } // End class

} // End namespace