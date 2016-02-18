namespace Gecko
{
	public enum JSType : int
	{
		JSTYPE_VOID,                /* undefined */
		JSTYPE_OBJECT,              /* object */
		JSTYPE_FUNCTION,            /* function */
		JSTYPE_STRING,              /* string */
		JSTYPE_NUMBER,              /* number */
		JSTYPE_BOOLEAN,             /* boolean */
		JSTYPE_NULL,                /* null */
		JSTYPE_XML,                 /* xml object */
		JSTYPE_LIMIT
	}
}