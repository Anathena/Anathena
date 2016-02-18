using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Collections
{
	/// <summary>
	/// Generic wrapper for nsISimpleEnumerator
	/// </summary>
	/// <typeparam name="TWrapper"></typeparam>
	/// <typeparam name="TGeckoObject"></typeparam>
	internal sealed class GeckoEnumerator<TWrapper, TGeckoObject>
		: IEnumerator<TWrapper>
	{ 
		private nsISimpleEnumerator _enumerator;
		private TGeckoObject _current;
		private Func<TGeckoObject, TWrapper> _translator;

		internal GeckoEnumerator(nsISimpleEnumerator enumerator, Func<TGeckoObject, TWrapper> translator)
		{
			_enumerator = enumerator;
			_translator = translator;
		}

		public void Dispose()
		{
			var disposable = _enumerator as IDisposable;
			if (disposable != null)
				disposable.Dispose();
			_enumerator = null;
			disposable = _current as IDisposable;
			if (disposable != null)
				disposable.Dispose();
			_current = default(TGeckoObject);
			_translator = null;
			GC.SuppressFinalize(this);
		}

		public bool MoveNext()
		{
			bool flag = _enumerator.HasMoreElements();
			if (flag)
			{
				var obj = _enumerator.GetNext();
				_current = ( TGeckoObject ) obj;
			}
			return flag;
		}

		public void Reset()
		{
			//There is no way to "reset" an enumerator, once you obtain one.
			throw new NotSupportedException("Reset is not supported for this enumeration");
		}

		public TWrapper Current
		{
			get { return _translator(_current); }
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}
	}
}
