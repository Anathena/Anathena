using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Anathema
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class ScriptTable : IScriptTableModel
    {
        private static ScriptTable ScriptTableInstance;

        private TableData CurrentTableData;

        public event ScriptTableEventHandler EventClearScriptCacheItem;
        public event ScriptTableEventHandler EventClearScriptCache;

        private ScriptTable()
        {
            CurrentTableData = new TableData();
        }

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
            Args.ItemCount = CurrentTableData.ScriptTable.Count;
            EventClearScriptCache(this, Args);
        }

        public Boolean SaveTable(String Path)
        {
            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Create, FileAccess.Write))
                {
                    DataContractSerializer Serializer = new DataContractSerializer(typeof(TableData));
                    Serializer.WriteObject(FileStream, CurrentTableData);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Boolean LoadTable(String Path)
        {
            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    DataContractSerializer Serializer = new DataContractSerializer(typeof(TableData));
                    CurrentTableData = (TableData)Serializer.ReadObject(FileStream);
                }
            }
            catch
            {
                return false;
            }

            RefreshDisplay();
            return true;
        }

        public void OpenScript(Int32 Index)
        {
            if (Index >= CurrentTableData.ScriptTable.Count)
                return;

            Main.GetInstance().OpenScriptEditor();
            ScriptEditor.GetInstance().OpenScript(CurrentTableData.ScriptTable[Index]);
        }

        public void SaveScript(ScriptItem ScriptItem)
        {
            if (!CurrentTableData.ScriptTable.Contains(ScriptItem))
            {
                // Adding a new script
                CurrentTableData.ScriptTable.Add(ScriptItem);

                ScriptTableEventArgs ScriptTableEventArgs = new ScriptTableEventArgs();
                ScriptTableEventArgs.ItemCount = CurrentTableData.ScriptTable.Count;
                EventClearScriptCache(this, ScriptTableEventArgs);
            }
            else
            {
                // Updating an existing script, clear it from the cache
                ClearScriptItemFromCache(ScriptItem);
            }
        }

        public ScriptItem GetScriptItemAt(Int32 Index)
        {
            return CurrentTableData.ScriptTable[Index];
        }

        public void SetScriptActivation(Int32 Index, Boolean Activated)
        {
            // Try to update the activation state
            CurrentTableData.ScriptTable[Index].SetActivationState(Activated);
            ClearScriptItemFromCache(CurrentTableData.ScriptTable[Index]);
        }

        private void ClearScriptItemFromCache(ScriptItem ScriptItem)
        {
            ScriptTableEventArgs ScriptTableEventArgs = new ScriptTableEventArgs();
            ScriptTableEventArgs.ClearCacheIndex = CurrentTableData.ScriptTable.IndexOf(ScriptItem);
            ScriptTableEventArgs.ItemCount = CurrentTableData.ScriptTable.Count;
            EventClearScriptCacheItem(this, ScriptTableEventArgs);
        }
        
    } // End class

} // End namespace