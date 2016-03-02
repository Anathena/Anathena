using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public enum ConstraintsEnum
    {
        Invalid,
        Equal,
        NotEqual,
        Changed,
        Unchanged,
        Increased,
        Decreased,
        IncreasedByX,
        DecreasedByX,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,

        NotScientificNotation,
    }

    /// <summary>
    /// Class to define a constraint for certain types of scans
    /// </summary>
    public class ScanConstraint
    {
        public ConstraintsEnum Constraint { get; set; }
        public dynamic Value { get; set; }

        public ScanConstraint()
        {
            Constraint = ConstraintsEnum.Changed;
            Value = null;
        }

        public ScanConstraint(ConstraintsEnum ValueConstraint, dynamic Value = null)
        {
            this.Constraint = ValueConstraint;
            this.Value = Value;
        }
    }
}
