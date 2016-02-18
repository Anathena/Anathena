using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace Gecko.Interop
{
	/// <summary>
	/// COM pointer wrapper
	/// SHOULD be used for temporal variables
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ComPtr<T>
		: IDisposable, IEquatable<ComPtr<T>>,IEquatable<T>
		where T:class
	{
		protected T _instance;

		#region ctor & dtor

		public ComPtr( T instance )
		{
			_instance = instance;
		}

		~ComPtr()
		{
			Free();
		}

		public void Dispose()
		{
			Free();
			GC.SuppressFinalize(this);
		}

		private void Free()
		{
			if (_instance != null)
				Xpcom.FreeComObject( ref _instance );
			_instance = null;
		}
		#endregion

		/// <summary>
		/// Finally releases COM object
		/// Decrement COM reference counter into zero
		/// </summary>
		public void FinalRelease()
		{
			if (_instance != null)
				Xpcom.FinalFreeComObject(ref _instance);
			_instance = null;
		}

		public T Instance
		{
			get { return _instance; }
		}

		#region Equality
		public bool Equals(ComPtr<T> other)
		{
			if (ReferenceEquals(this, other)) return true;
			if (ReferenceEquals(null, other)) return false;
			return _instance.GetHashCode() == other._instance.GetHashCode();
		}

		public bool Equals( T other )
		{
			if (ReferenceEquals(null, other)) return false;
			return _instance.GetHashCode() == other.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			if (ReferenceEquals(null, obj)) return false;
			if (!(obj is ComPtr<T>)) return false;
			return _instance.GetHashCode() == ((ComPtr<T>)obj)._instance.GetHashCode();
		}

		public override int GetHashCode()
		{
			return _instance.GetHashCode();
		}



		#endregion


		public ComPtr<TInterface> QueryInterface<TInterface>()
			where TInterface : class
		{
			var iface = Xpcom.QueryInterface<TInterface>( _instance );
			return iface == null ? null : new ComPtr<TInterface>( iface );
		}

		/// <summary>
  /// Reflection-like Mozilla API
  /// </summary>
  /// <returns></returns>
		public ClassInfo GetClassInfo()
		{
			var iface = Xpcom.QueryInterface<nsIClassInfo>(_instance);
			return iface == null ? null : new ClassInfo( iface );
		}

		/// <summary>
		/// Method for getting specific function in com object
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="slot"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		public bool GetComMethod<U>(int slot, out Delegate method)
			where U : class
		{
			
			IntPtr comInterfaceForObject = Marshal.GetComInterfaceForObject(_instance, typeof(T));
			if (comInterfaceForObject == IntPtr.Zero)
			{
				method = null;
				return false;
			}
			bool flag = false;
			try
			{
				IntPtr ptr = Marshal.ReadIntPtr(Marshal.ReadIntPtr(comInterfaceForObject, 0), slot * IntPtr.Size);
				method = Marshal.GetDelegateForFunctionPointer(ptr, typeof(U));
				flag = true;
			}
			finally
			{
				Marshal.Release(comInterfaceForObject);
			}
			return flag;
		}

		public int GetSlotOfComMethod(Delegate method)
		{
			if (method.Method.DeclaringType != typeof(T))
				throw new ArgumentOutOfRangeException("method");
			return Marshal.GetComSlotForMethodInfo(method.Method);
		}

		public T GetComMethod<T>(int slot)
			where T : class
		{
			Delegate method;
			if (!this.GetComMethod<T>(slot, out method))
				throw new MethodAccessException();
			return (T)(object)method;
		}
	}
}
