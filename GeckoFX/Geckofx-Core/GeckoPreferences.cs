using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.IO;
using Gecko.Interop;

namespace Gecko
{
	/// <summary>
	/// Provides access to Gecko preferences.
	/// </summary>
	public class GeckoPreferences: IDisposable
	{
		const int PREF_INVALID = 0;
		const int PREF_STRING = 32;
		const int PREF_INT = 64;
		const int PREF_BOOL = 128;

		#region Static part
		private static ComPtr<nsIPrefService> _prefService;
		private static GeckoPreferences _default;
		private static GeckoPreferences _user;

		static GeckoPreferences()
		{
			// ensure we're initialized
			Xpcom.Initialize();
			_prefService = Xpcom.GetService2<nsIPrefService>( Contracts.PreferenceService );
		}

		static public void Shutdown()
		{
			if (_prefService != null)
				_prefService.Dispose();
			if (_user != null)
				_user.Dispose();
			if (_default != null)
				_default.Dispose();
			_prefService = null;
			_user = null;
			_default = null;
		}
		/// <summary>
		/// Gets the string value of a pref. This is a replacement of nsIPrefBranch::GetCharPref(), which can't handle unicode values.
		/// See also <a href="http://www-archive.mozilla.org/projects/intl/changefontpref2.html">How to change the font setting in Gecko from the Embedding Application</a>
		/// </summary>
		static string GetUnicodePref(nsIPrefBranch branch, string name)
		{
			var iid = typeof(nsISupportsString).GUID;
			IntPtr pStr = branch.GetComplexValue (name, ref iid);
			if (pStr == IntPtr.Zero)
				return null;
			var sStr = (nsISupportsString) Xpcom.GetObjectForIUnknown(pStr);
			Marshal.Release(pStr);
			return nsString.Get(sStr.GetDataAttribute);
		}

		/// <summary>
		/// Sets the string value of a pref. This is a replacement of nsIPrefBranch::SetCharPref(), which can't handle unicode values.
		/// </summary>
		static void SetUnicodePref(nsIPrefBranch branch, string name, string value)
		{
			var iid = typeof(nsISupportsString).GUID;
			var str = Xpcom.CreateInstance<nsISupportsString>(Contracts.SupportsString);
			nsString.Set(str.SetDataAttribute, value);
			branch.SetComplexValue(name, ref iid, Xpcom.QueryInterface<nsISupports>(str));
		}


		#region Properties
		/// <summary>
		/// Gets the preferences defined for the current user.
		/// </summary>
		static public GeckoPreferences User
		{
			get { return _user ?? (_user = new GeckoPreferences(false)); }
		}
		
		
		/// <summary>
		/// Gets the set of preferences used as defaults when no user preference is set.
		/// </summary>
		static public GeckoPreferences Default
		{
			get { return _default ?? (_default = new GeckoPreferences(true)); }
		}
		#endregion

		#region Load & Save
		/// <summary>
		/// Reads all User preferences from the specified file.
		/// </summary>
		/// <param name="filename">Required. The name of the file from which preferences are read.  May not be null.</param>
		static public void Load(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				throw new ArgumentNullException("filename");
			else if (!File.Exists(filename))
				throw new FileNotFoundException();
			
			_prefService.Instance.ReadUserPrefs((nsIFile)Xpcom.NewNativeLocalFile(filename));
		}
		
		/// <summary>
		/// Saves all User preferences to the specified file.
		/// </summary>
		/// <param name="filename">Required. The name of the file to which preferences are saved.  May not be null.</param>
		static public void Save(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				throw new ArgumentNullException("filename");

			_prefService.Instance.SavePrefFile((nsIFile)Xpcom.NewNativeLocalFile(filename));
		}
		#endregion

		/// <summary>
		/// returns type constant
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static int GetValueType(object value)
		{
			if (value is int) return PREF_INT;
			if (value is string) return PREF_STRING;
			if (value is bool) return PREF_BOOL;
			if (value is float) return PREF_STRING;

			throw new ArgumentException("Gecko preferences must be either a String, Int32, or Boolean value.", "prefName");
		}

		#endregion

		private ComPtr<nsIPrefBranch> _branch;
		private readonly bool _isDefaultBranch;


