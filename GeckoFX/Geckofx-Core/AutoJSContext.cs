#region ***** BEGIN LICENSE BLOCK *****
/* Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is Skybound Software code.
 *
 * The Initial Developer of the Original Code is Skybound Software.
 * Portions created by the Initial Developer are Copyright (C) 2008-2009
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 */
#endregion END LICENSE BLOCK

using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Gecko
{
    /// <summary>
    /// Creates a scoped, fake "system principal" security context.  This class is used primarly to work around bugs in gecko
    /// which prevent methods on nsIDOMCSSStyleSheet from working outside of javascript.
    /// </summary>
    public class AutoJSContext : IDisposable
	{
		private IntPtr _cx;
		private JSAutoCompartment _defaultCompartment;

		public IntPtr ContextPointer { get { return _cx; } }

		Stack<JSAutoCompartment> _compartmentStack = new Stack<JSAutoCompartment>();
	    private nsIXPCComponents _nsIXPCComponents;

	    public void PushCompartmentScope(nsISupports obj)
		{
			_compartmentStack.Push(new JSAutoCompartment(this, obj));
		}

		public IntPtr PopCompartmentScope()
		{
			if (_compartmentStack.Count <= 0)
				throw new InvalidOperationException("The Compartment stack is empty.");


			var autoCompartment = _compartmentStack.Pop();
			IntPtr ret = autoCompartment.ScopeObject;
			autoCompartment.Dispose();

			return ret;
		}

		public void PushCompartmentScope(IntPtr jsObject)
		{
			_compartmentStack.Push(new JSAutoCompartment(ContextPointer, jsObject));
		}

		public IntPtr PeekCompartmentScope()
		{
			if (_compartmentStack.Count > 0)
				return _compartmentStack.Peek().ScopeObject;

			return _defaultCompartment.ScopeObject;
		}

		/// <summary>
		/// Create a AutoJSContext using the SafeJSContext.
		/// If context is IntPtr.Zero use the SafeJSContext
        /// (but SafeJSContext doesn't contain a Global object then try the BackstageJSContext instead)
		/// </summary>
		/// <param name="context"></param>
		public AutoJSContext(IntPtr context)
		{
			// We can't just use nsIXPConnect::GetSafeJSContext(); because its marked as [noxpcom, nostdcall]
			// TODO: Enhance IDL compiler to not generate methods for noxpcom, nostdcall tagged methods.
			if (context == IntPtr.Zero)
				context = GlobalJSContextHolder.SafeJSContext;			

            IntPtr globalObject = GetGlobalFromContext(context);
            if (globalObject == IntPtr.Zero && context == GlobalJSContextHolder.SafeJSContext)
		    {
		        context = GlobalJSContextHolder.BackstageJSContext;
                globalObject = GetGlobalFromContext(context);
		    }
            
            if (globalObject == IntPtr.Zero)
                throw new InvalidOperationException("JSContext don't store their default compartment object on the cx.");
		  
            _defaultCompartment = new JSAutoCompartment(context, globalObject);
			_cx = context;
		}

		/// <summary>
		/// Create a AutoJSContext using the SafeJSContext.
		/// </summary>		
		public AutoJSContext()
            : this(GlobalJSContextHolder.BackstageJSContext)
		{
		}

		/// <summary>
		/// Evaluate javascript in the current context.
		/// </summary>
		/// <param name="jsScript"></param>
		/// <param name="jsval"></param>
		/// <returns></returns>
		public bool EvaluateScript(string jsScript, out string result)
		{
			var ptr = new JsVal();
			bool ret = SpiderMonkey.JS_EvaluateScript(_cx, GetGlobalObject(), jsScript, (uint)jsScript.Length, "script", 1, ref ptr);			

			result = ret ? ConvertValueToString(ptr) : null;
			return ret;
		}

        /// <summary>
        /// Evaluate JavaScript in specified window.
        /// Will throw a GeckoJavaScriptException if unable to convert window into a JsVal
        /// </summary>
        /// <param name="javascript">The javascript to run.</param>
        /// <param name="window">The window to execuate javascript in.</param>
        /// <returns>The return value of the script as a JsVal</returns>
	    public JsVal EvaluateScript(string javascript, nsIDOMWindow window)
	    {
            string msg = String.Empty;
            var old = SpiderMonkey.JS_SetErrorReporter(_cx, (cx, message, report) => { msg = message; });
            try
            {
                IntPtr globalObject = ConvertCOMObjectToJSObject((nsISupports)window);

                using (new JSAutoCompartment(_cx, globalObject))
                {
                    var windowJsVal = new JsVal();
                    string jsScript = "this";
                    bool ret = SpiderMonkey.JS_EvaluateScript(_cx, globalObject, jsScript, (uint)jsScript.Length,
                        "script", 1, ref windowJsVal);

                    if (!ret)
                    {
                        throw new GeckoJavaScriptException(String.Format("JSError : {0}", msg));
                    }

                    if (GetComponentsObject().GetUtilsAttribute().IsXrayWrapper(windowJsVal))
                        windowJsVal = GetComponentsObject().GetUtilsAttribute().WaiveXrays(ref windowJsVal, _cx);

                    using (nsAStringBase b = new nsAString(javascript))
                    {
                        return GetComponentsObject().GetUtilsAttribute().EvalInWindow(b, windowJsVal, _cx);
                    }
                }
            }
            finally
            {
                SpiderMonkey.JS_SetErrorReporter(_cx, old);
            }
	    }

		public JsVal EvaluateScript(string javaScript)
		{
            string msg = String.Empty;
            var old = SpiderMonkey.JS_SetErrorReporter(_cx, (cx, message, report) => { msg = message; });
            try
            {
                var jsValue = new JsVal();
                var ret = SpiderMonkey.JS_EvaluateScript(_cx, PeekCompartmentScope(), javaScript, (uint)javaScript.Length, "script",
                    1, ref jsValue);

                if (!ret)
                {
                    throw new GeckoJavaScriptException(String.Format("JSError : {0}", msg));
                }

                return jsValue;
            }
            finally
            {
                SpiderMonkey.JS_SetErrorReporter(_cx, old);
            }
		}

		/// <summary>
		/// EvaluateScript Bypassing some Security Restrictions. 
		/// This comes at a performance and complexity cost, so only use if really neccessary.
		/// </summary>
		/// <param name="javaScript"></param>
		/// <returns></returns>
		public JsVal EvaluateScriptBypassingSomeSecurityRestrictions(string javaScript)
		{
			EvaluateScript(String.Format("parentthis = this; GeckoFxHandler = function GeckoFxHandler() {{ function geckofxInner() {{ GeckofxEvalScriptEventResult = {0}; }}; geckofxInner.call(parentthis); }}", javaScript));
			EvaluateScript("document.addEventListener('geckofxEvalScriptEvent', GeckoFxHandler, false);");
			EvaluateScript("var evt = document.createEvent('Event'); evt.initEvent('geckofxEvalScriptEvent',true,true); document.dispatchEvent(evt);");
			var result = EvaluateScript("GeckofxEvalScriptEventResult;");
			EvaluateScript("document.removeEventListener('geckofxEvalScriptEvent', GeckoFxHandler, false);");
			return result;
		}		
		
		/// <summary>
		/// Evaluate javascript in the current context.
		/// </summary>
		/// <param name="jsScript"></param>
		/// <param name="thisObject">a nsISupports com object that this is set too.</param>
		/// <param name="result"></param>
		/// <returns></returns>
		public bool EvaluateScript(string jsScript, nsISupports thisObject, out string result)
		{
			try
			{				
				var ptr = new JsVal();
				IntPtr globalObject = ConvertCOMObjectToJSObject(thisObject);

			    using (new JSAutoCompartment(_cx, globalObject))
				{
					bool ret = SpiderMonkey.JS_EvaluateScript(_cx, globalObject, jsScript, (uint)jsScript.Length, "script", 1, ref ptr);

                    if (GetComponentsObject().GetUtilsAttribute().IsXrayWrapper(ptr))
                        ptr = GetComponentsObject().GetUtilsAttribute().WaiveXrays(ref ptr, _cx);

				    result = ret ? ConvertValueToString(ptr) : null;

					return ret;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception {0}", e);
				result = String.Empty;
				return false;
			}
		}

		/// <summary>
		/// Evaluate javascript in the current context with system privileges.
		/// </summary>
		/// <param name="jsScript"></param>
		/// <param name="jsval"></param>
		/// <returns></returns>
		[Obsolete]
		public bool EvaluateTrustedScript(string jsScript, out string result)
		{
			throw new NotImplementedException();

			var ptr = new JsVal();
			IntPtr globalObject = SpiderMonkey.JS_GetGlobalForScopeChain(_cx);
			bool ret;
			IntPtr systemGlobalObject = SpiderMonkey.JS_GetGlobalForScopeChain(GlobalJSContextHolder.BackstageJSContext);
			// Compartments have to be entered and left in LIFO order.
			bool inSystemCompartment = false;
			IntPtr oldCompartment = IntPtr.Zero;
			try
			{
				// Allow access to any object on page.
				oldCompartment = SpiderMonkey.JS_EnterCompartment(_cx, systemGlobalObject);
				// At any time, a JSContext has a current (possibly-NULL) compartment.
				inSystemCompartment = true;
				ret = SpiderMonkey.JS_EvaluateScript(_cx, globalObject, jsScript, (uint)jsScript.Length, "script", 1, ref ptr);
				result = ConvertValueToString(ptr);
			}
			finally
			{
				if (inSystemCompartment)
					SpiderMonkey.JS_LeaveCompartment(_cx, oldCompartment);
			}
			return ret;
		}

	    /// <summary>
	    /// Helper method which attempts to find the global object in a Context.
	    /// </summary>
	    /// <returns>the Global object ptr or Null/Zero ptr if not found.</returns>
	    private IntPtr GetGlobalFromContext(IntPtr context)
	    {
            IntPtr globalObject = SpiderMonkey.CurrentGlobalOrNull(context);
            if (globalObject == IntPtr.Zero)
            {
                globalObject = SpiderMonkey.DefaultObjectForContextOrNull(context);
                if (globalObject == IntPtr.Zero)
                    return IntPtr.Zero;
            }

            return globalObject;
	    }

		private IntPtr GetGlobalObject()
		{
			IntPtr globalObject = SpiderMonkey.CurrentGlobalOrNull(_cx);
			if (globalObject == IntPtr.Zero)
				throw new ObjectDisposedException(this.GetType().Name);
			return globalObject;
		}

		public ComPtr<nsISupports> GetGlobalNsObject()
		{
			IntPtr globalObject = SpiderMonkey.CurrentGlobalOrNull(_cx);
			if (globalObject != IntPtr.Zero)
			{
				Guid guid = typeof(nsISupports).GUID;

				IntPtr pUnk = IntPtr.Zero;
				try
				{
					pUnk = Xpcom.XPConnect.Instance.WrapJS(_cx, globalObject, ref guid);
					object comObj = Xpcom.GetObjectForIUnknown(pUnk);
					try
					{
						return Xpcom.QueryInterface<nsISupports>(comObj).AsComPtr();
					}
					finally
					{
						Xpcom.FreeComObject(ref comObj);
					}
				}
				finally
				{
					if (pUnk != IntPtr.Zero)
						Marshal.Release(pUnk);
				}
			}
			return null;
		}

		internal string ConvertValueToString(JsVal value)
		{
			IntPtr jsp = SpiderMonkey.JS_ValueToString(_cx, value);
			if (jsp != IntPtr.Zero)
			{
				uint length;
				var chars = SpiderMonkey.JS_GetStringCharsAndLength(_cx, jsp, out length);
				if (chars != IntPtr.Zero)
					return Marshal.PtrToStringUni(chars, (int)length);
			}
			return null;
		}

		public IntPtr ConvertCOMObjectToJSObject(nsISupports obj)
		{
			Guid guid = typeof(nsISupports).GUID;

			IntPtr globalObject = GetGlobalObject();
            
			using (var holder = new ComPtr<nsIXPConnectJSObjectHolder>(Xpcom.XPConnect.Instance.WrapNative(_cx, globalObject, (nsISupports)obj, ref guid)))
			{
				int slot = holder.GetSlotOfComMethod(new Func<IntPtr>(holder.Instance.GetJSObject));
				var getJSObject = holder.GetComMethod<Xpcom.GetJSObjectFromHolderDelegate>(slot);
				return getJSObject(holder.Instance);
			}

		}

		public JsVal ConvertCOMObjectToJSVal(IntPtr globalObject, nsISupports thisObject)
		{
			var writableVariant = Xpcom.CreateInstance2<nsIWritableVariant>(Contracts.WritableVariant);
			writableVariant.Instance.SetAsISupports(thisObject);
			return Xpcom.XPConnect.Instance.VariantToJS(_cx, globalObject, writableVariant.Instance);
		}

        /// <summary>
        /// Gets the nsIXPCComponents which is the javascript 'Components' objects
        /// The Components objects is the main object XPConnect object.        
        /// </summary>
        /// <returns></returns>
	    public nsIXPCComponents GetComponentsObject()
	    {
	        if (_nsIXPCComponents == null)
	        {
	            string javaScript = "Components";

	            var jsValue = new JsVal();
	            var ret = SpiderMonkey.JS_EvaluateScript(_cx, PeekCompartmentScope(), javaScript, (uint) javaScript.Length,
	                "script",
	                1, ref jsValue);

	            if (!ret || !jsValue.IsObject)
	                return null;

                _nsIXPCComponents = jsValue.ToObject() as nsIXPCComponents;
	        }

	        return _nsIXPCComponents;
	    }

		public void Dispose()
		{
			if (_compartmentStack != null)
			{
				while (_compartmentStack.Count > 0)
					_compartmentStack.Pop().Dispose();
				_compartmentStack = null;
			}

			if (_defaultCompartment != null)
				_defaultCompartment.Dispose();
			_defaultCompartment = null;

			GC.SuppressFinalize(this);
		}
	}
}
