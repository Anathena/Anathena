using System;

namespace Anathena.Source.Engine.OperatingSystems.Windows.Internals
{
    /// <summary>
    /// Defines a element with a name.
    /// </summary>
    public interface INamedElement : IApplicableElement
    {
        /// <summary>
        /// The name of the element.
        /// </summary>
        String Name { get; }

    } // End interface

} // End namespace