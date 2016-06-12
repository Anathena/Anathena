using System;

namespace Capture
{
    /// <summary>
    /// Indicates that the provided process does not have a window handle.
    /// </summary>
    public class ProcessHasNoWindowHandleException : Exception
    {
        public ProcessHasNoWindowHandleException()
            : base("The process does not have a window handle.")
        {
        }
    }

    public class ProcessAlreadyHookedException : Exception
    {
        public ProcessAlreadyHookedException()
            : base("The process is already hooked.")
        {
        }
    }

    public class InjectionFailedException : Exception
    {
        public InjectionFailedException(Exception innerException)
            : base("Injection to the target process failed. See InnerException for more detail.", innerException)
        {
        }
    }
}
