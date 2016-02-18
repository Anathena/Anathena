using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.CustomMarshalers
{
	/// <summary>
	/// Custom Marshaler for xpcom/xulrunner native type string string
	/// Introduced to marshal return values from preference service (fix AccessViolationException)
	/// 
	/// To use managed type string as parameter and apply attribute:
	/// [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Gecko.CustomMarshalers.StringMarshaler")] string
	/// or for string return types use:
	/// [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Gecko.CustomMarshalers.StringMarshaler")]
	/// </summary>
	public class StringMarshaler
		: ICustomMarshaler
	{
		private List<IntPtr> allocs = new List<IntPtr>(5);

		/// <summary>
		/// Convert 
		/// </summary>
		/// <param name="pNativeData"></param>
		/// <returns>copy of native ASCII string as managed string.</returns>
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{			
			return Marshal.PtrToStringAnsi(pNativeData);
		}

		/// <summary>
		/// Convert the passed string to ASCII encoded string 
		/// written to memory allocated by xpcom/xulrunner itself.
		/// </summary>
		/// <param name="ManagedObj"></param>
		/// <returns>ptr to native memory containing ansi encoded string + null term</returns>
		public IntPtr MarshalManagedToNative(object ManagedObj)
		{			
			var str = ManagedObj as string;
			if (str == null) return IntPtr.Zero;
			// ascii char is 1 bytes and 1 bytes '\0'
			byte[] bytes = new byte[str.Length + 1];
			// translate string into bytes
			Encoding.ASCII.GetBytes(str, 0, str.Length, bytes, 0);
			// allocate memory by xulrunner runtime
			IntPtr unmanagedMemory = Xpcom.Alloc(new IntPtr(bytes.Length));
			allocs.Add(unmanagedMemory);
			// copy byte array into unmanaged memory
			Marshal.Copy(bytes, 0, unmanagedMemory, bytes.Length);
			return unmanagedMemory;
		}

		public void CleanUpNativeData(IntPtr pNativeData)
		{
			if (allocs.Contains(pNativeData))
			{
				allocs.Remove(pNativeData);
				Xpcom.Free(pNativeData);
			}
		}

		public void CleanUpManagedData(object ManagedObj)
		{
			
		}

		public int GetNativeDataSize()
		{			
			return -1;
		}

		static ICustomMarshaler GetInstance(string pstrCookie)
		{			
			return new StringMarshaler();
		}
	}
}