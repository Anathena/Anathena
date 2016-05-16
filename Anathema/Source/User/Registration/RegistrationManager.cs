using Microsoft.Win32;
using System;

namespace Anathema.User.Registration
{
    class RegistrationManager
    {
        // Singleton instance of Registration Manager
        private static Lazy<RegistrationManager> RegistrationManagerInstance = new Lazy<RegistrationManager>(() => { return new RegistrationManager(); });

        private const String RegistryKeyIsRegistered = "AnathemaRegistered";
        private const String RegistryKeyTrialStart = "AnathemaTrialStart";

        private TimeSpan TrialTimeout = TimeSpan.FromDays(14);

        internal class RegistryObject<T>
        {
            public T Data;
            public RegistryObject(T Data) { this.Data = Data; }
        }

        private RegistrationManager() { }
        
        public static RegistrationManager GetInstance()
        {
            return RegistrationManagerInstance.Value;
        }

        public Boolean Register()
        {
            RegistryObject<Boolean?> IsRegistered = new RegistryObject<Boolean?>(true);

            // Attempt to write the IsRegistered value to the registry
            return WriteRegistryKey(RegistryKeyIsRegistered, IsRegistered.Data.ToString());
        }

        public void Unregister()
        {
            WriteRegistryKey(RegistryKeyTrialStart, String.Empty);
            WriteRegistryKey(RegistryKeyIsRegistered, String.Empty);
        }

        public TimeSpan GetRemainingTime()
        {
            RegistryObject<DateTime?> TrialStart;
            DateTime Result;

            // Parse the trial start time from registry
            Boolean Success = DateTime.TryParse((String)ReadRegistryKey(RegistryKeyTrialStart), out Result);
            if (Success)
                TrialStart = new RegistryObject<DateTime?>(Result);
            else
                TrialStart = new RegistryObject<DateTime?>(null);

            // Create trial registry key if it is not already there
            if (TrialStart.Data == null)
                return TrialTimeout;

            return DateTime.Now - (TrialStart.Data.Value + TrialTimeout);
        }

        public Boolean IsRegistered()
        {
            RegistryObject<Boolean?> IsRegistered;
            Boolean Result;

            // Parse the registered flag from registry
            Boolean Success = Boolean.TryParse((String)ReadRegistryKey(RegistryKeyIsRegistered), out Result);
            if (Success)
                IsRegistered = new RegistryObject<Boolean?>(Result);
            else
                IsRegistered = new RegistryObject<Boolean?>(null);


            // Set registered to false if not registered
            if (IsRegistered.Data == null)
            {
                IsRegistered = new RegistryObject<Boolean?>(false);
                if (WriteRegistryKey(RegistryKeyIsRegistered, IsRegistered.Data.ToString()))
                    return false;
            }

            if (IsRegistered.Data == null)
                return false;

            // Access registration file and determine if it is valid
            return IsRegistered.Data.Value;
        }

        public Boolean IsTrialMode()
        {
            if (IsRegistered())
                return false;

            RegistryObject<DateTime?> TrialStart;
            DateTime Result;

            // Parse the trial start time from registry
            Boolean Success = DateTime.TryParse((String)ReadRegistryKey(RegistryKeyTrialStart), out Result);
            if (Success)
                TrialStart = new RegistryObject<DateTime?>(Result);
            else
                TrialStart = new RegistryObject<DateTime?>(null);

            // Create trial registry key if it is not already there
            if (TrialStart.Data == null)
            {
                TrialStart = new RegistryObject<DateTime?>(DateTime.Now);
                if (WriteRegistryKey(RegistryKeyTrialStart, TrialStart.Data.ToString()))
                    return true;
            }

            // Should not happen, but just in case
            if (TrialStart.Data == null)
                return false;

            // Prevent clock tricks (setting time to before trial start)
            if (DateTime.Now < TrialStart.Data)
                return false;

            // Determine if trial has ended
            if (DateTime.Now - TrialStart.Data > TrialTimeout)
                return false;
            else
                return true;
        }

        public Boolean WriteRegistryKey(String Key, Object Value)
        {
            RegistryKey RegistryKey;

            try
            {
                RegistryKey = Registry.CurrentUser.CreateSubKey(Key);
                RegistryKey.SetValue(Key, Value);
                RegistryKey.Close();
                return true;
            }
            catch { }

            return false;
        }

        public Object ReadRegistryKey(String Key)
        {
            Object KeyValue = null;
            RegistryKey RegistryKey;

            try
            {
                RegistryKey = Registry.CurrentUser.OpenSubKey(Key);
                KeyValue = RegistryKey.GetValue(Key);
                RegistryKey.Close();
            }
            catch { }

            return KeyValue;
        }

    } // End class

} // End namespace