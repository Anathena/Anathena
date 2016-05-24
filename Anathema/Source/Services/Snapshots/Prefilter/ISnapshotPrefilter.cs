namespace Anathema.Services.Snapshots.Prefilter
{
    /// <summary>
    /// Interface defining methods that a snapshot prefilter must implement
    /// </summary>
    interface ISnapshotPrefilter
    {
        void BeginPrefilter();
        Snapshot GetPrefilteredSnapshot();

    } // End interface

} // End namespace