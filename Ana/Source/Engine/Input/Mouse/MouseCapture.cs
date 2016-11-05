namespace Ana.Source.Engine.Input.Mouse
{
    using SharpDX;
    using SharpDX.DirectInput;
    using System;

    /// <summary>
    /// Class to capture mouse input
    /// </summary>
    internal class MouseCapture : IMouseSubject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseCapture" /> class
        /// </summary>
        public MouseCapture()
        {
            this.DirectInput = new DirectInput();
            this.Mouse = new Mouse(this.DirectInput);
            this.Mouse.Acquire();
        }

        /// <summary>
        /// An enumeration of possible mouse inputs
        /// </summary>
        private enum MouseButtonEnum
        {
            /// <summary>
            /// The left mouse button
            /// </summary>
            Left = 0,

            /// <summary>
            /// The middle mouse button
            /// </summary>
            Middle = 1,

            /// <summary>
            /// The right mouse button
            /// </summary>
            Right = 2,

            /// <summary>
            /// An additonal button featured on many mouses, often 'forwards' or 'backwards'
            /// </summary>
            XButton1,

            /// <summary>
            /// An additonal button featured on many mouses, often 'forwards' or 'backwards'
            /// </summary>
            XButton2
        }

        /// <summary>
        /// Gets or sets the DirectX input object to collect mouse input
        /// </summary>
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