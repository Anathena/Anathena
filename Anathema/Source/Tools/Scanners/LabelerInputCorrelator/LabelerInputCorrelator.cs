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
    /// <summary>
    /// http://www.ucl.ac.uk/english-usage/staff/sean/resources/phimeasures.pdf
    /// https://en.wikipedia.org/wiki/Contingency_table#Measures_of_association
    /// </summary>
    class LabelerInputCorrelator : ILabelerInputCorrelatorModel
    {
        private Snapshot<Single> LabeledSnapshot;

        private readonly IKeyboardMouseEvents InputHook;        // Input capturing class

        private Dictionary<Keys, DateTime> KeyBoardDown;    // List of keyboard down events
        private Dictionary<Keys, DateTime> KeyBoardUp;      // List of keyboard up events

        // User specified variables:
        private Int32 VariableSize; // Number of bytes to correlate at a time
        private Int32 WaitTime;     // Time (ms) to process new changes as correlations
        private Keys UserInput;     // Whatever

        public LabelerInputCorrelator()
        {
            InputHook = Hook.GlobalEvents();
        }

        public override void SetVariableSize(int VariableSize)
        {
            this.VariableSize = VariableSize;
        }

        public override void BeginScan()
        {
            // Initialize labeled snapshot
            LabeledSnapshot = new Snapshot<Single>(SnapshotManager.GetInstance().GetActiveSnapshot());
            LabeledSnapshot.SetVariableSize(VariableSize);

            // TEMP: variables that should be user-tuned
            UserInput = Keys.D;
            WaitTime = 800;

            // Initialize with no correlation
            foreach (SnapshotRegion<Single> Region in LabeledSnapshot)
                foreach (SnapshotElement<Single> Element in Region)
                    Element.MemoryLabel = 0.0f;

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
            LabeledSnapshot.ReadAllSnapshotMemory();

            foreach (SnapshotRegion<Single> Region in LabeledSnapshot)
            {
                if (!Region.CanCompare())
                    continue;

                foreach (SnapshotElement<Single> Element in Region)
                {
                    if (Element.Changed())
                    {
                        if (InputConditionValid(LabeledSnapshot.GetTimeStamp()))
                            Element.MemoryLabel += 1.0f;
                        else
                            Element.MemoryLabel -= 0.05f;
                    }
                }
            }
        }

        public override void EndScan()
        {
            base.EndScan();

            // Cleanup for the input hook
            InputHook.KeyUp -= GlobalHookKeyUp;
            InputHook.MouseDownExt -= GlobalHookMouseDownExt;
            InputHook.KeyDown -= GlobalHookKeyDown;
            InputHook.Dispose();

            List<SnapshotRegion<Single>> FilteredElements = new List<SnapshotRegion<Single>>();

            Single MaxValue = 1.0f;
            foreach (SnapshotRegion<Single> Region in LabeledSnapshot)
                foreach (SnapshotElement<Single> Element in Region)
                    if (Element.MemoryLabel.Value > MaxValue)
                        MaxValue = Element.MemoryLabel.Value;

            LabeledSnapshot.MarkAllValid();
            foreach (SnapshotRegion<Single> Region in LabeledSnapshot)
            {
                foreach (SnapshotElement<Single> Element in Region)
                {
                    Element.MemoryLabel = Element.MemoryLabel / MaxValue;
                    if (Element.MemoryLabel.Value > 0.80f)
                        Element.Valid = true;
                }
            } 

            Snapshot<Single> FilteredSnapshot = new Snapshot<Single>(FilteredElements.ToArray());
            FilteredSnapshot.SetScanMethod("Input Correlator");
            SnapshotManager.GetInstance().SaveSnapshot(FilteredSnapshot);
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