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
        public FolderItem(String Description = null) { }

    } // End class

} // End namespace