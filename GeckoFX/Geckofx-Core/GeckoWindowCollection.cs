﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
﻿using Gecko.Interop;

namespace Gecko
{
	public class GeckoWindowCollection : IEnumerable<GeckoWindow>
	{
		private ComPtr<nsIDOMWindowCollection> _collection;

		public GeckoWindowCollection(nsIDOMWindowCollection collection)
		{
			_collection = new ComPtr<nsIDOMWindowCollection>(collection);
		}


		public virtual uint Count
		{
			get { return _collection.Instance.GetLengthAttribute(); }
		}

		[IndexerName("Items")]
		public virtual GeckoWindow this[uint index]
		{
			get
			{
				if (index < 0 || index >= Count)
					throw new ArgumentOutOfRangeException("index");

				return _collection.Instance.Item( index ).Wrap( x => new GeckoWindow( x ) );
			}
		}

		public virtual IEnumerator<GeckoWindow> GetEnumerator()
		{
			uint length = Count;
			for (uint i = 0; i < length; i++)
			{
				yield return _collection.Instance.Item(i).Wrap(x => new GeckoWindow(x));
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (GeckoWindow element in this)
				yield return element;
		}

		public void Dispose()
		{
			Xpcom.DisposeObject(ref _collection);
			GC.SuppressFinalize(this);
		}

		~GeckoWindowCollection()
		{
			Xpcom.DisposeObject(ref _collection);
		}
	}
}