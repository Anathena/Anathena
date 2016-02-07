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

        private Dictionary<UInt64, Image> ImageCache;

        public ImageList ImageList { get; private set; }

        public ListViewCache(Int32 CacheSize = DefaultListViewCacheSize) : base(DefaultListViewCacheSize)
        {
            ImageCache = new Dictionary<UInt64, Image>(CacheSize);
        }

        public Boolean TryUpdateSubItem(Int32 Index, Int32 SubItemIndex, String Item)
        {
            lock (AccessLock)
            {
                if (!Cache.ContainsKey((UInt64)Index))
                    return false;

                Cache[(UInt64)Index].SubItems[SubItemIndex].Text = Item;
                return true;
            }
        }

        public ListViewItem Add(Int32 Index, String[] Items, Image Image = null)
        {
            lock (AccessLock)
            {
                if (Cache.Count == CacheSize)
                {
                    // Cache full, enforce LRU policy
                    Cache.Remove(LRUList.First.Value);
                    ImageCache.Remove(LRUList.First.Value);
                    LRUList.RemoveFirst();
                }

                LRUList.AddLast((UInt64)Index);
                Cache[(UInt64)Index] = new ListViewItem(Items);
                ImageCache[(UInt64)Index] = Image;

                UpdateImageList();

                return Cache[(UInt64)Index];
            }
        }

        public override void Delete(UInt64 Index)
        {
            lock (AccessLock)
            {
                if (Cache.ContainsKey(Index))
                    Cache.Remove(Index);
                if (ImageCache.ContainsKey(Index))
                    ImageCache.Remove(Index);
                if (LRUList.Contains(Index))
                    LRUList.Remove(Index);
            }
        }

        public new ListViewItem Get(UInt64 Index)
        {
            lock (AccessLock)
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

        private void UpdateImageList()
        {
            // Create imagelist
            ImageList = new ImageList();

            if (ImageCache.Values.Count < 0)
                return;

            ImageList.Images.AddRange(ImageCache.Values.ToArray());

            // Assign indicies
            Int32 ImageIndex = 0;
            foreach (ListViewItem Item in Cache.Values)
                Item.ImageIndex = ImageIndex++;
        }

        public override void FlushCache()
        {
            lock (AccessLock)
            {
                ImageCache.Clear();
                Cache.Clear();
                LRUList.Clear();
            }
        }

    } // End class

} // End namespace