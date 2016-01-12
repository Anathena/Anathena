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
        void UpdateRequiredStateSettings(Boolean Commit, Boolean Reserve, Boolean Free);
        void UpdateRequiredTypeSettings(Boolean Private, Boolean Mapped, Boolean Image);
        void UpdateRequiredProtectionSettings(Boolean NoAccess, Boolean ReadOnly, Boolean ReadWrite, Boolean WriteCopy, Boolean Execute,
           Boolean ExecuteRead, Boolean ExecuteReadWrite, Boolean ExecuteWriteCopy, Boolean Guard, Boolean NoCache, Boolean WriteCombine);
        
        void UpdateFreezeInterval(Int32 FreezeInterval);
        void UpdateRescanInterval(Int32 RescanInterval);
        void UpdateResultReadInterval(Int32 ResultReadInterval);
        void UpdateTableReadInterval(Int32 TableReadInterval);


        MemoryStateFlags GetRequiredStateSettings();
        MemoryTypeFlags GetRequiredTypeSettings();
        MemoryProtectionFlags GetRequiredProtectionSettings();

        MemoryStateFlags GetIgnoredStateSettings();
        MemoryTypeFlags GetIgnoredTypeSettings();
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

        public void UpdateStateSettings( Boolean Commit, Boolean Reserve, Boolean Free)
        {
            Model.UpdateRequiredStateSettings(Commit, Reserve, Free);
        }

        public void UpdateTypeSettings(Boolean Private, Boolean Mapped, Boolean Image)
        {
            Model.UpdateRequiredTypeSettings(Private, Mapped, Image);
        }

        public void UpdateProtectionSettings(Boolean NoAccess, Boolean ReadOnly, Boolean ReadWrite, Boolean WriteCopy, Boolean Execute,
            Boolean ExecuteRead, Boolean ExecuteReadWrite, Boolean ExecuteWriteCopy, Boolean Guard, Boolean NoCache, Boolean WriteCombine)
        {
            Model.UpdateRequiredProtectionSettings(NoAccess, ReadOnly, ReadWrite, WriteCopy, Execute, ExecuteRead, ExecuteReadWrite, ExecuteWriteCopy, Guard, NoCache, WriteCombine);
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

        public Boolean[] GetStateSettings()
        {
            MemoryStateFlags RequiredStateFlags = Model.GetRequiredStateSettings();
            Array StateEnumValues = Enum.GetValues(typeof(MemoryStateFlags));
            Boolean[] StateSettings = new Boolean[StateEnumValues.Length];

            StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Commit)] = (RequiredStateFlags & MemoryStateFlags.Commit) != 0 ? true : false;
            StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Free)] = (RequiredStateFlags & MemoryStateFlags.Free) != 0 ? true : false;
            StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Reserve)] = (RequiredStateFlags & MemoryStateFlags.Reserve) != 0 ? true : false;

            return StateSettings;
        }

        public Boolean[] GetTypeSettings()
        {
            MemoryTypeFlags RequiredTypeFlags = Model.GetRequiredTypeSettings();
            Array TypeEnumValues = Enum.GetValues(typeof(MemoryStateFlags));
            Boolean[] TypeSettings = new Boolean[TypeEnumValues.Length];

            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.None)] = (RequiredTypeFlags & MemoryTypeFlags.None) != 0 ? true : false;
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Private)] = (RequiredTypeFlags & MemoryTypeFlags.Private) != 0 ? true : false;
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Image)] = (RequiredTypeFlags & MemoryTypeFlags.Image) != 0 ? true : false;
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Mapped)] = (RequiredTypeFlags & MemoryTypeFlags.Mapped) != 0 ? true : false;

            return TypeSettings;
        }

        public Boolean[] GetProtectionSettings()
        {
            MemoryProtectionFlags RequiredProtectionFlags = Model.GetRequiredProtectionSettings();
            Array ProtectionEnumValues = Enum.GetValues(typeof(MemoryProtectionFlags));
            Boolean[] ProtectionSettings = new Boolean[ProtectionEnumValues.Length];

            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoAccess)] = (RequiredProtectionFlags & MemoryProtectionFlags.NoAccess) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadOnly)] = (RequiredProtectionFlags & MemoryProtectionFlags.ReadOnly) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadWrite)] = (RequiredProtectionFlags & MemoryProtectionFlags.ReadWrite) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCopy)] = (RequiredProtectionFlags & MemoryProtectionFlags.WriteCopy) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Execute)] = (RequiredProtectionFlags & MemoryProtectionFlags.Execute) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteRead)] = (RequiredProtectionFlags & MemoryProtectionFlags.ExecuteRead) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteReadWrite)] = (RequiredProtectionFlags & MemoryProtectionFlags.ExecuteReadWrite) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteWriteCopy)] = (RequiredProtectionFlags & MemoryProtectionFlags.ExecuteWriteCopy) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Guard)] = (RequiredProtectionFlags & MemoryProtectionFlags.Guard) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoCache)] = (RequiredProtectionFlags & MemoryProtectionFlags.NoCache) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCombine)] = (RequiredProtectionFlags & MemoryProtectionFlags.WriteCombine) != 0 ? true : false;

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