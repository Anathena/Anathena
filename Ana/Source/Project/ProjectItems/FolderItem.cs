namespace Ana.Source.Project.ProjectItems
{
    using System;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// A folder project item which may contain other project items
    /// </summary>
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    [DataContract]
    internal class FolderItem : ProjectItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FolderItem" /> class
        /// </summary>
        public FolderItem() : this("New Folder")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderItem" /> class
        /// </summary>
        /// <param name="description">The description of the folder</param>
        public FolderItem(String description) : base(description)
        {
        }

        public override void Update()
        {
        }
    }
    //// End class
}
//// End namespace