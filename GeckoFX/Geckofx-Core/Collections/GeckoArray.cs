using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.Collections
{
	/// <summary>
	/// Internal wrapper realization
	/// </summary>
	/// <typeparam name="TWrapper"></typeparam>
	/// <typeparam name="TGeckoObject"></typeparam>
	internal sealed class GeckoArray<TWrapper,TGeckoObject>
		: IGeckoArray<TWrapper>
		where TGeckoObject :class
		where TWrapper :class
	{
		private nsIArray _array;
		private Func<TGeckoObject, TWrapper> _translator;
		internal GeckoArray(nsIArray array, Func<TGeckoObject, TWrapper> translator)
		{
			_array = array;
			_translator = translator;
		}
		public int Length { get { return (int)_array.GetLengthAttribute(); } }

		public TWrapper this[ int index ]
		{
			get
			{
				var obj = _array.GetElementAs<TGeckoObject>( index );
				if (obj == null) return null;
				var ret = _translator( obj );
				return ret;
			}
		}

		public IEnumerator<TWrapper> GetEnumerator()
		{
			return new GeckoEnumerator<TWrapper, TGeckoObject>( _array.Enumerate(), _translator );
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	/// <summary>
	/// Universal external interface to Gecko Array and dom NodeMaps
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IGeckoArray<T>
		:IEnumerable<T>
	{
		T this[ int index ] { get; }
		int Length { get; }
	}
}
