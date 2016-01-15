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
            Snapshot.SetElementLabels(FiniteStateMachine.IndexOf(FiniteStateMachine.GetStartState()));

            switch (FiniteStateMachine.GetStartState().GetStateEvent())
            {
                case FiniteState.StateEventEnum.MarkValid:
                    Snapshot.MarkAllValid();
                    break;
                case FiniteState.StateEventEnum.MarkInvalid:
                    Snapshot.MarkAllInvalid();
                    break;
                default:
                    throw new Exception("Start state must mark elements as valid or invalid.");
            }

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
                    // Test the condition of each transition event in this element's current state
                    foreach (KeyValuePair<ScanConstraint, FiniteState> Transition in FiniteStateMachine[Element.ElementLabel.Value])
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
                            // DEBUG
                            //if ((UInt64)Element.BaseAddress != 0x0100579C)
                            {
                              //  Element.Valid = false;
                                //continue;
                            }

                            Element.ElementLabel = FiniteStateMachine.IndexOf(Transition.Value);
                            switch(Transition.Value.GetStateEvent())
                            {
                                case FiniteState.StateEventEnum.None:
                                    break;
                                case FiniteState.StateEventEnum.MarkValid:
                                    Element.Valid = true;
                                    break;
                                case FiniteState.StateEventEnum.MarkInvalid:
                                    Element.Valid = false;
                                    break;
                                case FiniteState.StateEventEnum.EndScan:
                                    FlagEndScan = true;
                                    break;
                            }
                        }

                    } // End foreach Constraint

                } // End foreach Element

            }); // End foreach Region
        }

        public override void EndScan()
        {
            base.EndScan();

            Snapshot FilteredSnapshot = new Snapshot<Null>(Snapshot.GetValidRegions());
            FilteredSnapshot.SetScanMethod("Manual Scan");

            SnapshotManager.GetInstance().SaveSnapshot(FilteredSnapshot);

            FiniteStateScannerEventArgs Args = new FiniteStateScannerEventArgs();
            OnEventScanFinished(Args);
        }
    }
}
