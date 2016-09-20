using System;
using System.Collections.Generic;

namespace Ana.Source.Utils.DataStructures
{
    class ObjectCache<T>
    {
        protected const Int32 DefaultCacheSize = 1024;

        protected readonly Dictionary<UInt64, T> Cache;
        protected readonly LinkedList<UInt64> LRUList;
        protected readonly Object AccessLock;

        public Int32 CacheSize { get; private set; }

        public ObjectCache(Int32 CacheSize = DefaultCacheSize)
        {
            this.CacheSize = CacheSize;

            Cache = new Dictionary<UInt64, T>(CacheSize);
            LRUList = new LinkedList<UInt64>();
            AccessLock = new Object();
        }

        public Boolean TryUpdateItem(UInt64 Index, T Item)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (!Cache.ContainsKey(Index))
                    return false;

                Cache[Index] = Item;
                return true;
            }
        }

        public virtual T Add(UInt64 Index, T Item)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (Cache.Count == CacheSize)
                {
                    // Cache full, enforce LRU policy
                    Cache.Remove(LRUList.First.Value);
                    LRUList.RemoveFirst();
                }

                LRUList.AddLast(Index);
                Cache[Index] = Item;
                return Cache[Index];
            }
        }

        public virtual void Delete(UInt64 Index)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (Cache.ContainsKey(Index))
                    Cache.Remove(Index);
                if (LRUList.Contains(Index))
                    LRUList.Remove(Index);
            }
        }

        public T Get(UInt64 Index)
        {
            using (TimedLock.Lock(AccessLock))
            {
                T Item;
                if (Cache.TryGetValue(Index, out Item))
                {
                    LRUList.Remove(Index);
                    LRUList.AddLast(Index);
                }
                else
                {
                    return (T)Convert.ChangeType(0, typeof(T));
                }
                return Item;
            }
        }

        public virtual void FlushCache()
        {
            using (TimedLock.Lock(AccessLock))
            {
                Cache.Clear();
                LRUList.Clear();
            }
        }

    } // End class

} // End namespace