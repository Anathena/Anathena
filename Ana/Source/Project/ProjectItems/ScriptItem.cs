namespace Ana.Source.Project.ProjectItems
{
    using LuaEngine;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.Serialization;

    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    [DataContract]
    internal class ScriptItem : ProjectItem
    {
        [Browsable(false)]
        private LuaScript luaScript;

        public ScriptItem() : this("New Script", null)
        {
        }

        public ScriptItem(String description, LuaScript luaScript) : base(description)
        {
            this.LuaScript = luaScript;

            LuaCore = null;
        }

        [DataMember]
        [Category("Properties"), DisplayName("Script"), Description("Lua script to interface with engine")]
        public LuaScript LuaScript
        {
            get
            {
                return this.luaScript;
            }

            set
            {
                this.luaScript = value;

                ProjectExplorer.GetInstance().ProjectChanged();
            }
        }

        [Browsable(false)]
        private LuaCore LuaCore { get; set; }

        public override void SetActivationState(Boolean activated)
        {
            if (LuaCore == null)
            {
                LuaCore = new LuaCore(LuaScript);
            }

            if (activated)
            {
                // Try to run script. Will not activate on failure.
                if (!LuaCore.RunActivationFunction())
                {
                    return;
                }

                LuaCore.RunUpdateFunction();
            }
            else
            {
                // Try to deactivate script (we do not care if this fails)
                LuaCore.RunDeactivationFunction();
            }

            base.SetActivationState(activated);
        }

        public override void Update()
        {
        }
    }
    //// End class
}
//// End namespace