namespace Gecko
{
	partial class PropertiesDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.pageGeneral = new Gecko.XPTabPage();
			this.txtTitle = new Gecko.ReadOnlyTextBox();
			this.lblModifiedDate = new System.Windows.Forms.Label();
			this.txtReferrer = new Gecko.ReadOnlyTextBox();
			this.lblSize = new System.Windows.Forms.Label();
			this.lblEncoding = new System.Windows.Forms.Label();
			this.lblDocType = new System.Windows.Forms.Label();
			this.lblContentType = new System.Windows.Forms.Label();
			this.txtAddress = new Gecko.ReadOnlyTextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.pageGeneral.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
					| System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.pageGeneral);
			this.tabControl.Location = new System.Drawing.Point(6, 6);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(338, 350);
			this.tabControl.TabIndex = 0;
			// 
			// pageGeneral
			// 
			this.pageGeneral.Controls.Add(this.txtTitle);
			this.pageGeneral.Controls.Add(this.lblModifiedDate);
			this.pageGeneral.Controls.Add(this.txtReferrer);
			this.pageGeneral.Controls.Add(this.lblSize);
			this.pageGeneral.Controls.Add(this.lblEncoding);
			this.pageGeneral.Controls.Add(this.lblDocType);
			this.pageGeneral.Controls.Add(this.lblContentType);
			this.pageGeneral.Controls.Add(this.txtAddress);
			this.pageGeneral.Controls.Add(this.label7);
			this.pageGeneral.Controls.Add(this.label6);
			this.pageGeneral.Controls.Add(this.label5);
			this.pageGeneral.Controls.Add(this.label4);
			this.pageGeneral.Controls.Add(this.label3);
			this.pageGeneral.Controls.Add(this.label2);
			this.pageGeneral.Controls.Add(this.label1);
			this.pageGeneral.Controls.Add(this.panel1);
			this.pageGeneral.Location = new System.Drawing.Point(4, 22);
			this.pageGeneral.Name = "pageGeneral";
			this.pageGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.pageGeneral.Size = new System.Drawing.Size(330, 324);
			this.pageGeneral.TabIndex = 0;
			this.pageGeneral.Text = "Page";
			this.pageGeneral.UseVisualStyleBackColor = true;
			// 
			// txtTitle
			// 
			this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtTitle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTitle.Location = new System.Drawing.Point(8, 16);
			this.txtTitle.Multiline = true;
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.ReadOnly = true;
			this.txtTitle.Size = new System.Drawing.Size(300, 16);
			this.txtTitle.TabIndex = 17;
			this.txtTitle.Text = "Title";
			this.txtTitle.WordWrap = false;
			// 
			// lblModifiedDate
			// 
			this.lblModifiedDate.AutoSize = true;
			this.lblModifiedDate.Location = new System.Drawing.Point(92, 204);
			this.lblModifiedDate.Name = "lblModifiedDate";
			this.lblModifiedDate.Size = new System.Drawing.Size(70, 13);
			this.lblModifiedDate.TabIndex = 16;
			this.lblModifiedDate.Text = "ModifiedDate";
			this.lblModifiedDate.Visible = false;
			// 
			// txtReferrer
			// 
			this.txtReferrer.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtReferrer.Location = new System.Drawing.Point(92, 108);
			this.txtReferrer.Multiline = true;
			this.txtReferrer.Name = "txtReferrer";
			this.txtReferrer.ReadOnly = true;
			this.txtReferrer.Size = new System.Drawing.Size(234, 13);
			this.txtReferrer.TabIndex = 15;
			this.txtReferrer.Text = "Referrer";
			this.txtReferrer.WordWrap = false;
			// 
			// lblSize
			// 
			this.lblSize.AutoSize = true;
			this.lblSize.Location = new System.Drawing.Point(92, 156);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(26, 13);
			this.lblSize.TabIndex = 14;
			this.lblSize.Text = "Size";
			this.lblSize.Visible = false;
			// 
			// lblEncoding
			// 
			this.lblEncoding.AutoSize = true;
			this.lblEncoding.Location = new System.Drawing.Point(92, 132);
			this.lblEncoding.Name = "lblEncoding";
			this.lblEncoding.Size = new System.Drawing.Size(50, 13);
			this.lblEncoding.TabIndex = 13;
			this.lblEncoding.Text = "Encoding";
			this.lblEncoding.Visible = false;
			// 
			// lblDocType
			// 
			this.lblDocType.AutoSize = true;
			this.lblDocType.Location = new System.Drawing.Point(92, 84);
			this.lblDocType.Name = "lblDocType";
			this.lblDocType.Size = new System.Drawing.Size(49, 13);
			this.lblDocType.TabIndex = 12;
			this.lblDocType.Text = "DocType";
			// 
			// lblContentType
			// 
			this.lblContentType.AutoSize = true;
			this.lblContentType.Location = new System.Drawing.Point(92, 180);
			this.lblContentType.Name = "lblContentType";
			this.lblContentType.Size = new System.Drawing.Size(70, 13);
			this.lblContentType.TabIndex = 11;
			this.lblContentType.Text = "ContentType";
			this.lblContentType.Visible = false;
			// 
			// txtAddress
			// 
			this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtAddress.Location = new System.Drawing.Point(92, 60);
			this.txtAddress.Multiline = true;
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.ReadOnly = true;
			this.txtAddress.Size = new System.Drawing.Size(234, 13);
			this.txtAddress.TabIndex = 10;
			this.txtAddress.Text = "Address";
			this.txtAddress.WordWrap = false;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 204);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(74, 13);
			this.label7.TabIndex = 9;
			this.label7.Text = "Last Modified:";
			this.label7.Visible = false;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 108);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(78, 13);
			this.label6.TabIndex = 8;
			this.label6.Text = "Referring URL:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 156);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(30, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Size:";
			this.label5.Visible = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 132);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Encoding:";
			this.label4.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Doc type:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 180);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Content type:";
			this.label2.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Address:";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Location = new System.Drawing.Point(4, 40);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(320, 16);
			this.panel1.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Location = new System.Drawing.Point(-8, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(338, 24);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(268, 360);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "Close";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// PropertiesDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(350, 390);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tabControl);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PropertiesDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Properties";
			this.tabControl.ResumeLayout(false);
			this.pageGeneral.ResumeLayout(false);
			this.pageGeneral.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private XPTabPage pageGeneral;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblModifiedDate;
		private ReadOnlyTextBox txtReferrer;
		private System.Windows.Forms.Label lblSize;
		private System.Windows.Forms.Label lblEncoding;
		private System.Windows.Forms.Label lblDocType;
		private System.Windows.Forms.Label lblContentType;
		private ReadOnlyTextBox txtAddress;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private ReadOnlyTextBox txtTitle;
	}
}