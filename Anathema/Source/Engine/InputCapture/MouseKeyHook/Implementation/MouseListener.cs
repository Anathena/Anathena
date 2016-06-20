using Anathema.Source.Engine.InputCapture.MouseKeyHook.WinApi;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Anathema.Source.Engine.InputCapture.MouseKeyHook.Implementation
{
    // Because it is a P/Invoke method, 'GetSystemMetrics(int)' should be defined in a class named
    // NativeMethods, SafeNativeMethods, or UnsafeNativeMethods.
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724385(v=vs.85).aspx
    internal static class NativeMethods
    {
        private const Int32 SM_CXDRAG = 68;
        private const Int32 SM_CYDRAG = 69;

        [DllImport("user32.dll")]
        private static extern Int32 GetSystemMetrics(Int32 Index);

        public static Int32 GetXDragThreshold()
        {
            return GetSystemMetrics(SM_CXDRAG);
        }

        public static Int32 GetYDragThreshold()
        {
            return GetSystemMetrics(SM_CYDRAG);
        }
    }

    internal abstract class MouseListener : BaseListener, IMouseEvents
    {
        private readonly Point MouseUninitialisedPoint = new Point(-1, -1);

        private readonly Int32 MouseXDragThreshold;
        private readonly Int32 MouseYDragThreshold;

        private readonly ButtonSet MouseDoubleDown;
        private readonly ButtonSet MouseSingleDown;

        private Boolean MouseIsDragging;

        private Point MousePreviousPosition;
        private Point MouseDragStartPosition;

        protected MouseListener(Subscribe Subscribe) : base(Subscribe)
        {
            MouseXDragThreshold = NativeMethods.GetXDragThreshold();
            MouseYDragThreshold = NativeMethods.GetYDragThreshold();
            MouseIsDragging = false;

            MousePreviousPosition = MouseUninitialisedPoint;
            MouseDragStartPosition = MouseUninitialisedPoint;

            MouseDoubleDown = new ButtonSet();
            MouseSingleDown = new ButtonSet();
        }

        protected override Boolean Callback(CallbackData Data)
        {
            MouseEventExtArgs E = GetEventArgs(Data);

            if (E.IsMouseButtonDown)
                ProcessDown(ref E);

            if (E.IsMouseButtonUp)
                ProcessUp(ref E);

            if (E.WheelScrolled)
                ProcessWheel(ref E);

            if (HasMoved(E.Point))
                ProcessMove(ref E);

            ProcessDrag(ref E);

            return !E.Handled;
        }

        protected abstract MouseEventExtArgs GetEventArgs(CallbackData Data);

        protected virtual void ProcessWheel(ref MouseEventExtArgs E)
        {
            OnWheel(E);
            OnWheelExt(E);
        }

        protected virtual void ProcessDown(ref MouseEventExtArgs E)
        {
            OnDown(E);
            OnDownExt(E);

            if (E.Handled)
                return;

            if (E.Clicks == 2)
                MouseDoubleDown.Add(E.Button);

            if (E.Clicks == 1)
                MouseSingleDown.Add(E.Button);
        }

        protected virtual void ProcessUp(ref MouseEventExtArgs E)
        {
            if (MouseSingleDown.Contains(E.Button))
            {
                OnUp(E);
                OnUpExt(E);

                if (E.Handled)
                    return;

                OnClick(E);
                MouseSingleDown.Remove(E.Button);
            }

            if (MouseDoubleDown.Contains(E.Button))
            {
                E = E.ToDoubleClickEventArgs();
                OnUp(E);
                OnDoubleClick(E);
                MouseDoubleDown.Remove(E.Button);
            }
        }

        private void ProcessMove(ref MouseEventExtArgs E)
        {
            MousePreviousPosition = E.Point;

            OnMove(E);
            OnMoveExt(E);
        }

        private void ProcessDrag(ref MouseEventExtArgs E)
        {
            if (MouseSingleDown.Contains(MouseButtons.Left))
            {
                if (MouseDragStartPosition.Equals(MouseUninitialisedPoint))
                    MouseDragStartPosition = E.Point;

                ProcessDragStarted(ref E);
            }
            else
            {
                MouseDragStartPosition = MouseUninitialisedPoint;
                ProcessDragFinished(ref E);
            }
        }

        private void ProcessDragStarted(ref MouseEventExtArgs Args)
        {
            if (!MouseIsDragging)
            {
                Boolean IsXDragging = Math.Abs(Args.Point.X - MouseDragStartPosition.X) > MouseXDragThreshold;
                Boolean IsYDragging = Math.Abs(Args.Point.Y - MouseDragStartPosition.Y) > MouseYDragThreshold;

                MouseIsDragging = IsXDragging || IsYDragging;

                if (MouseIsDragging)
                {
                    OnDragStarted(Args);
                    OnDragStartedExt(Args);
                }
            }
        }

        private void ProcessDragFinished(ref MouseEventExtArgs e)
        {
            if (MouseIsDragging)
            {
                OnDragFinished(e);
                OnDragFinishedExt(e);
                MouseIsDragging = false;
            }
        }

        private Boolean HasMoved(Point ActualPoint)
        {
            return MousePreviousPosition != ActualPoint;
        }

        public event MouseEventHandler MouseMove;
        public event EventHandler<MouseEventExtArgs> MouseMoveExt;
        public event MouseEventHandler MouseClick;
        public event MouseEventHandler MouseDown;
        public event EventHandler<MouseEventExtArgs> MouseDownExt;
        public event MouseEventHandler MouseUp;
        public event EventHandler<MouseEventExtArgs> MouseUpExt;
        public event MouseEventHandler MouseWheel;
        public event EventHandler<MouseEventExtArgs> MouseWheelExt;
        public event MouseEventHandler MouseDoubleClick;
        public event MouseEventHandler MouseDragStarted;
        public event EventHandler<MouseEventExtArgs> MouseDragStartedExt;
        public event MouseEventHandler MouseDragFinished;
        public event EventHandler<MouseEventExtArgs> MouseDragFinishedExt;

        protected virtual void OnMove(MouseEventArgs E)
        {
            MouseMove?.Invoke(this, E);
        }

        protected virtual void OnMoveExt(MouseEventExtArgs E)
        {
            MouseMoveExt?.Invoke(this, E);
        }

        protected virtual void OnClick(MouseEventArgs E)
        {
            MouseClick?.Invoke(this, E);
        }

        protected virtual void OnDown(MouseEventArgs E)
        {
            MouseDown?.Invoke(this, E);
        }

        protected virtual void OnDownExt(MouseEventExtArgs E)
        {
            MouseDownExt?.Invoke(this, E);
        }

        protected virtual void OnUp(MouseEventArgs E)
        {
            MouseUp?.Invoke(this, E);
        }

        protected virtual void OnUpExt(MouseEventExtArgs E)
        {
            MouseUpExt?.Invoke(this, E);
        }

        protected virtual void OnWheel(MouseEventArgs E)
        {
            MouseWheel?.Invoke(this, E);
        }

        protected virtual void OnWheelExt(MouseEventExtArgs E)
        {
            MouseWheelExt?.Invoke(this, E);
        }

        protected virtual void OnDoubleClick(MouseEventArgs E)
        {
            MouseDoubleClick?.Invoke(this, E);
        }

        protected virtual void OnDragStarted(MouseEventArgs E)
        {
            MouseDragStarted?.Invoke(this, E);
        }

        protected virtual void OnDragStartedExt(MouseEventExtArgs E)
        {
            MouseDragStartedExt?.Invoke(this, E);
        }

        protected virtual void OnDragFinished(MouseEventArgs E)
        {
            MouseDragFinished?.Invoke(this, E);
        }

        protected virtual void OnDragFinishedExt(MouseEventExtArgs E)
        {
            MouseDragFinishedExt?.Invoke(this, E);
        }

    } // End class

} // End namespace