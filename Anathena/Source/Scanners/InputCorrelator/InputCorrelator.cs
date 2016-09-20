using Ana.Source.Controller;
using Ana.Source.Engine;
using Ana.Source.Engine.InputCapture.Controller;
using Ana.Source.Engine.InputCapture.HotKeys;
using Ana.Source.Engine.InputCapture.Keyboard;
using Ana.Source.Engine.InputCapture.Mouse;
using Ana.Source.Engine.Processes;
using Ana.Source.Project.ProjectItems.TypeEditors;
using Ana.Source.Snapshots;
using Ana.Source.UserSettings;
using Ana.Source.Utils;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ana.Source.Scanners.InputCorrelator
{
    /// <summary>
    /// I Originally thought these might be a good idea:
    /// http://www.ucl.ac.uk/english-usage/staff/sean/resources/phimeasures.pdf
    /// https://en.wikipedia.org/wiki/Contingency_table#Measures_of_association
    /// It turns out a simple pentalty/reward system works fine
    /// </summary>
    class InputCorrelator : IInputCorrelatorModel, IProcessObserver, IKeyboardObserver, IControllerObserver, IMouseObserver
    {
        private EngineCore EngineCore;
        private Snapshot<Int16> Snapshot;

        private List<IHotKey> _HotKeys;
        public List<IHotKey> HotKeys { get { return _HotKeys; } set { _HotKeys = value; OnUpdateHotKeys(); } }

        private Int32 VariableSize;         // Number of bytes to correlate at a time
        private Int32 TimeOutIntervalMs;    // Time to consider a fired key event as active
        private ProgressItem ScanProgress;
        private DateTime LastActivated;

        public InputCorrelator()
        {
            InitializeProcessObserver();
        }

        private void InitializeObjects()
        {
            ScanProgress = new ProgressItem();
            LastActivated = DateTime.MinValue;

            ScanProgress.SetProgressLabel("Input Correlator");
            InitializeListeners();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
        }

        public override void EditKeys()
        {
            HotKeyEditor HotKeyEditor = new HotKeyEditor();
            HotKeyEditor.SetHotKeys(HotKeys);

            if (HotKeyEditor.ShowDialog() == DialogResult.OK)
                HotKeys = new List<IHotKey>(HotKeyEditor.GetHotKeys());
        }

        public override void SetVariableSize(Int32 VariableSize)
        {
            this.VariableSize = VariableSize;
        }

        private void OnUpdateHotKeys()
        {
            InputCorrelatorEventArgs Args = new InputCorrelatorEventArgs();
            Args.HotKeys = HotKeys;
            OnEventUpdateHotKeys(Args);
        }

        public override void Begin()
        {
            InitializeObjects();

            // Initialize labeled snapshot
            Snapshot = new Snapshot<Int16>(SnapshotManager.GetInstance().GetActiveSnapshot());

            if (Snapshot == null)
                return;

            Snapshot.SetVariableSize(VariableSize);
            Snapshot.SetAlignment(Settings.GetInstance().GetAlignmentSettings());

            // Initialize with no correlation
            Snapshot.SetElementLabels(0);
            TimeOutIntervalMs = Settings.GetInstance().GetInputCorrelatorTimeOutInterval();

            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            // Read memory to update previous and current values
            Snapshot.ReadAllSnapshotMemory();

            Boolean ConditionValid = IsInputConditionValid(Snapshot.GetTimeStamp());

            // Note the duplicated code here is an optimization to minimize comparisons done per iteration
            if (ConditionValid)
            {
                Parallel.ForEach(Snapshot.Cast<Object>(), (RegionObject) =>
                {
                    SnapshotRegion<Int16> Region = (SnapshotRegion<Int16>)RegionObject;

                    if (!Region.CanCompare())
                        return;

                    foreach (SnapshotElement<Int16> Element in Region)
                    {
                        if (Element.Changed())
                            Element.ElementLabel++;
                    }
                });
            }
            else
            {
                Parallel.ForEach(Snapshot.Cast<Object>(), (RegionObject) =>
                {
                    SnapshotRegion<Int16> Region = (SnapshotRegion<Int16>)RegionObject;

                    if (!Region.CanCompare())
                        return;

                    foreach (SnapshotElement<Int16> Element in Region)
                    {
                        if (Element.Changed())
                            Element.ElementLabel--;
                    }
                });
            }

            ScanProgress.FinishProgress();

            OnEventUpdateScanCount(new ScannerEventArgs(this.ScanCount));
        }

        private Boolean IsInputConditionValid(DateTime UpdateTime)
        {
            if ((UpdateTime - LastActivated).TotalMilliseconds < TimeOutIntervalMs)
                return true;

            return false;
        }

        protected override void End()
        {
            base.End();

            // Prefilter items with negative penalties (ie constantly changing variables)
            Snapshot.MarkAllInvalid();
            foreach (SnapshotRegion<Int16> Region in Snapshot)
                foreach (SnapshotElement<Int16> Element in Region)
                    if (Element.ElementLabel.Value > 0)
                        Element.Valid = true;

            Snapshot.DiscardInvalidRegions();
            Snapshot.SetScanMethod("Input Correlator");

            SnapshotManager.GetInstance().SaveSnapshot(Snapshot);

            CleanUp();

            Main.GetInstance().OpenLabelThresholder();
        }

        private void InitializeListeners()
        {
            if (EngineCore == null)
                return;

            EngineCore.InputManager.GetKeyboardCapture().Subscribe(this);
            EngineCore.InputManager.GetControllerCapture().Subscribe(this);
            EngineCore.InputManager.GetMouseCapture().Subscribe(this);
        }

        private void CleanUp()
        {
            Snapshot = null;

            if (EngineCore == null)
                return;

            EngineCore.InputManager.GetKeyboardCapture().Unsubscribe(this);
            EngineCore.InputManager.GetControllerCapture().Unsubscribe(this);
            EngineCore.InputManager.GetMouseCapture().Unsubscribe(this);
        }

        public void OnKeyPress(Key Key) { }

        public void OnKeyDown(Key Key) { }

        public void OnKeyRelease(Key Key) { }

        public void OnUpdateAllDownKeys(HashSet<Key> PressedKeys)
        {
            // If any of our keyboard hotkeys include the current set of pressed keys, trigger activation/deactivation
            if (HotKeys.Where(X => X.GetType().IsAssignableFrom(typeof(KeyboardHotKey))).Cast<KeyboardHotKey>().Any(X => X.GetActivationKeys().All(Y => PressedKeys.Contains(Y))))
                LastActivated = DateTime.Now;
        }

    } // End class

} // End namespace