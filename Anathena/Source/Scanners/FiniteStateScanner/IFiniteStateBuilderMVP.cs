using Anathena.Source.Scanners.ScanConstraints;
using Anathena.Source.Utils.MVP;
using Anathena.Source.Utils.Validation;
using System;
using System.Drawing;
using System.Reflection;

namespace Anathena.Source.Scanners.FiniteStateScanner
{
    delegate void FiniteStateBuilderEventHandler(Object Sender, FiniteStateBuilderEventArgs Args);
    class FiniteStateBuilderEventArgs : EventArgs
    {

    }

    interface IFiniteStateBuilderView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateDisplay();
    }

    interface IFiniteStateBuilderModel : IModel
    {
        // Events triggered by the model (upstream)
        event FiniteStateBuilderEventHandler EventUpdateDisplay;

        // Functions invoked by presenter (downstream)
        void BeginAction(Point Location);
        void UpdateAction(Point Location);
        void FinishAction(Point Location);

        FiniteStateMachine GetFiniteStateMachine();
        FiniteState GetMousedOverState();
        Point[] GetSelectionLine();
        Type GetElementType();

        void SetCurrentValueConstraint(ConstraintsEnum CurrentValueConstraint);
        [Obfuscation(Exclude = true)]
        void SetCurrentValue(dynamic CurrentValue);
        void SetFiniteStateMachine(FiniteStateMachine FiniteStateMachine);
        void SetElementType(Type ElementType);
        void SetStateRadius(Int32 Radius);
        void SetStateEdgeSize(Int32 EdgeSize);
    }

    class FiniteStateBuilderPresenter : Presenter<IFiniteStateBuilderView, IFiniteStateBuilderModel>
    {
        private new IFiniteStateBuilderView view { get; set; }
        private new IFiniteStateBuilderModel model { get; set; }

        public FiniteStateBuilderPresenter(IFiniteStateBuilderView view, IFiniteStateBuilderModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model
            model.EventUpdateDisplay += EventUpdateDisplay;

            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void BeginAction(Point Location)
        {
            model.BeginAction(Location);
        }

        public void UpdateAction(Point Location)
        {
            model.UpdateAction(Location);
        }

        public void FinishAction(Point Location)
        {
            model.FinishAction(Location);
        }

        public FiniteStateMachine GetFiniteStateMachine()
        {
            return model.GetFiniteStateMachine();
        }

        public FiniteState GetMousedOverState()
        {
            return model.GetMousedOverState();
        }

        public Point[] GetSelectionLine()
        {
            return model.GetSelectionLine();
        }

        public void SetStateRadius(Int32 StateRadius)
        {
            model.SetStateRadius(StateRadius);
        }

        public void SetStateEdgeSize(Int32 StateEdgeSize)
        {
            model.SetStateEdgeSize(StateEdgeSize);
        }

        public void SetCurrentValueConstraint(ConstraintsEnum CurrentValueConstraint)
        {
            model.SetCurrentValueConstraint(CurrentValueConstraint);
        }

        public Boolean TrySetValue(String ValueText)
        {
            if (CheckSyntax.CanParseValue(model.GetElementType(), ValueText))
            {
                model.SetCurrentValue(Conversions.ParseDecStringAsValue(model.GetElementType(), ValueText));
                return true;
            }
            return false;
        }

        public void SetElementType(String ElementType)
        {
            model.SetElementType(Conversions.StringToPrimitiveType(ElementType));
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        public void EventUpdateDisplay(Object Sender, FiniteStateBuilderEventArgs E)
        {
            view.UpdateDisplay();
        }

        #endregion

    } // End class

} // End namespace