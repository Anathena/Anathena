using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Project.ProjectItems
{
    [Obfuscation(ApplyToMembers = false)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class DotNetItem : ProjectItem
    {
        [Obfuscation(Exclude = true)]
        private String _DotNetObject;
        [Obfuscation(Exclude = true)]
        [DataMember()]
        [Category("Properties"), DisplayName(".NET Object"), Description("Full object path including namespaces")]
        public String DotNetObject
        {
            [Obfuscation(Exclude = true)]
            get { return _DotNetObject; }
            [Obfuscation(Exclude = true)]
            set { _DotNetObject = value; }
        }

        public DotNetItem() : this("New .NET Object") { }

        public DotNetItem(String Description) : base(Description) { }

    } // End class

} // End namespace