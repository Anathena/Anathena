using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Concurrent;
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
        private Snapshot Snapshot;

        private Type ScanType;

        private Int32 StartReadIndex;
        private Int32 EndReadIndex;
        private ConcurrentDictionary<Int32, String> IndexValueMap;

        private Results()
        {
            InitializeProcessObserver();
            SetScanType(typeof(Int32));
            IndexValueMap = new ConcurrentDictionary<Int32, String>();
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

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public void EnableResults()
        {
            OnEventEnableResults(new ResultsEventArgs());
            Begin();
        }

        public void DisableResults()
        {
            CancelFlag = true;
            OnEventDisableResults(new ResultsEventArgs());
        }

        public override void SetScanType(Type ScanType)
        {
            this.ScanType = ScanType;
        }

        public override Type GetScanType()
        {
            return ScanType;
        }

        public override void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            this.StartReadIndex = StartReadIndex;
            this.EndReadIndex = EndReadIndex;
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();
            if (Snapshot != ActiveSnapshot)
            {
                Snapshot = ActiveSnapshot;

                UInt64 MemorySize = (Snapshot == null ? 0 : Snapshot.GetElementCount());
                // Send the size of the filtered memory to the display
                ResultsEventArgs Args = new ResultsEventArgs();
                Args.MemorySize = MemorySize;
                OnEventFlushCache(Args);
                OnEventUpdateMemorySize(Args);
                return;
            }

            if (Snapshot == null)
                return;

            IndexValueMap.Clear();

            for (Int32 Index = StartReadIndex; Index < EndReadIndex; Index++)
            {
                if (Index >= (Int32)Snapshot.GetElementCount())
                    continue;

                Boolean ReadSuccess;
                String Value = MemoryEditor.Read(ScanType, Snapshot[Index].BaseAddress, out ReadSuccess, false).ToString();

                IndexValueMap[Index] = Value;
            }

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
            if (Snapshot == null || Index >= (Int32)Snapshot.GetElementCount())
                return IntPtr.Zero;

            return Snapshot[Index].BaseAddress;
        }

        public override String GetValueAtIndex(Int32 Index)
        {
            if (IndexValueMap.ContainsKey(Index))
                return IndexValueMap[Index];

            return "-";
        }

        public override String GetLabelAtIndex(Int32 Index)
        {
            if (Snapshot == null || Index >= (Int32)Snapshot.GetElementCount())
                return "-";

            dynamic Label = String.Empty;
            if (((dynamic)Snapshot)[Index].ElementLabel != null)
                Label = ((dynamic)Snapshot)[Index].ElementLabel;

            return Label.ToString();
        }

    } // End class

} // End namespace