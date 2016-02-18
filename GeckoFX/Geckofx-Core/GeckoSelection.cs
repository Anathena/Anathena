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
using System.Drawing;
using System.Collections.Generic;

namespace Gecko
{
	/// <summary>
	/// Manipulates and queries the current selected range of nodes within the document.
	/// </summary>
	public class GeckoSelection
	{
		private GeckoSelection(nsISelection selection)
		{
			// selection is always NOT null, when we use Create function
			//if (selection == null)
			//	throw new ArgumentException("selection");

			this.Selection = selection;
		}

		internal static GeckoSelection Create(nsISelection selection)
		{
			return selection == null ? null : new GeckoSelection( selection );
		}
		
		/// <summary>
		/// Gets the unmanaged nsISelection which this instance wraps.
		/// </summary>
		public object DomSelection { get { return Selection; } }
		
		nsISelection Selection;
		
		/// <summary>
		/// Gets the node in which the selection begins.
		/// </summary>
		public GeckoNode AnchorNode
		{
			get { return GeckoNode.Create(Selection.GetAnchorNodeAttribute()); }
		}
		
		/// <summary>
		/// Gets the offset within the (text) node where the selection begins.
		/// </summary>
		public int AnchorOffset
		{
			get { return Selection.GetAnchorOffsetAttribute(); }
		}
		
		/// <summary>
		/// Gets the node in which the selection ends.
		/// </summary>
		public GeckoNode FocusNode
		{
			get { return GeckoNode.Create(Selection.GetFocusNodeAttribute()); }
		}
		
		/// <summary>
		/// Gets the offset within the (text) node where the selection ends.
		/// </summary>
		public int FocusOffset
		{
			get { return Selection.GetFocusOffsetAttribute(); }
		}
		
		/// <summary>
		/// Gets whether the selection is collapsed or not.
		/// </summary>
		public bool IsCollapsed
		{
			get { return Selection.GetIsCollapsedAttribute(); }
		}
		
		/// <summary>
		/// Gets the number of ranges in the selection.
		/// </summary>
		public int RangeCount
		{
			get { return Selection.GetRangeCountAttribute(); }
		}
		
		/// <summary>
		/// Returns the range at the specified index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public GeckoRange GetRangeAt(int index)
		{
			return new GeckoRange(Selection.GetRangeAt(index));
		}
		
		/// <summary>
		/// Collapses the selection to a single point, at the specified offset in the given DOM node. When the selection is collapsed, and the content is focused and editable, the caret will blink there.
		/// </summary>
		/// <param name="parentNode"></param>
		/// <param name="offset"></param>
		public void Collapse(GeckoNode parentNode, int offset)
		{
			Selection.Collapse((nsIDOMNode)parentNode.DomObject, offset);
		}
		
		/// <summary>
		/// Extends the selection by moving the selection end to the specified node and offset, preserving the selection begin position. The new selection end result will always be from the anchorNode to the new focusNode, regardless of direction.
		/// </summary>
		/// <param name="parentNode">The node where the selection will be extended to.</param>
		/// <param name="offset">Where in node to place the offset in the new selection end.</param>
		public void Extend(GeckoNode parentNode, int offset)
		{
			Selection.Extend((nsIDOMNode)parentNode.DomObject, offset);
		}
		
		/// <summary>
		/// Collapses the whole selection to a single point at the start of the current selection (irrespective of direction). If content is focused and editable, the caret will blink there.
		/// </summary>
		public void CollapseToStart()
		{
			Selection.CollapseToStart();
		}
		
		/// <summary>
		/// Collapses the whole selection to a single point at the end of the current selection (irrespective of direction). If content is focused and editable, the caret will blink there.
		/// </summary>
		public void CollapseToEnd()
		{
			Selection.CollapseToEnd();
		}
		
		/// <summary>
		/// Returns whether the specified node is part of the selection.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="partlyContained">True if the function should return true when some part of the node is contained with the selection; when false, the function only returns true when the entire node is contained within the selection.</param>
		/// <returns></returns>
		public bool ContainsNode(GeckoNode node, bool partlyContained)
		{
			return Selection.ContainsNode((nsIDOMNode)node.DomObject, partlyContained);
		}
		
