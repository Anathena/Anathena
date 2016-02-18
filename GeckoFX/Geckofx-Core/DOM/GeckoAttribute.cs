namespace Gecko
{
	/// <summary>
	/// Represents a DOM attribute.
	/// </summary>
	public class GeckoAttribute : GeckoNode
	{
		internal GeckoAttribute(nsIDOMAttr attr) : base(attr)
		{
			this.DomAttr = attr;
		}
		internal nsIDOMAttr DomAttr;
		
		internal static GeckoAttribute CreateAttributeWrapper(nsIDOMAttr attr)
		{
			return (attr == null) ? null : new GeckoAttribute(attr);
		}
		
		/// <summary>
		/// Gets the name of the attribute.
		/// </summary>
		public string Name
		{
			get { return nsString.Get(DomAttr.GetNameAttribute); }
		}
		
		/// <summary>
		/// Gets the <see cref="GeckoHtmlElement"/> which contains this attribute.
		/// </summary>
		public GeckoNode OwnerDocument
		{
			get { return GeckoNode.Create((nsIDOMHTMLElement)DomAttr.GetOwnerDocumentAttribute()); }
		}
		
		/// <summary>
		/// Gets a value indicating whether the attribute is specified.
		/// </summary>
		public bool Specified
		{
			get { return DomAttr.GetSpecifiedAttribute(); }
		}

		/// <summary>
		/// Gets the value of the attribute.
		/// </summary>
		public string Value
		{
			get { return nsString.Get(DomAttr.GetValueAttribute); }
			set { nsString.Set(DomAttr.SetValueAttribute, value); }
		}
	}
}