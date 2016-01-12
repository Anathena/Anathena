using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    interface ISettingsView : IView
    {
        // Methods invoked by the presenter (upstream)

    }

    interface ISettingsModel : IModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
        void UpdateTypeSettings(Boolean None, Boolean Private, Boolean Mapped, Boolean Image);
        void UpdateRequiredProtectionSettings(Boolean NoAccess, Boolean ReadOnly, Boolean ReadWrite, Boolean WriteCopy, Boolean Execute,
           Boolean ExecuteRead, Boolean ExecuteReadWrite, Boolean ExecuteWriteCopy, Boolean Guard, Boolean NoCache, Boolean WriteCombine);
        void UpdateIgnoredProtectionSettings(Boolean NoAccess, Boolean ReadOnly, Boolean ReadWrite, Boolean WriteCopy, Boolean Execute,
           Boolean ExecuteRead, Boolean ExecuteReadWrite, Boolean ExecuteWriteCopy, Boolean Guard, Boolean NoCache, Boolean WriteCombine);

        void UpdateFreezeInterval(Int32 FreezeInterval);
        void UpdateRescanInterval(Int32 RescanInterval);
        void UpdateResultReadInterval(Int32 ResultReadInterval);
        void UpdateTableReadInterval(Int32 TableReadInterval);
        
        Boolean[] GetTypeSettings();
        MemoryProtectionFlags GetRequiredProtectionSettings();
        MemoryProtectionFlags GetIgnoredProtectionSettings();

        Int32 GetFreezeInterval();
        Int32 GetRescanInterval();
        Int32 GetResultReadInterval();
        Int32 GetTableReadInterval();
    }

    class SettingsPresenter : Presenter<ISettingsView, ISettingsModel>
    {
        protected new ISettingsView View { get; set; }
        protected new ISettingsModel Model { get; set; }

        public SettingsPresenter(ISettingsView View, ISettingsModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
        }

        #region Method definitions called by the view (downstream)

        public void UpdateTypeSettings(Boolean None, Boolean Private, Boolean Mapped, Boolean Image)
        {
            Model.UpdateTypeSettings(None, Private, Mapped, Image);
        }

        public void UpdateRequiredProtectionSettings(Boolean NoAccess, Boolean ReadOnly, Boolean ReadWrite, Boolean WriteCopy, Boolean Execute,
            Boolean ExecuteRead, Boolean ExecuteReadWrite, Boolean ExecuteWriteCopy, Boolean Guard, Boolean NoCache, Boolean WriteCombine)
        {
            Model.UpdateRequiredProtectionSettings(NoAccess, ReadOnly, ReadWrite, WriteCopy, Execute, ExecuteRead, ExecuteReadWrite, ExecuteWriteCopy, Guard, NoCache, WriteCombine);
        }

        public void UpdateIgnoredProtectionSettings(Boolean NoAccess, Boolean ReadOnly, Boolean ReadWrite, Boolean WriteCopy, Boolean Execute,
            Boolean ExecuteRead, Boolean ExecuteReadWrite, Boolean ExecuteWriteCopy, Boolean Guard, Boolean NoCache, Boolean WriteCombine)
        {
            Model.UpdateIgnoredProtectionSettings(NoAccess, ReadOnly, ReadWrite, WriteCopy, Execute, ExecuteRead, ExecuteReadWrite, ExecuteWriteCopy, Guard, NoCache, WriteCombine);
        }

        public void UpdateFreezeInterval(String FreezeInterval)
        {
            Model.UpdateFreezeInterval(Int32.Parse(FreezeInterval));
        }

        public void UpdateRescanInterval(String RescanInterval)
        {
            Model.UpdateRescanInterval(Int32.Parse(RescanInterval));
        }

        public void UpdateResultReadInterval(String ResultReadInterval)
        {
            Model.UpdateResultReadInterval(Int32.Parse(ResultReadInterval));
        }

        public void UpdateTableReadInterval(String TableReadInterval)
        {
            Model.UpdateTableReadInterval(Int32.Parse(TableReadInterval));
        }

        public Boolean[] GetTypeSettings()
        {
            return Model.GetTypeSettings();
        }

        public Boolean[] GetRequiredProtectionSettings()
        {
            return ProtectionFlagsToBooleanArray(Model.GetRequiredProtectionSettings());
        }

        public Boolean[] GetIgnoredProtectionSettings()
        {
            return ProtectionFlagsToBooleanArray(Model.GetIgnoredProtectionSettings());
        }

        private Boolean[] ProtectionFlagsToBooleanArray(MemoryProtectionFlags ProtectionFlags)
        {
            Array ProtectionEnumValues = Enum.GetValues(typeof(MemoryProtectionFlags));
            Boolean[] ProtectionSettings = new Boolean[ProtectionEnumValues.Length];

            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoAccess)] = (ProtectionFlags & MemoryProtectionFlags.NoAccess) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadOnly)] = (ProtectionFlags & MemoryProtectionFlags.ReadOnly) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadWrite)] = (ProtectionFlags & MemoryProtectionFlags.ReadWrite) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCopy)] = (ProtectionFlags & MemoryProtectionFlags.WriteCopy) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Execute)] = (ProtectionFlags & MemoryProtectionFlags.Execute) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteRead)] = (ProtectionFlags & MemoryProtectionFlags.ExecuteRead) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteReadWrite)] = (ProtectionFlags & MemoryProtectionFlags.ExecuteReadWrite) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteWriteCopy)] = (ProtectionFlags & MemoryProtectionFlags.ExecuteWriteCopy) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Guard)] = (ProtectionFlags & MemoryProtectionFlags.Guard) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoCache)] = (ProtectionFlags & MemoryProtectionFlags.NoCache) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCombine)] = (ProtectionFlags & MemoryProtectionFlags.WriteCombine) != 0 ? true : false;

            return ProtectionSettings;
        }

        public String GetFreezeInterval()
        {
            return Model.GetFreezeInterval().ToString();
        }

        public String GetRescanInterval()
        {
            return Model.GetRescanInterval().ToString();
        }

        public String GetResultReadInterval()
        {
            return Model.GetResultReadInterval().ToString();
        }

        public String GetTableReadInterval()
        {
            return Model.GetTableReadInterval().ToString();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion
    }
}