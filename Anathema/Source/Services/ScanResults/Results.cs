using Anathema.Services.ProcessManager;
using Anathema.Services.Snapshots;
using Anathema.User.UserAddressTable;
using Anathema.Utils.OS;
using System;
using System.Collections.Concurrent;

namespace Anathema.Services.ScanResults
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class Results : IResultsModel, IProcessObserver
    {
        private static Results ResultsInstance;
        private OSInterface OSInterface;
        private Snapshot Snapshot;

        private Type ScanType;

        private Int32 StartReadIndex;
        private Int32 EndReadIndex;
        private ConcurrentDictionary<Int32, String> IndexValueMap;
        private Boolean ForceRefreshFlag;

        private Object ResultsLock;

        private Results()
        {
            ResultsLock = new Object();
            InitializeProcessObserver();
            SetScanType(typeof(Int32));
            ForceRefreshFlag = false;
            IndexValueMap = new ConcurrentDictionary<Int32, String>();
            Begin();
        }

        public static Results GetInstance()
        {
            if (ResultsInstance == null)
                ResultsInstance = new Results();
            return ResultsInstance;
        }

        ~Results()
        {
            End();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateOSInterface(OSInterface OSInterface)
        {
            this.OSInterface = OSInterface;
        }

        public override void ForceRefresh()
        {
            lock (ResultsLock)
            {
                ForceRefreshFlag = true;
            }
        }

        public void EnableResults()
        {
            OnEventEnableResults(new ResultsEventArgs());
            Begin();
        }

        public void DisableResults()
        {
            lock (ResultsLock)
            {
                CancelFlag = true;
            }
            OnEventDisableResults(new ResultsEventArgs());
        }

        public override void SetScanType(Type ScanType)
        {
            lock (ResultsLock)
            {
                this.ScanType = ScanType;
            }
        }

        public override Type GetScanType()
        {
            lock (ResultsLock)
            {
                return ScanType;
            }
        }

        public override void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            lock (ResultsLock)
            {
                this.StartReadIndex = StartReadIndex;
                this.EndReadIndex = EndReadIndex;
            }
        }

        public override void Begin()
        {
            lock (ResultsLock)
            {
                base.Begin();
            }
        }

        protected override void Update()
        {
            lock (ResultsLock)
            {
                base.Update();

                Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot(false);
                if (ForceRefreshFlag || Snapshot != ActiveSnapshot)
                {
                    ForceRefreshFlag = false;
                    Snapshot = ActiveSnapshot;

                    // Send the size of the filtered memory to the display
                    ResultsEventArgs Args = new ResultsEventArgs();
                    Args.ElementCount = (Snapshot == null ? 0 : Snapshot.GetElementCount());
                    Args.MemorySize = (Snapshot == null ? 0 : Snapshot.GetMemorySize());
                    OnEventFlushCache(Args);
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
                    String Value = OSInterface.Process.Read(ScanType, Snapshot[Index].BaseAddress, out ReadSuccess).ToString();

                    IndexValueMap[Index] = Value;
                }
            }

            OnEventReadValues(new ResultsEventArgs());
        }

        public override void AddSelectionToTable(Int32 MinIndex, Int32 MaxIndex)
        {
            lock (ResultsLock)
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

                    AddressTable.GetInstance().AddAddressItem(ActiveSnapshot[Index].BaseAddress, ScanType, "No Description", Value: Value);

                    if (++Count >= MaxAdd)
                        break;
                }
            }
        }

        public override IntPtr GetAddressAtIndex(Int32 Index)
        {
            lock (ResultsLock)
            {
                if (Snapshot == null || Index >= (Int32)Snapshot.GetElementCount())
                    return IntPtr.Zero;

                return Snapshot[Index].BaseAddress;
            }
        }

        public override String GetValueAtIndex(Int32 Index)
        {
            lock (ResultsLock)
            {
                if (IndexValueMap.ContainsKey(Index))
                    return IndexValueMap[Index];

                return "-";
            }
        }

        public override String GetLabelAtIndex(Int32 Index)
        {
            lock (ResultsLock)
            {
                if (Snapshot == null || Index >= (Int32)Snapshot.GetElementCount())
                    return "-";

                dynamic Label = String.Empty;
                if (((dynamic)Snapshot)[Index].ElementLabel != null)
                    Label = ((dynamic)Snapshot)[Index].ElementLabel;

                return Label.ToString();
            }
        }

    } // End class

} // End namespace