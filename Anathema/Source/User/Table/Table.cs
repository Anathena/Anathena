using Anathema.Scanners.FiniteStateScanner;
using Anathema.User.UserAddressTable;
using Anathema.User.UserScriptTable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Anathema.User.UserTable
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    [Obfuscation(ApplyToMembers = false)]
    [Obfuscation(Exclude = true)]
    class Table : ITableModel
    {
        [Obfuscation(Exclude = true)]
        private static Table TableInstance;

        [Obfuscation(Exclude = true)]
        public event TableEventHandler EventHasChanges;

        [Obfuscation(Exclude = true)]
        private TableData CurrentTableData;

        [Obfuscation(Exclude = true)]
        private Boolean Changed;

        private Table()
        {
            CurrentTableData = new TableData();
        }

        [Obfuscation(Exclude = true)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Table GetInstance()
        {
            if (TableInstance == null)
                TableInstance = new Table();
            return TableInstance;
        }

        [Obfuscation(Exclude = true)]
        public void TableChanged()
        {
            Changed = true;

            TableEventArgs Args = new TableEventArgs();
            Args.HasChanges = Changed;
            EventHasChanges(this, Args);
        }

        [Obfuscation(Exclude = true)]
        public void TableSaved()
        {
            Changed = false;

            TableEventArgs Args = new TableEventArgs();
            Args.HasChanges = Changed;
            EventHasChanges(this, Args);
        }

        [Obfuscation(Exclude = true)]
        public Boolean HasChanges()
        {
            return Changed;
        }

        [Obfuscation(Exclude = true)]
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

            TableSaved();
            return true;
        }

        [Obfuscation(Exclude = true)]
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

            TableSaved();
            return true;
        }

        [Obfuscation(Exclude = true)]
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

            TableChanged();
            return true;
        }

        /// <summary>
        /// A serializable class (via DataContractSerializer) to allow for easy XML saving of our addresses, scripts, and FSMs
        /// Note that because of this, C# will be using reflection to reconstruct the table upon table load, which means
        /// things will break if we obfuscate members of this class.
        /// </summary>
        [Obfuscation(ApplyToMembers = false)]
        [Obfuscation(Exclude = true)]
        [DataContract()]
        private class TableData
        {
            [Obfuscation(Exclude = true)]
            [DataMember()]
            public List<AddressItem> AddressItems;

            [Obfuscation(Exclude = true)]
            [DataMember()]
            public List<ScriptItem> ScriptItems;

            [Obfuscation(Exclude = true)]
            public List<FiniteStateMachine> FiniteStateMachineItems;

            public TableData()
            {
                AddressItems = new List<AddressItem>();
                ScriptItems = new List<ScriptItem>();
                FiniteStateMachineItems = new List<FiniteStateMachine>();
            }
        }

    } // End class

} // End namespace