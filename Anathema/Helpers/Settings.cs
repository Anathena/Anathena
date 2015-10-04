using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// THIS NEEDS UPDATING UPON REMOVAL OF OSINTERFACE.
    /// </summary>
    class Settings
    {
        public UInt32[] StateSettings = new UInt32[Enum.GetNames(typeof(OSInterface.MEMORY_STATE)).Length];
        public UInt32[] TypeSettings = new UInt32[Enum.GetNames(typeof(OSInterface.MEMORY_TYPE)).Length];
        public UInt32[] ProtectionSettings = new UInt32[Enum.GetNames(typeof(OSInterface.MEMORY_PROTECTION)).Length];

        public Settings()
        {
            // TODO saving/loading user settings
            SetDefaults();
        }

        private void SetDefaults()
        {
            Array EnumValues;

            EnumValues = Enum.GetValues(typeof(OSInterface.MEMORY_STATE));
            StateSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_STATE.COMMIT)] = 1;
            StateSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_STATE.RESERVE)] = 0;
            StateSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_STATE.FREE)] = 0;
            StateSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_STATE.RESET_UNDO)] = 0;

            EnumValues = Enum.GetValues(typeof(OSInterface.MEMORY_TYPE));
            TypeSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_TYPE.PRIVATE)] = 1;
            TypeSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_TYPE.MAPPED)] = 0;
            TypeSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_TYPE.IMAGE)] = 1;

            EnumValues = Enum.GetValues(typeof(OSInterface.MEMORY_PROTECTION));
            ProtectionSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_PROTECTION.NO_ACCESS)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_PROTECTION.READ_ONLY)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_PROTECTION.READ_WRITE)] = 1;
            ProtectionSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_PROTECTION.WRITE_COPY)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_PROTECTION.EXECUTE)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_PROTECTION.EXECUTE_READ)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_PROTECTION.EXECUTE_READ_WRITE)] = 1;
            ProtectionSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_PROTECTION.EXECUTE_WRITE_COPY)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_PROTECTION.GUARD)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_PROTECTION.NO_CACHE)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, OSInterface.MEMORY_PROTECTION.WRITE_COMBINE)] = 0;
        }
    }
}
