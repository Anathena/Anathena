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
		/// Contains action mapping constants for baseball batting games.
		/// </summary>
		public ref class BaseballBattingControls sealed
		{
		private:
			BaseballBattingControls() { }

		public:
			/// <summary>
			/// Represents the baseball batting genre.
			/// </summary>
			literal int BaseballBattingGenre = DIVIRTUAL_SPORTS_BASEBALL_BAT;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_BASEBALLB_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_BASEBALLB_MOVE;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_BASEBALLB_SELECT;

			/// <summary>
			/// Represents a "Normal" action.
			/// </summary>
			literal int Normal = DIBUTTON_BASEBALLB_NORMAL;

			/// <summary>
			/// Represents a "Power" action.
			/// </summary>
			literal int Power = DIBUTTON_BASEBALLB_POWER;

			/// <summary>
			/// Represents a "Bunt" action.
			/// </summary>
			literal int Bunt = DIBUTTON_BASEBALLB_BUNT;

			/// <summary>
			/// Represents a "Steal" action.
			/// </summary>
			literal int Steal = DIBUTTON_BASEBALLB_STEAL;

			/// <summary>
			/// Represents a "Burst" action.
			/// </summary>
			literal int Burst = DIBUTTON_BASEBALLB_BURST;

			/// <summary>
			/// Represents a "Slide" action.
			/// </summary>
			literal int Slide = DIBUTTON_BASEBALLB_SLIDE;

			/// <summary>
			/// Represents a "Contact" action.
			/// </summary>
			literal int Contact = DIBUTTON_BASEBALLB_CONTACT;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_BASEBALLB_MENU;

			/// <summary>
			/// Represents a "NoSteal" action.
			/// </summary>
			literal int NoSteal = DIBUTTON_BASEBALLB_NOSTEAL;

			/// <summary>
			/// Represents a "Box" action.
			/// </summary>
			literal int Box = DIBUTTON_BASEBALLB_BOX;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_BASEBALLB_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_BASEBALLB_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_BASEBALLB_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_BASEBALLB_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_BASEBALLB_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_BASEBALLB_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for baseball fielding games.
		/// </summary>
		public ref class BaseballFieldingControls sealed
		{
		private:
			BaseballFieldingControls() { }

		public:
			/// <summary>
			/// Represents the baseball fielding genre.
			/// </summary>
			literal int BaseballFieldingGenre = DIVIRTUAL_SPORTS_BASEBALL_FIELD;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_BASEBALLF_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_BASEBALLF_MOVE;

			/// <summary>
			/// Represents a "Nearest" action.
			/// </summary>
			literal int Nearest = DIBUTTON_BASEBALLF_NEAREST;

			/// <summary>
			/// Represents a "Throw1" action.
			/// </summary>
			literal int Throw1 = DIBUTTON_BASEBALLF_THROW1;

			/// <summary>
			/// Represents a "Throw2" action.
			/// </summary>
			literal int Throw2 = DIBUTTON_BASEBALLF_THROW2;

			/// <summary>
			/// Represents a "Burst" action.
			/// </summary>
			literal int Burst = DIBUTTON_BASEBALLF_BURST;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_BASEBALLF_JUMP;

			/// <summary>
			/// Represents a "Dive" action.
			/// </summary>
			literal int Dive = DIBUTTON_BASEBALLF_DIVE;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_BASEBALLF_MENU;

			/// <summary>
			/// Represents a "ShiftIn" action.
			/// </summary>
			literal int ShiftIn = DIBUTTON_BASEBALLF_SHIFTIN;

			/// <summary>
			/// Represents a "ShiftOut" action.
			/// </summary>
			literal int ShiftOut = DIBUTTON_BASEBALLF_SHIFTOUT;

			/// <summary>
			/// Represents a "AimLeft" link.
			/// </summary>
			literal int AimLeftLink = DIBUTTON_BASEBALLF_AIM_LEFT_LINK;

			/// <summary>
			/// Represents a "AimRight" link.
			/// </summary>
			literal int AimRightLink = DIBUTTON_BASEBALLF_AIM_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_BASEBALLF_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_BASEBALLF_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_BASEBALLF_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_BASEBALLF_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for baseball pitching games.
		/// </summary>
		public ref class BaseballPitchingControls sealed
		{
		private:
			BaseballPitchingControls() { }

		public:
			/// <summary>
			/// Represents the baseball pitching genre.
			/// </summary>
			literal int BaseballPitchingGenre = DIVIRTUAL_SPORTS_BASEBALL_PITCH;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_BASEBALLP_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_BASEBALLP_MOVE;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_BASEBALLP_SELECT;

			/// <summary>
			/// Represents a "Pitch" action.
			/// </summary>
			literal int Pitch = DIBUTTON_BASEBALLP_PITCH;

			/// <summary>
			/// Represents a "Base" action.
			/// </summary>
			literal int Base = DIBUTTON_BASEBALLP_BASE;

			/// <summary>
			/// Represents a "Throw" action.
			/// </summary>
			literal int Throw = DIBUTTON_BASEBALLP_THROW;

			/// <summary>
			/// Represents a "Fake" action.
			/// </summary>
			literal int Fake = DIBUTTON_BASEBALLP_FAKE;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_BASEBALLP_MENU;

			/// <summary>
			/// Represents a "Walk" action.
			/// </summary>
			literal int Walk = DIBUTTON_BASEBALLP_WALK;

			/// <summary>
			/// Represents a "Look" action.
			/// </summary>
			literal int Look = DIBUTTON_BASEBALLP_LOOK;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_BASEBALLP_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_BASEBALLP_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_BASEBALLP_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_BASEBALLP_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_BASEBALLP_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_BASEBALLP_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for bsketball defense games.
		/// </summary>
		public ref class BasketballDefenseControls sealed
		{
		private:
			BasketballDefenseControls() { }

		public:
			/// <summary>
			/// Represents the basketball defense genre.
			/// </summary>
			literal int BasketballDefenseGenre = DIVIRTUAL_SPORTS_BASKETBALL_DEFENSE;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_BBALLD_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_BBALLD_MOVE;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_BBALLD_JUMP;

			/// <summary>
			/// Represents a "Steal" action.
			/// </summary>
			literal int Steal = DIBUTTON_BBALLD_STEAL;

			/// <summary>
			/// Represents a "Fake" action.
			/// </summary>
			literal int Fake = DIBUTTON_BBALLD_FAKE;

			/// <summary>
			/// Represents a "Special" action.
			/// </summary>
			literal int Special = DIBUTTON_BBALLD_SPECIAL;

			/// <summary>
			/// Represents a "Player" action.
			/// </summary>
			literal int Player = DIBUTTON_BBALLD_PLAYER;

			/// <summary>
			/// Represents a "Burst" action.
			/// </summary>
			literal int Burst = DIBUTTON_BBALLD_BURST;

			/// <summary>
			/// Represents a "Play" action.
			/// </summary>
			literal int Play = DIBUTTON_BBALLD_PLAY;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_BBALLD_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_BBALLD_GLANCE;

			/// <summary>
			/// Represents a "Timeout" action.
			/// </summary>
			literal int Timeout = DIBUTTON_BBALLD_TIMEOUT;

			/// <summary>
			/// Represents a "Substitute" action.
			/// </summary>
			literal int Substitute = DIBUTTON_BBALLD_SUBSTITUTE;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_BBALLD_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_BBALLD_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_BBALLD_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_BBALLD_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_BBALLD_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_BBALLD_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for basketball offense games.
		/// </summary>
		public ref class BasketballOffenseControls sealed
		{
		private:
			BasketballOffenseControls() { }

		public:
			/// <summary>
			/// Represents the basketball offense genre.
			/// </summary>
			literal int BasketballOffenseGenre = DIVIRTUAL_SPORTS_BASKETBALL_OFFENSE;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_BBALLO_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_BBALLO_MOVE;

			/// <summary>
			/// Represents a "Shoot" action.
			/// </summary>
			literal int Shoot = DIBUTTON_BBALLO_SHOOT;

			/// <summary>
			/// Represents a "Dunk" action.
			/// </summary>
			literal int Dunk = DIBUTTON_BBALLO_DUNK;

			/// <summary>
			/// Represents a "Pass" action.
			/// </summary>
			literal int Pass = DIBUTTON_BBALLO_PASS;

			/// <summary>
			/// Represents a "Fake" action.
			/// </summary>
			literal int Fake = DIBUTTON_BBALLO_FAKE;

			/// <summary>
			/// Represents a "Special" action.
			/// </summary>
			literal int Special = DIBUTTON_BBALLO_SPECIAL;

			/// <summary>
			/// Represents a "Player" action.
			/// </summary>
			literal int Player = DIBUTTON_BBALLO_PLAYER;

			/// <summary>
			/// Represents a "Burst" action.
			/// </summary>
			literal int Burst = DIBUTTON_BBALLO_BURST;

			/// <summary>
			/// Represents a "Call" action.
			/// </summary>
			literal int Call = DIBUTTON_BBALLO_CALL;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_BBALLO_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_BBALLO_GLANCE;

			/// <summary>
			/// Represents a "Screen" action.
			/// </summary>
			literal int Screen = DIBUTTON_BBALLO_SCREEN;

			/// <summary>
			/// Represents a "Play" action.
			/// </summary>
			literal int Play = DIBUTTON_BBALLO_PLAY;

			/// <summary>
			/// Represents a "Jab" action.
			/// </summary>
			literal int Jab = DIBUTTON_BBALLO_JAB;

			/// <summary>
			/// Represents a "Post" action.
			/// </summary>
			literal int Post = DIBUTTON_BBALLO_POST;

			/// <summary>
			/// Represents a "Timeout" action.
			/// </summary>
			literal int Timeout = DIBUTTON_BBALLO_TIMEOUT;

			/// <summary>
			/// Represents a "Substitute" action.
			/// </summary>
			literal int Substitute = DIBUTTON_BBALLO_SUBSTITUTE;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_BBALLO_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_BBALLO_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_BBALLO_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_BBALLO_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_BBALLO_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_BBALLO_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for fishing games.
		/// </summary>
		public ref class FishingControls sealed
		{
		private:
			FishingControls() { }

		public:
			/// <summary>
			/// Represents the fishing genre.
			/// </summary>
			literal int FishingGenre = DIVIRTUAL_SPORTS_FISHING;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_FISHING_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_FISHING_MOVE;

			/// <summary>
			/// Represents a "Cast" action.
			/// </summary>
			literal int Cast = DIBUTTON_FISHING_CAST;

			/// <summary>
			/// Represents a "Type" action.
			/// </summary>
			literal int Type = DIBUTTON_FISHING_TYPE;

			/// <summary>
			/// Represents a "Binocular" action.
			/// </summary>
			literal int Binocular = DIBUTTON_FISHING_BINOCULAR;

			/// <summary>
			/// Represents a "Bait" action.
			/// </summary>
			literal int Bait = DIBUTTON_FISHING_BAIT;

			/// <summary>
			/// Represents a "Map" action.
			/// </summary>
			literal int Map = DIBUTTON_FISHING_MAP;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_FISHING_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_FISHING_GLANCE;

			/// <summary>
			/// Represents a "Display" action.
			/// </summary>
			literal int Display = DIBUTTON_FISHING_DISPLAY;

			/// <summary>
			/// Represents a "Rotate" action.
			/// </summary>
			literal int Rotate = DIAXIS_FISHING_ROTATE;

			/// <summary>
			/// Represents a "Crouch" action.
			/// </summary>
			literal int Crouch = DIBUTTON_FISHING_CROUCH;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_FISHING_JUMP;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_FISHING_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_FISHING_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_FISHING_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_FISHING_BACK_LINK;

			/// <summary>
			/// Represents a "RotateLeft" link.
			/// </summary>
			literal int RotateLeftLink = DIBUTTON_FISHING_ROTATE_LEFT_LINK;

			/// <summary>
			/// Represents a "RotateRight" link.
			/// </summary>
			literal int RotateRightLink = DIBUTTON_FISHING_ROTATE_RIGHT_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_FISHING_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_FISHING_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for football defense games.
		/// </summary>
		public ref class FootballDefenseControls sealed
		{
		private:
			FootballDefenseControls() { }

		public:
			/// <summary>
			/// Represents the football defense genre.
			/// </summary>
			literal int FootballDefenseGenre = DIVIRTUAL_SPORTS_FOOTBALL_DEFENSE;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_FOOTBALLD_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_FOOTBALLD_MOVE;

			/// <summary>
			/// Represents a "Play" action.
			/// </summary>
			literal int Play = DIBUTTON_FOOTBALLD_PLAY;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_FOOTBALLD_SELECT;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_FOOTBALLD_JUMP;

			/// <summary>
			/// Represents a "Tackle" action.
			/// </summary>
			literal int Tackle = DIBUTTON_FOOTBALLD_TACKLE;

			/// <summary>
			/// Represents a "Fake" action.
			/// </summary>
			literal int Fake = DIBUTTON_FOOTBALLD_FAKE;

			/// <summary>
			/// Represents a "SuperTackle" action.
			/// </summary>
			literal int SuperTackle = DIBUTTON_FOOTBALLD_SUPERTACKLE;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_FOOTBALLD_MENU;

			/// <summary>
			/// Represents a "Spin" action.
			/// </summary>
			literal int Spin = DIBUTTON_FOOTBALLD_SPIN;

			/// <summary>
			/// Represents a "Swim" action.
			/// </summary>
			literal int Swim = DIBUTTON_FOOTBALLD_SWIM;

			/// <summary>
			/// Represents a "BullRush" action.
			/// </summary>
			literal int BullRush = DIBUTTON_FOOTBALLD_BULLRUSH;

			/// <summary>
			/// Represents a "Rip" action.
			/// </summary>
			literal int Rip = DIBUTTON_FOOTBALLD_RIP;

			/// <summary>
			/// Represents a "Audible" action.
			/// </summary>
			literal int Audible = DIBUTTON_FOOTBALLD_AUDIBLE;

			/// <summary>
			/// Represents a "Zoom" action.
			/// </summary>
			literal int Zoom = DIBUTTON_FOOTBALLD_ZOOM;

			/// <summary>
			/// Represents a "Substitute" action.
			/// </summary>
			literal int Substitute = DIBUTTON_FOOTBALLD_SUBSTITUTE;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_FOOTBALLD_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_FOOTBALLD_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_FOOTBALLD_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_FOOTBALLD_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_FOOTBALLD_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_FOOTBALLD_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for football offense games.
		/// </summary>
		public ref class FootballOffenseControls sealed
		{
		private:
			FootballOffenseControls() { }

		public:
			/// <summary>
			/// Represents the football offense genre.
			/// </summary>
			literal int FootballOffenseGenre = DIVIRTUAL_SPORTS_FOOTBALL_OFFENSE;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_FOOTBALLO_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_FOOTBALLO_MOVE;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_FOOTBALLO_JUMP;

			/// <summary>
			/// Represents a "LeftArm" action.
			/// </summary>
			literal int LeftArm = DIBUTTON_FOOTBALLO_LEFTARM;

			/// <summary>
			/// Represents a "RightArm" action.
			/// </summary>
			literal int RightArm = DIBUTTON_FOOTBALLO_RIGHTARM;

			/// <summary>
			/// Represents a "Throw" action.
			/// </summary>
			literal int Throw = DIBUTTON_FOOTBALLO_THROW;

			/// <summary>
			/// Represents a "Spin" action.
			/// </summary>
			literal int Spin = DIBUTTON_FOOTBALLO_SPIN;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_FOOTBALLO_MENU;

			/// <summary>
			/// Represents a "Juke" action.
			/// </summary>
			literal int Juke = DIBUTTON_FOOTBALLO_JUKE;

			/// <summary>
			/// Represents a "Shoulder" action.
			/// </summary>
			literal int Shoulder = DIBUTTON_FOOTBALLO_SHOULDER;

			/// <summary>
			/// Represents a "Turbo" action.
			/// </summary>
			literal int Turbo = DIBUTTON_FOOTBALLO_TURBO;

			/// <summary>
			/// Represents a "Dive" action.
			/// </summary>
			literal int Dive = DIBUTTON_FOOTBALLO_DIVE;

			/// <summary>
			/// Represents a "Zoom" action.
			/// </summary>
			literal int Zoom = DIBUTTON_FOOTBALLO_ZOOM;

			/// <summary>
			/// Represents a "Substitute" action.
			/// </summary>
			literal int Substitute = DIBUTTON_FOOTBALLO_SUBSTITUTE;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_FOOTBALLO_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_FOOTBALLO_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_FOOTBALLO_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_FOOTBALLO_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_FOOTBALLO_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_FOOTBALLO_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for football games.
		/// </summary>
		public ref class FootballPlayControls sealed
		{
		private:
			FootballPlayControls() { }

		public:
			/// <summary>
			/// Represents the football genre.
			/// </summary>
			literal int FootballPlayGenre = DIVIRTUAL_SPORTS_FOOTBALL_FIELD;

			/// <summary>
			/// Represents a "Play" action.
			/// </summary>
			literal int Play = DIBUTTON_FOOTBALLP_PLAY;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_FOOTBALLP_SELECT;

			/// <summary>
			/// Represents a "Help" action.
			/// </summary>
			literal int Help = DIBUTTON_FOOTBALLP_HELP;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_FOOTBALLP_MENU;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_FOOTBALLP_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_FOOTBALLP_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for football quarterback games.
		/// </summary>
		public ref class FootballQuarterbackControls sealed
		{
		private:
			FootballQuarterbackControls() { }

		public:
			/// <summary>
			/// Represents the football quarterback genre.
			/// </summary>
			literal int FootballQuarterbackGenre = DIVIRTUAL_SPORTS_FOOTBALL_QBCK;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_FOOTBALLQ_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_FOOTBALLQ_MOVE;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_FOOTBALLQ_SELECT;

			/// <summary>
			/// Represents a "Snap" action.
			/// </summary>
			literal int Snap = DIBUTTON_FOOTBALLQ_SNAP;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_FOOTBALLQ_JUMP;

			/// <summary>
			/// Represents a "Slide" action.
			/// </summary>
			literal int Slide = DIBUTTON_FOOTBALLQ_SLIDE;

			/// <summary>
			/// Represents a "Pass" action.
			/// </summary>
			literal int Pass = DIBUTTON_FOOTBALLQ_PASS;

			/// <summary>
			/// Represents a "Fake" action.
			/// </summary>
			literal int Fake = DIBUTTON_FOOTBALLQ_FAKE;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_FOOTBALLQ_MENU;

			/// <summary>
			/// Represents a "FakeSnap" action.
			/// </summary>
			literal int FakeSnap = DIBUTTON_FOOTBALLQ_FAKESNAP;

			/// <summary>
			/// Represents a "Motion" action.
			/// </summary>
			literal int Motion = DIBUTTON_FOOTBALLQ_MOTION;

			/// <summary>
			/// Represents a "Audible" action.
			/// </summary>
			literal int Audible = DIBUTTON_FOOTBALLQ_AUDIBLE;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_FOOTBALLQ_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_FOOTBALLQ_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_FOOTBALLQ_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_FOOTBALLQ_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_FOOTBALLQ_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_FOOTBALLQ_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for golf games.
		/// </summary>
		public ref class GolfControls sealed
		{
		private:
			GolfControls() { }

		public:
			/// <summary>
			/// Represents the golf genre.
			/// </summary>
			literal int GolfGenre = DIVIRTUAL_SPORTS_GOLF;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_GOLF_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_GOLF_MOVE;

			/// <summary>
			/// Represents a "Swing" action.
			/// </summary>
			literal int Swing = DIBUTTON_GOLF_SWING;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_GOLF_SELECT;

			/// <summary>
			/// Represents a "Up" action.
			/// </summary>
			literal int Up = DIBUTTON_GOLF_UP;

			/// <summary>
			/// Represents a "Down" action.
			/// </summary>
			literal int Down = DIBUTTON_GOLF_DOWN;

			/// <summary>
			/// Represents a "Terrain" action.
			/// </summary>
			literal int Terrain = DIBUTTON_GOLF_TERRAIN;

			/// <summary>
			/// Represents a "Flyby" action.
			/// </summary>
			literal int Flyby = DIBUTTON_GOLF_FLYBY;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_GOLF_MENU;

			/// <summary>
			/// Represents a "Scroll" action.
			/// </summary>
			literal int Scroll = DIHATSWITCH_GOLF_SCROLL;

			/// <summary>
			/// Represents a "Zoom" action.
			/// </summary>
			literal int Zoom = DIBUTTON_GOLF_ZOOM;

			/// <summary>
			/// Represents a "Timeout" action.
			/// </summary>
			literal int Timeout = DIBUTTON_GOLF_TIMEOUT;

			/// <summary>
			/// Represents a "Substitute" action.
			/// </summary>
			literal int Substitute = DIBUTTON_GOLF_SUBSTITUTE;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_GOLF_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_GOLF_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_GOLF_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_GOLF_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_GOLF_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_GOLF_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for hockey defense games.
		/// </summary>
		public ref class HockeyDefenseControls sealed
		{
		private:
			HockeyDefenseControls() { }

		public:
			/// <summary>
			/// Represents the hockey defense genre.
			/// </summary>
			literal int HockeyDefenseGenre = DIVIRTUAL_SPORTS_HOCKEY_DEFENSE;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_HOCKEYD_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_HOCKEYD_MOVE;

			/// <summary>
			/// Represents a "Player" action.
			/// </summary>
			literal int Player = DIBUTTON_HOCKEYD_PLAYER;

			/// <summary>
			/// Represents a "Steal" action.
			/// </summary>
			literal int Steal = DIBUTTON_HOCKEYD_STEAL;

			/// <summary>
			/// Represents a "Burst" action.
			/// </summary>
			literal int Burst = DIBUTTON_HOCKEYD_BURST;

			/// <summary>
			/// Represents a "Block" action.
			/// </summary>
			literal int Block = DIBUTTON_HOCKEYD_BLOCK;

			/// <summary>
			/// Represents a "Fake" action.
			/// </summary>
			literal int Fake = DIBUTTON_HOCKEYD_FAKE;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_HOCKEYD_MENU;

			/// <summary>
			/// Represents a "Scroll" action.
			/// </summary>
			literal int Scroll = DIHATSWITCH_HOCKEYD_SCROLL;

			/// <summary>
			/// Represents a "Zoom" action.
			/// </summary>
			literal int Zoom = DIBUTTON_HOCKEYD_ZOOM;

			/// <summary>
			/// Represents a "Strategy" action.
			/// </summary>
			literal int Strategy = DIBUTTON_HOCKEYD_STRATEGY;

			/// <summary>
			/// Represents a "Timeout" action.
			/// </summary>
			literal int Timeout = DIBUTTON_HOCKEYD_TIMEOUT;

			/// <summary>
			/// Represents a "Substitute" action.
			/// </summary>
			literal int Substitute = DIBUTTON_HOCKEYD_SUBSTITUTE;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_HOCKEYD_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_HOCKEYD_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_HOCKEYD_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_HOCKEYD_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_HOCKEYD_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_HOCKEYD_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for hockey goalie games.
		/// </summary>
		public ref class HockeyGoalieControls sealed
		{
		private:
			HockeyGoalieControls() { }

		public:
			/// <summary>
			/// Represents the hockey goalie genre.
			/// </summary>
			literal int HockeyGoalieGenre = DIVIRTUAL_SPORTS_HOCKEY_GOALIE;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_HOCKEYG_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_HOCKEYG_MOVE;

			/// <summary>
			/// Represents a "Pass" action.
			/// </summary>
			literal int Pass = DIBUTTON_HOCKEYG_PASS;

			/// <summary>
			/// Represents a "Poke" action.
			/// </summary>
			literal int Poke = DIBUTTON_HOCKEYG_POKE;

			/// <summary>
			/// Represents a "Steal" action.
			/// </summary>
			literal int Steal = DIBUTTON_HOCKEYG_STEAL;

			/// <summary>
			/// Represents a "Block" action.
			/// </summary>
			literal int Block = DIBUTTON_HOCKEYG_BLOCK;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_HOCKEYG_MENU;

			/// <summary>
			/// Represents a "Scroll" action.
			/// </summary>
			literal int Scroll = DIHATSWITCH_HOCKEYG_SCROLL;

			/// <summary>
			/// Represents a "Zoom" action.
			/// </summary>
			literal int Zoom = DIBUTTON_HOCKEYG_ZOOM;

			/// <summary>
			/// Represents a "Strategy" action.
			/// </summary>
			literal int Strategy = DIBUTTON_HOCKEYG_STRATEGY;

			/// <summary>
			/// Represents a "Timeout" action.
			/// </summary>
			literal int Timeout = DIBUTTON_HOCKEYG_TIMEOUT;

			/// <summary>
			/// Represents a "Substitute" action.
			/// </summary>
			literal int Substitute = DIBUTTON_HOCKEYG_SUBSTITUTE;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_HOCKEYG_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_HOCKEYG_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_HOCKEYG_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_HOCKEYG_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_HOCKEYG_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_HOCKEYG_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for hockey offense games.
		/// </summary>
		public ref class HockeyOffenseControls sealed
		{
		private:
			HockeyOffenseControls() { }

		public:
			/// <summary>
			/// Represents the hockey offense genre.
			/// </summary>
			literal int HockeyOffenseGenre = DIVIRTUAL_SPORTS_HOCKEY_OFFENSE;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_HOCKEYO_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_HOCKEYO_MOVE;

			/// <summary>
			/// Represents a "Shoot" action.
			/// </summary>
			literal int Shoot = DIBUTTON_HOCKEYO_SHOOT;

			/// <summary>
			/// Represents a "Pass" action.
			/// </summary>
			literal int Pass = DIBUTTON_HOCKEYO_PASS;

			/// <summary>
			/// Represents a "Burst" action.
			/// </summary>
			literal int Burst = DIBUTTON_HOCKEYO_BURST;

			/// <summary>
			/// Represents a "Special" action.
			/// </summary>
			literal int Special = DIBUTTON_HOCKEYO_SPECIAL;

			/// <summary>
			/// Represents a "Fake" action.
			/// </summary>
			literal int Fake = DIBUTTON_HOCKEYO_FAKE;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_HOCKEYO_MENU;

			/// <summary>
			/// Represents a "Scroll" action.
			/// </summary>
			literal int Scroll = DIHATSWITCH_HOCKEYO_SCROLL;

			/// <summary>
			/// Represents a "Zoom" action.
			/// </summary>
			literal int Zoom = DIBUTTON_HOCKEYO_ZOOM;

			/// <summary>
			/// Represents a "Strategy" action.
			/// </summary>
			literal int Strategy = DIBUTTON_HOCKEYO_STRATEGY;

			/// <summary>
			/// Represents a "Timeout" action.
			/// </summary>
			literal int Timeout = DIBUTTON_HOCKEYO_TIMEOUT;

			/// <summary>
			/// Represents a "Substitute" action.
			/// </summary>
			literal int Substitute = DIBUTTON_HOCKEYO_SUBSTITUTE;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_HOCKEYO_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_HOCKEYO_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_HOCKEYO_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_HOCKEYO_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_HOCKEYO_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_HOCKEYO_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for hunting games.
		/// </summary>
		public ref class HuntingControls sealed
		{
		private:
			HuntingControls() { }

		public:
			/// <summary>
			/// Represents the hunting genre.
			/// </summary>
			literal int HuntingGenre = DIVIRTUAL_SPORTS_HUNTING;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_HUNTING_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_HUNTING_MOVE;

			/// <summary>
			/// Represents a "Fire" action.
			/// </summary>
			literal int Fire = DIBUTTON_HUNTING_FIRE;

			/// <summary>
			/// Represents a "Aim" action.
			/// </summary>
			literal int Aim = DIBUTTON_HUNTING_AIM;

			/// <summary>
			/// Represents a "Weapon" action.
			/// </summary>
			literal int Weapon = DIBUTTON_HUNTING_WEAPON;

			/// <summary>
			/// Represents a "Binocular" action.
			/// </summary>
			literal int Binocular = DIBUTTON_HUNTING_BINOCULAR;

			/// <summary>
			/// Represents a "Call" action.
			/// </summary>
			literal int Call = DIBUTTON_HUNTING_CALL;

			/// <summary>
			/// Represents a "Map" action.
			/// </summary>
			literal int Map = DIBUTTON_HUNTING_MAP;

			/// <summary>
			/// Represents a "Special" action.
			/// </summary>
			literal int Special = DIBUTTON_HUNTING_SPECIAL;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_HUNTING_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_HUNTING_GLANCE;

			/// <summary>
			/// Represents a "Display" action.
			/// </summary>
			literal int Display = DIBUTTON_HUNTING_DISPLAY;

			/// <summary>
			/// Represents a "Rotate" action.
			/// </summary>
			literal int Rotate = DIAXIS_HUNTING_ROTATE;

			/// <summary>
			/// Represents a "Crouch" action.
			/// </summary>
			literal int Crouch = DIBUTTON_HUNTING_CROUCH;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_HUNTING_JUMP;

			/// <summary>
			/// Represents a "FireSecondary" action.
			/// </summary>
			literal int FireSecondary = DIBUTTON_HUNTING_FIRESECONDARY;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_HUNTING_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_HUNTING_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_HUNTING_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_HUNTING_BACK_LINK;

			/// <summary>
			/// Represents a "RotateLeft" link.
			/// </summary>
			literal int RotateLeftLink = DIBUTTON_HUNTING_ROTATE_LEFT_LINK;

			/// <summary>
			/// Represents a "RotateRight" link.
			/// </summary>
			literal int RotateRightLink = DIBUTTON_HUNTING_ROTATE_RIGHT_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_HUNTING_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_HUNTING_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for mountain biking games.
		/// </summary>
		public ref class MountainBikingControls sealed
		{
		private:
			MountainBikingControls() { }

		public:
			/// <summary>
			/// Represents the mountain biking genre.
			/// </summary>
			literal int MountainBikingGenre = DIVIRTUAL_SPORTS_BIKING_MOUNTAIN;

			/// <summary>
			/// Represents a "Turn" action.
			/// </summary>
			literal int Turn = DIAXIS_BIKINGM_TURN;

			/// <summary>
			/// Represents a "Pedal" action.
			/// </summary>
			literal int Pedal = DIAXIS_BIKINGM_PEDAL;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_BIKINGM_JUMP;

			/// <summary>
			/// Represents a "Camera" action.
			/// </summary>
			literal int Camera = DIBUTTON_BIKINGM_CAMERA;

			/// <summary>
			/// Represents a "Special1" action.
			/// </summary>
			literal int Special1 = DIBUTTON_BIKINGM_SPECIAL1;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_BIKINGM_SELECT;

			/// <summary>
			/// Represents a "Special2" action.
			/// </summary>
			literal int Special2 = DIBUTTON_BIKINGM_SPECIAL2;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_BIKINGM_MENU;

			/// <summary>
			/// Represents a "Scroll" action.
			/// </summary>
			literal int Scroll = DIHATSWITCH_BIKINGM_SCROLL;

			/// <summary>
			/// Represents a "Zoom" action.
			/// </summary>
			literal int Zoom = DIBUTTON_BIKINGM_ZOOM;

			/// <summary>
			/// Represents a "Brake" action.
			/// </summary>
			literal int Brake = DIAXIS_BIKINGM_BRAKE;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_BIKINGM_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_BIKINGM_RIGHT_LINK;

			/// <summary>
			/// Represents a "Faster" link.
			/// </summary>
			literal int FasterLink = DIBUTTON_BIKINGM_FASTER_LINK;

			/// <summary>
			/// Represents a "Slower" link.
			/// </summary>
			literal int SlowerLink = DIBUTTON_BIKINGM_SLOWER_LINK;

			/// <summary>
			/// Represents a "BrakeButton" link.
			/// </summary>
			literal int BrakeButtonLink = DIBUTTON_BIKINGM_BRAKE_BUTTON_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_BIKINGM_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_BIKINGM_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for racquet games.
		/// </summary>
		public ref class RacquetControls sealed
		{
		private:
			RacquetControls() { }

		public:
			/// <summary>
			/// Represents the racquet genre.
			/// </summary>
			literal int RacquetGenre = DIVIRTUAL_SPORTS_RACQUET;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_RACQUET_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_RACQUET_MOVE;

			/// <summary>
			/// Represents a "Swing" action.
			/// </summary>
			literal int Swing = DIBUTTON_RACQUET_SWING;

			/// <summary>
			/// Represents a "Backswing" action.
			/// </summary>
			literal int Backswing = DIBUTTON_RACQUET_BACKSWING;

			/// <summary>
			/// Represents a "Smash" action.
			/// </summary>
			literal int Smash = DIBUTTON_RACQUET_SMASH;

			/// <summary>
			/// Represents a "Special" action.
			/// </summary>
			literal int Special = DIBUTTON_RACQUET_SPECIAL;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_RACQUET_SELECT;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_RACQUET_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_RACQUET_GLANCE;

			/// <summary>
			/// Represents a "Timeout" action.
			/// </summary>
			literal int Timeout = DIBUTTON_RACQUET_TIMEOUT;

			/// <summary>
			/// Represents a "Substitute" action.
			/// </summary>
			literal int Substitute = DIBUTTON_RACQUET_SUBSTITUTE;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_RACQUET_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_RACQUET_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_RACQUET_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_RACQUET_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_RACQUET_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_RACQUET_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for skiing games.
		/// </summary>
		public ref class SkiingControls sealed
		{
		private:
			SkiingControls() { }

		public:
			/// <summary>
			/// Represents the skiing genre.
			/// </summary>
			literal int SkiingGenre = DIVIRTUAL_SPORTS_SKIING;

			/// <summary>
			/// Represents a "Turn" action.
			/// </summary>
			literal int Turn = DIAXIS_SKIING_TURN;

			/// <summary>
			/// Represents a "Speed" action.
			/// </summary>
			literal int Speed = DIAXIS_SKIING_SPEED;

			/// <summary>
			/// Represents a "Jump" action.
			/// </summary>
			literal int Jump = DIBUTTON_SKIING_JUMP;

			/// <summary>
			/// Represents a "Crouch" action.
			/// </summary>
			literal int Crouch = DIBUTTON_SKIING_CROUCH;

			/// <summary>
			/// Represents a "Camera" action.
			/// </summary>
			literal int Camera = DIBUTTON_SKIING_CAMERA;

			/// <summary>
			/// Represents a "Special1" action.
			/// </summary>
			literal int Special1 = DIBUTTON_SKIING_SPECIAL1;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_SKIING_SELECT;

			/// <summary>
			/// Represents a "Special2" action.
			/// </summary>
			literal int Special2 = DIBUTTON_SKIING_SPECIAL2;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_SKIING_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_SKIING_GLANCE;

			/// <summary>
			/// Represents a "Zoom" action.
			/// </summary>
			literal int Zoom = DIBUTTON_SKIING_ZOOM;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_SKIING_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_SKIING_RIGHT_LINK;

			/// <summary>
			/// Represents a "Faster" link.
			/// </summary>
			literal int FasterLink = DIBUTTON_SKIING_FASTER_LINK;

			/// <summary>
			/// Represents a "Slower" link.
			/// </summary>
			literal int SlowerLink = DIBUTTON_SKIING_SLOWER_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_SKIING_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_SKIING_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for soccer defense games.
		/// </summary>
		public ref class SoccerDefenseControls sealed
		{
		private:
			SoccerDefenseControls() { }

		public:
			/// <summary>
			/// Represents the soccer defense genre.
			/// </summary>
			literal int SoccerDefenseGenre = DIVIRTUAL_SPORTS_SOCCER_DEFENSE;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_SOCCERD_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_SOCCERD_MOVE;

			/// <summary>
			/// Represents a "Block" action.
			/// </summary>
			literal int Block = DIBUTTON_SOCCERD_BLOCK;

			/// <summary>
			/// Represents a "Steal" action.
			/// </summary>
			literal int Steal = DIBUTTON_SOCCERD_STEAL;

			/// <summary>
			/// Represents a "Fake" action.
			/// </summary>
			literal int Fake = DIBUTTON_SOCCERD_FAKE;

			/// <summary>
			/// Represents a "Player" action.
			/// </summary>
			literal int Player = DIBUTTON_SOCCERD_PLAYER;

			/// <summary>
			/// Represents a "Special" action.
			/// </summary>
			literal int Special = DIBUTTON_SOCCERD_SPECIAL;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_SOCCERD_SELECT;

			/// <summary>
			/// Represents a "Slide" action.
			/// </summary>
			literal int Slide = DIBUTTON_SOCCERD_SLIDE;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_SOCCERD_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_SOCCERD_GLANCE;

			/// <summary>
			/// Represents a "Foul" action.
			/// </summary>
			literal int Foul = DIBUTTON_SOCCERD_FOUL;

			/// <summary>
			/// Represents a "Head" action.
			/// </summary>
			literal int Head = DIBUTTON_SOCCERD_HEAD;

			/// <summary>
			/// Represents a "Clear" action.
			/// </summary>
			literal int Clear = DIBUTTON_SOCCERD_CLEAR;

			/// <summary>
			/// Represents a "GoalieCharge" action.
			/// </summary>
			literal int GoalieCharge = DIBUTTON_SOCCERD_GOALIECHARGE;

			/// <summary>
			/// Represents a "Substitute" action.
			/// </summary>
			literal int Substitute = DIBUTTON_SOCCERD_SUBSTITUTE;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_SOCCERD_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_SOCCERD_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_SOCCERD_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_SOCCERD_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_SOCCERD_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_SOCCERD_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for soccer offense games.
		/// </summary>
		public ref class SoccerOffenseControls sealed
		{
		private:
			SoccerOffenseControls() { }

		public:
			/// <summary>
			/// Represents the soccer offense genre.
			/// </summary>
			literal int SoccerOffenseGenre = DIVIRTUAL_SPORTS_SOCCER_OFFENSE;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_SOCCERO_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_SOCCERO_MOVE;

			/// <summary>
			/// Represents a "Bend" action.
			/// </summary>
			literal int Bend = DIAXIS_SOCCERO_BEND;

			/// <summary>
			/// Represents a "Shoot" action.
			/// </summary>
			literal int Shoot = DIBUTTON_SOCCERO_SHOOT;

			/// <summary>
			/// Represents a "Pass" action.
			/// </summary>
			literal int Pass = DIBUTTON_SOCCERO_PASS;

			/// <summary>
			/// Represents a "Fake" action.
			/// </summary>
			literal int Fake = DIBUTTON_SOCCERO_FAKE;

			/// <summary>
			/// Represents a "Player" action.
			/// </summary>
			literal int Player = DIBUTTON_SOCCERO_PLAYER;

			/// <summary>
			/// Represents a "Special1" action.
			/// </summary>
			literal int Special1 = DIBUTTON_SOCCERO_SPECIAL1;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_SOCCERO_SELECT;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_SOCCERO_MENU;

			/// <summary>
			/// Represents a "Glance" action.
			/// </summary>
			literal int Glance = DIHATSWITCH_SOCCERO_GLANCE;

			/// <summary>
			/// Represents a "Substitute" action.
			/// </summary>
			literal int Substitute = DIBUTTON_SOCCERO_SUBSTITUTE;

			/// <summary>
			/// Represents a "ShootLow" action.
			/// </summary>
			literal int ShootLow = DIBUTTON_SOCCERO_SHOOTLOW;

			/// <summary>
			/// Represents a "ShootHigh" action.
			/// </summary>
			literal int ShootHigh = DIBUTTON_SOCCERO_SHOOTHIGH;

			/// <summary>
			/// Represents a "PassThrough" action.
			/// </summary>
			literal int PassThrough = DIBUTTON_SOCCERO_PASSTHRU;

			/// <summary>
			/// Represents a "Sprint" action.
			/// </summary>
			literal int Sprint = DIBUTTON_SOCCERO_SPRINT;

			/// <summary>
			/// Represents a "Control" action.
			/// </summary>
			literal int Control = DIBUTTON_SOCCERO_CONTROL;

			/// <summary>
			/// Represents a "Head" action.
			/// </summary>
			literal int Head = DIBUTTON_SOCCERO_HEAD;

			/// <summary>
			/// Represents a "Left" link.
			/// </summary>
			literal int LeftLink = DIBUTTON_SOCCERO_LEFT_LINK;

			/// <summary>
			/// Represents a "Right" link.
			/// </summary>
			literal int RightLink = DIBUTTON_SOCCERO_RIGHT_LINK;

			/// <summary>
			/// Represents a "Forward" link.
			/// </summary>
			literal int ForwardLink = DIBUTTON_SOCCERO_FORWARD_LINK;

			/// <summary>
			/// Represents a "Back" link.
			/// </summary>
			literal int BackLink = DIBUTTON_SOCCERO_BACK_LINK;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_SOCCERO_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_SOCCERO_PAUSE;
		};
	}
}