		/// <summary>
		/// Adds all children of the specified node to the selection.
		/// </summary>
		/// <param name="parentNode"></param>
		public void SelectAllChildren(GeckoNode parentNode)
		{
			Selection.SelectAllChildren((nsIDOMNode)parentNode.DomObject);
		}
		
		/// <summary>
		/// Adds a range to the current selection.
		/// </summary>
		/// <param name="range"></param>
		public void AddRange(GeckoRange range)
		{
			Selection.AddRange((nsIDOMRange)range.DomRange);
		}
		
		/// <summary>
		/// Removes a range from the current selection.
		/// </summary>
		/// <param name="range"></param>
		public void RemoveRange(GeckoRange range)
		{
			Selection.RemoveRange((nsIDOMRange)range.DomRange);
		}
		
		/// <summary>
		/// Removes all ranges from the current selection.
		/// </summary>
		public void RemoveAllRanges()
		{
			Selection.RemoveAllRanges();
		}
		
		/// <summary>
		/// Deletes this selection from the document.
		/// </summary>
		public void DeleteFromDocument()
		{
			Selection.DeleteFromDocument();
		}	

		/// <summary>
		/// Match this up with EndbatchChanges. Will stop ui updates while multiple selection methods are called.
		/// </summary>
		public void StartBatchChanges()
		{
			((nsISelectionPrivate)Selection).StartBatchChanges();
		}

		/// <summary>
		/// Match this up with startBatchChanges
		/// </summary>
		public void EndBatchChanges()
		{
			((nsISelectionPrivate)Selection).EndBatchChanges();
		}

		/// <summary>
		/// Scrolls a region of the selection, so that it is visible in
		/// the scrolled view.
		///
		/// @param aRegion - the region inside the selection to scroll into view
		/// (see selection region constants defined in
		/// nsISelectionController).
		/// @param aIsSynchronous - when true, scrolls the selection into view
		/// before returning. If false, posts a request which
		/// is processed at some point after the method returns.
		/// @param aVPercent - how to align the frame vertically. A value of 0
		/// means the frame's upper edge is aligned with the top edge
		/// of the visible area. A value of 100 means the frame's
		/// bottom edge is aligned with the bottom edge of
		/// the visible area. For values in between, the point
		/// "aVPercent" down the frame is placed at the point
		/// "aVPercent" down the visible area. A value of 50 centers
		/// the frame vertically. A value of -1 means move
		/// the frame the minimum amount necessary in order for
		/// the entire frame to be visible vertically (if possible).
		/// @param aHPercent - how to align the frame horizontally. A value of 0
		/// means the frame's left edge is aligned with the left
		/// edge of the visible area. A value of 100 means the
		/// frame's right edge is aligned with the right edge of
		/// the visible area. For values in between, the point
		/// "aHPercent" across the frame is placed at the point
		/// "aHPercent" across the visible area. A value of 50
		/// centers the frame horizontally . A value of -1 means
		/// move the frame the minimum amount necessary in order
		/// for the entire frame to be visible horizontally
		/// (if possible).
		/// </summary>
		public void ScrollIntoView(short aRegion, bool aIsSynchronous, short aVPercent, short aHPercent)
		{
			((nsISelectionPrivate)Selection).ScrollIntoView(aRegion, aIsSynchronous, aVPercent, aHPercent);
		}

