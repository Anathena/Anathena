using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Anathema
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class AddressTable : IAddressTableModel, IProcessObserver
    {
        public enum TableColumnEnum
        {
            Frozen,
            Description,
            ValueType,
            Address,
            Value
        }

        private static AddressTable AddressTableInstance;
        private MemoryEditor MemoryEditor;

        private TableData CurrentTableData;

        private Int32 StartReadIndex;
        private Int32 EndReadIndex;

        private AddressTable()
        {
            InitializeProcessObserver();
            CurrentTableData = new TableData();

            Begin();
        }

        public static AddressTable GetInstance()
        {
            if (AddressTableInstance == null)
                AddressTableInstance = new AddressTable();
            return AddressTableInstance;
        }

        ~AddressTable()
        {
            End();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemoryEditor MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public override void ForceRefresh()
        {
            RefreshDisplay();
        }

        public override void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            this.StartReadIndex = StartReadIndex;
            this.EndReadIndex = EndReadIndex;
        }

        private void RefreshDisplay()
        {
            // Request that all data be updated
            AddressTableEventArgs Args = new AddressTableEventArgs();
            Args.ItemCount = CurrentTableData.AddressTable.Count;
            OnEventClearAddressCache(Args);
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
                CurrentTableData.AddressTable[Index].ResolveAddress(MemoryEditor);
                CurrentTableData.AddressTable[Index].Value = MemoryEditor.Read(CurrentTableData.AddressTable[Index].ElementType, unchecked((IntPtr)CurrentTableData.AddressTable[Index].EffectiveAddress), out ReadSuccess);
            }

            CurrentTableData.AddressTable[Index].SetActivationState(Activated);
        }

        public void AddTableItem(UInt64 BaseAddress, Type ElementType, String Description, Int32[] Offsets = null, Boolean IsHex = false, String Value = null)
        {
            CurrentTableData.AddressTable.Add(new AddressItem(BaseAddress, ElementType, Description, Offsets, IsHex, Value));

            AddressTableEventArgs AddressTableEventArgs = new AddressTableEventArgs();
            AddressTableEventArgs.ItemCount = CurrentTableData.AddressTable.Count;
            OnEventClearAddressCache(AddressTableEventArgs);
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
            CurrentTableData.AddressTable[Index].BaseAddress = AddressItem.BaseAddress;
            CurrentTableData.AddressTable[Index].Offsets = AddressItem.Offsets;
            CurrentTableData.AddressTable[Index].IsHex = AddressItem.IsHex;

            // Force update of value, regardless if frozen or not
            CurrentTableData.AddressTable[Index].ForceUpdateValue(AddressItem.Value);

            // Write change to memory
            if (AddressItem.Value != null)
            {
                CurrentTableData.AddressTable[Index].ResolveAddress(MemoryEditor);
                MemoryEditor.Write(CurrentTableData.AddressTable[Index].ElementType, unchecked((IntPtr)CurrentTableData.AddressTable[Index].EffectiveAddress), CurrentTableData.AddressTable[Index].Value);
            }
            // Clear this entry in the cache since it has been updated
            ClearAddressItemFromCache(CurrentTableData.AddressTable[Index]);
        }

        private void ClearAddressItemFromCache(AddressItem AddressItem)
        {
            AddressTableEventArgs AddressTableEventArgs = new AddressTableEventArgs();
            AddressTableEventArgs.ClearCacheIndex = CurrentTableData.AddressTable.IndexOf(AddressItem);
            AddressTableEventArgs.ItemCount = CurrentTableData.AddressTable.Count;
            OnEventClearAddressCacheItem(AddressTableEventArgs);
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
                {
                    Item.ResolveAddress(MemoryEditor);
                    MemoryEditor.Write(Item.ElementType, unchecked((IntPtr)Item.EffectiveAddress), Item.Value);
                }
            }

            for (Int32 Index = StartReadIndex; Index < EndReadIndex; Index++)
            {
                if (Index < 0 || Index >= CurrentTableData.AddressTable.Count)
                    continue;

                Boolean ReadSuccess;
                CurrentTableData.AddressTable[Index].ResolveAddress(MemoryEditor);
                CurrentTableData.AddressTable[Index].Value = MemoryEditor.Read(CurrentTableData.AddressTable[Index].ElementType, unchecked((IntPtr)CurrentTableData.AddressTable[Index].EffectiveAddress), out ReadSuccess);
            }

            if (CurrentTableData.AddressTable.Count != 0)
                OnEventReadValues(new AddressTableEventArgs());
        }
        
    } // End class

} // End namespace