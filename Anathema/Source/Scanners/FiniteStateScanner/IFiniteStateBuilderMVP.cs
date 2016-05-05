using Anathema.Scanners.ScanConstraints;
using Anathema.Utils.MVP;
using Anathema.Utils.Validation;
using System;
using System.Drawing;
using System.Reflection;

namespace Anathema.Scanners.FiniteStateScanner
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
        new IFiniteStateBuilderView View;
        new IFiniteStateBuilderModel Model;

        public FiniteStateBuilderPresenter(IFiniteStateBuilderView View, IFiniteStateBuilderModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventUpdateDisplay += EventUpdateDisplay;
        }

        #region Method definitions called by the view (downstream)

        public void BeginAction(Point Location)
        {
            Model.BeginAction(Location);
        }

        public void UpdateAction(Point Location)
        {
            Model.UpdateAction(Location);
        }

        public void FinishAction(Point Location)
        {
            Model.FinishAction(Location);
        }

        public FiniteStateMachine GetFiniteStateMachine()
        {
            return Model.GetFiniteStateMachine();
        }

        public FiniteState GetMousedOverState()
        {
            return Model.GetMousedOverState();
        }

        public Point[] GetSelectionLine()
        {
            return Model.GetSelectionLine();
        }

        public void SetStateRadius(Int32 StateRadius)
        {
            Model.SetStateRadius(StateRadius);
        }

        public void SetStateEdgeSize(Int32 StateEdgeSize)
        {
            Model.SetStateEdgeSize(StateEdgeSize);
        }

        public void SetCurrentValueConstraint(ConstraintsEnum CurrentValueConstraint)
        {
            Model.SetCurrentValueConstraint(CurrentValueConstraint);
        }

        public Boolean TrySetValue(String ValueText)
        {
            if (CheckSyntax.CanParseValue(Model.GetElementType(), ValueText))
            {
                Model.SetCurrentValue(Conversions.ParseValue(Model.GetElementType(), ValueText));
                return true;
            }
            return false;
        }

        public void SetElementType(String ElementType)
        {
            Model.SetElementType(Conversions.StringToPrimitiveType(ElementType));
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        public void EventUpdateDisplay(Object Sender, FiniteStateBuilderEventArgs E)
        {
            View.UpdateDisplay();
        }

        #endregion

    } // End class

} // End namespace