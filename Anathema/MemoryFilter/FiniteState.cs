using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public class FiniteState
    {
        private static Pen Pen = new Pen(Color.Black, StatePenWidth);           // Main pen for drawing state circles
        private static Pen GripPen = new Pen(Color.Gold, GripPenWidth);         // Pen for drawing grips
        private static Pen AcceptedPen = new Pen(Color.Green, StatePenWidth);   // Pen for drawing accepted state circle
        private static Pen DeadPen = new Pen(Color.Red, StatePenWidth);         // Pen for drawing dead state circle
        private static Pen InitialPen = new Pen(Color.Blue, StatePenWidth);     // Pen for drawing initial state circle

        public const Int32 MaximumStates = Byte.MaxValue;   // Maximum number of states total (nobody should ever need more than 255...)
        private const Int32 GripCount = 8;                  // Number of grips on each state circle
        public const Int32 InvalidGrip = -1;

        public const Int32 StateRadius = 24;               // Radius for a state's graphical circle
        private const Int32 StatePenWidth = 3;              // Width for drawing state circles
        private const Int32 GripRadius = 4;                 // Radius for the grips on the side of a state circle
        private const Int32 GripPenWidth = 4;               // Width for drawing the grips

        // Constants for default states that must exist in each finite state machine
        public enum RequiredStates
        {
            DeadState = 0,      // State for a denied variable
            AcceptedState = 1,  // State for an accepted variable
            InitialState = 2,   // Starting state for all varaibles
        }
        
        private List<FiniteStateTransition> Transitions;   // List of states to which this state connects and the port
        private FiniteStateTransition SelectedTransition;  // Transition being created
        private Point[] GripLocations;  // Location for each grip on the side of the circle
        private Point Location;         // Location of the center of the state's circle
        private Boolean Placing;        // Whether or not this state is being placed by the user
        private Boolean Connecting;     // Whether or not a transition is being created from this state
        private Boolean MousedOver;     // Whether or not this state is being moused over

        public FiniteState(Point Location)
        {
            Initialize(Location);
        }

        public FiniteState(Int32 X, Int32 Y)
        {
            Initialize(new Point(X, Y));
        }

        public void Initialize(Point Location)
        {
            GripLocations = new Point[GripCount];
            Transitions = new List<FiniteStateTransition>();
            EndPlace();
            SetLocation(Location);
        }

        public Point[] GetGripLocations()
        {
            return GripLocations;
        }

        public void StartCreateTransition(Int32 SourcePort, FiniteStateTransition.TransitionEventEnum TransitionEvent)
        {
            SelectedTransition = new FiniteStateTransition(SourcePort, this, TransitionEvent);
            Transitions.Add(SelectedTransition);
        }

        public void EndCreateTransition(Int32 DestinationPort, FiniteState DestinationState)
        {
            SelectedTransition.FinishCreateTransition(DestinationPort, DestinationState);
        }

        public Boolean IsPlacing()
        {
            return Placing;
        }

        public void BeginPlace()
        {
            Placing = true;
        }

        public void EndPlace()
        {
            Placing = false;
        }

        public Boolean IsConnecting()
        {
            return Connecting;
        }

        public void BeginConnecting()
        {
            Connecting = true;
        }

        public void EndConnecting()
        {
            Connecting = false;

            if (!SelectedTransition.HasTransition())
                Transitions.Remove(SelectedTransition);
        }

        public Boolean IsMousedOver()
        {
            return MousedOver;
        }

        public Boolean UpdateMouseOver(Point Location)
        {
            Boolean OldState = MousedOver;

            if (IsPointInCircle(Location) || GetGripAtLocation(Location) != InvalidGrip)
                MousedOver = true;
            else
                MousedOver = false;

            return (OldState != MousedOver);
        }

        public void EndMouseOver()
        {
            MousedOver = false;
        }

        public void SetLocation(Point Location)
        {
            this.Location = Location;

            // Update the positions for the various grips
            for (Int32 Index = 0; Index < GripCount; Index++)
            {
                GripLocations[Index].X = Location.X + (Int32)((Double)StateRadius * Math.Cos(2 * Math.PI * (Double)Index / (Double)GripCount));
                GripLocations[Index].Y = Location.Y + (Int32)((Double)StateRadius * Math.Sin(2 * Math.PI * (Double)Index / (Double)GripCount));
            }
        }

        public bool IsPointInCircle(Point ClickLocation)
        {
            if (Distance(Location, ClickLocation) <= StateRadius)
                return true;

            return false;
        }

        public Int32 GetGripAtLocation(Point ClickLocation)
        {
            // Test for clicking on any of the grips
            for (Int32 Index = 0; Index < GripCount; Index++)
            {
                if (Distance(GripLocations[Index], ClickLocation) <= GripRadius)
                    return Index;
            }

            return InvalidGrip;
        }

        private Int32 Distance(Point LocationA, Point LocationB)
        {
            return (Int32)Math.Sqrt(Math.Pow(LocationA.X - LocationB.X, 2) + Math.Pow(LocationA.Y - LocationB.Y, 2));
        }

        public void Draw(Graphics Graphics, Int32 StateID, Point MouseLocation)
        {

            // Set the pen color based on the state of this
            Pen PenType;
            switch (StateID)
            {
                case (Int32)RequiredStates.AcceptedState:
                    PenType = AcceptedPen;
                    break;
                case (Int32)RequiredStates.DeadState:
                    PenType = DeadPen;
                    break;
                case (Int32)RequiredStates.InitialState:
                    PenType = InitialPen;
                    break;
                default:
                    PenType = Pen;
                    break;
            }

            // Draw the circle representing this state
            Graphics.DrawEllipse(PenType, Location.X - StateRadius, Location.Y - StateRadius, StateRadius * 2, StateRadius * 2);

            for (Int32 Index = 0; Index < Transitions.Count; Index++)
            {
                Transitions[Index].Draw(Graphics, MouseLocation);
            }

            // Do not draw grips if placing, just return
            if (IsPlacing() || !IsMousedOver())
                return;

            // Draw the grips for this state circle
            for (Int32 Index = 0; Index < GripCount; Index++)
            {
                Graphics.DrawEllipse(GripPen, GripLocations[Index].X - GripRadius, GripLocations[Index].Y - GripRadius, GripRadius * 2, GripRadius * 2);
            }


        }
    }
}
