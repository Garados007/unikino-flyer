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
    public partial class JobHintBase : DualControlBase
    {
        public JobHintBase()
        {
            InitializeComponent();
            EnableSideControl = false;
        }

        public Image Image
        {
            get => image.BackgroundImage;
            set => image.BackgroundImage = value;
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get => taskText.Text;
            set => taskText.Text = value;
        }

        public string Option1
        {
            get => option1.Text;
            set => option1.Text = value;
        }

        public string Option2
        {
            get => option2.Text;
            set => option2.Text = value;
        }

        public bool EnableOption2
        {
            get => option2.Visible;
            set
            {
                if (option2.Visible = value)
                {
                    tableLayoutPanel2.SetColumnSpan(option1, 1);
                }
                else
                {
                    tableLayoutPanel2.SetColumnSpan(option1, 2);
                }
            }
        }

        public event EventHandler Option1Click, Option2Click;

        private void option2_Click(object sender, EventArgs e)
        {
            Option2Click?.Invoke(this, e);
        }

        private void option1_Click(object sender, EventArgs e)
        {
            Option1Click?.Invoke(this, e);
        }
    }
}
