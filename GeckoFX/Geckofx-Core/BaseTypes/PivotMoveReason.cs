namespace Gecko
{
	public enum PivotMoveReason : short
	{
		None = (short)nsIAccessiblePivotConsts.REASON_NONE,
		
		// 
		Next = (short)nsIAccessiblePivotConsts.REASON_NEXT,
		
		// 
		Prev = (short)nsIAccessiblePivotConsts.REASON_PREV,
		
		// 
		First = (short)nsIAccessiblePivotConsts.REASON_FIRST,
		
		// 
		Last = (short)nsIAccessiblePivotConsts.REASON_LAST,
		
		// 
		Text = (short)nsIAccessiblePivotConsts.REASON_TEXT,
		
		// 
		Point = (short)nsIAccessiblePivotConsts.REASON_POINT
	}
}