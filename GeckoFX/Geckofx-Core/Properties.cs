using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gecko
{
	public class Properties
	{
		private nsIProperties _properties;

		internal Properties(nsIProperties properties)
		{
			_properties = properties;
		//    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		//System.IntPtr Get([MarshalAs(UnmanagedType.LPStr)] string prop, ref System.Guid iid);
		
		///// <summary>
		///// Sets a property with a given name to a given value.
		///// </summary>
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		//void Set([MarshalAs(UnmanagedType.LPStr)] string prop, [MarshalAs(UnmanagedType.Interface)] nsISupports value);
		}

		//public void Set(string property,object value)
		//{
		//    if (value == null)
		//    {
		//        Undefine( property );
		//        return;
		//    }
		//    nsISupports container = null;
		//    var type=value.GetType();
		//    if (type.IsPrimitive)
		//    {
		//        if (type.IsValueType)
		//        {
		//            //byte,short,int,long
		//        }
		//        else
		//        {
		//            //string
		//        }
		//    }
		//    if (container!=null)
		//    {
		//        _properties.Set( property, container );
		//    }
		//    else
		//    {
		//        Debug.WriteLine( string.Format( "nsISupports container for value {0} of type {1} is not found", value, type ) );
		//    }
		//}

		public bool Has(string property)
		{
			return _properties.Has( property );
		}

		public bool Undefine(string property)
		{
			// If property is not exist it will throw exception
			bool flag = _properties.Has( property );
			if (flag)
			{
				_properties.Undefine( property );
			}
			return flag;
		}

		public string[] GetKeys()
		{
			uint count = 0;
			string[] ret = null;
#warning test it
			_properties.GetKeys( ref count,ref ret );
			return ret;
		}
	}

	public class PersistentProperties
		:Properties
	{
		private nsIPersistentProperties _persistentProperties;

		internal PersistentProperties (nsIPersistentProperties persistentProperties)
			:base(persistentProperties)
		{
			_persistentProperties = persistentProperties;
		}
	}
}
