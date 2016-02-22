using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class RegistrationManager
    {
        private RegistrationManager RegistrationManagerInstance;

        private RegistrationManager()
        {

        }

        public RegistrationManager GetInstance()
        {
            if (RegistrationManagerInstance == null)
                RegistrationManagerInstance = new RegistrationManager();
            return RegistrationManagerInstance;
        }

        public Boolean IsRegistered()
        {
            // Access registration file and determine if it is valid
            return true;
        }

        public void Register()
        {
            // Do register (via registry, file creation, etc)
        }

    } // End class

} // End namespace