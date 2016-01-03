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
        GreaterThanExclusive,
        GreaterThanInclusive,
        LessThanExclusive,
        LessThanInclusive,
        BetweenInclusive,
        BetweenExclusive,
        NotBetweenInclusive,
        NotBetweenExclusive
    }

    public class ValueConstraintsItem
    {
        public ValueConstraintsEnum ChangeConstraints { get; set; }
        public Type ValueType { get; set; }
        public dynamic ValueLeft { get; set; }
        public dynamic ValueRight { get; set; }

        public ValueConstraintsItem()
        {
            ChangeConstraints = ValueConstraintsEnum.Changed;
            ValueType = typeof(Int32);
            ValueLeft = String.Empty;
            ValueRight = String.Empty;
        }

        public ValueConstraintsItem(ValueConstraintsEnum ValueConstraint, Type ValueType, dynamic ValueLeft, dynamic ValueRight)
        {
            this.ChangeConstraints = ValueConstraint;
            this.ValueType = ValueType;
            this.ValueLeft = ValueLeft;
            this.ValueRight = ValueRight;
        }
    }
}
