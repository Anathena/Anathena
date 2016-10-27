namespace Ana.View.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// A textbox that supports a watermark hint
    /// </summary>
    internal class WatermarkTextBox : TextBox
    {
        private Panel waterMarkContainer;

        private SolidBrush waterMarkBrush;

        private String watermarkText;

        private Color watermarkColor;

        private Font waterMarkFont;

        /// <summary>
        /// Initializes a new instance of the <see cref="WatermarkTextBox" /> class
        /// </summary>
        public WatermarkTextBox()
        {
            // Set defaults
            this.watermarkColor = Color.LightGray;
            this.waterMarkFont = this.Font;
            this.waterMarkContainer = null;

            // Draw the watermark, so we can see it in design time
            this.DrawWaterMark();

            this.Enter += new EventHandler(this.ThisHasFocus);
            this.Leave += new EventHandler(this.ThisWasLeaved);
            this.TextChanged += new EventHandler(this.ThisTextChanged);
        }

        public String WaterMarkText
        {
            get
            {
                return this.watermarkText;
            }

            set
            {
                this.watermarkText = value;
                this.Invalidate();
            }
        }

        public Color WatermarkColor
        {
            get
            {
                return this.watermarkColor;
            }

            set
            {
                this.watermarkColor = value;
                this.Invalidate();
            }
        }

        public Font WaterMarkFont
        {
            get
            {
                return this.waterMarkFont;
            }

            set
            {
                this.waterMarkFont = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw the watermark even in design time
            this.DrawWaterMark();
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);

            // Check if there is a watermark
            if (this.waterMarkContainer != null)
            {
                // If there is a watermark it should also be invalidated();
                this.waterMarkContainer.Invalidate();
            }
        }

        /// <summary>
        /// Removes the watermark if it should
        /// </summary>
        private void RemoveWaterMark()
        {
            if (this.waterMarkContainer != null)
            {
                this.Controls.Remove(this.waterMarkContainer);
                this.waterMarkContainer = null;
            }
        }

        /// <summary>
        /// Draws the watermark if the text length is 0
        /// </summary>
        private void DrawWaterMark()
        {
            if (this.waterMarkContainer == null && this.TextLength <= 0)
            {
                this.waterMarkContainer = new Panel();
                this.waterMarkContainer.Click += this.WaterMarkContainerClick;
                this.waterMarkContainer.Paint += this.WaterMarkContainerPaint;
                this.waterMarkContainer.Invalidate();
                this.Controls.Add(this.waterMarkContainer);
            }
        }

        private void WaterMarkContainerClick(Object sender, EventArgs e)
        {
            this.Focus();
        }

        private void WaterMarkContainerPaint(Object sender, PaintEventArgs e)
        {
            this.waterMarkContainer.Location = new Point(2, 0);
            this.waterMarkContainer.Height = this.Height;
            this.waterMarkContainer.Width = this.Width;
            this.waterMarkContainer.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            if (this.ContainsFocus)
            {
                // If focused use normal color
                this.waterMarkBrush = new SolidBrush(this.BackColor);
            }
            else
            {
                // If not focused use not active color
                this.waterMarkBrush = new SolidBrush(this.watermarkColor);
            }

            // Drawing the string into the panel 
            e.Graphics.DrawString(this.watermarkText, this.waterMarkFont, this.waterMarkBrush, new PointF(-2f, 1f));
        }

        private void ThisHasFocus(Object sender, EventArgs e)
        {
            // If focused use focus color
            this.waterMarkBrush = new SolidBrush(this.BackColor);

            // The watermark should not be drawn if the user has already written some text
            if (this.TextLength <= 0)
            {
                this.RemoveWaterMark();
                this.DrawWaterMark();
            }
        }

        private void ThisWasLeaved(Object sender, EventArgs e)
        {
            // If the user has written something and left the control
            if (this.TextLength > 0)
            {
                // Remove the watermark
                this.RemoveWaterMark();
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
                this.RemoveWaterMark();
            }
            else
            {
                // But if the text is empty, draw the watermark again.
                this.DrawWaterMark();
            }
        }
    }
    //// End class
}
//// End namespace