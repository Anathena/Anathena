using System;
using System.Collections.Generic;

namespace Gecko.Events
{
	public sealed class GeckoEventListTmp
	{
		private Dictionary<int,Delegate> _delegates=new Dictionary<int, Delegate>();


		public Delegate this[int key]
		{
			get
			{
				Delegate ret = null;
				if ( _delegates.TryGetValue( key, out ret ) )
				{
					return ret;
				}
				return null;
			}
			set
			{
				Delegate ret = null;
				if (_delegates.TryGetValue(key, out ret))
				{
					_delegates[ key ] = value;
				}
				else
				{
					_delegates.Add( key, value );
				}
			}
		}

		public void AddHandler(int key, Delegate handler)
		{
			Delegate ret = null;
			if (_delegates.TryGetValue(key, out ret))
			{
				_delegates[key] = Delegate.Combine(ret, handler);
			}
			else
			{
				_delegates.Add( key, handler );
			}
		}

		public void RemoveHandler(int key, Delegate handler)
		{
			Delegate ret = null;
			if (_delegates.TryGetValue(key, out ret))
			{
				_delegates[key] = Delegate.Remove(ret, handler);
			}
		}
	}
}