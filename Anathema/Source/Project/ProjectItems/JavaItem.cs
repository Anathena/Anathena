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
    public class JavaItem : ProjectItem
    {
        private String _DotNetObject;
        [DataMember()]
        [Category("Properties"), DisplayName("Java Object"), Description("Full object path including namespaces")]
        public String DotNetObject
        {
            get { return _DotNetObject; }
            set { _DotNetObject = value; }
        }

        public JavaItem() : this("New Java Object") { }

        public JavaItem(String Description) : base(Description) { }

        public override void Update(EngineCore EngineCore)
        {
            // TODO: Resolve address once we implement a resolver
        }

    } // End class

} // End namespace