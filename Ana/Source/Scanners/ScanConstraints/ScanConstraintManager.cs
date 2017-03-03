namespace Ana.Source.Scanners.ScanConstraints
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Utils.Extensions;

    /// <summary>
    /// Class for storing a collection of constraints to be used in a scan that applies more than one constraint per update.
    /// </summary>
    internal class ScanConstraintManager : IEnumerable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScanConstraintManager" /> class.
        /// </summary>
        public ScanConstraintManager()
        {
            this.ValueConstraints = new ObservableCollection<ScanConstraint>();
        }

        /// <summary>
        /// Gets the collection of constraints.
        /// </summary>
        public ObservableCollection<ScanConstraint> ValueConstraints { get; private set; }

        /// <summary>
        /// Gets the element type of this constraint manager.
        /// </summary>
        public Type ElementType { get; private set; }

        /// <summary>
        /// Indexer into the collection of contraints.
        /// </summary>
        /// <param name="index">The constraint index.</param>
        /// <returns>The constraint at the specified index.</returns>
        public ScanConstraint this[Int32 index]
        {
            get
            {
                return this.ValueConstraints[index];
            }
        }

        /// <summary>
        /// Creates a shallow clone of the scan constraint manager.
        /// </summary>
        /// <returns>A shallow clone of the scan constraint manager.</returns>
        public ScanConstraintManager Clone()
        {
            ScanConstraintManager scanConstraintManager = new ScanConstraintManager();
            scanConstraintManager.SetElementType(this.ElementType);
            this.ValueConstraints.ForEach(x => scanConstraintManager.AddConstraint(x));

            return scanConstraintManager;
        }

        /// <summary>
        /// Gets the number of constraints being managed.
        /// </summary>
        /// <returns>The number of constraints being managed.</returns>
        public Int32 Count()
        {
            return this.ValueConstraints.Count;
        }

        /// <summary>
        /// Sets the element type to which all constraints apply.
        /// </summary>
        /// <param name="elementType">The new element type.</param>
        public void SetElementType(Type elementType)
        {
            this.ElementType = elementType;

            foreach (ScanConstraint scanConstraint in this.ValueConstraints.Select(x => x).Reverse())
            {
                if (scanConstraint.Constraint == ConstraintsEnum.NotScientificNotation)
                {
                    if (elementType != typeof(Single) && elementType != typeof(Double))
                    {
                        this.ValueConstraints.Remove(scanConstraint);
                        continue;
                    }
                }

                if (scanConstraint.ConstraintValue == null)
                {
                    continue;
                }

                try
                {
                    // Attempt to cast the value to the new type.
                    scanConstraint.ConstraintValue = Convert.ChangeType(scanConstraint.ConstraintValue, elementType);
                }
                catch
                {
                    // Could not convert the data type, just remove it.
                    this.ValueConstraints.Remove(scanConstraint);
                }
            }
        }

        /// <summary>
        /// Determines if there is any constraint being managed which uses a relative value constraint.
        /// </summary>
        /// <returns>True if any constraint has a relative value constraint.</returns>
        public Boolean HasRelativeConstraint()
        {
            foreach (ScanConstraint valueConstraint in this)
            {
                if (valueConstraint.IsRelativeConstraint())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds a new constraint to the constraint manager.
        /// </summary>
        /// <param name="newScanConstraint">The new constraint.</param>
        /// <param name="hasPriority">Whether or not the new constraint has priority for conflicts.</param>
        public void AddConstraint(ScanConstraint newScanConstraint, Boolean hasPriority = true)
        {
            // Resolve potential conflicts depending on if the new constraint has priority
            if (newScanConstraint.Constraint == ConstraintsEnum.NotScientificNotation)
            {
                if (this.ElementType != typeof(Single) && this.ElementType != typeof(Double))
                {
                    return;
                }
            }

            // Remove conflicting constraints
            foreach (ScanConstraint scanConstraint in this.ValueConstraints.Select(x => x).Reverse())
            {
                if (scanConstraint.ConflictsWith(newScanConstraint))
                {
                    this.ValueConstraints.Remove(scanConstraint);
                }
            }

            this.ValueConstraints.Add(newScanConstraint);
        }

        /// <summary>
        /// Removes the specified constraint.
        /// </summary>
        /// <param name="scanConstraint">The constraint to remove.</param>
        public void RemoveConstraints(ScanConstraint scanConstraint)
        {
            this.ValueConstraints.Remove(scanConstraint);
        }

        /// <summary>
        /// Clears all constraints from the constraint manager.
        /// </summary>
        public void ClearConstraints()
        {
            this.ValueConstraints.Clear();
        }

        /// <summary>
        /// Gets the enumerator for the constraints being managed.
        /// </summary>
        /// <returns>The enumerator for the constraints.</returns>
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)this.ValueConstraints).GetEnumerator();
        }
    }
    //// End class
}
//// End namespace