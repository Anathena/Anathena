using Anathena.Properties;
using Anathena.Source.Utils.MVP;
using Anathena.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Anathena.Source.Scanners.ScanConstraints
{
    delegate void ScanConstraintEditorEventHandler(Object Sender, ScanConstraintEditorEventArgs Args);
    class ScanConstraintEditorEventArgs : EventArgs
    {
        public ScanConstraintManager ScanConstraints = null;
    }

    interface IScanConstraintEditorView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateDisplay(IEnumerable<ListViewItem> ListViewItems, ImageList ImageList);
    }

    interface IScanConstraintEditorModel : IModel
    {
        // Events triggered by the model (upstream)
        event ScanConstraintEditorEventHandler EventUpdateDisplay;

        // Functions invoked by presenter (downstream)
        void SetElementType(Type ElementType);
        Type GetElementType();
        ScanConstraint GetConstraintAt(Int32 Index);
        [Obfuscation(Exclude = true)]
        void AddConstraint(ConstraintsEnum ValueConstraint, dynamic Value);
        [Obfuscation(Exclude = true)]
        void UpdateConstraint(Int32 Index, dynamic Value);
        void RemoveConstraints(IEnumerable<Int32> ConstraintIndicies);
        void ClearConstraints();

        ScanConstraintManager GetScanConstraintManager();
    }

    class ScanConstraintEditorPresenter : Presenter<IScanConstraintEditorView, IScanConstraintEditorModel>
    {
        private new IScanConstraintEditorView view { get; set; }
        private new IScanConstraintEditorModel model { get; set; }

        private ConstraintsEnum ValueConstraint;

        public ScanConstraintEditorPresenter(IScanConstraintEditorView view, IScanConstraintEditorModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model
            model.EventUpdateDisplay += EventUpdateDisplay;

            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public ScanConstraintManager GetScanConstraintManager()
        {
            return model.GetScanConstraintManager();
        }

        public void SetCurrentValueConstraint(ConstraintsEnum ValueConstraint)
        {
            this.ValueConstraint = ValueConstraint;
        }

        public void SetElementType(Type ElementType)
        {
            model.SetElementType(ElementType);
        }

        public void AddConstraint(String ValueString)
        {
            dynamic Value = null;

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
                    if (CheckSyntax.CanParseValue(model.GetElementType(), ValueString))
                        Value = Conversions.ParseDecStringAsValue(model.GetElementType(), ValueString);
                    else
                        return;
                    break;
            }

            model.AddConstraint(ValueConstraint, Value);
        }

        public Boolean TryUpdateConstraint(Int32 Index, String ValueString)
        {
            dynamic Value = null;

            switch (model.GetConstraintAt(Index).Constraint)
            {
                case ConstraintsEnum.Changed:
                case ConstraintsEnum.Unchanged:
                case ConstraintsEnum.Decreased:
                case ConstraintsEnum.Increased:
                case ConstraintsEnum.NotScientificNotation:
                    if (ValueString != null || ValueString != String.Empty)
                        return false;
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
                    if (CheckSyntax.CanParseValue(model.GetElementType(), ValueString))
                        Value = Conversions.ParseDecStringAsValue(model.GetElementType(), ValueString);
                    else
                        return false;
                    break;
            }

            model.UpdateConstraint(Index, Value);
            return true;
        }

        public void RemoveConstraints(params Int32[] ConstraintIndicies)
        {
            model.RemoveConstraints(ConstraintIndicies);
        }

        public void RemoveConstraints(IEnumerable<Int32> ConstraintIndicies)
        {
            model.RemoveConstraints(ConstraintIndicies);
        }

        public void ClearConstraints()
        {
            model.ClearConstraints();
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
                    case ConstraintsEnum.NotScientificNotation: ImageList.Images.Add(Resources.ENotation); break;
                    default: case ConstraintsEnum.Invalid: ImageList.Images.Add(Resources.X); break;
                }

                ScanConstraintItems.Add(new ListViewItem(Value));
                ScanConstraintItems.Last().ImageIndex = ImageIndex++;
            }

            view.UpdateDisplay(ScanConstraintItems, ImageList);
        }

        #endregion

    } // End class

} // End namespace