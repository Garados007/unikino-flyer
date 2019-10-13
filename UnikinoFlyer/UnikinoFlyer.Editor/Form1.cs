using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MaxLib.Data.IniFiles;

namespace UnikinoFlyer.Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!File.Exists("config.ini"))
            {
                SetMainControl(new Job.ConfigHint());
                return;
            }
            var config = new OptionsLoader("config.ini", false);
            var imgMirror = config[0].Options.GetString("ImageMirror", "");
            if (!Directory.Exists(imgMirror))
            {
                SetMainControl(new Job.InvalidFolderHint(imgMirror));
                return;
            }
        }

        #region Main Control Manager

        private DualControlBase mainControl = null;

        public bool SetMainControl(DualControlBase control)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            if (!ReleaseOldControl()) return false;
            mainControl = control;
            mainControl.SideControlChanged += MainControl_SideControlChanged;
            mainControl.EnableSideControlChanged += MainControl_EnableSideControlChanged;
            mainControl.Finished += MainControl_Finished;
            mainControl.SwitchMainControl += MainControl_SwitchMainControl;
            splitContainer1.Panel1Collapsed = !mainControl.EnableSideControl;
            splitContainer1.Panel1.Controls.Clear();
            if (mainControl.EnableSideControl)
            {
                var side = mainControl.SideControl;
                side.Dock = DockStyle.Fill;
                splitContainer1.Panel1.Controls.Add(side);
            }
            control.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(control);
            return true;
        }

        private void MainControl_SwitchMainControl(object sender, DualControlBase e)
        {
            if (!SetMainControl(e))
                throw new InvalidOperationException("cannot change main control. Try it later");
        }

        private void MainControl_Finished(object sender, EventArgs e)
        {
            ControlFinished((DualControlBase)sender);
        }

        private void MainControl_EnableSideControlChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !mainControl.EnableSideControl;
            splitContainer1.Panel1.Controls.Clear();
            if (mainControl.EnableSideControl)
            {
                var side = mainControl.SideControl;
                side.Dock = DockStyle.Fill;
                splitContainer1.Panel1.Controls.Add(side);
            }
        }

        private void MainControl_SideControlChanged(object sender, EventArgs e)
        {
            MainControl_EnableSideControlChanged(sender, e);
        }

        private bool ReleaseOldControl()
        {
            if (mainControl == null) return true;
            if (!mainControl.CloseControl()) return false;
            mainControl.SideControlChanged -= MainControl_SideControlChanged;
            mainControl.EnableSideControlChanged -= MainControl_EnableSideControlChanged;
            mainControl.Finished -= MainControl_Finished;
            mainControl.SwitchMainControl -= MainControl_SwitchMainControl;
            splitContainer1.Panel2.Controls.Clear();
            return true;
        }

        #endregion Main Control Manager

        private void ControlFinished(DualControlBase control)
        {

        }

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog(this);
        }

        private void konfigurationBearbeitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMainControl(new Job.ConfigEditor());
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!mainControl?.CloseControl() ?? false)
            {
                MessageBox.Show("Das Programm kann derzeit nicht geschlossen werden. Beenden Sie bitte Ihre Eingaben!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void bildcacheAktualisierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMainControl(new Job.JobCacheUpdate());
        }

        private void bildcacheFilternToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMainControl(new Job.FileFilter());
        }
    }
}
