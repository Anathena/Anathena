using System;

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
        void UpdateRequiredProtectionSettings(Boolean RequiredWrite, Boolean RequiredExecute, Boolean RequiredCopyOnWrite);
        void UpdateIgnoredProtectionSettings(Boolean ExcludedWrite, Boolean ExcludedExecute, Boolean ExcludedCopyOnWrite);

        void UpdateAlignmentSettings(Int32 Alignment);

        void UpdateFreezeInterval(Int32 FreezeInterval);
        void UpdateRescanInterval(Int32 RescanInterval);
        void UpdateResultReadInterval(Int32 ResultReadInterval);
        void UpdateTableReadInterval(Int32 TableReadInterval);
        void UpdateInputCorrelatorTimeOutInterval(Int32 InputCorrelatorTimeOutInterval);

        Boolean[] GetTypeSettings();
        MemoryProtectionEnum GetRequiredProtectionSettings();
        MemoryProtectionEnum GetExcludedProtectionSettings();
        Int32 GetAlignmentSettings();

        Int32 GetFreezeInterval();
        Int32 GetRescanInterval();
        Int32 GetResultReadInterval();
        Int32 GetTableReadInterval();
        Int32 GetInputCorrelatorTimeOutInterval();
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

        public void UpdateRequiredProtectionSettings(Boolean RequiredWrite, Boolean RequiredExecute, Boolean RequiredCopyOnWrite)
        {
            Model.UpdateRequiredProtectionSettings(RequiredWrite, RequiredExecute, RequiredCopyOnWrite);
        }

        public void UpdateIgnoredProtectionSettings(Boolean ExcludedWrite, Boolean ExcludedExecute, Boolean ExcludedCopyOnWrite)
        {
            Model.UpdateIgnoredProtectionSettings(ExcludedWrite, ExcludedExecute, ExcludedCopyOnWrite);
        }

        public void UpdateAlignmentSettings(Int32 Alignment)
        {
            Model.UpdateAlignmentSettings(Alignment);
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

        public void UpdateInputCorrelatorTimeOutInterval(String InputCorrelatorTimeOutInterval)
        {
            Model.UpdateInputCorrelatorTimeOutInterval(Int32.Parse(InputCorrelatorTimeOutInterval));
        }

        public Boolean[] GetTypeSettings()
        {
            return Model.GetTypeSettings();
        }

        public Boolean[] GetRequiredProtectionSettings()
        {
            return ProtectionFlagsToBooleanArray(Model.GetRequiredProtectionSettings());
        }

        public Boolean[] GetExcludedProtectionSettings()
        {
            return ProtectionFlagsToBooleanArray(Model.GetExcludedProtectionSettings());
        }

        private Boolean[] ProtectionFlagsToBooleanArray(MemoryProtectionEnum ProtectionFlags)
        {
            Array ProtectionEnumValues = Enum.GetValues(typeof(MemoryProtectionEnum));
            Boolean[] ProtectionSettings = new Boolean[ProtectionEnumValues.Length];

            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionEnum.Write)] = (ProtectionFlags & MemoryProtectionEnum.Write) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionEnum.Execute)] = (ProtectionFlags & MemoryProtectionEnum.Execute) != 0 ? true : false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionEnum.CopyOnWrite)] = (ProtectionFlags & MemoryProtectionEnum.CopyOnWrite) != 0 ? true : false;
            return ProtectionSettings;
        }

        public Int32 GetAlignmentSettings()
        {
            return Model.GetAlignmentSettings();
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

        public String GetInputCorrelatorTimeOutInterval()
        {
            return Model.GetInputCorrelatorTimeOutInterval().ToString();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion

    } // End class

} // End namespace