using Anathema.User.UserScriptEditor;
using Anathema.User.UserTable;
using System;
using System.Collections.Generic;

namespace Anathema.User.UserScriptTable
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class ScriptTable : IScriptTableModel
    {
        // Singleton instance of Script Table
        private static Lazy<ScriptTable> ScriptTableInstance = new Lazy<ScriptTable>(() => { return new ScriptTable(); });

        private List<ScriptItem> ScriptItems;

        public event ScriptTableEventHandler EventUpdateScriptTableItemCount;

        private ScriptTable()
        {
            ScriptItems = new List<ScriptItem>();
        }

        public void OnGUIOpen()
        {
            UpdateScriptTableItemCount();
        }

        public static ScriptTable GetInstance()
        {
            return ScriptTableInstance.Value;
        }

        private void UpdateScriptTableItemCount()
        {
            // Request that all data be updated
            ScriptTableEventArgs Args = new ScriptTableEventArgs();
            Args.ItemCount = ScriptItems.Count;
            EventUpdateScriptTableItemCount?.Invoke(this, Args);
        }

        public void OpenScript(Int32 Index)
        {
            if (Index >= ScriptItems.Count)
                return;

            Main.GetInstance().OpenScriptEditor();
            ScriptEditor.GetInstance().OpenScript(ScriptItems[Index]);
        }

        public void AddScriptItem(ScriptItem Item)
        {
            ScriptItems.Add(Item);
            UpdateScriptTableItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public void DeleteScript(Int32 Index)
        {
            ScriptItems.RemoveAt(Index);
            UpdateScriptTableItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public void ReorderScript(Int32 SourceIndex, Int32 DestinationIndex)
        {
            // Bounds checking
            if (SourceIndex < 0 || SourceIndex > ScriptItems.Count)
                return;

            // If an item is being removed before the destination, the destination must be shifted
            if (DestinationIndex > SourceIndex)
                DestinationIndex--;

            // Bounds checking
            if (DestinationIndex < 0 || DestinationIndex > ScriptItems.Count)
                return;

            ScriptItem Item = ScriptItems[SourceIndex];
            ScriptItems.RemoveAt(SourceIndex);
            ScriptItems.Insert(DestinationIndex, Item);
            UpdateScriptTableItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public void SaveScript(ScriptItem ScriptItem)
        {
            // Adding a new script if we do not contain this, otherwise it is already updated
            if (!ScriptItems.Contains(ScriptItem))
                ScriptItems.Add(ScriptItem);

            UpdateScriptTableItemCount();
            TableManager.GetInstance().TableChanged();
        }

        public ScriptItem GetScriptItemAt(Int32 Index)
        {
            return ScriptItems[Index];
        }

        public List<ScriptItem> GetScriptItems()
        {
            return ScriptItems;
        }

        public void SetScriptItems(List<ScriptItem> ScriptItems)
        {
            this.ScriptItems = ScriptItems;
            UpdateScriptTableItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public void SetScriptActivation(Int32 Index, Boolean Activated)
        {
            // Try to update the activation state
            ScriptItems[Index].SetActivationState(Activated);
            UpdateScriptTableItemCount();
        }

    } // End class

} // End namespace