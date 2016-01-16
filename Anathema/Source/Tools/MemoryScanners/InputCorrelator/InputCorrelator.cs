﻿using Binarysharp.MemoryManagement;
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
    /// <summary>
    /// http://www.ucl.ac.uk/english-usage/staff/sean/resources/phimeasures.pdf
    /// https://en.wikipedia.org/wiki/Contingency_table#Measures_of_association
    /// </summary>
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

            // Initialize a root for our tree
            InputConditionTree = new InputNode(InputNode.NodeTypeEnum.OR);
        }

        public override void SetVariableSize(int VariableSize)
        {
            this.VariableSize = VariableSize;
        }

        public override void AddNode(Stack<Int32> SelectedIndicies, InputNode.NodeTypeEnum NodeType)
        {
            // Determine the node the user is attempting to add a child to
            InputNode TargetNode = InputConditionTree;
            while (SelectedIndicies.Count > 0)
                TargetNode = TargetNode.GetChildAtIndex(SelectedIndicies.Pop());

            // Add the child
            TargetNode.AddChild(new InputNode(NodeType));
        }

        public override void DeleteNode(Stack<Int32> SelectedIndicies)
        {
            // Deleting root is not okay
            if (SelectedIndicies.Count == 0)
                return;
            
            // Determine the node the user is attempting to delete
            InputNode TargetNode = InputConditionTree;
            while (SelectedIndicies.Count > 0)
                TargetNode = TargetNode.GetChildAtIndex(SelectedIndicies.Pop());
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

            Boolean ConditionValid = InputConditionValid(Snapshot.GetTimeStamp());

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

        private Boolean InputConditionValid(DateTime ScanTime)
        {
            if (!KeyBoardDown.ContainsKey(UserInput))
                return false;

            // Determine if key was pressed within specified time
            if (Math.Abs((ScanTime - KeyBoardDown[UserInput]).TotalMilliseconds) < WaitTime)
                return true;

            return false;
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