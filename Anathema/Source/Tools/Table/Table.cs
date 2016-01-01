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

        private List<AddressItem> AddressTable;
        private List<ScriptItem> ScriptTable;

        public Table()
        {
            InitializeObserver();
            AddressTable = new List<AddressItem>();
            ScriptTable = new List<ScriptItem>();

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

        public void AddSnapshotElement(SnapshotElement Element)
        {
            AddressTable.Add(new AddressItem(Element.BaseAddress, Element.GetElementType()));
        }

        public void AddSnapshotElementAtIndex(Int32 Index)
        {
            Snapshot SnapshotInstance = SnapshotManager.GetInstance().GetActiveSnapshot();
            AddSnapshotElement(SnapshotInstance[Index]);

            // UPDATE DISPLAY
        }

        public void AddPointer(RemotePointer Pointer, Type ElementType)
        {
            // ??
        }

        public void AddScript(String Script)
        {
            ScriptTable.Add(new ScriptItem(Script));
        }

        public override void BeginScan()
        {
            base.BeginScan();
        }

        protected override void UpdateScan()
        {
            // Request that the display be updated
            TableEventArgs Args = new TableEventArgs();
            OnEventRefreshDisplay(Args);
        }

        public override AddressItem GetAddressItemAt(Int32 Index)
        {
            return AddressTable[Index];
        }

        public override dynamic GetAddressValueAt(Int32 Index)
        {
            throw new NotImplementedException();
        }

        public override void SetAddressItemAt(Int32 Index, AddressItem AddressItem)
        {
            AddressTable[Index] = AddressItem;
        }

        public override void SetAddressValueAt(Int32 Index, dynamic Value)
        {
            throw new NotImplementedException();
        }

        public override ScriptItem GetScriptItemAt(Int32 Index)
        {
            return ScriptTable[Index];
        }

        public override void SetScriptItemAt(Int32 Index, ScriptItem ScriptItem)
        {
            ScriptTable[Index] = ScriptItem;
        }
    }
}