using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class FiniteStateFilter : MemorySharp
    {
        private List<RemoteVirtualPage> RemoteRegions;

        public enum RequiredStates
        {
            DeadState = 0,      // State for a denied variable
            AcceptedState = 1,  // State for an accepted variable
            InitialState = 2,   // Starting state for all varaibles
        }

        public enum TransitionEventEnum
        {
            Changed,
            Increased,
            Decreased,
            GreaterThan,
            LessThan,
            GreatherThanOrEqual,
            LessThanOrEqual,
            IncreasedBy,
            DecreasedBy,
            ExponentialNotation,
        }

        public FiniteStateFilter(Process Process) : base(Process)
        {

        }

        public void Begin(List<RemoteVirtualPage> RemoteRegions)
        {
            this.RemoteRegions = RemoteRegions;
        }
    }
}
