using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void FilterManualScanEventHandler(Object Sender, FilterManualScanEventArgs Args);
    class FilterManualScanEventArgs : EventArgs
    {
        public ScanConstraints ScanConstraints = null;
    }

    interface IFilterManualScanView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateDisplay(List<String[]> ScanConstraintItems);
    }

    abstract class IFilterManualScanModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event FilterManualScanEventHandler EventUpdateDisplay;
        protected virtual void OnEventUpdateDisplay(FilterManualScanEventArgs E)
        {
            EventUpdateDisplay(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract void AddConstraint(ValueConstraintsEnum ValueConstraint, Type ElementType, dynamic Value);
    }

    class FilterManualScanPresenter : ScannerPresenter
    {
        new IFilterManualScanView View;
        new IFilterManualScanModel Model;

        private ValueConstraintsEnum ValueConstraint;

        public FilterManualScanPresenter(IFilterManualScanView View, IFilterManualScanModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventUpdateDisplay += EventUpdateDisplay;
        }

        #region Method definitions called by the view (downstream)
        
        public void AddCurrentConstraint(String ElementTypeString, String ValueString)
        {
            Type ElementType = Conversions.StringToPrimitiveType(ElementTypeString);
            dynamic Value = Conversions.ParseValue(ElementType, ValueString);

            Model.AddConstraint(ValueConstraint, ElementType, Value);
        }

        public void SetValueConstraints(ValueConstraintsEnum ValueConstraint)
        {
            this.ValueConstraint = ValueConstraint;
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        public void EventUpdateDisplay(Object Sender, FilterManualScanEventArgs E)
        {
            List<String[]> ScanConstraintItems = new List<String[]>();

            foreach (ScanConstraintItem ScanConstraint in E.ScanConstraints)
            {
                String Value = ScanConstraint.Value == null ? null : ScanConstraint.Value.ToString();
                ScanConstraintItems.Add(new String[] { ScanConstraint.ElementType.Name, Value, ScanConstraint.ValueConstraints.ToString() });
            }

            View.UpdateDisplay(ScanConstraintItems);
        }

        #endregion
    }
}
