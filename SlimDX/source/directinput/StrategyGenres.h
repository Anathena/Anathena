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
		/// Contains action mapping constants for role playing games.
		/// </summary>
		public ref class RolePlayingControls sealed
		{
		private:
			RolePlayingControls() { }

		public:
			/// <summary>
			/// Represents the role playing genre.
			/// </summary>
			literal int RolePlayingGenre = DIVIRTUAL_STRATEGY_ROLEPLAYING;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_STRATEGYR_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_STRATEGYR_MOVE;

			/// <summary>
			/// Represents a "Get" action.
			/// </summary>
			literal int Get = DIBUTTON_STRATEGYR_GET;

			/// <summary>
			/// Represents a "Apply" action.
			/// </summary>
			literal int Apply = DIBUTTON_STRATEGYR_APPLY;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_STRATEGYR_SELECT;

			/// <summary>
			/// Represents a "Attack" action.
			/// </summary>
			literal int Attack = DIBUTTON_STRATEGYR_ATTACK;

			/// <summary>
			/// Represents a "Cast" action.
			/// </summary>
			literal int Cast = DIBUTTON_STRATEGYR_CAST;

			/// <summary>
			/// Represents a "Crouch" action.
			/// </summary>
			literal int Crouch = DIBUTTON_STRATEGYR_CROUCH;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_STRATEGYR_JUMP;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_STRATEGYR_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_STRATEGYR_GLANCE;

			/// <summary>
			/// Represents a "Map" action.
			/// </summary>
			literal int Map = DIBUTTON_STRATEGYR_MAP;

			/// <summary>
			/// Represents a "Display" action.
			/// </summary>
			literal int Display = DIBUTTON_STRATEGYR_DISPLAY;

			/// <summary>
			/// Represents a "Rotate" action.
			/// </summary>
			literal int Rotate = DIAXIS_STRATEGYR_ROTATE;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_STRATEGYR_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_STRATEGYR_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_STRATEGYR_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_STRATEGYR_BACK_LINK;

			/// <summary>
			/// Represents a "RotateLeft" link.
			/// </summary>
			literal int RotateLeftLink = DIBUTTON_STRATEGYR_ROTATE_LEFT_LINK;

			/// <summary>
			/// Represents a "RotateRight" link.
			/// </summary>
			literal int RotateRightLink = DIBUTTON_STRATEGYR_ROTATE_RIGHT_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_STRATEGYR_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_STRATEGYR_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for turn based games.
		/// </summary>
		public ref class TurnBasedControls sealed
		{
		private:
			TurnBasedControls() { }

		public:
			/// <summary>
			/// Represents the turn based genre.
			/// </summary>
			literal int TurnBasedGenre = DIVIRTUAL_STRATEGY_TURN;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_STRATEGYT_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_STRATEGYT_MOVE;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_STRATEGYT_SELECT;

			/// <summary>
			/// Represents a "Instruct" action.
			/// </summary>
			literal int Instruct = DIBUTTON_STRATEGYT_INSTRUCT;

			/// <summary>
			/// Represents a "Apply" action.
			/// </summary>
			literal int Apply = DIBUTTON_STRATEGYT_APPLY;

			/// <summary>
			/// Represents a "Team" action.
			/// </summary>
			literal int Team = DIBUTTON_STRATEGYT_TEAM;

			/// <summary>
			/// Represents a "Turn" action.
			/// </summary>
			literal int Turn = DIBUTTON_STRATEGYT_TURN;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_STRATEGYT_MENU;

			/// <summary>
			/// Represents a "Zoom" action.
			/// </summary>
			literal int Zoom = DIBUTTON_STRATEGYT_ZOOM;

			/// <summary>
			/// Represents a "Map" action.
			/// </summary>
			literal int Map = DIBUTTON_STRATEGYT_MAP;

			/// <summary>
			/// Represents a "Display" action.
			/// </summary>
			literal int Display = DIBUTTON_STRATEGYT_DISPLAY;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_STRATEGYT_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_STRATEGYT_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_STRATEGYT_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_STRATEGYT_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_STRATEGYT_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_STRATEGYT_PAUSE;
		};
	}
}
