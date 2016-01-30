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
    class MemoryView : IMemoryViewModel, IProcessObserver
    {
        private MemorySharp MemoryEditor;
        private Snapshot Snapshot;
        
        private Int32 StartReadIndex;
        private Int32 EndReadIndex;
        private ConcurrentDictionary<Int32, String> IndexValueMap;
        private Boolean ForceRefreshFlag;

        public MemoryView()
        {
            InitializeProcessObserver();
            ForceRefreshFlag = false;
            IndexValueMap = new ConcurrentDictionary<Int32, String>();
            Begin();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public override void ForceRefresh()
        {
            ForceRefreshFlag = true;
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
            
            OnEventReadValues(new MemoryViewEventArgs());
        }

        public override void AddSelectionToTable(Int32 Index)
        {
            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            if (ActiveSnapshot == null)
                return;

            // Table.GetInstance().AddTableItem((UInt64)ActiveSnapshot[Index].BaseAddress, ScanType);
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