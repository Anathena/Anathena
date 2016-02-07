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
    public class ScanConstraintManager : IEnumerable
    {
        protected List<ScanConstraint> ValueConstraints;
        protected Type ElementType;

        public ScanConstraintManager()
        {
            ValueConstraints = new List<ScanConstraint>();
        }

        public Type GetElementType()
        {
            return ElementType;
        }

        public void SetElementType(Type ElementType)
        {
            this.ElementType = ElementType;
            
            foreach (ScanConstraint ScanConstraint in ValueConstraints.Select(x => x).Reverse())
            {
                if (ScanConstraint.Value == null)
                    continue;

                try
                {
                    // Attempt to cast the value to the new type
                    ScanConstraint.Value = Convert.ChangeType(ScanConstraint.Value, ElementType);
                }
                catch
                {
                    // Could not convert the data type, just removeit
                    ValueConstraints.Remove(ScanConstraint);
                }
            }
        }

        public void AddConstraint(ScanConstraint ValueConstraintsItem)
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

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)ValueConstraints).GetEnumerator();
        }

    } // End class

} // End namespace