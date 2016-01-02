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
        /// <summary>
        /// The text that will be presented as the watermak hint
        /// </summary>
        private string _WatermarkText;
        /// <summary>
        /// Gets or Sets the text that will be presented as the watermak hint
        /// </summary>
        public string WatermarkText
        {
            get { return _WatermarkText; }
            set { _WatermarkText = value; }
        }

        /// <summary>
        /// Whether watermark effect is enabled or not
        /// </summary>
        private bool _WatermarkActive = true;
        /// <summary>
        /// Gets or Sets whether watermark effect is enabled or not
        /// </summary>
        public bool WatermarkActive
        {
            get { return _WatermarkActive; }
            set { _WatermarkActive = value; }
        }

        /// <summary>
        /// Create a new TextBox that supports watermak hint
        /// </summary>
        public WatermarkTextBox()
        {
            this._WatermarkActive = true;
            _WatermarkText = this.Text;
            this.Text = String.Empty;
            this.ForeColor = Color.Gray;

            GotFocus += (source, e) =>
            {
                RemoveWatermak();
            };

            LostFocus += (source, e) =>
            {
                ApplyWatermark();
            };

        }

        /// <summary>
        /// Remove watermark from the textbox
        /// </summary>
        public void RemoveWatermak()
        {
            if (this._WatermarkActive)
            {
                this._WatermarkActive = false;
                this.Text = String.Empty;
                this.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Applywatermak immediately
        /// </summary>
        public void ApplyWatermark()
        {
            if (!this._WatermarkActive && string.IsNullOrEmpty(this.Text) || ForeColor == Color.Gray)
            {
                this._WatermarkActive = true;
                this.Text = _WatermarkText;
                this.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Apply watermak to the textbox. 
        /// </summary>
        /// <param name="WatermarkText">Text to apply</param>
        public void ApplyWatermark(string WatermarkText)
        {
            this.WatermarkText = WatermarkText;
            ApplyWatermark();
        }

    }
}
