using Ana.Source.Engine.AddressResolver.DotNet;
using Ana.Source.Engine.OperatingSystems;
using Ana.Source.Engine.Processes;
using Ana.Source.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ana.Source.Engine.AddressResolver
{
    public class AddressResolver : RepeatedTask, IProcessObserver
    {
        // Singleton instance of Address Resolver
        private static Lazy<AddressResolver> addressResolverInstance = new Lazy<AddressResolver>(() => { return new AddressResolver(); }, LazyThreadSafetyMode.PublicationOnly);

        private EngineCore engineCore;

        private const Int32 resolveIntervalInitial = 200;
        private const Int32 resolveInterval = 5000;

        private Dictionary<String, DotNetObject> dotNetNameMap;
        private IEnumerable<NormalizedModule> modules;

        public enum ResolveTypeEnum
        {
            Module,
            DotNet,
            // Java
        }

        private AddressResolver()
        {
            InitializeProcessObserver();

            dotNetNameMap = new Dictionary<String, DotNetObject>();
            modules = new List<NormalizedModule>();

            this.Begin();
        }

        public static AddressResolver GetInstance()
        {
            return addressResolverInstance.Value;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore engineCore)
        {
            this.engineCore = engineCore;
        }

        public IntPtr ResolveDotNetObject(String identifier)
        {
            IntPtr result = IntPtr.Zero;
            DotNetObject dotNetObject;

            if (dotNetNameMap.TryGetValue(identifier, out dotNetObject))
                result = dotNetObject.GetAddress();

            return result;
        }

        public override void Begin()
        {
            base.Begin();

            this.UpdateInterval = resolveIntervalInitial;
        }

        protected override void Update()
        {
            Dictionary<String, DotNetObject> nameMap = new Dictionary<String, DotNetObject>();
            List<DotNetObject> objectTrees = DotNetObjectCollector.GetInstance().GetObjectTrees();

            // Build module list
            modules = engineCore?.Memory?.GetModules();

            // Build .NET object list
            objectTrees?.ForEach(x => BuildNameMap(nameMap, x));
            this.dotNetNameMap = nameMap;

            // After we have successfully grabbed information from the process, slow the update interval
            if ((modules != null && modules.Count() != 0) || objectTrees != null)
                this.UpdateInterval = resolveInterval;
        }

        private void BuildNameMap(Dictionary<String, DotNetObject> nameMap, DotNetObject currentObject)
        {
            if (currentObject == null || currentObject.GetFullName() == null)
                return;

            nameMap[currentObject.GetFullName()] = currentObject;
            currentObject?.GetChildren()?.ForEach(x => BuildNameMap(nameMap, x));
        }

        protected override void End()
        {
            base.End();
        }
    }
    //// End class
}
//// End namespace