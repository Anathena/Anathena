using System;
using System.Runtime.InteropServices;

namespace Gecko.IO
{
	public sealed class StringInputStream
		:InputStream
	{
		private nsIStringInputStream _stringInputStream;

		internal StringInputStream(nsIStringInputStream stream)
			:base(stream)
		{
			_stringInputStream = stream;
		}

		public void SetData(string data,int dataLen)
		{
			_stringInputStream.SetData( data,dataLen );
		}

		public void AdoptData(byte[] data)
		{
			AdoptData( data, data.Length );
		}

		public void AdoptData(byte[] data,int dataLen)
		{
			// DON'T FREE THIS BUFFER
			var buffer = Xpcom.Alloc(new IntPtr(dataLen));
			Marshal.Copy( data, 0, buffer, dataLen );
			// the input stream takes
			// ownership of the given data buffer and will nsMemory::Free it when
			// the input stream is destroyed.
			_stringInputStream.AdoptData( buffer, dataLen );
		}

		public void ShareData(string data,int dataLen)
		{
			_stringInputStream.ShareData( data,dataLen );
		}
	}
}