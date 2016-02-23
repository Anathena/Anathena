using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class RegistrationManager
    {
        private const String AnathemaTrialRegistryKey = "AnathemaTrial";

        private static RegistrationManager RegistrationManagerInstance;
        private Boolean TrialActive;

        private RegistrationManager()
        {
            TrialActive = false;
        }

        public static RegistrationManager GetInstance()
        {
            if (RegistrationManagerInstance == null)
                RegistrationManagerInstance = new RegistrationManager();
            return RegistrationManagerInstance;
        }

        public void SetTrialActive()
        {
            this.TrialActive = true;
        }

        public Boolean IsRegistered()
        {
            // return false;

            if (TrialActive)
                return true;

            // Access registration file and determine if it is valid
            return true;
        }

        public void Register()
        {
            // Do register (via registry, file creation, etc)
        }

        public Boolean WriteRegistryKey(String Key, Object Value)
        {
            RegistryKey RegistryKey;

            try
            {
                RegistryKey = Registry.ClassesRoot.CreateSubKey(Key);
                RegistryKey.SetValue(Key, Value);
                RegistryKey.Close();
                return true;
            }
            catch (Exception Ex)
            {
                MessageBoxEx.Show(Ex.ToString());
            }

            return false;
        }

        public Object ReadRegistryKey(String Key)
        {
            String KeyValue = null;
            RegistryKey RegistryKey;

            try
            {
                RegistryKey = Registry.ClassesRoot.CreateSubKey(Key);
                KeyValue = RegistryKey.GetValue(Key).ToString();
                RegistryKey.Close();
            }
            catch (Exception Ex)
            {
                MessageBoxEx.Show(Ex.ToString());
            }

            return KeyValue;
        }

    } // End class

} // End namespace