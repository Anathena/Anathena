using Ana.Source.Engine;
using Ana.Source.Engine.Hook.Graphics;
using Ana.Source.Engine.Processes;
using Ana.Source.Utils.Extensions;
using System;

namespace Ana.Source.LuaEngine.Graphics
{
    class LuaGraphicsCore : IGraphicsCore, IProcessObserver
    {
        private EngineCore EngineCore;
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

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
        }

        /// <summary>
        /// Gives access to the graphics in the target process, injecting the hooks if needed
        /// </summary>
        /// <returns></returns>
        private IGraphicsInterface GetGraphicsInterface()
        {
            if (EngineCore == null)
                return null;

            lock (AccessLock)
            {
                if (EngineCore.HookCreator.GetGraphicsInterface() == null)
                    EngineCore.HookCreator.Inject(EngineCore.Memory.GetProcess());

                return EngineCore.HookCreator.GetGraphicsInterface();
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