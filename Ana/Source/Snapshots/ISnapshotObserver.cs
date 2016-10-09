namespace Ana.Source.Snapshots
{
    /// <summary>
    /// Interface for a class which listens for changes in the active snapshot
    /// </summary>
    interface ISnapshotObserver
    {
        /// <summary>
        /// Recieves an update of the active snapshot
        /// </summary>
        /// <param name="process">The active snapshot</param>
        void Update(Snapshot snapshot);
    }
    //// End interface
}
//// End namespace