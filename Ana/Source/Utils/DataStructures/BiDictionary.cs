using System;
using System.Collections;
using System.Collections.Generic;
namespace Ana.Source.Utils.DataStructures
{
    using System.Runtime.Serialization;

    [Serializable]
    public class BiDictionary<TFirst, TSecond> : IDictionary<TFirst, TSecond>, IReadOnlyDictionary<TFirst, TSecond>, IDictionary
    {
        private readonly IDictionary<TFirst, TSecond> firstToSecond = new Dictionary<TFirst, TSecond>();
        [NonSerialized]
        private readonly IDictionary<TSecond, TFirst> secondToFirst = new Dictionary<TSecond, TFirst>();
        [NonSerialized]
        private readonly ReverseDictionary reversedDictionary;

        public BiDictionary()
        {
            reversedDictionary = new ReverseDictionary(this);
        }

        public IDictionary<TSecond, TFirst> Reverse
        {
            get { return reversedDictionary; }
        }

        public Int32 Count
        {
            get
            {
                return firstToSecond.Count;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection)firstToSecond).SyncRoot;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection)firstToSecond).IsSynchronized;
            }
        }

        Boolean IDictionary.IsFixedSize
        {
            get
            {
                return ((IDictionary)firstToSecond).IsFixedSize;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return firstToSecond.IsReadOnly || secondToFirst.IsReadOnly;
            }
        }

        public TSecond this[TFirst Key]
        {
            get
            {
                return firstToSecond[Key];
            }

            set
            {
                firstToSecond[Key] = value;
                secondToFirst[value] = Key;
            }
        }

        Object IDictionary.this[Object Key]
        {
            get
            {
                return ((IDictionary)firstToSecond)[Key];
            }

            set
            {
                ((IDictionary)firstToSecond)[Key] = value;
                ((IDictionary)secondToFirst)[value] = Key;
            }
        }

        public ICollection<TFirst> Keys
        {
            get
            {
                return firstToSecond.Keys;
            }
        }

        ICollection IDictionary.Keys
        {
            get
            {
                return ((IDictionary)firstToSecond).Keys;
            }
        }

        IEnumerable<TFirst> IReadOnlyDictionary<TFirst, TSecond>.Keys
        {
            get
            {
                return ((IReadOnlyDictionary<TFirst, TSecond>)firstToSecond).Keys;
            }
        }

        public ICollection<TSecond> Values
        {
            get
            {
                return firstToSecond.Values;
            }
        }

        ICollection IDictionary.Values
        {
            get
            {
                return ((IDictionary)firstToSecond).Values;
            }
        }

        IEnumerable<TSecond> IReadOnlyDictionary<TFirst, TSecond>.Values
        {
            get
            {
                return ((IReadOnlyDictionary<TFirst, TSecond>)firstToSecond).Values;
            }
        }

        public IEnumerator<KeyValuePair<TFirst, TSecond>> GetEnumerator()
        {
            return firstToSecond.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)firstToSecond).GetEnumerator();
        }

        public void Add(TFirst Key, TSecond Value)
        {
            firstToSecond.Add(Key, Value);
            secondToFirst.Add(Value, Key);
        }

        void IDictionary.Add(Object Key, Object Value)
        {
            ((IDictionary)firstToSecond).Add(Key, Value);
            ((IDictionary)secondToFirst).Add(Value, Key);
        }

        void ICollection<KeyValuePair<TFirst, TSecond>>.Add(KeyValuePair<TFirst, TSecond> Item)
        {
            firstToSecond.Add(Item);
            secondToFirst.Add(new KeyValuePair<TSecond, TFirst>(Item.Value, Item.Key));
        }

        public Boolean ContainsKey(TFirst Key)
        {
            return firstToSecond.ContainsKey(Key);
        }

        Boolean ICollection<KeyValuePair<TFirst, TSecond>>.Contains(KeyValuePair<TFirst, TSecond> Item)
        {
            return firstToSecond.Contains(Item);
        }

        public Boolean TryGetValue(TFirst Key, out TSecond Value)
        {
            return firstToSecond.TryGetValue(Key, out Value);
        }

        public Boolean Remove(TFirst Key)
        {
            TSecond Value;
            if (firstToSecond.TryGetValue(Key, out Value))
            {
                firstToSecond.Remove(Key);
                secondToFirst.Remove(Value);
                return true;
            }

            return false;
        }

        void IDictionary.Remove(Object Key)
        {
            IDictionary FirstToSecond = (IDictionary)this.firstToSecond;

            if (!FirstToSecond.Contains(Key))
                return;

            Object Value = FirstToSecond[Key];
            FirstToSecond.Remove(Key);
            ((IDictionary)secondToFirst).Remove(Value);
        }

        Boolean ICollection<KeyValuePair<TFirst, TSecond>>.Remove(KeyValuePair<TFirst, TSecond> Item)
        {
            return firstToSecond.Remove(Item);
        }

        Boolean IDictionary.Contains(Object Key)
        {
            return ((IDictionary)firstToSecond).Contains(Key);
        }

        public void Clear()
        {
            firstToSecond.Clear();
            secondToFirst.Clear();
        }

        void ICollection<KeyValuePair<TFirst, TSecond>>.CopyTo(KeyValuePair<TFirst, TSecond>[] Array, Int32 ArrayIndex)
        {
            firstToSecond.CopyTo(Array, ArrayIndex);
        }

        void ICollection.CopyTo(Array Array, Int32 Index)
        {
            ((IDictionary)firstToSecond).CopyTo(Array, Index);
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext Context)
        {
            secondToFirst.Clear();

            foreach (KeyValuePair<TFirst, TSecond> Item in firstToSecond)
                secondToFirst.Add(Item.Value, Item.Key);
        }

        private class ReverseDictionary : IDictionary<TSecond, TFirst>, IReadOnlyDictionary<TSecond, TFirst>, IDictionary
        {
            private readonly BiDictionary<TFirst, TSecond> Owner;

            public ReverseDictionary(BiDictionary<TFirst, TSecond> Owner)
            {
                this.Owner = Owner;
            }

            public Int32 Count
            {
                get
                {
                    return Owner.secondToFirst.Count;
                }
            }

            Object ICollection.SyncRoot
            {
                get
                {
                    return ((ICollection)Owner.secondToFirst).SyncRoot;
                }
            }

            Boolean ICollection.IsSynchronized
            {
                get
                {
                    return ((ICollection)Owner.secondToFirst).IsSynchronized;
                }
            }

            Boolean IDictionary.IsFixedSize
            {
                get
                {
                    return ((IDictionary)Owner.secondToFirst).IsFixedSize;
                }
            }

            public Boolean IsReadOnly
            {
                get
                {
                    return Owner.secondToFirst.IsReadOnly || Owner.firstToSecond.IsReadOnly;
                }
            }

            public TFirst this[TSecond Key]
            {
                get
                {
                    return Owner.secondToFirst[Key];
                }

                set
                {
                    Owner.secondToFirst[Key] = value;
                    Owner.firstToSecond[value] = Key;
                }
            }

            Object IDictionary.this[Object Key]
            {
                get
                {
                    return ((IDictionary)Owner.secondToFirst)[Key];
                }

                set
                {
                    ((IDictionary)Owner.secondToFirst)[Key] = value;
                    ((IDictionary)Owner.firstToSecond)[value] = Key;
                }
            }

            public ICollection<TSecond> Keys
            {
                get
                {
                    return Owner.secondToFirst.Keys;
                }
            }

            ICollection IDictionary.Keys
            {
                get
                {
                    return ((IDictionary)Owner.secondToFirst).Keys;
                }
            }

            IEnumerable<TSecond> IReadOnlyDictionary<TSecond, TFirst>.Keys
            {
                get
                {
                    return ((IReadOnlyDictionary<TSecond, TFirst>)Owner.secondToFirst).Keys;
                }
            }

            public ICollection<TFirst> Values
            {
                get
                {
                    return Owner.secondToFirst.Values;
                }
            }

            ICollection IDictionary.Values
            {
                get
                {
                    return ((IDictionary)Owner.secondToFirst).Values;
                }
            }

            IEnumerable<TFirst> IReadOnlyDictionary<TSecond, TFirst>.Values
            {
                get
                {
                    return ((IReadOnlyDictionary<TSecond, TFirst>)Owner.secondToFirst).Values;
                }
            }

            public IEnumerator<KeyValuePair<TSecond, TFirst>> GetEnumerator()
            {
                return Owner.secondToFirst.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            IDictionaryEnumerator IDictionary.GetEnumerator()
            {
                return ((IDictionary)Owner.secondToFirst).GetEnumerator();
            }

            public void Add(TSecond Key, TFirst Value)
            {
                Owner.secondToFirst.Add(Key, Value);
                Owner.firstToSecond.Add(Value, Key);
            }

            void IDictionary.Add(Object Key, Object Value)
            {
                ((IDictionary)Owner.secondToFirst).Add(Key, Value);
                ((IDictionary)Owner.firstToSecond).Add(Value, Key);
            }

            void ICollection<KeyValuePair<TSecond, TFirst>>.Add(KeyValuePair<TSecond, TFirst> Item)
            {
                Owner.secondToFirst.Add(Item);
                Owner.firstToSecond.Add(new KeyValuePair<TFirst, TSecond>(Item.Value, Item.Key));
            }

            public Boolean ContainsKey(TSecond Key)
            {
                return Owner.secondToFirst.ContainsKey(Key);
            }

            Boolean ICollection<KeyValuePair<TSecond, TFirst>>.Contains(KeyValuePair<TSecond, TFirst> Item)
            {
                return Owner.secondToFirst.Contains(Item);
            }

            public Boolean TryGetValue(TSecond Key, out TFirst Value)
            {
                return Owner.secondToFirst.TryGetValue(Key, out Value);
            }

            public Boolean Remove(TSecond Key)
            {
                TFirst Value;
                if (Owner.secondToFirst.TryGetValue(Key, out Value))
                {
                    Owner.secondToFirst.Remove(Key);
                    Owner.firstToSecond.Remove(Value);
                    return true;
                }

                return false;
            }

            void IDictionary.Remove(Object Key)
            {
                IDictionary FirstToSecond = (IDictionary)Owner.secondToFirst;

                if (!FirstToSecond.Contains(Key))
                    return;

                Object Value = FirstToSecond[Key];
                FirstToSecond.Remove(Key);
                ((IDictionary)Owner.firstToSecond).Remove(Value);
            }

            Boolean ICollection<KeyValuePair<TSecond, TFirst>>.Remove(KeyValuePair<TSecond, TFirst> Item)
            {
                return Owner.secondToFirst.Remove(Item);
            }

            Boolean IDictionary.Contains(Object Key)
            {
                return ((IDictionary)Owner.secondToFirst).Contains(Key);
            }

            public void Clear()
            {
                Owner.secondToFirst.Clear();
                Owner.firstToSecond.Clear();
            }

            void ICollection<KeyValuePair<TSecond, TFirst>>.CopyTo(KeyValuePair<TSecond, TFirst>[] Array, Int32 ArrayIndex)
            {
                Owner.secondToFirst.CopyTo(Array, ArrayIndex);
            }

            void ICollection.CopyTo(Array Array, Int32 Index)
            {
                ((IDictionary)Owner.secondToFirst).CopyTo(Array, Index);
            }
        }

    }
    //// End class
}
//// End namespace