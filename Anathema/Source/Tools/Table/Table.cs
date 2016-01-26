using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

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

        private Int32 StartReadIndex;
        private Int32 EndReadIndex;

        private Table()
        {
            InitializeProcessObserver();
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

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public override void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            this.StartReadIndex = StartReadIndex;
            this.EndReadIndex = EndReadIndex;
        }

        private void RefreshDisplay()
        {
            // Request that all data be updated
            TableEventArgs Args = new TableEventArgs();
            Args.ItemCount = CurrentTableData.AddressTable.Count;
            OnEventClearAddressCache(Args);

            Args.ItemCount = CurrentTableData.ScriptTable.Count;
            OnEventClearScriptCache(Args);

            Args.ItemCount = CurrentTableData.FiniteStateMachineTable.Count;
            OnEventClearFSMCache(Args);
        }

        public override Boolean SaveTable(String Path)
        {
            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Create, FileAccess.Write))
                {
                    DataContractSerializer Serializer = new DataContractSerializer(typeof(TableData));
                    Serializer.WriteObject(FileStream, CurrentTableData);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public override Boolean LoadTable(String Path)
        {
            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    DataContractSerializer Serializer = new DataContractSerializer(typeof(TableData));
                    CurrentTableData = (TableData)Serializer.ReadObject(FileStream);
                }
            }
            catch
            {
                return false;
            }

            RefreshDisplay();
            return true;
        }

        public override void SetAddressFrozen(Int32 Index, Boolean Activated)
        {
            if (Activated)
            {
                Boolean ReadSuccess;
                CurrentTableData.AddressTable[Index].Value = MemoryEditor.Read(CurrentTableData.AddressTable[Index].ElementType, (IntPtr)CurrentTableData.AddressTable[Index].Address, out ReadSuccess, false);
            }

            CurrentTableData.AddressTable[Index].SetActivationState(Activated);
        }

        public void AddTableItem(UInt64 BaseAddress, Type ElementType)
        {
            CurrentTableData.AddressTable.Add(new AddressItem(BaseAddress, ElementType));

            TableEventArgs TableEventArgs = new TableEventArgs();
            TableEventArgs.ItemCount = CurrentTableData.ScriptTable.Count;
            OnEventClearAddressCache(TableEventArgs);
        }

        public override AddressItem GetAddressItemAt(Int32 Index)
        {
            return CurrentTableData.AddressTable[Index];
        }

        public override void SetAddressItemAt(Int32 Index, AddressItem AddressItem)
        {
            // Copy over attributes from the new item (such as to keep this item's color attributes)
            CurrentTableData.AddressTable[Index].Description = AddressItem.Description;
            CurrentTableData.AddressTable[Index].ElementType = AddressItem.ElementType;
            CurrentTableData.AddressTable[Index].Address = AddressItem.Address;
            CurrentTableData.AddressTable[Index].Offsets = AddressItem.Offsets;
            CurrentTableData.AddressTable[Index].IsHex = AddressItem.IsHex;

            // Force update of value, regardless if frozen or not
            CurrentTableData.AddressTable[Index].ForceUpdateValue(AddressItem.Value);

            // Write change to memory
            if (AddressItem.Value != null)
                MemoryEditor.Write(CurrentTableData.AddressTable[Index].ElementType, (IntPtr)CurrentTableData.AddressTable[Index].Address, CurrentTableData.AddressTable[Index].Value, false);

            // Clear this entry in the cache since it has been updated
            TableEventArgs TableEventArgs = new TableEventArgs();
            TableEventArgs.ClearCacheIndex = Index;
            OnEventClearAddressCacheItem(TableEventArgs);
        }

        public override void OpenScript(Int32 Index)
        {
            if (Index >= CurrentTableData.ScriptTable.Count)
                return;

            Main.GetInstance().OpenScriptEditor();
            ScriptEditor.GetInstance().OpenScript(CurrentTableData.ScriptTable[Index]);
        }

        public void SaveScript(ScriptItem ScriptItem)
        {
            if (!CurrentTableData.ScriptTable.Contains(ScriptItem))
            {
                // Adding a new script
                CurrentTableData.ScriptTable.Add(ScriptItem);

                TableEventArgs TableEventArgs = new TableEventArgs();
                TableEventArgs.ItemCount = CurrentTableData.ScriptTable.Count;
                OnEventClearScriptCache(TableEventArgs);
            }
            else
            {
                // Updating an existing script, clear it from the cache
                TableEventArgs TableEventArgs = new TableEventArgs();
                TableEventArgs.ClearCacheIndex = CurrentTableData.ScriptTable.IndexOf(ScriptItem);
                TableEventArgs.ItemCount = CurrentTableData.ScriptTable.Count;
                OnEventClearScriptCacheItem(TableEventArgs);
            }
        }

        public override ScriptItem GetScriptItemAt(Int32 Index)
        {
            return CurrentTableData.ScriptTable[Index];
        }

        public override void SetScriptActivation(Int32 Index, Boolean Activated)
        {
            // Try to update the activation state
            CurrentTableData.ScriptTable[Index].SetActivationState(Activated);
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            // Freeze addresses
            foreach (AddressItem Item in CurrentTableData.AddressTable)
            {
                if (Item.GetActivationState())
                    MemoryEditor.Write(Item.ElementType, (IntPtr)Item.Address, Item.Value, false);
            }

            for (Int32 Index = StartReadIndex; Index < EndReadIndex; Index++)
            {
                if (Index < 0 || Index >= CurrentTableData.AddressTable.Count)
                    continue;

                Boolean ReadSuccess;
                CurrentTableData.AddressTable[Index].Value = MemoryEditor.Read(CurrentTableData.AddressTable[Index].ElementType, (IntPtr)CurrentTableData.AddressTable[Index].Address, out ReadSuccess, false);
            }

            OnEventReadValues(new TableEventArgs());
        }

    } // End class

} // End namespace