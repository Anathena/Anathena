using Anathema.User.UserAddressTable;
using System;
using System.Collections.Generic;

namespace Anathema.User.UserAddressTableEntryEditor
{
    /// <summary>
    /// Handles the updating of address table items
    /// </summary>
    class TableAddressEntryEditor : ITableAddressEntryEditorModel
    {

        public TableAddressEntryEditor()  { }

        public void AddTableEntryItem(Int32 MainSelection, IEnumerable<Int32> SelectedIndicies, AddressItem AddressItem)
        {
            AddressTable.GetInstance().SetAddressItemAt(MainSelection, AddressItem);
        }
        
    } // End class

} // End namespace