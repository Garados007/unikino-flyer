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
    public partial class ConfigHint : JobHintBase
    {
        public ConfigHint()
        {
            InitializeComponent();
            Option1Click += ConfigHint_Option1Click;
        }

        private void ConfigHint_Option1Click(object sender, EventArgs e)
        {
            DoSwitchMainControl(new ConfigEditor());
        }
    }
}
