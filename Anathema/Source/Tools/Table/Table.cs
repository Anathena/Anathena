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
            dynamic Value = "-";
            IntPtr Address = AddressTable[Index].Address;

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

            if (@switch.ContainsKey(AddressTable[Index].ElementType))
                @switch[AddressTable[Index].ElementType]();

            if (!ReadSuccess)
                Value = "?";

            return Value;
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