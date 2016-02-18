using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Gecko.DOM;
using Gecko.Interop;

namespace Gecko
{
	/// <summary>
	/// Provides a base class for DOM nodes.
	/// </summary>
	public class GeckoNode
		:IDisposable
	{
		static GeckoWrapperCache<nsIDOMNode, GeckoNode> m_nodeCache = new GeckoWrapperCache<nsIDOMNode, GeckoNode>(DOMSelector.CreateDomNodeWrapper);

		#region fields
		ComPtr<nsIDOMNode> _domNode;
		#endregion

		#region ctor & creation methods
		internal GeckoNode(nsIDOMNode domObject)
		{
			//ComDebug.WriteDebugInfo( domObject );
			_domNode = new ComPtr<nsIDOMNode>( domObject );
		}

		internal GeckoNode(object domObject)
		{
			if (domObject is nsIDOMNode)
				_domNode = new ComPtr<nsIDOMNode>((nsIDOMNode)domObject);
			else
				throw new ArgumentException("domObject is not a nsIDOMNode");
		}

		~GeckoNode()
		{
			Xpcom.DisposeObject( ref _domNode );
		}

		public void Dispose()
		{
			Xpcom.DisposeObject(ref _domNode);
			GC.SuppressFinalize( this );
		}

		internal static GeckoNode Create(nsIDOMNode domObject)
		{
			return m_nodeCache.Get(domObject);
		}
		#endregion


		#region Properties for Native XPCOM objects access
		/// <summary>
		/// Gets the underlying XPCOM object.
		/// </summary>
		public nsIDOMNode DomObject
		{
			get { return _domNode.Instance; }
		}
		#endregion


		public override bool Equals(object obj)
		{
			if (this == obj) return true;
			if ( obj is GeckoNode )
			{
				// compare native objects, NOT wrappers
				return this._domNode.GetHashCode() == ( ( GeckoNode ) obj )._domNode.GetHashCode();
			}
			return false;
		}

		public override int GetHashCode()
		{
			return _domNode.GetHashCode();
			//IntPtr pUnk = Marshal.GetIUnknownForObject(this._domNode);
			//try
			//{
			//    return pUnk.GetHashCode();
			//}
			//finally
			//{
			//    if (pUnk != IntPtr.Zero)
			//        Marshal.Release(pUnk);
			//}
		}

		/// <summary>
		/// Gets the text contents of the node.
		/// </summary>
		public string TextContent
		{
			get { return nsString.Get(_domNode.Instance.GetTextContentAttribute); }
			set { nsString.Set(_domNode.Instance.SetTextContentAttribute, value); }
		}

		/// <summary>
		/// Gets or sets the value of the node.
		/// </summary>
		public string NodeValue
		{
			get { return nsString.Get(_domNode.Instance.GetNodeValueAttribute); }
			set { nsString.Set(_domNode.Instance.SetNodeValueAttribute, value); }
		}

		public string NodeName
		{
			get { return nsString.Get(_domNode.Instance.GetNodeNameAttribute); }
		}

		/// <summary>
		/// Gets a collection containing all child nodes of this node.
		/// </summary>
		public GeckoNodeCollection ChildNodes
		{
			get { return _domNode.Instance.GetChildNodesAttribute().Wrap(GeckoNodeCollection.Create); }
		}

		public GeckoNode FirstChild { get { return _domNode.Instance.GetFirstChildAttribute().Wrap(Create); } }
		public GeckoNode LastChild { get { return _domNode.Instance.GetLastChildAttribute().Wrap(Create); } }
		public GeckoNode NextSibling { get { return _domNode.Instance.GetNextSiblingAttribute().Wrap(Create); } }
		public GeckoNode PreviousSibling { get { return _domNode.Instance.GetPreviousSiblingAttribute().Wrap(Create); } }
		public bool HasChildNodes { get { return _domNode.Instance.HasChildNodes(); } }
		public bool HasAttributes { get { return _domNode.Instance.HasAttributes(); } }

		public GeckoDocument OwnerDocument
		{
			get { return GeckoDocument.Create(Xpcom.QueryInterface<nsIDOMHTMLDocument>(_domNode.Instance.GetOwnerDocumentAttribute())); }
		}

		public GeckoNode AppendChild(GeckoNode node)
		{
			if (node == null)
				throw new ArgumentNullException("node");

			_domNode.Instance.AppendChild(node._domNode.Instance);
			return node;
		}

		public GeckoNode CloneNode(bool deep)
		{
			return GeckoNode.Create(_domNode.Instance.CloneNode(deep, 1));
		}

		public GeckoNode InsertBefore(GeckoNode newChild, GeckoNode before)
		{
			if (newChild == null)
				throw new ArgumentNullException("newChild");
			if (before == null)
				throw new ArgumentNullException("before");

			_domNode.Instance.InsertBefore(newChild._domNode.Instance, before._domNode.Instance);
			return newChild;
		}

		public GeckoNode RemoveChild(GeckoNode node)
		{
			if (node == null)
				throw new ArgumentNullException("node");

			_domNode.Instance.RemoveChild(node._domNode.Instance);
			return node;
		}

		public GeckoNode ReplaceChild(GeckoNode newChild, GeckoNode oldChild)
		{
			if (newChild == null)
				throw new ArgumentNullException("newChild");
			if (oldChild == null)
				throw new ArgumentNullException("oldChild");

			_domNode.Instance.ReplaceChild(newChild._domNode.Instance, oldChild._domNode.Instance);
			return newChild;
		}

		public string NamespaceURI
		{
			get { return nsString.Get(_domNode.Instance.GetNamespaceURIAttribute); }
		}

		public string Prefix
		{
			get { return nsString.Get(_domNode.Instance.GetPrefixAttribute); }
		}

		public string LocalName
		{
			get { return nsString.Get(_domNode.Instance.GetLocalNameAttribute); }
		}

		private NodeType m_cachedType;

		public NodeType NodeType
		{
			get {
				if (m_cachedType != 0)
					return m_cachedType;

				return m_cachedType = (NodeType)_domNode.Instance.GetNodeTypeAttribute(); 
			}
		}

		public GeckoNode ParentNode
		{
			get { return _domNode.Instance.GetParentNodeAttribute().Wrap(Create); }
		}

		public GeckoElement ParentElement
		{
			get
			{
				nsIDOMNode node = _domNode.Instance.GetParentNodeAttribute();
				while ( node != null && !( node is nsIDOMElement ) )
				{
					node = node.GetParentNodeAttribute();
				}
				// after cycle node is nsIDOMElement or null

				return ((nsIDOMElement)node).Wrap(GeckoElement.CreateDomElementWrapper);
			}
		}


		private nsIDOMXPathResult EvaluateXPathInternal( string xpath )
		{
			nsIDOMXPathResult result = null;
            using (var evaluator = Xpcom.CreateInstance2<nsIDOMXPathEvaluator>(Contracts.XPathEvaluator))
			{
				var node = DomObject;
				nsIDOMXPathNSResolver resolver = evaluator.Instance.CreateNSResolver(node);
				result = (nsIDOMXPathResult)evaluator.Instance.Evaluate(new nsAString(xpath), node, resolver, 0, null);
				Xpcom.FreeComObject( ref resolver );
			}

			return result;
		}

		/// <summary>
		/// Evaluate xpath on this node.
		/// </summary>
		/// <param name="xpath"></param>
		/// <returns></returns>
		public XPathResult EvaluateXPath( string xpath )
		{
			var r = EvaluateXPathInternal( xpath );
			return new XPathResult(r);
		}



		/// <summary>
		/// Working similar to SelectSingle but not throwing exceptions on error (simply return first result)
		/// </summary>
		/// <param name="xpath"></param>
		/// <returns></returns>
		public GeckoNode SelectFirst( string xpath )
		{
			var r = EvaluateXPathInternal( xpath );

			nsIDOMNode singleNode = null;
			switch (r.GetResultTypeAttribute())
			{
				case nsIDOMXPathResultConsts.UNORDERED_NODE_ITERATOR_TYPE:
					singleNode = r.IterateNext();
					break;
				case nsIDOMXPathResultConsts.FIRST_ORDERED_NODE_TYPE:
				case nsIDOMXPathResultConsts.ANY_UNORDERED_NODE_TYPE:
					singleNode = r.GetSingleNodeValueAttribute();
					break;
			}

			var ret = singleNode.Wrap( GeckoNode.Create );
			Xpcom.FreeComObject( ref r );
			return ret;
		}

		public GeckoNode SelectSingle( string xpath )
		{
			var r = EvaluateXPathInternal( xpath );

			nsIDOMNode singleNode = null;
			switch (r.GetResultTypeAttribute())
			{
				case nsIDOMXPathResultConsts.UNORDERED_NODE_ITERATOR_TYPE:
					singleNode = r.IterateNext();
					var test = r.IterateNext();
					if (test != null)
					{
						Xpcom.FreeComObject( ref test );
						Xpcom.FreeComObject( ref singleNode );
						Xpcom.FreeComObject( ref r );
						throw new GeckoDomException( "There are more than 1 nodes in Single selection" );
					}
					break;
				case nsIDOMXPathResultConsts.FIRST_ORDERED_NODE_TYPE:
				case nsIDOMXPathResultConsts.ANY_UNORDERED_NODE_TYPE:
					singleNode = r.GetSingleNodeValueAttribute();
					break;
			}

			var ret = singleNode.Wrap( GeckoNode.Create );
			Xpcom.FreeComObject( ref r );
			return ret;
		}

		public DomEventTarget GetEventTarget()
		{
			var eventTarget = Xpcom.QueryInterface<nsIDOMEventTarget>( _domNode.Instance );
			return eventTarget.Wrap( DomEventTarget.Create );
		}
	}
}
