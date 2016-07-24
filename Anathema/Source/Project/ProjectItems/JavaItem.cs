using Anathema.Source.Engine;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Project.ProjectItems
{
    [Obfuscation(ApplyToMembers = false)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class JavaItem : ProjectItem
    {
        [Obfuscation(Exclude = true)]
        private String _DotNetObject;
        [Obfuscation(Exclude = true)]
        [DataMember()]
        [Category("Properties"), DisplayName("Java Object"), Description("Full object path including namespaces")]
        public String DotNetObject
        {
            [Obfuscation(Exclude = true)]
            get { return _DotNetObject; }
            [Obfuscation(Exclude = true)]
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