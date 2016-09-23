namespace Ana.Source.Engine.OperatingSystems
{
    using System;

    /// <summary>
    /// Defines an OS independent module region and attributes
    /// </summary>
    internal class NormalizedModule : NormalizedRegion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NormalizedModule" /> class
        /// </summary>
        /// <param name="name">The name of the module</param>
        /// <param name="baseAddress">The base address of the module</param>
        /// <param name="size">The total size of the module</param>
        public NormalizedModule(String name, IntPtr baseAddress, Int32 size) : base(baseAddress, size)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name of the module
        /// </summary>
        public String Name { get; private set; }
    }
    //// End interface
}
//// End namespace