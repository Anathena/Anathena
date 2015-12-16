namespace Anathema
{
    partial class GUITable
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
            this.TableListView = new System.Windows.Forms.ListView();
            this.CheckBoxHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // TableListView
            // 
            this.TableListView.BackColor = System.Drawing.SystemColors.Control;
            this.TableListView.CheckBoxes = true;
            this.TableListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CheckBoxHeader,
            this.DescriptionHeader,
            this.TypeHeader,
            this.ValueHeader});
            this.TableListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableListView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TableListView.FullRowSelect = true;
            this.TableListView.Location = new System.Drawing.Point(0, 0);
            this.TableListView.Name = "TableListView";
            this.TableListView.ShowGroups = false;
            this.TableListView.Size = new System.Drawing.Size(562, 251);
            this.TableListView.TabIndex = 143;
            this.TableListView.UseCompatibleStateImageBehavior = false;
            this.TableListView.View = System.Windows.Forms.View.Details;
            // 
            // CheckBoxHeader
            // 
            this.CheckBoxHeader.Text = "";
            this.CheckBoxHeader.Width = 24;
            // 
            // DescriptionHeader
            // 
            this.DescriptionHeader.Text = "Description";
            this.DescriptionHeader.Width = 146;
            // 
            // TypeHeader
            // 
            this.TypeHeader.Text = "Type";
            this.TypeHeader.Width = 70;
            // 
            // ValueHeader
            // 
            this.ValueHeader.Text = "Value";
            this.ValueHeader.Width = 143;
            // 
            // GUITable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 251);
            this.Controls.Add(this.TableListView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUITable";
            this.Text = "Table";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView TableListView;
        private System.Windows.Forms.ColumnHeader CheckBoxHeader;
        private System.Windows.Forms.ColumnHeader DescriptionHeader;
        private System.Windows.Forms.ColumnHeader TypeHeader;
        private System.Windows.Forms.ColumnHeader ValueHeader;
    }
}