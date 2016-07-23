namespace Anathema.GUI
{
    partial class GUIPropertyViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIPropertyViewer));
            this.EntryDescription = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.PropertyColumn = new Aga.Controls.Tree.TreeColumn();
            this.ValueColumn = new Aga.Controls.Tree.TreeColumn();
            this.EntryValue = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // EntryDescription
            // 
            this.EntryDescription.DataPropertyName = "EntryDescription";
            this.EntryDescription.IncrementalSearchEnabled = true;
            this.EntryDescription.LeftMargin = 3;
            this.EntryDescription.ParentColumn = null;
            // 
            // PropertyColumn
            // 
            this.PropertyColumn.Header = "Property";
            this.PropertyColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.PropertyColumn.TooltipText = null;
            this.PropertyColumn.Width = 128;
            // 
            // ValueColumn
            // 
            this.ValueColumn.Header = "Value";
            this.ValueColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.ValueColumn.TooltipText = null;
            this.ValueColumn.Width = 128;
            // 
            // EntryValue
            // 
            this.EntryValue.DataPropertyName = "EntryValue";
            this.EntryValue.IncrementalSearchEnabled = true;
            this.EntryValue.LeftMargin = 3;
            this.EntryValue.ParentColumn = null;
            // 
            // PropertyGrid
            // 
            this.PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.PropertyGrid.Name = "PropertyGrid";
            this.PropertyGrid.Size = new System.Drawing.Size(263, 216);
            this.PropertyGrid.TabIndex = 3;
            // 
            // GUIPropertyViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 216);
            this.Controls.Add(this.PropertyGrid);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUIPropertyViewer";
            this.Text = "Properties";
            this.ResumeLayout(false);

        }

        #endregion
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryDescription;
        private Aga.Controls.Tree.TreeColumn PropertyColumn;
        private Aga.Controls.Tree.TreeColumn ValueColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox EntryValue;
        private System.Windows.Forms.PropertyGrid PropertyGrid;
    }
}