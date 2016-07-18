using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Project.ProjectItems
{
    [Obfuscation(ApplyToMembers = false)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class FolderItem : ProjectItem
    {
        public FolderItem() : this("New Folder") { }

        public FolderItem(String Description) : base(Description) { }

    } // End class

} // End namespace