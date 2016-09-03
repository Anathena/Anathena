using System;

namespace Anathena.Source.Snapshots.Prefilter
{
    static class SnapshotPrefilterFactory
    {
        public static ISnapshotPrefilter GetSnapshotPrefilter(Type PrefilterClass)
        {
            if (PrefilterClass == typeof(LinkedListSnapshotPrefilter))
                return LinkedListSnapshotPrefilter.GetInstance();
            else if (PrefilterClass == typeof(QueueSnapshotPrefilter))
                return QueueSnapshotPrefilter.GetInstance();
            else if (PrefilterClass == typeof(PointerPoolPrefilter))
                return PointerPoolPrefilter.GetInstance();
            else if (PrefilterClass == typeof(ShallowPointerPrefilter))
                return ShallowPointerPrefilter.GetInstance();

            throw new Exception(PrefilterClass?.ToString() + " - Type is not a valid prefilter");
        }

    } // End class

} // End namespace