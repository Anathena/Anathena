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
    class Results : IResultsModel, IProcessObserver
    {
        private static Results ResultsInstance;
        private MemorySharp MemoryEditor;

        // User specified variables
        private Type ScanType;

        private Results()
        {
            InitializeObserver();
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

        public void InitializeObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
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

        public override void AddSelectionToTable(Int32 Index)
        {
            if (!SnapshotManager.GetInstance().HasActiveSnapshot())
                return;

            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            Table.GetInstance().AddTableItem((UInt64)ActiveSnapshot[Index].BaseAddress, ScanType);
        }

        public override IntPtr GetAddressAtIndex(Int32 Index)
        {
            if (!SnapshotManager.GetInstance().HasActiveSnapshot())
                return IntPtr.Zero;

            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            if (Index >= (Int32)ActiveSnapshot.GetMemorySize())
                return IntPtr.Zero;

            return ActiveSnapshot[Index].BaseAddress;
        }

        public override dynamic GetValueAtIndex(Int32 Index)
        {
            dynamic Value = "-";

            if (!SnapshotManager.GetInstance().HasActiveSnapshot())
                return Value;

            IntPtr Address = GetAddressAtIndex(Index);
            Boolean ReadSuccess;

            return MemoryEditor.Read(ScanType, Address, out ReadSuccess, false);
        }

        public override dynamic GetLabelAtIndex(Int32 Index)
        {
            if (!SnapshotManager.GetInstance().HasActiveSnapshot())
                return "-";

            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            if (Index >= (Int32)ActiveSnapshot.GetMemorySize())
                return "-";

            dynamic Label = String.Empty;
            if (((dynamic)ActiveSnapshot)[Index].ElementLabel != null)
                Label = ((dynamic)ActiveSnapshot)[Index].ElementLabel;
            return Label;
        }

        protected override void Update()
        {
            UInt64 MemorySize;

            if (!SnapshotManager.GetInstance().HasActiveSnapshot())
                MemorySize = 0;
            else
                MemorySize = SnapshotManager.GetInstance().GetActiveSnapshot().GetMemorySize();

            // Send the size of the filtered memory to the display
            ResultsEventArgs Args = new ResultsEventArgs();
            Args.MemorySize = MemorySize;
            OnEventUpdateMemorySize(Args);
            OnEventRefreshDisplay(Args);
        }
    }
}
