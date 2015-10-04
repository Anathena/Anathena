using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Be.HexEditor
{
	public partial class BitControl : UserControl
	{
		List<RichTextBox> _txtBits = new List<RichTextBox>();

		public event EventHandler BitChanged;

		protected virtual void OnBitChanged(EventArgs e)
		{
			if(BitChanged != null)
				BitChanged(this, e);
		}

        Panel _innerBorderHeaderPanel;

		Panel _innerBorderPanel;

		public BitControl()
		{
            _innerBorderHeaderPanel = new Panel();
            _innerBorderHeaderPanel.Dock = DockStyle.Fill;
            _innerBorderHeaderPanel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);

            _innerBorderPanel = new Panel();
            _innerBorderPanel.BackColor = Color.White;
            _innerBorderPanel.Dock = DockStyle.Fill;
            _innerBorderPanel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);

			InitializeComponent();

			pnBitsEditor.BackColor = System.Windows.Forms.VisualStyles.VisualStyleInformation.TextControlBorder;

            
            pnBitsHeader.Controls.Add(_innerBorderHeaderPanel);

            bool first = true;
            Size size = new Size();
            int pos = 5;
            for (int i = 7; i > -1; i--)
            {
                Label lbl = new Label();
                lbl.Tag = i;
                lbl.BorderStyle = System.Windows.Forms.BorderStyle.None;
                lbl.Font = new System.Drawing.Font("Consolas", SystemFonts.MessageBoxFont.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbl.Margin = new System.Windows.Forms.Padding(0);

                lbl.Name = "lbl" + i.ToString();

                //lbl.Size = new System.Drawing.Size(14, 14);
                
                lbl.AutoSize = true;
                lbl.Text = i.ToString();
                lbl.Enter += new System.EventHandler(this.txt_Enter);
                lbl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
                _innerBorderHeaderPanel.Controls.Add(lbl);

                if (first)
                {
                    size = lbl.Size;
                    lbl.AutoSize = false;
                    first = false;
                }
                
                lbl.Size = size;
                lbl.Left = pos;
                lbl.Top = 3;
                pos += size.Width;
            }
			
			pnBitsEditor.Controls.Add(_innerBorderPanel);
            pos = 8;
			for(int i = 7; i > -1; i--)
			{
				RichTextBox txt = new RichTextBox();
				txt.Tag = i;
				txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
                txt.Font = new System.Drawing.Font("Consolas", SystemFonts.MessageBoxFont.Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
				txt.Margin = new System.Windows.Forms.Padding(0);
                
				txt.MaxLength = 1;
				txt.Multiline = false;
				txt.Name = "txt" + i.ToString();
                //txt.Size = new System.Drawing.Size(14, 14);
                txt.Size = size;
                txt.Left = pos;
                txt.Top = 6;
                pos += size.Width;
				txt.TabIndex = 10 -i + 7;
				txt.Text = "0";
				txt.Visible = false;
				txt.SelectionChanged += new System.EventHandler(this.txt_SelectionChanged);
				txt.Enter += new System.EventHandler(this.txt_Enter);
				txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
				_innerBorderPanel.Controls.Add(txt);
				_txtBits.Add(txt);
			}
			UpdateView();
		}

		BitInfo _bitInfo;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public BitInfo BitInfo
		{
			get { return _bitInfo; }
			set
			{
				_bitInfo = value;
				UpdateView();
			}
		}

		private void UpdateView()
		{
			foreach(var txt in _txtBits)
				txt.TextChanged -= new EventHandler(txt_TextChanged);

			if (_bitInfo == null)
			{
				foreach (var txt in _txtBits)
				{
					txt.Text = string.Empty;
				}
				pnBitsEditor.Visible = lblValue.Visible = lblBit.Visible = pnBitsHeader.Visible = false;

				return;
			}
			else
			{
				foreach (var txt in _txtBits)
					txt.Visible = true;
				pnBitsEditor.Visible = lblValue.Visible = lblBit.Visible = pnBitsHeader.Visible = true;
			}

			foreach (var txt in _txtBits)
			{
				int bit = (int)txt.Tag;
				txt.Text = _bitInfo.GetBitAsString(bit);
			}

			foreach(var txt in _txtBits)
				txt.TextChanged += new EventHandler(txt_TextChanged);
		}

		int GetBitSetInt(byte b, int pos)
		{
			if (IsBitSet(b, pos))
				return 1;
			else
				return 0;
		}

		bool IsBitSet(byte b, int pos)
		{
			return (b & (1 << pos)) != 0;
		}

		byte SetBit(byte b, int BitNumber)
		{
			//Kleine Fehlerbehandlung
			if (BitNumber < 8 && BitNumber > -1)
			{
				return (byte)(b | (byte)(0x01 << BitNumber));
			}
			else
			{
				throw new InvalidOperationException(
				"Der Wert für BitNumber " + BitNumber.ToString() + " war nicht im zulässigen Bereich! (BitNumber = (min)0 - (max)7)");
			}
		}

		void txt_TextChanged(object sender, EventArgs e)
		{
			var txt = (RichTextBox)sender;
			var index = (int)txt.Tag;
			var value = txt.Text != "0";
			this.BitInfo[index] = value;
			OnBitChanged(EventArgs.Empty);

			NavigateRight((RichTextBox)sender);
		}

		void NavigateLeft(RichTextBox txt)
		{
			var indexOf = _txtBits.IndexOf(txt);

			NavigateTo(indexOf - 1);
		}

		void NavigateRight(RichTextBox txt)
		{
			var indexOf = _txtBits.IndexOf(txt);

			NavigateTo(indexOf + 1);
		}

		void NavigateTo(int indexOf)
		{
			if (indexOf > _txtBits.Count - 1 || indexOf < 0)
				return;

			var txtFocus = false;
			foreach (var txt in _txtBits)
			{
				if (txt.Focused)
				{
					txtFocus = true;
					break;
				}
			}

			if (!txtFocus)
				return;

			var selectBox = _txtBits[indexOf];
			selectBox.Focus();
		}

		void txt_KeyDown(object sender, KeyEventArgs e)
		{
			var txt = (RichTextBox)sender;

			List<Keys> bitKeys = new List<Keys>() { Keys.D0, Keys.D1 };

			var txt7 = _txtBits[0];
			if (txt7.SelectionLength > 1)
				txt7.SelectionLength = 1;

			var modifiersNone = e.Modifiers == Keys.None;
			var updateBit = modifiersNone && bitKeys.Contains(e.KeyCode);

			e.Handled = e.SuppressKeyPress = !updateBit;

			if (!updateBit && modifiersNone)
			{
				switch (e.KeyCode)
				{
					case Keys.Left:
						NavigateLeft(txt);
						break;
					case Keys.Right:
						NavigateRight(txt);
						break;
					case Keys.Home:
						NavigateTo(0);
						break;
					case Keys.End:
						NavigateTo(7);
						break;
				}
			}
		}

		void txt_SelectionChanged(object sender, EventArgs e)
		{
			var txt = (RichTextBox)sender;
			UpdateSelection(txt);
		}

		void UpdateSelection(RichTextBox txt)
		{
			txt.SelectionStart = 0;
			if (txt.SelectionLength == 0)
				txt.SelectionLength = 1;
		}

		void txt_Enter(object sender, EventArgs e)
		{
			var txt = (RichTextBox)sender;
			UpdateSelection(txt);
		}
   }
}
