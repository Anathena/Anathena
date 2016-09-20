using System;
using System.Collections.Generic;

namespace Ana.Source.Utils.DataStructures
{
    class ObjectCache<T>
    {
        protected const Int32 DefaultCacheSize = 1024;

        protected readonly Dictionary<UInt64, T> Cache;
        protected readonly LinkedList<UInt64> LruList;
        protected readonly Object AccessLock;

        public Int32 CacheSize { get; private set; }

        public ObjectCache(Int32 cacheSize = DefaultCacheSize)
        {
            this.CacheSize = cacheSize;

            Cache = new Dictionary<UInt64, T>(cacheSize);
            LruList = new LinkedList<UInt64>();
            AccessLock = new Object();
        }

        public Boolean TryUpdateItem(UInt64 index, T item)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (!Cache.ContainsKey(index))
                    return false;

                Cache[index] = item;
                return true;
            }
        }

        public virtual T Add(UInt64 index, T item)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (Cache.Count == CacheSize)
                {
                    // Cache full, enforce LRU policy
                    Cache.Remove(LruList.First.Value);
                    LruList.RemoveFirst();
                }

                LruList.AddLast(index);
                Cache[index] = item;
                return Cache[index];
            }
        }

        public virtual void Delete(UInt64 index)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (Cache.ContainsKey(index))
                    Cache.Remove(index);
                if (LruList.Contains(index))
                    LruList.Remove(index);
            }
        }

        public T Get(UInt64 index)
        {
            using (TimedLock.Lock(AccessLock))
            {
                T item;
                if (Cache.TryGetValue(index, out item))
                {
                    LruList.Remove(index);
                    LruList.AddLast(index);
                }
                else
                {
                    return (T)Convert.ChangeType(0, typeof(T));
                }
                return item;
            }
        }

        public virtual void FlushCache()
        {
            using (TimedLock.Lock(AccessLock))
            {
                Cache.Clear();
                LruList.Clear();
            }
        }

    }
    //// End class
}
//// End namespace