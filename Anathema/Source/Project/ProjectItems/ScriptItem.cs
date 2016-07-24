using Anathema.Source.Engine;
using Anathema.Source.LuaEngine;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Project.ProjectItems
{
    [Obfuscation(ApplyToMembers = false)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class ScriptItem : ProjectItem
    {
        [Obfuscation(Exclude = true)]
        public LuaScript _LuaScript;
        [Obfuscation(Exclude = true)]
        [DataMember()]
        [Category("Properties"), DisplayName("Script"), Description("Lua script to interface with engine")]
        public LuaScript LuaScript
        {
            [Obfuscation(Exclude = true)]
            get { return _LuaScript; }
            [Obfuscation(Exclude = true)]
            set { _LuaScript = value; }
        }

        [Obfuscation(Exclude = true)]
        private LuaCore LuaCore;

        public ScriptItem()
        {
            LuaCore = null;
        }

        public ScriptItem(LuaScript LuaScript) : this()
        {
            this.LuaScript = LuaScript;
        }

        [Obfuscation(Exclude = true)]
        public override void SetActivationState(Boolean Activated)
        {
            if (LuaCore == null)
                LuaCore = new LuaCore(LuaScript);

            if (Activated)
            {
                // Try to run script. Will not activate on failure.
                if (!LuaCore.RunActivationFunction())
                    return;

                LuaCore.RunUpdateFunction();
            }
            else
            {
                // Try to deactivate script (we do not care if this fails)
                LuaCore.RunDeactivationFunction();
            }

            base.SetActivationState(Activated);
        }

        public override void Update(EngineCore EngineCore) { }

    } // End class

} // End namespace