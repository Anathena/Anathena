using Aga.Controls.Tree;
using Anathena.Source.Project.ProjectItems;
using System;
using System.Drawing;

namespace Anathena.GUI.CustomControls.TreeViews
{
    /// <summary>
    /// Defines a node in the project explorer
    /// </summary>
    public class ProjectNode : Node
    {
        public String EntryDescription { get; set; }
        public Image EntryIcon { get; set; }

        public ProjectItem ProjectItem { get; set; }

        /// <summary>
        /// Initializes a new MyNode class with a given Text property.
        /// </summary>
        /// <param name="text">String to set the text property with.</param>
        public ProjectNode(String EntryDescription) : base(String.Empty)
        {
            this.EntryDescription = EntryDescription;

            EntryIcon = null;
        }

    } // End class

} // End namespace