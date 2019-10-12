namespace UnikinoFlyer.Editor.Job
{
    partial class JobHintBase
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.taskText = new System.Windows.Forms.Label();
            this.option1 = new System.Windows.Forms.Button();
            this.option2 = new System.Windows.Forms.Button();
            this.image = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(599, 452);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.taskText, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.option1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.option2, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.image, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(122, 93);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(353, 265);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // taskText
            // 
            this.taskText.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.taskText, 2);
            this.taskText.Dock = System.Windows.Forms.DockStyle.Top;
            this.taskText.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskText.ForeColor = System.Drawing.SystemColors.Highlight;
            this.taskText.Location = new System.Drawing.Point(3, 208);
            this.taskText.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.taskText.Name = "taskText";
            this.taskText.Size = new System.Drawing.Size(347, 22);
            this.taskText.TabIndex = 1;
            this.taskText.Text = "Bitte erledigen Sie folgende Aufgabe:";
            this.taskText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // option1
            // 
            this.option1.Dock = System.Windows.Forms.DockStyle.Top;
            this.option1.Location = new System.Drawing.Point(3, 239);
            this.option1.Name = "option1";
            this.option1.Size = new System.Drawing.Size(170, 23);
            this.option1.TabIndex = 2;
            this.option1.Text = "button1";
            this.option1.UseVisualStyleBackColor = true;
            this.option1.Click += new System.EventHandler(this.option1_Click);
            // 
            // option2
            // 
            this.option2.Dock = System.Windows.Forms.DockStyle.Top;
            this.option2.Location = new System.Drawing.Point(179, 239);
            this.option2.Name = "option2";
            this.option2.Size = new System.Drawing.Size(171, 23);
            this.option2.TabIndex = 3;
            this.option2.Text = "button2";
            this.option2.UseVisualStyleBackColor = true;
            this.option2.Click += new System.EventHandler(this.option2_Click);
            // 
            // image
            // 
            this.image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tableLayoutPanel2.SetColumnSpan(this.image, 2);
            this.image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.image.Location = new System.Drawing.Point(3, 3);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(347, 196);
            this.image.TabIndex = 4;
            // 
            // JobHintBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "JobHintBase";
            this.Size = new System.Drawing.Size(599, 452);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label taskText;
        private System.Windows.Forms.Button option1;
        private System.Windows.Forms.Button option2;
        private System.Windows.Forms.Panel image;
    }
}
