/*
* Copyright (c) 2007-2012 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/
#pragma once

namespace SlimDX
{
	namespace DirectInput
	{
		/// <summary>
		/// Contains action mapping constants for air combat games.
		/// </summary>
		public ref class AirCombatControls sealed
		{
		private:
			AirCombatControls() { }

		public:
			/// <summary>
			/// Represents the air combat genre.
			/// </summary>
			literal int AirCombatGenre = DIVIRTUAL_FLYING_MILITARY;

			/// <summary>
			/// Represents a "Bank" action.
			/// </summary>
			literal int Bank = DIAXIS_FLYINGM_BANK;

			/// <summary>
			/// Represents a "Pitch" action.
			/// </summary>
			literal int Pitch = DIAXIS_FLYINGM_PITCH;

			/// <summary>
			/// Represents a "Throttle" action.
			/// </summary>
			literal int Throttle = DIAXIS_FLYINGM_THROTTLE;

			/// <summary>
			/// Represents a "Fire" action.
			/// </summary>
			literal int Fire = DIBUTTON_FLYINGM_FIRE;

			/// <summary>
			/// Represents a "Weapons" action.
			/// </summary>
			literal int Weapons = DIBUTTON_FLYINGM_WEAPONS;

			/// <summary>
			/// Represents a "Target" action.
			/// </summary>
			literal int Target = DIBUTTON_FLYINGM_TARGET;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_FLYINGM_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_FLYINGM_GLANCE;

			/// <summary>
			/// Represents a "Counter" action.
			/// </summary>
			literal int Counter = DIBUTTON_FLYINGM_COUNTER;

			/// <summary>
			/// Represents a "Rudder" action.
			/// </summary>
			literal int Rudder = DIAXIS_FLYINGM_RUDDER;

			/// <summary>
			/// Represents a "Brake" action.
			/// </summary>
			literal int Brake = DIAXIS_FLYINGM_BRAKE;

			/// <summary>
			/// Represents a "View" action.
			/// </summary>
			literal int View = DIBUTTON_FLYINGM_VIEW;

			/// <summary>
			/// Represents a "Display" action.
			/// </summary>
			literal int Display = DIBUTTON_FLYINGM_DISPLAY;

			/// <summary>
			/// Represents a "Flaps" action.
			/// </summary>
			literal int Flaps = DIAXIS_FLYINGM_FLAPS;

			/// <summary>
			/// Represents a "FlapsUp" action.
			/// </summary>
			literal int FlapsUp = DIBUTTON_FLYINGM_FLAPSUP;

			/// <summary>
			/// Represents a "FlapsDown" action.
			/// </summary>
			literal int FlapsDown = DIBUTTON_FLYINGM_FLAPSDOWN;

			/// <summary>
			/// Represents a "FireSecondary" action.
			/// </summary>
			literal int FireSecondary = DIBUTTON_FLYINGM_FIRESECONDARY;

			/// <summary>
			/// Represents a "Gear" action.
			/// </summary>
			literal int Gear = DIBUTTON_FLYINGM_GEAR;

			/// <summary>
			/// Represents a "Brake" link.
			/// </summary>
			literal int BrakeLink = DIBUTTON_FLYINGM_BRAKE_LINK;

			/// <summary>
			/// Represents a "Faster" link.
			/// </summary>
			literal int FasterLink = DIBUTTON_FLYINGM_FASTER_LINK;

			/// <summary>
			/// Represents a "Slower" link.
			/// </summary>
			literal int SlowerLink = DIBUTTON_FLYINGM_SLOWER_LINK;

			/// <summary>
			/// Represents a "GlanceLeft" link.
			/// </summary>
			literal int GlanceLeftLink = DIBUTTON_FLYINGM_GLANCE_LEFT_LINK;

			/// <summary>
			/// Represents a "GlanceRight" link.
			/// </summary>
			literal int GlanceRightLink = DIBUTTON_FLYINGM_GLANCE_RIGHT_LINK;

			/// <summary>
			/// Represents a "GlanceUp" link.
			/// </summary>
			literal int GlanceUpLink = DIBUTTON_FLYINGM_GLANCE_UP_LINK;

			/// <summary>
			/// Represents a "GlanceDown" link.
			/// </summary>
			literal int GlanceDownLink = DIBUTTON_FLYINGM_GLANCE_DOWN_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_FLYINGM_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_FLYINGM_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for civilian flight games.
		/// </summary>
		public ref class CivilianFlightControls sealed
		{
		private:
			CivilianFlightControls() { }

		public:
			/// <summary>
			/// Represents the civilian flight genre.
			/// </summary>
			literal int CivilianFlightGenre = DIVIRTUAL_FLYING_CIVILIAN;

			/// <summary>
			/// Represents a "Bank" action.
			/// </summary>
			literal int Bank = DIAXIS_FLYINGC_BANK;

			/// <summary>
			/// Represents a "Pitch" action.
			/// </summary>
			literal int Pitch = DIAXIS_FLYINGC_PITCH;

			/// <summary>
			/// Represents a "Throttle" action.
			/// </summary>
			literal int Throttle = DIAXIS_FLYINGC_THROTTLE;

			/// <summary>
			/// Represents a "View" action.
			/// </summary>
			literal int View = DIBUTTON_FLYINGC_VIEW;

			/// <summary>
			/// Represents a "Display" action.
			/// </summary>
			literal int Display = DIBUTTON_FLYINGC_DISPLAY;

			/// <summary>
			/// Represents a "Gear" action.
			/// </summary>
			literal int Gear = DIBUTTON_FLYINGC_GEAR;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_FLYINGC_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_FLYINGC_GLANCE;

			/// <summary>
			/// Represents a "Brake" action.
			/// </summary>
			literal int Brake = DIAXIS_FLYINGC_BRAKE;

			/// <summary>
			/// Represents a "Rudder" action.
			/// </summary>
			literal int Rudder = DIAXIS_FLYINGC_RUDDER;

			/// <summary>
			/// Represents a "Flaps" action.
			/// </summary>
			literal int Flaps = DIAXIS_FLYINGC_FLAPS;

			/// <summary>
			/// Represents a "FlapsUp" action.
			/// </summary>
			literal int FlapsUp = DIBUTTON_FLYINGC_FLAPSUP;

			/// <summary>
			/// Represents a "FlapsDown" action.
			/// </summary>
			literal int FlapsDown = DIBUTTON_FLYINGC_FLAPSDOWN;

			/// <summary>
			/// Represents a "Brake" link.
			/// </summary>
			literal int BrakeLink = DIBUTTON_FLYINGC_BRAKE_LINK;

			/// <summary>
			/// Represents a "Faster" link.
			/// </summary>
			literal int FasterLink = DIBUTTON_FLYINGC_FASTER_LINK;

			/// <summary>
			/// Represents a "Slower" link.
			/// </summary>
			literal int SlowerLink = DIBUTTON_FLYINGC_SLOWER_LINK;

			/// <summary>
			/// Represents a "GlanceLeft" link.
			/// </summary>
			literal int GlanceLeftLink = DIBUTTON_FLYINGC_GLANCE_LEFT_LINK;

			/// <summary>
			/// Represents a "GlanceRight" link.
			/// </summary>
			literal int GlanceRightLink = DIBUTTON_FLYINGC_GLANCE_RIGHT_LINK;

			/// <summary>
			/// Represents a "GlanceUp" link.
			/// </summary>
			literal int GlanceUpLink = DIBUTTON_FLYINGC_GLANCE_UP_LINK;

			/// <summary>
			/// Represents a "GlanceDown" link.
			/// </summary>
			literal int GlanceDownLink = DIBUTTON_FLYINGC_GLANCE_DOWN_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_FLYINGC_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_FLYINGC_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for helicopter combat games.
		/// </summary>
		public ref class HelicopterCombatControls sealed
		{
		private:
			HelicopterCombatControls() { }

		public:
			/// <summary>
			/// Represents the helicopter combat genre.
			/// </summary>
			literal int HelicopterCombatGenre = DIVIRTUAL_FLYING_HELICOPTER;

			/// <summary>
			/// Represents a "Bank" action.
			/// </summary>
			literal int Bank = DIAXIS_FLYINGH_BANK;

			/// <summary>
			/// Represents a "Pitch" action.
			/// </summary>
			literal int Pitch = DIAXIS_FLYINGH_PITCH;

			/// <summary>
			/// Represents a "Collective" action.
			/// </summary>
			literal int Collective = DIAXIS_FLYINGH_COLLECTIVE;

			/// <summary>
			/// Represents a "Fire" action.
			/// </summary>
			literal int Fire = DIBUTTON_FLYINGH_FIRE;

			/// <summary>
			/// Represents a "Weapons" action.
			/// </summary>
			literal int Weapons = DIBUTTON_FLYINGH_WEAPONS;

			/// <summary>
			/// Represents a "Target" action.
			/// </summary>
			literal int Target = DIBUTTON_FLYINGH_TARGET;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_FLYINGH_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_FLYINGH_GLANCE;

			/// <summary>
			/// Represents a "Torque" action.
			/// </summary>
			literal int Torque = DIAXIS_FLYINGH_TORQUE;

			/// <summary>
			/// Represents a "Throttle" action.
			/// </summary>
			literal int Throttle = DIAXIS_FLYINGH_THROTTLE;

			/// <summary>
			/// Represents a "Counter" action.
			/// </summary>
			literal int Counter = DIBUTTON_FLYINGH_COUNTER;

			/// <summary>
			/// Represents a "View" action.
			/// </summary>
			literal int View = DIBUTTON_FLYINGH_VIEW;

			/// <summary>
			/// Represents a "Gear" action.
			/// </summary>
			literal int Gear = DIBUTTON_FLYINGH_GEAR;

			/// <summary>
			/// Represents a "FireSecondary" action.
			/// </summary>
			literal int FireSecondary = DIBUTTON_FLYINGH_FIRESECONDARY;

			/// <summary>
			/// Represents a "Faster" link.
			/// </summary>
			literal int FasterLink = DIBUTTON_FLYINGH_FASTER_LINK;

			/// <summary>
			/// Represents a "Slower" link.
			/// </summary>
			literal int SlowerLink = DIBUTTON_FLYINGH_SLOWER_LINK;

			/// <summary>
			/// Represents a "GlanceLeft" link.
			/// </summary>
			literal int GlanceLeftLink = DIBUTTON_FLYINGH_GLANCE_LEFT_LINK;

			/// <summary>
			/// Represents a "GlanceRight" link.
			/// </summary>
			literal int GlanceRightLink = DIBUTTON_FLYINGH_GLANCE_RIGHT_LINK;

			/// <summary>
			/// Represents a "GlanceUp" link.
			/// </summary>
			literal int GlanceUpLink = DIBUTTON_FLYINGH_GLANCE_UP_LINK;

			/// <summary>
			/// Represents a "GlanceDown" link.
			/// </summary>
			literal int GlanceDownLink = DIBUTTON_FLYINGH_GLANCE_DOWN_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_FLYINGH_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_FLYINGH_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for space combat games.
		/// </summary>
		public ref class SpaceCombatControls sealed
		{
		private:
			SpaceCombatControls() { }

		public:
			/// <summary>
			/// Represents the space combat genre.
			/// </summary>
			literal int SpaceCombatGenre = DIVIRTUAL_SPACESIM;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_SPACESIM_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_SPACESIM_MOVE;

			/// <summary>
			/// Represents a "Throttle" action.
			/// </summary>
			literal int Throttle = DIAXIS_SPACESIM_THROTTLE;

			/// <summary>
			/// Represents a "Fire" action.
			/// </summary>
			literal int Fire = DIBUTTON_SPACESIM_FIRE;

			/// <summary>
			/// Represents a "Weapons" action.
			/// </summary>
			literal int Weapons = DIBUTTON_SPACESIM_WEAPONS;

			/// <summary>
			/// Represents a "Target" action.
			/// </summary>
			literal int Target = DIBUTTON_SPACESIM_TARGET;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_SPACESIM_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_SPACESIM_GLANCE;

			/// <summary>
			/// Represents a "Climb" action.
			/// </summary>
			literal int Climb = DIAXIS_SPACESIM_CLIMB;

			/// <summary>
			/// Represents a "Rotate" action.
			/// </summary>
			literal int Rotate = DIAXIS_SPACESIM_ROTATE;

			/// <summary>
			/// Represents a "View" action.
			/// </summary>
			literal int View = DIBUTTON_SPACESIM_VIEW;

			/// <summary>
			/// Represents a "Display" action.
			/// </summary>
			literal int Display = DIBUTTON_SPACESIM_DISPLAY;

			/// <summary>
			/// Represents a "Raise" action.
			/// </summary>
			literal int Raise = DIBUTTON_SPACESIM_RAISE;

			/// <summary>
			/// Represents a "Lower" action.
			/// </summary>
			literal int Lower = DIBUTTON_SPACESIM_LOWER;

			/// <summary>
			/// Represents a "Gear" action.
			/// </summary>
			literal int Gear = DIBUTTON_SPACESIM_GEAR;

			/// <summary>
			/// Represents a "FireSecondary" action.
			/// </summary>
			literal int FireSecondary = DIBUTTON_SPACESIM_FIRESECONDARY;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_SPACESIM_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_SPACESIM_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_SPACESIM_FORWARD_LINK;

			/// <summary>
			/// Represents a "Backward" link.
			/// </summary>
			literal int BackwardLink = DIBUTTON_SPACESIM_BACKWARD_LINK;

			/// <summary>
			/// Represents a "Faster" link.
			/// </summary>
			literal int FasterLink = DIBUTTON_SPACESIM_FASTER_LINK;

			/// <summary>
			/// Represents a "Slower" link.
			/// </summary>
			literal int SlowerLink = DIBUTTON_SPACESIM_SLOWER_LINK;

			/// <summary>
			/// Represents a "TurnLeft" link.
			/// </summary>
			literal int TurnLeftLink = DIBUTTON_SPACESIM_TURN_LEFT_LINK;

			/// <summary>
			/// Represents a "TurnRight" link.
			/// </summary>
			literal int TurnRightLink = DIBUTTON_SPACESIM_TURN_RIGHT_LINK;

			/// <summary>
			/// Represents a "GlanceLeft" link.
			/// </summary>
			literal int GlanceLeftLink = DIBUTTON_SPACESIM_GLANCE_LEFT_LINK;

			/// <summary>
			/// Represents a "GlanceRight" link.
			/// </summary>
			literal int GlanceRightLink = DIBUTTON_SPACESIM_GLANCE_RIGHT_LINK;

			/// <summary>
			/// Represents a "GlanceUp" link.
			/// </summary>
			literal int GlanceUpLink = DIBUTTON_SPACESIM_GLANCE_UP_LINK;

			/// <summary>
			/// Represents a "GlanceDown" link.
			/// </summary>
			literal int GlanceDownLink = DIBUTTON_SPACESIM_GLANCE_DOWN_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_SPACESIM_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_SPACESIM_PAUSE;
		};
	}
}