		private GeckoPreferences(bool defaultBranch)
		{
			_isDefaultBranch = defaultBranch;
			if (defaultBranch)
			{
				_branch = new ComPtr<nsIPrefBranch>( _prefService.Instance.GetDefaultBranch( "" ) );
			}
			else
			{
				_branch = new ComPtr<nsIPrefBranch>( _prefService.Instance.GetBranch( "" ) );
			}
		}

		#region IDisposable implementation

		public void Dispose()
		{
			if (_branch != null)
				_branch.Dispose();
			_branch = null;
			GC.SuppressFinalize(this);
		}

		#endregion
	
		/// <summary>
		/// Resets all preferences to their default values.
		/// </summary>
		public void Reset()
		{
			if (_isDefaultBranch)
			{
				_prefService.Instance.ResetPrefs();
			}
			else
			{
				_prefService.Instance.ResetUserPrefs();
			}			
		}
		

		
		/// <summary>
		/// Gets or sets the preference with the given name.
		/// </summary>
		/// <param name="prefName">Required. The name of the preference to get or set.</param>
		/// <returns></returns>
		public object this[string prefName]
		{
			get
			{
				int type = _branch.Instance.GetPrefType(prefName);
				switch (type)
				{
					case PREF_INVALID: return null;
					case PREF_STRING: return GetUnicodePref(_branch.Instance, prefName);
					case PREF_INT: return _branch.Instance.GetIntPref(prefName);
					case PREF_BOOL: return _branch.Instance.GetBoolPref(prefName);
				}
				throw new ArgumentException("prefName");
			}
			set
			{
				if (string.IsNullOrEmpty(prefName))
					throw new ArgumentException("prefName");
				else if (value == null)
					throw new ArgumentNullException("value");

				int existingType = _branch.Instance.GetPrefType(prefName);
				int assignedType = GetValueType(value);

				if ((existingType != PREF_INVALID) && (existingType != assignedType))
					throw new InvalidCastException("A " + value.GetType().Name + " value may not be assigned to '" + prefName + "' because it is already defined as " + GetPreferenceType(prefName).Name + ".");

				if (value is string)
				{
					SetUnicodePref(_branch.Instance, prefName, (string) value);
					return;
				}
				if (value is int)
				{
					_branch.Instance.SetIntPref(prefName, (int)value);
					return;
				}
				if (value is bool)
				{
					_branch.Instance.SetBoolPref( prefName, (bool) value );
				}
				if (value is float)
				{
					SetUnicodePref(_branch.Instance, prefName, ( (float) value ).ToString());
				}
			}
		}


		public string Root
		{
			get
			{
				return _branch.Instance.GetRootAttribute();
			}
		}

		#region Typed properties
		#region int pref
		public bool GetIntPref( string prefName,out int? value )
		{
			if (string.IsNullOrEmpty(prefName)) throw new ArgumentException("prefName");

			int existingType = _branch.Instance.GetPrefType( prefName );
			switch (existingType)
			{
				case PREF_INVALID:
					value = null;
					return true;
				case PREF_INT:
					value= _branch.Instance.GetIntPref( prefName );
					return true;
				default:
					value = null;
					return false;
			}
		}

		public bool SetIntPref( string prefName, int? value )
		{
			if (string.IsNullOrEmpty(prefName)) throw new ArgumentException("prefName");

			int existingType = _branch.Instance.GetPrefType(prefName);
			switch (existingType)
			{
				case PREF_INVALID:
					if (value != null)
					{
						_branch.Instance.SetIntPref( prefName, value.Value );
					}
					return true;
				case PREF_INT:
					if (value != null)
					{
						_branch.Instance.SetIntPref(prefName, value.Value);
					}
					else
					{
						_branch.Instance.ClearUserPref( prefName );
					}
					return true;
				default:
					return false;
			}
		}

		#endregion

		#region bool pref
		public bool GetBoolPref( string prefName, out bool? value )
		{
			if (string.IsNullOrEmpty(prefName)) throw new ArgumentException("prefName");

			int existingType = _branch.Instance.GetPrefType(prefName);
			switch (existingType)
			{
				case PREF_INVALID:
					value = null;
					return true;
				case PREF_BOOL:
					value = _branch.Instance.GetBoolPref(prefName);
					return true;
				default:
					value = null;
					return false;
			}
		}

