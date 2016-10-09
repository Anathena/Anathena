namespace Ana.Source.Utils.VirtualItemProvider
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Threading;

    /// <summary>
    /// Derived VirtualizatingCollection, performing loading asychronously
    /// </summary>
    /// <typeparam name="T">The type of items in the collection</typeparam>
    internal class AsyncVirtualizingCollection<T> : VirtualizingCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        /// <summary>
        /// An object to assist in UI multithreading
        /// </summary>
        private readonly SynchronizationContext synchronizationContext;

        /// <summary>
        /// Indicates whether or not the collection is loading
        /// </summary>
        private Boolean isLoading;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncVirtualizingCollection{T}"/> class.
        /// </summary>
        /// <param name="itemsProvider">The items provider</param>
        /// <param name="pageSize">Size of the page</param>
        /// <param name="pageTimeout">The page timeout</param>
        public AsyncVirtualizingCollection(IItemsProvider<T> itemsProvider, int pageSize, int pageTimeout) : base(itemsProvider, pageSize, pageTimeout)
        {
            this.synchronizationContext = SynchronizationContext.Current;
        }

        /// <summary>
        /// Occurs when the collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the collection is loading
        /// </summary>
        /// <value>
        /// <c>true</c> if this collection is loading; otherwise, <c>false</c>
        /// </value>
        public Boolean IsLoading
        {
            get
            {
                return this.isLoading;
            }

            set
            {
                if (value != this.isLoading)
                {
                    this.isLoading = value;
                }

                this.FirePropertyChanged("IsLoading");
            }
        }

        /// <summary>
        /// Gets the synchronization context used for UI-related operations. This is obtained as
        /// the current SynchronizationContext when the AsyncVirtualizingCollection is created
        /// </summary>
        /// <value>The synchronization context</value>
        protected SynchronizationContext SynchronizationContext
        {
            get
            {
                return this.synchronizationContext;
            }
        }

        /// <summary>
        /// Asynchronously loads the count of items
        /// </summary>
        protected override void LoadCount()
        {
            this.Count = 0;
            this.IsLoading = true;
            ThreadPool.QueueUserWorkItem(this.LoadCountWork);
        }

        /// <summary>
        /// Asynchronously loads the page
        /// </summary>
        /// <param name="index">The index.</param>
        protected override void LoadPage(Int32 index)
        {
            this.IsLoading = true;
            ThreadPool.QueueUserWorkItem(this.LoadPageWork, index);
        }

        /// <summary>
        /// Raises the <see cref="E:CollectionChanged"/> event
        /// </summary>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data</param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            this.CollectionChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event
        /// </summary>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Fires the collection reset event
        /// </summary>
        private void FireCollectionReset()
        {
            NotifyCollectionChangedEventArgs e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            this.OnCollectionChanged(e);
        }

        /// <summary>
        /// Fires the property changed event
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        private void FirePropertyChanged(String propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            this.OnPropertyChanged(e);
        }

        /// <summary>
        /// Performed on background thread
        /// </summary>
        /// <param name="args">None required</param>
        private void LoadCountWork(Object args)
        {
            Int32 count = this.GetCount();
            this.SynchronizationContext.Send(this.LoadCountCompleted, count);
        }

        /// <summary>
        /// Performed on UI-thread after LoadCountWork
        /// </summary>
        /// <param name="args">Number of items returned</param>
        private void LoadCountCompleted(Object args)
        {
            this.Count = (Int32)args;
            this.IsLoading = false;
            this.FireCollectionReset();
        }

        /// <summary>
        /// Performed on background thread
        /// </summary>
        /// <param name="args">Index of the page to load</param>
        private void LoadPageWork(Object args)
        {
            Int32 pageIndex = (Int32)args;
            IList<T> page = this.GetPage(pageIndex);
            SynchronizationContext.Send(this.LoadPageCompleted, new Object[] { pageIndex, page });
        }

        /// <summary>
        /// Performed on UI-thread after LoadPageWork
        /// </summary>
        /// <param name="args">object[] { int pageIndex, IList(T) page }</param>
        private void LoadPageCompleted(Object args)
        {
            Int32 pageIndex = (Int32)((Object[])args)[0];
            IList<T> page = (IList<T>)((Object[])args)[1];

            this.PopulatePage(pageIndex, page);
            this.IsLoading = false;
            this.FireCollectionReset();
        }
    }
    //// End class
}
//// End namespace