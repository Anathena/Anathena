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
        private new IScanConstraintEditorView View { get; set; }
        private new IScanConstraintEditorModel Model { get; set; }

        private ConstraintsEnum ValueConstraint;

        public ScanConstraintEditorPresenter(IScanConstraintEditorView View, IScanConstraintEditorModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventUpdateDisplay += EventUpdateDisplay;

            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public ScanConstraintManager GetScanConstraintManager()
        {
            return Model.GetScanConstraintManager();
        }

        public void SetCurrentValueConstraint(ConstraintsEnum ValueConstraint)
        {
            this.ValueConstraint = ValueConstraint;
        }

        public void SetElementType(Type ElementType)
        {
            Model.SetElementType(ElementType);
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
                    if (CheckSyntax.CanParseValue(Model.GetElementType(), ValueString))
                        Value = Conversions.ParseDecStringAsValue(Model.GetElementType(), ValueString);
                    else
                        return;
                    break;
            }

            Model.AddConstraint(ValueConstraint, Value);
        }

        public Boolean TryUpdateConstraint(Int32 Index, String ValueString)
        {
            dynamic Value = null;

            switch (Model.GetConstraintAt(Index).Constraint)
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
                    if (CheckSyntax.CanParseValue(Model.GetElementType(), ValueString))
                        Value = Conversions.ParseDecStringAsValue(Model.GetElementType(), ValueString);
                    else
                        return false;
                    break;
            }

            Model.UpdateConstraint(Index, Value);
            return true;
        }

        public void RemoveConstraints(params Int32[] ConstraintIndicies)
        {
            Model.RemoveConstraints(ConstraintIndicies);
        }

        public void RemoveConstraints(IEnumerable<Int32> ConstraintIndicies)
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
                    case ConstraintsEnum.NotScientificNotation: ImageList.Images.Add(Resources.ENotation); break;
                    default: case ConstraintsEnum.Invalid: ImageList.Images.Add(Resources.X); break;
                }

                ScanConstraintItems.Add(new ListViewItem(Value));
                ScanConstraintItems.Last().ImageIndex = ImageIndex++;
            }

            View.UpdateDisplay(ScanConstraintItems, ImageList);
        }

        #endregion

    } // End class

} // End namespace