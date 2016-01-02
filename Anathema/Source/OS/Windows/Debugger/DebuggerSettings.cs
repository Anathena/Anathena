using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinarySharp.MemoryManagement.Debugger
{
    /// <summary>
    /// Settings object used by the Debugger class.  Only purpose is to provide 
    /// a secondary object so that the Debugger does not reference the overall 
    /// EngineSettings object.  This object is always instantiated according to 
    /// the values in the current EngineSettings object.
    /// </summary>
	class DebuggerSettings
    {
        public bool HideMainWindow = false;
        public bool AutoCloseSpawnedWindows = false;
        public bool OKbeforeCLOSE = false;
        public bool AlwaysClick = false;
        public bool HideDebugger = false;
        public bool WaitForFullStartup = false;
        public bool EnableKillAlso = false;
        public string KillAlso = String.Empty;
        public bool OnlyClosedIfButtonClick = false;

        public bool IgnoreExceptions = false;

        public List<ErrorCode> ErrorCodes = new List<ErrorCode>();

        /// <summary>
        /// Standard constructor to initialze member variables according to 
        /// the provided EngineSettings object
        /// </summary>
        /// <param name="s"></param>
		public DebuggerSettings()
        {
            this.HideMainWindow = false;
            this.AutoCloseSpawnedWindows = false;
            this.OKbeforeCLOSE = false;
            this.AlwaysClick = false;
            this.EnableKillAlso = false;
            this.KillAlso = null;
            this.HideDebugger = false;
            this.WaitForFullStartup = true;
            this.OnlyClosedIfButtonClick = false;

            this.IgnoreExceptions = false;

            this.ErrorCodes = ErrorCodes;
        }
    }
}
