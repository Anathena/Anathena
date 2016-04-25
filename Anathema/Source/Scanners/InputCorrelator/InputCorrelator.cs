using Anathema.Services.Snapshots;
using Anathema.Source.Utils;
using Anathema.User.UserSettings;
using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema.Scanners.InputCorrelator
{
    // I Originally thought these might be a good idea:
    // http://www.ucl.ac.uk/english-usage/staff/sean/resources/phimeasures.pdf
    // https://en.wikipedia.org/wiki/Contingency_table#Measures_of_association
    // It turns out a simple pentalty/reward system works fine

    class InputCorrelator : IInputCorrelatorModel
    {
        private Snapshot<Int16> Snapshot;

        private readonly IKeyboardMouseEvents InputHook;    // Input capturing class

        private Dictionary<Keys, DateTime> KeyBoardDown;    // List of keyboard down events
        private Dictionary<Keys, DateTime> KeyBoardUp;      // List of keyboard up events

        private Int32 VariableSize;     // Number of bytes to correlate at a time
        private Int32 TimeOutInterval;  // ms to consider a fired key event as active

        private InputNode InputConditionTree;
        
        private ProgressItem ScanProgress;
        private Object ProgressLock;

        public InputCorrelator()
        {
            ScanProgress = new ProgressItem();
            ProgressLock = new Object();
            ScanProgress.SetProgressLabel("Input Correlator");

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
            if (TargetNode.CanAddChild(Node))
                InputConditionTree = TargetNode.AddChild(Node);

            UpdateDisplay();
        }

        public override void ClearNodes()
        {
            if (InputConditionTree != null)
                InputConditionTree.Nodes.Clear();
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

        public override void Begin()
        {
            // Initialize labeled snapshot
            Snapshot = new Snapshot<Int16>(SnapshotManager.GetInstance().GetActiveSnapshot());

            if (Snapshot == null)
                return;

            Snapshot.SetVariableSize(VariableSize);
            Snapshot.SetAlignment(Settings.GetInstance().GetAlignmentSettings());

            // Initialize with no correlation
            Snapshot.SetElementLabels(0);
            TimeOutInterval = Settings.GetInstance().GetInputCorrelatorTimeOutInterval();

            // Initialize input dictionaries
            KeyBoardUp = new Dictionary<Keys, DateTime>();
            KeyBoardDown = new Dictionary<Keys, DateTime>();

            // Create input hook events
            //InputHook.MouseDownExt += GlobalHookMouseDownExt;
            InputHook.KeyUp += GlobalHookKeyUp;
            InputHook.KeyDown += GlobalHookKeyDown;

            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            Int32 ProcessedPages = 0;

            // Read memory to update previous and current values
            Snapshot.ReadAllSnapshotMemory();

            Boolean ConditionValid = InputConditionTree.EvaluateCondition(KeyBoardDown, Snapshot.GetTimeStamp(), TimeOutInterval);

            Parallel.ForEach(Snapshot.Cast<Object>(), (RegionObject) =>
            {
                SnapshotRegion<Int16> Region = (SnapshotRegion<Int16>)RegionObject;

                if (!Region.CanCompare())
                    return;

                foreach (SnapshotElement<Int16> Element in Region)
                {
                    if (Element.Changed())
                    {
                        if (ConditionValid)
                            Element.ElementLabel++;
                        else
                            Element.ElementLabel--;
                    }
                }

                lock (ProgressLock)
                {
                    ProcessedPages++;

                    if (ProcessedPages < Snapshot.GetRegionCount())
                        ScanProgress.UpdateProgress(ProcessedPages, Snapshot.GetRegionCount());
                }
            });

            OnEventUpdateScanCount(new ScannerEventArgs(this.ScanCount));
        }

        public override void End()
        {
            base.End();

            // Cleanup for the input hook
            //InputHook.MouseDownExt -= GlobalHookMouseDownExt;
            InputHook.KeyUp -= GlobalHookKeyUp;
            InputHook.KeyDown -= GlobalHookKeyDown;

            Snapshot.MarkAllInvalid();
            foreach (SnapshotRegion<Int16> Region in Snapshot)
                foreach (SnapshotElement<Int16> Element in Region)
                    if (Element.ElementLabel.Value > 0)
                        Element.Valid = true;

            Snapshot.DiscardInvalidRegions();
            Snapshot.SetScanMethod("Input Correlator");

            SnapshotManager.GetInstance().SaveSnapshot(Snapshot);
            ScanProgress.FinishProgress();

            CleanUp();

            Main.GetInstance().OpenLabelThresholder();
        }

        private void CleanUp()
        {
            Snapshot = null;
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