using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
namespace Gecko
{
	partial class ConfirmDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private Button button1;
		private Button button2;
		private Button button3;
		private CheckBox checkBox;
		private Label label;
		private Label label1;
		private Label label2;
		private Panel panel1;
		private PictureBox pictureBox;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.checkBox = new System.Windows.Forms.CheckBox();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.label = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Retry;
			this.button1.Location = new System.Drawing.Point(159, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(239, 8);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button3.Location = new System.Drawing.Point(319, 8);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 2;
			this.button3.UseVisualStyleBackColor = true;
			// 
			// checkBox
			// 
			this.checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
					| System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkBox.Location = new System.Drawing.Point(8, 46);
			this.checkBox.Name = "checkBox";
			this.checkBox.Size = new System.Drawing.Size(385, 16);
			this.checkBox.TabIndex = 3;
			this.checkBox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkBox.UseVisualStyleBackColor = true;
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(12, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(32, 32);
			this.pictureBox.TabIndex = 4;
			this.pictureBox.TabStop = false;
			this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
			// 
			// label
			// 
			this.label.Location = new System.Drawing.Point(52, 12);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(338, 63);
			this.label.TabIndex = 5;
			this.label.Text = "label";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.checkBox);
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 92);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(401, 70);
			this.panel1.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.label2.Location = new System.Drawing.Point(0, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(2000, 1);
			this.label2.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.label1.Location = new System.Drawing.Point(0, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(2000, 1);
			this.label1.TabIndex = 4;
			// 
			// ConfirmDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(401, 162);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label);
			this.Controls.Add(this.pictureBox);
			this.FormBorderStyle = FormBorderStyle.Sizable;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfirmDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ConfirmDialog";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
	}
}