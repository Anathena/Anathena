namespace Ana.Source.Utils.VirtualItemProvider
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Specialized list implementation that provides data virtualization. The collection is divided up into pages,
    /// and pages are dynamically fetched from the IItemsProvider when required. Stale pages are removed after a
    /// configurable period of time.
    /// Int32ended for use with large collections on a network or disk resource that cannot be instantiated locally
    /// due to memory consumption or fetch latency.
    /// </summary>
    /// <remarks>
    /// The IList implmentation is not fully complete, but should be sufficient for use as read only collection 
    /// data bound to a suitable ItemsControl.
    /// </remarks>
    /// <typeparam name="T">The type contained in the collection</typeparam>
    internal class VirtualizingCollection<T> : IList<T>, IList
    {
        /// <summary>
        /// The size of each page
        /// </summary>
        private readonly Int32 pageSize;

        /// <summary>
        /// The timeout for the page
        /// </summary>
        private readonly Int64 pageTimeout;

        /// <summary>
        /// The items provider
        /// </summary>
        private readonly IItemsProvider<T> itemsProvider;

        /// <summary>
        /// Collection for each page
        /// </summary>
        private readonly Dictionary<Int32, IList<T>> pages;

        /// <summary>
        /// Contains the timestamp of page accesses
        /// </summary>
        private readonly Dictionary<Int32, DateTime> pageTouchTimes;

        /// <summary>
        /// The count of items
        /// </summary>
        private Int32 count = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualizingCollection{T}" /> class
        /// </summary>
        /// <param name="itemsProvider">The items provider</param>
        /// <param name="pageSize">Size of the page</param>
        /// <param name="pageTimeout">The page timeout</param>
        public VirtualizingCollection(IItemsProvider<T> itemsProvider, Int32 pageSize = 100, Int32 pageTimeout = 10000)
        {
            this.itemsProvider = itemsProvider;
            this.pageSize = pageSize;
            this.pageTimeout = pageTimeout;
            this.pages = new Dictionary<Int32, IList<T>>();
            this.pageTouchTimes = new Dictionary<Int32, DateTime>();
        }

        /// <summary>
        /// Gets the items provider
        /// </summary>
        public IItemsProvider<T> ItemsProvider
        {
            get
            {
                return this.itemsProvider;
            }
        }

        /// <summary>
        /// Gets the size of the page
        /// </summary>
        public Int32 PageSize
        {
            get
            {
                return this.pageSize;
            }
        }

        /// <summary>
        /// Gets the page timeout
        /// </summary>
        public Int64 PageTimeout
        {
            get
            {
                return this.pageTimeout;
            }
        }

        /// <summary>
        /// Gets or sets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>
        /// The first time this property is accessed, it will fetch the count from the IItemsProvider
        /// </summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/></returns>
        public virtual Int32 Count
        {
            get
            {
                if (this.count == -1)
                {
                    this.LoadCount();
                }

                return this.count;
            }

            protected set
            {
                this.count = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe)
        /// </summary>
        /// <returns>Always false</returns>
        public Boolean IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only
        /// </summary>
        /// <value></value>
        /// <returns>Always true</returns>
        public Boolean IsReadOnly
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IList"/> has a fixed size
        /// </summary>
        /// <value></value>
        /// <returns>Always false</returns>
        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>
        /// </summary>
        /// <value></value>
        /// <returns>
        /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>
        /// </returns>
        public Object SyncRoot
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Gets the item at the specified index. This property will fetch the
        /// corresponding page from the IItemsProvider if required
        /// </summary>
        /// <param name="index">The index of the item to access</param>
        /// <returns>The item at the specified index</returns>
        public T this[Int32 index]
        {
            get
            {
                // determine which page and offset within page
                Int32 pageIndex = index / this.PageSize;
                Int32 pageOffset = index % this.PageSize;

                // request primary page
                this.RequestPage(pageIndex);

                // if accessing upper 50% then request next page
                if (pageOffset > this.PageSize / 2 && pageIndex < this.Count / this.PageSize)
                {
                    this.RequestPage(pageIndex + 1);
                }

                // if accessing lower 50% then request prev page
                if (pageOffset < this.PageSize / 2 && pageIndex > 0)
                {
                    this.RequestPage(pageIndex - 1);
                }

                // remove stale pages
                this.CleanUpPages();

                // defensive check in case of async load
                if (this.pages[pageIndex] == null)
                {
                    return default(T);
                }

                // return requested item
                return this.pages[pageIndex][pageOffset];
            }

            set
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets the object at the specified index
        /// </summary>
        /// <param name="index">The index of the object</param>
        /// <returns>The object at the specified index</returns>
        Object IList.this[Int32 index]
        {
            get
            {
                return this[index];
            }

            set
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <remarks>
        /// This method should be avoided on large collections due to poor performance.
        /// </remarks>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (Int32 index = 0; index < this.Count; index++)
            {
                yield return this[index];
            }
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/></param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only
        /// </exception>
        public void Add(T item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/></param>
        /// <returns>
        /// Always false
        /// </returns>
        public Boolean Contains(T item)
        {
            return false;
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only
        /// </exception>
        public void Clear()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/></param>
        /// <returns>Always -1</returns>
        public Int32 IndexOf(T item)
        {
            return -1;
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted</param>
        /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1"/></param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only
        /// </exception>
        public void Insert(Int32 index, T item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only
        /// </exception>
        public void RemoveAt(Int32 index)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only
        /// </exception>
        public Boolean Remove(T item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="array"/> is null
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than 0
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="array"/> is multidimensional
        /// -or-
        /// <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>
        /// -or-
        /// The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>
        /// -or-
        /// Type <paramref name="T"/> cannot be cast automatically to the type of the destination <paramref name="array"/>
        /// </exception>
        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Cleans up any stale pages that have not been accessed in the period dictated by PageTimeout
        /// </summary>
        public void CleanUpPages()
        {
            List<Int32> keys = new List<Int32>(this.pageTouchTimes.Keys);

            foreach (Int32 key in keys)
            {
                // page 0 is a special case, since WPF ItemsControl access the first item frequently
                if (key != 0 && (DateTime.Now - this.pageTouchTimes[key]).TotalMilliseconds > this.PageTimeout)
                {
                    this.pages.Remove(key);
                    this.pageTouchTimes.Remove(key);
                    Trace.WriteLine("Removed Page: " + key);
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="value">The parameter value is not used</param>
        /// <returns>Throws an exception</returns>
        Int32 IList.Add(Object value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets a value indicating whether the collection contains the specified object
        /// </summary>
        /// <param name="value">The object to check</param>
        /// <returns>A value indicating whether the collection contains the specified object</returns>
        Boolean IList.Contains(Object value)
        {
            return this.Contains((T)value);
        }

        /// <summary>
        /// Gets the index of the contained object
        /// </summary>
        /// <param name="value">The object in the collection</param>
        /// <returns>The index of the contained object</returns>
        Int32 IList.IndexOf(Object value)
        {
            return this.IndexOf((T)value);
        }

        /// <summary>
        /// Inserts an object into the collection
        /// </summary>
        /// <param name="index">The insertion index</param>
        /// <param name="value">The object to insert</param>
        void IList.Insert(Int32 index, Object value)
        {
            this.Insert(index, (T)value);
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="value">The parameter value is not used</param>
        void IList.Remove(Object value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="array">The parameter array is not used</param>
        /// <param name="index">The parameter index is not used</param>
        void ICollection.CopyTo(Array array, Int32 index)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Populates the page within the dictionary
        /// </summary>
        /// <param name="pageIndex">Index of the page</param>
        /// <param name="page">The page</param>
        protected virtual void PopulatePage(Int32 pageIndex, IList<T> page)
        {
            Trace.WriteLine("Page populated: " + pageIndex);
            if (this.pages.ContainsKey(pageIndex))
            {
                this.pages[pageIndex] = page;
            }
        }

        /// <summary>
        /// Makes a request for the specified page, creating the necessary slots in the dictionary,
        /// and updating the page touch time
        /// </summary>
        /// <param name="pageIndex">Index of the page</param>
        protected virtual void RequestPage(Int32 pageIndex)
        {
            if (!this.pages.ContainsKey(pageIndex))
            {
                this.pages.Add(pageIndex, null);
                this.pageTouchTimes.Add(pageIndex, DateTime.Now);
                Trace.WriteLine("Added page: " + pageIndex);
                this.LoadPage(pageIndex);
            }
            else
            {
                this.pageTouchTimes[pageIndex] = DateTime.Now;
            }
        }

        /// <summary>
        /// Loads the count of items.
        /// </summary>
        protected virtual void LoadCount()
        {
            this.Count = this.GetCount();
        }

        /// <summary>
        /// Loads the page of items
        /// </summary>
        /// <param name="pageIndex">Index of the page</param>
        protected virtual void LoadPage(Int32 pageIndex)
        {
            this.PopulatePage(pageIndex, this.GetPage(pageIndex));
        }

        /// <summary>
        /// Gets the requested page from the IItemsProvider
        /// </summary>
        /// <param name="pageIndex">Index of the page</param>
        /// <returns>The requested page</returns>
        protected IList<T> GetPage(Int32 pageIndex)
        {
            return this.ItemsProvider.GetItems(pageIndex * this.PageSize, this.PageSize);
        }

        /// <summary>
        /// Gets the count of itmes from the IItemsProvider
        /// </summary>
        /// <returns>The count of itmes</returns>
        protected Int32 GetCount()
        {
            return this.ItemsProvider.GetCount();
        }
    }
    //// End class
}
//// End namespace