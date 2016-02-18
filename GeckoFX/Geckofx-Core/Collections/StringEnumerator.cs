using System;
using System.Collections;
using System.Collections.Generic;

namespace Gecko.Collections
{
	/// <summary>
	/// String Enumerator
	/// </summary>
	internal sealed class StringEnumerator
		:IEnumerator<string>
	{
		private nsIStringEnumerator _enumerator;
		private string _current;

		internal StringEnumerator(nsIStringEnumerator enumerator)
		{
			_enumerator = enumerator;
		}

		public void Dispose()
		{
			var disposable = _enumerator as IDisposable;
			if (disposable != null)
				disposable.Dispose();
			_enumerator = null;
			_current = null;
			GC.SuppressFinalize(this);
		}

		public bool MoveNext()
		{
			bool flag = _enumerator.HasMore();
			if (flag)
			{
				_current = nsString.Get( _enumerator.GetNext );
			}
			return flag;
		}

		public void Reset()
		{
			//There is no way to "reset" an enumerator, once you obtain one.
			throw new NotSupportedException("Reset is not supported for this enumeration");
		}

		public string Current
		{
			get { return _current; }
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}

		public static string[] ToStringArray(nsIStringEnumerator enumerator)
		{
			var ret = new List<string>();
			while (enumerator.HasMore())
			{
				var value = nsString.Get(enumerator.GetNext);
				ret.Add(value);
			}
			return ret.ToArray();
		}
	}

	internal sealed class Utf8StringEnumerator
		:IEnumerator<string>
	{
		private nsIUTF8StringEnumerator _enumerator;
		private string _current;

		internal Utf8StringEnumerator(nsIUTF8StringEnumerator enumerator)
		{
			_enumerator = enumerator;
		}

		public void Dispose()
		{
			_enumerator = null;
			_current = null;
		}

		public bool MoveNext()
		{
			bool flag = _enumerator.HasMore();
			if (flag)
			{
				_current = nsString.Get( _enumerator.GetNext );
			}
			return flag;
		}

		public void Reset()
		{
			//There is no way to "reset" an enumerator, once you obtain one.
			throw new NotSupportedException("Reset is not supported for this enumeration");
		}

		public string Current
		{
			get { return _current; }
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}

		public static string[] ToStringArray(nsIUTF8StringEnumerator enumerator)
		{
			List<string> ret = new List<string>();
			while ( enumerator.HasMore() )
			{
				var value = nsString.Get(enumerator.GetNext);
				ret.Add( value );
			}
			return ret.ToArray();
		}
	}
}