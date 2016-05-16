using System;

namespace Gecko
{
    public class ConsoleMessageEventArgs : EventArgs
	{
		public string Message { get; protected set; }

		public ConsoleMessageEventArgs( string message )
		{
			Message = message;
		}
	}
}
