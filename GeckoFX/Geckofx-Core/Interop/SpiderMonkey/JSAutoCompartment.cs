using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace Gecko
{
	public sealed class JSAutoCompartment : IDisposable
	{
		private IntPtr _oldCompartment;
		private IntPtr _cx;
		private IntPtr _obj;
		private bool _isDisposed;

		public JSAutoCompartment(AutoJSContext context, nsISupports comObject)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (context.ContextPointer == IntPtr.Zero)
				throw new ArgumentException("context has Null ContextPointer");
			if (context == null)
				throw new ArgumentNullException("comObject");

			_obj = context.ConvertCOMObjectToJSObject(comObject);
			_cx = context.ContextPointer;
			_oldCompartment = SpiderMonkey.JS_EnterCompartment(_cx, _obj);			
		}


		public JSAutoCompartment(IntPtr context, IntPtr obj)
		{
			if (context == IntPtr.Zero)
				throw new ArgumentNullException("context");
			if (context == IntPtr.Zero)
				throw new ArgumentNullException("obj");

			_obj = obj;
			_oldCompartment = SpiderMonkey.JS_EnterCompartment(context, _obj);
			_cx = context;
		}

		public IntPtr ScopeObject { get { return _obj; } }

		public void Dispose()
		{
			if (!_isDisposed)
			{
				_isDisposed = true;
				if (_cx != IntPtr.Zero)
				{
					SpiderMonkey.JS_LeaveCompartment(_cx, _oldCompartment);
				}
				GC.SuppressFinalize(this);
			}
		}
	}
}
