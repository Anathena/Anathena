using System;
using System.Drawing;
using System.Windows.Forms;

namespace Anathema.GUI
{
    /// <summary>
    /// A textbox that supports a watermak hint.
    /// </summary>
    public class WatermarkTextBox : TextBox
    {
        protected Panel WaterMarkContainer;
        protected SolidBrush WaterMarkBrush;

        protected String _WatermarkText;
        public String WaterMarkText
        {
            get { return this._WatermarkText; }
            set
            {
                this._WatermarkText = value;
                this.Invalidate();
            }
        }

        protected Color _WatermarkColor;
        public Color WatermarkColor
        {
            get { return this._WatermarkColor; }

            set
            {
                this._WatermarkColor = value;
                this.Invalidate();
            }
        }

        protected Font _WaterMarkFont;
        public Font WaterMarkFont
        {
            get
            {
                return this._WaterMarkFont;
            }

            set
            {
                this._WaterMarkFont = value;
                this.Invalidate();
            }
        }
        
        public WatermarkTextBox()
        {
            // Set defaults
            _WatermarkColor = Color.LightGray;
            _WaterMarkFont = this.Font;
            WaterMarkContainer = null;

            // Draw the watermark, so we can see it in design time
            DrawWaterMark();
            
            this.Enter += new EventHandler(ThisHasFocus);
            this.Leave += new EventHandler(ThisWasLeaved);
            this.TextChanged += new EventHandler(ThisTextChanged);
        }
        
        /// <summary>
        /// Removes the watermark if it should
        /// </summary>
        private void RemoveWaterMark()
        {
            if (WaterMarkContainer != null)
            {
                this.Controls.Remove(WaterMarkContainer);
                WaterMarkContainer = null;
            }
        }

        /// <summary>
        /// Draws the watermark if the text length is 0
        /// </summary>
        private void DrawWaterMark()
        {
            if (this.WaterMarkContainer == null && this.TextLength <= 0)
            {
                WaterMarkContainer = new Panel();
                WaterMarkContainer.Click += WaterMarkContainer_Click;
                WaterMarkContainer.Paint += WaterMarkContainer_Paint;
                WaterMarkContainer.Invalidate();
                this.Controls.Add(WaterMarkContainer);
            }
        }

        private void WaterMarkContainer_Click(Object Sender, EventArgs E)
        {
            this.Focus();
        }

        private void WaterMarkContainer_Paint(Object Sender, PaintEventArgs E)
        {
            WaterMarkContainer.Location = new Point(2, 0);                      // Set location of watermark container
            WaterMarkContainer.Height = this.Height;                            // Height should be the same as its parent
            WaterMarkContainer.Width = this.Width;                              // same goes for width and the parent
            WaterMarkContainer.Anchor = AnchorStyles.Left | AnchorStyles.Right; // makes sure that it resizes with the parent control
            
            if (this.ContainsFocus)
            {
                // If focused use normal color
                WaterMarkBrush = new SolidBrush(this.BackColor);
            }

            else
            {
                // If not focused use not active color
                WaterMarkBrush = new SolidBrush(this._WatermarkColor);
            }

            // Drawing the string into the panel 
            E.Graphics.DrawString(this._WatermarkText, _WaterMarkFont, WaterMarkBrush, new PointF(-2f, 1f));
        }
        
        private void ThisHasFocus(Object Sender, EventArgs E)
        {
            // If focused use focus color
            WaterMarkBrush = new SolidBrush(this.BackColor);

            // The watermark should not be drawn if the user has already written some text
            if (this.TextLength <= 0)
            {
                RemoveWaterMark();
                DrawWaterMark();
            }
        }

        private void ThisWasLeaved(Object Sender, EventArgs E)
        {
            // If the user has written something and left the control
            if (this.TextLength > 0)
            {
                // Remove the watermark
                RemoveWaterMark();
            }
            else
            {
                // But if the user didn't write anything, Then redraw the control.
                this.Invalidate();
            }
        }

        private void ThisTextChanged(Object Sender, EventArgs E)
        {
            // If the text of the textbox is not empty
            if (this.TextLength > 0)
            {
                // Remove the watermark
                RemoveWaterMark();
            }
            else
            {
                // But if the text is empty, draw the watermark again.
                DrawWaterMark();
            }
        }

        protected override void OnPaint(PaintEventArgs E)
        {
            base.OnPaint(E);
            // Draw the watermark even in design time
            DrawWaterMark();
        }

        protected override void OnInvalidated(InvalidateEventArgs E)
        {
            base.OnInvalidated(E);
            // Check if there is a watermark
            if (WaterMarkContainer != null)
                // If there is a watermark it should also be invalidated();
                WaterMarkContainer.Invalidate();
        }
       
    } // End class

} // End namespace