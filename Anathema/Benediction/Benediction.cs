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

    public delegate void BenedictionModelHandler<IBenedictionModel>(IBenedictionModel sender, BenedictionModelEventArgs e);

    public class BenedictionModelEventArgs : EventArgs
    {
        public MemorySharp MemoryEditor;
        public BenedictionModelEventArgs(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }
    }

    public interface IBenedictionModelObserver
    {
        void ProcessSelected(IBenedictionModel model, BenedictionModelEventArgs e);
    }

    public interface IBenedictionModel
    {
        void Attach(IBenedictionModelObserver BenedictionModelObserver);
        void UpdateProcess(MemorySharp MemoryEditor);
    }

    /*
    TODO:
    - Speedhack
    - Manual Scan
    - Batch read/write function (automatic API call minimization)
    - Multiprocess Scan
    - Plugin Support
    - File sharing
    */
    /// <summary>
    /// Singleton class to controls the main memory editor. Individual tools subscribe to this tool to recieve updates to
    /// changes in the target process.
    /// </summary>
    class Benediction : IBenedictionModel
    {
        private static Benediction BenedictionInstance; // Static reference to this class
        private MemorySharp MemoryEditor;               // Memory editor instance
        
        public event BenedictionModelHandler<Benediction> Changed;

        private IMemoryFilter MemoryFilter;         // Current memory filter
        private IMemoryLabeler MemoryLabeler;       // Current memory labeler
        private SnapshotManager SnapshotManager;    // Memory snapshot manager instance

        public Benediction()
        {
            SnapshotManager = new SnapshotManager();
        }

        /// <summary>
        /// Returns the instance of the singleton anathema object
        /// </summary>
        public static Benediction GetBenedictionInstance()
        {
            if (BenedictionInstance == null)
                BenedictionInstance = new Benediction();
            return BenedictionInstance;
        }

        public void Attach(IBenedictionModelObserver BenedictionModelObserver)
        {
            Changed += new BenedictionModelHandler<Benediction>(BenedictionModelObserver.ProcessSelected);
        }

        public void UpdateProcess(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
            Changed.Invoke(this, new BenedictionModelEventArgs(MemoryEditor));
        }

        /// <summary>
        /// Begin the filtering process with the specified filter
        /// </summary>
        /// <param name="MemoryFilter"></param>
        public void BeginFilter(IMemoryFilter MemoryFilter)
        {
            if (MemoryFilter == null)
                return;

            this.MemoryFilter = MemoryFilter;

            // Start scanning with the active memory snapshot
            MemoryFilter.BeginFilter(MemoryEditor, SnapshotManager.GetActiveSnapshot(MemoryEditor));
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
            MemoryLabeler.BeginLabeler(MemoryEditor, SnapshotManager.GetActiveSnapshot(MemoryEditor));
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