		public bool SetBoolPref(string prefName, bool? value)
		{
			if (string.IsNullOrEmpty(prefName)) throw new ArgumentException("prefName");

			int existingType = _branch.Instance.GetPrefType(prefName);
			switch (existingType)
			{
				case PREF_INVALID:
					if (value != null)
					{
						_branch.Instance.SetBoolPref(prefName, value.Value);
					}
					return true;
				case PREF_BOOL:
					if (value != null)
					{
						_branch.Instance.SetBoolPref(prefName, value.Value);
					}
					else
					{
						_branch.Instance.ClearUserPref(prefName);
					}
					return true;
				default:
					return false;
			}
		}

		#endregion

		#region string pref
		public bool GetCharPref(string prefName, out string value)
		{
			if (string.IsNullOrEmpty(prefName)) throw new ArgumentException("prefName");

			int existingType = _branch.Instance.GetPrefType(prefName);
			switch (existingType)
			{
				case PREF_INVALID:
					value = null;
					return true;
				case PREF_STRING:
					value = GetUnicodePref(_branch.Instance, prefName);
					return true;
				default:
					value = null;
					return false;
			}
		}

		public bool SetCharPref(string prefName, string value)
		{
			if (string.IsNullOrEmpty(prefName)) throw new ArgumentException("prefName");

			int existingType = _branch.Instance.GetPrefType(prefName);
			switch (existingType)
			{
				case PREF_INVALID:
					if (value != null)
					{
						SetUnicodePref(_branch.Instance, prefName, value);
					}
					return true;
				case PREF_STRING:
					if (value != null)
					{
						SetUnicodePref(_branch.Instance, prefName, value);
					}
					else
					{
						_branch.Instance.ClearUserPref(prefName);
					}
					return true;
				default:
					return false;
			}
		}
		#endregion

		#region float pref
		public bool GetFloatPref( string prefName,float? value )
		{
			if (string.IsNullOrEmpty(prefName)) throw new ArgumentException("prefName");

			int existingType = _branch.Instance.GetPrefType( prefName );
			switch (existingType)
			{
				// not exist
				case PREF_INVALID:
					value = null;
					return true;
				// float is stored as string
				case PREF_STRING:
					value=_branch.Instance.GetFloatPref( prefName );
					return true;
				default:
					value = null;
					return false;
			}
		}

		public bool SetFloatPref(string prefName, float? value)
		{
			if (string.IsNullOrEmpty(prefName)) throw new ArgumentException("prefName");

			int existingType = _branch.Instance.GetPrefType(prefName);
			switch (existingType)
			{
				case PREF_INVALID:
					if (value != null)
					{
						SetUnicodePref(_branch.Instance, prefName, value.Value.ToString());
					}
					return true;
				case PREF_STRING:
					if (value != null)
					{
						SetUnicodePref(_branch.Instance, prefName, value.Value.ToString());
					}
					else
					{
						_branch.Instance.ClearUserPref(prefName);
					}
					return true;
				default:
					return false;
			}
		}
		#endregion
		#endregion

		/// <summary>
		/// Gets the <see cref="Type"/> of the specified preference.
		/// </summary>
		/// <param name="name">Required. The name of the preference whose type is returned.</param>
		/// <returns></returns>
		public Type GetPreferenceType(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("name");
			
			switch (_branch.Instance.GetPrefType(name))
			{
				case PREF_STRING: return typeof(string);
				case PREF_INT: return typeof(int);
				case PREF_BOOL: return typeof(bool);
			}
			return null;
		}
		
		/// <summary>
		/// Sets whether the specified preference is locked. Locking a preference will cause the preference service to always return the default value regardless of whether there is a user set value or not.
		/// </summary>
		/// <param name="name">Required. The preference to lock or unlock.</param>
		/// <param name="locked">True if the preference should be locked; otherwise, false, and the preference is unlocked.</param>
		public void SetLocked(string name, bool locked)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("name");
			
			if (locked)
				_branch.Instance.LockPref(name);
			else
				_branch.Instance.UnlockPref(name);
		}
		
		/// <summary>
		/// Gets whether the specified preference is locked.
		/// </summary>
		/// <param name="name">Required. The preference whose lock status is returned.</param>
		/// <returns></returns>
		public bool GetLocked(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("name");

			return _branch.Instance.PrefIsLocked(name);
		}

		/// <summary>
		/// Clear user preferences
		/// </summary>
		/// <param name="name">Required. The preference to lock or unlock.</param>
		public void Clear(string name) {
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("name");

			_branch.Instance.ClearUserPref(name);
		}
	}
}
