using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using Gma.System.MouseKeyHook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    // I Originally thought these might be a good idea:
    // http://www.ucl.ac.uk/english-usage/staff/sean/resources/phimeasures.pdf
    // https://en.wikipedia.org/wiki/Contingency_table#Measures_of_association
    // It turns out a simple pentalty/reward system works fine

    class InputCorrelator : IInputCorrelatorModel
    {
        private Snapshot<Single> Snapshot;

        private readonly IKeyboardMouseEvents InputHook;    // Input capturing class

        private Dictionary<Keys, DateTime> KeyBoardDown;    // List of keyboard down events
        private Dictionary<Keys, DateTime> KeyBoardUp;      // List of keyboard up events

        private Int32 VariableSize; // Number of bytes to correlate at a time
        private Int32 WaitTime;     // Time (ms) to process new changes as correlations
        private Keys UserInput;     // Whatever

        private InputNode InputConditionTree;

        public InputCorrelator()
        {
            // Initialize input hook
            InputHook = Hook.GlobalEvents();
        }

        public override void SetVariableSize(int VariableSize)
        {
            this.VariableSize = VariableSize;
        }

        private void UpdateDisplay()
        {
            if (InputConditionTree != null)
                InputConditionTree.EvaluateText();
            InputCorrelatorEventArgs InputCorrelatorEventArgs = new InputCorrelatorEventArgs();
            InputCorrelatorEventArgs.Root = InputConditionTree;
            OnEventUpdateDisplay(InputCorrelatorEventArgs);
        }

        public override void AddInputNode(Stack<int> Indicies, Keys Key)
        {
            AddNode(Indicies, new InputNode(Key));
        }

        public override void AddNode(Stack<Int32> Indicies, InputNode Node)
        {
            if (InputConditionTree == null)
            {
                InputConditionTree = Node;
                UpdateDisplay();
                return;
            }

            if (Indicies.Count == 0)
                return;

            // We only allow a single root, so pop the root from this stack
            Indicies.Pop();

            // Determine the node the user is attempting to add a child to
            InputNode TargetNode = InputConditionTree;
            while (Indicies.Count > 0)
                TargetNode = TargetNode.GetChildAtIndex(Indicies.Pop());

            // Add the child
            TargetNode.AddChild(Node);

            UpdateDisplay();
        }

        public override void ClearNodes()
        {
            InputConditionTree = null;
            UpdateDisplay();
        }

        public override void DeleteNode(Stack<Int32> Indicies)
        {
            if (Indicies.Count == 0)
                return;

            // We only allow a single root, so pop the root from this stack
            Indicies.Pop();

            // Determine the node the user is attempting to delete
            InputNode TargetNode = InputConditionTree;
            while (Indicies.Count > 0)
                TargetNode = TargetNode.GetChildAtIndex(Indicies.Pop());

            // Delete the node and all children under it
            if (TargetNode == InputConditionTree)
                InputConditionTree = null;
            else
                TargetNode.DeleteNode();

            UpdateDisplay();
        }

        public override void BeginScan()
        {
            // Initialize labeled snapshot
            Snapshot = new Snapshot<Single>(SnapshotManager.GetInstance().GetActiveSnapshot());
            Snapshot.SetVariableSize(VariableSize);

            // Initialize with no correlation
            Snapshot.SetElementLabels(0.0f);

            // TEMP: variables that should be user-tuned
            UserInput = Keys.D;
            WaitTime = 800;

            // Initialize input dictionaries
            KeyBoardUp = new Dictionary<Keys, DateTime>();
            KeyBoardDown = new Dictionary<Keys, DateTime>();

            // Create input hook events
            InputHook.MouseDownExt += GlobalHookMouseDownExt;
            InputHook.KeyUp += GlobalHookKeyUp;
            InputHook.KeyDown += GlobalHookKeyDown;

            base.BeginScan();
        }

        protected override void UpdateScan()
        {
            // Read memory to update previous and current values
            Snapshot.ReadAllSnapshotMemory();

            Boolean ConditionValid = InputConditionTree.EvaluateCondition(KeyBoardDown, Snapshot.GetTimeStamp(), WaitTime);

            Parallel.ForEach(Snapshot.Cast<Object>(), (RegionObject) =>
            {
                SnapshotRegion<Single> Region = (SnapshotRegion<Single>)RegionObject;

                if (!Region.CanCompare())
                    return;

                foreach (SnapshotElement<Single> Element in Region)
                {
                    if (Element.Changed())
                    {
                        if (ConditionValid)
                            Element.ElementLabel += 1.0f;
                        else
                            Element.ElementLabel -= 1.0f;
                    }

                }
            });
        }

        public override void EndScan()
        {
            base.EndScan();

            // Cleanup for the input hook
            InputHook.KeyUp -= GlobalHookKeyUp;
            InputHook.MouseDownExt -= GlobalHookMouseDownExt;
            InputHook.KeyDown -= GlobalHookKeyDown;
            InputHook.Dispose();

            Single MaxValue = 1.0f;
            foreach (SnapshotRegion<Single> Region in Snapshot)
                foreach (SnapshotElement<Single> Element in Region)
                    if (Element.ElementLabel.Value > MaxValue)
                        MaxValue = Element.ElementLabel.Value;

            Snapshot.MarkAllInvalid();
            foreach (SnapshotRegion<Single> Region in Snapshot)
            {
                foreach (SnapshotElement<Single> Element in Region)
                {
                    Element.ElementLabel = Element.ElementLabel / MaxValue;
                    if (Element.ElementLabel.Value > 0.75f)
                        Element.Valid = true;
                }
            }

            Snapshot.DiscardInvalidRegions();
            Snapshot.SetScanMethod("Input Correlator");

            SnapshotManager.GetInstance().SaveSnapshot(Snapshot);
        }

        private void RegisterKey(Keys Key)
        {
            if (!KeyBoardDown.ContainsKey(Key))
                KeyBoardDown.Add(Key, DateTime.MinValue);

            if (!KeyBoardUp.ContainsKey(Key))
                KeyBoardUp.Add(Key, DateTime.MinValue);
        }

        private void GlobalHookKeyUp(Object Sender, KeyEventArgs E)
        {
            RegisterKey(E.KeyCode);
            KeyBoardUp[E.KeyCode] = DateTime.Now;
        }

        private void GlobalHookKeyDown(Object Sender, KeyEventArgs E)
        {
            RegisterKey(E.KeyCode);
            KeyBoardDown[E.KeyCode] = DateTime.Now;
        }

        private void GlobalHookMouseDownExt(Object Sender, MouseEventExtArgs E)
        {
            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", E.Button, E.Timestamp);

            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

    } // End class

} // End namespace