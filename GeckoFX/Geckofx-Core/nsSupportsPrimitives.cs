using System;
using System.Collections.Generic;

namespace Gecko
{
	public static class nsSupportsPrimitiveConverter
	{
		#region Get
		public static object GetObject(nsISupportsPrimitive value)
		{
			switch ( value.GetTypeAttribute() )
			{
				//TYPE_ID 	1 	Corresponding to nsISupportsID .
				case 1:
					return GetID( ( nsISupportsID ) value );
				//TYPE_CSTRING 	2 	Corresponding to nsISupportsCString .
				case 2:
					return GetString( ( nsISupportsCString ) value );
				//TYPE_STRING 	3 	Corresponding to nsISupportsString .
				case 3:
					return GetString( ( nsISupportsString ) value );
				//TYPE_PRBOOL 	4 	Corresponding to nsISupportsPRBool .
				case 4:
					return GetBool( ( nsISupportsPRBool ) value );
				//TYPE_PRUINT8 	5 	Corresponding to nsISupportsPRUint8 .
				case 5:
					return GetByte( ( nsISupportsPRUint8 ) value );
				//TYPE_PRUINT16 	6 	Corresponding to nsISupportsPRUint16 .
				case 6:
					return GetUInt16( ( nsISupportsPRUint16 ) value );
				//TYPE_PRUINT32 	7 	Corresponding to nsISupportsPRUint32 .
				case 7:
					return GetUInt32((nsISupportsPRUint32)value);
				//TYPE_PRUINT64 	8 	Corresponding to nsISupportsPRUint64 .
				case 8:
					return GetUInt64((nsISupportsPRUint64)value);
				//TYPE_PRTIME 	9 	Corresponding to nsISupportsPRTime .
				case 9:
					return GetTime((nsISupportsPRTime)value);
				//TYPE_CHAR 	10 	Corresponding to nsISupportsChar .
				case 10:
					return GetChar((nsISupportsChar)value);	
				//TYPE_PRINT16 	11 	Corresponding to nsISupportsPRInt16 .
				case 11:
					return GetInt16((nsISupportsPRInt16)value);	
				//TYPE_PRINT32 	12 	Corresponding to nsISupportsPRInt32 .
				case 12:
					return GetInt32((nsISupportsPRInt32)value);	
				//TYPE_PRINT64 	13 	Corresponding to nsISupportsPRInt64 .
				case 13:
					return GetInt64((nsISupportsPRInt64)value);	
				//TYPE_FLOAT 	14 	Corresponding to nsISupportsFloat .
				case 14:
					return GetFloat((nsISupportsFloat)value);	
				//TYPE_DOUBLE 	15 	Corresponding to nsISupportsDouble .
				case 15:
					return GetDouble((nsISupportsDouble)value);
				//TYPE_VOID 	16 	Corresponding to nsISupportsVoid .
				case 16:
					return GetVoid( ( nsISupportsVoid ) value );
				//TYPE_INTERFACE_POINTER 	17 	Corresponding to nsISupportsInterfacePointer .
				case 17:
					return GetInterfacePointer( ( nsISupportsInterfacePointer ) value );
			}
			return null;


		}

		public static object GetID(nsISupportsID value)
		{
			return value.GetDataAttribute();
		}

		public static string GetString(nsISupportsCString value)
		{
			return nsString.Get( value.GetDataAttribute );
		}

		public static string GetString(nsISupportsString value)
		{
			return nsString.Get(value.GetDataAttribute);
		}

		public static bool GetBool(nsISupportsPRBool value)
		{
			return value.GetDataAttribute();
		}

		public static byte GetByte(nsISupportsPRUint8 value)
		{
			return value.GetDataAttribute();
		}

		public static UInt16 GetUInt16(nsISupportsPRUint16 value)
		{
			return value.GetDataAttribute();
		}

		public static UInt32 GetUInt32(nsISupportsPRUint32 value)
		{
			return value.GetDataAttribute();
		}

		public static UInt64 GetUInt64(nsISupportsPRUint64 value)
		{
			return value.GetDataAttribute();
		}

		public static Int64 GetTime(nsISupportsPRTime value)
		{
			return value.GetDataAttribute();
		}

		public static char GetChar(nsISupportsChar value)
		{
			return value.GetDataAttribute();
		}

		public static Int16 GetInt16(nsISupportsPRInt16 value)
		{
			return value.GetDataAttribute();
		}

		public static Int32 GetInt32(nsISupportsPRInt32 value)
		{
			return value.GetDataAttribute();
		}

		public static Int64 GetInt64(nsISupportsPRInt64 value)
		{
			return value.GetDataAttribute();
		}

		public static float GetFloat(nsISupportsFloat value)
		{
			return value.GetDataAttribute();
		}

		public static double GetDouble(nsISupportsDouble value)
		{
			return value.GetDataAttribute();
		}

		public static IntPtr GetVoid(nsISupportsVoid value)
		{
			return value.GetDataAttribute();
		}

		internal static nsISupports GetInterfacePointer(nsISupportsInterfacePointer value)
		{
			return value.GetDataAttribute();
		}
		#endregion


		public static nsISupportsPRBool SetBool(bool value)
		{
			var bvalue = Xpcom.CreateInstance<nsISupportsPRBool>(Contracts.SupportsBool);
			bvalue=Xpcom.QueryInterface<nsISupportsPRBool>(bvalue);
			bvalue.SetDataAttribute( value );

			var test = Xpcom.QueryInterface<nsISupports>(bvalue);
			return bvalue;
		}
	}
}