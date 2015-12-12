namespace Anathema
{
    partial class GUIResults
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
            this.AddressListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LabelHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddressListView
            // 
            this.AddressListView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.AddressListView.BackColor = System.Drawing.SystemColors.Control;
            this.AddressListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.LabelHeader,
            this.columnHeader2});
            this.AddressListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddressListView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressListView.FullRowSelect = true;
            this.AddressListView.Location = new System.Drawing.Point(0, 0);
            this.AddressListView.Name = "AddressListView";
            this.AddressListView.Size = new System.Drawing.Size(284, 244);
            this.AddressListView.TabIndex = 151;
            this.AddressListView.UseCompatibleStateImageBehavior = false;
            this.AddressListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Address";
            this.columnHeader1.Width = 86;
            // 
            // LabelHeader
            // 
            this.LabelHeader.Text = "Label";
            this.LabelHeader.Width = 86;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            // 
            // AddressCount
            // 
            this.AddressCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddressCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AddressCount.Location = new System.Drawing.Point(0, 244);
            this.AddressCount.Name = "AddressCount";
            this.AddressCount.Size = new System.Drawing.Size(284, 17);
            this.AddressCount.TabIndex = 152;
            this.AddressCount.Text = "Items: 0";
            // 
            // GUIResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.AddressListView);
            this.Controls.Add(this.AddressCount);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUIResults";
            this.Text = "Results";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView AddressListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader LabelHeader;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label AddressCount;
    }
}