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
		/// Contains action mapping constants for platform arcade games.
		/// </summary>
		public ref class PlatformControls sealed
		{
		private:
			PlatformControls() { }

		public:
			/// <summary>
			/// Represents the platform genre.
			/// </summary>
			literal int PlatformGenre = DIVIRTUAL_ARCADE_PLATFORM;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_ARCADEP_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_ARCADEP_MOVE;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_ARCADEP_JUMP;

			/// <summary>
			/// Represents a "Fire" action.
			/// </summary>
			literal int Fire = DIBUTTON_ARCADEP_FIRE;

			/// <summary>
			/// Represents a "Crouch" action.
			/// </summary>
			literal int Crouch = DIBUTTON_ARCADEP_CROUCH;

			/// <summary>
			/// Represents a "Special" action.
			/// </summary>
			literal int Special = DIBUTTON_ARCADEP_SPECIAL;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_ARCADEP_SELECT;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_ARCADEP_MENU;

			/// <summary>
			/// Represents a "View" action.
			/// </summary>
			literal int View = DIHATSWITCH_ARCADEP_VIEW;

			/// <summary>
			/// Represents a "FireSecondary" action.
			/// </summary>
			literal int FireSecondary = DIBUTTON_ARCADEP_FIRESECONDARY;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_ARCADEP_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_ARCADEP_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_ARCADEP_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_ARCADEP_BACK_LINK;

			/// <summary>
			/// Represents a "ViewUp" link.
			/// </summary>
			literal int ViewUpLink = DIBUTTON_ARCADEP_VIEW_UP_LINK;

			/// <summary>
			/// Represents a "ViewDown" link.
			/// </summary>
			literal int ViewDownLink = DIBUTTON_ARCADEP_VIEW_DOWN_LINK;

			/// <summary>
			/// Represents a "ViewLeft" link.
			/// </summary>
			literal int ViewLeftLink = DIBUTTON_ARCADEP_VIEW_LEFT_LINK;

			/// <summary>
			/// Represents a "ViewRight" link.
			/// </summary>
			literal int ViewRightLink = DIBUTTON_ARCADEP_VIEW_RIGHT_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_ARCADEP_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_ARCADEP_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for side-to-side arcade games.
		/// </summary>
		public ref class SideToSideControls sealed
		{
		private:
			SideToSideControls() { }

		public:
			/// <summary>
			/// Represents the side-to-side genre.
			/// </summary>
			literal int SideToSideGenre = DIVIRTUAL_ARCADE_SIDE2SIDE;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_ARCADES_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_ARCADES_MOVE;

			/// <summary>
			/// Represents a "Throw" action.
			/// </summary>
			literal int Throw = DIBUTTON_ARCADES_THROW;

			/// <summary>
			/// Represents a "Carry" action.
			/// </summary>
			literal int Carry = DIBUTTON_ARCADES_CARRY;

			/// <summary>
			/// Represents a "Attack" action.
			/// </summary>
			literal int Attack = DIBUTTON_ARCADES_ATTACK;

			/// <summary>
			/// Represents a "Special" action.
			/// </summary>
			literal int Special = DIBUTTON_ARCADES_SPECIAL;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_ARCADES_SELECT;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_ARCADES_MENU;

			/// <summary>
			/// Represents a "View" action.
			/// </summary>
			literal int View = DIHATSWITCH_ARCADES_VIEW;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_ARCADES_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_ARCADES_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_ARCADES_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_ARCADES_BACK_LINK;

			/// <summary>
			/// Represents a "ViewUp" link.
			/// </summary>
			literal int ViewUpLink = DIBUTTON_ARCADES_VIEW_UP_LINK;

			/// <summary>
			/// Represents a "ViewDown" link.
			/// </summary>
			literal int ViewDownLink = DIBUTTON_ARCADES_VIEW_DOWN_LINK;

			/// <summary>
			/// Represents a "ViewLeft" link.
			/// </summary>
			literal int ViewLeftLink = DIBUTTON_ARCADES_VIEW_LEFT_LINK;

			/// <summary>
			/// Represents a "ViewRight" link.
			/// </summary>
			literal int ViewRightLink = DIBUTTON_ARCADES_VIEW_RIGHT_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_ARCADES_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_ARCADES_PAUSE;
		};
	}
}
