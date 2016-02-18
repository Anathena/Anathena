namespace Gecko
{
	public enum TextBoundaryType : long
	{
		CharBoundary = nsIAccessiblePivotConsts.CHAR_BOUNDARY,
		WordBoundary = nsIAccessiblePivotConsts.WORD_BOUNDARY,
		LineBoundary = nsIAccessiblePivotConsts.LINE_BOUNDARY,
		AttributeRangeBoundary = nsIAccessiblePivotConsts.ATTRIBUTE_RANGE_BOUNDARY,
	}
}