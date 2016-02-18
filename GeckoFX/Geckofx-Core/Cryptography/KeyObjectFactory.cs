using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;

namespace Gecko.Cryptography
{
	/// <summary>
	/// DO NOT USE KeyFromString or expose it (according to xulrunner comments)
	/// </summary>
	public static class KeyObjectFactory
	{
		private static ComPtr<nsIKeyObjectFactory> _keyObjectFactory;

		static KeyObjectFactory()
		{
			_keyObjectFactory = Xpcom.GetService2<nsIKeyObjectFactory>(Contracts.KeyObjectFactory);
		}

		public static KeyObject UnwrapKey(AlgorithmType algorithm,byte[] key)
		{
			var keyObj = _keyObjectFactory.Instance.UnwrapKey((short)algorithm, key, (uint)key.Length);
			return new KeyObject(keyObj);
		}

		public static KeyObject LookupKeyByName(string name)
		{
			var keyObj = nsString.Pass<nsIKeyObject>( _keyObjectFactory.Instance.LookupKeyByName, name );
			return new KeyObject( keyObj );
		}
	}
}
