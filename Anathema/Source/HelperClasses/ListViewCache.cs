using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    class ListViewCache : ObjectCache<ListViewItem>
    {
        private const Int32 DefaultListViewCacheSize = 256;

        public ListViewCache(Int32 CacheSize = DefaultListViewCacheSize) : base(DefaultListViewCacheSize) { }

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

        public ListViewItem Add(int Index, String[] Items)
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

        public new ListViewItem Get(Int32 Index)
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

    } // End class

} // End namespace