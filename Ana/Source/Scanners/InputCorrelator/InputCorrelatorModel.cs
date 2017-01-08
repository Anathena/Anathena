namespace Ana.Source.Scanners.InputCorrelator
{
    using Engine;
    using Engine.Input.Controller;
    using Engine.Input.HotKeys;
    using Engine.Input.Keyboard;
    using Engine.Input.Mouse;
    using LabelThresholder;
    using SharpDX.DirectInput;
    using Snapshots;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UserSettings;

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

        private Snapshot Snapshot { get; set; }

        private Action UpdateScanCount { get; set; }

        /// <summary>
        /// Gets or sets the time to consider a fired key event as active.
        /// </summary>
        private Int32 TimeOutIntervalMs { get; set; }

        private DateTime LastActivated { get; set; }

        public void EditKeys()
        {
            View.Editors.HotkeyEditor hotKeyEditor = new View.Editors.HotkeyEditor(this.HotKeys);

            if (hotKeyEditor.ShowDialog() == true)
            {
                this.HotKeys = new List<IHotkey>(hotKeyEditor.HotkeyEditorViewModel.Hotkeys);
            }
        }

        public override void Begin()
        {
            this.InitializeObjects();

            // Initialize labeled snapshot
            this.Snapshot = SnapshotManager.GetInstance().GetActiveSnapshot(createIfNone: true).Clone(this.ScannerName);
            this.Snapshot.LabelType = typeof(Int16);

            if (this.Snapshot == null)
            {
                this.End();
                return;
            }

            // Initialize with no correlation
            this.Snapshot.SetElementLabels<Int16>(0);
            this.TimeOutIntervalMs = SettingsViewModel.GetInstance().InputCorrelatorTimeOutInterval;

            base.Begin();
        }

        /// <summary>
        /// Event received when a key is pressed.
        /// </summary>
        /// <param name="key">The key that was pressed.</param>
        public void OnKeyPress(Key key)
        {
        }

        /// <summary>
        /// Event received when a key is down.
        /// </summary>
        /// <param name="key">The key that is down.</param>
        public void OnKeyDown(Key key)
        {
        }

        /// <summary>
        /// Event received when a key is released.
        /// </summary>
        /// <param name="key">The key that was released.</param>
        public void OnKeyRelease(Key key)
        {
        }

        /// <summary>
        /// Event received when a set of keys are down.
        /// </summary>
        /// <param name="pressedKeys">The down keys.</param>
        public void OnUpdateAllDownKeys(HashSet<Key> pressedKeys)
        {
            // If any of our keyboard hotkeys include the current set of pressed keys, trigger activation/deactivation
            if (this.HotKeys.Where(x => x.GetType().IsAssignableFrom(typeof(KeyboardHotkey))).Cast<KeyboardHotkey>().Any(x => x.ActivationKeys.All(y => pressedKeys.Contains(y))))
            {
                this.LastActivated = DateTime.Now;
            }
        }

        protected override void OnUpdate()
        {
            // Read memory to update previous and current values
            this.Snapshot.ReadAllMemory();

            Boolean conditionValid = this.IsInputConditionValid(this.Snapshot.GetTimeSinceLastUpdate());

            // Note the duplicated code here is an optimization to minimize comparisons done per iteration
            if (conditionValid)
            {
                Parallel.ForEach(
                this.Snapshot.Cast<Object>(),
                SettingsViewModel.GetInstance().ParallelSettings,
                (regionObject) =>
                {
                    SnapshotRegion region = regionObject as SnapshotRegion;

                    if (!region.CanCompare())
                    {
                        return;
                    }

                    foreach (SnapshotElementRef element in region)
                    {
                        if (element.Changed())
                        {
                            ((dynamic)element).ElementLabel++;
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
                    SnapshotRegion region = regionObject as SnapshotRegion;

                    if (!region.CanCompare())
                    {
                        return;
                    }

                    foreach (SnapshotElementRef element in region)
                    {
                        if (element.Changed())
                        {
                            ((dynamic)element).ElementLabel--;
                        }
                    }
                });
            }

            base.OnUpdate();
            this.UpdateScanCount?.Invoke();
        }

        /// <summary>
        /// Called when the repeated task completes.
        /// </summary>
        protected override void OnEnd()
        {
            // Prefilter items with negative penalties (ie constantly changing variables)
            this.Snapshot.SetAllValidBits(false);
            foreach (SnapshotRegion region in this.Snapshot)
            {
                foreach (SnapshotElementRef element in region)
                {
                    if ((Int16)element.ElementLabel > 0)
                    {
                        element.SetValid(true);
                    }
                }
            }

            this.Snapshot.DiscardInvalidRegions();

            SnapshotManager.GetInstance().SaveSnapshot(this.Snapshot);

            this.CleanUp();
            LabelThresholderViewModel.GetInstance().OpenLabelThresholder();
            base.OnEnd();
        }

        private void InitializeObjects()
        {
            this.LastActivated = DateTime.MinValue;
            this.InitializeListeners();
        }

        private void InitializeListeners()
        {
            EngineCore.GetInstance().Input?.GetKeyboardCapture().Subscribe(this);
            EngineCore.GetInstance().Input?.GetControllerCapture().Subscribe(this);
            EngineCore.GetInstance().Input?.GetMouseCapture().Subscribe(this);
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

            EngineCore.GetInstance().Input?.GetKeyboardCapture().Unsubscribe(this);
            EngineCore.GetInstance().Input?.GetControllerCapture().Unsubscribe(this);
            EngineCore.GetInstance().Input?.GetMouseCapture().Unsubscribe(this);
        }
    }
    //// End class
}
//// End namespace