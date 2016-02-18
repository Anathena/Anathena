namespace Gecko
{
	public enum NodeType
	{
		Element = ( int ) nsIDOMNodeConsts.ELEMENT_NODE,
		Attribute = ( int ) nsIDOMNodeConsts.ATTRIBUTE_NODE,
		Text = ( int ) nsIDOMNodeConsts.TEXT_NODE,
		CDataSection = ( int ) nsIDOMNodeConsts.CDATA_SECTION_NODE,
		EntityReference = ( int ) nsIDOMNodeConsts.ENTITY_REFERENCE_NODE,
		Entity = ( int ) nsIDOMNodeConsts.ENTITY_NODE,
		ProcessingInstruction = ( int ) nsIDOMNodeConsts.PROCESSING_INSTRUCTION_NODE,
		Comment = ( int ) nsIDOMNodeConsts.COMMENT_NODE,
		Document = ( int ) nsIDOMNodeConsts.DOCUMENT_NODE,
		DocumentType = ( int ) nsIDOMNodeConsts.DOCUMENT_TYPE_NODE,
		DocumentFragment = ( int ) nsIDOMNodeConsts.DOCUMENT_FRAGMENT_NODE,
		Notation = ( int ) nsIDOMNodeConsts.NOTATION_NODE
	}
}