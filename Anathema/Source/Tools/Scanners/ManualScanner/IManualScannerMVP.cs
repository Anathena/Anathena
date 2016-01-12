using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void ManualScannerEventHandler(Object Sender, ManualScannerEventArgs Args);
    class ManualScannerEventArgs : EventArgs
    {
        public ScanConstraintManager ScanConstraints = null;
    }

    interface IManualScannerView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateDisplay(List<String[]> ScanConstraintItems);
        void ScanFinished();
    }

    abstract class IManualScannerModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event ManualScannerEventHandler EventUpdateDisplay;
        protected virtual void OnEventUpdateDisplay(ManualScannerEventArgs E)
        {
            EventUpdateDisplay(this, E);
        }

        public event ManualScannerEventHandler EventScanFinished;
        protected virtual void OnEventScanFinished(ManualScannerEventArgs E)
        {
            EventScanFinished(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract void SetElementType(Type ElementType);
        public abstract Type GetElementType();
        public abstract void AddConstraint(ConstraintsEnum ValueConstraint, dynamic Value);
        public abstract void RemoveConstraints(Int32[] ConstraintIndicies);
        public abstract void ClearConstraints();
    }

    class ManualScannerPresenter : ScannerPresenter
    {
        new IManualScannerView View;
        new IManualScannerModel Model;

        private ConstraintsEnum ValueConstraint;

        public ManualScannerPresenter(IManualScannerView View, IManualScannerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventUpdateDisplay += EventUpdateDisplay;
            Model.EventScanFinished += EventScanFinished;
        }

        #region Method definitions called by the view (downstream)

        public void SetValueConstraints(ConstraintsEnum ValueConstraint)
        {
            this.ValueConstraint = ValueConstraint;
        }

        public void SetElementType(String ElementType)
        {
            Model.SetElementType(Conversions.StringToPrimitiveType(ElementType));
        }

        public void AddConstraint(String ValueString)
        {
            dynamic Value = String.Empty;
            
            switch (ValueConstraint)
            {
                case ConstraintsEnum.Changed:
                case ConstraintsEnum.Unchanged:
                case ConstraintsEnum.Decreased:
                case ConstraintsEnum.Increased:
                    break;
                case ConstraintsEnum.Invalid:
                case ConstraintsEnum.GreaterThan:
                case ConstraintsEnum.LessThan:
                case ConstraintsEnum.Equal:
                case ConstraintsEnum.NotEqual:
                case ConstraintsEnum.IncreasedByX:
                case ConstraintsEnum.DecreasedByX:
                    if (CheckSyntax.CanParseValue(Model.GetElementType(), ValueString))
                        Value = Conversions.ParseValue(Model.GetElementType(), ValueString);
                    else
                        return;
                    break;
            }

            Model.AddConstraint(ValueConstraint, Value);
        }

        public void RemoveConstraints(Int32[] ConstraintIndicies)
        {
            Model.RemoveConstraints(ConstraintIndicies);
        }

        public void ClearConstraints()
        {
            Model.ClearConstraints();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        public void EventUpdateDisplay(Object Sender, ManualScannerEventArgs E)
        {
            List<String[]> ScanConstraintItems = new List<String[]>();

            foreach (ScanConstraint ScanConstraint in E.ScanConstraints)
            {
                String Value = ScanConstraint.Value == null ? null : ScanConstraint.Value.ToString();
                ScanConstraintItems.Add(new String[] { Value, ScanConstraint.Constraint.ToString() });
            }

            View.UpdateDisplay(ScanConstraintItems);
        }

        public void EventScanFinished(Object Sender, ManualScannerEventArgs E)
        {
            View.ScanFinished();
        }

        #endregion
    }
}
