using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class Results : IResultsModel, IProcessObserver, ISnapshotManagerObserver
    {
        private static Results ResultsInstance;
        private MemorySharp MemoryEditor;

        // User specified variables
        private Type ScanType;

        private Results()
        {
            InitializeProcessObserver();
            InitializeSnapshotManagerObserver();
            SetScanType(typeof(Int32));
            Begin();
        }

        public static Results GetInstance()
        {
            if (ResultsInstance == null)
                ResultsInstance = new Results();
            return ResultsInstance;
        }

        ~Results()
        {
            End();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void InitializeSnapshotManagerObserver()
        {
            SnapshotManager.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public void SnapshotUpdated()
        {
            Snapshot Snapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            UInt64 MemorySize = (Snapshot == null ? 0 : Snapshot.GetMemorySize());

            // Send the size of the filtered memory to the display
            ResultsEventArgs Args = new ResultsEventArgs();
            Args.MemorySize = MemorySize;
            OnEventFlushCache(Args);
            OnEventUpdateMemorySize(Args);
        }

        public override void SetScanType(Type ScanType)
        {
            this.ScanType = ScanType;
        }

        public override Type GetScanType()
        {
            return ScanType;
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            OnEventReadValues(new ResultsEventArgs());
        }

        public override void AddSelectionToTable(Int32 Index)
        {
            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            if (ActiveSnapshot == null)
                return;

            Table.GetInstance().AddTableItem((UInt64)ActiveSnapshot[Index].BaseAddress, ScanType);
        }

        public override IntPtr GetAddressAtIndex(Int32 Index)
        {
            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            if (ActiveSnapshot == null || Index >= (Int32)ActiveSnapshot.GetElementCount())
                return IntPtr.Zero;

            return ActiveSnapshot[Index].BaseAddress;
        }

        public override dynamic GetValueAtIndex(Int32 Index)
        {
            dynamic Value = "-";

            IntPtr Address = GetAddressAtIndex(Index);
            Boolean ReadSuccess;

            return MemoryEditor.Read(ScanType, Address, out ReadSuccess, false);
        }

        public override dynamic GetLabelAtIndex(Int32 Index)
        {
            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            if (ActiveSnapshot == null || Index >= (Int32)ActiveSnapshot.GetElementCount())
                return "-";

            dynamic Label = String.Empty;
            if (((dynamic)ActiveSnapshot)[Index].ElementLabel != null)
                Label = ((dynamic)ActiveSnapshot)[Index].ElementLabel;
            return Label;
        }

    } // End class

} // End namespace