using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySharp.MemoryManagement.Debugger
{
    class DebuggerException : Exception
    {
        public DebuggerException(string Message) : base(Message) { }
    }
}
