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
    class Table : ITableModel, IProcessObserver
    {
        private static Table TableInstance;
        private MemorySharp MemoryEditor;

        private const Int32 DisplayCount = 1000;
        private Type ScanType;

        private Table()
        {
            InitializeObserver();
            UpdateScanType(typeof(Int32));
            BeginScan();
        }

        public static Table GetInstance()
        {
            if (TableInstance == null)
                TableInstance = new Table();
            return TableInstance;
        }

        ~Table()
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

        public override void UpdateScanType(Type ScanType)
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

        public override IntPtr GetAddressAtIndex(Int32 Index)
        {
            if (!SnapshotManager.GetInstance().HasActiveSnapshot())
                return IntPtr.Zero;

            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();
            return ActiveSnapshot[Index].BaseAddress;
        }

        public override dynamic GetValueAtIndex(Int32 Index)
        {
            dynamic Value = "-";
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
                return IntPtr.Zero;

            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            dynamic Label = "";

            if (ActiveSnapshot.GetType() != typeof(Snapshot))
            {
                dynamic LabeledSnapshot = ActiveSnapshot;
                Label = LabeledSnapshot[Index].MemoryLabel;
            }

            return Label;
        }

        protected override void UpdateScan()
        {
            if (!SnapshotManager.GetInstance().HasActiveSnapshot())
                return;

            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            // Send the size of the filtered memory to the display
            TableEventArgs Args = new TableEventArgs();
            Args.MemorySize = ActiveSnapshot.GetMemorySize();
            OnEventUpdateMemorySize(Args);
            OnEventRefreshDisplay(Args);
        }
    }
}
