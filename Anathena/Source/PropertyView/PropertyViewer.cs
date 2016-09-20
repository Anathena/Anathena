using Ana.Source.Engine;
using Ana.Source.Engine.Processes;
using System;
using System.Threading;

namespace Ana.Source.PropertyView
{
    class PropertyViewer : IPropertyViewerModel, IProcessObserver
    {
        // Singleton instance of Project Explorer
        private static Lazy<PropertyViewer> PropertyViewerInstance = new Lazy<PropertyViewer>(() => { return new PropertyViewer(); }, LazyThreadSafetyMode.PublicationOnly);

        private EngineCore EngineCore;
        private Object[] TargetObjects;

        private PropertyViewer()
        {
            InitializeProcessObserver();

            Begin();
        }

        public override void OnGUIOpen()
        {
            SetTargetObjects();
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

        public override void SetTargetObjects(params Object[] TargetObjects)
        {
            this.TargetObjects = TargetObjects;

            SetTargetObjects();
        }

        private void SetTargetObjects()
        {
            PropertyViewerEventArgs PropertyViewerEventArgs = new PropertyViewerEventArgs();
            PropertyViewerEventArgs.SelectedObjects = TargetObjects;
            OnEventSetTargetObjects(PropertyViewerEventArgs);
        }

        private void RefreshProperties()
        {
            PropertyViewerEventArgs PropertyViewerEventArgs = new PropertyViewerEventArgs();
            OnEventRefreshProperties(PropertyViewerEventArgs);
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            RefreshProperties();
        }

        protected override void End()
        {
            base.End();
        }

    } // End class

} // End namespace