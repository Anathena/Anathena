using Anathema.Source.Controller;
using Anathema.Source.Project.ProjectItems;
using Anathema.Source.Scanners.FiniteStateScanner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Anathema.Source.Project.Deprecating
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    [Obfuscation(ApplyToMembers = false)]
    [Obfuscation(Exclude = true)]
    class TableManager
    {
        // Singleton instance of Table
        [Obfuscation(Exclude = true)]
        private static Lazy<TableManager> TableInstance = new Lazy<TableManager>(() => { return new TableManager(); });

        [Obfuscation(Exclude = true)]
        private TableData CurrentTableData;

        [Obfuscation(Exclude = true)]
        private Boolean Changed;

        private TableManager()
        {
            CurrentTableData = new TableData();
        }

        [Obfuscation(Exclude = true)]
        public static TableManager GetInstance()
        {
            return TableInstance.Value;
        }

        [Obfuscation(Exclude = true)]
        public void TableChanged()
        {
            Changed = true;
            Main.GetInstance().UpdateHasChanges(Changed);
        }

        [Obfuscation(Exclude = true)]
        public void TableSaved()
        {
            Changed = false;
            Main.GetInstance().UpdateHasChanges(Changed);
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
                    DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(TableData));

                    // Gather items we need to save
                    CurrentTableData.AddressItems = ProjectExplorer.GetInstance().GetAddressItems();
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
            if (Path == null || Path == String.Empty)
                return false;

            // DELETE XML SERIALIZER EVENTUALLY (Legacy loading scheme, switched to JSON)
            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    DataContractSerializer Serializer = new DataContractSerializer(typeof(TableData));
                    CurrentTableData = (TableData)Serializer.ReadObject(FileStream);

                    // Distribute loaded items to the appropriate tables
                    ProjectExplorer.GetInstance().SetAddressItems(CurrentTableData.AddressItems);
                    ScriptTable.GetInstance().SetScriptItems(CurrentTableData.ScriptItems);
                }
            }
            catch
            {
                try
                {
                    using (FileStream FileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                    {
                        DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(TableData));
                        CurrentTableData = (TableData)Serializer.ReadObject(FileStream);

                        // Distribute loaded items to the appropriate tables
                        ProjectExplorer.GetInstance().SetAddressItems(CurrentTableData.AddressItems);
                        ScriptTable.GetInstance().SetScriptItems(CurrentTableData.ScriptItems);
                    }
                }
                catch
                {
                    return false;
                }
            }

            TableSaved();
            return true;
        }

        [Obfuscation(Exclude = true)]
        public Boolean MergeTable(String Path)
        {
            if (Path == null || Path == String.Empty)
                return false;

            try
            {
                using (FileStream FileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    DataContractSerializer Serializer = new DataContractSerializer(typeof(TableData));
                    CurrentTableData = (TableData)Serializer.ReadObject(FileStream);

                    // Distribute loaded items to the appropriate tables
                    foreach (AddressItem Item in CurrentTableData.AddressItems)
                        ProjectExplorer.GetInstance().AddAddressItem(Item);

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