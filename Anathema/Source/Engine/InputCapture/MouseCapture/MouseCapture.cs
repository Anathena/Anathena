using SharpDX.DirectInput;
using System;

namespace Anathema.Source.Engine.InputCapture.MouseCapture
{
    class MouseCapture : IMouseSubject
    {
        private DirectInput DirectInput;
        private Mouse Mouse;

        private MouseState MouseState;
        private MouseState PreviousMouseState;

        private enum MouseButtonEnum
        {
            Left = 0,
            Middle = 1,
            Right = 2
        }

        public MouseCapture()
        {
            DirectInput = new DirectInput();
            Mouse = new Mouse(DirectInput);
        }

        public void Update()
        {
            MouseState = Mouse.GetCurrentState();

            if (MouseState.Buttons[(Int32)MouseButtonEnum.Left] == true
                && PreviousMouseState.Buttons[(Int32)MouseButtonEnum.Left] == false)
                OnMouseDown(MouseButtonEnum.Left);

            if (MouseState.Buttons[(Int32)MouseButtonEnum.Right] == true
                && PreviousMouseState.Buttons[(Int32)MouseButtonEnum.Right] == false)
                OnMouseDown(MouseButtonEnum.Right);

            if (MouseState.Buttons[(Int32)MouseButtonEnum.Left] == false
                && PreviousMouseState.Buttons[(Int32)MouseButtonEnum.Left] == true)
                OnMouseUp(MouseButtonEnum.Left);

            PreviousMouseState = MouseState;
        }

        private void OnMouseDown(MouseButtonEnum MouseButton)
        {
            switch (MouseButton)
            {
                case MouseButtonEnum.Left:
                    break;
                case MouseButtonEnum.Middle:
                    break;
                case MouseButtonEnum.Right:
                    break;
                default:
                    return;
            }
        }

        private void OnMouseUp(MouseButtonEnum MouseButton)
        {
            switch (MouseButton)
            {
                case MouseButtonEnum.Left:
                    break;
                case MouseButtonEnum.Middle:
                    break;
                case MouseButtonEnum.Right:
                    break;
                default:
                    return;
            }
        }

    } // End class

} // End namespace