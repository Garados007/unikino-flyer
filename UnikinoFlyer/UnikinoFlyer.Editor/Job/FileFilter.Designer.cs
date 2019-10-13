namespace UnikinoFlyer.Editor.Job
{
    partial class FileFilter
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileFilter));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.listView = new System.Windows.Forms.ListView();
            this.images = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.showUnsorted = new System.Windows.Forms.ToolStripButton();
            this.showVisible = new System.Windows.Forms.ToolStripButton();
            this.showHidden = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.doVisible = new System.Windows.Forms.ToolStripButton();
            this.doHide = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.listView);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(590, 424);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(590, 449);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // listView
            // 
            this.listView.CheckBoxes = true;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.images;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(590, 424);
            this.listView.SmallImageList = this.images;
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.listView_ItemMouseHover);
            // 
            // images
            // 
            this.images.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.images.ImageSize = new System.Drawing.Size(64, 64);
            this.images.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showUnsorted,
            this.showVisible,
            this.showHidden,
            this.toolStripSeparator1,
            this.doVisible,
            this.doHide});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(348, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // showUnsorted
            // 
            this.showUnsorted.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showUnsorted.Image = ((System.Drawing.Image)(resources.GetObject("showUnsorted.Image")));
            this.showUnsorted.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showUnsorted.Name = "showUnsorted";
            this.showUnsorted.Size = new System.Drawing.Size(63, 22);
            this.showUnsorted.Text = "Unsortiert";
            this.showUnsorted.Click += new System.EventHandler(this.showUnsorted_Click);
            // 
            // showVisible
            // 
            this.showVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showVisible.Image = ((System.Drawing.Image)(resources.GetObject("showVisible.Image")));
            this.showVisible.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showVisible.Name = "showVisible";
            this.showVisible.Size = new System.Drawing.Size(85, 22);
            this.showVisible.Text = "Anzuzeigende";
            this.showVisible.Click += new System.EventHandler(this.showVisible_Click);
            // 
            // showHidden
            // 
            this.showHidden.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showHidden.Image = ((System.Drawing.Image)(resources.GetObject("showHidden.Image")));
            this.showHidden.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showHidden.Name = "showHidden";
            this.showHidden.Size = new System.Drawing.Size(64, 22);
            this.showHidden.Text = "Versteckte";
            this.showHidden.Click += new System.EventHandler(this.showHidden_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // doVisible
            // 
            this.doVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.doVisible.Image = ((System.Drawing.Image)(resources.GetObject("doVisible.Image")));
            this.doVisible.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.doVisible.Name = "doVisible";
            this.doVisible.Size = new System.Drawing.Size(60, 22);
            this.doVisible.Text = "Anzeigen";
            this.doVisible.Click += new System.EventHandler(this.doVisible_Click);
            // 
            // doHide
            // 
            this.doHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.doHide.Image = ((System.Drawing.Image)(resources.GetObject("doHide.Image")));
            this.doHide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.doHide.Name = "doHide";
            this.doHide.Size = new System.Drawing.Size(67, 22);
            this.doHide.Text = "Verstecken";
            this.doHide.Click += new System.EventHandler(this.doHide_Click);
            // 
            // FileFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "FileFilter";
            this.Size = new System.Drawing.Size(590, 449);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ImageList images;
        private System.Windows.Forms.ToolStripButton showUnsorted;
        private System.Windows.Forms.ToolStripButton showVisible;
        private System.Windows.Forms.ToolStripButton showHidden;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton doVisible;
        private System.Windows.Forms.ToolStripButton doHide;
    }
}
