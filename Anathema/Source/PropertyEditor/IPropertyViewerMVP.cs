using Anathema.Source.Utils;
using Anathema.Source.Utils.MVP;
using Anathema.Source.Utils.Setting;
using System;
using System.Collections.Generic;

namespace Anathema.Source.PropertyEditor
{
    delegate void PropertyViewerEventHandler(Object Sender, PropertyViewerEventArgs Args);
    class PropertyViewerEventArgs : EventArgs
    {
        public IEnumerable<Object> SelectedObjects;
    }

    interface IPropertyViewerView : IView
    {
        // Methods invoked by the presenter (upstream)
        void RefreshStructure(IEnumerable<Object> SelectedObjects);
    }

    abstract class IPropertyViewerModel : RepeatedTask, IModel
    {
        public virtual void OnGUIOpen() { }

        // Events triggered by the model (upstream)
        public event PropertyViewerEventHandler EventRefresh;
        protected virtual void OnEventRefresh(PropertyViewerEventArgs E)
        {
            EventRefresh?.Invoke(this, E);
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
        private new IPropertyViewerView View { get; set; }
        private new IPropertyViewerModel Model { get; set; }

        public PropertyViewerPresenter(IPropertyViewerView View, IPropertyViewerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventRefresh += EventRefresh;

            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventRefresh(Object Sender, PropertyViewerEventArgs E)
        {
            View.RefreshStructure(E.SelectedObjects);
        }

        #endregion

    } // End class

} // End namespace