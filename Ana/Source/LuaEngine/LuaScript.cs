namespace Ana.Source.LuaEngine
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.Serialization;

    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    [DataContract]
    internal class LuaScript
    {
        [Browsable(false)]
        private String script;

        public LuaScript()
        {
            this.Script = String.Empty;
        }

        public LuaScript(String script)
        {
            this.Script = script;
        }

        [DataMember]
        [Browsable(false)]
        public String Script
        {
            get
            {
                return this.script;
            }

            set
            {
                this.script = value == null ? String.Empty : value;
            }
        }
    }
    //// End class
}
//// End namespace