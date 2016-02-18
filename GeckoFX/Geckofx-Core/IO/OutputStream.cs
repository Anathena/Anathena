using System;
using System.IO;

namespace Gecko.IO
{
	public sealed class OutputStream
		: Stream
	{
		private bool _seekable;
		private nsISeekableStream _seekableStream;
		internal nsIOutputStream _outputStream;

		#region ctor & dtor
		private OutputStream( nsIOutputStream outputStream )
		{
			_outputStream = outputStream;
			_seekableStream = Xpcom.QueryInterface<nsISeekableStream>( outputStream );
			_seekable = _seekableStream != null;

		}

		protected override void Dispose( bool disposing )
		{
			// refcount (-1)
			Xpcom.FreeComObject( ref _seekableStream );
			// refcount (-1)
			Xpcom.FreeComObject( ref _outputStream );
			base.Dispose( disposing );
		}
		#endregion

		#region .NET Stream Model
		public override void Close()
		{
			_outputStream.Close();
		}

		public override void Flush()
		{
			_outputStream.Flush();
		}

		public override long Seek( long offset, SeekOrigin origin )
		{
			//NS_SEEK_SET 	0 	Specifiesthat the offset is relative to the start of the stream.
			//NS_SEEK_CUR 	1 	Specifies that the offset is relative to the current position in the stream.
			//NS_SEEK_END 	2 	Specifies that the offset is relative to the end of the stream.
			_seekableStream.Seek( (int) origin, (int) offset );
			return _seekableStream.Tell();
		}

		public override void SetLength( long value )
		{
			int current = _seekableStream.Tell();
			_seekableStream.Seek( 0, (int) value );
			_seekableStream.SetEOF();
			_seekableStream.Seek( 0, current );
		}

		public override int Read( byte[] buffer, int offset, int count )
		{
			throw new NotSupportedException( "OutputStream can only write data :)" );
		}

		public override unsafe void WriteByte( byte value )
		{
			byte* ptr = &value;
			_outputStream.Write( new IntPtr(ptr), 1 );
		}

		public override unsafe void Write( byte[] buffer, int offset, int count )
		{
			if (( offset + count ) > buffer.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			fixed (byte* ptr = buffer)
			{
				byte* start = ptr + offset;
				_outputStream.Write( new IntPtr( (void*) start ), (uint) count );
			}
		}

		public override bool CanRead
		{
			get { return false; }
		}

		public override bool CanSeek
		{
			get { return _seekable; }
		}

		public override bool CanWrite
		{
			get { return true; }
		}

		public override long Length
		{
			get { return _seekableStream.Tell(); }
		}

		public override long Position
		{
			get { return _seekableStream.Tell(); }
			set { _seekableStream.Seek( 0, (int) value ); }
		}
		#endregion

		public static OutputStream Create( nsIOutputStream stream )
		{
			return new OutputStream( stream );
		}
	}
}