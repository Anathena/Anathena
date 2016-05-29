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
            this.AddScriptButton = new System.Windows.Forms.ToolStripButton();
            this.TableToolStrip = new System.Windows.Forms.ToolStrip();
            this.ScriptTableContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScriptTableListView = new Anathema.GUI.CheckableListView();
            this.ScriptActiveHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ScriptDescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TableToolStrip.SuspendLayout();
            this.ScriptTableContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddScriptButton
            // 
            this.AddScriptButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddScriptButton.Image = global::Anathema.Properties.Resources.Increased;
            this.AddScriptButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddScriptButton.Name = "AddScriptButton";
            this.AddScriptButton.Size = new System.Drawing.Size(21, 20);
            this.AddScriptButton.Text = "Add New Script";
            this.AddScriptButton.Click += new System.EventHandler(this.AddScriptButton_Click);
            // 
            // TableToolStrip
            // 
            this.TableToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.TableToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.TableToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddScriptButton});
            this.TableToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.TableToolStrip.Location = new System.Drawing.Point(459, 0);
            this.TableToolStrip.Name = "TableToolStrip";
            this.TableToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.TableToolStrip.Size = new System.Drawing.Size(24, 225);
            this.TableToolStrip.TabIndex = 150;
            // 
            // ScriptTableContextMenuStrip
            // 
            this.ScriptTableContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewScriptToolStripMenuItem,
            this.OpenScriptToolStripMenuItem,
            this.DeleteScriptToolStripMenuItem});
            this.ScriptTableContextMenuStrip.Name = "RightClickMenu";
            this.ScriptTableContextMenuStrip.Size = new System.Drawing.Size(141, 70);
            // 
            // NewScriptToolStripMenuItem
            // 
            this.NewScriptToolStripMenuItem.Name = "NewScriptToolStripMenuItem";
            this.NewScriptToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.NewScriptToolStripMenuItem.Text = "New Script";
            this.NewScriptToolStripMenuItem.Click += new System.EventHandler(this.NewScriptToolStripMenuItem_Click);
            // 
            // OpenScriptToolStripMenuItem
            // 
            this.OpenScriptToolStripMenuItem.Name = "OpenScriptToolStripMenuItem";
            this.OpenScriptToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.OpenScriptToolStripMenuItem.Text = "Open Script";
            this.OpenScriptToolStripMenuItem.Click += new System.EventHandler(this.OpenScriptToolStripMenuItem_Click);
            // 
            // DeleteScriptToolStripMenuItem
            // 
            this.DeleteScriptToolStripMenuItem.Name = "DeleteScriptToolStripMenuItem";
            this.DeleteScriptToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.DeleteScriptToolStripMenuItem.Text = "Delete Script";
            this.DeleteScriptToolStripMenuItem.Click += new System.EventHandler(this.DeleteScriptToolStripMenuItem_Click);
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
        private System.Windows.Forms.ToolStripButton AddScriptButton;
        private System.Windows.Forms.ToolStrip TableToolStrip;
        private GUI.CheckableListView ScriptTableListView;
        private System.Windows.Forms.ColumnHeader ScriptActiveHeader;
        private System.Windows.Forms.ColumnHeader ScriptDescriptionHeader;
        private System.Windows.Forms.ContextMenuStrip ScriptTableContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewScriptToolStripMenuItem;
    }
}