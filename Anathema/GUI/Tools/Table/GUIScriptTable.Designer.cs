namespace Anathema
{
    partial class GUIScriptTable
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIScriptTable));
            this.AddAddressButton = new System.Windows.Forms.ToolStripButton();
            this.TableToolStrip = new System.Windows.Forms.ToolStrip();
            this.ScriptTableListView = new Anathema.GUI.CheckableListView();
            this.ScriptActiveHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ScriptDescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ScriptTableContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TableToolStrip.SuspendLayout();
            this.ScriptTableContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddAddressButton
            // 
            this.AddAddressButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddAddressButton.Image = global::Anathema.Properties.Resources.Increased;
            this.AddAddressButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddAddressButton.Name = "AddAddressButton";
            this.AddAddressButton.Size = new System.Drawing.Size(21, 20);
            this.AddAddressButton.Text = "Add New Address";
            // 
            // TableToolStrip
            // 
            this.TableToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.TableToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.TableToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddAddressButton});
            this.TableToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.TableToolStrip.Location = new System.Drawing.Point(459, 0);
            this.TableToolStrip.Name = "TableToolStrip";
            this.TableToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.TableToolStrip.Size = new System.Drawing.Size(24, 225);
            this.TableToolStrip.TabIndex = 150;
            // 
            // ScriptTableListView
            // 
            this.ScriptTableListView.AllowDrop = true;
            this.ScriptTableListView.BackColor = System.Drawing.SystemColors.Control;
            this.ScriptTableListView.CheckBoxes = true;
            this.ScriptTableListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ScriptActiveHeader,
            this.ScriptDescriptionHeader});
            this.ScriptTableListView.ContextMenuStrip = this.ScriptTableContextMenuStrip;
            this.ScriptTableListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScriptTableListView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScriptTableListView.FullRowSelect = true;
            this.ScriptTableListView.Location = new System.Drawing.Point(0, 0);
            this.ScriptTableListView.MultiSelect = false;
            this.ScriptTableListView.Name = "ScriptTableListView";
            this.ScriptTableListView.OwnerDraw = true;
            this.ScriptTableListView.ShowGroups = false;
            this.ScriptTableListView.Size = new System.Drawing.Size(459, 225);
            this.ScriptTableListView.TabIndex = 151;
            this.ScriptTableListView.UseCompatibleStateImageBehavior = false;
            this.ScriptTableListView.View = System.Windows.Forms.View.Details;
            this.ScriptTableListView.VirtualMode = true;
            this.ScriptTableListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ScriptTableListView_ItemDrag);
            this.ScriptTableListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ScriptTableListView_RetrieveVirtualItem);
            this.ScriptTableListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ScriptTableListView_DragDrop);
            this.ScriptTableListView.DragOver += new System.Windows.Forms.DragEventHandler(this.ScriptTableListView_DragOver);
            this.ScriptTableListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScriptTableListView_MouseClick);
            this.ScriptTableListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ScriptTableListView_MouseDoubleClick);
            // 
            // ScriptActiveHeader
            // 
            this.ScriptActiveHeader.Text = "";
            this.ScriptActiveHeader.Width = 25;
            // 
            // ScriptDescriptionHeader
            // 
            this.ScriptDescriptionHeader.Text = "Description";
            this.ScriptDescriptionHeader.Width = 191;
            // 
            // ScriptTableContextMenuStrip
            // 
            this.ScriptTableContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenScriptToolStripMenuItem,
            this.DeleteScriptToolStripMenuItem});
            this.ScriptTableContextMenuStrip.Name = "RightClickMenu";
            this.ScriptTableContextMenuStrip.Size = new System.Drawing.Size(141, 48);
            // 
            // OpenScriptToolStripMenuItem
            // 
            this.OpenScriptToolStripMenuItem.Name = "OpenScriptToolStripMenuItem";
            this.OpenScriptToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.OpenScriptToolStripMenuItem.Text = "Open Script";
            // 
            // DeleteScriptToolStripMenuItem
            // 
            this.DeleteScriptToolStripMenuItem.Name = "DeleteScriptToolStripMenuItem";
            this.DeleteScriptToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.DeleteScriptToolStripMenuItem.Text = "Delete Script";
            // 
            // GUIScriptTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 225);
            this.Controls.Add(this.ScriptTableListView);
            this.Controls.Add(this.TableToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUIScriptTable";
            this.Text = "Scripts";
            this.TableToolStrip.ResumeLayout(false);
            this.TableToolStrip.PerformLayout();
            this.ScriptTableContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripButton AddAddressButton;
        private System.Windows.Forms.ToolStrip TableToolStrip;
        private GUI.CheckableListView ScriptTableListView;
        private System.Windows.Forms.ColumnHeader ScriptActiveHeader;
        private System.Windows.Forms.ColumnHeader ScriptDescriptionHeader;
        private System.Windows.Forms.ContextMenuStrip ScriptTableContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteScriptToolStripMenuItem;
    }
}