using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko
{
	public class TreeView
	{
		private nsITreeView _treeView;

		internal TreeView( nsITreeView treeView )
		{
			_treeView = treeView;
		}

		/// <summary>
		/// The total number of rows in the tree (including the offscreen rows).
		/// </summary>
		public int RowCount
		{
			get { return _treeView.GetRowCountAttribute(); }
		}

		/// <summary>
		/// The selection for this view.
		/// </summary>
		public TreeSelection Selection
		{
			get { return new TreeSelection( _treeView.GetSelectionAttribute() ); }
			set { _treeView.SetSelectionAttribute( value._treeSelection ); }
		}

		/// <summary>
		/// An atomized list of properties for a given row.  Each property, x, that
		/// the view gives back will cause the pseudoclass :moz-tree-row-x
		/// to be matched on the pseudoelement ::moz-tree-row.
		/// </summary>
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//void GetRowProperties(int index, [MarshalAs(UnmanagedType.Interface)] nsISupportsArray properties);

		/// <summary>
		/// An atomized list of properties for a given cell.  Each property, x, that
		/// the view gives back will cause the pseudoclass :moz-tree-cell-x
		/// to be matched on the ::moz-tree-cell pseudoelement.
		/// </summary>
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//void GetCellProperties(int row, [MarshalAs(UnmanagedType.Interface)] nsITreeColumn col, [MarshalAs(UnmanagedType.Interface)] nsISupportsArray properties);

		/// <summary>
		/// Called to get properties to paint a column background.  For shading the sort
		/// column, etc.
		/// </summary>
		//MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//void GetColumnProperties([MarshalAs(UnmanagedType.Interface)] nsITreeColumn col, [MarshalAs(UnmanagedType.Interface)] nsISupportsArray properties);

		/// <summary>
		/// Methods that can be used to test whether or not a twisty should be drawn,
		/// and if so, whether an open or closed twisty should be used.
		/// </summary>
		public bool IsContainer(int index)
		{
			return _treeView.IsContainer( index );
		}

		/// <summary>Member IsContainerOpen </summary>
		/// <param name='index'> </param>
		/// <returns>A System.Boolean</returns>
		public bool IsContainerOpen(int index)
		{
			return _treeView.IsContainerOpen( index );
		}

		/// <summary>Member IsContainerEmpty </summary>
		/// <param name='index'> </param>
		/// <returns>A System.Boolean</returns>
		public bool IsContainerEmpty(int index)
		{
			return _treeView.IsContainerEmpty( index );
		}

		/// <summary>
		/// isSeparator is used to determine if the row at index is a separator.
		/// A value of true will result in the tree drawing a horizontal separator.
		/// The tree uses the ::moz-tree-separator pseudoclass to draw the separator.
		/// </summary>
		public bool IsSeparator(int index)
		{
			return _treeView.IsSeparator( index );
		}

		/// <summary>
		/// Specifies if there is currently a sort on any column. Used mostly by dragdrop
		/// to affect drop feedback.
		/// </summary>
		public bool IsSorted
		{
			get { return _treeView.IsSorted(); }
		}

		/// <summary>
		/// Methods used by the drag feedback code to determine if a drag is allowable at
		/// the current location. To get the behavior where drops are only allowed on
		/// items, such as the mailNews folder pane, always return false when
		/// the orientation is not DROP_ON.
		/// </summary>
		//bool CanDrop(int index, int orientation, [MarshalAs(UnmanagedType.Interface)] nsIDOMDataTransfer dataTransfer);

		/// <summary>
		/// Called when the user drops something on this view. The |orientation| param
		/// specifies before/on/after the given |row|.
		/// </summary>
		//void Drop(int row, int orientation, [MarshalAs(UnmanagedType.Interface)] nsIDOMDataTransfer dataTransfer);

		/// <summary>
		/// Methods used by the tree to draw thread lines in the tree.
		/// getParentIndex is used to obtain the index of a parent row.
		/// If there is no parent row, getParentIndex returns -1.
		/// </summary>
		public int GetParentIndex(int rowIndex)
		{
			return _treeView.GetParentIndex( rowIndex );
		}

		/// <summary>
		/// hasNextSibling is used to determine if the row at rowIndex has a nextSibling
		/// that occurs *after* the index specified by afterIndex.  Code that is forced
		/// to march down the view looking at levels can optimize the march by starting
		/// at afterIndex+1.
		/// </summary>
		public bool HasNextSibling(int rowIndex, int afterIndex)
		{
			return _treeView.HasNextSibling( rowIndex, afterIndex );
		}

		/// <summary>
		/// The level is an integer value that represents
		/// the level of indentation.  It is multiplied by the width specified in the
		/// :moz-tree-indentation pseudoelement to compute the exact indendation.
		/// </summary>
		public int GetLevel(int index)
		{
			return _treeView.GetLevel( index );
		}

		/// <summary>
		/// The image path for a given cell. For defining an icon for a cell.
		/// If the empty string is returned, the :moz-tree-image pseudoelement
		/// will be used.
		/// </summary>
		public string GetImageSrc(int row,TreeColumn col)
		{
			return nsString.Get( _treeView.GetImageSrc, row, col._treeColumn );
		}

		/// <summary>
		/// The progress mode for a given cell. This method is only called for
		/// columns of type |progressmeter|.
		/// </summary>
		public int GetProgressMode(int row, TreeColumn col)
		{
			return _treeView.GetProgressMode( row, col._treeColumn );
		}

		/// <summary>
		/// The value for a given cell. This method is only called for columns
		/// of type other than |text|.
		/// </summary>
		public string GetCellValue(int row, TreeColumn col)
		{
			return nsString.Get( _treeView.GetCellValue, row, col._treeColumn );
		}

		/// <summary>
		/// The text for a given cell.  If a column consists only of an image, then
		/// the empty string is returned.
		/// </summary>
		public string GetCellText(int row, TreeColumn col)
		{
			return nsString.Get(_treeView.GetCellText, row, col._treeColumn);
		}

		/// <summary>
		/// Called during initialization to link the view to the front end box object.
		/// </summary>
		public void SetTree(TreeBoxObject tree)
		{
			_treeView.SetTree( tree._treeBoxObject );
		}

		/// <summary>
		/// Called on the view when an item is opened or closed.
		/// </summary>
		public void ToggleOpenState(int index)
		{
			_treeView.ToggleOpenState( index );
		}
 
		/// <summary>
		/// Called on the view when a header is clicked.
		/// </summary>
		public void CycleHeader(TreeColumn col)
		{
			_treeView.CycleHeader( col._treeColumn );
		}

		/// <summary>
		/// Should be called from a XUL onselect handler whenever the selection changes.
		/// </summary>
		public void SelectionChanged()
		{
			_treeView.SelectionChanged();
		}

		/// <summary>
		/// Called on the view when a cell in a non-selectable cycling column (e.g., unread/flag/etc.) is clicked.
		/// </summary>
		public void CycleCell(int row, TreeColumn col)
		{
			_treeView.CycleCell(row, col._treeColumn);
		}

		/// <summary>
		/// isEditable is called to ask the view if the cell contents are editable.
		/// A value of true will result in the tree popping up a text field when
		/// the user tries to inline edit the cell.
		/// </summary>
		public bool IsEditable(int row,  TreeColumn col)
		{
			return _treeView.IsEditable( row, col._treeColumn );
		}

		/// <summary>
		/// isSelectable is called to ask the view if the cell is selectable.
		/// This method is only called if the selection style is |cell| or |text|.
		/// XXXvarga shouldn't this be called isCellSelectable?
		/// </summary>
		public bool IsSelectable(int row,TreeColumn col)
		{
			return _treeView.IsSelectable( row, col._treeColumn );
		}

		/// <summary>
		/// setCellValue is called when the value of the cell has been set by the user.
		/// This method is only called for columns of type other than |text|.
		/// </summary>
		public void SetCellValue(int row, TreeColumn col,  string value)
		{
			nsString.Set( _treeView.SetCellValue, row, col._treeColumn, value );
		}

		/// <summary>
		/// setCellText is called when the contents of the cell have been edited by the user.
		/// </summary>
		public void SetCellText(int row, TreeColumn col,string value)
		{
			nsString.Set( _treeView.SetCellText, row, col._treeColumn, value );
		}

		/// <summary>
		/// A command API that can be used to invoke commands on the selection.  The tree
		/// will automatically invoke this method when certain keys are pressed.  For example,
		/// when the DEL key is pressed, performAction will be called with the "delete" string.
		/// </summary>
		public void PerformAction( string action)
		{
			_treeView.PerformAction( action );
		}

		/// <summary>
		/// A command API that can be used to invoke commands on a specific row.
		/// </summary>
		public void PerformActionOnRow(string action, int row)
		{
			_treeView.PerformActionOnRow( action,row );
		}

		/// <summary>
		/// A command API that can be used to invoke commands on a specific cell.
		/// </summary>
		public void PerformActionOnCell( string action, int row,  TreeColumn col)
		{
			_treeView.PerformActionOnCell( action, row, col._treeColumn );
		}
	}


	public class TreeSelection
	{
		internal nsITreeSelection _treeSelection;

		internal TreeSelection(nsITreeSelection treeSelection)
		{
			_treeSelection = treeSelection;
		}

		/// <summary>
		/// The tree widget for this selection.
		/// </summary>
		public TreeBoxObject Tree
		{
			get{return new TreeBoxObject(_treeSelection.GetTreeAttribute()); }
			set { _treeSelection.SetTreeAttribute( value._treeBoxObject ); }
		}

		/// <summary>
		/// Boolean indicating single selection.
		/// </summary>
		public bool Single
		{
			get { return _treeSelection.GetSingleAttribute(); }
		}

		
		/// <summary>
		/// The number of rows currently selected in this tree.
		/// </summary>
		public int Count
		{
			get { return _treeSelection.GetCountAttribute(); }
		}

		/// <summary>
		/// Indicates whether or not the row at the specified index is
		/// part of the selection.
		/// </summary>
		public bool IsSelected(int index)
		{
			return _treeSelection.IsSelected( index );
		}

		/// <summary>
		/// Deselect all rows and select the row at the specified index.
		/// </summary>
		public void Select(int index)
		{
			_treeSelection.Select( index );
		}

		/// <summary>
		/// Perform a timed select.
		/// </summary>
		public void TimedSelect(int index, int delay)
		{
			_treeSelection.TimedSelect( index, delay );
		}

		/// <summary>
		/// Toggle the selection state of the row at the specified index.
		/// </summary>
		public void ToggleSelect(int index)
		{
			_treeSelection.ToggleSelect( index );
		}

		/// <summary>
		/// Select the range specified by the indices.  If augment is true,
		/// then we add the range to the selection without clearing out anything
		/// else.  If augment is false, everything is cleared except for the specified range.
		/// </summary>
		public void RangedSelect(int startIndex, int endIndex,bool augment)
		{
			_treeSelection.RangedSelect( startIndex, endIndex, augment );
		}

		/// <summary>
		/// Clears the range.
		/// </summary>
		public void ClearRange(int startIndex, int endIndex)
		{
			_treeSelection.ClearRange( startIndex, endIndex );
		}

		/// <summary>
		/// Clears the selection.
		/// </summary>
		public void ClearSelection()
		{
			_treeSelection.ClearSelection();
		}

		/// <summary>
		/// Inverts the selection.
		/// </summary>
		public void InvertSelection()
		{
			_treeSelection.InvertSelection();
		}

		/// <summary>
		/// Selects all rows.
		/// </summary>
		public void SelectAll()
		{
			_treeSelection.SelectAll();
		}

		/// <summary>
		/// Iterate the selection using these methods.
		/// </summary>
		public int RangeCount
		{
			get { return _treeSelection.GetRangeCount(); }
		}

		/// <summary>Member GetRangeAt </summary>
		/// <param name='i'> </param>
		/// <param name='min'> </param>
		/// <param name='max'> </param>
		public void GetRangeAt(int i, ref int min, ref int max)
		{
			_treeSelection.GetRangeAt( i, ref min, ref max );
		}

		/// <summary>
		/// Can be used to invalidate the selection.
		/// </summary>
		public void InvalidateSelection()
		{
			_treeSelection.InvalidateSelection();
		}

		/// <summary>
		/// Called when the row count changes to adjust selection indices.
		/// </summary>
		public void AdjustSelection(int index, int count)
		{
			_treeSelection.AdjustSelection( index,count );
		}

		/// <summary>
		/// This attribute is a boolean indicating whether or not the
		/// "select" event should fire when the selection is changed using
		/// one of our methods.  A view can use this to temporarily suppress
		/// the selection while manipulating all of the indices, e.g., on
		/// a sort.
		/// Note: setting this attribute to false will fire a select event.
		/// </summary>
		public bool SelectEventsSuppressed
		{
			get { return _treeSelection.GetSelectEventsSuppressedAttribute(); }
			set { _treeSelection.SetSelectEventsSuppressedAttribute( value ); }
		}

		/// <summary>
		/// The current item (the one that gets a focus rect in addition to being
		/// selected).
		/// </summary>
		public int CurrentIndex
		{
			get { return _treeSelection.GetCurrentIndexAttribute(); }
			set { _treeSelection.SetCurrentIndexAttribute( value ); }
		}

		/// <summary>
		/// The current column.
		/// </summary>
		public TreeColumn CureentColumn
		{
			get { return new TreeColumn( _treeSelection.GetCurrentColumnAttribute() ); }
			set { _treeSelection.SetCurrentColumnAttribute( value._treeColumn ); }
		}

		/// <summary>
		/// The selection "pivot".  This is the first item the user selected as
		/// part of a ranged select.
		/// </summary>
		public int ShiftSelectPivot
		{
			get { return _treeSelection.GetShiftSelectPivotAttribute(); }
		}
	}


	public class TreeBoxObject
	{
		internal nsITreeBoxObject _treeBoxObject;

		internal TreeBoxObject(nsITreeBoxObject treeBoxObject)
		{
			_treeBoxObject = treeBoxObject;
		}
	}


	public class TreeColumn
	{
		internal nsITreeColumn _treeColumn;

		internal TreeColumn(nsITreeColumn treeColumn)
		{
			_treeColumn = treeColumn;
		}
	}
}
