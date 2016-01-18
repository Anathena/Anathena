using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Anathema
{
    class FiniteStateBuilder : IFiniteStateBuilderModel
    {
        // User controlled variables
        private FiniteStateMachine FiniteStateMachine;

        public FiniteStateBuilder()
        {
            FiniteStateMachine = new FiniteStateMachine();
        }

        public override void SetElementType(Type ElementType)
        {
            FiniteStateMachine.SetElementType(ElementType);
        }

        public override Type GetElementType()
        {
            return FiniteStateMachine.GetElementType();
        }

        public override FiniteStateMachine GetFiniteStateMachine()
        {
            return FiniteStateMachine;
        }

        private void UpdateDisplay()
        {
            FiniteStateBuilderEventArgs FilterFSMEventArgs = new FiniteStateBuilderEventArgs();
            OnEventUpdateDisplay(FilterFSMEventArgs);
        }
    }
}
