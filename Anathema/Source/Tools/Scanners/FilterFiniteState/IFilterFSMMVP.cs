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
    delegate void FilterFSMEventHandler(Object Sender, FilterFSMEventArgs Args);
    class FilterFSMEventArgs : EventArgs
    {

    }

    interface IFilterFSMView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateDisplay();
    }

    abstract class IFilterFSMModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event FilterFSMEventHandler EventUpdateDisplay;
        protected virtual void OnEventUpdateDisplay(FilterFSMEventArgs E)
        {
            EventUpdateDisplay(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract FiniteState GetEdgeUnderPoint(Point Location, Int32 Radius, Int32 EdgeSize);
        public abstract FiniteState GetStateUnderPoint(Point Location, Int32 Radius);
        public abstract void AddNewState(Point Location);
        public abstract void AddTransition();

        public abstract void FinishAction(Point Location);

        public abstract void DeleteAtPoint(Point Location);

        public abstract void SetElementType(Type ElementType);
        public abstract Type GetElementType();
    }

    class FilterFSMPresenter : ScannerPresenter
    {
        new IFilterFSMView View;
        new IFilterFSMModel Model;

        private ConstraintsEnum ValueConstraintSelection;
        private FiniteState DraggedState;
        private FiniteState SelectedState;

        private Int32 StateRadius;
        private Int32 StateEdgeSize;

        public FilterFSMPresenter(IFilterFSMView View, IFilterFSMModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventUpdateDisplay += EventUpdateDisplay;
        }

        #region Method definitions called by the view (downstream)

        public void SetStateRadius(Int32 StateRadius)
        {
            this.StateRadius = StateRadius;
        }

        public void SetStateEdgeSize(Int32 StateEdgeSize)
        {
            this.StateEdgeSize = StateEdgeSize;
        }

        public void SetValueConstraintSelection(ConstraintsEnum ValueConstraintSelection)
        {
            this.ValueConstraintSelection = ValueConstraintSelection;
        }

        public ConstraintsEnum GetValueConstraintSelection()
        {
            return ValueConstraintSelection;
        }

        public void SetElementType(String ElementType)
        {
            Model.SetElementType(Conversions.StringToPrimitiveType(ElementType));
        }

        public Type GetElementType()
        {
            return Model.GetElementType();
        }

        public void BeginAction(Point Location)
        {
            FiniteState State;

            // Handle edge selection
            State = Model.GetEdgeUnderPoint(Location, StateRadius, StateEdgeSize);
            if (State != null)
            {

                return;
            }

            // Handle state selection (drag)
            State = Model.GetStateUnderPoint(Location, StateRadius);
            if (State != null)
            {

                return;
            }

            // Handle state creation
            Model.AddNewState(Location);
        }

        public void UpdateAction(Point Location)
        {

        }

        public void FinishAction(Point Location, String ValueText)
        {
            Model.FinishAction(Location);
        }

        public void DeleteAtPoint(Point Location)
        {
            Model.DeleteAtPoint(Location);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        public void EventUpdateDisplay(Object Sender, FilterFSMEventArgs E)
        {
            View.UpdateDisplay();
        }

        #endregion
    }
}
