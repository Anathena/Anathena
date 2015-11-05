using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class Benediction
    {
        private static Benediction BenedictionInstance; // Static reference to this class
        private MemorySharp MemoryEditor;               // Memory editor instance

        private Benediction()
        {

        }

        /// <summary>
        /// Returns the instance of the singleton anathema object
        /// </summary>
        public static Benediction GetAnathemaInstance()
        {
            if (BenedictionInstance == null)
                BenedictionInstance = new Benediction();
            return BenedictionInstance;
        }

        /// <summary>
        /// Update the target process 
        /// </summary>
        /// <param name="TargetProcess"></param>
        public void UpdateTargetProcess(Process TargetProcess)
        {
            // Instantiate a new memory editor with the new target process
            MemoryEditor = new MemorySharp(TargetProcess);
        }
    }
}
