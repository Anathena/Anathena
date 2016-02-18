using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.Interop
{
	public static class ExtensionMethods
	{
		/// <summary>
		/// Function that check if object is null -> then call wrapper creator
		/// </summary>
		/// <typeparam name="TGeckoObject"></typeparam>
		/// <typeparam name="TWrapper"></typeparam>
		/// <param name="obj"></param>
		/// <param name="wrapper"></param>
		/// <returns></returns>
		public static TWrapper Wrap<TGeckoObject, TWrapper>( this TGeckoObject obj, Func<TGeckoObject, TWrapper> wrapper )
			where TGeckoObject : class
			where TWrapper : class
		{
			return obj == null ? null : wrapper( obj );
		}

		/// <summary>
		/// Function that check if object is null -> then call property getter,check returned object, and if it not null -> calls wrapper creator
		/// </summary>
		/// <typeparam name="TWrapper"></typeparam>
		/// <typeparam name="TGeckoObject2"></typeparam>
		/// <typeparam name="TGeckoObject1"></typeparam>
		/// <param name="obj"></param>
		/// <param name="wrapper"></param>
		/// <returns></returns>
		public static TWrapper Wrap<TGeckoObject1,TGeckoObject2, TWrapper>(this TGeckoObject1 obj,Func<TGeckoObject1, TGeckoObject2> getter, Func<TGeckoObject2, TWrapper> wrapper)
			where TGeckoObject1 : class
			where TGeckoObject2 : class
			where TWrapper : class
		{
			if ( obj == null ) return null;
			var obj1 = getter( obj );
			return obj1 == null ? null : wrapper( obj1 );
		}

		public static ComPtr<TCOMObject> AsComPtr<TCOMObject>(this TCOMObject obj)
			where TCOMObject : class
		{
			if (obj == null) return null;
			return new ComPtr<TCOMObject>(obj);
		}
	}
}