		/// <summary>
		/// Returns the whole selection as a plain text string.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return Selection.ToString();
		}
	}
	
	/// <summary>
	/// Identifies a range of content in a document.
	/// </summary>
	public class GeckoRange : ICloneable
	{
		internal GeckoRange(nsIDOMRange range)
		{
			this.DomRange = range;			
		}
		
		/// <summary>
		/// Gets the unmanaged nsIDOMRange which this instance wraps.
		/// </summary>
		public nsIDOMRange DomRange { get; protected set; }		
		
		public GeckoNode StartContainer
		{
			get { return GeckoNode.Create(DomRange.GetStartContainerAttribute()); }
		}

		public int StartOffset { get { return DomRange.GetStartOffsetAttribute(); } }
		
		public GeckoNode EndContainer
		{
			get { return GeckoNode.Create(DomRange.GetEndContainerAttribute()); }
		}

		public int EndOffset { get { return DomRange.GetEndOffsetAttribute(); } }

		public bool Collapsed { get { return DomRange.GetCollapsedAttribute(); } }
		
		public GeckoNode CommonAncestorContainer
		{
			get { return GeckoNode.Create(DomRange.GetCommonAncestorContainerAttribute()); }
		}
		
		public void SetStart(GeckoNode node, int offset)
		{
			DomRange.SetStart((nsIDOMNode)node.DomObject, offset);
		}
		
		public void SetEnd(GeckoNode node, int offset)
		{
			DomRange.SetEnd((nsIDOMNode)node.DomObject, offset);
		}
		
		public void SetStartBefore(GeckoNode node)
		{
			DomRange.SetStartBefore((nsIDOMNode)node.DomObject);
		}
		
		public void SetStartAfter(GeckoNode node)
		{
			DomRange.SetStartAfter((nsIDOMNode)node.DomObject);
		}
		
		public void SetEndBefore(GeckoNode node)
		{
			DomRange.SetEndBefore((nsIDOMNode)node.DomObject);
		}
		
		public void SetEndAfter(GeckoNode node)
		{
			DomRange.SetEndAfter((nsIDOMNode)node.DomObject);
		}
		
		public void Collapse(bool toStart)
		{
			DomRange.Collapse(toStart);
		}
		
		public void SelectNode(GeckoNode node)
		{
			DomRange.SelectNode((nsIDOMNode)node.DomObject);
		}
		
		public void SelectNodeContents(GeckoNode node)
		{
			DomRange.SelectNodeContents((nsIDOMNode)node.DomObject);
		}
		
		public short CompareBoundaryPoints(ushort how, GeckoRange sourceRange)
		{
			return DomRange.CompareBoundaryPoints(how, (nsIDOMRange)sourceRange.DomRange);
		}
		
		public void DeleteContents()
		{
			DomRange.DeleteContents();
		}
		
		public GeckoNode ExtractContents()
		{
			return GeckoNode.Create(DomRange.ExtractContents());
		}
		
		public GeckoNode CloneContents()
		{
			return GeckoNode.Create(DomRange.CloneContents());
		}
		
		public void InsertNode(GeckoNode newNode)
		{
			DomRange.InsertNode((nsIDOMNode)newNode.DomObject);
		}
		
		public void SurroundContents(GeckoNode newParent)
		{
			DomRange.SurroundContents((nsIDOMNode)newParent.DomObject);
		}
		
		public object Clone()
		{
			return CloneRange();
		}

		public GeckoRange CloneRange()
		{
			return new GeckoRange(DomRange.CloneRange());
		}
		
		public override string ToString()
		{
			using (nsAString retval = new nsAString())
			{
				DomRange.ToString(retval);
				return retval.ToString();
			}
		}
		
		public void Detach()
		{
			DomRange.Detach();
		}

		// Get Rectange which surrounds entire selection.
		public Rectangle BoundingClientRect
		{
			get
			{
				nsIDOMClientRect domRect = DomRange.GetBoundingClientRect();
				var r = new Rectangle((int)domRect.GetLeftAttribute(), (int)domRect.GetTopAttribute(), (int)domRect.GetWidthAttribute(), (int)domRect.GetHeightAttribute());
				return r;				
			}
		}

		/// <summary>
		/// Get the Individual rectangles that make up a selection.
		/// </summary>
		public IEnumerable<Rectangle> ClientRects
		{
			get
			{
				//List<Rectangle> retVal = new List<Rectangle>();
				nsIDOMClientRectList domRectangles = DomRange.GetClientRects();
				for(uint i = 0; i < domRectangles.GetLengthAttribute(); i++)
				{
					nsIDOMClientRect domRect = domRectangles.Item(i);
					//retVal.Add(new Rectangle((int)domRect.GetLeftAttribute(), (int)domRect.GetTopAttribute(), (int)domRect.GetWidthAttribute(), (int)domRect.GetHeightAttribute());
					yield return new Rectangle((int)domRect.GetLeftAttribute(), (int)domRect.GetTopAttribute(), (int)domRect.GetWidthAttribute(), (int)domRect.GetHeightAttribute());
				}

				yield break;
			}
		}

		public bool IsPointInRange(GeckoNode node, int offset)
		{
			return DomRange.IsPointInRange(node.DomObject, offset);			
		}
	}
}
