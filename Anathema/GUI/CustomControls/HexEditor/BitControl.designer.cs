namespace Be.HexEditor
{
	partial class BitControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BitControl));
            this.lblValue = new System.Windows.Forms.Label();
            this.lblBit = new System.Windows.Forms.Label();
            this.pnBitsEditor = new System.Windows.Forms.Panel();
            this.pnBitsHeader = new System.Windows.Forms.Panel();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblValue
            // 
            resources.ApplyResources(this.lblValue, "lblValue");
            this.lblValue.Name = "lblValue";
            // 
            // lblBit
            // 
            resources.ApplyResources(this.lblBit, "lblBit");
            this.lblBit.Name = "lblBit";
            // 
            // pnBitsEditor
            // 
            resources.ApplyResources(this.pnBitsEditor, "pnBitsEditor");
            this.pnBitsEditor.Name = "pnBitsEditor";
            // 
            // pnBitsHeader
            // 
            resources.ApplyResources(this.pnBitsHeader, "pnBitsHeader");
            this.flowLayoutPanel.SetFlowBreak(this.pnBitsHeader, true);
            this.pnBitsHeader.Name = "pnBitsHeader";
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.lblBit);
            this.flowLayoutPanel.Controls.Add(this.pnBitsHeader);
            this.flowLayoutPanel.Controls.Add(this.lblValue);
            this.flowLayoutPanel.Controls.Add(this.pnBitsEditor);
            resources.ApplyResources(this.flowLayoutPanel, "flowLayoutPanel");
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            // 
            // BitControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.flowLayoutPanel);
            this.Name = "BitControl";
            this.flowLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label lblBit;
        private System.Windows.Forms.Panel pnBitsEditor;
        private System.Windows.Forms.Panel pnBitsHeader;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
	}
}
