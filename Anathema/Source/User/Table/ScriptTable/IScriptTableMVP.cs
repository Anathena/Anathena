using Anathema.Utils.MVP;
using System;

namespace Anathema.User.UserScriptTable
{
    delegate void ScriptTableEventHandler(Object Sender, ScriptTableEventArgs Args);
    class ScriptTableEventArgs : EventArgs
    {
        public Int32 ItemCount = 0;
        public Int32 ClearCacheIndex = -1;
    }

    interface IScriptTableView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateScriptTableItemCount(Int32 ItemCount);
    }

    interface IScriptTableModel : IModel
    {
        // Events triggered by the model (upstream)
        event ScriptTableEventHandler EventUpdateScriptTableItemCount;

        // Functions invoked by presenter (downstream)
        void OpenScript(Int32 Index);
        void DeleteScript(Int32 Index);
        void ReorderScript(Int32 SourceIndex, Int32 DestinationIndex);
        void AddScriptItem(ScriptItem Item);

        ScriptItem GetScriptItemAt(Int32 Index);
        void SetScriptActivation(Int32 Index, Boolean Activated);
    }

    class ScriptTablePresenter : Presenter<IScriptTableView, IScriptTableModel>
    {
        protected new IScriptTableView View { get; set; }
        protected new IScriptTableModel Model { get; set; }

        public ScriptTablePresenter(IScriptTableView View, IScriptTableModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventUpdateScriptTableItemCount += EventUpdateScriptTableItemCount;
        }

        #region Method definitions called by the view (downstream)

        public ScriptItem GetScriptTableItemAt(Int32 Index)
        {
            return Model.GetScriptItemAt(Index);
        }

        public void OpenScript(Int32 Index)
        {
            Model.OpenScript(Index);
        }

        public void DeleteScript(Int32 Index)
        {
            Model.DeleteScript(Index);
        }

        public void ReorderScript(Int32 SourceIndex, Int32 DestinationIndex)
        {
            Model.ReorderScript(SourceIndex, DestinationIndex);
        }

        public String GetScriptTableScriptAt(Int32 Index)
        {
            return Model.GetScriptItemAt(Index).Script;
        }

        public void SetScriptActivation(Int32 Index, Boolean Activated)
        {
            Model.SetScriptActivation(Index, Activated);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateScriptTableItemCount(Object Sender, ScriptTableEventArgs E)
        {
            View.UpdateScriptTableItemCount(E.ItemCount);
        }

        #endregion

    } // End class

} // End namespace