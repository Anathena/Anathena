using System;
using System.Collections.Generic;

namespace Anathema.Source.Engine.OperatingSystems.Windows.Debugger
{
    /// <summary>
    /// Settings object used by the Debugger class.  Only purpose is to provide 
    /// a secondary object so that the Debugger does not reference the overall 
    /// EngineSettings object.  This object is always instantiated according to 
    /// the values in the current EngineSettings object.
    /// </summary>
    class DebuggerSettings
    {
        public Boolean HideMainWindow = false;
        public Boolean AutoCloseSpawnedWindows = false;
        public Boolean OKbeforeCLOSE = false;
        public Boolean AlwaysClick = false;
        public Boolean HideDebugger = false;
        public Boolean WaitForFullStartup = false;
        public Boolean EnableKillAlso = false;
        public String KillAlso = String.Empty;
        public Boolean OnlyClosedIfButtonClick = false;

        public Boolean IgnoreExceptions = false;

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

    } // End class

} // End namespace