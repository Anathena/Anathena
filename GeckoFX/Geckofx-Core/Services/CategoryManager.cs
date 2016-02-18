using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Collections;
using Gecko.Interop;

namespace Gecko
{
	public static class CategoryManager
	{
		private static ComPtr<nsICategoryManager> _categoryManager;

		static CategoryManager()
		{
			_categoryManager = Xpcom.GetService2<nsICategoryManager>(Contracts.CategoryManager);
		}

		public static string AddCategoryEntry(string aCategory, string aEntry, string aValue, bool aPersist, bool aReplace)
		{
			// check if it exists to prevent crash
			string value = null;
			try
			{
				value = _categoryManager.Instance.GetCategoryEntry(aCategory, aEntry);
			}
			catch ( Exception )
			{
			}
			
			if (value != null)
			{
				_categoryManager.Instance.DeleteCategoryEntry(aCategory, aEntry, aPersist);
			}

			return _categoryManager.Instance.AddCategoryEntry(aCategory, aEntry, aValue, aPersist, aReplace);
		}

		public static void DeleteCategory(string aCategory)
		{
			_categoryManager.Instance.DeleteCategory(aCategory);	
		}

		public static void DeleteCategoryEntry(string aCategory, string aEntry, bool aPersist) 
		{
			_categoryManager.Instance.DeleteCategoryEntry(aCategory, aEntry, aPersist);
		}

		public static string GetCategoryEntry(string aCategory, string aEntry)
		{
			return _categoryManager.Instance.GetCategoryEntry(aCategory, aEntry);
		}

		public static IEnumerable<string> Categories
		{
			get
			{
				return new GeckoEnumerableCollection<string, nsISupportsCString>(() => _categoryManager.Instance.EnumerateCategories(),
				                                                                  nsSupportsPrimitiveConverter.GetString );
			}
		}

		public static IEnumerable<string> GetCategoryEntries(string category)
		{
			return new GeckoEnumerableCollection<string, nsISupportsCString>(
					() => _categoryManager.Instance.EnumerateCategory( category ),
					nsSupportsPrimitiveConverter.GetString );

		}

		#region Categories List - TODO
		public static string ContentPolicy = "content-policy";
		#endregion


	}
}
