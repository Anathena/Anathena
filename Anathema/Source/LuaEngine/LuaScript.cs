using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.LuaEngine
{
    public class LuaScript
    {
        [Obfuscation(Exclude = true)]
        [Browsable(false)]
        private String _Script;
        [Obfuscation(Exclude = true)]
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