namespace Ana.Source.Scanners.ScanConstraints
{
    using Content;
    using System;
    using System.ComponentModel;
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
    public class ScanConstraint : INotifyPropertyChanged
    {
        private ConstraintsEnum constraint;
        private dynamic constraintValue;

        public ScanConstraint()
        {
            this.Constraint = ConstraintsEnum.Changed;
            this.ConstraintValue = null;
        }

        public ScanConstraint(ConstraintsEnum valueConstraint, dynamic addressValue = null)
        {
            this.Constraint = valueConstraint;
            this.ConstraintValue = addressValue;
        }

        public ConstraintsEnum Constraint
        {
            get
            {
                return this.constraint;
            }

            set
            {
                this.constraint = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Constraint)));
            }
        }

        [Obfuscation(Exclude = true)]
        public dynamic ConstraintValue
        {
            get
            {
                return this.constraintValue;
            }

            set
            {
                this.constraintValue = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ConstraintValue)));
            }
        }

        public String ConstraintName
        {
            get
            {
                switch (this.Constraint)
                {
                    case ConstraintsEnum.Equal:
                        return "Equal";
                    case ConstraintsEnum.NotEqual:
                        return "Not Equal";
                    case ConstraintsEnum.GreaterThan:
                        return "Greater Than";
                    case ConstraintsEnum.GreaterThanOrEqual:
                        return "Greater Than Or Equal";
                    case ConstraintsEnum.LessThan:
                        return "Less Than";
                    case ConstraintsEnum.LessThanOrEqual:
                        return "Less Than Or Equal";
                    case ConstraintsEnum.NotScientificNotation:
                        return "Not Scientific Notation";
                    case ConstraintsEnum.Changed:
                        return "Changed";
                    case ConstraintsEnum.Unchanged:
                        return "Unchanged";
                    case ConstraintsEnum.Increased:
                        return "Increased";
                    case ConstraintsEnum.Decreased:
                        return "Decreased";
                    case ConstraintsEnum.IncreasedByX:
                        return "Increased By X";
                    case ConstraintsEnum.DecreasedByX:
                        return "Decreased By X";
                    case ConstraintsEnum.Invalid:
                        return "Invalid";
                    default:
                        throw new Exception("Unrecognized Constraint");
                }
            }
        }

        public BitmapSource ConstraintImage
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
                        return Images.ExponentialNotation;
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

        public event PropertyChangedEventHandler PropertyChanged;

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
    }
    //// End class
}
//// End namespace