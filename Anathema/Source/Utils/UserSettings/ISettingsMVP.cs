using Anathema.Source.OS;
using Anathema.Source.Utils.MVP;
using System;
using System.Reflection;

namespace Anathema.Source.Utils.Setting
{
    [Obfuscation(Exclude = true)]
    interface ISettingsView : IView
    {
        // Methods invoked by the presenter (upstream)

    }

    [Obfuscation(Exclude = true)]
    interface ISettingsModel : IModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
        [Obfuscation(Exclude = true)]
        void UpdateTypeSettings(Boolean None, Boolean Private, Boolean Mapped, Boolean Image);
        [Obfuscation(Exclude = true)]
        void UpdateRequiredProtectionSettings(Boolean RequiredWrite, Boolean RequiredExecute, Boolean RequiredCopyOnWrite);
        [Obfuscation(Exclude = true)]
        void UpdateIgnoredProtectionSettings(Boolean ExcludedWrite, Boolean ExcludedExecute, Boolean ExcludedCopyOnWrite);

        [Obfuscation(Exclude = true)]
        void UpdateAlignmentSettings(Int32 Alignment);
        [Obfuscation(Exclude = true)]
        void UpdateIsUserMode(Boolean IsUserMode);
        [Obfuscation(Exclude = true)]
        void UpdateStartAddress(UInt64 StartAddress);
        [Obfuscation(Exclude = true)]
        void UpdateEndAddress(UInt64 EndAddress);

        [Obfuscation(Exclude = true)]
        void UpdateFreezeInterval(Int32 FreezeInterval);
        [Obfuscation(Exclude = true)]
        void UpdateRescanInterval(Int32 RescanInterval);
        [Obfuscation(Exclude = true)]
        void UpdateResultReadInterval(Int32 ResultReadInterval);
        [Obfuscation(Exclude = true)]
        void UpdateTableReadInterval(Int32 TableReadInterval);
        [Obfuscation(Exclude = true)]
        void UpdateInputCorrelatorTimeOutInterval(Int32 InputCorrelatorTimeOutInterval);

        [Obfuscation(Exclude = true)]
        MemoryTypeEnum GetAllowedTypeSettings();
        [Obfuscation(Exclude = true)]
        MemoryProtectionEnum GetRequiredProtectionSettings();
        [Obfuscation(Exclude = true)]
        MemoryProtectionEnum GetExcludedProtectionSettings();
        [Obfuscation(Exclude = true)]
        Int32 GetAlignmentSettings();
        [Obfuscation(Exclude = true)]
        Boolean GetIsUserMode();
        [Obfuscation(Exclude = true)]
        UInt64 GetStartAddress();
        [Obfuscation(Exclude = true)]
        UInt64 GetEndAddress();

