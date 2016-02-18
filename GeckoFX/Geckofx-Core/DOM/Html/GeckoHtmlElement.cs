using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Gecko.Interop;

namespace Gecko
{
	/// <summary>
	/// Represents a DOM HTML element.
	/// </summary>
	public class GeckoHtmlElement
		: GeckoElement
	{
		private nsIDOMHTMLElement _domHtmlElement;

		//nsIDOMElement DomNSElement;

		#region ctor
		internal GeckoHtmlElement(nsIDOMHTMLElement element)
			: base(element)
		{
			_domHtmlElement = element;
			//this.DomNSElement = (nsIDOMElement)element;
		}

		internal GeckoHtmlElement(object element)
			: base(element)
		{
			if (element is nsIDOMHTMLElement)
				_domHtmlElement = (nsIDOMHTMLElement)element;
			else
				throw new ArgumentException("element is not a nsIDOMHTMLElement");
			
		}
		
		internal static GeckoHtmlElement Create(nsIDOMHTMLElement element)
		{
			return (element == null) ? null : DOM.DOMSelector.GetClassFor(element);
		}

		internal static T Create<T>(nsIDOMHTMLElement element) where T : GeckoHtmlElement 
		{
			return (element == null) ? null : DOM.DOMSelector.GetClassFor<T>(element);
		}
		#endregion

		public nsIDOMHTMLElement DOMHtmlElement
		{
			get { return _domHtmlElement; }
		}


		
		/// <summary>
		/// Gets the inline style of the GeckoElement. 
		/// </summary>
		public GeckoStyle Style
		{
			get
			{
				var cssInlineStyle = Xpcom.QueryInterface<nsIDOMElementCSSInlineStyle>( DomObject );
				if ( cssInlineStyle == null ) return null;
				var ret = cssInlineStyle.GetStyleAttribute().Wrap( GeckoStyle.Create );
				Marshal.ReleaseComObject( cssInlineStyle );
				return ret;
			}
		}

		/// <summary>
		/// Gets style of the GeckoElement. 
		/// </summary>
		public GeckoStyle ComputedStyle
		{
			get
			{
				nsIDOMCSSStyleDeclaration style;
				using (var element = new ComPtr<nsIDOMElement>(Xpcom.QueryInterface<nsIDOMElement>(this.DomObject)))
				{
					using (var nullString = new nsAString())
					{
						nullString.SetData(null);
						style = this.OwnerDocument.DefaultView.DomWindow.GetComputedStyle(element.Instance, nullString);
					}
				}
				return GeckoStyle.Create(style);
			}
		}

		/// <summary>
		/// Gets the parent element of this one.
		/// </summary>
		public GeckoHtmlElement Parent
		{
			get
			{
				// note: the parent node could also be the document
				return GeckoHtmlElement.Create( Xpcom.QueryInterface<nsIDOMHTMLElement>( _domHtmlElement.GetParentNodeAttribute() ) );
			}
		}


		
		/// <summary>
		/// Gets the value of the id attribute.
		/// </summary>
		public string Id
		{
			get { return nsString.Get( _domHtmlElement.GetIdAttribute ); }
			set 
			{				
				if (string.IsNullOrEmpty(value))
					this.RemoveAttribute("id");
				else
					nsString.Set( _domHtmlElement.SetIdAttribute, value ); 
			}
		}
		
		/// <summary>
		/// Gets the value of the class attribute.
		/// </summary>
		public string ClassName
		{
			get { return nsString.Get( _domHtmlElement.GetClassNameAttribute ); }
			set 
			{
				if (string.IsNullOrEmpty(value))
					this.RemoveAttribute("class");
				else
					nsString.Set( _domHtmlElement.SetClassNameAttribute, value ); 
			}
		}

		public void Blur()
		{
			_domHtmlElement.Blur();
		}

		public string AccessKey
		{
			get { return nsString.Get( _domHtmlElement.GetAccessKeyAttribute ); }
			set { _domHtmlElement.SetAccessKeyAttribute( new nsAString( value ) ); }
		}

		public void Focus()
		{
			_domHtmlElement.Focus();
		}

		public void Click()
		{
			_domHtmlElement.Click();
		}

		public bool Draggable
		{
			get { return _domHtmlElement.GetDraggableAttribute(); }
			set { _domHtmlElement.SetDraggableAttribute( value ); }
		}
	
		/// <summary>
		/// Get the value of the ContentEditable Attribute
		/// </summary>
		public string ContentEditable
		{
			get { return nsString.Get( _domHtmlElement.GetContentEditableAttribute ); }
			set { nsString.Set( _domHtmlElement.GetContentEditableAttribute, value ); }
		}

		public System.Drawing.Rectangle[] ClientRects
		{
			get
			{
				nsIDOMClientRectList domRects = DOMHtmlElement.GetClientRects();
				uint count = domRects.GetLengthAttribute();
				Rectangle[] rects = new Rectangle[count];
				for (uint i = 0; i < count; i++)
				{
					nsIDOMClientRect domRect = domRects.Item(i);
					rects[i] = new Rectangle(
						(int) Math.Round(domRect.GetLeftAttribute()),
						(int) Math.Round(domRect.GetTopAttribute()),
						(int) Math.Round(domRect.GetWidthAttribute()),
						(int) Math.Round(domRect.GetHeightAttribute()));
				}
				return rects;
			}
		}

		public int OffsetLeft { get { return _domHtmlElement.GetOffsetLeftAttribute(); } }
		public int OffsetTop { get { return _domHtmlElement.GetOffsetTopAttribute(); } }
		public int OffsetWidth { get { return _domHtmlElement.GetOffsetWidthAttribute(); } }
		public int OffsetHeight { get { return _domHtmlElement.GetOffsetHeightAttribute(); } }


		

		
		
		public GeckoHtmlElement OffsetParent
		{
			get { return GeckoHtmlElement.Create( ( nsIDOMHTMLElement ) _domHtmlElement.GetOffsetParentAttribute() ); }
		}
		
		public void ScrollIntoView(bool top)
		{
			_domHtmlElement.ScrollIntoView( top, 1 );
		}
		

		public bool Spellcheck
		{
			get { return _domHtmlElement.GetSpellcheckAttribute(); }
			set { _domHtmlElement.SetSpellcheckAttribute( value ); }
		}

		public string InnerHtml
		{
			get { return nsString.Get( _domHtmlElement.GetInnerHTMLAttribute ); }
			set { nsString.Set( _domHtmlElement.SetInnerHTMLAttribute, value ); }
		}

		public virtual string OuterHtml
		{
			get { return nsString.Get( _domHtmlElement.GetOuterHTMLAttribute ); }			
		}
		
		public int TabIndex
		{
			get { return _domHtmlElement.GetTabIndexAttribute(); }
			set { _domHtmlElement.SetTabIndexAttribute( value ); }
		}

		public void InsertAdjacentHTML(string position, string text)
		{
			using(nsAString tempPos = new nsAString(position), tempText = new nsAString(text))
			{
				_domHtmlElement.InsertAdjacentHTML( tempPos, tempText );
			}
		}

		public void MozRequestFullScreen()
		{
			_domHtmlElement.MozRequestFullScreen();
		}
	}
}