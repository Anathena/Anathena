using Anathema.MemoryManagement;
using Anathema.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Handles the updating of address table items
    /// </summary>
    class TableAddressEntryEditor : ITableAddressEntryEditorModel
    {

        public TableAddressEntryEditor()  { }

        public void AddTableEntryItem(Int32 MainSelection, Int32[] SelectedIndicies, AddressItem AddressItem)
        {
            AddressTable.GetInstance().SetAddressItemAt(MainSelection, AddressItem);
        }
        
    } // End class

} // End namespace