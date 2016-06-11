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
		/// Contains action mapping constants for browser controls.
		/// </summary>
		public ref class BrowserControls sealed
		{
		private:
			BrowserControls() { }

		public:
			/// <summary>
			/// Represents the browser genre.
			/// </summary>
			literal int BrowserGenre = DIVIRTUAL_BROWSER_CONTROL;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_BROWSER_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_BROWSER_MOVE;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_BROWSER_SELECT;

			/// <summary>
			/// Represents a "View" action.
			/// </summary>
			literal int View = DIAXIS_BROWSER_VIEW;

			/// <summary>
			/// Represents a "Refresh" action.
			/// </summary>
			literal int Refresh = DIBUTTON_BROWSER_REFRESH;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_BROWSER_MENU;

			/// <summary>
			/// Represents a "Search" action.
			/// </summary>
			literal int Search = DIBUTTON_BROWSER_SEARCH;

			/// <summary>
			/// Represents a "Stop" action.
			/// </summary>
			literal int Stop = DIBUTTON_BROWSER_STOP;

			/// <summary>
			/// Represents a "Home" action.
			/// </summary>
			literal int Home = DIBUTTON_BROWSER_HOME;

			/// <summary>
			/// Represents a "Favorites" action.
			/// </summary>
			literal int Favorites = DIBUTTON_BROWSER_FAVORITES;

			/// <summary>
			/// Represents a "Next" action.
			/// </summary>
			literal int Next = DIBUTTON_BROWSER_NEXT;

			/// <summary>
			/// Represents a "Previous" action.
			/// </summary>
			literal int Previous = DIBUTTON_BROWSER_PREVIOUS;

			/// <summary>
			/// Represents a "History" action.
			/// </summary>
			literal int History = DIBUTTON_BROWSER_HISTORY;

			/// <summary>
			/// Represents a "Print" action.
			/// </summary>
			literal int Print = DIBUTTON_BROWSER_PRINT;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_BROWSER_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_BROWSER_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for remote controls.
		/// </summary>
		public ref class RemoteControls sealed
		{
		private:
			RemoteControls() { }

		public:
			/// <summary>
			/// Represents the remote genre.
			/// </summary>
			literal int RemoteGenre = DIVIRTUAL_REMOTE_CONTROL;

			/// <summary>
			/// Represents a "Slider" action.
			/// </summary>
			literal int Slider = DIAXIS_REMOTE_SLIDER;

			/// <summary>
			/// Represents a "Mute" action.
			/// </summary>
			literal int Mute = DIBUTTON_REMOTE_MUTE;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_REMOTE_SELECT;

			/// <summary>
			/// Represents a "Play" action.
			/// </summary>
			literal int Play = DIBUTTON_REMOTE_PLAY;

			/// <summary>
			/// Represents a "Cue" action.
			/// </summary>
			literal int Cue = DIBUTTON_REMOTE_CUE;

			/// <summary>
			/// Represents a "Review" action.
			/// </summary>
			literal int Review = DIBUTTON_REMOTE_REVIEW;

			/// <summary>
			/// Represents a "Change" action.
			/// </summary>
			literal int Change = DIBUTTON_REMOTE_CHANGE;

			/// <summary>
			/// Represents a "Record" action.
			/// </summary>
			literal int Record = DIBUTTON_REMOTE_RECORD;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_REMOTE_MENU;

			/// <summary>
			/// Represents a "Slider2" action.
			/// </summary>
			literal int Slider2 = DIAXIS_REMOTE_SLIDER2;

			/// <summary>
			/// Represents a "TV" action.
			/// </summary>
			literal int TV = DIBUTTON_REMOTE_TV;

			/// <summary>
			/// Represents a "Cable" action.
			/// </summary>
			literal int Cable = DIBUTTON_REMOTE_CABLE;

			/// <summary>
			/// Represents a "CD" action.
			/// </summary>
			literal int CD = DIBUTTON_REMOTE_CD;

			/// <summary>
			/// Represents a "VCR" action.
			/// </summary>
			literal int VCR = DIBUTTON_REMOTE_VCR;

			/// <summary>
			/// Represents a "Tuner" action.
			/// </summary>
			literal int Tuner = DIBUTTON_REMOTE_TUNER;

			/// <summary>
			/// Represents a "DVD" action.
			/// </summary>
			literal int DVD = DIBUTTON_REMOTE_DVD;

			/// <summary>
			/// Represents a "Adjust" action.
			/// </summary>
			literal int Adjust = DIBUTTON_REMOTE_ADJUST;

			/// <summary>
			/// Represents a "Digit0" action.
			/// </summary>
			literal int Digit0 = DIBUTTON_REMOTE_DIGIT0;

			/// <summary>
			/// Represents a "Digit1" action.
			/// </summary>
			literal int Digit1 = DIBUTTON_REMOTE_DIGIT1;

			/// <summary>
			/// Represents a "Digit2" action.
			/// </summary>
			literal int Digit2 = DIBUTTON_REMOTE_DIGIT2;

			/// <summary>
			/// Represents a "Digit3" action.
			/// </summary>
			literal int Digit3 = DIBUTTON_REMOTE_DIGIT3;

			/// <summary>
			/// Represents a "Digit4" action.
			/// </summary>
			literal int Digit4 = DIBUTTON_REMOTE_DIGIT4;

			/// <summary>
			/// Represents a "Digit5" action.
			/// </summary>
			literal int Digit5 = DIBUTTON_REMOTE_DIGIT5;

			/// <summary>
			/// Represents a "Digit6" action.
			/// </summary>
			literal int Digit6 = DIBUTTON_REMOTE_DIGIT6;

			/// <summary>
			/// Represents a "Digit7" action.
			/// </summary>
			literal int Digit7 = DIBUTTON_REMOTE_DIGIT7;

			/// <summary>
			/// Represents a "Digit8" action.
			/// </summary>
			literal int Digit8 = DIBUTTON_REMOTE_DIGIT8;

			/// <summary>
			/// Represents a "Digit9" action.
			/// </summary>
			literal int Digit9 = DIBUTTON_REMOTE_DIGIT9;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_REMOTE_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_REMOTE_PAUSE;

		};
	}
}
