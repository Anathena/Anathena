using Aga.Controls.Tree;
using Anathema.Source.Engine.AddressResolver.DotNet;
using System;

namespace Anathema.GUI.CustomControls.TreeViews
{
    /// <summary>
    /// Node to be shown in the DotNet Explorer
    /// </summary>
    public class DotNetNode : Node
    {
        public String EntryName { get; set; }

        public DotNetObject DotNetObject { get; set; }

        /// <summary>
        /// Initializes a new node class with a given Text property
        /// </summary>
        /// <param name="text">String to set the text property with.</param>
        public DotNetNode(String EntryName) : base(String.Empty)
        {
            this.EntryName = EntryName;
        }

    } // End class

} // End namespace