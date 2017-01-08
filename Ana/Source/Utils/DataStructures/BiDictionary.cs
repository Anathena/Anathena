namespace Ana.Source.Utils.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// A bidirectional dictionary supporting a mapping of two types.
    /// </summary>
    /// <typeparam name="TFirst">The first type.</typeparam>
    /// <typeparam name="TSecond">The second type.</typeparam>
    [Serializable]
    internal class BiDictionary<TFirst, TSecond> : IDictionary<TFirst, TSecond>, IReadOnlyDictionary<TFirst, TSecond>, IDictionary
    {
        private readonly IDictionary<TFirst, TSecond> firstToSecond;

        [NonSerialized]
        private readonly IDictionary<TSecond, TFirst> secondToFirst;

        [NonSerialized]
        private readonly ReverseDictionary reversedDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="BiDictionary{TFirst,TSecond}" /> class.
        /// </summary>
        public BiDictionary()
        {
            this.reversedDictionary = new ReverseDictionary(this);
            this.firstToSecond = new Dictionary<TFirst, TSecond>();
            this.secondToFirst = new Dictionary<TSecond, TFirst>();
        }

        public IDictionary<TSecond, TFirst> Reverse
        {
            get { return this.reversedDictionary; }
        }

        public Int32 Count
        {
            get { return this.firstToSecond.Count; }
        }

        Object ICollection.SyncRoot
        {
            get { return ((ICollection)this.firstToSecond).SyncRoot; }
        }

        Boolean ICollection.IsSynchronized
        {
            get { return ((ICollection)this.firstToSecond).IsSynchronized; }
        }

        Boolean IDictionary.IsFixedSize
        {
            get { return ((IDictionary)this.firstToSecond).IsFixedSize; }
        }

        public Boolean IsReadOnly
        {
            get { return this.firstToSecond.IsReadOnly || this.secondToFirst.IsReadOnly; }
        }

        public ICollection<TFirst> Keys
        {
            get { return this.firstToSecond.Keys; }
        }

        ICollection IDictionary.Keys
        {
            get { return ((IDictionary)this.firstToSecond).Keys; }
        }

        IEnumerable<TFirst> IReadOnlyDictionary<TFirst, TSecond>.Keys
        {
            get { return ((IReadOnlyDictionary<TFirst, TSecond>)this.firstToSecond).Keys; }
        }

        public ICollection<TSecond> Values
        {
            get { return this.firstToSecond.Values; }
        }

        ICollection IDictionary.Values
        {
            get { return ((IDictionary)this.firstToSecond).Values; }
        }

        IEnumerable<TSecond> IReadOnlyDictionary<TFirst, TSecond>.Values
        {
            get { return ((IReadOnlyDictionary<TFirst, TSecond>)this.firstToSecond).Values; }
        }

        public TSecond this[TFirst key]
        {
            get
            {
                return this.firstToSecond[key];
            }

            set
            {
                this.firstToSecond[key] = value;
                this.secondToFirst[value] = key;
            }
        }

        Object IDictionary.this[Object key]
        {
            get
            {
                return ((IDictionary)this.firstToSecond)[key];
            }

            set
            {
                ((IDictionary)this.firstToSecond)[key] = value;
                ((IDictionary)this.secondToFirst)[value] = key;
            }
        }

        public IEnumerator<KeyValuePair<TFirst, TSecond>> GetEnumerator()
        {
            return this.firstToSecond.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)this.firstToSecond).GetEnumerator();
        }

        public void Add(TFirst key, TSecond value)
        {
            this.firstToSecond.Add(key, value);
            this.secondToFirst.Add(value, key);
        }

        void IDictionary.Add(Object key, Object value)
        {
            ((IDictionary)this.firstToSecond).Add(key, value);
            ((IDictionary)this.secondToFirst).Add(value, key);
        }

        void ICollection<KeyValuePair<TFirst, TSecond>>.Add(KeyValuePair<TFirst, TSecond> item)
        {
            this.firstToSecond.Add(item);
            this.secondToFirst.Add(new KeyValuePair<TSecond, TFirst>(item.Value, item.Key));
        }

        public Boolean ContainsKey(TFirst key)
        {
            return this.firstToSecond.ContainsKey(key);
        }

        Boolean ICollection<KeyValuePair<TFirst, TSecond>>.Contains(KeyValuePair<TFirst, TSecond> item)
        {
            return this.firstToSecond.Contains(item);
        }

        public Boolean TryGetValue(TFirst key, out TSecond value)
        {
            return this.firstToSecond.TryGetValue(key, out value);
        }

        public Boolean Remove(TFirst key)
        {
            TSecond value;

            if (this.firstToSecond.TryGetValue(key, out value))
            {
                this.firstToSecond.Remove(key);
                this.secondToFirst.Remove(value);
                return true;
            }

            return false;
        }

        void IDictionary.Remove(Object key)
        {
            IDictionary firstToSecond = (IDictionary)this.firstToSecond;

            if (!firstToSecond.Contains(key))
            {
                return;
            }

            Object value = firstToSecond[key];
            firstToSecond.Remove(key);
            ((IDictionary)this.secondToFirst).Remove(value);
        }

        Boolean ICollection<KeyValuePair<TFirst, TSecond>>.Remove(KeyValuePair<TFirst, TSecond> item)
        {
            return this.firstToSecond.Remove(item);
        }

        Boolean IDictionary.Contains(Object key)
        {
            return ((IDictionary)this.firstToSecond).Contains(key);
        }

        public void Clear()
        {
            this.firstToSecond.Clear();
            this.secondToFirst.Clear();
        }

        void ICollection<KeyValuePair<TFirst, TSecond>>.CopyTo(KeyValuePair<TFirst, TSecond>[] array, Int32 arrayIndex)
        {
            this.firstToSecond.CopyTo(array, arrayIndex);
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            ((IDictionary)this.firstToSecond).CopyTo(array, index);
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            this.secondToFirst.Clear();

            foreach (KeyValuePair<TFirst, TSecond> item in this.firstToSecond)
            {
                this.secondToFirst.Add(item.Value, item.Key);
            }
        }

        private class ReverseDictionary : IDictionary<TSecond, TFirst>, IReadOnlyDictionary<TSecond, TFirst>, IDictionary
        {
            private readonly BiDictionary<TFirst, TSecond> owner;

            public ReverseDictionary(BiDictionary<TFirst, TSecond> owner)
            {
                this.owner = owner;
            }

            public Int32 Count
            {
                get { return this.owner.secondToFirst.Count; }
            }

            Object ICollection.SyncRoot
            {
                get { return ((ICollection)this.owner.secondToFirst).SyncRoot; }
            }

            Boolean ICollection.IsSynchronized
            {
                get { return ((ICollection)this.owner.secondToFirst).IsSynchronized; }
            }

            Boolean IDictionary.IsFixedSize
            {
                get { return ((IDictionary)this.owner.secondToFirst).IsFixedSize; }
            }

            public Boolean IsReadOnly
            {
                get { return this.owner.secondToFirst.IsReadOnly || this.owner.firstToSecond.IsReadOnly; }
            }

            public ICollection<TSecond> Keys
            {
                get { return this.owner.secondToFirst.Keys; }
            }

            ICollection IDictionary.Keys
            {
                get { return ((IDictionary)this.owner.secondToFirst).Keys; }
            }

            IEnumerable<TSecond> IReadOnlyDictionary<TSecond, TFirst>.Keys
            {
                get { return ((IReadOnlyDictionary<TSecond, TFirst>)this.owner.secondToFirst).Keys; }
            }

            public ICollection<TFirst> Values
            {
                get { return this.owner.secondToFirst.Values; }
            }

            ICollection IDictionary.Values
            {
                get { return ((IDictionary)this.owner.secondToFirst).Values; }
            }

            IEnumerable<TFirst> IReadOnlyDictionary<TSecond, TFirst>.Values
            {
                get { return ((IReadOnlyDictionary<TSecond, TFirst>)this.owner.secondToFirst).Values; }
            }

            public TFirst this[TSecond key]
            {
                get
                {
                    return this.owner.secondToFirst[key];
                }

                set
                {
                    this.owner.secondToFirst[key] = value;
                    this.owner.firstToSecond[value] = key;
                }
            }

            Object IDictionary.this[Object key]
            {
                get
                {
                    return ((IDictionary)this.owner.secondToFirst)[key];
                }

                set
                {
                    ((IDictionary)this.owner.secondToFirst)[key] = value;
                    ((IDictionary)this.owner.firstToSecond)[value] = key;
                }
            }

            public IEnumerator<KeyValuePair<TSecond, TFirst>> GetEnumerator()
            {
                return this.owner.secondToFirst.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            IDictionaryEnumerator IDictionary.GetEnumerator()
            {
                return ((IDictionary)this.owner.secondToFirst).GetEnumerator();
            }

            public void Add(TSecond key, TFirst value)
            {
                this.owner.secondToFirst.Add(key, value);
                this.owner.firstToSecond.Add(value, key);
            }

            void IDictionary.Add(Object key, Object value)
            {
                ((IDictionary)this.owner.secondToFirst).Add(key, value);
                ((IDictionary)this.owner.firstToSecond).Add(value, key);
            }

            void ICollection<KeyValuePair<TSecond, TFirst>>.Add(KeyValuePair<TSecond, TFirst> item)
            {
                this.owner.secondToFirst.Add(item);
                this.owner.firstToSecond.Add(new KeyValuePair<TFirst, TSecond>(item.Value, item.Key));
            }

            public Boolean ContainsKey(TSecond key)
            {
                return this.owner.secondToFirst.ContainsKey(key);
            }

            Boolean ICollection<KeyValuePair<TSecond, TFirst>>.Contains(KeyValuePair<TSecond, TFirst> item)
            {
                return this.owner.secondToFirst.Contains(item);
            }

            public Boolean TryGetValue(TSecond key, out TFirst value)
            {
                return this.owner.secondToFirst.TryGetValue(key, out value);
            }

            public Boolean Remove(TSecond key)
            {
                TFirst value;
                if (this.owner.secondToFirst.TryGetValue(key, out value))
                {
                    this.owner.secondToFirst.Remove(key);
                    this.owner.firstToSecond.Remove(value);
                    return true;
                }

                return false;
            }

            void IDictionary.Remove(Object key)
            {
                IDictionary firstToSecond = (IDictionary)this.owner.secondToFirst;

                if (!firstToSecond.Contains(key))
                {
                    return;
                }

                Object value = firstToSecond[key];
                firstToSecond.Remove(key);
                ((IDictionary)this.owner.firstToSecond).Remove(value);
            }

            Boolean ICollection<KeyValuePair<TSecond, TFirst>>.Remove(KeyValuePair<TSecond, TFirst> item)
            {
                return this.owner.secondToFirst.Remove(item);
            }

            Boolean IDictionary.Contains(Object key)
            {
                return ((IDictionary)this.owner.secondToFirst).Contains(key);
            }

            public void Clear()
            {
                this.owner.secondToFirst.Clear();
                this.owner.firstToSecond.Clear();
            }

            void ICollection<KeyValuePair<TSecond, TFirst>>.CopyTo(KeyValuePair<TSecond, TFirst>[] array, Int32 arrayIndex)
            {
                this.owner.secondToFirst.CopyTo(array, arrayIndex);
            }

            void ICollection.CopyTo(Array array, Int32 index)
            {
                ((IDictionary)this.owner.secondToFirst).CopyTo(array, index);
            }
        }
    }
    //// End class
}
//// End namespace