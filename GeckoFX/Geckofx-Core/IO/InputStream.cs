using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Gecko.Interop;

namespace Gecko.IO
{
	public class InputStream
		: System.IO.Stream
	{
		private bool _seekable;
		private nsISeekableStream _seekableStream;
		internal nsIInputStream _inputStream;

		#region ctor & dtor
		internal InputStream( nsIInputStream inputStream )
		{
			_inputStream = inputStream;
			// refcount (+1)
			_seekableStream = Xpcom.QueryInterface<nsISeekableStream>( inputStream );
			_seekable = _seekableStream != null;
		}

		protected override void Dispose( bool disposing )
		{
			// refcount (-1)
			Xpcom.FreeComObject( ref _seekableStream );
			// refcount (-1)
			Xpcom.FreeComObject( ref _inputStream );
			base.Dispose( disposing );
		}
		#endregion

		#region .NET Stream Model
		public override void Flush()
		{
		}

		public override void Close()
		{
			_inputStream.Close();
		}

		public override long Seek( long offset, SeekOrigin origin )
		{
			//NS_SEEK_SET 	0 	Specifiesthat the offset is relative to the start of the stream.
			//NS_SEEK_CUR 	1 	Specifies that the offset is relative to the current position in the stream.
			//NS_SEEK_END 	2 	Specifies that the offset is relative to the end of the stream.

			// SeekOrigin.Begin   0
			// SeekOrigin.Current 1
			// SeekOrigin.End     2

			_seekableStream.Seek( (int) origin, offset );
			return _seekableStream.Tell();
		}

		public override void SetLength( long value )
		{
			var position = _seekableStream.Tell();
			_seekableStream.Seek( 0, (int) value );
			_seekableStream.SetEOF();
			if (position < value)
			{
				// Returning to old position
				_seekableStream.Seek( 0, position );
			}

		}

		public override unsafe int Read( byte[] buffer, int offset, int count )
		{
			uint ret;
			// strict values & buffer size check before using pointers
			if (( offset < 0 ) || ( count <= 0 )) return 0;
			// offset >= 0 count>0
			if (( offset + count ) > buffer.Length) return 0;
			fixed (byte* bufferPtr = buffer)
			{
				byte* writePtr = bufferPtr + offset;
				ret = _inputStream.Read( new IntPtr( writePtr ), (uint) count );
			}
			return (int) ret;
		}

		public override unsafe int ReadByte()
		{
			byte ret;
			byte* ptr = &ret;
			uint count = _inputStream.Read( new IntPtr( ptr ), 1 );
			return count == 0 ? -1 : ret;
		}

		/// <summary>
		/// Warning: .NET have another stream model. DON'T USE THIS FUNCTION
		/// InputStream can only read data
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		public override void Write( byte[] buffer, int offset, int count )
		{
			throw new NotSupportedException( "InputStream can only read data :)" );
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanSeek
		{
			get { return _seekable; }
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		/// <summary>
		/// Warning: .NET have another stream model. DON'T USE THIS PROPERTY
		/// </summary>
		public override long Length
		{
			get
			{
				if (_seekable)
				{
					return _seekableStream.Tell() + _inputStream.Available();
				}
				return _inputStream.Available();
			}
		}

		public override long Position
		{
			get
			{
				return _seekable ? _seekableStream.Tell() : 0;
			}
			set
			{
				if (_seekable)
				{
					_seekableStream.Seek(0, (int)value);
				}
			}
		}
		#endregion

		public long Available
		{
			get { return _inputStream.Available(); }
		}

		/// <summary>
		/// Method is useful when reading headers
		/// </summary>
		/// <returns></returns>
		public string ReadLine()
		{
			StringBuilder ret = new StringBuilder( 64 );
			var count = _inputStream.Available();
			for (var i = 0; i < count; i++)
			{
				var character = ReadByte();
				if (character < 0) break;
				char test = (char) (byte) character;
				if (test == '\r')
				{
					// nothing
				}
				else
				{
					if (test == '\n')
					{
						break;
					}
					ret.Append( test );
				}
			}
			return ret.ToString();
		}

		public static InputStream Create( nsIInputStream stream )
		{
			var mimeInputStream = Xpcom.QueryInterface<nsIMIMEInputStream>( stream );
			if (mimeInputStream != null)
			{
				Marshal.ReleaseComObject( stream );
				return new MimeInputStream( mimeInputStream );
			}
			var stringInputStream = Xpcom.QueryInterface<nsIStringInputStream>( stream );
			if (stringInputStream != null)
			{
				Marshal.ReleaseComObject( stream );
				return new StringInputStream( stringInputStream );
			}
			return new InputStream( stream );
		}
	}
}
