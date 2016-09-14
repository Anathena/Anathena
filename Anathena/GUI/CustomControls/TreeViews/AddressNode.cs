using Aga.Controls.Tree;
using Anathena.Source.Project.ProjectItems;
using System;
using System.Drawing;

namespace Anathena.GUI.CustomControls.TreeViews
{
    /// <summary>
    /// This only exists for the scratchpad implementation, which may not be kept
    /// </summary>
    public class AddressNode : Node
    {
        public String FreezeCheckBox { get; set; }
        public String EntryIcon { get; set; }
        public String EntryDescription { get; set; }
        public String EntryAddress { get; set; }
        public String EntryType { get; set; }
        public String EntryValue { get; set; }

        public ProjectItem ProjectItem { get; set; }

        private Boolean _checked;
        public Boolean Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        private Image _icon;
        public Image Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }


        /// <summary>
        /// Initializes a new MyNode class with a given Text property.
        /// </summary>
        /// <param name="text">String to set the text property with.</param>
        public AddressNode(String entryDescription, String entryAddress, String entryType, String entryValue) : base(String.Empty)
        {
            this.EntryDescription = entryDescription;
            this.EntryAddress = entryAddress;
            this.EntryType = entryType;
            this.EntryValue = entryValue;

            FreezeCheckBox = String.Empty;
            EntryIcon = String.Empty;
        }

    } // End class

} // End namespace