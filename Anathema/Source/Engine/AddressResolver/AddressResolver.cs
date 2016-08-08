using Anathema.Source.Engine.AddressResolver.DotNet;
using Anathema.Source.Engine.OperatingSystems;
using Anathema.Source.Engine.Processes;
using Anathema.Source.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Anathema.Source.Engine.AddressResolver
{
    public class AddressResolver : RepeatedTask, IProcessObserver
    {
        // Singleton instance of Address Resolver
        private static Lazy<AddressResolver> AddressResolverInstance = new Lazy<AddressResolver>(() => { return new AddressResolver(); }, LazyThreadSafetyMode.PublicationOnly);

        private EngineCore EngineCore;

        private const Int32 ResolveIntervalInitial = 200;
        private const Int32 ResolveInterval = 5000;

        private Dictionary<String, DotNetObject> DotNetNameMap;
        private IEnumerable<NormalizedModule> Modules;

        public enum ResolveTypeEnum
        {
            Module,
            DotNet,
            // Java
        }

        private AddressResolver()
        {
            InitializeProcessObserver();

            DotNetNameMap = new Dictionary<String, DotNetObject>();
            Modules = new List<NormalizedModule>();

            this.Begin();
        }

        public static AddressResolver GetInstance()
        {
            return AddressResolverInstance.Value;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
        }

        public IntPtr ResolveDotNetObject(String Identifier)
        {
            IntPtr Result = IntPtr.Zero;
            DotNetObject DotNetObject;

            if (DotNetNameMap.TryGetValue(Identifier, out DotNetObject))
                Result = DotNetObject.GetAddress();

            return Result;
        }

        public override void Begin()
        {
            base.Begin();

            this.UpdateInterval = ResolveIntervalInitial;
        }

        protected override void Update()
        {
            Dictionary<String, DotNetObject> NameMap = new Dictionary<String, DotNetObject>();
            List<DotNetObject> ObjectTrees = DotNetObjectCollector.GetInstance().GetObjectTrees();

            // Build module list
            Modules = EngineCore?.Memory?.GetModules();

            // Build .NET object list
            ObjectTrees?.ForEach(X => BuildNameMap(NameMap, X));
            this.DotNetNameMap = NameMap;

            // After we have successfully grabbed information from the process, slow the update interval
            if ((Modules != null && Modules.Count() != 0) || ObjectTrees != null)
                this.UpdateInterval = ResolveInterval;
        }

        private void BuildNameMap(Dictionary<String, DotNetObject> NameMap, DotNetObject CurrentObject)
        {
            if (CurrentObject == null || CurrentObject.GetFullName() == null)
                return;

            NameMap[CurrentObject.GetFullName()] = CurrentObject;
            CurrentObject?.GetChildren()?.ForEach(X => BuildNameMap(NameMap, X));
        }

        protected override void End()
        {
            base.End();
        }

    } // End class

} // End namespace