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
		/// Contains action mapping constants for combat racing games.
		/// </summary>
		public ref class CombatRacingControls sealed
		{
		private:
			CombatRacingControls() { }

		public:
			/// <summary>
			/// Represents the combat racing genre.
			/// </summary>
			literal int CombatRacingGenre = DIVIRTUAL_DRIVING_COMBAT;

			/// <summary>
			/// Represents a "Steer" action.
			/// </summary>
			literal int Steer = DIAXIS_DRIVINGC_STEER;

			/// <summary>
			/// Represents a "Accelerate" action.
			/// </summary>
			literal int Accelerate = DIAXIS_DRIVINGC_ACCELERATE;

			/// <summary>
			/// Represents a "Brake" action.
			/// </summary>
			literal int BrakeAxis = DIAXIS_DRIVINGC_BRAKE;

			/// <summary>
			/// Represents a "Fire" action.
			/// </summary>
			literal int Fire = DIBUTTON_DRIVINGC_FIRE;

			/// <summary>
			/// Represents a "Weapons" action.
			/// </summary>
			literal int Weapons = DIBUTTON_DRIVINGC_WEAPONS;

			/// <summary>
			/// Represents a "Target" action.
			/// </summary>
			literal int Target = DIBUTTON_DRIVINGC_TARGET;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_DRIVINGC_MENU;

			/// <summary>
			/// Represents a "AccelerateAndBrake" action.
			/// </summary>
			literal int AccelerateAndBrake = DIAXIS_DRIVINGC_ACCEL_AND_BRAKE;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_DRIVINGC_GLANCE;

			/// <summary>
			/// Represents a "ShiftUp" action.
			/// </summary>
			literal int ShiftUp = DIBUTTON_DRIVINGC_SHIFTUP;

			/// <summary>
			/// Represents a "ShiftDown" action.
			/// </summary>
			literal int ShiftDown = DIBUTTON_DRIVINGC_SHIFTDOWN;

			/// <summary>
			/// Represents a "Dashboard" action.
			/// </summary>
			literal int Dashboard = DIBUTTON_DRIVINGC_DASHBOARD;

			/// <summary>
			/// Represents a "Aids" action.
			/// </summary>
			literal int Aids = DIBUTTON_DRIVINGC_AIDS;

			/// <summary>
			/// Represents a "Brake" action.
			/// </summary>
			literal int Brake = DIBUTTON_DRIVINGC_BRAKE;

			/// <summary>
			/// Represents a "FireSecondary" action.
			/// </summary>
			literal int FireSecondary = DIBUTTON_DRIVINGC_FIRESECONDARY;

			/// <summary>
			/// Represents a "Accelerate" link.
			/// </summary>
			literal int AccelerateLink = DIBUTTON_DRIVINGC_ACCELERATE_LINK;

			/// <summary>
			/// Represents a "SteerLeft" link.
			/// </summary>
			literal int SteerLeftLink = DIBUTTON_DRIVINGC_STEER_LEFT_LINK;

			/// <summary>
			/// Represents a "SteerRight" link.
			/// </summary>
			literal int SteerRightLink = DIBUTTON_DRIVINGC_STEER_RIGHT_LINK;

			/// <summary>
			/// Represents a "GlanceLeft" link.
			/// </summary>
			literal int GlanceLeftLink = DIBUTTON_DRIVINGC_GLANCE_LEFT_LINK;

			/// <summary>
			/// Represents a "GlanceRight" link.
			/// </summary>
			literal int GlanceRightLink = DIBUTTON_DRIVINGC_GLANCE_RIGHT_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_DRIVINGC_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_DRIVINGC_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for mechanical fighting games.
		/// </summary>
		public ref class MechanicalFightingControls sealed
		{
		private:
			MechanicalFightingControls() { }

		public:
			/// <summary>
			/// Represents the mechanical fighting genre.
			/// </summary>
			literal int MechanicalFightingGenre = DIVIRTUAL_DRIVING_MECHA;

			/// <summary>
			/// Represents a "Steer" action.
			/// </summary>
			literal int Steer = DIAXIS_MECHA_STEER;

			/// <summary>
			/// Represents a "Torso" action.
			/// </summary>
			literal int Torso = DIAXIS_MECHA_TORSO;

			/// <summary>
			/// Represents a "Rotate" action.
			/// </summary>
			literal int Rotate = DIAXIS_MECHA_ROTATE;

			/// <summary>
			/// Represents a "Throttle" action.
			/// </summary>
			literal int Throttle = DIAXIS_MECHA_THROTTLE;

			/// <summary>
			/// Represents a "Fire" action.
			/// </summary>
			literal int Fire = DIBUTTON_MECHA_FIRE;

			/// <summary>
			/// Represents a "Weapons" action.
			/// </summary>
			literal int Weapons = DIBUTTON_MECHA_WEAPONS;

			/// <summary>
			/// Represents a "Target" action.
			/// </summary>
			literal int Target = DIBUTTON_MECHA_TARGET;

			/// <summary>
			/// Represents a "Reverse" action.
			/// </summary>
			literal int Reverse = DIBUTTON_MECHA_REVERSE;

			/// <summary>
			/// Represents a "Zoom" action.
			/// </summary>
			literal int Zoom = DIBUTTON_MECHA_ZOOM;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_MECHA_JUMP;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_MECHA_MENU;

			/// <summary>
			/// Represents a "Center" action.
			/// </summary>
			literal int Center = DIBUTTON_MECHA_CENTER;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_MECHA_GLANCE;

			/// <summary>
			/// Represents a "View" action.
			/// </summary>
			literal int View = DIBUTTON_MECHA_VIEW;

			/// <summary>
			/// Represents a "FireSecondary" action.
			/// </summary>
			literal int FireSecondary = DIBUTTON_MECHA_FIRESECONDARY;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_MECHA_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_MECHA_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_MECHA_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_MECHA_BACK_LINK;

			/// <summary>
			/// Represents a "RotateLeft" link.
			/// </summary>
			literal int RotateLeftLink = DIBUTTON_MECHA_ROTATE_LEFT_LINK;

			/// <summary>
			/// Represents a "RotateRight" link.
			/// </summary>
			literal int RotateRightLink = DIBUTTON_MECHA_ROTATE_RIGHT_LINK;

			/// <summary>
			/// Represents a "Faster" link.
			/// </summary>
			literal int FasterLink = DIBUTTON_MECHA_FASTER_LINK;

			/// <summary>
			/// Represents a "Slower" link.
			/// </summary>
			literal int SlowerLink = DIBUTTON_MECHA_SLOWER_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_MECHA_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_MECHA_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for racing games.
		/// </summary>
		public ref class RacingControls sealed
		{
		private:
			RacingControls() { }

		public:
			/// <summary>
			/// Represents the racing genre.
			/// </summary>
			literal int RacingGenre = DIVIRTUAL_DRIVING_RACE;

			/// <summary>
			/// Represents a "Steer" action.
			/// </summary>
			literal int Steer = DIAXIS_DRIVINGR_STEER;

			/// <summary>
			/// Represents a "Accelerate" action.
			/// </summary>
			literal int Accelerate = DIAXIS_DRIVINGR_ACCELERATE;

			/// <summary>
			/// Represents a "Brake" action.
			/// </summary>
			literal int BrakeAxis = DIAXIS_DRIVINGR_BRAKE;

			/// <summary>
			/// Represents a "ShiftUp" action.
			/// </summary>
			literal int ShiftUp = DIBUTTON_DRIVINGR_SHIFTUP;

			/// <summary>
			/// Represents a "ShiftDown" action.
			/// </summary>
			literal int ShiftDown = DIBUTTON_DRIVINGR_SHIFTDOWN;

			/// <summary>
			/// Represents a "View" action.
			/// </summary>
			literal int View = DIBUTTON_DRIVINGR_VIEW;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_DRIVINGR_MENU;

			/// <summary>
			/// Represents a "AccelerateAndBrake" action.
			/// </summary>
			literal int AccelerateAndBrake = DIAXIS_DRIVINGR_ACCEL_AND_BRAKE;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_DRIVINGR_GLANCE;

			/// <summary>
			/// Represents a "Brake" action.
			/// </summary>
			literal int Brake = DIBUTTON_DRIVINGR_BRAKE;

			/// <summary>
			/// Represents a "Dashboard" action.
			/// </summary>
			literal int Dashboard = DIBUTTON_DRIVINGR_DASHBOARD;

			/// <summary>
			/// Represents a "Aids" action.
			/// </summary>
			literal int Aids = DIBUTTON_DRIVINGR_AIDS;

			/// <summary>
			/// Represents a "Map" action.
			/// </summary>
			literal int Map = DIBUTTON_DRIVINGR_MAP;

			/// <summary>
			/// Represents a "Boost" action.
			/// </summary>
			literal int Boost = DIBUTTON_DRIVINGR_BOOST;

			/// <summary>
			/// Represents a "Pit" action.
			/// </summary>
			literal int Pit = DIBUTTON_DRIVINGR_PIT;

			/// <summary>
			/// Represents a "Accelerate" link.
			/// </summary>
			literal int AccelerateLink = DIBUTTON_DRIVINGR_ACCELERATE_LINK;

			/// <summary>
			/// Represents a "SteerLeft" link.
			/// </summary>
			literal int SteerLeftLink = DIBUTTON_DRIVINGR_STEER_LEFT_LINK;

			/// <summary>
			/// Represents a "SteerRight" link.
			/// </summary>
			literal int SteerRightLink = DIBUTTON_DRIVINGR_STEER_RIGHT_LINK;

			/// <summary>
			/// Represents a "GlanceLeft" link.
			/// </summary>
			literal int GlanceLeftLink = DIBUTTON_DRIVINGR_GLANCE_LEFT_LINK;

			/// <summary>
			/// Represents a "GlanceRight" link.
			/// </summary>
			literal int GlanceRightLink = DIBUTTON_DRIVINGR_GLANCE_RIGHT_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_DRIVINGR_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_DRIVINGR_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for tank driving games.
		/// </summary>
		public ref class TankDrivingControls sealed
		{
		private:
			TankDrivingControls() { }

		public:
			/// <summary>
			/// Represents the tank driving genre.
			/// </summary>
			literal int TankDrivingGenre = DIVIRTUAL_DRIVING_TANK;

			/// <summary>
			/// Represents a "Steer" action.
			/// </summary>
			literal int Steer = DIAXIS_DRIVINGT_STEER;

			/// <summary>
			/// Represents a "Barrel" action.
			/// </summary>
			literal int Barrel = DIAXIS_DRIVINGT_BARREL;

			/// <summary>
			/// Represents a "Accelerate" action.
			/// </summary>
			literal int Accelerate = DIAXIS_DRIVINGT_ACCELERATE;

			/// <summary>
			/// Represents a "Rotate" action.
			/// </summary>
			literal int Rotate = DIAXIS_DRIVINGT_ROTATE;

			/// <summary>
			/// Represents a "Fire" action.
			/// </summary>
			literal int Fire = DIBUTTON_DRIVINGT_FIRE;

			/// <summary>
			/// Represents a "Weapons" action.
			/// </summary>
			literal int Weapons = DIBUTTON_DRIVINGT_WEAPONS;

			/// <summary>
			/// Represents a "Target" action.
			/// </summary>
			literal int Target = DIBUTTON_DRIVINGT_TARGET;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_DRIVINGT_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_DRIVINGT_GLANCE;

			/// <summary>
			/// Represents a "Brake" action.
			/// </summary>
			literal int BrakeAxis = DIAXIS_DRIVINGT_BRAKE;

			/// <summary>
			/// Represents a "AccelerateAndBrake" action.
			/// </summary>
			literal int AccelerateAndBrake = DIAXIS_DRIVINGT_ACCEL_AND_BRAKE;

			/// <summary>
			/// Represents a "View" action.
			/// </summary>
			literal int View = DIBUTTON_DRIVINGT_VIEW;

			/// <summary>
			/// Represents a "Dashboard" action.
			/// </summary>
			literal int Dashboard = DIBUTTON_DRIVINGT_DASHBOARD;

			/// <summary>
			/// Represents a "Brake" action.
			/// </summary>
			literal int Brake = DIBUTTON_DRIVINGT_BRAKE;

			/// <summary>
			/// Represents a "FireSecondary" action.
			/// </summary>
			literal int FireSecondary = DIBUTTON_DRIVINGT_FIRESECONDARY;

			/// <summary>
			/// Represents a "Accelerate" link.
			/// </summary>
			literal int AccelerateLink = DIBUTTON_DRIVINGT_ACCELERATE_LINK;

			/// <summary>
			/// Represents a "SteerLeft" link.
			/// </summary>
			literal int SteerLeftLink = DIBUTTON_DRIVINGT_STEER_LEFT_LINK;

			/// <summary>
			/// Represents a "SteerRight" link.
			/// </summary>
			literal int SteerRightLink = DIBUTTON_DRIVINGT_STEER_RIGHT_LINK;

			/// <summary>
			/// Represents a "BarrelUp" link.
			/// </summary>
			literal int BarrelUpLink = DIBUTTON_DRIVINGT_BARREL_UP_LINK;

			/// <summary>
			/// Represents a "BarrelDown" link.
			/// </summary>
			literal int BarrelDownLink = DIBUTTON_DRIVINGT_BARREL_DOWN_LINK;

			/// <summary>
			/// Represents a "RotateLeft" link.
			/// </summary>
			literal int RotateLeftLink = DIBUTTON_DRIVINGT_ROTATE_LEFT_LINK;

			/// <summary>
			/// Represents a "RotateRight" link.
			/// </summary>
			literal int RotateRightLink = DIBUTTON_DRIVINGT_ROTATE_RIGHT_LINK;

			/// <summary>
			/// Represents a "GlanceLeft" link.
			/// </summary>
			literal int GlanceLeftLink = DIBUTTON_DRIVINGT_GLANCE_LEFT_LINK;

			/// <summary>
			/// Represents a "GlanceRight" link.
			/// </summary>
			literal int GlanceRightLink = DIBUTTON_DRIVINGT_GLANCE_RIGHT_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_DRIVINGT_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_DRIVINGT_PAUSE;
		};
	}
}
