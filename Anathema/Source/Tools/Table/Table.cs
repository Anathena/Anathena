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

        private Table()
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
            AddressTable.Add(new AddressItem(Element.BaseAddress, Element.ElementType));

            TableEventArgs Args = new TableEventArgs();
            Args.AddressTableItemCount = AddressTable.Count;
            OnEventUpdateAddressTableItemCount(Args);
        }

        public override void SetFrozenAt(Int32 Index, Boolean Activated)
        {
            AddressTable[Index].SetActivationState(Activated);
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
            Boolean ReadSuccess;
            AddressTable[Index].Value = MemoryEditor.Read(AddressTable[Index].ElementType, AddressTable[Index].Address, out ReadSuccess, false);
            return AddressTable[Index];
        }

        public override void SetAddressItemAt(Int32 Index, AddressItem AddressItem)
        {
            // Copy over attributes from the new item
            AddressTable[Index].Description = AddressItem.Description;
            AddressTable[Index].ElementType = AddressItem.ElementType;
            AddressTable[Index].Address = AddressItem.Address;
            AddressTable[Index].Offsets = AddressItem.Offsets;

            // Force update of value, regardless if frozen or not
            AddressTable[Index].ForceUpdateValue(AddressItem.Value);

            if (AddressTable[Index].Value != null)
                MemoryEditor.Write(AddressTable[Index].ElementType, AddressTable[Index].Address, AddressTable[Index].Value, false);
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