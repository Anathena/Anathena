using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Anathema
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class Table : ITableModel
    {
        private static Table TableInstance;

        private TableData CurrentTableData;

        private Table()
        {
            CurrentTableData = new TableData();
        }

        public static Table GetInstance()
        {
            if (TableInstance == null)
                TableInstance = new Table();
            return TableInstance;
        }

        /// <summary>
        /// A serializable class (via DataContractSerializer) to allow for easy XML saving of our addresses, scripts, and FSMs
        /// </summary>
        [DataContract()]
        private class TableData
        {
            [DataMember()]
            public List<AddressItem> AddressItems;

            [DataMember()]
            public List<ScriptItem> ScriptItems;

            public List<FiniteStateMachine> FiniteStateMachineItems;

            public TableData()
            {
                AddressItems = new List<AddressItem>();
                ScriptItems = new List<ScriptItem>();
                FiniteStateMachineItems = new List<FiniteStateMachine>();
            }
        }

        public Boolean SaveTable(String Path)
        {
            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Create, FileAccess.Write))
                {
                    DataContractSerializer Serializer = new DataContractSerializer(typeof(TableData));

                    // Gather items we need to save
                    CurrentTableData.AddressItems = AddressTable.GetInstance().GetAddressItems();
                    CurrentTableData.ScriptItems = ScriptTable.GetInstance().GetScriptItems();

                    Serializer.WriteObject(FileStream, CurrentTableData);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Boolean OpenTable(String Path)
        {
            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    DataContractSerializer Serializer = new DataContractSerializer(typeof(TableData));
                    CurrentTableData = (TableData)Serializer.ReadObject(FileStream);

                    // Distribute loaded items to the appropriate tables
                    AddressTable.GetInstance().SetAddressItems(CurrentTableData.AddressItems);
                    ScriptTable.GetInstance().SetScriptItems(CurrentTableData.ScriptItems);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Boolean MergeTable(String Path)
        {
            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    DataContractSerializer Serializer = new DataContractSerializer(typeof(TableData));
                    CurrentTableData = (TableData)Serializer.ReadObject(FileStream);

                    // Distribute loaded items to the appropriate tables
                    foreach (AddressItem Item in CurrentTableData.AddressItems)
                        AddressTable.GetInstance().AddAddressItem(Item);

                    foreach (ScriptItem Item in CurrentTableData.ScriptItems)
                        ScriptTable.GetInstance().AddScriptItem(Item);
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