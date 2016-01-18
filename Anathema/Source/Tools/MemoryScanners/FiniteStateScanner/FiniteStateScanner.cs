using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

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

        private void UpdateDisplay()
        {
            FiniteStateScannerEventArgs FilterFSMEventArgs = new FiniteStateScannerEventArgs();
            OnEventUpdateDisplay(FilterFSMEventArgs);
        }

        public override void Begin()
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

            // Initialize state counts
            foreach (FiniteState State in FiniteStateMachine)
                State.StateCount = 0;
            FiniteStateMachine.GetStartState().StateCount = (Int64)Snapshot.GetMemorySize();

            base.Begin();
        }

        protected override void Update()
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
                            case ConstraintsEnum.GreaterThanOrEqual:
                                if (Element.GreaterThanOrEqualToValue(Transition.Key.Value))
                                    DoTransition = true;
                                break;
                            case ConstraintsEnum.LessThan:
                                if (Element.LessThanValue(Transition.Key.Value))
                                    DoTransition = true;
                                break;
                            case ConstraintsEnum.LessThanOrEqual:
                                if (Element.LessThanOrEqualToValue(Transition.Key.Value))
                                    DoTransition = true;
                                break;
                        }

                        if (DoTransition)
                        {
                            // Update counts (thread safe)
                            Interlocked.Decrement(ref FiniteStateMachine[Element.ElementLabel.Value].StateCount);
                            Interlocked.Increment(ref Transition.Value.StateCount);
                            
                            // Do transition and transition event
                            Element.ElementLabel = FiniteStateMachine.IndexOf(Transition.Value);
                            switch (Transition.Value.GetStateEvent())
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
                                    CancelFlag = true;
                                    break;
                            }
                        }

                    } // End foreach Constraint

                } // End foreach Element

            }); // End foreach Region

            UpdateDisplay();
        }

        public override void End()
        {
            base.End();

            Snapshot.DiscardInvalidRegions();
            Snapshot.SetScanMethod("Manual Scan");

            SnapshotManager.GetInstance().SaveSnapshot(Snapshot);

            // Reset state counts
            foreach (FiniteState State in FiniteStateMachine)
                State.StateCount = 0;

            UpdateDisplay();

            FiniteStateScannerEventArgs Args = new FiniteStateScannerEventArgs();
            OnEventScanFinished(Args);
        }

    } // End class

} // End namespace