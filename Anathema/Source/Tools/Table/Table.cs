using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Anathema
{
    [DataContract()]
    public class TableData
    {
        [DataMember()]
        public List<AddressItem> AddressTable;

        [DataMember()]
        public List<ScriptItem> ScriptTable;

        public List<FiniteStateMachine> FiniteStateMachineTable;

        public TableData()
        {
            AddressTable = new List<AddressItem>();
            ScriptTable = new List<ScriptItem>();
            FiniteStateMachineTable = new List<FiniteStateMachine>();
        }
    }

    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class Table : ITableModel, IProcessObserver
    {
        public enum TableColumnEnum
        {
            Frozen,
            Description,
            ValueType,
            Address,
            Value
        }

        private static Table TableInstance;
        private MemoryEditor MemoryEditor;

        private TableData CurrentTableData;

        private Table()
        {
            InitializeProcessObserver();
            CurrentTableData = new TableData();
        }

        public static Table GetInstance()
        {
            if (TableInstance == null)
                TableInstance = new Table();
            return TableInstance;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemoryEditor MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
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
            return true;
        }
        
    } // End class

} // End namespace