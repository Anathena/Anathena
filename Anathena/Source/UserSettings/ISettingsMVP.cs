using Ana.Source.Engine.OperatingSystems;
using Ana.Source.Utils.MVP;
using System;
using System.Reflection;

namespace Ana.Source.UserSettings
{
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    interface ISettingsView : IView
    {
        // Methods invoked by the presenter (upstream)

    }

    [Obfuscation(ApplyToMembers = true, Exclude = true)]
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

    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    class SettingsPresenter : Presenter<ISettingsView, ISettingsModel>
    {
        private new ISettingsView view { get; set; }
        private new ISettingsModel model { get; set; }

        public SettingsPresenter(ISettingsView view, ISettingsModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model


            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void UpdateTypeSettings(Boolean None, Boolean Private, Boolean Mapped, Boolean Image)
        {
            model.UpdateTypeSettings(None, Private, Mapped, Image);
        }

        public void UpdateRequiredProtectionSettings(Boolean RequiredWrite, Boolean RequiredExecute, Boolean RequiredCopyOnWrite)
        {
            model.UpdateRequiredProtectionSettings(RequiredWrite, RequiredExecute, RequiredCopyOnWrite);
        }

        public void UpdateExcludedProtectionSettings(Boolean ExcludedWrite, Boolean ExcludedExecute, Boolean ExcludedCopyOnWrite)
        {
            model.UpdateIgnoredProtectionSettings(ExcludedWrite, ExcludedExecute, ExcludedCopyOnWrite);
        }

        public void UpdateAlignmentSettings(Int32 Alignment)
        {
            model.UpdateAlignmentSettings(Alignment);
        }

        public void UpdateIsUserMode(Boolean IsUserMode)
        {
            model.UpdateIsUserMode(IsUserMode);
        }

        public void UpdateStartAddress(UInt64 StartAddress)
        {
            model.UpdateStartAddress(StartAddress);
        }

        public void UpdateEndAddress(UInt64 EndAddress)
        {
            model.UpdateEndAddress(EndAddress);
        }

        public virtual void SetScanUserMode(Boolean IsUserMode)
        {
            model.UpdateIsUserMode(IsUserMode);
        }

        public void UpdateFreezeInterval(String FreezeInterval)
        {
            model.UpdateFreezeInterval(Int32.Parse(FreezeInterval));
        }

        public void UpdateRescanInterval(String RescanInterval)
        {
            model.UpdateRescanInterval(Int32.Parse(RescanInterval));
        }

        public void UpdateResultReadInterval(String ResultReadInterval)
        {
            model.UpdateResultReadInterval(Int32.Parse(ResultReadInterval));
        }

        public void UpdateTableReadInterval(String TableReadInterval)
        {
            model.UpdateTableReadInterval(Int32.Parse(TableReadInterval));
        }

        public void UpdateInputCorrelatorTimeOutInterval(String InputCorrelatorTimeOutInterval)
        {
            model.UpdateInputCorrelatorTimeOutInterval(Int32.Parse(InputCorrelatorTimeOutInterval));
        }

        public MemoryTypeEnum GetAllowedTypeSettings()
        {
            return model.GetAllowedTypeSettings();
        }

        public MemoryProtectionEnum GetRequiredProtectionSettings()
        {
            return model.GetRequiredProtectionSettings();
        }

        public MemoryProtectionEnum GetExcludedProtectionSettings()
        {
            return model.GetExcludedProtectionSettings();
        }

        public Int32 GetAlignmentSettings()
        {
            return model.GetAlignmentSettings();
        }

        public Boolean GetIsUserMode()
        {
            return model.GetIsUserMode();
        }

        public UInt64 GetStartAddress()
        {
            return model.GetStartAddress();
        }

        public UInt64 GetEndAddress()
        {
            return model.GetEndAddress();
        }

        public Int32 GetFreezeInterval()
        {
            return model.GetFreezeInterval();
        }

        public Int32 GetRescanInterval()
        {
            return model.GetRescanInterval();
        }

        public Int32 GetResultReadInterval()
        {
            return model.GetResultReadInterval();
        }

        public Int32 GetTableReadInterval()
        {
            return model.GetTableReadInterval();
        }

        public Int32 GetInputCorrelatorTimeOutInterval()
        {
            return model.GetInputCorrelatorTimeOutInterval();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion

    } // End class

} // End namespace