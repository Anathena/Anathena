namespace Ana.Source.Scanners.ScanConstraints
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using Utils.Extensions;
    /// <summary>
    /// Class for storing a collection of constraints to be used in a scan that applies more than one constraint per update
    /// </summary>
    public class ScanConstraintManager : IEnumerable
    {
        public ScanConstraintManager()
        {
            this.ValueConstraints = new ObservableCollection<ScanConstraint>();
        }

        public ObservableCollection<ScanConstraint> ValueConstraints { get; private set; }

        public Type ElementType { get; private set; }

        [Obfuscation(Exclude = true)]
        public ScanConstraint this[Int32 index]
        {
            get
            {
                return this.ValueConstraints[index];
            }
        }

        /// <summary>
        /// Creates a shallow clone of the scan constraint manager
        /// </summary>
        /// <returns></returns>
        public ScanConstraintManager Clone()
        {
            ScanConstraintManager scanConstraintManager = new ScanConstraintManager();
            scanConstraintManager.SetElementType(this.ElementType);
            this.ValueConstraints.ForEach(x => scanConstraintManager.AddConstraint(x));

            return scanConstraintManager;
        }

        public Int32 GetCount()
        {
            return this.ValueConstraints.Count;
        }

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

                if (scanConstraint.Value == null)
                {
                    continue;
                }

                try
                {
                    // Attempt to cast the value to the new type
                    scanConstraint.Value = Convert.ChangeType(scanConstraint.Value, elementType);
                }
                catch
                {
                    // Could not convert the data type, just remove it
                    this.ValueConstraints.Remove(scanConstraint);
                }
            }
        }

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

        public void AddConstraint(ScanConstraint scanConstraint)
        {
            if (scanConstraint.Constraint == ConstraintsEnum.NotScientificNotation)
            {
                if (this.ElementType != typeof(Single) && this.ElementType != typeof(Double))
                {
                    return;
                }
            }

            this.ValueConstraints.Add(scanConstraint);
        }

        public void RemoveConstraints(IEnumerable<Int32> constraintIndicies)
        {
            foreach (Int32 index in constraintIndicies.OrderByDescending(x => x))
            {
                this.ValueConstraints.RemoveAt(index);
            }
        }

        public void ClearConstraints()
        {
            this.ValueConstraints.Clear();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)this.ValueConstraints).GetEnumerator();
        }
    }
    //// End class
}
//// End namespace