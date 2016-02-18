using System;

namespace Gecko.Search
{
	public sealed class SearchSubmission
	{
		private nsISearchSubmission _searchSubmission;

		internal SearchSubmission(nsISearchSubmission searchSubmission)
		{
			_searchSubmission = searchSubmission;

			_searchSubmission.GetPostDataAttribute();
		}

		public Uri Uri
		{
			get { return Xpcom.TranslateUriAttribute( _searchSubmission.GetUriAttribute ); }
		}

		public IO.InputStream PostData
		{
			get { return IO.InputStream.Create( _searchSubmission.GetPostDataAttribute() ); }
		}
	}
}