        [Obfuscation(Exclude = true)]
        Int32 GetFreezeInterval();
        [Obfuscation(Exclude = true)]
        Int32 GetRescanInterval();
        [Obfuscation(Exclude = true)]
        Int32 GetResultReadInterval();
        [Obfuscation(Exclude = true)]
        Int32 GetTableReadInterval();
        [Obfuscation(Exclude = true)]
        Int32 GetInputCorrelatorTimeOutInterval();
    }

    [Obfuscation(Exclude = true)]
    class SettingsPresenter : Presenter<ISettingsView, ISettingsModel>
    {
        [Obfuscation(Exclude = true)]
        private new ISettingsView View { get; set; }
        [Obfuscation(Exclude = true)]
        private new ISettingsModel Model { get; set; }

        public SettingsPresenter(ISettingsView View, ISettingsModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model


            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        [Obfuscation(Exclude = true)]
        public void UpdateTypeSettings(Boolean None, Boolean Private, Boolean Mapped, Boolean Image)
        {
            Model.UpdateTypeSettings(None, Private, Mapped, Image);
        }

        [Obfuscation(Exclude = true)]
        public void UpdateRequiredProtectionSettings(Boolean RequiredWrite, Boolean RequiredExecute, Boolean RequiredCopyOnWrite)
        {
            Model.UpdateRequiredProtectionSettings(RequiredWrite, RequiredExecute, RequiredCopyOnWrite);
        }

        [Obfuscation(Exclude = true)]
        public void UpdateExcludedProtectionSettings(Boolean ExcludedWrite, Boolean ExcludedExecute, Boolean ExcludedCopyOnWrite)
        {
            Model.UpdateIgnoredProtectionSettings(ExcludedWrite, ExcludedExecute, ExcludedCopyOnWrite);
        }

        [Obfuscation(Exclude = true)]
        public void UpdateAlignmentSettings(Int32 Alignment)
        {
            Model.UpdateAlignmentSettings(Alignment);
        }

        [Obfuscation(Exclude = true)]
        public void UpdateIsUserMode(Boolean IsUserMode)
        {
            Model.UpdateIsUserMode(IsUserMode);
        }

        [Obfuscation(Exclude = true)]
        public void UpdateStartAddress(UInt64 StartAddress)
        {
            Model.UpdateStartAddress(StartAddress);
        }

        [Obfuscation(Exclude = true)]
        public void UpdateEndAddress(UInt64 EndAddress)
        {
            Model.UpdateEndAddress(EndAddress);
        }

        [Obfuscation(Exclude = true)]
        public virtual void SetScanUserMode(Boolean IsUserMode)
        {
            Model.UpdateIsUserMode(IsUserMode);
        }

        [Obfuscation(Exclude = true)]
        public void UpdateFreezeInterval(String FreezeInterval)
        {
            Model.UpdateFreezeInterval(Int32.Parse(FreezeInterval));
        }

        [Obfuscation(Exclude = true)]
        public void UpdateRescanInterval(String RescanInterval)
        {
            Model.UpdateRescanInterval(Int32.Parse(RescanInterval));
        }

        [Obfuscation(Exclude = true)]
        public void UpdateResultReadInterval(String ResultReadInterval)
        {
            Model.UpdateResultReadInterval(Int32.Parse(ResultReadInterval));
        }

        [Obfuscation(Exclude = true)]
        public void UpdateTableReadInterval(String TableReadInterval)
        {
            Model.UpdateTableReadInterval(Int32.Parse(TableReadInterval));
        }

        [Obfuscation(Exclude = true)]
        public void UpdateInputCorrelatorTimeOutInterval(String InputCorrelatorTimeOutInterval)
        {
            Model.UpdateInputCorrelatorTimeOutInterval(Int32.Parse(InputCorrelatorTimeOutInterval));
        }

        [Obfuscation(Exclude = true)]
        public MemoryTypeEnum GetAllowedTypeSettings()
        {
            return Model.GetAllowedTypeSettings();
        }

        [Obfuscation(Exclude = true)]
        public MemoryProtectionEnum GetRequiredProtectionSettings()
        {
            return Model.GetRequiredProtectionSettings();
        }

        [Obfuscation(Exclude = true)]
        public MemoryProtectionEnum GetExcludedProtectionSettings()
        {
            return Model.GetExcludedProtectionSettings();
        }

        [Obfuscation(Exclude = true)]
        public Int32 GetAlignmentSettings()
        {
            return Model.GetAlignmentSettings();
        }

        [Obfuscation(Exclude = true)]
        public Boolean GetIsUserMode()
        {
            return Model.GetIsUserMode();
        }

        [Obfuscation(Exclude = true)]
        public UInt64 GetStartAddress()
        {
            return Model.GetStartAddress();
        }

        [Obfuscation(Exclude = true)]
        public UInt64 GetEndAddress()
        {
            return Model.GetEndAddress();
        }

        [Obfuscation(Exclude = true)]
        public Int32 GetFreezeInterval()
        {
            return Model.GetFreezeInterval();
        }

        [Obfuscation(Exclude = true)]
        public Int32 GetRescanInterval()
        {
            return Model.GetRescanInterval();
        }

        [Obfuscation(Exclude = true)]
        public Int32 GetResultReadInterval()
        {
            return Model.GetResultReadInterval();
        }

        [Obfuscation(Exclude = true)]
        public Int32 GetTableReadInterval()
        {
            return Model.GetTableReadInterval();
        }

        [Obfuscation(Exclude = true)]
        public Int32 GetInputCorrelatorTimeOutInterval()
        {
            return Model.GetInputCorrelatorTimeOutInterval();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion

    } // End class

} // End namespace