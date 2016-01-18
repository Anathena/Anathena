using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void FiniteStateBuilderEventHandler(Object Sender, FiniteStateBuilderEventArgs Args);
    class FiniteStateBuilderEventArgs : EventArgs
    {

    }

    interface IFiniteStateBuilderView : IScannerView
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

        void SetValueConstraintSelection(ConstraintsEnum ValueConstraintSelection);
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

        public void SetValueConstraintSelection(ConstraintsEnum ValueConstraintSelection)
        {
            Model.SetValueConstraintSelection(ValueConstraintSelection);
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
    }
}
