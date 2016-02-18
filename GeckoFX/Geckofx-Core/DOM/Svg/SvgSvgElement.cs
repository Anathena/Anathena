namespace Gecko.DOM.Svg
{
	public class SvgSvgElement
		:SvgElement
	{
		protected SvgSvgElement(nsIDOMSVGElement svgSvgElement)
			:base(svgSvgElement)
		{
			
		}

		public static SvgSvgElement CreateSvgSvgElementWrapper(nsIDOMSVGElement svgSvgElement)
		{
			return svgSvgElement == null ? null : new SvgSvgElement( svgSvgElement );
		}
	}
}