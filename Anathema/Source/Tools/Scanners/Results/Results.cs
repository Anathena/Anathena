using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class Results : IResultsModel, IProcessObserver
    {
        private static Results ResultsInstance;
        private MemorySharp MemoryEditor;

        private const Int32 DisplayCount = 1000;
        private Type ScanType;

        private Results()
        {
            InitializeObserver();
            UpdateScanType(typeof(Int32));
            BeginScan();
        }

        public static Results GetInstance()
        {
            if (ResultsInstance == null)
                ResultsInstance = new Results();
            return ResultsInstance;
        }

        ~Results()
        {
            EndScan();
        }

        public void InitializeObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public override void UpdateScanType(Type ScanType)
        {
            this.ScanType = ScanType;
        }

        public override Type GetScanType()
        {
            return ScanType;
        }

        public override void BeginScan()
        {
            base.BeginScan();
        }

        protected override void UpdateScan()
        {
            if (!SnapshotManager.GetInstance().HasActiveSnapshot())
                return;

            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            // Addresses to read
            List<IntPtr> AccessedAddresses = new List<IntPtr>();

            // Labels to be passed to the GUI
            List<String> Addresses = new List<String>();
            List<String> Values = new List<String>();
            List<String> Labels = new List<String>();

            // Gather addresses to display
            foreach (RemoteRegion MemoryRegion in ActiveSnapshot)
            {
                for (UInt64 Address = (UInt64)MemoryRegion.BaseAddress; Address < (UInt64)MemoryRegion.EndAddress; Address++)
                {
                    AccessedAddresses.Add((IntPtr)Address);

                    Addresses.Add(Conversions.ToAddress(Address.ToString()));
                    if (Addresses.Count >= DisplayCount)
                        break;
                }
                if (Addresses.Count >= DisplayCount)
                    break;
            }

            // Gather values to display
            foreach (IntPtr Address in AccessedAddresses)
            {
                Boolean ReadSuccess = false;

                dynamic Value = "?";
                var @switch = new Dictionary<Type, Action> {
                    { typeof(Byte), () => Value = MemoryEditor.Read<Byte>(Address, out ReadSuccess, false) },
                    { typeof(SByte), () => Value = MemoryEditor.Read<SByte>(Address, out ReadSuccess, false) },
                    { typeof(Int16), () => Value = MemoryEditor.Read<Int16>(Address, out ReadSuccess, false) },
                    { typeof(Int32), () => Value = MemoryEditor.Read<Int32>(Address, out ReadSuccess, false) },
                    { typeof(Int64), () => Value = MemoryEditor.Read<Int64>(Address, out ReadSuccess, false) },
                    { typeof(UInt16), () => Value = MemoryEditor.Read<UInt16>(Address, out ReadSuccess, false) },
                    { typeof(UInt32), () => Value = MemoryEditor.Read<UInt32>(Address, out ReadSuccess, false) },
                    { typeof(UInt64), () => Value = MemoryEditor.Read<UInt64>(Address, out ReadSuccess, false) },
                    { typeof(Single), () => Value = MemoryEditor.Read<Single>(Address, out ReadSuccess, false) },
                    { typeof(Double), () => Value = MemoryEditor.Read<Double>(Address, out ReadSuccess, false) },
                };

                if (@switch.ContainsKey(ScanType))
                    @switch[ScanType]();

                if (ReadSuccess)
                    Values.Add(Value.ToString());
                else
                    Values.Add("-");
            }

            // Gather labels to display
            if (ActiveSnapshot.GetType() != typeof(Snapshot))
            {
                dynamic LabeledSnapshot = ActiveSnapshot;
                foreach (dynamic Region in LabeledSnapshot)
                {
                    foreach (dynamic Element in Region)
                    {
                        Labels.Add(Element.MemoryLabel.ToString());

                        if (Labels.Count >= DisplayCount)
                            break;
                    }

                    if (Labels.Count >= DisplayCount)
                        break;
                }
            }

            // Send the labels to the display
            ResultsEventArgs Args = new ResultsEventArgs();
            Args.Addresses = Addresses;
            Args.Values = Values;
            Args.Labels = Labels;
            OnEventUpdateDisplay(Args);

            // Send the size of the filtered memory to the display
            Args.MemorySize = ActiveSnapshot.GetMemorySize();
            OnEventUpdateMemorySize(Args);
        }
    }
}
