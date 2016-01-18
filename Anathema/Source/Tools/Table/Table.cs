using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Anathema
{
    [DataContract()]
    public class TableData
    {
        [DataMember()]
        public List<AddressItem> AddressTable;

        [DataMember()]
        public List<ScriptItem> ScriptTable;
        
        public List<FiniteStateMachine> FiniteStateMachineTable;

        public TableData()
        {
            AddressTable = new List<AddressItem>();
            ScriptTable = new List<ScriptItem>();
            FiniteStateMachineTable = new List<FiniteStateMachine>();
        }
    }

    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    /// 
    class Table : ITableModel, IProcessObserver
    {
        public enum TableColumnEnum
        {
            Frozen,
            Description,
            ValueType,
            Address,
            Value
        }



        private static Table TableInstance;
        private MemorySharp MemoryEditor;

        private TableData CurrentTableData;

        private Table()
        {
            InitializeObserver();
            CurrentTableData = new TableData();

            Begin();
        }

        public static Table GetInstance()
        {
            if (TableInstance == null)
                TableInstance = new Table();
            return TableInstance;
        }

        ~Table()
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

        public override Boolean SaveTable(String Path)
        {
            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Create, FileAccess.Write))
                {
                    DataContractSerializer Serializer = new DataContractSerializer(CurrentTableData.GetType());
                    Serializer.WriteObject(FileStream, CurrentTableData);
                }
            }
            catch (Exception Ex)
            {
                return false;
            }
            return true;
        }

        public void AddSnapshotElement(SnapshotElement Element)
        {
            CurrentTableData.AddressTable.Add(new AddressItem(Element.BaseAddress, Element.ElementType));

            TableEventArgs Args = new TableEventArgs();
            Args.AddressTableItemCount = CurrentTableData.AddressTable.Count;
            OnEventUpdateAddressTableItemCount(Args);
        }

        public override void SetFrozenAt(Int32 Index, Boolean Activated)
        {
            CurrentTableData.AddressTable[Index].SetActivationState(Activated);
        }

        public void AddScript(String Script)
        {
            CurrentTableData.ScriptTable.Add(new ScriptItem(Script));
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            // Freeze addresses
            foreach (AddressItem Item in CurrentTableData.AddressTable)
                if (Item.GetActivationState())
                    MemoryEditor.Write(Item.ElementType, Item.Address, Item.Value, false);

            // Request that the display be updated
            TableEventArgs Args = new TableEventArgs();
            OnEventRefreshDisplay(Args);
        }

        public override AddressItem GetAddressItemAt(Int32 Index)
        {
            Boolean ReadSuccess;
            CurrentTableData.AddressTable[Index].Value = MemoryEditor.Read(CurrentTableData.AddressTable[Index].ElementType, CurrentTableData.AddressTable[Index].Address, out ReadSuccess, false);
            return CurrentTableData.AddressTable[Index];
        }

        public override void SetAddressItemAt(Int32 Index, AddressItem AddressItem)
        {
            // Copy over attributes from the new item
            CurrentTableData.AddressTable[Index].Description = AddressItem.Description;
            CurrentTableData.AddressTable[Index].ElementType = AddressItem.ElementType;
            CurrentTableData.AddressTable[Index].Address = AddressItem.Address;
            CurrentTableData.AddressTable[Index].Offsets = AddressItem.Offsets;

            // Force update of value, regardless if frozen or not
            CurrentTableData.AddressTable[Index].ForceUpdateValue(AddressItem.Value);

            if (CurrentTableData.AddressTable[Index].Value != null)
                MemoryEditor.Write(CurrentTableData.AddressTable[Index].ElementType, CurrentTableData.AddressTable[Index].Address, CurrentTableData.AddressTable[Index].Value, false);
        }

        public override ScriptItem GetScriptItemAt(Int32 Index)
        {
            return CurrentTableData.ScriptTable[Index];
        }

        public override void SetScriptItemAt(Int32 Index, ScriptItem ScriptItem)
        {
            CurrentTableData.ScriptTable[Index] = ScriptItem;
        }
    }
}