using Anathema.Utils.Cache;
using Anathema.Utils.MVP;
using System;
using System.Windows.Forms;

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
        event ScriptTableEventHandler EventClearScriptCacheItem;
        event ScriptTableEventHandler EventClearScriptCache;

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
        
        private ListViewCache ScriptTableCache;

        private const Int32 ActivationCheckBoxIndex = 0;
        private const Int32 ScriptDescriptionIndex = 1;

        public ScriptTablePresenter(IScriptTableView View, IScriptTableModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;
            
            ScriptTableCache = new ListViewCache();

            // Bind events triggered by the model

            Model.EventClearScriptCacheItem += EventClearScriptCacheItem;
            Model.EventClearScriptCache += EventClearScriptCache;
        }

        #region Method definitions called by the view (downstream)

        public ListViewItem GetScriptTableItemAt(Int32 Index)
        {
            ListViewItem Item = ScriptTableCache.Get((UInt64)Index);
            ScriptItem ScriptItem = Model.GetScriptItemAt(Index);

            // Try to update and return the item if it is a valid item
            if (Item != null)
            {
                Item.Checked = ScriptItem.GetActivationState();
                return Item;
            }
            // Add the properties to the manager and get the list view item back
            Item = ScriptTableCache.Add(Index, new String[] { String.Empty, String.Empty });

            Item.SubItems[ActivationCheckBoxIndex].Text = String.Empty;
            Item.SubItems[ScriptDescriptionIndex].Text = ScriptItem.GetDescription();
            Item.Checked = ScriptItem.GetActivationState();

            return Item;
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
        
        private void EventClearScriptCacheItem(Object Sender, ScriptTableEventArgs E)
        {
            ScriptTableCache.Delete((UInt64)E.ClearCacheIndex);
            View.UpdateScriptTableItemCount(E.ItemCount);
        }

        private void EventClearScriptCache(Object Sender, ScriptTableEventArgs E)
        {
            ScriptTableCache.FlushCache();
            View.UpdateScriptTableItemCount(E.ItemCount);
        }

        #endregion
    }
}