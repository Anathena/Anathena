namespace Ana.Source.Snapshots.Prefilters
{
    using System;

    /// <summary>
    /// Factory for obtaining a memory prefilter
    /// </summary>
    internal static class SnapshotPrefilterFactory
    {
        /// <summary>
        /// Gets a prefilter based on the provided class type
        /// </summary>
        /// <param name="prefilterClass">The class type of the prefilter</param>
        /// <returns>A prefilter of the specified type</returns>
        public static ISnapshotPrefilter GetSnapshotPrefilter(Type prefilterClass)
        {
            if (prefilterClass == typeof(ShallowPointerPrefilter))
            {
                return ShallowPointerPrefilter.GetInstance();
            }
            else if (prefilterClass == typeof(ChunkLinkedListPrefilter))
            {
                return ChunkLinkedListPrefilter.GetInstance();
            }

            throw new Exception(prefilterClass?.ToString() + " - Type is not a valid prefilter");
        }
    }
    //// End class
}
//// End namespace