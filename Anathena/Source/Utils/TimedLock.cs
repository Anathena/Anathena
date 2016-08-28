using System;
using System.Threading;

namespace Anathena.Source.Utils
{
    public struct TimedLock : IDisposable
    {
        public static TimedLock Lock(Object Object)
        {
#if DEBUG
            return Lock(Object, TimeSpan.FromSeconds(120));
#else
            return Lock(Object, TimeSpan.FromSeconds(1000));
#endif
        }

        public static TimedLock Lock(Object Object, TimeSpan Timeout)
        {
            TimedLock TimedLock = new TimedLock(Object);
            if (!Monitor.TryEnter(Object, Timeout))
            {
#if DEBUG
                System.GC.SuppressFinalize(TimedLock.LeakDetector);
#endif
                throw new LockTimeoutException();
            }

            return TimedLock;
        }

        private TimedLock(Object Object)
        {
            Target = Object;
#if DEBUG
            LeakDetector = new Sentinel();
#endif
        }
        private object Target;

        public void Dispose()
        {
            if (Target != null)
                Monitor.Exit(Target);

            // It's a bad error if someone forgets to call Dispose,
            // so in Debug builds, we put a finalizer in to detect
            // the error. If Dispose is called, we suppress the
            // finalizer.
#if DEBUG
            if (LeakDetector != null)
                GC.SuppressFinalize(LeakDetector);
#endif
        }

#if DEBUG
        // (In Debug mode, we make it a class so that we can add a finalizer
        // in order to detect when the object is not freed.)
        private class Sentinel
        {
            ~Sentinel()
            {
                // If this finalizer runs, someone somewhere failed to
                // call Dispose, which means we've failed to leave
                // a monitor!
                System.Diagnostics.Debug.Fail("Undisposed lock");
            }
        }
        private Sentinel LeakDetector;
#endif

    }
    public class LockTimeoutException : ApplicationException
    {
        public LockTimeoutException() : base("Timeout waiting for lock") { }

    } // End class

} // End namespace