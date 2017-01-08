namespace Ana.Source.CustomControls.TreeView
{
    using Aga.Controls.Tree;
    using Project.ProjectItems;
    using System;
    using System.Drawing;

    /// <summary>
    /// Defines a <see cref="TreeViewAdv"/> node in the project explorer.
    /// </summary>
    internal class ProjectNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectNode" /> class.
        /// </summary>
        /// <param name="entryDescription">String with which to set the description property.</param>
        public ProjectNode(String entryDescription) : base(String.Empty)
        {
            this.EntryDescription = entryDescription;
            this.EntryValuePreview = String.Empty;
            this.EntryIcon = null;
        }

        /// <summary>
        /// Gets or sets the description of the project node.
        /// </summary>
        public String EntryDescription { get; set; }

        /// <summary>
        /// Gets or sets the value preview of the project node.
        /// </summary>
        public String EntryValuePreview { get; set; }

        /// <summary>
        /// Gets or sets the icon of the project node.
        /// </summary>
        public Image EntryIcon { get; set; }

        /// <summary>
        /// Gets or sets the project item associated with this node.
        /// </summary>
        public ProjectItem ProjectItem { get; set; }
    }
    //// End class
}
//// End namespace
