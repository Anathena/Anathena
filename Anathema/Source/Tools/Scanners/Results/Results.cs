using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class Results : IResultsModel
    {
        private const Int32 DisplayCount = 1000;

        public Results()
        {
            BeginScan();
        }

        ~Results()
        {
            EndScan();
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

            List<RemoteRegion> AccessedRegions = new List<RemoteRegion>();

            List<String> Addresses = new List<String>();
            List<String> Values = new List<String>();
            List<String> Labels = new List<String>();

            // Gather addresses to display
            foreach (RemoteRegion Region in ActiveSnapshot.GetMemoryRegions())
            {
                AccessedRegions.Add(Region);

                for (UInt64 Address = (UInt64)Region.BaseAddress; Address < (UInt64)Region.EndAddress; Address++)
                {
                    Addresses.Add(Conversions.ToAddress(Address.ToString()));
                    if (AccessedRegions.Count >= DisplayCount)
                        break;
                }
                if (Addresses.Count >= DisplayCount)
                    break;
            }

            // Read and get an update on the regions that are being shown
            ActiveSnapshot.ReadSpecifiedRegions(AccessedRegions);
            
            List<Byte[]> MemoryValues = ActiveSnapshot.GetReadMemory();
            List<Int32> LabelMapping = ActiveSnapshot.GetLabelMapping();

            // Gather values to display
            foreach (RemoteRegion Region in AccessedRegions)
            {
                //Values.Add(Value.ToString());
                Values.Add("??");
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
