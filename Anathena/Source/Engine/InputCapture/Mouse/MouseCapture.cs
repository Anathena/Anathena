using SharpDX;
using SharpDX.DirectInput;
using System;

namespace Ana.Source.Engine.InputCapture.Mouse
{
    class MouseCapture : IMouseSubject
    {
        private DirectInput DirectInput;
        private SharpDX.DirectInput.Mouse Mouse;

        private MouseState MouseState;
        private MouseState PreviousMouseState;

        private enum MouseButtonEnum
        {
            Left = 0,
            Middle = 1,
            Right = 2,
            XButton1,
            XButton2
        }

        public MouseCapture()
        {
            DirectInput = new DirectInput();
            Mouse = new SharpDX.DirectInput.Mouse(DirectInput);
            Mouse.Acquire();
        }

        public void Update()
        {
            try
            {
                MouseState = Mouse.GetCurrentState();

                if (MouseState == null)
                    return;

                if (PreviousMouseState != null)
                {
                    if (MouseState.Buttons[(Int32)MouseButtonEnum.Left] == true
                        && PreviousMouseState.Buttons[(Int32)MouseButtonEnum.Left] == false)
                        OnMouseDown(MouseButtonEnum.Left);

                    if (MouseState.Buttons[(Int32)MouseButtonEnum.Right] == true
                        && PreviousMouseState.Buttons[(Int32)MouseButtonEnum.Right] == false)
                        OnMouseDown(MouseButtonEnum.Right);

                    if (MouseState.Buttons[(Int32)MouseButtonEnum.Left] == false
                        && PreviousMouseState.Buttons[(Int32)MouseButtonEnum.Left] == true)
                        OnMouseUp(MouseButtonEnum.Left);
                }

                PreviousMouseState = MouseState;
            }
            catch (SharpDXException)
            {
                Mouse.Acquire();
            }
        }

        #region Events

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

        public void Subscribe(IMouseObserver Subject)
        {

        }

        public void Unsubscribe(IMouseObserver Subject)
        {

        }

        #endregion

    } // End class

} // End namespace