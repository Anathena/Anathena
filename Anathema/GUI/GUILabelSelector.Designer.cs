namespace Anathema.GUI
{
    partial class GUILabelSelector
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
            this.AddressListView = new System.Windows.Forms.ListView();
            this._AddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddressListView
            // 
            this.AddressListView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.AddressListView.BackColor = System.Drawing.SystemColors.Control;
            this.AddressListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._AddressHeader,
            this._ValueHeader});
            this.AddressListView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressListView.FullRowSelect = true;
            this.AddressListView.Location = new System.Drawing.Point(1, 1);
            this.AddressListView.Name = "AddressListView";
            this.AddressListView.Size = new System.Drawing.Size(212, 234);
            this.AddressListView.TabIndex = 117;
            this.AddressListView.UseCompatibleStateImageBehavior = false;
            this.AddressListView.View = System.Windows.Forms.View.Details;
            this.AddressListView.VirtualMode = true;
            // 
            // _AddressHeader
            // 
            this._AddressHeader.Text = "Address";
            this._AddressHeader.Width = 86;
            // 
            // _ValueHeader
            // 
            this._ValueHeader.Text = "Value";
            this._ValueHeader.Width = 86;
            // 
            // AddressCount
            // 
            this.AddressCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddressCount.Location = new System.Drawing.Point(0, 234);
            this.AddressCount.Name = "AddressCount";
            this.AddressCount.Size = new System.Drawing.Size(213, 17);
            this.AddressCount.TabIndex = 124;
            this.AddressCount.Text = "Items: 0";
            // 
            // GUILabelSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AddressCount);
            this.Controls.Add(this.AddressListView);
            this.Name = "GUILabelSelector";
            this.Size = new System.Drawing.Size(267, 289);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView AddressListView;
        private System.Windows.Forms.ColumnHeader _AddressHeader;
        private System.Windows.Forms.ColumnHeader _ValueHeader;
        private System.Windows.Forms.Label AddressCount;
    }
}
