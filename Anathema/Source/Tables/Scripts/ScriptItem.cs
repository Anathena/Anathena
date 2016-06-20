using Anathema.Source.LuaEngine;
using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Tables.Scripts
{
    [Obfuscation(ApplyToMembers = false)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class ScriptItem : TableItem
    {
        [DataMember()]
        public String LuaScript
        {
            [Obfuscation(Exclude = true)]
            get;
            [Obfuscation(Exclude = true)]
            set;
        }

        [Obfuscation(Exclude = true)]
        private LuaCore LuaCore;

        public ScriptItem()
        {
            LuaCore = null;
        }

        public ScriptItem(String LuaScript) : this()
        {
            this.LuaScript = LuaScript;
        }

        [Obfuscation(Exclude = true)]
        public String GetDescription()
        {
            if (LuaScript != null)
            {
                String[] Lines = LuaScript.Trim().Split('\n');
                if (Lines.Length > 0 && Lines[0].StartsWith("--"))
                    return Lines[0].TrimStart('-').Trim();
            }

            return "No Description";
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

    } // End class

} // End namespace