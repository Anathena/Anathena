using System;
using System.Drawing;
using System.Windows.Forms;

namespace Anathena.GUI.CustomControls.TextBoxes
{
    /// <summary>
    /// A textbox that supports a watermak hint.
    /// </summary>
    public class WatermarkTextBox : TextBox
    {
        protected Panel waterMarkContainer;
        protected SolidBrush waterMarkBrush;

        protected String _watermarkText;
        public String WaterMarkText
        {
            get { return this._watermarkText; }
            set
            {
                this._watermarkText = value;
                this.Invalidate();
            }
        }

        protected Color _watermarkColor;
        public Color WatermarkColor
        {
            get { return this._watermarkColor; }

            set
            {
                this._watermarkColor = value;
                this.Invalidate();
            }
        }

        protected Font _waterMarkFont;
        public Font WaterMarkFont
        {
            get
            {
                return this._waterMarkFont;
            }

            set
            {
                this._waterMarkFont = value;
                this.Invalidate();
            }
        }

        public WatermarkTextBox()
        {
            // Set defaults
            _watermarkColor = Color.LightGray;
            _waterMarkFont = this.Font;
            waterMarkContainer = null;

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
            if (waterMarkContainer != null)
            {
                this.Controls.Remove(waterMarkContainer);
                waterMarkContainer = null;
            }
        }

        /// <summary>
        /// Draws the watermark if the text length is 0
        /// </summary>
        private void DrawWaterMark()
        {
            if (this.waterMarkContainer == null && this.TextLength <= 0)
            {
                waterMarkContainer = new Panel();
                waterMarkContainer.Click += WaterMarkContainer_Click;
                waterMarkContainer.Paint += WaterMarkContainer_Paint;
                waterMarkContainer.Invalidate();
                this.Controls.Add(waterMarkContainer);
            }
        }

        private void WaterMarkContainer_Click(Object sender, EventArgs e)
        {
            this.Focus();
        }

        private void WaterMarkContainer_Paint(Object sender, PaintEventArgs e)
        {
            waterMarkContainer.Location = new Point(2, 0);                      // Set location of watermark container
            waterMarkContainer.Height = this.Height;                            // Height should be the same as its parent
            waterMarkContainer.Width = this.Width;                              // same goes for width and the parent
            waterMarkContainer.Anchor = AnchorStyles.Left | AnchorStyles.Right; // makes sure that it resizes with the parent control

            if (this.ContainsFocus)
            {
                // If focused use normal color
                waterMarkBrush = new SolidBrush(this.BackColor);
            }

            else
            {
                // If not focused use not active color
                waterMarkBrush = new SolidBrush(this._watermarkColor);
            }

            // Drawing the string into the panel 
            e.Graphics.DrawString(this._watermarkText, _waterMarkFont, waterMarkBrush, new PointF(-2f, 1f));
        }

        private void ThisHasFocus(Object sender, EventArgs e)
        {
            // If focused use focus color
            waterMarkBrush = new SolidBrush(this.BackColor);

            // The watermark should not be drawn if the user has already written some text
            if (this.TextLength <= 0)
            {
                RemoveWaterMark();
                DrawWaterMark();
            }
        }

        private void ThisWasLeaved(Object sender, EventArgs e)
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

        private void ThisTextChanged(Object sender, EventArgs e)
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw the watermark even in design time
            DrawWaterMark();
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);

            // Check if there is a watermark
            if (waterMarkContainer != null)
                // If there is a watermark it should also be invalidated();
                waterMarkContainer.Invalidate();
        }

    } // End class

} // End namespace