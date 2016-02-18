using Gecko.Interop;

namespace Gecko.DOM
{
	public class GeckoTextNode
		:DomCharacterData
	{
		private nsIDOMText _domText;

		private GeckoTextNode(nsIDOMText domText)
			:base(domText)
		{
			_domText = domText;
		}

		public static GeckoTextNode CreateTextNodeWrapper(nsIDOMText domText)
		{
			return new GeckoTextNode( domText );
		}


		public GeckoTextNode SplitText(uint offset)
		{
			return _domText.SplitText( offset ).Wrap( CreateTextNodeWrapper );
		}


		public string WholeText
		{
			get { return nsString.Get( _domText.GetWholeTextAttribute ); }
		}
	}
}