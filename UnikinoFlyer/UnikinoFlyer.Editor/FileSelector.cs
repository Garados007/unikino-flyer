using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnikinoFlyer.Editor
{
    public partial class FileSelector : UserControl
    {
        public FileSelector()
        {
            InitializeComponent();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool SearchFolder { get; set; }

        public string FileName
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public FolderBrowserDialog FolderDialog => folder;

        private void button1_Click(object sender, EventArgs e)
        {
            if (!SearchFolder)
            {
                dialog.FileName = textBox1.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = dialog.FileName;
                }
            }
            else
            {
                folder.SelectedPath = textBox1.Text;
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = folder.SelectedPath;
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public FileDialog FileDialog => dialog;

        public event EventHandler FileChanged;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FileChanged?.Invoke(this, e);
        }
    }
}
