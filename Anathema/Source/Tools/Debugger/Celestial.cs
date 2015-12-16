using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class Celestial
    {
        private static Celestial CelestialInstance; // Static reference to this class
        private MemorySharp MemoryEditor;           // Memory editor instance

        private Celestial()
        {

        }

        /// <summary>
        /// Returns the instance of the singleton anathema object
        /// </summary>
        public static Celestial GetCelestialInstance()
        {
            if (CelestialInstance == null)
                CelestialInstance = new Celestial();
            return CelestialInstance;
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
