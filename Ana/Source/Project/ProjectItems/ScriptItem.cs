﻿namespace Ana.Source.Project.ProjectItems
{
    using LuaEngine;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.Serialization;
    using Utils.ScriptEditor;
    using Utils.TypeConverters;

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
        [TypeConverter(typeof(ScriptConverter))]
        [Editor(typeof(ScriptEditorModel), typeof(UITypeEditor))]
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
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
            }
        }

        [Browsable(false)]
        private LuaCore LuaCore { get; set; }

        public override void Update()
        {
        }

        protected override void OnActivationChanged()
        {
            if (LuaCore == null)
            {
                LuaCore = new LuaCore(LuaScript);
            }

            if (this.IsActivated)
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
        }
    }
    //// End class
}
//// End namespace