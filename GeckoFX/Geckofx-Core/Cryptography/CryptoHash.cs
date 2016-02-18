using System.Security.Cryptography;
using Gecko.Interop;

namespace Gecko.Cryptography
{
	/// <summary>
	/// Xulrunner and .NET cryptography models are not similar
	/// PLZ don't make CryptoHash extends System.Security.Cryptography.HashAlgorithm
	/// </summary>
	public sealed class CryptoHash
	{
		private ComPtr<nsICryptoHash> _cryptoHash;

		public CryptoHash(HashAlgorithm algorithm)
		{
			_cryptoHash = Xpcom.CreateInstance2<nsICryptoHash>(Contracts.Hash);
			_cryptoHash.Instance.Init( ( uint ) algorithm );
		}

		public void Update(byte[] array)
		{
			_cryptoHash.Instance.Update(array,(uint) array.Length);
		}

		public void UpdateFromStream(Gecko.IO.InputStream stream,int count)
		{
			_cryptoHash.Instance.UpdateFromStream( stream._inputStream, ( uint ) count );
		}

		public void UpdateFromStream(Gecko.IO.InputStream stream)
		{
			_cryptoHash.Instance.UpdateFromStream( stream._inputStream, 0xFFFFFFFF );
		}


		/// <summary>
		/// Return hash in base64 form
		/// </summary>
		/// <returns></returns>
		public string FinishBase64()
		{
			var ret = nsString.Get(_cryptoHash.Instance.Finish, true);
			return ret;
		}

		public byte[] Finish()
		{
			byte[] ret;
			using (var str = new nsACString())
			{
				_cryptoHash.Instance.Finish(false, str);
				ret = str.GetRawData();
			}
			return ret;
		}
	}
}