using System;
using System.Runtime.InteropServices;
using Gecko.Interop;

namespace Gecko.Search
{
	public static class BrowserSearchService
	{
		private static ComPtr<nsIBrowserSearchService> _browserSearchService;

		static BrowserSearchService()
		{
			_browserSearchService = Xpcom.GetService2<nsIBrowserSearchService>(Contracts.BrowserSearchService);
		}

		/// <summary>
		/// Adds a new search engine from the file at the supplied URI, optionally
		/// asking the user for confirmation first.  If a confirmation dialog is
		/// shown, it will offer the option to begin using the newly added engine
		/// right away; if no confirmation dialog is shown, the new engine will be
		/// used right away automatically.
		///	</summary>
		/// <param name="engineUrl">The URL to the search engine's description file.</param>
		/// <param name="dataType">
		/// An integer representing the plugin file format. Must be one
		/// of the supported search engine data types defined above.
		/// </param>
		/// <param name="iconUrl">
		/// A URL string to an icon file to be used as the search engine's
		/// icon. This value may be overridden by an icon specified in the
		/// engine description file.
		///	</param>
		/// <param name="confirm">
		/// A boolean value indicating whether the user should be asked for
		/// confirmation before this engine is added to the list.  If this
		/// value is false, the engine will be added to the list upon successful
		/// load, but it will not be selected as the current engine.
		/// </param>
		/// <exception cref="COMException">
		/// @throws NS_ERROR_FAILURE if the type is invalid, or if the description
		/// file cannot be successfully loaded.
		/// </exception>
		public static void AddEngine(string engineUrl, int dataType, string iconUrl, bool confirm)
		{
			using (nsAString native1=new nsAString(engineUrl),native2=new nsAString(iconUrl))
			{
				_browserSearchService.Instance.AddEngine( native1, dataType, native2, confirm, null);
			}
		}

		/// <summary>
		/// Adds a new search engine, without asking the user for confirmation and
		/// without starting to use it right away.
		/// </summary>
		/// <param name="name">The search engine's name. Must be unique. Must not be null.</param>
		/// <param name="iconUrl">
		/// Optional: A URL string pointing to the icon to be used to represent
		/// the engine.
		/// </param>
		/// <param name="alias">
		/// Optional: A unique shortcut that can be used to retrieve the
		/// search engine.
		/// </param>
		///	<param name="description">Optional: a description of the search engine.</param>
		/// <param name="method">
		/// The HTTP request method used when submitting a search query.
		/// Must be a case insensitive value of either "get" or "post".
		/// </param>
		///	<param name="url">
		/// The URL to which search queries should be sent.
		/// Must not be null.
		/// </param>
		public static void AddEngineWithDetails(string name, string iconUrl, string alias, string description, string method, string url)
		{
			if (string.IsNullOrEmpty( name )) throw new ArgumentException("Must not be null","name");

			using (nsAString native1 = new nsAString(name),
				native2 = new nsAString(iconUrl),
				native3 = new nsAString(alias),
				native4 = new nsAString(description),
				native5 = new nsAString(method),
				native6=new nsAString(url))
			{
				_browserSearchService.Instance.AddEngineWithDetails(native1, native2, native3, native4, native5, native6);
			}
		}

		/// <summary>
		/// Un-hides all engines installed in the directory corresponding to
		/// the directory service's NS_APP_SEARCH_DIR key. (i.e. the set of
		/// engines returned by getDefaultEngines)
		/// </summary>
		public static void RestoreDefaultEngines()
		{
			_browserSearchService.Instance.RestoreDefaultEngines();
		}

		/// <summary>
		/// Returns an engine with the specified alias.
		/// </summary>
		/// <param name="alias">The search engine's alias.</param>
		/// <returns>The corresponding SearchEngine object, or null if it doesn't exist.</returns>
		public static SearchEngine GetEngineByAlias(string alias)
		{
			var ret = nsString.Pass<nsISearchEngine>(_browserSearchService.Instance.GetEngineByAlias, alias);
			return SearchEngine.Create(ret);
		}

		/// <summary>
		/// Returns an engine with the specified name.
		/// </summary>
		/// <param name="engineName">The name of the engine.</param>
		/// <returns>The corresponding SearchEngine object, or null if it doesn't exist.</returns>
		public static SearchEngine GetEngineByName(string engineName)
		{
			var ret = nsString.Pass<nsISearchEngine>(_browserSearchService.Instance.GetEngineByName, engineName);
			return SearchEngine.Create( ret );
		}

		/// <summary>
		/// Returns an array of all installed search engines.
		///
		/// @returns an array of nsISearchEngine objects.
		/// </summary>
	
		//void GetEngines(ref uint engineCount, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] ref nsISearchEngine[] engines);

		/// <summary>
		/// Returns an array of all installed search engines whose hidden attribute is
		/// false.
		///
		/// @returns an array of nsISearchEngine objects.
		/// </summary>
		
		//void GetVisibleEngines(ref uint engineCount, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] ref nsISearchEngine[] engines);

		/// <summary>
		/// Returns an array of all default search engines. This includes all loaded
		/// engines that aren't in the user's profile directory
		/// (NS_APP_USER_SEARCH_DIR).
		///
		/// @returns an array of nsISearchEngine objects.
		/// </summary>
		
		//void GetDefaultEngines(ref uint engineCount, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] ref nsISearchEngine[] engines);

		/// <summary>
		/// Moves a visible search engine.
		///
		/// @param  engine
		/// The engine to move.
		/// @param  newIndex
		/// The engine's new index in the set of visible engines.
		///
		/// @throws NS_ERROR_FAILURE if newIndex is out of bounds, or if engine is
		/// hidden.
		/// </summary>
		public static void MoveEngine(SearchEngine engine, int newIndex)
		{
			_browserSearchService.Instance.MoveEngine(engine._searchEngine, newIndex);
		}

		/// <summary>
		/// Removes the search engine. If the search engine is installed in a global
		/// location, this will just hide the engine. If the engine is in the user's
		/// profile directory, it will be removed from disk.
		///
		/// @param  engine
		/// The engine to remove.
		/// </summary>
		public static void RemoveEngine(SearchEngine engine)
		{
			_browserSearchService.Instance.RemoveEngine(engine._searchEngine);
		}

		/// <summary>
		/// The default search engine. Returns the first visible engine if the default
		/// engine is hidden. May be null if there are no visible search engines.
		/// </summary>

		public static SearchEngine DefaultEngine
		{
			get { return SearchEngine.Create(_browserSearchService.Instance.GetDefaultEngineAttribute()); }
		}

		/// <summary>
		/// The currently active search engine. May be null if there are no visible
		/// search engines.
		/// </summary>
		public static SearchEngine CurrentEngine
		{
			get { return SearchEngine.Create(_browserSearchService.Instance.GetCurrentEngineAttribute()); }
			set { _browserSearchService.Instance.SetCurrentEngineAttribute(value._searchEngine); }
		}
	}
}
