namespace Ana.Source.LuaEngine
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines a Lua script that can leverage the engine to execute a cheat
    /// </summary>
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    [DataContract]
    internal class LuaScript
    {
        [Browsable(false)]
        private String script;

        /// <summary>
        /// Initializes a new instance of the <see cref="LuaScript" /> class
        /// </summary>
        public LuaScript()
        {
            this.Script = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LuaScript" /> class
        /// </summary>
        /// <param name="script">The raw script text</param>
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