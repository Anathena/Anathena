using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Anathema
{
    class FiniteStateScanner : IFiniteStateScannerModel
    {
        // Snapshot, where the label represents the state
        private Snapshot<Byte> Snapshot;

        // User controlled variables
        private FiniteStateMachine FiniteStateMachine;

        public FiniteStateScanner()
        {
            FiniteStateMachine = new FiniteStateMachine();
        }

        public override void SetElementType(Type ElementType)
        {
            FiniteStateMachine.SetElementType(ElementType);
        }

        public override Type GetElementType()
        {
            return FiniteStateMachine.GetElementType();
        }

        public override FiniteStateMachine GetFiniteStateMachine()
        {
            return FiniteStateMachine;
        }

        private void UpdateDisplay()
        {
            FiniteStateScannerEventArgs FilterFSMEventArgs = new FiniteStateScannerEventArgs();
            OnEventScanFinished(FilterFSMEventArgs);
        }

        public override void BeginScan()
        {
            // Initialize snapshot
            Snapshot = new Snapshot<Byte>(SnapshotManager.GetInstance().GetActiveSnapshot());
            Snapshot.MarkAllValid();
            Snapshot.SetElementType(FiniteStateMachine.GetElementType());
            Snapshot.SetMemoryLabels(0);

            base.BeginScan();
        }

        protected override void UpdateScan()
        {
            // Read memory to get current values
            Snapshot.ReadAllSnapshotMemory();

            // Enforce each value constraint

            Parallel.ForEach(Snapshot.Cast<Object>(), (RegionObject) =>
            {
                SnapshotRegion<Byte> Region = (SnapshotRegion<Byte>)RegionObject;

                if (!Region.CanCompare())
                    return;

                foreach (SnapshotElement<Byte> Element in Region)
                {
                    if (!Element.Valid)
                        continue;

                    // Test the condition of each transition event in this element's current state
                    foreach (KeyValuePair<ScanConstraint, FiniteState> Transition in FiniteStateMachine[Element.MemoryLabel.Value])
                    {
                        Boolean DoTransition = false;
                        switch (Transition.Key.Constraint)
                        {
                            case ConstraintsEnum.Unchanged:
                                if (Element.Unchanged())
                                    DoTransition = true;
                                break;
                            case ConstraintsEnum.Changed:
                                if (Element.Changed())
                                    DoTransition = true;
                                break;
                            case ConstraintsEnum.Increased:
                                if (Element.Increased())
                                    DoTransition = true;
                                break;
                            case ConstraintsEnum.Decreased:
                                if (Element.Decreased())
                                    DoTransition = true;
                                break;
                            case ConstraintsEnum.IncreasedByX:
                                if (Element.IncreasedByValue(Transition.Key.Value))
                                    DoTransition = true;
                                break;
                            case ConstraintsEnum.DecreasedByX:
                                if (Element.DecreasedByValue(Transition.Key.Value))
                                    DoTransition = true;
                                break;
                            case ConstraintsEnum.Equal:
                                if (Element.EqualToValue(Transition.Key.Value))
                                    DoTransition = true;
                                break;
                            case ConstraintsEnum.NotEqual:
                                if (Element.NotEqualToValue(Transition.Key.Value))
                                    DoTransition = true;
                                break;
                            case ConstraintsEnum.GreaterThan:
                                if (Element.GreaterThanValue(Transition.Key.Value))
                                    DoTransition = true;
                                break;
                            case ConstraintsEnum.LessThan:
                                if (Element.LessThanValue(Transition.Key.Value))
                                    DoTransition = true;
                                break;
                        }

                        if (DoTransition)
                        {
                            Element.MemoryLabel = FiniteStateMachine.IndexOf(Transition.Value);
                            if (FiniteStateMachine.IsFinalState(Transition.Value))
                                FlagEndScan = true;
                        }

                    } // End foreach Constraint

                } // End foreach Element

            }); // End foreach Region
        }

        public override void EndScan()
        {
            // base.EndScan();

            Snapshot.MarkAllInvalid();
            foreach (SnapshotRegion<Byte> Region in Snapshot)
            {
                foreach (SnapshotElement<Byte> Element in Region)
                {
                    if (Element.MemoryLabel == FiniteStateMachine.GetFinalStateIndex())
                        Element.Valid = true;
                }
            }

            Snapshot.ExpandValidRegions();
            Snapshot FilteredSnapshot = new Snapshot(Snapshot.GetValidRegions());
            FilteredSnapshot.SetScanMethod("Manual Scan");

            SnapshotManager.GetInstance().SaveSnapshot(FilteredSnapshot);

            FiniteStateScannerEventArgs Args = new FiniteStateScannerEventArgs();
            OnEventScanFinished(Args);
        }
    }
}
