using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.IO
{
	public class WrapperInputStream
		:nsIInputStream
	{
		private Stream _stream;
		public WrapperInputStream( Stream stream )
		{
			_stream = stream;
		}
		public void Close()
		{
			_stream.Close();
		}

		public uint Available()
		{
			return (uint) ( _stream.Length - _stream.Position );
		}

		public uint Read( IntPtr aBuf, uint aCount )
		{
			byte[] tempBuffer = new byte[aCount];
			int count = _stream.Read( tempBuffer, 0, tempBuffer.Length );
			Marshal.Copy( tempBuffer, 0, aBuf, count );
			return (uint) count;
		}

		public uint ReadSegments( nsWriteSegmentFun aWriter, IntPtr aClosure, uint aCount )
		{
#warning implement it
			return 0;
		}

		public bool IsNonBlocking()
		{
			return true;
		}
	}
}
