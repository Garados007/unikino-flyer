namespace UnikinoFlyer.Editor
{
    partial class Form1
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ladenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.zuletztGeöffnetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bildcacheAktualisierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bildUndTextaustauschToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konfigurationBearbeitenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bildcacheFilternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.jobToolStripMenuItem,
            this.konfigurationToolStripMenuItem,
            this.hilfeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neuToolStripMenuItem,
            this.ladenToolStripMenuItem,
            this.toolStripSeparator1,
            this.zuletztGeöffnetToolStripMenuItem,
            this.toolStripSeparator2,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // neuToolStripMenuItem
            // 
            this.neuToolStripMenuItem.Name = "neuToolStripMenuItem";
            this.neuToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.neuToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.neuToolStripMenuItem.Text = "Neu";
            // 
            // ladenToolStripMenuItem
            // 
            this.ladenToolStripMenuItem.Name = "ladenToolStripMenuItem";
            this.ladenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.ladenToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.ladenToolStripMenuItem.Text = "Laden";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(159, 6);
            // 
            // zuletztGeöffnetToolStripMenuItem
            // 
            this.zuletztGeöffnetToolStripMenuItem.Name = "zuletztGeöffnetToolStripMenuItem";
            this.zuletztGeöffnetToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.zuletztGeöffnetToolStripMenuItem.Text = "Zuletzt geöffnet";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(159, 6);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // jobToolStripMenuItem
            // 
            this.jobToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bildcacheAktualisierenToolStripMenuItem,
            this.bildcacheFilternToolStripMenuItem,
            this.toolStripSeparator3,
            this.bildUndTextaustauschToolStripMenuItem});
            this.jobToolStripMenuItem.Name = "jobToolStripMenuItem";
            this.jobToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.jobToolStripMenuItem.Text = "Job";
            // 
            // bildcacheAktualisierenToolStripMenuItem
            // 
            this.bildcacheAktualisierenToolStripMenuItem.Name = "bildcacheAktualisierenToolStripMenuItem";
            this.bildcacheAktualisierenToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.bildcacheAktualisierenToolStripMenuItem.Text = "Bildcache aktualisieren";
            this.bildcacheAktualisierenToolStripMenuItem.Click += new System.EventHandler(this.bildcacheAktualisierenToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(197, 6);
            // 
            // bildUndTextaustauschToolStripMenuItem
            // 
            this.bildUndTextaustauschToolStripMenuItem.Name = "bildUndTextaustauschToolStripMenuItem";
            this.bildUndTextaustauschToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.bildUndTextaustauschToolStripMenuItem.Text = "Bild- und Textaustausch";
            // 
            // konfigurationToolStripMenuItem
            // 
            this.konfigurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.konfigurationBearbeitenToolStripMenuItem});
            this.konfigurationToolStripMenuItem.Name = "konfigurationToolStripMenuItem";
            this.konfigurationToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.konfigurationToolStripMenuItem.Text = "Konfiguration";
            // 
            // konfigurationBearbeitenToolStripMenuItem
            // 
            this.konfigurationBearbeitenToolStripMenuItem.Name = "konfigurationBearbeitenToolStripMenuItem";
            this.konfigurationBearbeitenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.konfigurationBearbeitenToolStripMenuItem.Text = "bearbeiten";
            this.konfigurationBearbeitenToolStripMenuItem.Click += new System.EventHandler(this.konfigurationBearbeitenToolStripMenuItem_Click);
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.überToolStripMenuItem});
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hilfeToolStripMenuItem.Text = "Hilfe";
            // 
            // überToolStripMenuItem
            // 
            this.überToolStripMenuItem.Name = "überToolStripMenuItem";
            this.überToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.überToolStripMenuItem.Text = "Über";
            this.überToolStripMenuItem.Click += new System.EventHandler(this.überToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(800, 387);
            this.splitContainer1.SplitterDistance = 177;
            this.splitContainer1.TabIndex = 1;
            // 
            // bildcacheFilternToolStripMenuItem
            // 
            this.bildcacheFilternToolStripMenuItem.Name = "bildcacheFilternToolStripMenuItem";
            this.bildcacheFilternToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.bildcacheFilternToolStripMenuItem.Text = "Bildcache filtern";
            this.bildcacheFilternToolStripMenuItem.Click += new System.EventHandler(this.bildcacheFilternToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(800, 411);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Unikino Flyer Save file Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ladenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem zuletztGeöffnetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bildcacheAktualisierenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem bildUndTextaustauschToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konfigurationBearbeitenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem überToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem bildcacheFilternToolStripMenuItem;
    }
}

