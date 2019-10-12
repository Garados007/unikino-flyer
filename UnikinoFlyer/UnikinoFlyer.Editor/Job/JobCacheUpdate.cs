using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaxLib.Data.IniFiles;
using System.IO;

namespace UnikinoFlyer.Editor.Job
{
    public partial class JobCacheUpdate : DualControlBase
    {
        OptionsLoader config;
        string cacheDir;

        TimeSpan delaySpan = new TimeSpan(0, 0, 5);

        public JobCacheUpdate()
        {
            InitializeComponent();
            Load += JobCacheUpdate_Load;
        }

        public JobCacheUpdate(TimeSpan delayIfTimeout)
            : this()
        {
            delaySpan = delayIfTimeout;
        }

        private void JobCacheUpdate_Load(object sender, EventArgs e)
        {
            config = new OptionsLoader("config.ini", false);
            cacheDir = config[0].Options.GetString("ImageMirror", "");
            if (!Directory.Exists(cacheDir))
            {
                DoSwitchMainControl(new InvalidFolderHint(cacheDir));
                return;
            }

            var newDelay = delaySpan + delaySpan;
            if (newDelay >= new TimeSpan(1, 30, 0))
                newDelay = new TimeSpan(1, 30, 0);
            DoSwitchMainControl(new NoConnection(
                new JobCacheUpdate(newDelay), delaySpan));
        }
    }
}
