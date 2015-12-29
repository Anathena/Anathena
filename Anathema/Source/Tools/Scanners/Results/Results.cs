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
        private MemorySharp MemoryEditor;
        private const Int32 DisplayCount = 1000;

        public Results()
        {
            InitializeObserver();
            BeginScan();
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
            foreach (RemoteRegion Region in ActiveSnapshot.GetMemoryRegions())
            {
                for (UInt64 Address = (UInt64)Region.BaseAddress; Address < (UInt64)Region.EndAddress; Address++)
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
                Boolean ReadSuccess;
                var Value = MemoryEditor.Read<Byte> (Address, out ReadSuccess, false);

                if (ReadSuccess)
                    Values.Add(Value.ToString());
                else
                    Values.Add("??");
            }

            // Gather labels to display
            if (ActiveSnapshot.GetType() != typeof(Snapshot))
            {
                dynamic LabeledSnapshot = ActiveSnapshot;
                foreach (var RegionLabels in LabeledSnapshot.GetMemoryLabels())
                {
                    foreach (var Lables in RegionLabels)
                    { 
                        Labels.Add(Lables.ToString());

                        if (Labels.Count >= DisplayCount)
                            break;
                    }

                    if (Labels.Count >= DisplayCount)
                        break;
                }
            }
            else
            {
                for (Int32 Index = 0; Index < DisplayCount; Index++)
                    Labels.Add("");
            }
            
            // Send the size of the filtered memory to the GUI
            ResultsEventArgs Args = new ResultsEventArgs();
            Args.Addresses = Addresses;
            Args.Values = Values;
            Args.Labels = Labels;
            OnEventUpdateDisplay(Args);
        }

        public override void EndScan()
        {
            base.EndScan();
        }
    }
}
