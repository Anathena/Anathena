using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko
{
	/// <summary>
	///  REPORT_ERROR 0 - Report is an error.
	///  REPORT_WARNING 1 - Report is a warning.
	///  REPORT_EXCEPTION 2 - Report represents an uncaught exception.
	///  REPORT_STRICT 4 - Report is due to strict mode.
	/// </summary> 
	[Flags]
	public enum ErrorFlags
	{
		REPORT_ERROR = 0,
		REPORT_WARNING = 1,
		REPORT_EXCEPTION = 2,
		REPORT_STRICT = 4
	}

	public class JavascriptErrorEventArgs : EventArgs
	{
		public string Message { get; protected set; }
		public string Filename { get; protected set; }
		public uint Line { get; protected set; }
		public uint Pos { get; protected set; }
		public ErrorFlags Flags { get; protected set; }
		public uint ErrorNumber { get; protected set; }

		public JavascriptErrorEventArgs(string message, string filename, uint line, uint pos, uint flags, uint errorNumber)
		{
			Message = message;
			Filename = filename;
			Line = line;
			Pos = pos;
			Flags = (ErrorFlags)flags;
			ErrorNumber = errorNumber;
		}
	}
}
