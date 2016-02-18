namespace Gecko
{
	partial class PromptDialog
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
			this.label = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.checkBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label
			// 
			this.label.Location = new System.Drawing.Point(8, 8);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(328, 52);
			this.label.TabIndex = 0;
			this.label.Text = "label";
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(340, 8);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(340, 36);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// txtResult
			// 
			this.txtResult.Location = new System.Drawing.Point(8, 68);
			this.txtResult.Name = "txtResult";
			this.txtResult.Size = new System.Drawing.Size(408, 21);
			this.txtResult.TabIndex = 1;
			// 
			// checkBox
			// 
			this.checkBox.AutoSize = true;
			this.checkBox.Location = new System.Drawing.Point(8, 100);
			this.checkBox.Name = "checkBox";
			this.checkBox.Size = new System.Drawing.Size(15, 14);
			this.checkBox.TabIndex = 2;
			this.checkBox.UseVisualStyleBackColor = true;
			// 
			// PromptDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(423, 123);
			this.Controls.Add(this.checkBox);
			this.Controls.Add(this.txtResult);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PromptDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Javascript Prompt";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.CheckBox checkBox;
	}
}