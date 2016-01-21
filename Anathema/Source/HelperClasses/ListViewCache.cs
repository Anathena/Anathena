using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    class ListViewCache
    {
        private const Int32 DefaultCacheSize = 256;

        private readonly Dictionary<Int32, ListViewItem> Cache;
        private readonly LinkedList<Int32> LRUQueue;
        private readonly ImageList Images;
        private readonly Object AccessLock;
        private readonly Int32 CacheSize;

        public ListViewCache(Int32 CacheSize = DefaultCacheSize)
        {
            this.CacheSize = CacheSize;

            Cache = new Dictionary<Int32, ListViewItem>(CacheSize);
            LRUQueue = new LinkedList<Int32>();
            AccessLock = new Object();
            Images = new ImageList();
        }

        public Boolean TryUpdateSubItem(Int32 Index, Int32 SubItemIndex, String Item)
        {
            lock (AccessLock)
            {
                if (!Cache.ContainsKey(Index))
                    return false;

                Cache[Index].SubItems[SubItemIndex].Text = Item;
                return true;
            }
        }

        public ListViewItem Add(Int32 Index, String[] Items)
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
                Cache[Index] = new ListViewItem(Items);
                return Cache[Index];
            }
        }

        public void Delete(Int32 Index)
        {
            lock (AccessLock)
            {
                Cache.Remove(Index);
                LRUQueue.Remove(Index);
            }
        }

        public ListViewItem Get(Int32 Index)
        {
            lock (AccessLock)
            {
                ListViewItem Item = null;
                if (Cache.TryGetValue(Index, out Item))
                {
                    LRUQueue.Remove(Index);
                    LRUQueue.AddLast(Index);
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