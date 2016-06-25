using Anathema.Source.Engine;
using Anathema.Source.Engine.DotNetObjectCollector;
using Anathema.Source.Engine.OperatingSystems;
using Anathema.Source.Engine.Processes;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Anathema.Source.Utils.AddressResolver
{
    class AddressResolver : RepeatedTask, IProcessObserver
    {
        private static Lazy<AddressResolver> AddressResolverInstance = new Lazy<AddressResolver>(() => { return new AddressResolver(); });

        private const String Addition = "+";
        private const String Subtraction = "-";
        private readonly Char[] AdditionArray = new Char[] { Addition[0] };
        private readonly Char[] SubtractionArray = new Char[] { Subtraction[0] };
        private readonly Char[] AllOperations = new Char[] { Addition[0], Subtraction[0] };

        private EngineCore EngineCore;

        private const Int32 ResolveIntervalInitial = 200;
        private const Int32 ResolveInterval = 5000;

        private Dictionary<String, DotNetObject> NameMap;
        private IEnumerable<NormalizedModule> Modules;

        private enum PendingOperationEnum
        {
            None,
            Addition,
            Subtraction
        }

        private AddressResolver()
        {
            InitializeProcessObserver();

            NameMap = new Dictionary<String, DotNetObject>();
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

        /// <summary>
        /// Resolves a given address expression, which may include addresses, modules, .NET objects, and arethmetic
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public IntPtr ResolveExpression(String Expression)
        {
            if (Expression == null || Expression == String.Empty)
                return IntPtr.Zero;

            return ResolveTokenizedExpression(TokenizeExpression(Expression), IntPtr.Zero);
        }

        private IntPtr ResolveTokenizedExpression(IEnumerable<String> Tokens, IntPtr PendingValue, PendingOperationEnum PendingOperation = PendingOperationEnum.None)
        {
            if (Tokens == null || Tokens.Count() == 0)
                return IntPtr.Zero;

            // Handle operator tokens
            switch (Tokens.First())
            {
                case Addition:
                    return ResolveTokenizedExpression(Tokens.Skip(1), PendingValue, PendingOperationEnum.Addition);
                case Subtraction:
                    return ResolveTokenizedExpression(Tokens.Skip(1), PendingValue, PendingOperationEnum.Subtraction);
                default:
                    break;
            }

            IntPtr Result = PendingValue;

            // Try to resolve as raw address
            if (CheckSyntax.CanParseAddress(Tokens.First()))
                Result = Conversions.AddressToValue(Tokens.First()).ToIntPtr();

            // Try to resolve as module
            foreach (NormalizedModule Module in Modules)
                if (String.Compare(Module?.Name, Tokens.First(), StringComparison.OrdinalIgnoreCase) == 0)
                    Result = Module.BaseAddress;

            // Try to resolve as .NET object
            DotNetObject DotNetObject;
            if (NameMap.TryGetValue(Tokens.First(), out DotNetObject))
                Result = DotNetObject.GetAddress();

            // Execute any pending operations
            switch (PendingOperation)
            {
                case PendingOperationEnum.Addition:
                    Result = PendingValue.Add(Result);
                    break;
                case PendingOperationEnum.Subtraction:
                    Result = PendingValue.Subtract(Result);
                    break;
                case PendingOperationEnum.None:
                    break;
            }

            if (Tokens.Count() == 1)
                return Result;

            return ResolveTokenizedExpression(Tokens.Skip(1), Result);
        }

        public IEnumerable<String> TokenizeExpression(String Expression)
        {
            List<String> Result = new List<String>();
            String Buffer = String.Empty;

            // Clean input -- trailing white space and quotes are ignored
            Expression = Expression.Trim().Replace("\"", "");

            foreach (Char Char in Expression)
            {
                if (AllOperations.Contains(Char))
                {
                    if (Buffer.Length > 0)
                        Result.Add(Buffer);

                    Result.Add(Char.ToString(CultureInfo.InvariantCulture));
                    Buffer = String.Empty;
                }
                else
                {
                    Buffer += Char;
                }
            }

            if (Buffer != String.Empty)
                Result.Add(Buffer);

            Result.ForEach(X => X = X.Trim());

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
            this.NameMap = NameMap;

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

        protected override void End() { }

    } // End class

} // End namespace