namespace Ana.Source.Engine.InputCapture.Mouse
{
    using SharpDX;
    using SharpDX.DirectInput;
    using System;

    internal class MouseCapture : IMouseSubject
    {
        public MouseCapture()
        {
            this.DirectInput = new DirectInput();
            this.Mouse = new Mouse(this.DirectInput);
            this.Mouse.Acquire();
        }

        private enum MouseButtonEnum
        {
            Left = 0,

            Middle = 1,

            Right = 2,

            XButton1,

            XButton2
        }

        private DirectInput DirectInput { get; set; }

        private Mouse Mouse { get; set; }

        private MouseState MouseState { get; set; }

        private MouseState PreviousMouseState { get; set; }

        public void Update()
        {
            try
            {
                this.MouseState = this.Mouse.GetCurrentState();

                if (this.MouseState == null)
                {
                    return;
                }

                if (this.PreviousMouseState != null)
                {
                    if (this.MouseState.Buttons[(Int32)MouseButtonEnum.Left] == true && this.PreviousMouseState.Buttons[(Int32)MouseButtonEnum.Left] == false)
                    {
                        this.OnMouseDown(MouseButtonEnum.Left);
                    }

                    if (this.MouseState.Buttons[(Int32)MouseButtonEnum.Right] == true && this.PreviousMouseState.Buttons[(Int32)MouseButtonEnum.Right] == false)
                    {
                        this.OnMouseDown(MouseButtonEnum.Right);
                    }

                    if (this.MouseState.Buttons[(Int32)MouseButtonEnum.Left] == false && this.PreviousMouseState.Buttons[(Int32)MouseButtonEnum.Left] == true)
                    {
                        this.OnMouseUp(MouseButtonEnum.Left);
                    }
                }

                this.PreviousMouseState = this.MouseState;
            }
            catch (SharpDXException)
            {
                this.Mouse.Acquire();
            }
        }

        public void Subscribe(IMouseObserver subject)
        {
        }

        public void Unsubscribe(IMouseObserver subject)
        {
        }

        private void OnMouseDown(MouseButtonEnum mouseButton)
        {
            switch (mouseButton)
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

        private void OnMouseUp(MouseButtonEnum mouseButton)
        {
            switch (mouseButton)
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
    }
    //// End class
}
//// End namespace