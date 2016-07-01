using Aga.Controls.Tree;
using Anathema.Source.Project.ProjectItems;
using System;
using System.Drawing;

namespace Anathema.GUI
{
    /// <summary>
    /// TODO: This class should not have to exist, and only is required because of a hack done by the
    /// creators of TreeViewAdv. Fix their library for them.
    /// 
    /// Until then keep the public name strings in sync with the column headers for the address table
    /// </summary>
    public class ProjectNode : Node
    {
        public String FreezeCheckBox { get; set; }
        public String EntryIcon { get; set; }
        public String EntryDescription { get; set; }

        public ProjectItem ProjectItem { get; set; }

        private Boolean _Checked;
        public Boolean Checked
        {
            get { return _Checked; }
            set { _Checked = value; }
        }


        private Image _Icon;
        public Image Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }


        /// <summary>
        /// Initializes a new MyNode class with a given Text property.
        /// </summary>
        /// <param name="text">String to set the text property with.</param>
        public ProjectNode(String EntryDescription) : base(String.Empty)
        {
            this.EntryDescription = EntryDescription;

            FreezeCheckBox = String.Empty;
            EntryIcon = String.Empty;
        }

    } // End class

} // End namespace