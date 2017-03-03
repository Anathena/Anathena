namespace Ana.Source.Scanners.ScanConstraints
{
    using Content;
    using System;
    using System.ComponentModel;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Enumeration of all possible scan constraints.
    /// </summary>
    internal enum ConstraintsEnum
    {
        /// <summary>
        /// Comparative: The values must be equal.
        /// </summary>
        Equal,

        /// <summary>
        /// Comparative: The values must not be equal.
        /// </summary>
        NotEqual,

        /// <summary>
        /// Relative: The value must have changed.
        /// </summary>
        Changed,

        /// <summary>
        /// Relative: The value must not have changed.
        /// </summary>
        Unchanged,

        /// <summary>
        /// Relative: The value must have increased.
        /// </summary>
        Increased,

        /// <summary>
        /// Relative: The value must have decreased.
        /// </summary>
        Decreased,

        /// <summary>
        /// Relative: The value must have increased by a specific value.
        /// </summary>
        IncreasedByX,

        /// <summary>
        /// Relative: The value must have decreased by a specific value.
        /// </summary>
        DecreasedByX,

        /// <summary>
        /// Comparative: The value must be greater than the other value.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Comparative: The value must be greater than or equal the other value.
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// Comparative: The value must be less than the other value.
        /// </summary>
        LessThan,

        /// <summary>
        /// Comparative: The value must be less than or equal the other value.
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// Special: Only applies to singles and doubles. The value must not be in E notation.
        /// </summary>
        NotScientificNotation,
    }

    /// <summary>
    /// Class to define a constraint for certain types of scans.
    /// </summary>
    internal class ScanConstraint : INotifyPropertyChanged
    {
        /// <summary>
        /// The constraint type.
        /// </summary>
        private ConstraintsEnum constraint;

        /// <summary>
        /// The value associated with this constraint, if applicable.
        /// </summary>
        private dynamic constraintValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanConstraint" /> class.
        /// </summary>
        public ScanConstraint()
        {
            this.Constraint = ConstraintsEnum.Changed;
            this.ConstraintValue = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanConstraint" /> class.
        /// </summary>
        /// <param name="valueConstraint">The constraint type.</param>
        /// <param name="addressValue">The value associated with this constraint.</param>
        public ScanConstraint(ConstraintsEnum valueConstraint, dynamic addressValue = null)
        {
            this.Constraint = valueConstraint;
            this.ConstraintValue = addressValue;
        }

        /// <summary>
        /// Occurs after a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the constraint type.
        /// </summary>
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

                // Force an update of the constraint value, to determine if it is still valid for the new constraint
                this.ConstraintValue = this.constraintValue;
            }
        }

        /// <summary>
        /// Gets or sets the value associated with this constraint, if applicable.
        /// </summary>
        public dynamic ConstraintValue
        {
            get
            {
                if (this.IsValuedConstraint())
                {
                    return this.constraintValue;
                }
                else
                {
                    return null;
                }
            }

            set
            {
                this.constraintValue = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ConstraintValue)));
            }
        }

        /// <summary>
        /// Gets the name associated with this constraint.
        /// </summary>
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
                    default:
                        throw new Exception("Unrecognized Constraint");
                }
            }
        }

        /// <summary>
        /// Gets the image associated with this constraint.
        /// </summary>
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
                    default:
                        throw new Exception("Unrecognized Constraint");
                }
            }
        }

        /// <summary>
        /// Determines if this constraint conflicts with another constraint.
        /// </summary>
        /// <param name="scanConstraint">The other scan constraint.</param>
        /// <returns>True if the constraints conflict, otherwise false.</returns>
        public Boolean ConflictsWith(ScanConstraint scanConstraint)
        {
            bool thisRel = this.IsRelativeConstraint();
            bool thisVal = this.IsValuedConstraint();
            bool thatRel = scanConstraint.IsRelativeConstraint();
            bool thatVal = scanConstraint.IsValuedConstraint();

            if (this.Constraint == scanConstraint.Constraint ||
                this.IsRelativeConstraint() && scanConstraint.IsRelativeConstraint() ||
                (this.IsValuedConstraint() && scanConstraint.IsValuedConstraint() &&
                (!this.IsRelativeConstraint() && !scanConstraint.IsRelativeConstraint())))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a value indicating whether this constraint is a relative comparison constraint or not.
        /// </summary>
        /// <returns>True if the constraint is a relative value constraint.</returns>
        public Boolean IsRelativeConstraint()
        {
            switch (this.Constraint)
            {
                case ConstraintsEnum.Changed:
                case ConstraintsEnum.Unchanged:
                case ConstraintsEnum.Increased:
                case ConstraintsEnum.Decreased:
                case ConstraintsEnum.IncreasedByX:
                case ConstraintsEnum.DecreasedByX:
                    return true;
                case ConstraintsEnum.Equal:
                case ConstraintsEnum.NotEqual:
                case ConstraintsEnum.GreaterThan:
                case ConstraintsEnum.GreaterThanOrEqual:
                case ConstraintsEnum.LessThan:
                case ConstraintsEnum.LessThanOrEqual:
                case ConstraintsEnum.NotScientificNotation:
                    return false;
                default:
                    throw new Exception("Unrecognized Constraint");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this constraint requires a value.
        /// </summary>
        /// <returns>True if the constraint requires a value.</returns>
        public Boolean IsValuedConstraint()
        {
            switch (this.Constraint)
            {
                case ConstraintsEnum.Equal:
                case ConstraintsEnum.NotEqual:
                case ConstraintsEnum.GreaterThan:
                case ConstraintsEnum.GreaterThanOrEqual:
                case ConstraintsEnum.LessThan:
                case ConstraintsEnum.LessThanOrEqual:
                case ConstraintsEnum.IncreasedByX:
                case ConstraintsEnum.DecreasedByX:
                    return true;
                case ConstraintsEnum.NotScientificNotation:
                case ConstraintsEnum.Changed:
                case ConstraintsEnum.Unchanged:
                case ConstraintsEnum.Increased:
                case ConstraintsEnum.Decreased:
                    return false;
                default:
                    throw new Exception("Unrecognized Constraint");
            }
        }
    }
    //// End class
}
//// End namespace