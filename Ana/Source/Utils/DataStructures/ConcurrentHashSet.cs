using System;

namespace Ana.Source.Utils.DataStructures
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    public class ConcurrentHashSet<T> : IEnumerable, IDisposable
    {
        private readonly ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private readonly HashSet<T> hashSet = new HashSet<T>();

        public Boolean Add(T item)
        {
            lockSlim.EnterWriteLock();

            try
            {
                return hashSet.Add(item);
            }
            finally
            {
                if (lockSlim.IsWriteLockHeld) lockSlim.ExitWriteLock();
            }
        }

        public void Clear()
        {
            lockSlim.EnterWriteLock();

            try
            {
                hashSet.Clear();
            }
            finally
            {
                if (lockSlim.IsWriteLockHeld) lockSlim.ExitWriteLock();
            }
        }

        public Boolean Contains(T item)
        {
            lockSlim.EnterReadLock();

            try
            {
                return hashSet.Contains(item);
            }
            finally
            {
                if (lockSlim.IsReadLockHeld) lockSlim.ExitReadLock();
            }
        }

        public Boolean Remove(T item)
        {
            lockSlim.EnterWriteLock();

            try
            {
                return hashSet.Remove(item);
            }
            finally
            {
                if (lockSlim.IsWriteLockHeld) lockSlim.ExitWriteLock();
            }
        }

        public Int32 Count
        {
            get
            {
                lockSlim.EnterReadLock();

                try
                {
                    return hashSet.Count;
                }
                finally
                {
                    if (lockSlim.IsReadLockHeld) lockSlim.ExitReadLock();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                if (lockSlim != null)
                {
                    lockSlim.Dispose();
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)hashSet).GetEnumerator();
        }

        ~ConcurrentHashSet()
        {
            Dispose(false);
        }

    } // End class

} // End namespace