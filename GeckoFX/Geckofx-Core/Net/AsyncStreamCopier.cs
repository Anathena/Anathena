namespace Gecko.Net
{
	public class AsyncStreamCopier
		:Request
	{
		private nsIAsyncStreamCopier _asyncStreamCopier;

		public AsyncStreamCopier(nsIAsyncStreamCopier asyncStreamCopier)
			:base(asyncStreamCopier)
		{
			_asyncStreamCopier = asyncStreamCopier;
		}
	}
}