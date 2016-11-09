namespace Ana.Source.Scanners.InputCorrelator
{
    using Engine;
    using Engine.Input.Controller;
    using Engine.Input.HotKeys;
    using Engine.Input.Keyboard;
    using Engine.Input.Mouse;
    using LabelThresholder;
    using Results.ScanResults;
    using SharpDX.DirectInput;
    using Snapshots;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UserSettings;

    /// <summary>
    /// 
    /// </summary>
    internal class InputCorrelatorModel : ScannerBase, IKeyboardObserver, IControllerObserver, IMouseObserver
    {
        private List<IHotkey> hotKeys;

        public InputCorrelatorModel(Action updateScanCount) : base("Input Correlator")
        {
            this.UpdateScanCount = updateScanCount;
        }

        public List<IHotkey> HotKeys
        {
            get
            {
                return this.hotKeys;
            }

            set
            {
                this.hotKeys = value;
            }
        }

        private Snapshot<Int16> Snapshot { get; set; }

        private Action UpdateScanCount { get; set; }

        /// <summary>
        /// Gets or sets the time to consider a fired key event as active
        /// </summary>
        private Int32 TimeOutIntervalMs { get; set; }

        private DateTime LastActivated { get; set; }

        public void EditKeys()
        {
            View.HotkeyEditor hotKeyEditor = new View.HotkeyEditor(this.HotKeys);

            if (hotKeyEditor.ShowDialog() == true)
            {
                this.HotKeys = new List<IHotkey>(hotKeyEditor.HotkeyEditorViewModel.Hotkeys);
            }
        }

        public override void Begin()
        {
            this.InitializeObjects();

            // Initialize labeled snapshot
            this.Snapshot = SnapshotManager.GetInstance().GetActiveSnapshot(createIfNone: true).CloneAs<Int16>();

            if (this.Snapshot == null)
            {
                this.End();
                return;
            }

            this.Snapshot.ElementType = ScanResultsViewModel.GetInstance().ActiveType;
            this.Snapshot.Alignment = SettingsViewModel.GetInstance().Alignment;

            // Initialize with no correlation
            this.Snapshot.SetElementLabels(0);
            this.TimeOutIntervalMs = SettingsViewModel.GetInstance().InputCorrelatorTimeOutInterval;

            base.Begin();
        }

        public void OnKeyPress(Key key)
        {
        }

        public void OnKeyDown(Key key)
        {
        }

        public void OnKeyRelease(Key key)
        {
        }

        public void OnUpdateAllDownKeys(HashSet<Key> pressedKeys)
        {
            // If any of our keyboard hotkeys include the current set of pressed keys, trigger activation/deactivation
            if (this.HotKeys.Where(x => x.GetType().IsAssignableFrom(typeof(KeyboardHotkey))).Cast<KeyboardHotkey>().Any(x => x.GetActivationKeys().All(y => pressedKeys.Contains(y))))
            {
                this.LastActivated = DateTime.Now;
            }
        }

        protected override void OnUpdate()
        {
            // Read memory to update previous and current values
            this.Snapshot.ReadAllSnapshotMemory();

            Boolean conditionValid = this.IsInputConditionValid(this.Snapshot.TimeStamp);

            // Note the duplicated code here is an optimization to minimize comparisons done per iteration
            if (conditionValid)
            {
                Parallel.ForEach(
                this.Snapshot.Cast<Object>(),
                SettingsViewModel.GetInstance().ParallelSettings,
                (regionObject) =>
                {
                    SnapshotRegion<Int16> region = (SnapshotRegion<Int16>)regionObject;

                    if (!region.CanCompare())
                    {
                        return;
                    }

                    foreach (SnapshotElement<Int16> element in region)
                    {
                        if (element.Changed())
                        {
                            element.ElementLabel++;
                        }
                    }
                });
            }
            else
            {
                Parallel.ForEach(
                this.Snapshot.Cast<Object>(),
                SettingsViewModel.GetInstance().ParallelSettings,
                (regionObject) =>
                {
                    SnapshotRegion<Int16> region = regionObject as SnapshotRegion<Int16>;

                    if (!region.CanCompare())
                    {
                        return;
                    }

                    foreach (SnapshotElement<Int16> element in region)
                    {
                        if (element.Changed())
                        {
                            element.ElementLabel--;
                        }
                    }
                });
            }

            base.OnUpdate();
            this.UpdateScanCount?.Invoke();
        }

        /// <summary>
        /// Called when the repeated task completes
        /// </summary>
        protected override void OnEnd()
        {
            base.OnEnd();

            // Prefilter items with negative penalties (ie constantly changing variables)
            this.Snapshot.MarkAllInvalid();
            foreach (SnapshotRegion<Int16> region in this.Snapshot)
            {
                foreach (SnapshotElement<Int16> element in region)
                {
                    if (element.ElementLabel.Value > 0)
                    {
                        element.Valid = true;
                    }
                }
            }

            this.Snapshot.DiscardInvalidRegions();
            this.Snapshot.ScanMethod = "Input Correlator";

            SnapshotManager.GetInstance().SaveSnapshot(this.Snapshot);

            this.CleanUp();
            LabelThresholderViewModel.GetInstance().IsVisible = true;
            LabelThresholderViewModel.GetInstance().IsActive = true;
        }

        private void InitializeObjects()
        {
            this.LastActivated = DateTime.MinValue;
            this.InitializeListeners();
        }

        private void InitializeListeners()
        {
            EngineCore.GetInstance()?.Input?.GetKeyboardCapture().Subscribe(this);
            EngineCore.GetInstance()?.Input?.GetControllerCapture().Subscribe(this);
            EngineCore.GetInstance()?.Input?.GetMouseCapture().Subscribe(this);
        }

        private Boolean IsInputConditionValid(DateTime updateTime)
        {
            if ((updateTime - this.LastActivated).TotalMilliseconds < this.TimeOutIntervalMs)
            {
                return true;
            }

            return false;
        }

        private void CleanUp()
        {
            this.Snapshot = null;

            EngineCore.GetInstance()?.Input?.GetKeyboardCapture().Unsubscribe(this);
            EngineCore.GetInstance()?.Input?.GetControllerCapture().Unsubscribe(this);
            EngineCore.GetInstance()?.Input?.GetMouseCapture().Unsubscribe(this);
        }
    }
    //// End class
}
//// End namespace