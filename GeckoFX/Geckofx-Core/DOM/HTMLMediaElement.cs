using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Skybound.Gecko.DOM
{
	[Guid("91f65f50-74ea-40ea-8e26-652290738730"), ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	interface nsIDOMHTMLMediaElement : nsIDOMHTMLElement
	{
		#region Inherited Intefaces
		// nsIDOMNode:
		new void GetNodeName(nsAString aNodeName);
		new void GetNodeValue(nsAString aNodeValue);
		new void SetNodeValue(nsAString aNodeValue);
		new ushort GetNodeType();
		new nsIDOMNode GetParentNode();
		new nsIDOMNodeList GetChildNodes();
		new nsIDOMNode GetFirstChild();
		new nsIDOMNode GetLastChild();
		new nsIDOMNode GetPreviousSibling();
		new nsIDOMNode GetNextSibling();
		new nsIDOMNamedNodeMap GetAttributes();
		new nsIDOMDocument GetOwnerDocument();
		new nsIDOMNode InsertBefore(nsIDOMNode newChild, nsIDOMNode refChild);
		new nsIDOMNode ReplaceChild(nsIDOMNode newChild, nsIDOMNode oldChild);
		new nsIDOMNode RemoveChild(nsIDOMNode oldChild);
		new nsIDOMNode AppendChild(nsIDOMNode newChild);
		new bool HasChildNodes();
		new nsIDOMNode CloneNode(bool deep);
		new void Normalize();
		new bool IsSupported(nsAString feature, nsAString version);
		new void GetNamespaceURI(nsAString aNamespaceURI);
		new void GetPrefix(nsAString aPrefix);
		new void SetPrefix(nsAString aPrefix);
		new void GetLocalName(nsAString aLocalName);
		new bool HasAttributes();

		// nsIDOMElement:
		new void GetTagName(nsAString aTagName);
		new void GetAttribute(nsAString name, nsAString _retval);
		new void SetAttribute(nsAString name, nsAString value);
		new void RemoveAttribute(nsAString name);
		new nsIDOMAttr GetAttributeNode(nsAString name);
		new nsIDOMAttr SetAttributeNode(nsIDOMAttr newAttr);
		new nsIDOMAttr RemoveAttributeNode(nsIDOMAttr oldAttr);
		new nsIDOMNodeList GetElementsByTagName(nsAString name);
		new void GetAttributeNS(nsAString namespaceURI, nsAString localName, nsAString _retval);
		new void SetAttributeNS(nsAString namespaceURI, nsAString qualifiedName, nsAString value);
		new void RemoveAttributeNS(nsAString namespaceURI, nsAString localName);
		new nsIDOMAttr GetAttributeNodeNS(nsAString namespaceURI, nsAString localName);
		new nsIDOMAttr SetAttributeNodeNS(nsIDOMAttr newAttr);
		new nsIDOMNodeList GetElementsByTagNameNS(nsAString namespaceURI, nsAString localName);
		new bool HasAttribute(nsAString name);
		new bool HasAttributeNS(nsAString namespaceURI, nsAString localName);

		// nsIDOMHTMLElement:
		new void GetId(nsAString aId);
		new void SetId(nsAString aId);
		new void GetTitle(nsAString aTitle);
		new void SetTitle(nsAString aTitle);
		new void GetLang(nsAString aLang);
		new void SetLang(nsAString aLang);
		new void GetDir(nsAString aDir);
		new void SetDir(nsAString aDir);
		new void GetClassName(nsAString aClassName);
		new void SetClassName(nsAString aClassName);
		#endregion

		#region Error State

		nsIDOMMediaError GetError();

		#endregion

		#region Network State

		void GetSrc(nsAString aSrc);
		void SetSrc(nsAString aSrc);

		void GetCurrentSrc(nsAString aCurrentSrc);

		short GetNetworkState();

		void GetPreload(nsAString aPreload);
		void SetPreload(nsAString aPreload);

		nsIDOMTimeRanges GetBuffered();

		void Load();

		nsAString CanPlayType(nsAString Type);

		#endregion

		#region Ready State

		short GetReadyState();
		bool GetSeeking();

		#endregion

		#region Playback State

		void GetCurrentTime(double aCurrentTime);
		void SetCurrentTime(double aCurrentTime);

		double GetDuration();

		bool GetPaused();

		bool GetEnded();

		bool GetMozAutoplayEnabled();

		void GetAutoplay(bool aAutoplay);
		void SetAutoplay(bool aAutoplay);

		void Play();
		void Pause();

		#endregion

		#region Controls

		void GetControls(bool aControls);
		void SetControls(bool aControls);

		void GetVolume(double aVolume);
		void SetVolume(double aVolume);

		void GetMuted(bool aMuted);
		void SetMuted(bool aMuted);

		#endregion

		#region Mozilla Extensions

		long GetMozChannels();
		long GetMozSampleRate();

		void GetMozFrameBufferLength(long aMozFrameBufferLength);
		void SetMozFrameBufferLength(long aMozFrameBufferLength);

		void MozLoadFrom(nsIDOMHTMLMediaElement Other);

		#endregion
	}

	public class GeckoMediaElement : GeckoElement
	{
		nsIDOMHTMLMediaElement DOMHTMLElement;
		internal GeckoMediaElement(nsIDOMHTMLMediaElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoMediaElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLMediaElement;
		}

		public string Src
		{
			get { return nsString.Get(DOMHTMLElement.GetSrc); }
			set { DOMHTMLElement.SetSrc(new nsAString(value)); }
		}
		public string CurrentSrc
		{
			get { return nsString.Get(DOMHTMLElement.GetCurrentSrc); }
		}
		public short NetworkState
		{
			get { return DOMHTMLElement.GetNetworkState(); }
		}
	}
}
