using Anathema.Source.Engine;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Project.ProjectItems
{
    [Obfuscation(ApplyToMembers = true)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class DotNetItem : ProjectItem
    {
        private String _DotNetObject;
        [DataMember()]
        [Category("Properties"), DisplayName(".NET Object"), Description("Full object path including namespaces")]
        public String DotNetObject
        {
            get { return _DotNetObject; }
            set { _DotNetObject = value; }
        }

        public DotNetItem() : this("New .NET Object") { }

        public DotNetItem(String Description) : base(Description) { }

        public override void Update(EngineCore EngineCore)
        {
            // TODO: Resolve address
        }

    } // End class

} // End namespace