using Anathema.Source.SystemInternals.Graphics;
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
            if (Engine == null)
                return null;

            lock (AccessLock)
            {
                if (Engine.HookCreator.GetGraphicsInterface() == null)
                    Engine.HookCreator.Inject(Engine.Memory.GetProcess());

                return Engine.HookCreator.GetGraphicsInterface();
            }
        }

        public Guid CreateText(String Text, Int32 LocationX, Int32 LocationY)
        {
            this.PrintDebugTag();

            return GetGraphicsInterface().CreateText(Text, LocationX, LocationY);
        }

        public Guid CreateImage(String FileName, Int32 LocationX, Int32 LocationY)
        {
            this.PrintDebugTag();

            return GetGraphicsInterface().CreateImage(FileName, LocationX, LocationY);
        }

        public void DestroyObject(Guid Guid)
        {
            this.PrintDebugTag();

            GetGraphicsInterface().DestroyObject(Guid);
        }

        public void ShowObject(Guid Guid)
        {
            this.PrintDebugTag();

            GetGraphicsInterface().ShowObject(Guid);
        }

        public void HideObject(Guid Guid)
        {
            this.PrintDebugTag();

            GetGraphicsInterface().HideObject(Guid);
        }

    } // End class

} // End namespace