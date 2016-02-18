using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Gecko.CustomMarshalers
{
	/// <summary>
	/// Custom Marshaler for xpcom/xulrunner native type wstring wstring
	/// 
	/// To use managed type string as parameter and apply attribute:
	/// [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Gecko.CustomMarshalers.WStringMarshaler")] string
	/// or for string return types use:
	/// [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Gecko.CustomMarshalers.WStringMarshaler")]
	/// </summary>
	public class WStringMarshaler
		: ICustomMarshaler
	{
		public void CleanUpManagedData(object ManagedObj)
		{
			
		}

		public void CleanUpNativeData(IntPtr pNativeData)
		{			
			Xpcom.Free(pNativeData);
		}

		public int GetNativeDataSize()
		{
			return -1;
		}

		/// <summary>
		/// Convert the passed string to Utf16 encoded string 
		/// written to memory allocated by xpcom/xulrunner itself.
		/// </summary>
		/// <param name="ManagedObj"></param>
		/// <returns>ptr to native memory containing utf16 encoded string + null term</returns>
		public IntPtr MarshalManagedToNative(object ManagedObj)
		{			
			var str = ManagedObj as string;
			if (str == null) return IntPtr.Zero;
			// unicode char is 2 bytes and 2 bytes '\0\0'
			byte[] bytes = new byte[str.Length*2 + 2];
			// translate string into bytes
			Encoding.Unicode.GetBytes( str, 0, str.Length, bytes, 0 );
			// allocate memory by xulrunner runtime
			IntPtr unmanagedMemory = Xpcom.Alloc(new IntPtr(bytes.Length));
			// copy byte array into unmanaged memory
			Marshal.Copy( bytes, 0, unmanagedMemory, bytes.Length );
			return unmanagedMemory;
		}

		/// <summary>
		/// Convert 
		/// </summary>
		/// <param name="pNativeData"></param>
		/// <returns>copy of native ut16 string as managed string.</returns>
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{			
			return Marshal.PtrToStringUni(pNativeData);
		}

		static ICustomMarshaler GetInstance(string pstrCookie)
		{			
			return new WStringMarshaler();
		}
	}
}
