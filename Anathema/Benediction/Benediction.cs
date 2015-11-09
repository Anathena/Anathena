using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Static class to control the main memory editor. Individual tools subscribe to this tool to recieve updates to
    /// changes in the target process.
    /// </summary>
    class Benediction : IBenedictionModel
    {
        private static Benediction BenedictionInstance; // Static self reference
        private MemorySharp MemoryEditor;               // Memory editor instance

        private IFilterModel MemoryFilter;         // Current memory filter
        private IMemoryLabeler MemoryLabeler;       // Current memory labeler
        private SnapshotManager SnapshotManager;    // Memory snapshot manager instance

        private Benediction()
        {
            SnapshotManager = SnapshotManager.GetSnapshotManagerInstance();
        }

        public static Benediction GetBenedictionInstance()
        {
            if (BenedictionInstance == null)
                BenedictionInstance = new Benediction();

            return BenedictionInstance;
        }

        public void UpdateProcess(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        /// <summary>
        /// Begin the filtering process with the specified filter
        /// </summary>
        /// <param name="MemoryFilter"></param>
        public void BeginFilter(IFilterModel MemoryFilter)
        {
            if (MemoryFilter == null)
                return;

            this.MemoryFilter = MemoryFilter;

            // Start scanning with the active memory snapshot
            MemoryFilter.BeginFilter();
        }

        public void EndFilter()
        {
            if (MemoryFilter == null)
                return;

            MemoryFilter.EndFilter();
            MemoryFilter = null;
        }

        public void AbortFilter()
        {
            if (MemoryFilter == null)
                return;

            MemoryFilter.AbortFilter();
        }

        /// <summary>
        /// Begin the labeling process with the specified labeler
        /// </summary>
        /// <param name="MemoryLabeler"></param>
        public void BeginLabeler(IMemoryLabeler MemoryLabeler)
        {
            if (MemoryLabeler == null)
                return;

            this.MemoryLabeler = MemoryLabeler;

            // Start labeling the active memory snapshot
            //MemoryLabeler.BeginLabeler(MemoryEditor, SnapshotManager.GetActiveSnapshot(MemoryEditor));
        }

        public void EndLabeler()
        {
            if (MemoryLabeler == null)
                return;

            MemoryLabeler.EndLabeler();
            MemoryLabeler = null;
        }

        public void AbortLabeler()
        {
            if (MemoryLabeler == null)
                return;

            MemoryLabeler.AbortLabeler();
        }
    }
}
