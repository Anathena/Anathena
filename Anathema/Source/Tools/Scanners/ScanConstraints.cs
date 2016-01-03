using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Class for storing a collection of constraints to be used in a scan that applies more than one constraint per update
    /// </summary>
    class ScanConstraints
    {
        private List<ValueConstraintsItem> ValueConstraints;
        private Boolean FilterScientificNotation;

        public ScanConstraints()
        {
            FilterScientificNotation = false;
            ValueConstraints = new List<ValueConstraintsItem>();
        }

        public void AddConstraint(ValueConstraintsItem ValueConstraintsItem)
        {
            this.ValueConstraints.Add(ValueConstraintsItem);
        }

        public void SetFilterScientificNotation(Boolean FilterScientificNotation)
        {
            this.FilterScientificNotation = FilterScientificNotation;
        }

        public enum ValueConstraintsEnum
        {
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
            BetweenExclusive
        }

        public class ValueConstraintsItem
        {
            public ValueConstraintsEnum ChangeConstraints { get; set; }
            public Type ValueType { get; set; }
            public dynamic ValueLeft { get; set; }
            public dynamic ValueRight { get; set; }

            public ValueConstraintsItem(ValueConstraintsEnum ValueConstraint, Type ValueType, dynamic ValueLeft, dynamic ValueRight)
            {
                this.ChangeConstraints = ValueConstraint;
                this.ValueType = ValueType;
                this.ValueLeft = ValueLeft;
                this.ValueRight = ValueRight;
            }
        }

    } // End class

} // End namespace