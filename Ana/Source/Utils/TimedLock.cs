﻿using System;
using System.Threading;

namespace Ana.Source.Utils
{
    public struct TimedLock : IDisposable
    {
        private Object targetObject;

        public static TimedLock Lock(Object lockObject)
        {
#if DEBUG
            return Lock(lockObject, TimeSpan.FromSeconds(120));
#else
            return Lock(lockObject, TimeSpan.FromSeconds(1000));
#endif
        }

        public static TimedLock Lock(Object lockObject, TimeSpan timeout)
        {
            TimedLock TimedLock = new TimedLock(lockObject);
            if (!Monitor.TryEnter(lockObject, timeout))
            {
#if DEBUG
                GC.SuppressFinalize(TimedLock.leakDetector);
#endif
                throw new LockTimeoutException();
            }

            return TimedLock;
        }

        private TimedLock(Object lockObject)
        {
            targetObject = lockObject;
#if DEBUG
            leakDetector = new Sentinel();
#endif
        }

        public void Dispose()
        {
            if (targetObject != null)
                Monitor.Exit(targetObject);

            // It's a bad error if someone forgets to call Dispose,
            // so in Debug builds, we put a finalizer in to detect
            // the error. If Dispose is called, we suppress the
            // finalizer.
#if DEBUG
            if (leakDetector != null)
            {
                GC.SuppressFinalize(leakDetector);
            }
#endif
        }

#if DEBUG
        /// <summary>
        /// (In Debug mode, we make it a class so that we can add a finalizer in order to detect when the object is not freed.)
        /// </summary>
        private class Sentinel
        {
            ~Sentinel()
            {
                // If this finalizer runs, someone somewhere failed to
                // call Dispose, which means we've failed to leave a monitor!
                System.Diagnostics.Debug.Fail("Undisposed lock");
            }
        }
        private Sentinel leakDetector;
#endif

    }

    public class LockTimeoutException : ApplicationException
    {
        public LockTimeoutException() : base("Timeout waiting for lock") { }

    }
    //// End class
}
//// End namespace