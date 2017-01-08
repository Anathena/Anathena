namespace Ana.Source.ScriptEngine
{
    using Graphics;
    using Hook;
    using Input;
    using Memory;

    /// <summary>
    /// Script engine which contains classes with easy to use API, which wrap complex functionality of Ana's Engine.
    /// </summary>
    public class ScriptEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptEngine" /> class.
        /// </summary>
        public ScriptEngine()
        {
            this.MemoryCore = new MemoryCore();
            this.GraphicsCore = new GraphicsCore();
            this.HookCore = new HookCore();
            this.InputCore = new InputCore();
        }

        /// <summary>
        /// Gets or sets an interface to provide access to memory manipulations in an external process.
        /// </summary>
        public IMemoryCore MemoryCore { get; set; }

        /// <summary>
        /// Gets or sets an interface to provide access to manipulating graphics in an external process.
        /// </summary>
        public IGraphicsCore GraphicsCore { get; set; }

        /// <summary>
        /// Gets or sets an interface for environment manipulations in a hooked process.
        /// </summary>
        public IHookCore HookCore { get; set; }

        /// <summary>
        /// Gets or sets an interface to provide access to user input.
        /// </summary>
        public IInputCore InputCore { get; set; }
    }
    //// End class
}
//// End namespace