using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Project.ProjectItems
{
    [Obfuscation(ApplyToMembers = true)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class FolderItem : ProjectItem
    {
        public FolderItem() : this("New Folder") { }

        public FolderItem(String Description) : base(Description) { }

        public override void Update() { }

    } // End class

} // End namespace