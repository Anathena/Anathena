using System;

namespace Anathema.Source.Engine.InputCapture.MouseKeyHook.WinApi
{
    internal class HookResult : IDisposable
    {
        private readonly HookProcedureHandle MHandle;
        private readonly HookProcedure MProcedure;

        public HookResult(HookProcedureHandle Handle, HookProcedure Procedure)
        {
            MHandle = Handle;
            MProcedure = Procedure;
        }

        public HookProcedureHandle Handle
        {
            get { return MHandle; }
        }

        public HookProcedure Procedure
        {
            get { return MProcedure; }
        }

        public void Dispose()
        {
            MHandle.Dispose();
        }

    } // End class

} // End namespace