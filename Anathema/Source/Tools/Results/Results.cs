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
            BeginScan();
        }

        public static Results GetInstance()
        {
            if (ResultsInstance == null)
                ResultsInstance = new Results();
            return ResultsInstance;
        }

        ~Results()
        {
            EndScan();
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

        public override void BeginScan()
        {
            base.BeginScan();
        }

        public override void AddSelectionToTable(Int32 Index)
        {
            if (!SnapshotManager.GetInstance().HasActiveSnapshot())
                return;

            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();
            dynamic Element = ActiveSnapshot[Index];
            Element.ElementType = ScanType;

            Table.GetInstance().AddSnapshotElement(Element);
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

            Boolean ReadSuccess = false;
            var @switch = new Dictionary<Type, Action> {
                    { typeof(Byte), () => Value = MemoryEditor.Read<Byte>(Address, out ReadSuccess, false) },
                    { typeof(SByte), () => Value = MemoryEditor.Read<SByte>(Address, out ReadSuccess, false) },
                    { typeof(Int16), () => Value = MemoryEditor.Read<Int16>(Address, out ReadSuccess, false) },
                    { typeof(Int32), () => Value = MemoryEditor.Read<Int32>(Address, out ReadSuccess, false) },
                    { typeof(Int64), () => Value = MemoryEditor.Read<Int64>(Address, out ReadSuccess, false) },
                    { typeof(UInt16), () => Value = MemoryEditor.Read<UInt16>(Address, out ReadSuccess, false) },
                    { typeof(UInt32), () => Value = MemoryEditor.Read<UInt32>(Address, out ReadSuccess, false) },
                    { typeof(UInt64), () => Value = MemoryEditor.Read<UInt64>(Address, out ReadSuccess, false) },
                    { typeof(Single), () => Value = MemoryEditor.Read<Single>(Address, out ReadSuccess, false) },
                    { typeof(Double), () => Value = MemoryEditor.Read<Double>(Address, out ReadSuccess, false) },
                };

            if (@switch.ContainsKey(ScanType))
                @switch[ScanType]();

            if (!ReadSuccess)
                Value = "?";

            return Value;
        }

        public override dynamic GetLabelAtIndex(Int32 Index)
        {
            if (!SnapshotManager.GetInstance().HasActiveSnapshot())
                return "-";

            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            if (Index >= (Int32)ActiveSnapshot.GetMemorySize())
                return "-";

            dynamic Label = String.Empty;
            if (((dynamic)ActiveSnapshot[Index]).ElementLabel != null)
                Label = ((dynamic)ActiveSnapshot[Index])[Index].ElementLabel;

            return Label;
        }

        protected override void UpdateScan()
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
