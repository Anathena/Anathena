namespace Ana.Source.Scanners.ScanConstraints
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    class ScanConstraintEditor
    {
        private ScanConstraintManager ScanConstraints;

        public ScanConstraintEditor()
        {
            ScanConstraints = new ScanConstraintManager();
        }

        public ScanConstraintManager GetScanConstraintManager()
        {
            return ScanConstraints;
        }

        public Type GetElementType()
        {
            return ScanConstraints.GetElementType();
        }

        public void SetElementType(Type ElementType)
        {
            ScanConstraints.SetElementType(ElementType);
        }

        public ScanConstraint GetConstraintAt(Int32 Index)
        {
            return ScanConstraints[Index];
        }

        [Obfuscation(Exclude = true)]
        public void AddConstraint(ConstraintsEnum ValueConstraint, dynamic Value)
        {
            ScanConstraints.AddConstraint(new ScanConstraint(ValueConstraint, Value));
        }

        [Obfuscation(Exclude = true)]
        public void UpdateConstraint(Int32 Index, dynamic Value)
        {
            ScanConstraints[Index].Value = Value;
        }

        public void RemoveConstraints(IEnumerable<Int32> ConstraintIndicies)
        {
            ScanConstraints.RemoveConstraints(ConstraintIndicies);
        }

        public void ClearConstraints()
        {
            ScanConstraints.ClearConstraints();
        }
    }
    //// End class
}
//// End namespace