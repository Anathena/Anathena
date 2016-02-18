using System;
using System.Runtime.InteropServices;

namespace Gecko
{
	/// <summary>
	/// Streams a byte array using nsIInputStream.
	/// </summary>
	public class ByteArrayInputStream
		: nsIInputStream
	{
		private byte[] _data;
		private int _position;

		private ByteArrayInputStream( byte[] data )
		{
			_data = data;
		}

		public static ByteArrayInputStream Create( byte[] data )
		{
			return ( data == null ) ? null : new ByteArrayInputStream( data );
		}

		#region nsIInputStream Members

		public void Close()
		{
			// do nothing
		}

		public uint Available()
		{
			return (uint)(_data.Length - _position);
		}

		public uint Read( IntPtr aBuf, uint aCount )
		{
			uint count = Math.Min( aCount, Available() );

			if ( count > 0 )
			{
				Marshal.Copy(_data, _position, aBuf, (int)count);
#if DEBUG
				for ( int i = 0; i < count; ++i )
				{
					//Console.WriteLine( ( char ) Marshal.ReadByte( aBuf, i ) );
				}
#endif
				_position += (int)count;
			}

			return count;
		}

		public unsafe uint ReadSegments( nsWriteSegmentFun aWriter, IntPtr aClosure, uint aCount )
		{
			int length = ( int ) Math.Min( aCount, Available() );
			int writeCount = 0;

			if ( length > 0 )
			{
				nsWriteSegmentFun fun = aWriter;

				fixed (byte* data = &_data[_position])
				{
					fun(this, aClosure, (IntPtr)data, _position, length, out writeCount);
				}

				_position += writeCount;
			}

			return ( uint ) writeCount;
		}

		public bool IsNonBlocking()
		{
			return true;
		}

		#endregion
	}
}