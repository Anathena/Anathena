using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binarysharp.MemoryManagement;

namespace Anathema
{
    class ScanConstraintEditor : IScanConstraintEditorModel
    {
        // User controlled variables
        private ScanConstraintManager ScanConstraints;

        public event ScanConstraintEditorEventHandler EventUpdateDisplay;

        public ScanConstraintEditor()
        {
            ScanConstraints = new ScanConstraintManager();
        }

        private void UpdateDisplay()
        {
            ScanConstraintEditorEventArgs FilterManualScanEventArgs = new ScanConstraintEditorEventArgs();
            FilterManualScanEventArgs.ScanConstraints = ScanConstraints;
            EventUpdateDisplay(this, FilterManualScanEventArgs);
        }

        public Type GetElementType()
        {
            return ScanConstraints.GetElementType();
        }

        public void SetElementType(Type ElementType)
        {
            ScanConstraints.SetElementType(ElementType);
            UpdateDisplay();
        }

        public void AddConstraint(ConstraintsEnum ValueConstraint, dynamic Value)
        {
            ScanConstraints.AddConstraint(new ScanConstraint(ValueConstraint, Value));
            UpdateDisplay();
        }

        public void RemoveConstraints(Int32[] ConstraintIndicies)
        {
            ScanConstraints.RemoveConstraints(ConstraintIndicies);
            UpdateDisplay();
        }

        public void ClearConstraints()
        {
            ScanConstraints.ClearConstraints();
            UpdateDisplay();
        }

    } // End class

} // End namespace