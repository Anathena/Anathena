using System;

namespace Gecko.Events
{
	/// <summary>
	/// NOT TESTED YET :)
	/// 
	/// Based on System.ComponentModel.EventHandlerList but:
	/// 1. use integer as key
	/// 2. use binary search for finding delegate
	/// 3. use array for store entries
	/// 4. special method for fast initialize
	/// Why:
	/// 1. adding new delegates operations are not frequent
	/// 2. call/combine operation must perform fast
	/// result:
	/// 1. log(N) searching -> fast call and combine
	/// </summary>
	public sealed class GeckoEventList
	{
		private HashTableEntry[] _entries;
		private int _count = 0;


		public GeckoEventList()
		{
			_entries = new HashTableEntry[10];
		}


		public Delegate this[int key]
		{
			get
			{
				HashTableEntry e = null;
				e = BinarySearch(key);
				return e != null ? e.handler : null;
			}
			set
			{
				HashTableEntry e = null;
				int nearest;
				e = BinarySearch( key, out nearest );
				if ( e != null )
				{
					e.handler = value;
				}
				else
				{
					InsertDelegate( key, nearest, value );
				}
			}
		}


		public void AddHandler( int key, Delegate handler )
		{
			HashTableEntry e = null;
			int nearest;
			e = BinarySearch(key, out nearest);
			if (e != null)
			{
				e.handler = Delegate.Combine( e.handler, handler );
			}
			else
			{
				InsertDelegate(key, nearest, handler);
			}
		}

		public void RemoveHandler( int key, Delegate handler )
		{
			HashTableEntry e = null;
			e = BinarySearch( key );
			if ( e != null )
			{
				e.handler = Delegate.Remove( e.handler, handler );
			}
		}

		private void InsertDelegate( int key, int afterPosition, Delegate handler )
		{
			if ( ( _count + 1 ) == _entries.Length )
			{
				Array.Resize( ref _entries, _entries.Length*2 );
			}
			for ( int i = _count - 1; i > afterPosition; i-- )
			{
				_entries[ i + 1 ] = _entries[ i ];
			}
			_entries[afterPosition + 1] = new HashTableEntry(key, handler);
		}

		private HashTableEntry BinarySearch( int key )
		{
			int start = 0;
			int end = _count - 1;
			HashTableEntry currentEntry = null;
			while ( start!=end )
			{
				int middle=( start + end )/2;
				currentEntry = _entries[ middle ];
				var testKey = currentEntry.key;
				if ( testKey == key ) return currentEntry;
				if ( testKey > key )
				{
					// search in interval [start;middle-1]
					end = middle - 1;
				}
				else
				{
					// search in interval [middle+1,_count-1)
					start = middle + 1;
				}
			}
			currentEntry = _entries[ start ];
			return currentEntry.key == key ? currentEntry : null;
		}

		private HashTableEntry BinarySearch(int key,out int nearest)
		{
			int start = 0;
			int end = _count - 1;
			HashTableEntry currentEntry = null;
			while (start != end)
			{
				int middle = (start + end) / 2;
				currentEntry = _entries[middle];
				var testKey = currentEntry.key;
				if ( testKey == key )
				{
					nearest = middle;
					return currentEntry;
				}
				if (testKey > key)
				{
					// search in interval [start;middle-1]
					end = middle - 1;
				}
				else
				{
					// search in interval [middle+1,_count-1)
					start = middle + 1;
				}
			}
			currentEntry = _entries[start];
			nearest = start;
			return currentEntry.key == key ? currentEntry : null;
		}

		/// <summary>
		/// Fast initialize
		/// </summary>
		/// <param name="keys"></param>
		/// <param name="handlers"></param>
		internal void Initialize( int[] keys, Delegate[] handlers )
		{
			HashTableEntry[] table = new HashTableEntry[keys.Length];
			for ( int i = 0; i < keys.Length; i++ )
			{
				table[ i ] = new HashTableEntry( keys[ i ], handlers[ i ] );
			}
			Array.Sort( table, ( x, y ) => x.key.CompareTo( y.key ) );

			_entries = table;
			_count = _entries.Length;
		}


		internal sealed class HashTableEntry
		{
			internal int key;
			internal Delegate handler;
			public HashTableEntry(int key, Delegate handler)
			{
				this.key = key;
				this.handler = handler;
			}
		}
	}
}