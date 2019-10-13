using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnikinoFlyer.Editor.Job
{
    public partial class InvalidFolderHint : JobHintBase
    {
        string folder;
        DualControlBase switchTarget;

        public InvalidFolderHint(string folder)
        {
            InitializeComponent();
            Option1Click += InvalidFolderHint_Option1Click;
            Option2Click += InvalidFolderHint_Option2Click;
            this.folder = folder;
        }

        public InvalidFolderHint(string folder, DualControlBase switchTarget)
            : this(folder)
        {
            this.switchTarget = switchTarget;
        }

        private void InvalidFolderHint_Option2Click(object sender, EventArgs e)
        {
            DoSwitchMainControl(new ConfigEditor());   
        }

        private void InvalidFolderHint_Option1Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.Directory.CreateDirectory(folder);
                DoSwitchMainControl(switchTarget ?? new JobCacheUpdate());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Directory couldn't be created." +
                    $"\r\n\r\nException: {ex}",
                    ParentForm.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                DoSwitchMainControl(new ConfigEditor());
            }
            
        }
    }
}
