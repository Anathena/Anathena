using Anathema.Source.Graphics;
using Anathema.Source.SystemInternals.OperatingSystems;
using Anathema.Source.SystemInternals.Processes;
using Anathema.Source.Utils.Extensions;
using System;

namespace Anathema.Source.SystemInternals.LuaWrapper.Graphics
{
    class LuaGraphicsCore : IGraphicsCore, IProcessObserver
    {
        private Engine Engine;
        private Object AccessLock;

        public LuaGraphicsCore()
        {
            InitializeProcessObserver();

            AccessLock = new Object();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngine(Engine Engine)
        {
            this.Engine = Engine;
        }

        /// <summary>
        /// Gives access to the graphics in the target process, injecting the hooks if needed
        /// </summary>
        /// <returns></returns>
        private IGraphicsInterface GetGraphicsInterface()
        {
            lock (AccessLock)
            {
                if (Engine.GraphicsConnector.GetGraphicsInterface() == null)
                    Engine.GraphicsConnector.Inject(Engine.Memory.GetProcess());

                return Engine.GraphicsConnector.GetGraphicsInterface();
            }
        }

        public void DrawString(String Text, Int32 LocationX, Int32 LocationY)
        {
            this.PrintDebugTag();

            GetGraphicsInterface().DrawString(Text, LocationX, LocationY);
        }

    } // End class

} // End namespace