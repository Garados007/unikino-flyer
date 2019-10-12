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
    public partial class NoConnection : JobHintBase
    {
        DualControlBase returnControl;
        TimeSpan delay = new TimeSpan(0, 0, 5);

        public NoConnection(DualControlBase returnControl)
        {
            InitializeComponent();
            if (returnControl == null) throw new ArgumentNullException(nameof(returnControl));
            this.returnControl = returnControl;
            Load += NoConnection_Load;
            Option1Click += NoConnection_Option1Click;
        }

        public NoConnection(DualControlBase returnControl, TimeSpan delay)
            : this(returnControl)
        {
            if (delay.TotalSeconds < 1) throw new ArgumentOutOfRangeException(nameof(delay));
            this.delay = delay;
        }

        DateTime until;

        private void NoConnection_Load(object sender, EventArgs e)
        {
            until = DateTime.Now + delay;
            timer.Enabled = true;
        }

        private void NoConnection_Option1Click(object sender, EventArgs e)
        {
            DoSwitchMainControl(returnControl);
            timer.Enabled = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var left = until - DateTime.Now;
            if (left.TotalSeconds <= 0)
            {
                DoSwitchMainControl(returnControl);
                timer.Enabled = false;
                return;
            }
            else
            {
                Text = $"Keine Verbindung. Erneuter Versuch in " +
                    $"{left:hh\\:mm\\:ss}.";
            }
        }
    }
}
