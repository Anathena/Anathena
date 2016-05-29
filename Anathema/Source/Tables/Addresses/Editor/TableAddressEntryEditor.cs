using System;
using System.Collections.Generic;

namespace Anathema.Source.Tables.Addresses.Editor
{
    /// <summary>
    /// Handles the updating of address table items
    /// </summary>
    class TableAddressEntryEditor : ITableAddressEntryEditorModel
    {

        public TableAddressEntryEditor() { }

        public void OnGUIOpen() { }

        public void AddTableEntryItem(Int32 MainSelection, IEnumerable<Int32> SelectedIndicies, AddressItem AddressItem)
        {
            AddressTable.GetInstance().SetAddressItemAt(MainSelection, AddressItem);
        }

    } // End class

} // End namespace