namespace Gecko.Search
{
	public sealed class SearchEngine
	{
		internal nsISearchEngine _searchEngine;

		private SearchEngine(nsISearchEngine searchEngine)
		{
			_searchEngine = searchEngine;
		}

		public void AddParam(string name,string value,string responseType)
		{
			nsString.Set( _searchEngine.AddParam, name, value, responseType );
		}

		public string Name
		{
			get { return nsString.Get( _searchEngine.GetNameAttribute ); }
		}

		public string SearchForm
		{
			get { return nsString.Get( _searchEngine.GetSearchFormAttribute ); }
		}


		public string Alias
		{
			get { return nsString.Get( _searchEngine.GetAliasAttribute ); }
			set { nsString.Set( _searchEngine.SetAliasAttribute, value ); }
		}

		public string Description
		{
			get { return nsString.Get( _searchEngine.GetDescriptionAttribute ); }
		}

		public bool Hidden
		{
			get { return _searchEngine.GetHiddenAttribute(); }
			set{_searchEngine.SetHiddenAttribute( value );}
		}

		public object GetSubmission(string data, string responseType, string purpose)
		{
			return nsString.Pass<nsISearchSubmission>(_searchEngine.GetSubmission, data, responseType, purpose);
		}

		internal static SearchEngine Create(nsISearchEngine searchEngine)
		{
			return searchEngine == null ? null : new SearchEngine( searchEngine );
		}
	}
}