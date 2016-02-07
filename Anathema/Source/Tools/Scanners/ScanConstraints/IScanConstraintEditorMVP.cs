using Anathema.Properties;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    delegate void ScanConstraintEditorEventHandler(Object Sender, ScanConstraintEditorEventArgs Args);
    class ScanConstraintEditorEventArgs : EventArgs
    {
        public ScanConstraintManager ScanConstraints = null;
    }

    interface IScanConstraintEditorView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateDisplay(ListViewItem[] ListViewItems, ImageList ImageList);
    }

    interface IScanConstraintEditorModel : IModel
    {
        // Events triggered by the model (upstream)
        event ScanConstraintEditorEventHandler EventUpdateDisplay;

        // Functions invoked by presenter (downstream)
        void SetElementType(Type ElementType);
        Type GetElementType();
        void AddConstraint(ConstraintsEnum ValueConstraint, dynamic Value);
        void RemoveConstraints(Int32[] ConstraintIndicies);
        void ClearConstraints();
    }

    class ScanConstraintEditorPresenter : Presenter<IScanConstraintEditorView, IScanConstraintEditorModel>
    {
        protected new IScanConstraintEditorView View;
        protected new IScanConstraintEditorModel Model;

        private ConstraintsEnum ValueConstraint;
        
        public ScanConstraintEditorPresenter(IScanConstraintEditorView View, IScanConstraintEditorModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventUpdateDisplay += EventUpdateDisplay;
        }

        #region Method definitions called by the view (downstream)

        public void SetCurrentValueConstraint(ConstraintsEnum ValueConstraint)
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
                case ConstraintsEnum.NotScientificNotation:
                    break;
                case ConstraintsEnum.Invalid:
                case ConstraintsEnum.GreaterThan:
                case ConstraintsEnum.GreaterThanOrEqual:
                case ConstraintsEnum.LessThan:
                case ConstraintsEnum.LessThanOrEqual:
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

        public void EventUpdateDisplay(Object Sender, ScanConstraintEditorEventArgs E)
        {
            List<ListViewItem> ScanConstraintItems = new List<ListViewItem>();
            ImageList ImageList = new ImageList();
            Int32 ImageIndex = 0;

            foreach (ScanConstraint ScanConstraint in E.ScanConstraints)
            {
                String Value = ScanConstraint.Value == null ? String.Empty : ScanConstraint.Value.ToString();

                switch (ScanConstraint.Constraint)
                {
                    case ConstraintsEnum.Changed: ImageList.Images.Add(Resources.Changed); break;
                    case ConstraintsEnum.Unchanged: ImageList.Images.Add(Resources.Unchanged); break;
                    case ConstraintsEnum.Decreased: ImageList.Images.Add(Resources.Decreased); break;
                    case ConstraintsEnum.Increased: ImageList.Images.Add(Resources.Increased); break;
                    case ConstraintsEnum.GreaterThan: ImageList.Images.Add(Resources.GreaterThan); break;
                    case ConstraintsEnum.GreaterThanOrEqual: ImageList.Images.Add(Resources.GreaterThanOrEqual); break;
                    case ConstraintsEnum.LessThan: ImageList.Images.Add(Resources.LessThan); break;
                    case ConstraintsEnum.LessThanOrEqual: ImageList.Images.Add(Resources.LessThanOrEqual); break;
                    case ConstraintsEnum.Equal: ImageList.Images.Add(Resources.Equal); break;
                    case ConstraintsEnum.NotEqual: ImageList.Images.Add(Resources.NotEqual); break;
                    case ConstraintsEnum.IncreasedByX: ImageList.Images.Add(Resources.PlusX); break;
                    case ConstraintsEnum.DecreasedByX: ImageList.Images.Add(Resources.MinusX); break;
                    case ConstraintsEnum.NotScientificNotation: ImageList.Images.Add(Resources.Intersection); break;
                    default: case ConstraintsEnum.Invalid: ImageList.Images.Add(Resources.AnathemaIcon); break;
                }

                ScanConstraintItems.Add(new ListViewItem(Value));
                ScanConstraintItems.Last().ImageIndex = ImageIndex++;
            }

            View.UpdateDisplay(ScanConstraintItems.ToArray(), ImageList);
        }

        #endregion

    } // End class

} // End namespace