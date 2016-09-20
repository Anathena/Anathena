using System;
using System.Reflection;

namespace Ana.Source.Scanners.ScanConstraints
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
        [Obfuscation(Exclude = true)]
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

        public Boolean IsRelativeConstraint()
        {
            switch (Constraint)
            {
                case ConstraintsEnum.Equal:
                case ConstraintsEnum.NotEqual:
                case ConstraintsEnum.GreaterThan:
                case ConstraintsEnum.GreaterThanOrEqual:
                case ConstraintsEnum.LessThan:
                case ConstraintsEnum.LessThanOrEqual:
                case ConstraintsEnum.NotScientificNotation:
                    return false;
                case ConstraintsEnum.Changed:
                case ConstraintsEnum.Unchanged:
                case ConstraintsEnum.Increased:
                case ConstraintsEnum.Decreased:
                case ConstraintsEnum.IncreasedByX:
                case ConstraintsEnum.DecreasedByX:
                    return true;
                case ConstraintsEnum.Invalid:
                default:
                    throw new Exception("Unrecognized Constraint");
            }
        }

    } // End class

} // End namespace