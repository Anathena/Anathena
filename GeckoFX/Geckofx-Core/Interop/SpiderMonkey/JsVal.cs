using System;
using System.Runtime.InteropServices;
namespace Gecko
{

    /// <summary>
    /// This is just a piece of a JsVal implementation, just enough to get the message out.
    /// netmonkey and spidermonkeydotnet have fuller implementations
    /// </summary>
    /// <remarks>
    /// 		/// WARNING:
    /// https://developer.mozilla.org/En/SpiderMonkey/JSAPI_Reference/Jsval
    /// Says: "jsval is a variant type whose exact representation varies by architecture.  
    ///			Embeddings should not rely on observed representation details, the size of jsval, 
    ///			or whether jsval is a primitive type."
    ///	In the code below, we are just using API calls to access it, except for the attempt to get at the value tag directly (which didn't work).
    ///		
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
	public struct JsVal
	{
		public static JsVal FromPtr(ulong value)
		{
			return new JsVal() { AsBits = value };
		}

		[FieldOffset(0)]
		public ulong AsBits;
		[FieldOffset(0)]
		public double AsDouble;
		[FieldOffset(0)]
		public IntPtr AsPtr;
		[FieldOffset(0)]
		public int I32;
		[FieldOffset(0)]
		public uint U32;
		[FieldOffset(0)]
		public int Boo;
		[FieldOffset(0)]
		public ulong Ptr;
		[FieldOffset(4)]
		public uint tag;
		
		public enum ValueTag64Bit : uint
		{		
            Clear = 0x1FFF0,
            Int32 = Clear | 1,
            Undefined = Clear | 2,
            String = Clear | 5,
            Boolean = Clear | 3,
            Magic = Clear | 4,
            Null = Clear | 6,
            Object = Clear | 7
		}

		public enum ValueTag32Bit : uint
		{
			Clear = 0xFFFFFF80,
			Int32 = Clear | 1,
			Undefined = Clear | 2,
			String = Clear | 5,
			Boolean = Clear | 3,
			Magic = Clear | 4,
			Null = Clear | 6,
			Object = Clear | 7
		}

        public uint Tag
        {
            get { return Xpcom.Is32Bit ? tag : tag >> 47; }
        }

		public bool IsNull
		{
			get { return Tag == (Xpcom.Is32Bit ? (uint)ValueTag32Bit.Null : (uint)ValueTag64Bit.Null); }
		}

		public bool IsUndefined
		{
			get { return Tag == (Xpcom.Is32Bit ? (uint)ValueTag32Bit.Undefined : (uint)ValueTag64Bit.Undefined); }
		}

		public bool IsBoolean
		{
			get { return Tag == (Xpcom.Is32Bit ? (uint)ValueTag32Bit.Boolean : (uint)ValueTag64Bit.Boolean);}
		}

		public bool IsNumber
		{
			get { return Tag <= (Xpcom.Is32Bit ? (uint)ValueTag32Bit.Int32 : (uint)ValueTag64Bit.Int32); }
		}

		public bool IsInt
		{
			get { return Tag <= (Xpcom.Is32Bit ? (uint)ValueTag32Bit.Int32 : (uint)ValueTag64Bit.Int32); }
		}

		public bool IsDouble
		{
			get { return Tag <= (Xpcom.Is32Bit ? (uint)ValueTag32Bit.Clear : (uint)ValueTag64Bit.Clear); }
		}

		public bool IsString
		{
			get { return Tag == (Xpcom.Is32Bit ? (uint)ValueTag32Bit.String : (uint)ValueTag64Bit.String); }
		}

		public bool IsObject
		{
			get	{ return Tag <= (Xpcom.Is32Bit ? (uint)ValueTag32Bit.Object : (uint)ValueTag64Bit.Object); }
		}

		public bool ToBoolean()
		{
			return Boo != 0;
		}

		public double ToDouble()
		{
			return AsDouble;
		}

		public int ToInteger()
		{
			return I32;
		}

		public override string ToString()
		{
			using (var context = new AutoJSContext())
			{
				return context.ConvertValueToString(this);
			}
		}

		public object ToObject()
		{
			if (IsNull)
			{
				return null;
			}
			if (IsUndefined)
			{
				return "Undefined";
			}

			if (IsBoolean)
			{
				return ToBoolean();
			}

			if (IsInt)
			{
				return ToInteger();
			}

			if (IsDouble)
			{
				return ToDouble();
			}

			if (IsString)
			{
				return ToString();
			}

			if (IsObject)
			{
				return ToComObject(IntPtr.Zero);
			}

			return null;
		}

		internal object ToComObject(IntPtr cx)
		{
			using (var context = new AutoJSContext(cx))
			{
				var jsObject = SpiderMonkey.JS_ValueToObject(context.ContextPointer, this);

				var guid = typeof(nsISupports).GUID;
				var pUnk = IntPtr.Zero;
				try
				{
					pUnk = Xpcom.XPConnect.Instance.WrapJS(context.ContextPointer, jsObject, ref guid);
					var comObj = Xpcom.GetObjectForIUnknown(pUnk);

					return comObj;
				}
				finally
				{
					if (pUnk != IntPtr.Zero)
					{
						Marshal.Release(pUnk);
					}
				}
			}
		}

		public JSType Type
		{
			get
			{
				using (var context = new AutoJSContext())
				{
					return SpiderMonkey.JS_TypeOfValue(context.ContextPointer, this);
				}
			}
		}
	}
}
