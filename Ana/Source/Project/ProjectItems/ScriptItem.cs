namespace Ana.Source.Project.ProjectItems
{
    using Output;
    using ScriptEngine;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.Serialization;
    using Utils.ScriptEditor;
    using Utils.TypeConverters;

    /// <summary>
    /// Defines a script that can be added to the project explorer.
    /// </summary>
    [DataContract]
    internal class ScriptItem : ProjectItem
    {
        /// <summary>
        /// The raw script text.
        /// </summary>
        [Browsable(false)]
        private String script;

        /// <summary>
        /// Whether the script is compiled.
        /// </summary>
        [Browsable(false)]
        private Boolean isCompiled;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptItem" /> class.
        /// </summary>
        public ScriptItem() : this("New Script", null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptItem" /> class.
        /// </summary>
        /// <param name="description">The description of the project item.</param>
        /// <param name="script">The raw script text.</param>
        /// <param name="compiled">Whether or not this script is compiled.</param>
        public ScriptItem(String description, String script, Boolean compiled = false) : base(description)
        {
            this.Script = script;
            this.IsCompiled = compiled;

            this.ScriptManager = null;
        }

        /// <summary>
        /// Gets or sets the raw script text.
        /// </summary>
        [DataMember]
        [ReadOnly(false)]
        [TypeConverter(typeof(ScriptConverter))]
        [Editor(typeof(ScriptEditorModel), typeof(UITypeEditor))]
        [Category("Properties"), DisplayName("Script"), Description("C# script to interface with engine")]
        public String Script
        {
            get
            {
                return this.script;
            }

            set
            {
                this.script = value;
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the script is compiled.
        /// </summary>
        [DataMember]
        [ReadOnly(true)]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Properties"), DisplayName("Compiled"), Description("Whether or not this script has been compiled.")]
        public Boolean IsCompiled
        {
            get
            {
                return this.isCompiled;
            }

            set
            {
                this.isCompiled = value;
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
            }
        }

        /// <summary>
        /// Gets or sets the script manager associated with this script.
        /// </summary>
        [Browsable(false)]
        private ScriptManager ScriptManager { get; set; }

        /// <summary>
        /// Clones the project item.
        /// </summary>
        /// <returns>The clone of the project item.</returns>
        public override ProjectItem Clone()
        {
            ScriptItem clone = new ScriptItem();
            clone.Description = this.Description;
            clone.Parent = this.Parent;
            clone.script = this.Script;
            clone.isCompiled = this.IsCompiled;

            return clone;
        }

        /// <summary>
        /// Clones the script item and compiles it.
        /// </summary>
        /// <returns>The clone of the project. Returns null on compilation failure.</returns>
        public ScriptItem Compile()
        {
            if (this.ScriptManager == null)
            {
                this.ScriptManager = new ScriptManager();
            }

            if (this.IsCompiled)
            {
                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Warn, "Script already compiled");
                return null;
            }

            try
            {
                ScriptItem clone = this.Clone() as ScriptItem;
                clone.Description += " - [Compiled]";
                clone.isCompiled = true;
                clone.script = this.ScriptManager.CompileScript(clone.script);
                return clone;
            }
            catch
            {
                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Error, "Unable to complete compile request.");
                return null;
            }
        }

        /// <summary>
        /// Update event for this project item.
        /// </summary>
        public override void Update()
        {
        }

        /// <summary>
        /// Called when the activation state changes. Toggles this script.
        /// </summary>
        protected override void OnActivationChanged()
        {
            if (this.ScriptManager == null)
            {
                this.ScriptManager = new ScriptManager();
            }

            if (this.IsActivated)
            {
                // Try to run script.
                if (!this.ScriptManager.RunActivationFunction(this))
                {
                    // Script error -- clear activation.
                    this.ResetActivation();
                    return;
                }

                // Run the update loop for the script
                this.ScriptManager.RunUpdateFunction(this);
            }
            else
            {
                // Try to deactivate script (we do not care if this fails)
                this.ScriptManager.RunDeactivationFunction(this);
            }
        }
    }
    //// End class
}
//// End namespace