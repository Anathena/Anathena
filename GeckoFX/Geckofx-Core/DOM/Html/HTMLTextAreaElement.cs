

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{
	public class GeckoTextAreaElement : GeckoHtmlElement
	{
		nsIDOMHTMLTextAreaElement DOMHTMLElement;
		internal GeckoTextAreaElement(nsIDOMHTMLTextAreaElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoTextAreaElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLTextAreaElement;
		}
		
		#region nsIDOMHTMLTextAreaElement members
		public bool Autofocus
		{
			get { return DOMHTMLElement.GetAutofocusAttribute(); }
			set { DOMHTMLElement.SetAutofocusAttribute(value); }
		}

		public uint Cols
		{
			get { return DOMHTMLElement.GetColsAttribute(); }
			set { DOMHTMLElement.SetColsAttribute(value); }
		}

		public bool Disabled
		{
			get { return DOMHTMLElement.GetDisabledAttribute(); }
			set { DOMHTMLElement.SetDisabledAttribute(value); }
		}

		public GeckoFormElement Form
		{
			get { return new GeckoFormElement(DOMHTMLElement.GetFormAttribute()); }
		}

		public int MaxLength 
		{
			get { return DOMHTMLElement.GetMaxLengthAttribute(); }
			set { DOMHTMLElement.SetMaxLengthAttribute(value); }
		}

		public string Name
		{
			get { return nsString.Get(DOMHTMLElement.GetNameAttribute); }
			set { nsString.Set(DOMHTMLElement.SetNameAttribute, value); }
		}

		public string Placeholder
		{
			get { return nsString.Get(DOMHTMLElement.GetPlaceholderAttribute); }
			set { nsString.Set(DOMHTMLElement.SetPlaceholderAttribute, value); }
		}

		public bool ReadOnly
		{
			get { return DOMHTMLElement.GetReadOnlyAttribute(); }
			set { DOMHTMLElement.SetReadOnlyAttribute(value); }
		}

		public bool Required
		{
			get { return DOMHTMLElement.GetRequiredAttribute(); }
			set { DOMHTMLElement.SetRequiredAttribute(value); }
		}

		public uint Rows
		{
			get { return DOMHTMLElement.GetRowsAttribute(); }
			set { DOMHTMLElement.SetRowsAttribute(value); }
		}

		public string Wrap
		{
			get { return nsString.Get(DOMHTMLElement.GetWrapAttribute); }
			set { nsString.Set(DOMHTMLElement.SetWrapAttribute, value); }
		}

		public string Type
		{
			get { return nsString.Get(DOMHTMLElement.GetTypeAttribute); }
		}

		public string DefaultValue {
			get { return nsString.Get(DOMHTMLElement.GetDefaultValueAttribute); }
			set { nsString.Set(DOMHTMLElement.SetDefaultValueAttribute, value); }
		}

		public string Value
		{
			get { return nsString.Get(DOMHTMLElement.GetValueAttribute); }
			set { nsString.Set(DOMHTMLElement.SetValueAttribute, value); }
		}

		public int TextLength
		{
			get { return DOMHTMLElement.GetTextLengthAttribute(); }
		}

		public bool WillValidate
		{
			get { return DOMHTMLElement.GetWillValidateAttribute(); }
		}
		
		public string ValidationMessage
		{
			get { return nsString.Get(DOMHTMLElement.GetValidationMessageAttribute); }
		}

		public bool CheckValidity()
		{
			return DOMHTMLElement.CheckValidity();
		}

		public void SetCustomValidity(string error)
		{
			DOMHTMLElement.SetCustomValidity(new nsAString(error));
		}

		public void Select()
		{
			DOMHTMLElement.Select();
		}

		public int SelectionStart
		{
			get { return DOMHTMLElement.GetSelectionStartAttribute(); }
			set { DOMHTMLElement.SetSelectionStartAttribute(value); }				
		}

		public int SelectionEnd
		{
			get { return DOMHTMLElement.GetSelectionEndAttribute(); }
			set { DOMHTMLElement.SetSelectionEndAttribute(value); }
		}

		public void SetSelectionRange(int selectionStart, int selectionEnd, string direction)
		{
			DOMHTMLElement.SetSelectionRange(selectionStart, selectionEnd, new nsAString(direction));
		}

		public string SelectionDirection
		{
			get { return nsString.Get(DOMHTMLElement.GetSelectionDirectionAttribute); }
			set { nsString.Set(DOMHTMLElement.SetSelectionDirectionAttribute, value); }
		}
		#endregion
	}
}

