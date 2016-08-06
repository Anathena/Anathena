using Anathema.Source.LuaEngine;
using Anathema.Source.Project.ProjectItems.TypeEditors;
using Anathema.Source.Project.PropertyView.TypeConverters;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Project.ProjectItems
{
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    [DataContract()]
    public class ScriptItem : ProjectItem
    {
        private LuaScript _LuaScript;
        [DataMember()]
        [TypeConverter(typeof(LuaScriptConverter))]
        [Editor(typeof(ScriptEditor), typeof(UITypeEditor))]
        [Category("Properties"), DisplayName("Script"), Description("Lua script to interface with engine")]
        public LuaScript LuaScript
        {
            get { return _LuaScript; }
            set { _LuaScript = value; }
        }

        private LuaCore LuaCore;

        public ScriptItem() : this("New Script", null) { }
        public ScriptItem(String Description, LuaScript LuaScript) : base(Description)
        {
            this.LuaScript = LuaScript;

            LuaCore = null;
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

        public override void Update() { }

    } // End class

} // End namespace