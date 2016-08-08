using Anathena.Source.Engine;
using Anathena.Source.Engine.Processes;
using Anathena.Source.Project;
using Anathena.Source.Project.ProjectItems;
using Anathena.Source.Snapshots;
using Anathena.Source.Utils;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Anathena.Source.Results.ScanResults
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class ScanResults : IScanResultsModel, IProcessObserver
    {
        // Singleton instance of Results
        private static Lazy<ScanResults> ResultsInstance = new Lazy<ScanResults>(() => { return new ScanResults(); }, LazyThreadSafetyMode.PublicationOnly);

        private EngineCore EngineCore;
        private Snapshot Snapshot;

        private Type ScanType;

        private Int32 StartReadIndex;
        private Int32 EndReadIndex;
        private ConcurrentDictionary<Int32, String> IndexValueMap;
        private Boolean ForceRefreshFlag;

        private Object ResultsLock;

        private const String EmptyValue = "-";
        private const String EmptyLabel = "-";

        private ScanResults()
        {
            ResultsLock = new Object();
            InitializeProcessObserver();
            SetScanType(typeof(Int32));
            ForceRefreshFlag = false;
            IndexValueMap = new ConcurrentDictionary<Int32, String>();
            Begin();
        }

        public static ScanResults GetInstance()
        {
            return ResultsInstance.Value;
        }

        ~ScanResults()
        {
            TriggerEnd();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
        }

        public override void OnGUIOpen()
        {
            ForceRefresh();
        }

        public override void ForceRefresh()
        {
            ForceRefreshFlag = true;
        }

        public void EnableResults()
        {
            OnEventEnableResults(new ScanResultsEventArgs());
            Begin();
        }

        public void DisableResults()
        {
            CancelFlag = true;
            OnEventDisableResults(new ScanResultsEventArgs());
        }

        public override void SetScanType(Type ScanType)
        {
            this.ScanType = ScanType;
        }

        public override Type GetScanType()
        {
            return ScanType;
        }

        public override void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            this.StartReadIndex = StartReadIndex;
            this.EndReadIndex = EndReadIndex;
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            using (TimedLock.Lock(ResultsLock))
            {
                base.Update();

                Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot(false);
                if (ForceRefreshFlag || Snapshot != ActiveSnapshot)
                {
                    ForceRefreshFlag = false;
                    Snapshot = ActiveSnapshot;

                    // Send the size of the filtered memory to the display
                    ScanResultsEventArgs Args = new ScanResultsEventArgs();
                    Args.ElementCount = (Snapshot == null ? 0 : Snapshot.GetElementCount());
                    Args.MemorySize = (Snapshot == null ? 0 : Snapshot.GetMemorySize());
                    Task.Run(() => { OnEventUpdateItemCounts(Args); });
                    return;
                }

                if (Snapshot == null)
                    return;

                IndexValueMap.Clear();

                Int32 ElementCount = (Int32)(Math.Min((UInt64)Int32.MaxValue, Snapshot.GetElementCount()));

                for (Int32 Index = StartReadIndex; Index <= EndReadIndex; Index++)
                {
                    if (Index < 0 || Index >= ElementCount)
                        continue;

                    Boolean ReadSuccess;
                    String Value = EngineCore.Memory.Read(ScanType, Snapshot[Index].BaseAddress, out ReadSuccess).ToString();

                    IndexValueMap[Index] = Value;
                }
            }

            OnEventReadValues(new ScanResultsEventArgs());
        }

        protected override void End()
        {
            base.End();
        }

        public override void AddSelectionToTable(Int32 MinIndex, Int32 MaxIndex)
        {
            using (TimedLock.Lock(ResultsLock))
            {
                const Int32 MaxAdd = 4096;

                Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot(false);

                if (ActiveSnapshot == null)
                    return;

                if (MinIndex < 0)
                    MinIndex = 0;

                if (MaxIndex > (Int32)ActiveSnapshot.GetElementCount())
                    MaxIndex = (Int32)ActiveSnapshot.GetElementCount();

                Int32 Count = 0;
                for (Int32 Index = MinIndex; Index <= MaxIndex; Index++)
                {
                    String Value = String.Empty;
                    IndexValueMap.TryGetValue(Index, out Value);

                    AddressItem NewAddress = new AddressItem(ActiveSnapshot[Index].BaseAddress, ScanType, "No Description", Value: Value);
                    ProjectExplorer.GetInstance().AddProjectItem(NewAddress);

                    if (++Count >= MaxAdd)
                        break;
                }
            }
        }

        public override IntPtr GetAddressAtIndex(Int32 Index)
        {
            using (TimedLock.Lock(ResultsLock))
            {
                if (Snapshot == null || Index >= (Int32)Snapshot.GetElementCount())
                    return IntPtr.Zero;

                return Snapshot[Index].BaseAddress;
            }
        }

        public override String GetValueAtIndex(Int32 Index)
        {
            using (TimedLock.Lock(ResultsLock))
            {
                if (IndexValueMap.ContainsKey(Index))
                    return IndexValueMap[Index];

                return EmptyValue;
            }
        }

        public override String GetLabelAtIndex(Int32 Index)
        {
            using (TimedLock.Lock(ResultsLock))
            {
                if (Snapshot == null || Index >= (Int32)Snapshot.GetElementCount())
                    return EmptyLabel;

                dynamic Label = String.Empty;
                if (((dynamic)Snapshot)[Index].ElementLabel != null)
                    Label = ((dynamic)Snapshot)[Index].ElementLabel;

                return Label.ToString();
            }
        }

    } // End class

} // End namespace