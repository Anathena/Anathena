using Anathema.Source.Engine;
using Anathema.Source.LuaEngine;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Project.ProjectItems
{
    [Obfuscation(ApplyToMembers = true)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class ScriptItem : ProjectItem
    {
        public LuaScript _LuaScript;
        [DataMember()]
        [Category("Properties"), DisplayName("Script"), Description("Lua script to interface with engine")]
        public LuaScript LuaScript
        {
            get { return _LuaScript; }
            set { _LuaScript = value; }
        }

        private LuaCore LuaCore;

        public ScriptItem()
        {
            LuaCore = null;
        }

        public ScriptItem(LuaScript LuaScript) : this()
        {
            this.LuaScript = LuaScript;
        }

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