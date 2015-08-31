using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public class FiniteStateTransition
    {
        private static Pen Pen = new Pen(Color.Black, TransitionLineWidth); // Pen for drawing transition line
        private static Pen ConnectionPen = new Pen(Color.Black, ConnectionWidth);   // Pen for drawing connection sphere
        private const Int32 TransitionLineWidth = 2;
        private const Int32 ConnectionWidth = 4;
        private const Int32 ConnectionRadius = 4;
        private const Int32 InvalidPort = -1;

        private FiniteState SourceState;
        private FiniteState DestinationState;
        private Int32 SourcePort;
        private Int32 DestinationPort;
        private TransitionEventEnum TransitionEvent;

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

        public FiniteStateTransition(Int32 SourcePort, FiniteState SourceState, TransitionEventEnum TransitionEvent)
        {
            this.SourceState = SourceState;
            this.SourcePort = SourcePort;
            this.TransitionEvent = TransitionEvent;
            DestinationPort = InvalidPort;
        }

        public void FinishCreateTransition(Int32 DestinationPort, FiniteState DestinationState)
        {
            this.DestinationState = DestinationState;
            this.DestinationPort = DestinationPort;
        }

        public Boolean HasTransition()
        {
            if (DestinationState == null)
                return false;

            return true;
        }

        public void Draw(Graphics Graphics, Point DestinationLocation)
        {
            if (SourceState == null || SourcePort == InvalidPort)
                return;

            if (DestinationState == null || DestinationPort == InvalidPort)
            {
                Graphics.DrawLine(Pen, SourceState.GetGripLocations()[SourcePort], DestinationLocation);
            }
            else
            {
                Graphics.DrawLine(Pen, SourceState.GetGripLocations()[SourcePort], DestinationState.GetGripLocations()[DestinationPort]);
                Graphics.DrawEllipse(ConnectionPen, DestinationState.GetGripLocations()[DestinationPort].X - ConnectionRadius, DestinationState.GetGripLocations()[DestinationPort].Y - ConnectionRadius, ConnectionRadius * 2, ConnectionRadius * 2);
            }
        }
    }
}
