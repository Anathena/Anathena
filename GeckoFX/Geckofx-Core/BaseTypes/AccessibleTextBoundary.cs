namespace Gecko
{
	public enum AccessibleTextBoundary : int
	{
		TextOffsetEndOfText = (int)nsIAccessibleTextConsts.TEXT_OFFSET_END_OF_TEXT,
		TextOffsetCaret = (int)nsIAccessibleTextConsts.TEXT_OFFSET_CARET,
		BoundaryChar = (int)nsIAccessibleTextConsts.BOUNDARY_CHAR,
		BoundaryWordStart = (int)nsIAccessibleTextConsts.BOUNDARY_WORD_START,
		BoundaryWordEnd = (int)nsIAccessibleTextConsts.BOUNDARY_WORD_END,
		BoundarySentenceStart = (int)nsIAccessibleTextConsts.BOUNDARY_SENTENCE_START,
		BoundarySentenceEnd = (int)nsIAccessibleTextConsts.BOUNDARY_SENTENCE_END,
		BoundaryLineStart = (int)nsIAccessibleTextConsts.BOUNDARY_LINE_START,
		BoundaryLineEnd = (int)nsIAccessibleTextConsts.BOUNDARY_LINE_END,		
	}
}