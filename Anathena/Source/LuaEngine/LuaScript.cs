using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace Ana.Source.LuaEngine
{
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    [DataContract()]
    public class LuaScript
    {
        [Browsable(false)]
        private String _Script;
        [DataMember()]
        [Browsable(false)]
        public String Script
        {
            get { return _Script; }
            set { if (value == null) value = String.Empty; _Script = value; }
        }

        public LuaScript()
        {
            Script = String.Empty;
        }

        public LuaScript(String Script)
        {
            this.Script = Script;
        }

    } // End class

} // End namespace