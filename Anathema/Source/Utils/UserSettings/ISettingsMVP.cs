using Anathema.Source.Engine.OperatingSystems;
using Anathema.Source.Utils.MVP;
using System;
using System.Reflection;

namespace Anathema.Source.Utils.Setting
{
    [Obfuscation(ApplyToMembers = true)]
    [Obfuscation(Exclude = true)]
    interface ISettingsView : IView
    {
        // Methods invoked by the presenter (upstream)

    }

    [Obfuscation(ApplyToMembers = true)]
    [Obfuscation(Exclude = true)]
    interface ISettingsModel : IModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
        void UpdateTypeSettings(Boolean None, Boolean Private, Boolean Mapped, Boolean Image);
        void UpdateRequiredProtectionSettings(Boolean RequiredWrite, Boolean RequiredExecute, Boolean RequiredCopyOnWrite);
        void UpdateIgnoredProtectionSettings(Boolean ExcludedWrite, Boolean ExcludedExecute, Boolean ExcludedCopyOnWrite);

        void UpdateAlignmentSettings(Int32 Alignment);
        void UpdateIsUserMode(Boolean IsUserMode);
        void UpdateStartAddress(UInt64 StartAddress);
        void UpdateEndAddress(UInt64 EndAddress);

        void UpdateFreezeInterval(Int32 FreezeInterval);
        void UpdateRescanInterval(Int32 RescanInterval);
        void UpdateResultReadInterval(Int32 ResultReadInterval);
        void UpdateTableReadInterval(Int32 TableReadInterval);
        void UpdateInputCorrelatorTimeOutInterval(Int32 InputCorrelatorTimeOutInterval);

        MemoryTypeEnum GetAllowedTypeSettings();
        MemoryProtectionEnum GetRequiredProtectionSettings();
        MemoryProtectionEnum GetExcludedProtectionSettings();
        Int32 GetAlignmentSettings();
        Boolean GetIsUserMode();
        UInt64 GetStartAddress();
        UInt64 GetEndAddress();

        Int32 GetFreezeInterval();
        Int32 GetRescanInterval();
        Int32 GetResultReadInterval();
        Int32 GetTableReadInterval();
        Int32 GetInputCorrelatorTimeOutInterval();
    }

    [Obfuscation(ApplyToMembers = true)]
    [Obfuscation(Exclude = true)]
    class SettingsPresenter : Presenter<ISettingsView, ISettingsModel>
    {
        private new ISettingsView View { get; set; }
        private new ISettingsModel Model { get; set; }

        public SettingsPresenter(ISettingsView View, ISettingsModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model


            Model.OnGUIOpen();
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

        public void UpdateExcludedProtectionSettings(Boolean ExcludedWrite, Boolean ExcludedExecute, Boolean ExcludedCopyOnWrite)
        {
            Model.UpdateIgnoredProtectionSettings(ExcludedWrite, ExcludedExecute, ExcludedCopyOnWrite);
        }

        public void UpdateAlignmentSettings(Int32 Alignment)
        {
            Model.UpdateAlignmentSettings(Alignment);
        }

        public void UpdateIsUserMode(Boolean IsUserMode)
        {
            Model.UpdateIsUserMode(IsUserMode);
        }

        public void UpdateStartAddress(UInt64 StartAddress)
        {
            Model.UpdateStartAddress(StartAddress);
        }

        public void UpdateEndAddress(UInt64 EndAddress)
        {
            Model.UpdateEndAddress(EndAddress);
        }

        public virtual void SetScanUserMode(Boolean IsUserMode)
        {
            Model.UpdateIsUserMode(IsUserMode);
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

        public MemoryTypeEnum GetAllowedTypeSettings()
        {
            return Model.GetAllowedTypeSettings();
        }

        public MemoryProtectionEnum GetRequiredProtectionSettings()
        {
            return Model.GetRequiredProtectionSettings();
        }

        public MemoryProtectionEnum GetExcludedProtectionSettings()
        {
            return Model.GetExcludedProtectionSettings();
        }

        public Int32 GetAlignmentSettings()
        {
            return Model.GetAlignmentSettings();
        }

        public Boolean GetIsUserMode()
        {
            return Model.GetIsUserMode();
        }

        public UInt64 GetStartAddress()
        {
            return Model.GetStartAddress();
        }

        public UInt64 GetEndAddress()
        {
            return Model.GetEndAddress();
        }

        public Int32 GetFreezeInterval()
        {
            return Model.GetFreezeInterval();
        }

        public Int32 GetRescanInterval()
        {
            return Model.GetRescanInterval();
        }

        public Int32 GetResultReadInterval()
        {
            return Model.GetResultReadInterval();
        }

        public Int32 GetTableReadInterval()
        {
            return Model.GetTableReadInterval();
        }

        public Int32 GetInputCorrelatorTimeOutInterval()
        {
            return Model.GetInputCorrelatorTimeOutInterval();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion

    } // End class

} // End namespace