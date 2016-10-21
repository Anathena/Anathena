namespace Ana.Source.Scanners.ScanConstraints
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal class ScanConstraintEditor
    {
        private ScanConstraintManager scanConstraints;

        public ScanConstraintEditor()
        {
            this.scanConstraints = new ScanConstraintManager();
        }

        public ScanConstraintManager GetScanConstraintManager()
        {
            return this.scanConstraints;
        }

        public Type GetElementType()
        {
            return this.scanConstraints.ElementType;
        }

        public void SetElementType(Type elementType)
        {
            this.scanConstraints.SetElementType(elementType);
        }

        public ScanConstraint GetConstraintAt(Int32 index)
        {
            return this.scanConstraints[index];
        }

        [Obfuscation(Exclude = true)]
        public void AddConstraint(ConstraintsEnum valueConstraint, dynamic value)
        {
            this.scanConstraints.AddConstraint(new ScanConstraint(valueConstraint, value));
        }

        [Obfuscation(Exclude = true)]
        public void UpdateConstraint(Int32 index, dynamic value)
        {
            this.scanConstraints[index].ConstraintValue = value;
        }

        public void RemoveConstraints(IEnumerable<Int32> constraintIndicies)
        {
            this.scanConstraints.RemoveConstraints(constraintIndicies);
        }

        public void ClearConstraints()
        {
            this.scanConstraints.ClearConstraints();
        }
    }
    //// End class
}
//// End namespace