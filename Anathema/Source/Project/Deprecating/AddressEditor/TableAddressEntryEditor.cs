using Anathema.Source.Project.ProjectItems;
using System;
using System.Collections.Generic;

namespace Anathema.Source.Project.Editors.AddressEditor
{
    /// <summary>
    /// Handles the updating of address table items
    /// </summary>
    class TableAddressEntryEditor : IAddressEditorModel
    {

        public TableAddressEntryEditor() { }

        public void OnGUIOpen() { }

        public void AddTableEntryItem(Int32 MainSelection, IEnumerable<Int32> SelectedIndicies, AddressItem AddressItem)
        {
            ProjectExplorer.GetInstance().SetAddressItemAt(MainSelection, AddressItem);
        }

    } // End class

} // End namespace