﻿namespace Ana.Source.Snapshots
{
    using Docking;
    using Main;
    using System;
    using System.Threading;

    /// <summary>
    /// View model for the Snapshot Manager
    /// </summary>
    internal class SnapshotManagerViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(SnapshotManagerViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="SnapshotManagerViewModel" /> class
        /// </summary>
        private static Lazy<SnapshotManagerViewModel> snapshotManagerViewModelInstance = new Lazy<SnapshotManagerViewModel>(
                () => { return new SnapshotManagerViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Prevents a default instance of the <see cref="SnapshotManagerViewModel" /> class from being created
        /// </summary>
        private SnapshotManagerViewModel() : base("Snapshot Manager")
        {
            this.ContentId = ToolContentId;
            this.IsVisible = true;

            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="SnapshotManagerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static SnapshotManagerViewModel GetInstance()
        {
            return snapshotManagerViewModelInstance.Value;
        }
    }
    //// End class
}
//// End namespace