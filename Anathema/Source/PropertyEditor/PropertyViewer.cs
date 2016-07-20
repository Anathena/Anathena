using Anathema.Source.Engine;
using Anathema.Source.Engine.Processes;
using System;
using System.Collections.Generic;

namespace Anathema.Source.PropertyEditor
{
    class PropertyViewer : IPropertyViewerModel, IProcessObserver
    {
        // Singleton instance of project explorer
        private static Lazy<PropertyViewer> PropertyViewerInstance = new Lazy<PropertyViewer>(() => { return new PropertyViewer(); });

        private EngineCore EngineCore;
        private IEnumerable<Property> PropertySet;

        private PropertyViewer()
        {
            InitializeProcessObserver();

            Begin();
        }

        public override void OnGUIOpen()
        {
            Refresh();
        }

        public static PropertyViewer GetInstance()
        {
            return PropertyViewerInstance.Value;
        }

        ~PropertyViewer()
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

        public override void SetProperties(IEnumerable<Property> PropertySet)
        {
            this.PropertySet = PropertySet;
        }

        private void Refresh()
        {
            PropertyViewerEventArgs PropertyViewerEventArgs = new PropertyViewerEventArgs();
            OnEventRefresh(PropertyViewerEventArgs);
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            Refresh();

            // TODO: offload these to individual properties
            /*
            // Freeze addresses
            foreach (ProjectItem ProjectItem in ProjectItems)
            {
                if (ProjectItem is AddressItem)
                {
                    AddressItem AddressItem = (AddressItem)ProjectItem;
                    if (AddressItem.GetActivationState())
                    {
                        AddressItem.ResolveAddress(EngineCore);

                        if (EngineCore != null && AddressItem.Value != null)
                            EngineCore.Memory.Write(AddressItem.ElementType, AddressItem.EffectiveAddress, AddressItem.Value);
                    }
                }
            }

            for (Int32 Index = VisibleIndexStart; Index < VisibleIndexEnd; Index++)
            {
                if (Index < 0 || Index >= ProjectItems.Count)
                    continue;

                ProjectItem ProjectItem = ProjectItems[Index];

                if (ProjectItem is AddressItem)
                {
                    AddressItem AddressItem = (AddressItem)ProjectItem;
                    Boolean ReadSuccess;
                    AddressItem.ResolveAddress(EngineCore);

                    if (EngineCore != null)
                        AddressItem.Value = EngineCore.Memory.Read(AddressItem.ElementType, AddressItem.EffectiveAddress, out ReadSuccess);
                }
            }

            if (ProjectItems.Count != 0)
                OnEventReadValues(new ProjectExplorerEventArgs());
                */
        }

        protected override void End() { }

    } // End class

} // End namespace