using Anathema.User.UserScriptEditor;
using Anathema.User.UserTable;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Anathema.User.UserScriptTable
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class ScriptTable : IScriptTableModel
    {
        private static ScriptTable ScriptTableInstance;

        private List<ScriptItem> ScriptItems;

        public event ScriptTableEventHandler EventClearScriptCacheItem;
        public event ScriptTableEventHandler EventClearScriptCache;

        private ScriptTable()
        {
            ScriptItems = new List<ScriptItem>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ScriptTable GetInstance()
        {
            if (ScriptTableInstance == null)
                ScriptTableInstance = new ScriptTable();
            return ScriptTableInstance;
        }

        private void RefreshDisplay()
        {
            // Request that all data be updated
            ScriptTableEventArgs Args = new ScriptTableEventArgs();
            Args.ItemCount = ScriptItems.Count;
            EventClearScriptCache(this, Args);
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

            ScriptTableEventArgs ScriptTableEventArgs = new ScriptTableEventArgs();
            ScriptTableEventArgs.ItemCount = ScriptItems.Count;
            EventClearScriptCache(this, ScriptTableEventArgs);

            Table.GetInstance().TableChanged();
        }

        public void DeleteScript(Int32 Index)
        {
            ScriptItems.RemoveAt(Index);

            ScriptTableEventArgs ScriptTableEventArgs = new ScriptTableEventArgs();
            ScriptTableEventArgs.ItemCount = ScriptItems.Count;
            EventClearScriptCache(this, ScriptTableEventArgs);

            Table.GetInstance().TableChanged();
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

            ScriptTableEventArgs ScriptTableEventArgs = new ScriptTableEventArgs();
            ScriptTableEventArgs.ItemCount = ScriptItems.Count;
            EventClearScriptCache(this, ScriptTableEventArgs);

            Table.GetInstance().TableChanged();
        }

        public void SaveScript(ScriptItem ScriptItem)
        {
            if (!ScriptItems.Contains(ScriptItem))
            {
                // Adding a new script
                ScriptItems.Add(ScriptItem);

                ScriptTableEventArgs ScriptTableEventArgs = new ScriptTableEventArgs();
                ScriptTableEventArgs.ItemCount = ScriptItems.Count;
                EventClearScriptCache(this, ScriptTableEventArgs);
            }
            else
            {
                // Updating an existing script, clear it from the cache
                ClearScriptItemFromCache(ScriptItem);
            }

            Table.GetInstance().TableChanged();
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
            ScriptTableEventArgs ScriptTableEventArgs = new ScriptTableEventArgs();
            ScriptTableEventArgs.ItemCount = ScriptItems.Count;
            EventClearScriptCache(this, ScriptTableEventArgs);

            Table.GetInstance().TableChanged();
        }

        public void SetScriptActivation(Int32 Index, Boolean Activated)
        {
            // Try to update the activation state
            ScriptItems[Index].SetActivationState(Activated);
            ClearScriptItemFromCache(ScriptItems[Index]);
        }

        private void ClearScriptItemFromCache(ScriptItem ScriptItem)
        {
            ScriptTableEventArgs ScriptTableEventArgs = new ScriptTableEventArgs();
            ScriptTableEventArgs.ClearCacheIndex = ScriptItems.IndexOf(ScriptItem);
            ScriptTableEventArgs.ItemCount = ScriptItems.Count;
            EventClearScriptCacheItem(this, ScriptTableEventArgs);
        }

    } // End class

} // End namespace