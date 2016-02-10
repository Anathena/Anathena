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

        private List<AddressItem> AddressItems;

        private Int32 StartReadIndex;
        private Int32 EndReadIndex;

        private AddressTable()
        {
            InitializeProcessObserver();
            AddressItems = new List<AddressItem>();

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
            Args.ItemCount = AddressItems.Count;
            OnEventClearAddressCache(Args);
        }

        public override void SetAddressFrozen(Int32 Index, Boolean Activated)
        {
            if (MemoryEditor == null)
            {
                // Allow disabling even if there is no valid process
                if (!Activated)
                    AddressItems[Index].SetActivationState(Activated);

                return;
            }

            if (Activated)
            {
                Boolean ReadSuccess;
                AddressItems[Index].ResolveAddress(MemoryEditor);
                AddressItems[Index].Value = MemoryEditor.Read(AddressItems[Index].ElementType, unchecked((IntPtr)AddressItems[Index].EffectiveAddress), out ReadSuccess);
            }

            AddressItems[Index].SetActivationState(Activated);
        }

        public void AddTableItem(UInt64 BaseAddress, Type ElementType, String Description, Int32[] Offsets = null, Boolean IsHex = false, String Value = null)
        {
            AddressItems.Add(new AddressItem(BaseAddress, ElementType, Description, Offsets, IsHex, Value));

            AddressTableEventArgs AddressTableEventArgs = new AddressTableEventArgs();
            AddressTableEventArgs.ItemCount = AddressItems.Count;
            OnEventClearAddressCache(AddressTableEventArgs);
        }

        public override AddressItem GetAddressItemAt(Int32 Index)
        {
            return AddressItems[Index];
        }

        public List<AddressItem> GetAddressItems()
        {
            return AddressItems;
        }

        public void SetAddressItems(List<AddressItem> AddressItems)
        {
            this.AddressItems = AddressItems;

            AddressTableEventArgs AddressTableEventArgs = new AddressTableEventArgs();
            AddressTableEventArgs.ItemCount = AddressItems.Count;
            OnEventClearAddressCache(AddressTableEventArgs);
        }

        public override void SetAddressItemAt(Int32 Index, AddressItem AddressItem)
        {
            // Copy over attributes from the new item (such as to keep this item's color attributes)
            AddressItems[Index].Description = AddressItem.Description;
            AddressItems[Index].ElementType = AddressItem.ElementType;
            AddressItems[Index].BaseAddress = AddressItem.BaseAddress;
            AddressItems[Index].Offsets = AddressItem.Offsets;
            AddressItems[Index].IsHex = AddressItem.IsHex;

            // Force update of value, regardless if frozen or not
            AddressItems[Index].ForceUpdateValue(AddressItem.Value);

            // Write change to memory
            if (AddressItem.Value != null)
            {
                AddressItems[Index].ResolveAddress(MemoryEditor);
                if (MemoryEditor != null)
                    MemoryEditor.Write(AddressItems[Index].ElementType, unchecked((IntPtr)AddressItems[Index].EffectiveAddress), AddressItems[Index].Value);
            }

            // Clear this entry in the cache since it has been updated
            ClearAddressItemFromCache(AddressItems[Index]);
        }

        public override void ReorderScript(Int32 SourceIndex, Int32 DestinationIndex)
        {
            // Bounds checking
            if (SourceIndex < 0 || SourceIndex > AddressItems.Count)
                return;

            // If an item is being removed before the destination, the destination must be shifted
            if (DestinationIndex > SourceIndex)
                DestinationIndex--;

            // Bounds checking
            if (DestinationIndex < 0 || DestinationIndex > AddressItems.Count)
                return;

            AddressItem Item = AddressItems[SourceIndex];
            AddressItems.RemoveAt(SourceIndex);
            AddressItems.Insert(DestinationIndex, Item);

            AddressTableEventArgs AddressTableEventArgs = new AddressTableEventArgs();
            AddressTableEventArgs.ItemCount = AddressItems.Count;
            OnEventClearAddressCache(AddressTableEventArgs);
        }

        private void ClearAddressItemFromCache(AddressItem AddressItem)
        {
            AddressTableEventArgs AddressTableEventArgs = new AddressTableEventArgs();
            AddressTableEventArgs.ClearCacheIndex = AddressItems.IndexOf(AddressItem);
            AddressTableEventArgs.ItemCount = AddressItems.Count;
            OnEventClearAddressCacheItem(AddressTableEventArgs);
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            // Freeze addresses
            foreach (AddressItem Item in AddressItems)
            {
                if (Item.GetActivationState())
                {
                    Item.ResolveAddress(MemoryEditor);

                    if (MemoryEditor != null && Item.Value != null)
                        MemoryEditor.Write(Item.ElementType, unchecked((IntPtr)Item.EffectiveAddress), Item.Value);
                }
            }

            for (Int32 Index = StartReadIndex; Index < EndReadIndex; Index++)
            {
                if (Index < 0 || Index >= AddressItems.Count)
                    continue;

                Boolean ReadSuccess;
                AddressItems[Index].ResolveAddress(MemoryEditor);

                if (MemoryEditor != null)
                    AddressItems[Index].Value = MemoryEditor.Read(AddressItems[Index].ElementType, unchecked((IntPtr)AddressItems[Index].EffectiveAddress), out ReadSuccess);
            }

            if (AddressItems.Count != 0)
                OnEventReadValues(new AddressTableEventArgs());
        }

    } // End class

} // End namespace