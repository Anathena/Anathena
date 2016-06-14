using System;

namespace Anathema.Source.SystemInternals.Graphics.DirectXHook
{
    /// <summary>
    /// Indicates that the provided process does not have a window handle.
    /// </summary>
    public class ProcessHasNoWindowHandleException : Exception
    {
        public ProcessHasNoWindowHandleException() : base("The process does not have a window handle.") { }

    }

    public class ProcessAlreadyHookedException : Exception
    {
        public ProcessAlreadyHookedException() : base("The process is already hooked.") { }

    }

    public class InjectionFailedException : Exception
    {
        public InjectionFailedException(Exception innerException) : base("Injection failed. See InnerException for more detail.", innerException) { }

    } // End class

} // End namespace