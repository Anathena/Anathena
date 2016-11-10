namespace Ana.Source.Project.ProjectItems
{
    using System;
    using System.Reflection;
    using System.Runtime.Serialization;

    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    [DataContract]
    internal class FolderItem : ProjectItem
    {
        /// <summary>
        /// 
        /// </summary>
        public FolderItem() : this("New Folder")
        {
        }

        /// <summary>
        /// 
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