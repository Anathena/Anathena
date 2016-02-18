using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Collections
{
	/// <summary>
	/// Generic Gecko enumerable collection
	/// this class don't actualy have any fields with gecko objects (it create them if it is needed)
	/// </summary>
	/// <typeparam name="TWrapper">Wraper type</typeparam>
	/// <typeparam name="TGeckoObject">Gecko object type</typeparam>
	internal sealed class GeckoEnumerableCollection<TWrapper, TGeckoObject>
	: IEnumerable<TWrapper>
	{
		private readonly Func<TGeckoObject, TWrapper> _translator;
		private readonly Func<nsISimpleEnumerator> _enumeratorCreator;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="enumeratorCreator">Function for gecko nsISimpleEnumerator object creation</param>
		/// <param name="translator">Function for wraper creation</param>
		public GeckoEnumerableCollection(Func<nsISimpleEnumerator> enumeratorCreator, Func<TGeckoObject, TWrapper> translator)
		{
			_enumeratorCreator = enumeratorCreator;
			_translator = translator;
		}

		public IEnumerator<TWrapper> GetEnumerator()
		{
			return new GeckoEnumerator<TWrapper, TGeckoObject>(_enumeratorCreator(), _translator);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal sealed class SimpleGeckoEnumerableCollection<T>
		:IEnumerable<T>
	{
		private Func<IEnumerator<T>> _enumeratorCreator;

		public SimpleGeckoEnumerableCollection(Func<IEnumerator<T>> enumeratorCreator)
		{
			_enumeratorCreator = enumeratorCreator;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _enumeratorCreator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
