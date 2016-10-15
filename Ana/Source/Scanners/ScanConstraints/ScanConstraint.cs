namespace Ana.Source.Scanners.ScanConstraints
{
    using Content;
    using System;
    using System.Reflection;
    using System.Windows.Media.Imaging;

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
        public ScanConstraint()
        {
            this.Constraint = ConstraintsEnum.Changed;
            this.Value = null;
        }

        public ScanConstraint(ConstraintsEnum valueConstraint, dynamic addressValue = null)
        {
            this.Constraint = valueConstraint;
            this.Value = addressValue;
        }

        public ConstraintsEnum Constraint { get; set; }

        [Obfuscation(Exclude = true)]
        public dynamic Value { get; set; }

        public Boolean IsRelativeConstraint()
        {
            switch (this.Constraint)
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

        public BitmapImage ConstraintImage
        {
            get
            {
                switch (this.Constraint)
                {
                    case ConstraintsEnum.Equal:
                        return Images.Equal;
                    case ConstraintsEnum.NotEqual:
                        return Images.NotEqual;
                    case ConstraintsEnum.GreaterThan:
                        return Images.GreaterThan;
                    case ConstraintsEnum.GreaterThanOrEqual:
                        return Images.GreaterThanOrEqual;
                    case ConstraintsEnum.LessThan:
                        return Images.LessThan;
                    case ConstraintsEnum.LessThanOrEqual:
                        return Images.LessThanOrEqual;
                    case ConstraintsEnum.NotScientificNotation:
                        return Images.ENotation;
                    case ConstraintsEnum.Changed:
                        return Images.Changed;
                    case ConstraintsEnum.Unchanged:
                        return Images.Unchanged;
                    case ConstraintsEnum.Increased:
                        return Images.Increased;
                    case ConstraintsEnum.Decreased:
                        return Images.Decreased;
                    case ConstraintsEnum.IncreasedByX:
                        return Images.PlusX;
                    case ConstraintsEnum.DecreasedByX:
                        return Images.MinusX;
                    case ConstraintsEnum.Invalid:
                        return Images.Cancel;
                    default:
                        throw new Exception("Unrecognized Constraint");
                }
            }
        }
    }
    //// End class
}
//// End namespace