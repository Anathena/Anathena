using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anathena.Source.Utils.Caches
{
    [Serializable]
    public class BiDictionary<TFirst, TSecond> : IDictionary<TFirst, TSecond>, IReadOnlyDictionary<TFirst, TSecond>, IDictionary
    {
        private readonly IDictionary<TFirst, TSecond> FirstToSecond = new Dictionary<TFirst, TSecond>();
        [NonSerialized]
        private readonly IDictionary<TSecond, TFirst> SecondToFirst = new Dictionary<TSecond, TFirst>();
        [NonSerialized]
        private readonly ReverseDictionary ReversedDictionary;

        public BiDictionary()
        {
            ReversedDictionary = new ReverseDictionary(this);
        }

        public IDictionary<TSecond, TFirst> Reverse
        {
            get { return ReversedDictionary; }
        }

        public Int32 Count
        {
            get { return FirstToSecond.Count; }
        }

        Object ICollection.SyncRoot
        {
            get { return ((ICollection)FirstToSecond).SyncRoot; }
        }

        Boolean ICollection.IsSynchronized
        {
            get { return ((ICollection)FirstToSecond).IsSynchronized; }
        }

        Boolean IDictionary.IsFixedSize
        {
            get { return ((IDictionary)FirstToSecond).IsFixedSize; }
        }

        public Boolean IsReadOnly
        {
            get { return FirstToSecond.IsReadOnly || SecondToFirst.IsReadOnly; }
        }

        public TSecond this[TFirst Key]
        {
            get { return FirstToSecond[Key]; }
            set
            {
                FirstToSecond[Key] = value;
                SecondToFirst[value] = Key;
            }
        }

        Object IDictionary.this[Object Key]
        {
            get { return ((IDictionary)FirstToSecond)[Key]; }
            set
            {
                ((IDictionary)FirstToSecond)[Key] = value;
                ((IDictionary)SecondToFirst)[value] = Key;
            }
        }

        public ICollection<TFirst> Keys
        {
            get { return FirstToSecond.Keys; }
        }

        ICollection IDictionary.Keys
        {
            get { return ((IDictionary)FirstToSecond).Keys; }
        }

        IEnumerable<TFirst> IReadOnlyDictionary<TFirst, TSecond>.Keys
        {
            get { return ((IReadOnlyDictionary<TFirst, TSecond>)FirstToSecond).Keys; }
        }

        public ICollection<TSecond> Values
        {
            get { return FirstToSecond.Values; }
        }

        ICollection IDictionary.Values
        {
            get { return ((IDictionary)FirstToSecond).Values; }
        }

        IEnumerable<TSecond> IReadOnlyDictionary<TFirst, TSecond>.Values
        {
            get { return ((IReadOnlyDictionary<TFirst, TSecond>)FirstToSecond).Values; }
        }

        public IEnumerator<KeyValuePair<TFirst, TSecond>> GetEnumerator()
        {
            return FirstToSecond.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)FirstToSecond).GetEnumerator();
        }

        public void Add(TFirst Key, TSecond Value)
        {
            FirstToSecond.Add(Key, Value);
            SecondToFirst.Add(Value, Key);
        }

        void IDictionary.Add(Object Key, Object Value)
        {
            ((IDictionary)FirstToSecond).Add(Key, Value);
            ((IDictionary)SecondToFirst).Add(Value, Key);
        }

        void ICollection<KeyValuePair<TFirst, TSecond>>.Add(KeyValuePair<TFirst, TSecond> Item)
        {
            FirstToSecond.Add(Item);
            SecondToFirst.Add(new KeyValuePair<TSecond, TFirst>(Item.Value, Item.Key));
        }

        public Boolean ContainsKey(TFirst Key)
        {
            return FirstToSecond.ContainsKey(Key);
        }

        Boolean ICollection<KeyValuePair<TFirst, TSecond>>.Contains(KeyValuePair<TFirst, TSecond> Item)
        {
            return FirstToSecond.Contains(Item);
        }

        public Boolean TryGetValue(TFirst Key, out TSecond Value)
        {
            return FirstToSecond.TryGetValue(Key, out Value);
        }

        public Boolean Remove(TFirst Key)
        {
            TSecond Value;
            if (FirstToSecond.TryGetValue(Key, out Value))
            {
                FirstToSecond.Remove(Key);
                SecondToFirst.Remove(Value);
                return true;
            }

            return false;
        }

        void IDictionary.Remove(Object Key)
        {
            IDictionary FirstToSecond = (IDictionary)this.FirstToSecond;

            if (!FirstToSecond.Contains(Key))
                return;

            Object Value = FirstToSecond[Key];
            FirstToSecond.Remove(Key);
            ((IDictionary)SecondToFirst).Remove(Value);
        }

        Boolean ICollection<KeyValuePair<TFirst, TSecond>>.Remove(KeyValuePair<TFirst, TSecond> Item)
        {
            return FirstToSecond.Remove(Item);
        }

        Boolean IDictionary.Contains(Object Key)
        {
            return ((IDictionary)FirstToSecond).Contains(Key);
        }

        public void Clear()
        {
            FirstToSecond.Clear();
            SecondToFirst.Clear();
        }

        void ICollection<KeyValuePair<TFirst, TSecond>>.CopyTo(KeyValuePair<TFirst, TSecond>[] Array, Int32 ArrayIndex)
        {
            FirstToSecond.CopyTo(Array, ArrayIndex);
        }

        void ICollection.CopyTo(Array Array, Int32 Index)
        {
            ((IDictionary)FirstToSecond).CopyTo(Array, Index);
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext Context)
        {
            SecondToFirst.Clear();

            foreach (KeyValuePair<TFirst, TSecond> Item in FirstToSecond)
                SecondToFirst.Add(Item.Value, Item.Key);
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
                get { return Owner.SecondToFirst.Count; }
            }

            Object ICollection.SyncRoot
            {
                get { return ((ICollection)Owner.SecondToFirst).SyncRoot; }
            }

            Boolean ICollection.IsSynchronized
            {
                get { return ((ICollection)Owner.SecondToFirst).IsSynchronized; }
            }

            Boolean IDictionary.IsFixedSize
            {
                get { return ((IDictionary)Owner.SecondToFirst).IsFixedSize; }
            }

            public Boolean IsReadOnly
            {
                get { return Owner.SecondToFirst.IsReadOnly || Owner.FirstToSecond.IsReadOnly; }
            }

            public TFirst this[TSecond Key]
            {
                get { return Owner.SecondToFirst[Key]; }
                set
                {
                    Owner.SecondToFirst[Key] = value;
                    Owner.FirstToSecond[value] = Key;
                }
            }

            Object IDictionary.this[Object Key]
            {
                get { return ((IDictionary)Owner.SecondToFirst)[Key]; }
                set
                {
                    ((IDictionary)Owner.SecondToFirst)[Key] = value;
                    ((IDictionary)Owner.FirstToSecond)[value] = Key;
                }
            }

            public ICollection<TSecond> Keys
            {
                get { return Owner.SecondToFirst.Keys; }
            }

            ICollection IDictionary.Keys
            {
                get { return ((IDictionary)Owner.SecondToFirst).Keys; }
            }

            IEnumerable<TSecond> IReadOnlyDictionary<TSecond, TFirst>.Keys
            {
                get { return ((IReadOnlyDictionary<TSecond, TFirst>)Owner.SecondToFirst).Keys; }
            }

            public ICollection<TFirst> Values
            {
                get { return Owner.SecondToFirst.Values; }
            }

            ICollection IDictionary.Values
            {
                get { return ((IDictionary)Owner.SecondToFirst).Values; }
            }

            IEnumerable<TFirst> IReadOnlyDictionary<TSecond, TFirst>.Values
            {
                get { return ((IReadOnlyDictionary<TSecond, TFirst>)Owner.SecondToFirst).Values; }
            }

            public IEnumerator<KeyValuePair<TSecond, TFirst>> GetEnumerator()
            {
                return Owner.SecondToFirst.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            IDictionaryEnumerator IDictionary.GetEnumerator()
            {
                return ((IDictionary)Owner.SecondToFirst).GetEnumerator();
            }

            public void Add(TSecond Key, TFirst Value)
            {
                Owner.SecondToFirst.Add(Key, Value);
                Owner.FirstToSecond.Add(Value, Key);
            }

            void IDictionary.Add(Object Key, Object Value)
            {
                ((IDictionary)Owner.SecondToFirst).Add(Key, Value);
                ((IDictionary)Owner.FirstToSecond).Add(Value, Key);
            }

            void ICollection<KeyValuePair<TSecond, TFirst>>.Add(KeyValuePair<TSecond, TFirst> Item)
            {
                Owner.SecondToFirst.Add(Item);
                Owner.FirstToSecond.Add(new KeyValuePair<TFirst, TSecond>(Item.Value, Item.Key));
            }

            public Boolean ContainsKey(TSecond Key)
            {
                return Owner.SecondToFirst.ContainsKey(Key);
            }

            Boolean ICollection<KeyValuePair<TSecond, TFirst>>.Contains(KeyValuePair<TSecond, TFirst> Item)
            {
                return Owner.SecondToFirst.Contains(Item);
            }

            public Boolean TryGetValue(TSecond Key, out TFirst Value)
            {
                return Owner.SecondToFirst.TryGetValue(Key, out Value);
            }

            public Boolean Remove(TSecond Key)
            {
                TFirst Value;
                if (Owner.SecondToFirst.TryGetValue(Key, out Value))
                {
                    Owner.SecondToFirst.Remove(Key);
                    Owner.FirstToSecond.Remove(Value);
                    return true;
                }

                return false;
            }

            void IDictionary.Remove(Object Key)
            {
                IDictionary FirstToSecond = (IDictionary)Owner.SecondToFirst;

                if (!FirstToSecond.Contains(Key))
                    return;

                Object Value = FirstToSecond[Key];
                FirstToSecond.Remove(Key);
                ((IDictionary)Owner.FirstToSecond).Remove(Value);
            }

            Boolean ICollection<KeyValuePair<TSecond, TFirst>>.Remove(KeyValuePair<TSecond, TFirst> Item)
            {
                return Owner.SecondToFirst.Remove(Item);
            }

            Boolean IDictionary.Contains(Object Key)
            {
                return ((IDictionary)Owner.SecondToFirst).Contains(Key);
            }

            public void Clear()
            {
                Owner.SecondToFirst.Clear();
                Owner.FirstToSecond.Clear();
            }

            void ICollection<KeyValuePair<TSecond, TFirst>>.CopyTo(KeyValuePair<TSecond, TFirst>[] Array, Int32 ArrayIndex)
            {
                Owner.SecondToFirst.CopyTo(Array, ArrayIndex);
            }

            void ICollection.CopyTo(Array Array, Int32 Index)
            {
                ((IDictionary)Owner.SecondToFirst).CopyTo(Array, Index);
            }
        }

    } // End class

} // End namespace