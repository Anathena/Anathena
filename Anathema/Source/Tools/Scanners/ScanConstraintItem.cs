using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public enum ValueConstraintsEnum
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
        LessThan,
    }

    public class ScanConstraintItem
    {
        public ValueConstraintsEnum ValueConstraints { get; set; }
        public dynamic Value { get; set; }

        public ScanConstraintItem()
        {
            ValueConstraints = ValueConstraintsEnum.Changed;
            Value = null;
        }

        public ScanConstraintItem(ValueConstraintsEnum ValueConstraint, dynamic Value = null)
        {
            this.ValueConstraints = ValueConstraint;
            this.Value = Value;
        }
    }
}
