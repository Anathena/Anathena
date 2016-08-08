using Aga.Controls.Tree;
using Anathena.Source.Project.ProjectItems;
using System;

namespace Anathena.GUI.CustomControls.TreeViews
{
    /// <summary>
    /// Defines a node in the property viewer
    /// </summary>
    public class PropertyNode : Node
    {
        public String EntryDescription { get; set; }
        public String EntryValue { get; set; }

        public ProjectItem ProjectItem { get; set; }

        /// <summary>
        /// Initializes a new MyNode class with a given Text property.
        /// </summary>
        /// <param name="text">String to set the text property with.</param>
        public PropertyNode(String EntryDescription, String EntryValue) : base(String.Empty)
        {
            this.EntryDescription = EntryDescription;
            this.EntryValue = EntryValue;
        }

    } // End class

} // End namespace