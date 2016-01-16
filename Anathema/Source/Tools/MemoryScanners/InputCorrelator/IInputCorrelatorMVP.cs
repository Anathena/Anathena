using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anathema
{
    delegate void InputCorrelatorEventHandler(Object Sender, InputCorrelatorEventArgs Args);
    class InputCorrelatorEventArgs : EventArgs
    {
        public InputNode Root = null;
    }

    interface IInputCorrelatorView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        event InputCorrelatorEventHandler EventUpdateDisplay;
    }

    abstract class IInputCorrelatorModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event InputCorrelatorEventHandler EventUpdateDisplay;
        protected virtual void OnEventUpdateDisplay(InputCorrelatorEventArgs E)
        {
            EventUpdateDisplay(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract void SetVariableSize(Int32 VariableSize);
        public abstract void AddNode(Stack<Int32> Indicies, InputNode.NodeTypeEnum NodeType);
        public abstract void DeleteNode(Stack<Int32> Indicies);
    }

    class InputCorrelatorPresenter : ScannerPresenter
    {
        new IInputCorrelatorView View;
        new IInputCorrelatorModel Model;

        public InputCorrelatorPresenter(IInputCorrelatorView View, IInputCorrelatorModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;
            // Bind events triggered by the model
            Model.EventUpdateDisplay += EventUpdateDisplay;
        }

        #region Method definitions called by the view (downstream)

        public void SetVariableSize(Int32 VariableSize)
        {
            if (VariableSize <= 0)
                return;

            Model.SetVariableSize(VariableSize);
        }

        public void AddAND(Stack<Int32> Indicies)
        {
            Model.AddNode(Indicies, InputNode.NodeTypeEnum.AND);
        }

        public void AddOR(Stack<Int32> Indicies)
        {
            Model.AddNode(Indicies, InputNode.NodeTypeEnum.OR);
        }

        public void AddNOT(Stack<Int32> Indicies)
        {
            Model.AddNode(Indicies, InputNode.NodeTypeEnum.NOT);
        }

        public void AddKey(Stack<Int32> Indicies, Keys Key)
        {

        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        public void EventUpdateDisplay(Object Sender, InputCorrelatorEventArgs E)
        {
            // View.EventUpdateDisplay();
        }

        #endregion
    }
}
