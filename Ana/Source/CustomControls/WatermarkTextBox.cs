namespace Ana.Source.CustomControls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// A textbox that supports a watermark hint.
    /// </summary>
    internal class WatermarkTextBox : TextBox
    {
        /// <summary>
        /// The water mark draw color.
        /// </summary>
        private Color watermarkColor;

        /// <summary>
        /// The font used to draw the water mark.
        /// </summary>
        private Font waterMarkFont;

        /// <summary>
        /// Initializes a new instance of the <see cref="WatermarkTextBox" /> class.
        /// </summary>
        public WatermarkTextBox()
        {
            // Set defaults
            this.watermarkColor = Color.LightGray;
            this.waterMarkFont = this.Font;
            this.WaterMarkContainer = null;

            // Draw the watermark, so we can see it in design time
            this.DrawWaterMark();

            this.Enter += new EventHandler(this.TextEnter);
            this.Leave += new EventHandler(this.TextLeave);
            this.TextChanged += new EventHandler(this.TextChange);
        }

        /// <summary>
        /// Gets or sets the water mark text.
        /// </summary>
        public String WaterMarkText
        {
            get
            {
                return this.WatermarkText;
            }

            set
            {
                this.WatermarkText = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the water mark draw color.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the font used to draw the water mark.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the panel to contain the watermark, rather than using the textbox itself.
        /// </summary>
        private Panel WaterMarkContainer { get; set; }

        /// <summary>
        /// Gets or sets the brush used to render the watermark.
        /// </summary>
        private SolidBrush WaterMarkBrush { get; set; }

        /// <summary>
        /// Gets or sets the watermark text.
        /// </summary>
        private String WatermarkText { get; set; }

        /// <summary>
        /// Invoked when the control is being rendered.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw the watermark even in design time
            this.DrawWaterMark();
        }

        /// <summary>
        /// Invoked when the control is invalidated.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);

            // Check if there is a watermark
            if (this.WaterMarkContainer != null)
            {
                // If there is a watermark it should also be invalidated();
                this.WaterMarkContainer.Invalidate();
            }
        }

        /// <summary>
        /// Removes the watermark if it should.
        /// </summary>
        private void RemoveWaterMark()
        {
            if (this.WaterMarkContainer != null)
            {
                this.Controls.Remove(this.WaterMarkContainer);
                this.WaterMarkContainer = null;
            }
        }

        /// <summary>
        /// Draws the watermark if the text length is 0.
        /// </summary>
        private void DrawWaterMark()
        {
            if (this.WaterMarkContainer == null && this.TextLength <= 0)
            {
                this.WaterMarkContainer = new Panel();
                this.WaterMarkContainer.Click += this.WaterMarkContainerClick;
                this.WaterMarkContainer.Paint += this.WaterMarkContainerPaint;
                this.WaterMarkContainer.Invalidate();
                this.Controls.Add(this.WaterMarkContainer);
            }
        }

        /// <summary>
        /// Invoked when the water mark container is clicked. Note this is not the textbox itself.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event args.</param>
        private void WaterMarkContainerClick(Object sender, EventArgs e)
        {
            this.Focus();
        }

        /// <summary>
        /// Invoked when the water mark container is rendered.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event args.</param>
        private void WaterMarkContainerPaint(Object sender, PaintEventArgs e)
        {
            this.WaterMarkContainer.Location = new Point(2, 0);
            this.WaterMarkContainer.Height = this.Height;
            this.WaterMarkContainer.Width = this.Width;
            this.WaterMarkContainer.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            if (this.ContainsFocus)
            {
                // If focused use normal color
                this.WaterMarkBrush = new SolidBrush(this.BackColor);
            }
            else
            {
                // If not focused use not active color
                this.WaterMarkBrush = new SolidBrush(this.watermarkColor);
            }

            // Drawing the string into the panel 
            e.Graphics.DrawString(this.WatermarkText, this.waterMarkFont, this.WaterMarkBrush, new PointF(-2f, 1f));
        }

        /// <summary>
        /// Invoked when the control is selected.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event args.</param>
        private void TextEnter(Object sender, EventArgs e)
        {
            // If focused use focus color
            this.WaterMarkBrush = new SolidBrush(this.BackColor);

            // The watermark should not be drawn if the user has already written some text
            if (this.TextLength <= 0)
            {
                this.RemoveWaterMark();
                this.DrawWaterMark();
            }
        }

        /// <summary>
        /// Invoked when the control is deselected.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event args.</param>
        private void TextLeave(Object sender, EventArgs e)
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

        /// <summary>
        /// Invoked when the text changes for the textbox.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event args.</param>
        private void TextChange(Object sender, EventArgs e)
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