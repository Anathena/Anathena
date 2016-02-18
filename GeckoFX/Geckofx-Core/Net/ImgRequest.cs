namespace Gecko.Net
{
	public class ImgRequest
		: Request
	{
		private imgIRequest _imgRequest;

		public ImgRequest( imgIRequest request )
			: base( request )
		{
			_imgRequest = request;
		}
	}
}