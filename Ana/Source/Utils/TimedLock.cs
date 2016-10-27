namespace Ana.Source.Utils
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    /// <summary>
    /// A lock used in debugging which throws an exception if unreleased for too long
    /// </summary>
    internal struct TimedLock : IDisposable
    {
        /// <summary>
        /// The object being locked
        /// </summary>
        private Object targetObject;

        /// <summary>
        /// Object to check for undisposed locks
        /// </summary>
        private Sentinel leakDetector;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedLock" /> struct
        /// </summary>
        /// <param name="lockObject">The object being locked</param>
        private TimedLock(Object lockObject)
        {
            this.targetObject = lockObject;
#if DEBUG
            this.leakDetector = new Sentinel();
#endif
        }

        /// <summary>
        /// Creates a lock using the provided object. An exception will be thrown after a default timespan if the lock is not released
        /// </summary>
        /// <param name="lockObject">The object on which to initiate the lcok</param>
        /// <returns>Returns a reference to the timed lock object</returns>
        public static TimedLock Lock(Object lockObject)
        {
#if DEBUG
            return Lock(lockObject, TimeSpan.FromSeconds(120));
#else
            return Lock(lockObject, TimeSpan.FromSeconds(1000));
#endif
        }

        /// <summary>
        /// Creates a lock using the provided object
        /// </summary>
        /// <param name="lockObject">The object on which to initiate the lcok</param>
        /// <param name="timeout">The timeout until an exception is thrown</param>
        /// <returns>Returns a reference to the timed lock object</returns>
        public static TimedLock Lock(Object lockObject, TimeSpan timeout)
        {
            TimedLock timedLock = new TimedLock(lockObject);

            if (!Monitor.TryEnter(lockObject, timeout))
            {
#if DEBUG
                GC.SuppressFinalize(timedLock.leakDetector);
#endif
                throw new LockTimeoutException();
            }

            return timedLock;
        }

        /// <summary>
        /// Disposes of managed resources
        /// </summary>
        public void Dispose()
        {
            if (this.targetObject != null)
            {
                Monitor.Exit(this.targetObject);
            }

            // It's a bad error if someone forgets to call Dispose, so in Debug builds, we put a finalizer in to detect
            // the error. If Dispose is called, we suppress the finalizer.
#if DEBUG
            if (this.leakDetector != null)
            {
                GC.SuppressFinalize(this.leakDetector);
            }
#endif
        }

#if DEBUG
        /// <summary>
        /// (In Debug mode, we make it a class so that we can add a finalizer in order to detect when the object is not freed.)
        /// </summary>
        private class Sentinel
        {
            /// <summary>
            /// Finalizes an instance of the <see cref="Sentinel" /> class. Runs Debug.Fail if we failed to call a dispose and leave a monitor
            /// </summary>
            ~Sentinel()
            {
                // If this finalizer runs, someone somewhere failed to call Dispose, which means we've failed to leave a monitor!
                Debug.Fail("Undisposed lock");
            }
        }
#endif
    }

    /// <summary>
    /// Exception indicating a lock has been held for too long
    /// </summary>
    internal class LockTimeoutException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LockTimeoutException" /> class
        /// </summary>
        public LockTimeoutException() : base("Timeout waiting for lock")
        {
        }
    }
    //// End class
}
//// End namespace