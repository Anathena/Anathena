using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    class ObjectCache<T>
    {
        protected const Int32 DefaultCacheSize = 256;

        protected readonly Dictionary<Int32, T> Cache;
        protected readonly LinkedList<Int32> LRUQueue;
        protected readonly ImageList Images;
        protected readonly Object AccessLock;
        protected readonly Int32 CacheSize;

        public ObjectCache(Int32 CacheSize = DefaultCacheSize)
        {
            this.CacheSize = CacheSize;

            Cache = new Dictionary<Int32, T>(CacheSize);
            LRUQueue = new LinkedList<Int32>();
            AccessLock = new Object();
            Images = new ImageList();
        }

        public virtual T Add(Int32 Index, T Item)
        {
            lock (AccessLock)
            {
                if (Cache.Count == CacheSize)
                {
                    // Cache full, enforce LRU policy
                    Cache.Remove(LRUQueue.First.Value);
                    LRUQueue.RemoveFirst();
                }

                LRUQueue.AddLast(Index);
                Cache[Index] = Item;
                return Cache[Index];
            }
        }

        public void Delete(Int32 Index)
        {
            lock (AccessLock)
            {
                if (Cache.ContainsKey(Index))
                    Cache.Remove(Index);
                if (LRUQueue.Contains(Index))
                    LRUQueue.Remove(Index);
            }
        }

        public T Get(Int32 Index)
        {
            lock (AccessLock)
            {
                T Item;
                if (Cache.TryGetValue(Index, out Item))
                {
                    LRUQueue.Remove(Index);
                    LRUQueue.AddLast(Index);
                }
                else
                {
                    throw new Exception("Cache does not contain value");
                }
                return Item;
            }
        }

        public void FlushCache()
        {
            lock (AccessLock)
            {
                Cache.Clear();
                LRUQueue.Clear();
            }
        }

    } // End class

} // End namespace