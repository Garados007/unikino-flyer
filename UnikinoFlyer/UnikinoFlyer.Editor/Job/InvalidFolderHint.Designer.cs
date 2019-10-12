namespace UnikinoFlyer.Editor.Job
{
    partial class InvalidFolderHint
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
            this.SuspendLayout();
            // 
            // InvalidFolderHint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Image = global::UnikinoFlyer.Editor.Properties.Resources.icons8_folder_128;
            this.Name = "InvalidFolderHint";
            this.Option1 = "Ordner erstellen";
            this.Option2 = "Konfiguration öffnen";
            this.Text = "Der lokale Cache Ordner für Bilder existiert nicht";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
