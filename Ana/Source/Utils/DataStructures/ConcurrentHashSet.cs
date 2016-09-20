using System;

namespace Anathena.Source.Utils.DataStructures
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    public class ConcurrentHashSet<T> : IEnumerable, IDisposable
    {
        private readonly ReaderWriterLockSlim Lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private readonly HashSet<T> HashSet = new HashSet<T>();

        public Boolean Add(T item)
        {
            Lock.EnterWriteLock();
            try
            {
                return HashSet.Add(item);
            }
            finally
            {
                if (Lock.IsWriteLockHeld) Lock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            Lock.EnterWriteLock();
            try
            {
                HashSet.Clear();
            }
            finally
            {
                if (Lock.IsWriteLockHeld) Lock.ExitWriteLock();
            }
        }

        public Boolean Contains(T item)
        {
            Lock.EnterReadLock();
            try
            {
                return HashSet.Contains(item);
            }
            finally
            {
                if (Lock.IsReadLockHeld) Lock.ExitReadLock();
            }
        }

        public Boolean Remove(T item)
        {
            Lock.EnterWriteLock();
            try
            {
                return HashSet.Remove(item);
            }
            finally
            {
                if (Lock.IsWriteLockHeld) Lock.ExitWriteLock();
            }
        }

        public Int32 Count
        {
            get
            {
                Lock.EnterReadLock();
                try
                {
                    return HashSet.Count;
                }
                finally
                {
                    if (Lock.IsReadLockHeld) Lock.ExitReadLock();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean Disposing)
        {
            if (Disposing)
                if (Lock != null)
                    Lock.Dispose();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)HashSet).GetEnumerator();
        }

        ~ConcurrentHashSet()
        {
            Dispose(false);
        }

    } // End class

} // End namespace