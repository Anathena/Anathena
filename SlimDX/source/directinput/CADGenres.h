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
		/// Contains action mapping constants for 2D objects in CAD.
		/// </summary>
		public ref class Objects2DControls sealed
		{
		private:
			Objects2DControls() { }

		public:
			/// <summary>
			/// Represents the 2D control genre.
			/// </summary>
			literal int Objects2DGenre = DIVIRTUAL_CAD_2DCONTROL;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_2DCONTROL_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_2DCONTROL_MOVE;

			/// <summary>
			/// Represents a "InOut" action.
			/// </summary>
			literal int InOut = DIAXIS_2DCONTROL_INOUT;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_2DCONTROL_SELECT;

			/// <summary>
			/// Represents a "Special1" action.
			/// </summary>
			literal int Special1 = DIBUTTON_2DCONTROL_SPECIAL1;

			/// <summary>
			/// Represents a "Special" action.
			/// </summary>
			literal int Special = DIBUTTON_2DCONTROL_SPECIAL;

			/// <summary>
			/// Represents a "Special2" action.
			/// </summary>
			literal int Special2 = DIBUTTON_2DCONTROL_SPECIAL2;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_2DCONTROL_MENU;

			/// <summary>
			/// Represents a "HatSwitch" action.
			/// </summary>
			literal int HatSwitch = DIHATSWITCH_2DCONTROL_HATSWITCH;

			/// <summary>
			/// Represents a "RotateZ" action.
			/// </summary>
			literal int RotateZ = DIAXIS_2DCONTROL_ROTATEZ;

			/// <summary>
			/// Represents a "Display" action.
			/// </summary>
			literal int Display = DIBUTTON_2DCONTROL_DISPLAY;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_2DCONTROL_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_2DCONTROL_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for 3D models in CAD.
		/// </summary>
		public ref class Models3DControls sealed
		{
		private:
			Models3DControls() { }

		public:
			/// <summary>
			/// Represents the 3D model genre.
			/// </summary>
			literal int Models3DGenre = DIVIRTUAL_CAD_MODEL;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_CADM_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_CADM_MOVE;

			/// <summary>
			/// Represents a "InOut" action.
			/// </summary>
			literal int InOut = DIAXIS_CADM_INOUT;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_CADM_SELECT;

			/// <summary>
			/// Represents a "Special1" action.
			/// </summary>
			literal int Special1 = DIBUTTON_CADM_SPECIAL1;

			/// <summary>
			/// Represents a "Special" action.
			/// </summary>
			literal int Special = DIBUTTON_CADM_SPECIAL;

			/// <summary>
			/// Represents a "Special2" action.
			/// </summary>
			literal int Special2 = DIBUTTON_CADM_SPECIAL2;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_CADM_MENU;

			/// <summary>
			/// Represents a "HatSwitch" action.
			/// </summary>
			literal int HatSwitch = DIHATSWITCH_CADM_HATSWITCH;

			/// <summary>
			/// Represents a "RotateX" action.
			/// </summary>
			literal int RotateX = DIAXIS_CADM_ROTATEX;

			/// <summary>
			/// Represents a "RotateY" action.
			/// </summary>
			literal int RotateY = DIAXIS_CADM_ROTATEY;

			/// <summary>
			/// Represents a "RotateZ" action.
			/// </summary>
			literal int RotateZ = DIAXIS_CADM_ROTATEZ;

			/// <summary>
			/// Represents a "Display" action.
			/// </summary>
			literal int Display = DIBUTTON_CADM_DISPLAY;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_CADM_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_CADM_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for 3D navigation in CAD.
		/// </summary>
		public ref class Navigation3DControls sealed
		{
		private:
			Navigation3DControls() { }

		public:
			/// <summary>
			/// Represents the 3D navigation genre.
			/// </summary>
			literal int Navigation3DGenre = DIVIRTUAL_CAD_FLYBY;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_CADF_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_CADF_MOVE;

			/// <summary>
			/// Represents a "InOut" action.
			/// </summary>
			literal int InOut = DIAXIS_CADF_INOUT;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_CADF_SELECT;

			/// <summary>
			/// Represents a "Special1" action.
			/// </summary>
			literal int Special1 = DIBUTTON_CADF_SPECIAL1;

			/// <summary>
			/// Represents a "Special" action.
			/// </summary>
			literal int Special = DIBUTTON_CADF_SPECIAL;

			/// <summary>
			/// Represents a "Special2" action.
			/// </summary>
			literal int Special2 = DIBUTTON_CADF_SPECIAL2;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_CADF_MENU;

			/// <summary>
			/// Represents a "HatSwitch" action.
			/// </summary>
			literal int HatSwitch = DIHATSWITCH_CADF_HATSWITCH;

			/// <summary>
			/// Represents a "RotateX" action.
			/// </summary>
			literal int RotateX = DIAXIS_CADF_ROTATEX;

			/// <summary>
			/// Represents a "RotateY" action.
			/// </summary>
			literal int RotateY = DIAXIS_CADF_ROTATEY;

			/// <summary>
			/// Represents a "RotateZ" action.
			/// </summary>
			literal int RotateZ = DIAXIS_CADF_ROTATEZ;

			/// <summary>
			/// Represents a "Display" action.
			/// </summary>
			literal int Display = DIBUTTON_CADF_DISPLAY;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_CADF_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_CADF_PAUSE;
		};

		/// <summary>
		/// Contains action mapping constants for 3D objects in CAD.
		/// </summary>
		public ref class Objects3DControls sealed
		{
		private:
			Objects3DControls() { }

		public:
			/// <summary>
			/// Represents the 3D control genre.
			/// </summary>
			literal int Objects3DGenre = DIVIRTUAL_CAD_3DCONTROL;

			/// <summary>
			/// Represents a "Lateral" action.
			/// </summary>
			literal int Lateral = DIAXIS_3DCONTROL_LATERAL;

			/// <summary>
			/// Represents a "Move" action.
			/// </summary>
			literal int Move = DIAXIS_3DCONTROL_MOVE;

			/// <summary>
			/// Represents a "InOut" action.
			/// </summary>
			literal int InOut = DIAXIS_3DCONTROL_INOUT;

			/// <summary>
			/// Represents a "Select" action.
			/// </summary>
			literal int Select = DIBUTTON_3DCONTROL_SELECT;

			/// <summary>
			/// Represents a "Special1" action.
			/// </summary>
			literal int Special1 = DIBUTTON_3DCONTROL_SPECIAL1;

			/// <summary>
			/// Represents a "Special" action.
			/// </summary>
			literal int Special = DIBUTTON_3DCONTROL_SPECIAL;

			/// <summary>
			/// Represents a "Special2" action.
			/// </summary>
			literal int Special2 = DIBUTTON_3DCONTROL_SPECIAL2;

			/// <summary>
			/// Represents a "Menu" action.
			/// </summary>
			literal int Menu = DIBUTTON_3DCONTROL_MENU;

			/// <summary>
			/// Represents a "HatSwitch" action.
			/// </summary>
			literal int HatSwitch = DIHATSWITCH_3DCONTROL_HATSWITCH;

			/// <summary>
			/// Represents a "RotateX" action.
			/// </summary>
			literal int RotateX = DIAXIS_3DCONTROL_ROTATEX;

			/// <summary>
			/// Represents a "RotateY" action.
			/// </summary>
			literal int RotateY = DIAXIS_3DCONTROL_ROTATEY;

			/// <summary>
			/// Represents a "RotateZ" action.
			/// </summary>
			literal int RotateZ = DIAXIS_3DCONTROL_ROTATEZ;

			/// <summary>
			/// Represents a "Display" action.
			/// </summary>
			literal int Display = DIBUTTON_3DCONTROL_DISPLAY;

			/// <summary>
			/// Represents a "Device" action.
			/// </summary>
			literal int Device = DIBUTTON_3DCONTROL_DEVICE;

			/// <summary>
			/// Represents a "Pause" action.
			/// </summary>
			literal int Pause = DIBUTTON_3DCONTROL_PAUSE;
		};
	}
}
