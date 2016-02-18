using System;

namespace Gecko.IO.Native
{
	/// <summary>
	/// Basic nsIInputStream stream implementation
	/// this class does not perform any memory allocation operations
	/// </summary>
	public abstract class BaseNativeInputStream
		: BaseNativeStream,nsIInputStream
	{
		public uint Available()
		{
			return (uint)(Length - Position);
		}

		public unsafe uint Read(IntPtr aBuf, uint aCount)
		{
			uint count = Math.Min(Available(), aCount);
			if (count == 0) return 0;
			if (Buffer == IntPtr.Zero) return 0;
			Interop.MemCopy.Copy(CurrentPointer, aBuf.ToPointer(), (int)count);
			MovePointer(count);
			return count;
		}

		public unsafe uint ReadSegments(nsWriteSegmentFun aWriter, IntPtr aClosure, uint aCount)
		{
			int count = (int)Math.Min(aCount, Available());
			if (count == 0) return 0;
			if (Buffer == IntPtr.Zero) return 0;
			int writeCount;
			aWriter(this, aClosure, new IntPtr(CurrentPointer), (int)Position, count, out writeCount);
			MovePointer((uint)count);
			return (uint)writeCount;
		}

		public bool IsNonBlocking()
		{
			return true;
		}

		public abstract void Close();
	}
}