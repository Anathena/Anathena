using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class Settings
    {
        public UInt32[] StateSettings = new UInt32[Enum.GetNames(typeof(MemoryStateFlags)).Length];
        public UInt32[] TypeSettings = new UInt32[Enum.GetNames(typeof(MemoryTypeFlags)).Length];
        public UInt32[] ProtectionSettings = new UInt32[Enum.GetNames(typeof(MemoryProtectionFlags)).Length];

        public Settings()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            Array EnumValues;

            EnumValues = Enum.GetValues(typeof(MemoryStateFlags));
            StateSettings[Array.IndexOf(EnumValues, MemoryStateFlags.Commit)] = 1;
            StateSettings[Array.IndexOf(EnumValues, MemoryStateFlags.Reserve)] = 0;
            StateSettings[Array.IndexOf(EnumValues, MemoryStateFlags.Free)] = 0;

            EnumValues = Enum.GetValues(typeof(MemoryTypeFlags));
            TypeSettings[Array.IndexOf(EnumValues, MemoryTypeFlags.Private)] = 1;
            TypeSettings[Array.IndexOf(EnumValues, MemoryTypeFlags.Mapped)] = 0;
            TypeSettings[Array.IndexOf(EnumValues, MemoryTypeFlags.Image)] = 1;

            EnumValues = Enum.GetValues(typeof(MemoryProtectionFlags));
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.NoAccess)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ReadOnly)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ReadWrite)] = 1;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.WriteCopy)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.Execute)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ExecuteRead)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ExecuteReadWrite)] = 1;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ExecuteWriteCopy)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.Guard)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.NoCache)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.WriteCombine)] = 0;
        }
    }
}
