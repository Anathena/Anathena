using System;
using System.Drawing;
using System.Windows.Forms;

namespace Anathema
{
    /// <summary>
    /// A textbox that supports a watermak hint.
    /// </summary>
    public class WatermarkTextBox : TextBox
    {

        protected Panel WaterMarkContainer;
        protected SolidBrush WaterMarkBrush;

        protected string _WatermarkText;
        public string WaterMarkText
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
            Initialize();
        }

        #region Private Methods

        /// <summary>
        /// Initializes watermark properties and adds CtextBox events
        /// </summary>
        private void Initialize()
        {
            //Sets some default values to the watermark properties
            _WatermarkColor = Color.LightGray;
            _WaterMarkFont = this.Font;
            WaterMarkContainer = null;

            //Draw the watermark, so we can see it in design time
            DrawWaterMark();

            //Eventhandlers which contains function calls. 
            //Either to draw or to remove the watermark
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
                WaterMarkContainer = new Panel(); // Creates the new panel instance
                WaterMarkContainer.Paint += new PaintEventHandler(waterMarkContainer_Paint);
                WaterMarkContainer.Invalidate();
                WaterMarkContainer.Click += new EventHandler(waterMarkContainer_Click);
                this.Controls.Add(WaterMarkContainer); // adds the control
            }
        }

        #endregion

        #region Eventhandlers

        #region WaterMark Events

        private void waterMarkContainer_Click(object sender, EventArgs e)
        {
            this.Focus(); //Makes sure you can click wherever you want on the control to gain focus
        }

        private void waterMarkContainer_Paint(object sender, PaintEventArgs e)
        {
            //Setting the watermark container up
            WaterMarkContainer.Location = new Point(2, 0); // sets the location
            WaterMarkContainer.Height = this.Height; // Height should be the same as its parent
            WaterMarkContainer.Width = this.Width; // same goes for width and the parent
            WaterMarkContainer.Anchor = AnchorStyles.Left | AnchorStyles.Right; // makes sure that it resizes with the parent control



            if (this.ContainsFocus)
            {
                //if focused use normal color
                WaterMarkBrush = new SolidBrush(this.BackColor);
            }

            else
            {
                //if not focused use not active color
                WaterMarkBrush = new SolidBrush(this._WatermarkColor);
            }

            //Drawing the string into the panel 
            Graphics g = e.Graphics;
            g.DrawString(this._WatermarkText, _WaterMarkFont, WaterMarkBrush, new PointF(-2f, 1f));//Take a look at that point
            //The reason I'm using the panel at all, is because of this feature, that it has no limits
            //I started out with a label but that looked very very bad because of its paddings 

        }

        #endregion

        #region WatermarkTextBox Events

        private void ThisHasFocus(object sender, EventArgs e)
        {
            //if focused use focus color
            WaterMarkBrush = new SolidBrush(this.BackColor);

            //The watermark should not be drawn if the user has already written some text
            if (this.TextLength <= 0)
            {
                RemoveWaterMark();
                DrawWaterMark();
            }
        }

        private void ThisWasLeaved(object sender, EventArgs e)
        {
            //if the user has written something and left the control
            if (this.TextLength > 0)
            {
                //Remove the watermark
                RemoveWaterMark();
            }
            else
            {
                //But if the user didn't write anything, Then redraw the control.
                this.Invalidate();
            }
        }

        private void ThisTextChanged(object sender, EventArgs e)
        {
            //If the text of the textbox is not empty
            if (this.TextLength > 0)
            {
                //Remove the watermark
                RemoveWaterMark();
            }
            else
            {
                //But if the text is empty, draw the watermark again.
                DrawWaterMark();
            }
        }

        #region Overrided Events

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Draw the watermark even in design time
            DrawWaterMark();
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            //Check if there is a watermark
            if (WaterMarkContainer != null)
                //if there is a watermark it should also be invalidated();
                WaterMarkContainer.Invalidate();
        }

        #endregion

        #endregion

        #endregion
       
    }
}
