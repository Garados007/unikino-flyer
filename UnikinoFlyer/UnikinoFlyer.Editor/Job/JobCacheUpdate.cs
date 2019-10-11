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
    public partial class JobCacheUpdate : UserControl
    {
        public event EventHandler Finished;

        public JobCacheUpdate()
        {
            InitializeComponent();
        }
    }
}
