namespace Ana.Source.Project.ProjectItems
{
    using System;
    using System.Reflection;
    using System.Runtime.Serialization;

    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    [DataContract]
    public class FolderItem : ProjectItem
    {
        public FolderItem() : this("New Folder")
        {
        }

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