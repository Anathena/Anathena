using System;
using System.Collections.Generic;
using System.Text;

namespace Gecko.DOM
{
    internal static class DOMSelector
    {
    	private static readonly Dictionary<string, GeckoClassDesc> _dictionary =
    		new Dictionary<string, GeckoClassDesc>();

        private struct GeckoClassDesc
        {
            public string TagName { get; set; }
            public Type InterfaceType { get; set; }
            public Type GeckoElement { get; set; }

			public ObjectCreator CreationMethod { get; set; }
			public delegate GeckoHtmlElement ObjectCreator(object htmlElementInterface);
        }

		private static void Add(GeckoClassDesc classDesc)
		{
			_dictionary.Add( classDesc.TagName.ToLower(), classDesc );
		}

        static DOMSelector()
        {
            Add(new GeckoClassDesc() { TagName = "a", InterfaceType = typeof(nsIDOMHTMLAnchorElement), GeckoElement = typeof(GeckoAnchorElement), 
				CreationMethod = (x) => new GeckoAnchorElement((nsIDOMHTMLAnchorElement)x) });
			Add(new GeckoClassDesc() { TagName = "applet", InterfaceType = typeof(nsIDOMHTMLAppletElement), GeckoElement = typeof(GeckoAppletElement), 
				CreationMethod = (x) => new GeckoAppletElement((nsIDOMHTMLAppletElement)x) });
			Add(new GeckoClassDesc() { TagName = "area", InterfaceType = typeof(nsIDOMHTMLAreaElement), GeckoElement = typeof(GeckoAreaElement), 
				CreationMethod = (x) => new GeckoAreaElement((nsIDOMHTMLAreaElement)x) });
			Add(new GeckoClassDesc() { TagName = "base", InterfaceType = typeof(nsIDOMHTMLBaseElement), GeckoElement = typeof(GeckoBaseElement), 
				CreationMethod = (x) => new GeckoBaseElement((nsIDOMHTMLBaseElement)x) });
			Add(new GeckoClassDesc() { TagName = "body", InterfaceType = typeof(nsIDOMHTMLBodyElement), GeckoElement = typeof(GeckoBodyElement), 
				CreationMethod = (x) => new GeckoBodyElement((nsIDOMHTMLBodyElement)x) });
			Add(new GeckoClassDesc() { TagName = "br", InterfaceType = typeof(nsIDOMHTMLBRElement), GeckoElement = typeof(GeckoBRElement), 
				CreationMethod = (x) => new GeckoBRElement((nsIDOMHTMLBRElement)x) });
			Add(new GeckoClassDesc() { TagName = "button", InterfaceType = typeof(nsIDOMHTMLButtonElement), GeckoElement = typeof(GeckoButtonElement), 
				CreationMethod = (x) => new GeckoButtonElement((nsIDOMHTMLButtonElement)x) });
			Add(new GeckoClassDesc() { TagName = "canvas", InterfaceType = typeof(nsIDOMHTMLCanvasElement), GeckoElement = typeof(GeckoCanvasElement),
				CreationMethod = (x) => new GeckoCanvasElement((nsIDOMHTMLCanvasElement)x) });
			Add(new GeckoClassDesc() { TagName = "dir", InterfaceType = typeof(nsIDOMHTMLDirectoryElement), GeckoElement = typeof(GeckoDirectoryElement), 
				CreationMethod = (x) => new GeckoDirectoryElement((nsIDOMHTMLDirectoryElement)x) });
			Add(new GeckoClassDesc() { TagName = "div", InterfaceType = typeof(nsIDOMHTMLDivElement), GeckoElement = typeof(GeckoDivElement), 
				CreationMethod = (x) => new GeckoDivElement((nsIDOMHTMLDivElement)x) });
			Add(new GeckoClassDesc() { TagName = "embed", InterfaceType = typeof(nsIDOMHTMLEmbedElement), GeckoElement = typeof(GeckoEmbedElement), 
				CreationMethod = (x) => new GeckoEmbedElement((nsIDOMHTMLEmbedElement)x) });
			Add(new GeckoClassDesc() { TagName = "fieldset", InterfaceType = typeof(nsIDOMHTMLFieldSetElement), GeckoElement = typeof(GeckoFieldSetElement), 
				CreationMethod = (x) => new GeckoFieldSetElement((nsIDOMHTMLFieldSetElement)x) });
			Add(new GeckoClassDesc() { TagName = "form", InterfaceType = typeof(nsIDOMHTMLFormElement), GeckoElement = typeof(GeckoFormElement), 
				CreationMethod = (x) => new GeckoFormElement((nsIDOMHTMLFormElement)x) });
			Add(new GeckoClassDesc() { TagName = "frame", InterfaceType = typeof(nsIDOMHTMLFrameElement), GeckoElement = typeof(GeckoFrameElement), 
				CreationMethod = (x) => new GeckoFrameElement((nsIDOMHTMLFrameElement)x) });
			Add(new GeckoClassDesc() { TagName = "frameset", InterfaceType = typeof(nsIDOMHTMLFrameSetElement), GeckoElement = typeof(GeckoFrameSetElement), 
				CreationMethod = (x) => new GeckoFrameSetElement((nsIDOMHTMLFrameSetElement)x) });
			Add(new GeckoClassDesc() { TagName = "head", InterfaceType = typeof(nsIDOMHTMLHeadElement), GeckoElement = typeof(GeckoHeadElement), 
				CreationMethod = (x) => new GeckoHeadElement((nsIDOMHTMLHeadElement)x) });
			Add(new GeckoClassDesc() { TagName = "h1", InterfaceType = typeof(nsIDOMHTMLHeadingElement), GeckoElement = typeof(GeckoHeadingElement), 
				CreationMethod = (x) => new GeckoHeadingElement((nsIDOMHTMLHeadingElement)x) });
			Add(new GeckoClassDesc() { TagName = "h2", InterfaceType = typeof(nsIDOMHTMLHeadingElement), GeckoElement = typeof(GeckoHeadingElement), 
				CreationMethod = (x) => new GeckoHeadingElement((nsIDOMHTMLHeadingElement)x) });
			Add(new GeckoClassDesc() { TagName = "h3", InterfaceType = typeof(nsIDOMHTMLHeadingElement), GeckoElement = typeof(GeckoHeadingElement), 
				CreationMethod = (x) => new GeckoHeadingElement((nsIDOMHTMLHeadingElement)x) });
			Add(new GeckoClassDesc() { TagName = "h4", InterfaceType = typeof(nsIDOMHTMLHeadingElement), GeckoElement = typeof(GeckoHeadingElement), 
				CreationMethod = (x) => new GeckoHeadingElement((nsIDOMHTMLHeadingElement)x) });
			Add(new GeckoClassDesc() { TagName = "h5", InterfaceType = typeof(nsIDOMHTMLHeadingElement), GeckoElement = typeof(GeckoHeadingElement), 
				CreationMethod = (x) => new GeckoHeadingElement((nsIDOMHTMLHeadingElement)x) });
			Add(new GeckoClassDesc() { TagName = "h6", InterfaceType = typeof(nsIDOMHTMLHeadingElement), GeckoElement = typeof(GeckoHeadingElement), 
				CreationMethod = (x) => new GeckoHeadingElement((nsIDOMHTMLHeadingElement)x) });
			Add(new GeckoClassDesc() { TagName = "hr", InterfaceType = typeof(nsIDOMHTMLHRElement), GeckoElement = typeof(GeckoHRElement), 
				CreationMethod = (x) => new GeckoHRElement((nsIDOMHTMLHRElement)x) });
			Add(new GeckoClassDesc() { TagName = "html", InterfaceType = typeof(nsIDOMHTMLHtmlElement), GeckoElement = typeof(GeckoHtmlHtmlElement), 
				CreationMethod = (x) => new GeckoHtmlHtmlElement((nsIDOMHTMLHtmlElement)x) });
			Add(new GeckoClassDesc() { TagName = "iframe", InterfaceType = typeof(nsIDOMHTMLIFrameElement), GeckoElement = typeof(GeckoIFrameElement), 
				CreationMethod = (x) => new GeckoIFrameElement((nsIDOMHTMLIFrameElement)x) });
			Add(new GeckoClassDesc() { TagName = "img", InterfaceType = typeof(nsIDOMHTMLImageElement), GeckoElement = typeof(GeckoImageElement), 
				CreationMethod = (x) => new GeckoImageElement((nsIDOMHTMLImageElement)x) });
			Add(new GeckoClassDesc() { TagName = "input", InterfaceType = typeof(nsIDOMHTMLInputElement), GeckoElement = typeof(GeckoInputElement), 
				CreationMethod = (x) => new GeckoInputElement((nsIDOMHTMLInputElement)x) });			
			Add(new GeckoClassDesc() { TagName = "label", InterfaceType = typeof(nsIDOMHTMLLabelElement), GeckoElement = typeof(GeckoLabelElement), 
				CreationMethod = (x) => new GeckoLabelElement((nsIDOMHTMLLabelElement)x) });
			Add(new GeckoClassDesc() { TagName = "li", InterfaceType = typeof(nsIDOMHTMLLIElement), GeckoElement = typeof(GeckoLIElement), 
				CreationMethod = (x) => new GeckoLIElement((nsIDOMHTMLLIElement)x) });
			Add(new GeckoClassDesc() { TagName = "link", InterfaceType = typeof(nsIDOMHTMLLinkElement), GeckoElement = typeof(GeckoLinkElement), 
				CreationMethod = (x) => new GeckoLinkElement((nsIDOMHTMLLinkElement)x) });
			Add(new GeckoClassDesc() { TagName = "map", InterfaceType = typeof(nsIDOMHTMLMapElement), GeckoElement = typeof(GeckoMapElement), 
				CreationMethod = (x) => new GeckoMapElement((nsIDOMHTMLMapElement)x) });
			Add(new GeckoClassDesc() { TagName = "menu", InterfaceType = typeof(nsIDOMHTMLMenuElement), GeckoElement = typeof(GeckoMenuElement), 
				CreationMethod = (x) => new GeckoMenuElement((nsIDOMHTMLMenuElement)x) });
			Add(new GeckoClassDesc() { TagName = "meta", InterfaceType = typeof(nsIDOMHTMLMetaElement), GeckoElement = typeof(GeckoMetaElement), 
				CreationMethod = (x) => new GeckoMetaElement((nsIDOMHTMLMetaElement)x) });
			Add(new GeckoClassDesc() { TagName = "object", InterfaceType = typeof(nsIDOMHTMLObjectElement), GeckoElement = typeof(GeckoObjectElement), 
				CreationMethod = (x) => new GeckoObjectElement((nsIDOMHTMLObjectElement)x) });
			Add(new GeckoClassDesc() { TagName = "ol", InterfaceType = typeof(nsIDOMHTMLOListElement), GeckoElement = typeof(GeckoOListElement), 
				CreationMethod = (x) => new GeckoOListElement((nsIDOMHTMLOListElement)x) });
			Add(new GeckoClassDesc() { TagName = "optgroup", InterfaceType = typeof(nsIDOMHTMLOptGroupElement), GeckoElement = typeof(GeckoOptGroupElement), 
				CreationMethod = (x) => new GeckoOptGroupElement((nsIDOMHTMLOptGroupElement)x) });
			Add(new GeckoClassDesc() { TagName = "option", InterfaceType = typeof(nsIDOMHTMLOptionElement), GeckoElement = typeof(GeckoOptionElement), 
				CreationMethod = (x) => new GeckoOptionElement((nsIDOMHTMLOptionElement)x) });
			Add(new GeckoClassDesc() { TagName = "p", InterfaceType = typeof(nsIDOMHTMLParagraphElement), GeckoElement = typeof(GeckoParagraphElement), 
				CreationMethod = (x) => new GeckoParagraphElement((nsIDOMHTMLParagraphElement)x) });
			Add(new GeckoClassDesc() { TagName = "pre", InterfaceType = typeof(nsIDOMHTMLPreElement), GeckoElement = typeof(GeckoPreElement), 
				CreationMethod = (x) => new GeckoPreElement((nsIDOMHTMLPreElement)x) });
			Add(new GeckoClassDesc() { TagName = "q", InterfaceType = typeof(nsIDOMHTMLQuoteElement), GeckoElement = typeof(GeckoQuoteElement), 
				CreationMethod = (x) => new GeckoQuoteElement((nsIDOMHTMLQuoteElement)x) });
			Add(new GeckoClassDesc() { TagName = "script", InterfaceType = typeof(nsIDOMHTMLScriptElement), GeckoElement = typeof(GeckoScriptElement), 
				CreationMethod = (x) => new GeckoScriptElement((nsIDOMHTMLScriptElement)x) });
			Add(new GeckoClassDesc() { TagName = "select", InterfaceType = typeof(nsIDOMHTMLSelectElement), GeckoElement = typeof(GeckoSelectElement), 
				CreationMethod = (x) => new GeckoSelectElement((nsIDOMHTMLSelectElement)x) });
			Add(new GeckoClassDesc() { TagName = "style", InterfaceType = typeof(nsIDOMHTMLStyleElement), GeckoElement = typeof(GeckoStyleElement),
				CreationMethod = (x) => new GeckoStyleElement((nsIDOMHTMLStyleElement)x) });
			Add(new GeckoClassDesc() { TagName = "caption", InterfaceType = typeof(nsIDOMHTMLTableCaptionElement), GeckoElement = typeof(GeckoTableCaptionElement),
				CreationMethod = (x) => new GeckoTableCaptionElement((nsIDOMHTMLTableCaptionElement)x) });
			Add(new GeckoClassDesc() { TagName = "td", InterfaceType = typeof(nsIDOMHTMLTableCellElement), GeckoElement = typeof(GeckoTableCellElement),
				CreationMethod = (x) => new GeckoTableCellElement((nsIDOMHTMLTableCellElement)x) });
			Add(new GeckoClassDesc() { TagName = "textarea", InterfaceType = typeof(nsIDOMHTMLTextAreaElement), GeckoElement = typeof(GeckoTextAreaElement), 
				CreationMethod = (x) => new GeckoTextAreaElement((nsIDOMHTMLTextAreaElement)x) });
			Add(new GeckoClassDesc() { TagName = "title", InterfaceType = typeof(nsIDOMHTMLTitleElement), GeckoElement = typeof(GeckoTitleElement), 
				CreationMethod = (x) => new GeckoTitleElement((nsIDOMHTMLTitleElement)x) });
			Add(new GeckoClassDesc() { TagName = "ul", InterfaceType = typeof(nsIDOMHTMLUListElement), GeckoElement = typeof(GeckoUListElement), 
				CreationMethod = (x) => new GeckoUListElement((nsIDOMHTMLUListElement)x) });
        }
		
