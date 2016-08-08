namespace Anathena.Source.Engine.OperatingSystems.Windows.Internals
{
    /// <summary>
    /// Defines an element able to be activated in the remote process.
    /// </summary>
    public interface IApplicableElement : IDisposableState
    {
        /// <summary>
        /// States if the element is enabled.
        /// </summary>
        bool IsEnabled { get; set; }
        /// <summary>
        /// Disables the element.
        /// </summary>
        void Disable();
        /// <summary>
        /// Enables the element.
        /// </summary>
        void Enable();

    } // End interface

} // End namespace