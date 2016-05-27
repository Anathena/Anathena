using Anathema.Source.Utils.Snapshots;

namespace Anathema.Source.Prefilter
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