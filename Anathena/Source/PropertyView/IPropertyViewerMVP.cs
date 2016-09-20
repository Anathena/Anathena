using Ana.Source.UserSettings;
using Ana.Source.Utils;
using Ana.Source.Utils.MVP;
using System;
using System.Collections.Generic;

namespace Ana.Source.PropertyView
{
    delegate void PropertyViewerEventHandler(Object Sender, PropertyViewerEventArgs Args);
    class PropertyViewerEventArgs : EventArgs
    {
        public IEnumerable<Object> SelectedObjects;
    }

    interface IPropertyViewerView : IView
    {
        // Methods invoked by the presenter (upstream)
        void SetTargetObjects(IEnumerable<Object> SelectedObjects);
        void RefreshProperties();
    }

    abstract class IPropertyViewerModel : RepeatedTask, IModel
    {
        public virtual void OnGUIOpen() { }

        // Events triggered by the model (upstream)
        public event PropertyViewerEventHandler EventSetTargetObjects;
        protected virtual void OnEventSetTargetObjects(PropertyViewerEventArgs E)
        {
            EventSetTargetObjects?.Invoke(this, E);
        }

        public event PropertyViewerEventHandler EventRefreshProperties;
        protected virtual void OnEventRefreshProperties(PropertyViewerEventArgs E)
        {
            EventRefreshProperties?.Invoke(this, E);
        }

        public override void Begin()
        {
            // Temporary workaround until I feel like adding multiple tasks to the RepeatTask class
            UpdateInterval = Math.Min(Settings.GetInstance().GetTableReadInterval(), Settings.GetInstance().GetFreezeInterval());
            base.Begin();
        }

        protected override void Update()
        {
            UpdateInterval = Math.Min(Settings.GetInstance().GetTableReadInterval(), Settings.GetInstance().GetFreezeInterval());
        }

        // Functions invoked by presenter (downstream)
        public abstract void SetTargetObjects(params Object[] TargetObjects);
    }

    class PropertyViewerPresenter : Presenter<IPropertyViewerView, IPropertyViewerModel>
    {
        private new IPropertyViewerView view { get; set; }
        private new IPropertyViewerModel model { get; set; }

        public PropertyViewerPresenter(IPropertyViewerView view, IPropertyViewerModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model
            model.EventSetTargetObjects += EventSetTargetObjects;
            model.EventRefreshProperties += EventRefreshProperties;

            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventSetTargetObjects(Object Sender, PropertyViewerEventArgs E)
        {
            view.SetTargetObjects(E.SelectedObjects);
        }

        private void EventRefreshProperties(Object Sender, PropertyViewerEventArgs E)
        {
            // View.RefreshProperties();
        }

        #endregion

    } // End class

} // End namespace