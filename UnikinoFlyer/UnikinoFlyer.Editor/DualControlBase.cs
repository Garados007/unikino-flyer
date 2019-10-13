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
    public partial class DualControlBase : UserControl
    {
        public DualControlBase()
        {
            InitializeComponent();
        }

        private Control sideControl = null;
        public event EventHandler SideControlChanged;
        
        public Control SideControl
        {
            get => sideControl;
            protected set
            {
                sideControl = value;
                SideControlChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private bool enableSideControl = false;
        public event EventHandler EnableSideControlChanged;

        public bool EnableSideControl
        {
            get => enableSideControl;
            protected set
            {
                enableSideControl = value;
                EnableSideControlChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler Finished;
        protected void DoFinished()
            => Finished?.Invoke(this, EventArgs.Empty);

        public event EventHandler<DualControlBase> SwitchMainControl;

        protected void DoSwitchMainControl(DualControlBase control)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            SwitchMainControl?.Invoke(this, control);
        }

        public virtual bool CloseControl()
        {
            return true;
        }

        public void Invoke(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (InvokeRequired)
                base.Invoke(action);
            else action();
        }
    }
}
