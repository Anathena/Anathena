using Anathema.Source.Engine;
using Anathema.Source.Engine.Processes;
using Anathema.Source.Project.Deprecating;
using Anathema.Source.Project.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anathema.Source.Project
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class ScratchPad : IScratchPadModel, IProcessObserver
    {
        public enum TableColumnEnum
        {
            Frozen,
            Description,
            ValueType,
            Address,
            Value
        }

        // Singleton instance of address table
        private static Lazy<ScratchPad> ScratchPadInstance = new Lazy<ScratchPad>(() => { return new ScratchPad(); });

        private EngineCore EngineCore;

        private List<ProjectItem> ProjectItems;

        private Int32 VisibleIndexStart;
        private Int32 VisibleIndexEnd;

        private ScratchPad()
        {
            InitializeProcessObserver();
            ProjectItems = new List<ProjectItem>();

            Begin();
        }

        public override void OnGUIOpen()
        {
            UpdateScratchPadItemCount();
        }

        public static ScratchPad GetInstance()
        {
            return ScratchPadInstance.Value;
        }

        ~ScratchPad()
        {
            TriggerEnd();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
        }

        public override void UpdateReadBounds(Int32 VisibleIndexStart, Int32 VisibleIndexEnd)
        {
            this.VisibleIndexStart = VisibleIndexStart;
            this.VisibleIndexEnd = VisibleIndexEnd;
        }

        public override void SetItemActivation(Int32 Index, Boolean Activated)
        {
            if (EngineCore == null)
            {
                // Allow disabling even if there is no valid process
                if (!Activated)
                    ProjectItems[Index].SetActivationState(Activated);

                return;
            }

            if (Activated)
            {
                ProjectItem ProjectItem = ProjectItems[Index];

                if (ProjectItem.GetType() == typeof(AddressItem))
                {
                    AddressItem AddressItem = (AddressItem)ProjectItem;
                    Boolean ReadSuccess;
                    AddressItem.ResolveAddress(EngineCore);
                    AddressItem.Value = EngineCore.Memory.Read(AddressItem.ElementType, AddressItem.EffectiveAddress, out ReadSuccess);
                }
            }
            ProjectItems[Index].SetActivationState(Activated);
        }

        public override void AddProjectItem(ProjectItem ProjectItem)
        {
            ProjectItems.Add(ProjectItem);
            UpdateScratchPadItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public override void AddAddressItem(String BaseAddress, Type ElementType, String Description, IEnumerable<Int32> Offsets = null, Boolean IsHex = false, String Value = null)
        {
            ProjectItems.Add(new AddressItem(BaseAddress, ElementType, Description, Offsets, IsHex, Value));

            UpdateScratchPadItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public override void AddFolderItem(String FolderName)
        {
            ProjectItems.Add(new FolderItem(FolderName));
            UpdateScratchPadItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public override void DeleteTableItems(IEnumerable<Int32> Indicies)
        {
            foreach (Int32 Index in Indicies.OrderByDescending(X => X))
                ProjectItems.RemoveAt(Index);

            UpdateScratchPadItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public override ProjectItem GetProjectItemAt(Int32 Index)
        {
            return ProjectItems[Index];
        }

        public override Int32 GetItemCount()
        {
            return ProjectItems.Count;
        }

        public List<ProjectItem> GetProjectItems()
        {
            return ProjectItems;
        }

        public void SetProjectItems(List<ProjectItem> AddressItems)
        {
            this.ProjectItems = AddressItems;
            UpdateScratchPadItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public override void SetAddressItemAt(Int32 Index, AddressItem AddressItem)
        {
            ProjectItems[Index] = AddressItem;

            // Force update of value, regardless if frozen or not
            AddressItem.ForceUpdateValue(AddressItem.Value);

            // Write change to memory
            if (AddressItem.Value != null)
            {
                AddressItem.ResolveAddress(EngineCore);
                if (EngineCore != null)
                    EngineCore.Memory.Write(AddressItem.ElementType, AddressItem.EffectiveAddress, AddressItem.Value);
            }

            UpdateScratchPadItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public override void ReorderItem(Int32 SourceIndex, Int32 DestinationIndex)
        {
            // Bounds checking
            if (SourceIndex < 0 || SourceIndex > ProjectItems.Count)
                return;

            // If an item is being removed before the destination, the destination must be shifted
            if (DestinationIndex > SourceIndex)
                DestinationIndex--;

            // Bounds checking
            if (DestinationIndex < 0 || DestinationIndex > ProjectItems.Count)
                return;

            ProjectItem Item = ProjectItems[SourceIndex];
            ProjectItems.RemoveAt(SourceIndex);
            ProjectItems.Insert(DestinationIndex, Item);
            UpdateScratchPadItemCount();

            TableManager.GetInstance().TableChanged();
        }

        private void UpdateScratchPadItemCount()
        {
            ScratchPadEventArgs ScratchPadEventArgs = new ScratchPadEventArgs();
            ScratchPadEventArgs.ItemCount = ProjectItems.Count;
            OnEventRefreshStructure(ScratchPadEventArgs);
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            // Freeze addresses
            foreach (AddressItem Item in ProjectItems)
            {
                if (Item.GetActivationState())
                {
                    Item.ResolveAddress(EngineCore);

                    if (EngineCore != null && Item.Value != null)
                        EngineCore.Memory.Write(Item.ElementType, Item.EffectiveAddress, Item.Value);
                }
            }

            for (Int32 Index = VisibleIndexStart; Index < VisibleIndexEnd; Index++)
            {
                if (Index < 0 || Index >= ProjectItems.Count)
                    continue;

                ProjectItem ProjectItem = ProjectItems[Index];

                if (ProjectItem.GetType() == typeof(AddressItem))
                {
                    AddressItem AddressItem = (AddressItem)ProjectItem;
                    Boolean ReadSuccess;
                    AddressItem.ResolveAddress(EngineCore);

                    if (EngineCore != null)
                        AddressItem.Value = EngineCore.Memory.Read(AddressItem.ElementType, AddressItem.EffectiveAddress, out ReadSuccess);
                }
            }

            if (ProjectItems.Count != 0)
                OnEventReadValues(new ScratchPadEventArgs());
        }

        protected override void End() { }

    } // End class

} // End namespace