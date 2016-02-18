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

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko
{	
	[Guid("8E4AABE2-B832-4cff-B213-2174DE2B818D")]
	[ContractID(PromptServiceFactory.ContractID)]
	class PromptServiceFactory
		: GenericOneClassNsFactory<PromptServiceFactory, PromptService>
	{
		public const string ContractID ="@mozilla.org/embedcomp/prompt-service;1";
	}

	[ContractID(PromptFactoryFactory.ContractID)]
	class PromptFactoryFactory
		: GenericOneClassNsFactory<PromptFactoryFactory, PromptFactory>
	{
		public const string ContractID = "@mozilla.org/prompter;1";

		public static void Init()
		{
			DefaultPromptFactory.Init();
			Register();
		}

		public static void Shutdown()
		{
			DefaultPromptFactory.Shutdown();
		}
	}

	/// <summary>
	/// A wrapper of XULRunner's default nsIPromptFactory implementation, i.e. @mozilla.org/prompter;1
	/// </summary>
	class DefaultPromptFactory
	{
		private static nsIPromptFactory factory;

		internal static void Init()
		{
			factory = Xpcom.GetService<nsIPromptFactory>("@mozilla.org/prompter;1");
		}

		internal static void Shutdown()
		{
			Marshal.ReleaseComObject(factory);
		}

		public static IntPtr GetPrompt(nsIDOMWindow aParent, ref Guid iid)
		{
			return factory.GetPrompt(aParent, ref iid);
		}

		/// <summary>
		/// Wrapper of nsIPromptFactory.GetPrompt(nsIDOMWindow aParent, ref Guid iid)
		/// </summary>
		/// <typeparam name="TPrompt">prompt type, may be nsIPrompt, nsIAuthPrompt, or nsIAuthPrompt2</typeparam>
		/// <param name="aParent">window object</param>
		/// <returns></returns>
		public static TPrompt GetPrompt<TPrompt>(nsIDOMWindow aParent = null)
		{
			var iid = (GuidAttribute)typeof(TPrompt).GetCustomAttributes(typeof(GuidAttribute), false)[0];
			var g = new Guid (iid.Value);
			var ptr = factory.GetPrompt(aParent, ref g);
			var prompt = (TPrompt)Marshal.GetTypedObjectForIUnknown(ptr, typeof(TPrompt));
			Marshal.Release(ptr);
			return prompt;
		}
	}

	public class PromptFactory
		:nsIPromptFactory
	{
		static PromptFactory()
		{
			PromptServiceCreator = () => new PromptService();
		}

		/// <summary>
		/// Allow injecting different PromptService implementations into PromptFactory.
		/// Custom PromptService may implement some or all of nsIPrompt, nsIAuthPrompt2, and nsIAuthPrompt.
		/// </summary>
		public static Func<object> PromptServiceCreator { get; set; }

		public IntPtr GetPrompt( nsIDOMWindow aParent, ref Guid iid )
		{
			IntPtr result = IntPtr.Zero;
			IntPtr iUnknownForObject = Marshal.GetIUnknownForObject(PromptServiceCreator());
			Marshal.QueryInterface(iUnknownForObject, ref iid, out result);
			Marshal.Release(iUnknownForObject);
			return result;
		}
	}

	public class PromptService : nsIPrompt, nsIAuthPrompt2, nsIAuthPrompt
	{

		public virtual void Alert(string dialogTitle, string text)
		{
			DefaultPromptFactory.GetPrompt<nsIPrompt>().Alert(dialogTitle, text);
		}

		public virtual void AlertCheck(string dialogTitle, string text, string checkMsg, ref bool checkValue)
		{
			DefaultPromptFactory.GetPrompt<nsIPrompt>().AlertCheck(dialogTitle, text, checkMsg, ref checkValue);
		}

		public virtual bool Confirm(string dialogTitle, string text)
		{
			return DefaultPromptFactory.GetPrompt<nsIPrompt>().Confirm(dialogTitle, text);
		}

		public virtual bool ConfirmCheck(string dialogTitle, string text, string checkMsg, ref bool checkValue)
		{
			return DefaultPromptFactory.GetPrompt<nsIPrompt>().ConfirmCheck(dialogTitle, text, checkMsg, ref checkValue);
		}

		public virtual int ConfirmEx(string dialogTitle, string text, uint buttonFlags, string button0Title, string button1Title, string button2Title, string checkMsg, ref bool checkValue)
		{
			return DefaultPromptFactory.GetPrompt<nsIPrompt>().ConfirmEx(dialogTitle, text, buttonFlags, button0Title, button1Title, button2Title, checkMsg, ref checkValue);
		}

		public virtual bool Prompt(string dialogTitle, string text, ref string value, string checkMsg, ref bool checkValue)
		{
			return DefaultPromptFactory.GetPrompt<nsIPrompt>().Prompt(dialogTitle, text, ref value, checkMsg, ref checkValue);
		}

		public virtual bool PromptPassword(string dialogTitle, string text, ref string password, string checkMsg, ref bool checkValue)
		{
			return DefaultPromptFactory.GetPrompt<nsIPrompt>().PromptPassword(dialogTitle, text, ref password, checkMsg, ref checkValue);
		}

		public virtual bool PromptUsernameAndPassword(string dialogTitle, string text, ref string username, ref string password, string checkMsg, ref bool checkValue)
		{
			return DefaultPromptFactory.GetPrompt<nsIPrompt>().PromptUsernameAndPassword(dialogTitle, text, ref username, ref password, checkMsg, ref checkValue);
		}

		public virtual bool Select(string dialogTitle, string text, uint count, IntPtr[] selectList, ref int outSelection)
		{
			return DefaultPromptFactory.GetPrompt<nsIPrompt>().Select(dialogTitle, text, count, selectList, ref outSelection);
		}

		public virtual bool PromptAuth(nsIChannel aChannel, uint level, nsIAuthInformation authInfo)
		{
			return DefaultPromptFactory.GetPrompt<nsIAuthPrompt2>().PromptAuth(aChannel, level, authInfo);
		}

		public virtual nsICancelable AsyncPromptAuth(nsIChannel aChannel, nsIAuthPromptCallback aCallback, nsISupports aContext, uint level, nsIAuthInformation authInfo)
		{
			return DefaultPromptFactory.GetPrompt<nsIAuthPrompt2>().AsyncPromptAuth(aChannel, aCallback, aContext, level, authInfo);
		}

		public virtual bool Prompt(string dialogTitle, string text, string passwordRealm, uint savePassword, string defaultText, ref string result)
		{
			return DefaultPromptFactory.GetPrompt<nsIAuthPrompt>().Prompt(dialogTitle, text, passwordRealm, savePassword, defaultText, ref result);
		}

		public virtual bool PromptUsernameAndPassword(string dialogTitle, string text, string passwordRealm, uint savePassword, ref string user, ref string pwd)
		{
			return DefaultPromptFactory.GetPrompt<nsIAuthPrompt>().PromptUsernameAndPassword(dialogTitle, text, passwordRealm, savePassword, ref user, ref pwd);
		}

		public virtual bool PromptPassword(string dialogTitle, string text, string passwordRealm, uint savePassword, ref string pwd)
		{
			return DefaultPromptFactory.GetPrompt<nsIAuthPrompt>().PromptPassword(dialogTitle, text, passwordRealm, savePassword, ref pwd);
		}
	}
}
