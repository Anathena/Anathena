using System;
using System.Windows.Forms;

namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.Implementation
{
    internal abstract class EventFacade : IKeyboardMouseEvents
    {
        private KeyListener MKeyListenerCache;
        private MouseListener MouseListenerCache;

        public event KeyEventHandler KeyDown
        {
            add { GetKeyListener().KeyDown += value; }
            remove { GetKeyListener().KeyDown -= value; }
        }

        public event KeyPressEventHandler KeyPress
        {
            add { GetKeyListener().KeyPress += value; }
            remove { GetKeyListener().KeyPress -= value; }
        }

        public event KeyEventHandler KeyUp
        {
            add { GetKeyListener().KeyUp += value; }
            remove { GetKeyListener().KeyUp -= value; }
        }

        public event MouseEventHandler MouseMove
        {
            add { GetMouseListener().MouseMove += value; }
            remove { GetMouseListener().MouseMove -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseMoveExt
        {
            add { GetMouseListener().MouseMoveExt += value; }
            remove { GetMouseListener().MouseMoveExt -= value; }
        }

        public event MouseEventHandler MouseClick
        {
            add { GetMouseListener().MouseClick += value; }
            remove { GetMouseListener().MouseClick -= value; }
        }

        public event MouseEventHandler MouseDown
        {
            add { GetMouseListener().MouseDown += value; }
            remove { GetMouseListener().MouseDown -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseDownExt
        {
            add { GetMouseListener().MouseDownExt += value; }
            remove { GetMouseListener().MouseDownExt -= value; }
        }

        public event MouseEventHandler MouseUp
        {
            add { GetMouseListener().MouseUp += value; }
            remove { GetMouseListener().MouseUp -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseUpExt
        {
            add { GetMouseListener().MouseUpExt += value; }
            remove { GetMouseListener().MouseUpExt -= value; }
        }

        public event MouseEventHandler MouseWheel
        {
            add { GetMouseListener().MouseWheel += value; }
            remove { GetMouseListener().MouseWheel -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseWheelExt
        {
            add { GetMouseListener().MouseWheelExt += value; }
            remove { GetMouseListener().MouseWheelExt -= value; }
        }

        public event MouseEventHandler MouseDoubleClick
        {
            add { GetMouseListener().MouseDoubleClick += value; }
            remove { GetMouseListener().MouseDoubleClick -= value; }
        }

        public event MouseEventHandler MouseDragStarted
        {
            add { GetMouseListener().MouseDragStarted += value; }
            remove { GetMouseListener().MouseDragStarted -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseDragStartedExt
        {
            add { GetMouseListener().MouseDragStartedExt += value; }
            remove { GetMouseListener().MouseDragStartedExt -= value; }
        }

        public event MouseEventHandler MouseDragFinished
        {
            add { GetMouseListener().MouseDragFinished += value; }
            remove { GetMouseListener().MouseDragFinished -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseDragFinishedExt
        {
            add { GetMouseListener().MouseDragFinishedExt += value; }
            remove { GetMouseListener().MouseDragFinishedExt -= value; }
        }

        public void Dispose()
        {
            MouseListenerCache?.Dispose();
            MKeyListenerCache?.Dispose();
        }

        private KeyListener GetKeyListener()
        {
            KeyListener KeyListener = MKeyListenerCache;

            if (KeyListener != null)
                return KeyListener;

            KeyListener = CreateKeyListener();
            MKeyListenerCache = KeyListener;

            return KeyListener;
        }

        private MouseListener GetMouseListener()
        {
            MouseListener MouseListener = MouseListenerCache;

            if (MouseListener != null)
                return MouseListener;

            MouseListener = CreateMouseListener();
            MouseListenerCache = MouseListener;

            return MouseListener;
        }

        protected abstract MouseListener CreateMouseListener();

        protected abstract KeyListener CreateKeyListener();

    } // End class

} // End namespace