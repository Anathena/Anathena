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
    class FilterFSM : IFilterFSMModel
    {
        // Snapshot being labeled with change counts
        private Snapshot Snapshot;

        // User controlled variables
        private FiniteStateMachine FiniteStateMachine;

        public FilterFSM()
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

        public override FiniteState GetEdgeUnderPoint(Point Location, Int32 Radius, Int32 EdgeSize)
        {
            foreach (FiniteState State in FiniteStateMachine)
            {
                Single Distance = (Single)Math.Sqrt((Location.X - State.Location.X) * (Location.X - State.Location.X) + (Location.Y - State.Location.Y) * (Location.Y - State.Location.Y));

                if ((Int32)Distance <= Radius && Distance >= Radius - EdgeSize)
                    return State;
            }
            return null;
        }

        public override FiniteState GetStateUnderPoint(Point Location, Int32 Radius)
        {
            foreach (FiniteState State in FiniteStateMachine)
            {
                Single Distance = (Single)Math.Sqrt((Location.X - State.Location.X) * (Location.X - State.Location.X) + (Location.Y - State.Location.Y) * (Location.Y - State.Location.Y));

                if ((Int32)Distance <= Radius)
                    return State;
            }
            return null;
        }

        public override void AddNewState(Point Location)
        {
            //FiniteStateMachine.AddState();
        }

        public override void FinishAction(Point Location)
        {
            throw new NotImplementedException();
        }
        
        public override void DeleteAtPoint(Point Location)
        {
            throw new NotImplementedException();
        }

        public override void AddTransition()
        {
            throw new NotImplementedException();
        }

        private void UpdateDisplay()
        {
            FilterFSMEventArgs FilterFSMEventArgs = new FilterFSMEventArgs();
            OnEventUpdateDisplay(FilterFSMEventArgs);
        }

        public override void BeginScan()
        {
            // Initialize snapshot
            Snapshot = new Snapshot(SnapshotManager.GetInstance().GetActiveSnapshot());
            Snapshot.MarkAllValid();
            Snapshot.SetElementType(FiniteStateMachine.GetElementType());

            base.BeginScanRunOnce();
        }

        protected override void UpdateScan()
        {
            // Read memory to get current values
            Snapshot.ReadAllSnapshotMemory();

            // Enforce each value constraint
            foreach (FiniteState State in FiniteStateMachine)
            {

                Parallel.ForEach(Snapshot.Cast<Object>(), (RegionObject) =>
                {
                    SnapshotRegion Region = (SnapshotRegion)RegionObject;

                    if (!Region.CanCompare())
                        return;

                    foreach (SnapshotElement Element in Region)
                    {
                        if (!Element.Valid)
                            continue;

                        /*switch (State.)
                        {
                            case ConstraintsEnum.Unchanged:
                                if (!Element.Unchanged())
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.Changed:
                                if (!Element.Changed())
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.Increased:
                                if (!Element.Increased())
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.Decreased:
                                if (!Element.Decreased())
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.IncreasedByX:
                                if (!Element.IncreasedByValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.DecreasedByX:
                                if (!Element.DecreasedByValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.Equal:
                                if (!Element.EqualToValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.NotEqual:
                                if (!Element.NotEqualToValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.GreaterThan:
                                if (!Element.GreaterThanValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.LessThan:
                                if (!Element.LessThanValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                        }*/

                    } // End foreach Element

                }); // End foreach Region

            } // End foreach Constraint
        }

        public override void EndScan()
        {
            // base.EndScan();
            Snapshot.ExpandValidRegions();
            Snapshot FilteredSnapshot = new Snapshot(Snapshot.GetValidRegions());
            FilteredSnapshot.SetScanMethod("Manual Scan");

            SnapshotManager.GetInstance().SaveSnapshot(FilteredSnapshot);
        }
    }
}
