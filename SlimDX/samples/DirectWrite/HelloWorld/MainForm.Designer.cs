namespace HelloWorld
{
	partial class MainForm
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
			this.TabControl = new System.Windows.Forms.TabControl();
			this.simpleTextTabPage = new System.Windows.Forms.TabPage();
			this.multiformattedTextTabPage = new System.Windows.Forms.TabPage();
			this.customTextRendererTabPage = new System.Windows.Forms.TabPage();
			this.SimpleTextRenderPanel = new HelloWorld.RenderPanel();
			this.MultiformattedTextRenderPanel = new HelloWorld.RenderPanel();
			this.CustomTextRenderPanel = new HelloWorld.RenderPanel();
			this.TabControl.SuspendLayout();
			this.simpleTextTabPage.SuspendLayout();
			this.multiformattedTextTabPage.SuspendLayout();
			this.customTextRendererTabPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// TabControl
			// 
			this.TabControl.Controls.Add(this.simpleTextTabPage);
			this.TabControl.Controls.Add(this.multiformattedTextTabPage);
			this.TabControl.Controls.Add(this.customTextRendererTabPage);
			this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabControl.Location = new System.Drawing.Point(0, 0);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(629, 449);
			this.TabControl.TabIndex = 0;
			// 
			// simpleTextTabPage
			// 
			this.simpleTextTabPage.Controls.Add(this.SimpleTextRenderPanel);
			this.simpleTextTabPage.Location = new System.Drawing.Point(4, 22);
			this.simpleTextTabPage.Name = "simpleTextTabPage";
			this.simpleTextTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.simpleTextTabPage.Size = new System.Drawing.Size(621, 423);
			this.simpleTextTabPage.TabIndex = 0;
			this.simpleTextTabPage.Text = "Simple Text";
			this.simpleTextTabPage.UseVisualStyleBackColor = true;
			// 
			// multiformattedTextTabPage
			// 
			this.multiformattedTextTabPage.Controls.Add(this.MultiformattedTextRenderPanel);
			this.multiformattedTextTabPage.Location = new System.Drawing.Point(4, 22);
			this.multiformattedTextTabPage.Name = "multiformattedTextTabPage";
			this.multiformattedTextTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.multiformattedTextTabPage.Size = new System.Drawing.Size(621, 423);
			this.multiformattedTextTabPage.TabIndex = 1;
			this.multiformattedTextTabPage.Text = "Multiformatted Text";
			this.multiformattedTextTabPage.UseVisualStyleBackColor = true;
			// 
			// customTextRendererTabPage
			// 
			this.customTextRendererTabPage.Controls.Add(this.CustomTextRenderPanel);
			this.customTextRendererTabPage.Location = new System.Drawing.Point(4, 22);
			this.customTextRendererTabPage.Name = "customTextRendererTabPage";
			this.customTextRendererTabPage.Size = new System.Drawing.Size(621, 423);
			this.customTextRendererTabPage.TabIndex = 2;
			this.customTextRendererTabPage.Text = "Custom Text Renderer";
			this.customTextRendererTabPage.UseVisualStyleBackColor = true;
			// 
			// SimpleTextRenderPanel
			// 
			this.SimpleTextRenderPanel.BackColor = System.Drawing.Color.White;
			this.SimpleTextRenderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SimpleTextRenderPanel.Location = new System.Drawing.Point(3, 3);
			this.SimpleTextRenderPanel.Name = "SimpleTextRenderPanel";
			this.SimpleTextRenderPanel.Size = new System.Drawing.Size(615, 417);
			this.SimpleTextRenderPanel.TabIndex = 0;
			// 
			// MultiformattedTextRenderPanel
			// 
			this.MultiformattedTextRenderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MultiformattedTextRenderPanel.Location = new System.Drawing.Point(3, 3);
			this.MultiformattedTextRenderPanel.Name = "MultiformattedTextRenderPanel";
			this.MultiformattedTextRenderPanel.Size = new System.Drawing.Size(615, 417);
			this.MultiformattedTextRenderPanel.TabIndex = 0;
			// 
			// CustomTextRenderPanel
			// 
			this.CustomTextRenderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CustomTextRenderPanel.Location = new System.Drawing.Point(0, 0);
			this.CustomTextRenderPanel.Name = "CustomTextRenderPanel";
			this.CustomTextRenderPanel.Size = new System.Drawing.Size(621, 423);
			this.CustomTextRenderPanel.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(629, 449);
			this.Controls.Add(this.TabControl);
			this.Name = "MainForm";
			this.Text = "Hello World";
			this.TabControl.ResumeLayout(false);
			this.simpleTextTabPage.ResumeLayout(false);
			this.multiformattedTextTabPage.ResumeLayout(false);
			this.customTextRendererTabPage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabPage simpleTextTabPage;
		private System.Windows.Forms.TabPage multiformattedTextTabPage;
		private System.Windows.Forms.TabPage customTextRendererTabPage;
		public RenderPanel SimpleTextRenderPanel;
		public RenderPanel MultiformattedTextRenderPanel;
		public RenderPanel CustomTextRenderPanel;
		public System.Windows.Forms.TabControl TabControl;
	}
}