namespace Anathema
{
    partial class GUIFilterFSM
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
            this.DummyToolStrip = new System.Windows.Forms.ToolStrip();
            this.SuspendLayout();
            // 
            // DummyToolStrip
            // 
            this.DummyToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.DummyToolStrip.Location = new System.Drawing.Point(0, 0);
            this.DummyToolStrip.Name = "DummyToolStrip";
            this.DummyToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.DummyToolStrip.Size = new System.Drawing.Size(287, 25);
            this.DummyToolStrip.TabIndex = 150;
            // 
            // GUIFilterFSM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DummyToolStrip);
            this.Name = "GUIFilterFSM";
            this.Size = new System.Drawing.Size(287, 282);
            this.Load += new System.EventHandler(this.GUIFiniteStateMachine_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GUIFiniteStateMachine_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GUIFiniteStateMachine_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GUIFiniteStateMachine_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip DummyToolStrip;
    }
}
