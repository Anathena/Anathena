namespace AnaTests
{
    using Ana.Source.Snapshots;
    using Ana.Source.UserSettings;
    using Ana.Source.Utils.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class SnapshotTests
    {
        [TestMethod]
        public void TestRegionChunking()
        {
            Random random = new Random();
            List<SnapshotRegion> groundTruth = new List<SnapshotRegion>();
            List<SnapshotRegion> chunkedRegions = new List<SnapshotRegion>();
            SettingsViewModel.GetInstance().Alignment = 1;

            UInt64 baseAddress = 0;
            for (Int32 i = 0; i < 10000; i++)
            {
                baseAddress += (UInt64)random.Next(1, 1024);
                Int32 regionSize = random.Next(1, 1024);
                Int32 chunkSize = random.Next(32, 128);

                SnapshotRegion region = new SnapshotRegion(baseAddress.ToIntPtr(), regionSize);
                IEnumerable<SnapshotRegion> regions = region.ChunkNormalizedRegion(chunkSize).Select(x => new SnapshotRegion(x)).OrderBy(x => random.Next());

                groundTruth.Add(region);
                chunkedRegions.AddRange(regions);
                baseAddress += (UInt64)regionSize;
            }

            Int32 originalSize = groundTruth.Sum(x => x.RegionSize);
            Int32 newSize = chunkedRegions.Sum(x => x.RegionSize);

            Assert.AreEqual(originalSize, newSize);
        }

        [TestMethod]
        public void TestSnapshotMerging()
        {
            Random random = new Random();
            List<SnapshotRegion> groundTruth = new List<SnapshotRegion>();
            List<SnapshotRegion> chunkedRegions = new List<SnapshotRegion>();
            SettingsViewModel.GetInstance().Alignment = 1;

            UInt64 baseAddress = 0;
            for (Int32 i = 0; i < 10000; i++)
            {
                baseAddress += (UInt64)random.Next(1, 1024);
                Int32 regionSize = random.Next(1, 1024);
                Int32 chunkSize = random.Next(32, 2048);

                SnapshotRegion region = new SnapshotRegion(baseAddress.ToIntPtr(), regionSize);
                IEnumerable<SnapshotRegion> regions = region.ChunkNormalizedRegion(chunkSize).Select(x => new SnapshotRegion(x)).OrderBy(x => random.Next());

                groundTruth.Add(region);
                chunkedRegions.AddRange(regions);

                baseAddress += (UInt64)regionSize;
            }

            Snapshot snapshot = new Snapshot(chunkedRegions);
            List<SnapshotRegion> reconstructedRegions = snapshot.GetSnapshotRegions().ToList();

            Assert.AreEqual(groundTruth.Count, reconstructedRegions.Count);

            Int32 originalSize = groundTruth.Sum(x => x.RegionSize);
            Int32 newSize = reconstructedRegions.Sum(x => x.RegionSize);

            Assert.AreEqual(originalSize, newSize);
        }
    }
    //// End class
}
//// End namespace