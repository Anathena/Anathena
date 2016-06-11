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
		/// Contains action mapping constants for Hand-To-Hand fighting games.
		/// </summary>
		public ref class HandToHandControls sealed
		{
		private:
			HandToHandControls() { }

		public:
			/// <summary>
			/// Represents the hand-to-hand genre.
			/// </summary>
			literal int HandToHandGenre = DIVIRTUAL_FIGHTING_HAND2HAND;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_FIGHTINGH_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_FIGHTINGH_MOVE;

			/// <summary>
			/// Represents a "Block" action.
			/// </summary>
			literal int Block = DIBUTTON_FIGHTINGH_BLOCK;

			/// <summary>
			/// Represents a "Crouch" action.
			/// </summary>
			literal int Crouch = DIBUTTON_FIGHTINGH_CROUCH;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_FIGHTINGH_JUMP;

			/// <summary>
			/// Represents a "Kick" action.
			/// </summary>
			literal int Kick = DIBUTTON_FIGHTINGH_KICK;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_FIGHTINGH_MENU;

			/// <summary>
			/// Represents a "Punch" action.
			/// </summary>
			literal int Punch = DIBUTTON_FIGHTINGH_PUNCH;

			/// <summary>
			/// Represents a "Special1" action.
			/// </summary>
			literal int Special1 = DIBUTTON_FIGHTINGH_SPECIAL1;

			/// <summary>
			/// Represents a "Special2" action.
			/// </summary>
			literal int Special2 = DIBUTTON_FIGHTINGH_SPECIAL2;

			/// <summary>
			/// Represents a "Rotate" action.
			/// </summary>
			literal int Rotate = DIAXIS_FIGHTINGH_ROTATE;

			/// <summary>
			/// Represents a "Backward" link.
			/// </summary>
			literal int BackwardLink = DIBUTTON_FIGHTINGH_BACKWARD_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_FIGHTINGH_DEVICE;

			/// <summary>
			/// Represents a "Display" action.
			/// </summary>
			literal int Display = DIBUTTON_FIGHTINGH_DISPLAY;

			/// <summary>
			/// Represents a "Dodge" action.
			/// </summary>
			literal int Dodge = DIBUTTON_FIGHTINGH_DODGE;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_FIGHTINGH_FORWARD_LINK;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_FIGHTINGH_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_FIGHTINGH_RIGHT_LINK;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_FIGHTINGH_PAUSE;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_FIGHTINGH_SELECT;

			/// <summary>
			/// Represents a "Slide" action.
			/// </summary>
			literal int Slide = DIHATSWITCH_FIGHTINGH_SLIDE;
		};

		/// <summary>
		/// Contains action mapping constants for first person shooter games.
		/// </summary>
		public ref class ShootingControls sealed
		{
		private:
			ShootingControls() { }

		public:
			/// <summary>
			/// Represents the shooting genre.
			/// </summary>
			literal int ShootingGenre = DIVIRTUAL_FIGHTING_FPS;

			/// <summary>
			/// Represents a "LookUpDown" action.
			/// </summary>
			literal int LookUpDown = DIAXIS_FPS_LOOKUPDOWN;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_FPS_MOVE;

			/// <summary>
			/// Represents a "Rotate" action.
			/// </summary>
			literal int Rotate = DIAXIS_FPS_ROTATE;

			/// <summary>
			/// Represents a "Apply" action.
			/// </summary>
			literal int Apply = DIBUTTON_FPS_APPLY;

			/// <summary>
			/// Represents a "Crouch" action.
			/// </summary>
			literal int Crouch = DIBUTTON_FPS_CROUCH;

			/// <summary>
			/// Represents a "Fire" action.
			/// </summary>
			literal int Fire = DIBUTTON_FPS_FIRE;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_FPS_JUMP;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_FPS_MENU;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_FPS_SELECT;

			/// <summary>
			/// Represents a "Strafe" action.
			/// </summary>
			literal int Strafe = DIBUTTON_FPS_STRAFE;

			/// <summary>
			/// Represents a "Weapons" action.
			/// </summary>
			literal int Weapons = DIBUTTON_FPS_WEAPONS;

			/// <summary>
			/// Represents a "SideStep" action.
			/// </summary>
			literal int SideStep = DIAXIS_FPS_SIDESTEP;

			/// <summary>
			/// Represents a "Backward" link.
			/// </summary>
			literal int BackwardLink = DIBUTTON_FPS_BACKWARD_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_FPS_DEVICE;

			/// <summary>
			/// Represents a "Display" action.
			/// </summary>
			literal int Display = DIBUTTON_FPS_DISPLAY;

			/// <summary>
			/// Represents a "Dodge" action.
			/// </summary>
			literal int Dodge = DIBUTTON_FPS_DODGE;

			/// <summary>
			/// Represents a "FireSecondary" action.
			/// </summary>
			literal int FireSecondary = DIBUTTON_FPS_FIRESECONDARY;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_FPS_FORWARD_LINK;

			/// <summary>
			/// Represents a "GlanceDown" link.
			/// </summary>
			literal int GlanceDownLink = DIBUTTON_FPS_GLANCE_DOWN_LINK;

			/// <summary>
			/// Represents a "GlanceUp" link.
			/// </summary>
			literal int GlanceUpLink = DIBUTTON_FPS_GLANCE_UP_LINK;

			/// <summary>
			/// Represents a "GlanceLeft" link.
			/// </summary>
			literal int GlanceLeft = DIBUTTON_FPS_GLANCEL;

			/// <summary>
			/// Represents a "GlanceRight" link.
			/// </summary>
			literal int GlanceRight = DIBUTTON_FPS_GLANCER;

			/// <summary>
			/// Represents a "RotateLeft" link.
			/// </summary>
			literal int RotateLeftLink = DIBUTTON_FPS_ROTATE_LEFT_LINK;

			/// <summary>
			/// Represents a "RotateRight" link.
			/// </summary>
			literal int RotateRightLink = DIBUTTON_FPS_ROTATE_RIGHT_LINK;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_FPS_PAUSE;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_FPS_GLANCE;
		};

		/// <summary>
		/// Contains action mapping constants for third person fighting games.
		/// </summary>
		public ref class ThirdPersonControls sealed
		{
		private:
			ThirdPersonControls() { }

		public:
			/// <summary>
			/// Represents the third person genre.
			/// </summary>
			literal int ThirdPersonGenre = DIVIRTUAL_FIGHTING_THIRDPERSON;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_TPS_MOVE;

			/// <summary>
			/// Represents a "Turn" action.
			/// </summary>
			literal int Turn = DIAXIS_TPS_TURN;

			/// <summary>
			/// Represents a "Action" action.
			/// </summary>
			literal int Action = DIBUTTON_TPS_ACTION;

			/// <summary>
			/// Represents a "Use" action.
			/// </summary>
			literal int Use = DIBUTTON_TPS_USE;

			/// <summary>
			/// Represents a "Run" action.
			/// </summary>
			literal int Run = DIBUTTON_TPS_RUN;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_TPS_JUMP;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_TPS_MENU;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_TPS_SELECT;

			/// <summary>
			/// Represents a "Step" action.
			/// </summary>
			literal int Step = DIAXIS_TPS_STEP;

			/// <summary>
			/// Represents a "Backward" link.
			/// </summary>
			literal int BackwardLink = DIBUTTON_TPS_BACKWARD_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_TPS_DEVICE;

			/// <summary>
			/// Represents a "Dodge" action.
			/// </summary>
			literal int Dodge = DIBUTTON_TPS_DODGE;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_TPS_FORWARD_LINK;

			/// <summary>
			/// Represents a "GlanceDown" link.
			/// </summary>
			literal int GlanceDownLink = DIBUTTON_TPS_GLANCE_DOWN_LINK;

			/// <summary>
			/// Represents a "GlanceUp" link.
			/// </summary>
			literal int GlanceUpLink = DIBUTTON_TPS_GLANCE_UP_LINK;

			/// <summary>
			/// Represents a "GlanceLeft" link.
			/// </summary>
			literal int GlanceLeftLink = DIBUTTON_TPS_GLANCE_LEFT_LINK;

			/// <summary>
			/// Represents a "GlanceRight" link.
			/// </summary>
			literal int GlanceRightLink = DIBUTTON_TPS_GLANCE_RIGHT_LINK;

			/// <summary>
			/// Represents a "TurnLeft" link.
			/// </summary>
			literal int TurnLeftLink = DIBUTTON_TPS_TURN_LEFT_LINK;

			/// <summary>
			/// Represents a "TurnRight" link.
			/// </summary>
			literal int TurnRightLink = DIBUTTON_TPS_TURN_RIGHT_LINK;

			/// <summary>
			/// Represents a "Inventory" action.
			/// </summary>
			literal int Inventory = DIBUTTON_TPS_INVENTORY;

			/// <summary>
			/// Represents a "StepLeft" action.
			/// </summary>
			literal int StepLeft = DIBUTTON_TPS_STEPLEFT;

			/// <summary>
			/// Represents a "StepRight" action.
			/// </summary>
			literal int StepRight = DIBUTTON_TPS_STEPRIGHT;

			/// <summary>
			/// Represents a "View" action.
			/// </summary>
			literal int View = DIBUTTON_TPS_VIEW;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_TPS_PAUSE;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_TPS_GLANCE;
		};
	}
}