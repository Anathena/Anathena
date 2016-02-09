using System;
using System.Runtime.Serialization;

namespace Anathema
{
    [DataContract()]
    public class ScriptItem : TableItem
    {
        [DataMember()]
        public String Script { get; set; }

        private LuaEngine LuaEngine;

        public ScriptItem()
        {
            LuaEngine = new LuaEngine();
        }

        public ScriptItem(String Script) : this()
        {
            this.Script = Script;
        }

        public String GetDescription()
        {
            if (Script != null)
            {
                String[] Lines = Script.Trim().Split('\n');
                if (Lines.Length > 0 && Lines[0].StartsWith("--"))
                    return Lines[0].TrimStart('-').Trim();
            }

            return "No Description";
        }

        public override void SetActivationState(Boolean Activated)
        {
            if (LuaEngine == null)
                LuaEngine = new LuaEngine();

            if (Activated)
            {
                // Try to run script. Will not activate on failure.
                if (!LuaEngine.RunActivationFunction(Script))
                    return;
            }
            else
            {
                // Try to deactivate script (we do not care if this fails)
                LuaEngine.RunDeactivationFunction(Script);
            }

            base.SetActivationState(Activated);
        }

    } // End class
}
