using Anathema.Source.Utils;
using System;
using System.Windows.Forms;

namespace Anathema.Utils.Cache
{
    class ListViewCache : ObjectCache<ListViewItem>
    {
        private const Int32 DefaultListViewCacheSize = 256;

        public ListViewCache(Int32 CacheSize = DefaultListViewCacheSize) : base(DefaultListViewCacheSize) { }

        public Boolean TryUpdateSubItem(Int32 Index, Int32 SubItemIndex, String Item)
        {
            using (TimedLock.Lock(AccessLock))
            // lock (AccessLock)
            {
                if (!Cache.ContainsKey((UInt64)Index))
                    return false;

                Cache[(UInt64)Index].SubItems[SubItemIndex].Text = Item;
                return true;
            }
        }

        public ListViewItem Add(Int32 Index, String[] Items)
        {
            using (TimedLock.Lock(AccessLock))
            // lock (AccessLock)
            {
                if (Cache.Count == CacheSize)
                {
                    // Cache full, enforce LRU policy
                    Cache.Remove(LRUList.First.Value);
                    LRUList.RemoveFirst();
                }

                LRUList.AddLast((UInt64)Index);
                Cache[(UInt64)Index] = new ListViewItem(Items);

                return Cache[(UInt64)Index];
            }
        }

        public new ListViewItem Get(UInt64 Index)
        {
            using (TimedLock.Lock(AccessLock))
            // lock (AccessLock)
            {
                ListViewItem Item = null;
                if (Cache.TryGetValue((UInt64)Index, out Item))
                {
                    LRUList.Remove((UInt64)Index);
                    LRUList.AddLast((UInt64)Index);
                }
                return Item;
            }
        }

    } // End class

} // End namespace