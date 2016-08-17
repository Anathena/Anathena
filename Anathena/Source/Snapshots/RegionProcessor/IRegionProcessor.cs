namespace Anathena.Source.Snapshots.RegionProcessor
{
    /// <summary>
    /// Interface that describes a class that processes a snapshot region
    /// </summary>
    interface IRegionProcessor
    {
        void ProcessRegion(SnapshotRegion Region);

    } // End interface

} // End namespace