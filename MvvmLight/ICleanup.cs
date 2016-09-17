namespace GalaSoft.MvvmLight
{
    /// <summary>
    /// Defines a common interface for classes that should be cleaned up,
    /// but without the implications that IDisposable presupposes. An instance
    /// implementing ICleanup can be cleaned up without being
    /// disposed and garbage collected.
    /// </summary>
    //// [ClassInfo(typeof(ViewModelBase))]
    public interface ICleanup
    {
        /// <summary>
        /// Cleans up the instance, for example by saving its state,
        /// removing resources, etc...
        /// </summary>
        void Cleanup();
    }
    //// End interface
}
//// End namespace