		static GeckoWrapperCache<nsIDOMHTMLElement, GeckoHtmlElement> m_cache = new GeckoWrapperCache<nsIDOMHTMLElement, GeckoHtmlElement>(CreateDomHtmlElementWrapper);

		internal static GeckoHtmlElement GetClassFor(nsIDOMHTMLElement element)
		{
			GeckoHtmlElement ret = GetClassFor<GeckoHtmlElement>(element);
			if (ret == null)
				ret = new GeckoHtmlElement(element);
			return ret;
		}

        internal static T GetClassFor<T>(nsIDOMHTMLElement element)  where T : GeckoHtmlElement
        {
			return (T)m_cache.Get(element);
        }

		/// <summary>
		/// Creates wrapper for nsIDOMNode object	
		/// </summary>
		/// <param name="domObject"></param>
		/// <returns></returns>
		internal static GeckoNode CreateDomNodeWrapper(nsIDOMNode domObject)
		{
			// if null -> return null
			if (domObject == null) return null;
			var nodeType = (NodeType)domObject.GetNodeTypeAttribute();
			// by nodeType we can find proper wrapper faster, than perform QueryInterface
			switch (nodeType)
			{
				case NodeType.Element:
					nsIDOMHTMLElement htmlElement = Xpcom.QueryInterface<nsIDOMHTMLElement>(domObject);
					if (htmlElement != null) return GeckoHtmlElement.Create(htmlElement);
					nsIDOMElement element = Xpcom.QueryInterface<nsIDOMElement>(domObject);
					if (element != null) return GeckoElement.CreateDomElementWrapper(element);
					break;
				case NodeType.Attribute:
					nsIDOMAttr attr = Xpcom.QueryInterface<nsIDOMAttr>(domObject);
					if (attr != null) return GeckoAttribute.CreateAttributeWrapper(attr);
					break;
				case NodeType.Comment:
					nsIDOMComment comment = Xpcom.QueryInterface<nsIDOMComment>(domObject);
					if (comment != null) return GeckoComment.CreateCommentWrapper(comment);
					break;
				case NodeType.DocumentFragment:
					nsIDOMDocumentFragment fragment = Xpcom.QueryInterface<nsIDOMDocumentFragment>(domObject);
					if (fragment != null) return DOM.DocumentFragment.CreateDocumentFragmentWrapper(fragment);
					break;
			}
			// if we don't handle this type - just create GeckoNode
			return new GeckoNode(domObject);
		}

		internal static GeckoHtmlElement CreateDomHtmlElementWrapper(nsIDOMHTMLElement instance)
		{
			var lowerTagName = nsString.Get(instance.GetTagNameAttribute).ToLower();
        	GeckoClassDesc desc;

			if (_dictionary.TryGetValue(lowerTagName, out desc))
			{
				object HTMLElement = Xpcom.QueryInterface(instance, desc.InterfaceType.GUID);
				if(HTMLElement != null)
				{
					GeckoHtmlElement e = desc.CreationMethod(HTMLElement);
					return e;
				}
			}
			return null;
		}

		
    }
}

