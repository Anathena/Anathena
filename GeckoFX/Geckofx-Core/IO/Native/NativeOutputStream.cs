using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.IO.Native
{
	public sealed class NativeOutputStream
		:BaseNativeStream, nsIOutputStream,IDisposable
	{

		public NativeOutputStream(int firstSize)
		{
			Init(Marshal.AllocHGlobal(firstSize), firstSize);
		}

		~NativeOutputStream()
		{
			Close();
		}


		public void Dispose()
		{
			Close();
			GC.SuppressFinalize( this );
		}



		public void Close()
		{
			if (Buffer != IntPtr.Zero)
			{
				Marshal.FreeHGlobal( Buffer );
				Init( IntPtr.Zero, 0 );
			}
		}

		/// <summary>
		/// Internal function
		/// Check 
		/// </summary>
		/// <param name="count"></param>
		private void PreparePointer(int count)
		{
			var newTotalSize = Position + count;
			if (newTotalSize < Length)
			{
				return;
			}
			// expanding -> adding 64 Kbyte block
			var newSize = Length + 1024 * 64;
			// max of newSize and newTotalSize
			var newBufferSize = Math.Max( newSize, newTotalSize );
			var buffer = Marshal.ReAllocHGlobal(Buffer, new IntPtr(newBufferSize));
			Init( buffer,(int) newBufferSize );
		}

		public void Flush()
		{
			
		}

		public uint Write(string aBuf, uint aCount)
		{
			throw new NotImplementedException();
		}

		public unsafe uint Write( IntPtr aBuf, uint aCount )
		{
			PreparePointer((int)aCount);
			Interop.MemCopy.Copy( aBuf.ToPointer(), CurrentPointer, ( int ) aCount );
			MovePointer(  aCount );
			return aCount;
		}

		public unsafe uint WriteFrom( nsIInputStream aFromStream, uint aCount )
		{
			PreparePointer( ( int ) aCount );
			aFromStream.Read(new IntPtr(CurrentPointer), aCount);
			MovePointer( aCount );
			return aCount;
		}

		public uint WriteSegments( IntPtr aReader, IntPtr aClosure, uint aCount )
		{
			throw new NotImplementedException();
		}

		public bool IsNonBlocking()
		{
			return false;
		}		
	}
}