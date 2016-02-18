using System;

namespace Gecko.Interop
{
	/// <summary>
	/// Internal unsafe memory copier
	/// </summary>
	internal static class MemCopy
	{
		/// <summary>
		/// Architecture independent memory copy
		/// will operate 32-bit blocks on IA32 and 64-bit blocks on IA32_x64
		/// </summary>
		/// <param name="source"></param>
		/// <param name="destination"></param>
		/// <param name="count"></param>
		internal unsafe static void Copy(void* source, void* destination, int count)
		{
			//count of [__int32 on IA32] or [__int64 on IA32_x64]
			var xwordCount = count / IntPtr.Size;
			var bytesCount = count % IntPtr.Size;
			IntPtr* srcPtr = (IntPtr*)source;
			IntPtr* dstPtr = (IntPtr*)destination;
			// copy by blocks
			for (int i = 0; i < xwordCount; i++)
			{
				dstPtr[i] = srcPtr[i];
			}
			byte* srcBytes = ((byte*)srcPtr) + xwordCount * IntPtr.Size;
			byte* dstBytes = ((byte*)dstPtr) + xwordCount * IntPtr.Size;
			for (int i = 0; i < bytesCount; i++)
			{
				dstBytes[i] = srcBytes[i];
			}
		}
	}
}