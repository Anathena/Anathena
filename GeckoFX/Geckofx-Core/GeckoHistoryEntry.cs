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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Proxies;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Gecko
{
	/// <summary>
	/// Represents an entry in a <see cref="GeckoWebBrowser"/> session history.
	/// </summary>
	public class GeckoHistoryEntry
	{
		internal GeckoHistoryEntry()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of <see cref="GeckoHistoryEntry"/>.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="title"></param>
		/// <param name="isSubFrame"></param>
		public GeckoHistoryEntry(Uri url, string title, bool isSubFrame)
		{
			this._Url = url;
			this._Title = title;
			this._IsSubFrame = isSubFrame;
		}
		
		/// <summary>
		/// Gets the URL of the page in history.
		/// </summary>
		public virtual Uri Url
		{
			get { return _Url; }
		}
		Uri _Url;
		
		/// <summary>
		/// Gets the title of the page in history.
		/// </summary>
		public virtual string Title
		{
			get { return _Title; }
		}
		string _Title;
		
		/// <summary>
		/// Gets whether the page was loaded into a sub-frame.
		/// </summary>
		public virtual bool IsSubFrame
		{
			get { return _IsSubFrame; }
		}
		bool _IsSubFrame;
	}
	
	/// <summary>
	/// Represents the navigation history for the current session.
	/// </summary>
	public class GeckoSessionHistory : IList<GeckoHistoryEntry>
	{
		internal GeckoSessionHistory(nsIWebNavigation webNav)
		{
			this.WebNav = webNav;
			this.History = webNav.GetSessionHistoryAttribute();
		}
		
		nsIWebNavigation WebNav;
		nsISHistory History;
		
		class HistoryEntry : GeckoHistoryEntry
		{
			internal HistoryEntry(nsISHEntry entry)
			{
				this.Entry = entry;
			}
			nsISHEntry Entry;
			
			public override Uri Url
			{
				get
				{
					nsIURI uri = Entry.GetURIAttribute();
					return (uri == null) ? null : new Uri(nsString.Get(uri.GetSpecAttribute));
				}
			}
			
			public override string Title
			{
				get
				{
#if PORT
					return Entry.GetTitleAttribute();
#else
					//TODO: maybe use mozIPlaceInfo?
					throw new NotImplementedException();
#endif
				}
			}
			
			public override bool IsSubFrame
			{
				get
				{
#if PORT
					return Entry.GetIsSubFrameAttribute();
#else
					//TODO: maybe use mozIPlaceInfo?
					throw new NotImplementedException();
#endif
				}
			}
		}
		
		/// <summary>
		/// Navigates the browser to the specified position in its session history.
		/// </summary>
		/// <param name="index">The index to navigate to.  This value must a valid index in this collection.</param>
		/// <returns></returns>
		public void GotoIndex(int index)
		{
			if (index < 0 || index >= Count)
				throw new ArgumentOutOfRangeException("index");
			
			WebNav.GotoIndex(index);
		}
		
		#region IList<GeckoHistoryEntry> Members
		
		int IList<GeckoHistoryEntry>.IndexOf(GeckoHistoryEntry item)
		{
			throw new NotSupportedException();
		}
		
		void IList<GeckoHistoryEntry>.Insert(int index, GeckoHistoryEntry item)
		{
			throw new NotSupportedException();
		}
		
		void IList<GeckoHistoryEntry>.RemoveAt(int index)
		{
			if (index != Count - 1)
				throw new InvalidOperationException();
			Purge(1);
		}
		
		public void Purge(int count)
		{
			History.PurgeHistory(count);
		}
		
		/// <summary>
		/// Gets the history entry at the given index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public GeckoHistoryEntry this[int index]
		{
			get
			{
				return new HistoryEntry((nsISHEntry)History.GetEntryAtIndex(index, false));
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		#endregion

		#region ICollection<GeckoHistoryEntry> Members

		void ICollection<GeckoHistoryEntry>.Add(GeckoHistoryEntry item)
		{
			throw new InvalidOperationException();
		}
		
		/// <summary>
		/// Purges the history.
		/// </summary>
		public void Clear()
		{
			History.PurgeHistory(Count);
		}

		bool ICollection<GeckoHistoryEntry>.Contains(GeckoHistoryEntry item)
		{
			throw new InvalidOperationException();
		}
		
		public void CopyTo(GeckoHistoryEntry[] array, int arrayIndex)
		{
			for (int i = 0; i < Count; i++)
			{
				array[arrayIndex + i] = this[i];
			}
		}
		
		/// <summary>
		/// Gets the number of items in the history.
		/// </summary>
		public int Count
		{
			get { return History.GetCountAttribute(); }
		}

		/// <summary>
		/// Gets the index of the current document in session history.
		/// </summary>
		public int Index
		{
			get { return History.GetIndexAttribute(); }
		}
		
		/// <summary>
		/// Gets or sets the maximum number of items the history may contain.
		/// </summary>
		public int MaxLength
		{
			get { return History.GetMaxLengthAttribute(); }
			set { History.SetMaxLengthAttribute(value); }
		}
		
		public bool IsReadOnly
		{
			get { return false; }
		}
		
		bool ICollection<GeckoHistoryEntry>.Remove(GeckoHistoryEntry item)
		{
			throw new InvalidOperationException();
		}

		#endregion

		#region IEnumerable<GeckoHistoryEntry> Members

		public IEnumerator<GeckoHistoryEntry> GetEnumerator()
		{
			GeckoHistoryEntry [] entry = new GeckoHistoryEntry[this.Count];
			CopyTo(entry, 0);
			
			for (int i = 0; i < entry.Length; i++)
			{
				yield return entry[i];
			}
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (GeckoHistoryEntry entry in this)
			{
				yield return entry;
			}
		}

		#endregion
	}
}
