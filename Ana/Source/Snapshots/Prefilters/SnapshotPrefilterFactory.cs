using System;

namespace Ana.Source.Snapshots.Prefilter
{
    static class SnapshotPrefilterFactory
    {
        public static ISnapshotPrefilter GetSnapshotPrefilter(Type PrefilterClass)
        {
            if (PrefilterClass == typeof(ShallowPointerPrefilter))
                return ShallowPointerPrefilter.GetInstance();

            throw new Exception(PrefilterClass?.ToString() + " - Type is not a valid prefilter");
        }

    } // End class

} // End namespace