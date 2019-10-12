using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MaxLib.Data.IniFiles;

namespace UnikinoFlyer.Editor.Job
{
    public partial class ConfigEditor : DualControlBase
    {
        OptionsLoader config;

        public ConfigEditor()
        {
            InitializeComponent();
            EnableSideControl = false;
        }

        bool load = false;
        protected override void OnLoad(EventArgs e)
        {
            load = true;
            base.OnLoad(e);
            if (File.Exists("config.ini"))
            {
                config = new OptionsLoader("config.ini", false);
                fileSelector1.FileName = config[0].Options.GetString("ImageMirror", "");
                var group = config.Get("Uploader")?.Options;
                if (group != null)
                {
                    tbUserName.Text = group.GetString("User", "");
                    tbPassword.Text = group.GetString("PW", "");
                    tbBaseUrl.Text = group.GetString("BaseUrl", "");
                    tbLoginCheck.Text = group.GetString("Login Check", "");
                    tbEditImageId1.Text = group.GetString("EditImageId", "");
                    tbPageId.Text = group.GetString("PageId", "");
                }
            }
            else config = new OptionsLoader();
            load = false;
        }

        ulong changeId = 0;

        private void tbChanged(object sender, EventArgs e)
        {
            if (load) return;

            //main group
            var opts = config[0].Options;
            opts.Clear();
            opts.FastAdd("ImageMirror", fileSelector1.FileName);

            //group: Uploader
            var group = config.Get("Uploader") ?? config.Add("Uploader");
            opts = group.Options;
            opts.Clear();
            opts.FastAdd("User", tbUserName.Text);
            opts.FastAdd("PW", tbPassword.Text);
            opts.FastAdd("BaseUrl", tbBaseUrl.Text);
            opts.FastAdd("Login Check", tbLoginCheck.Text);
            opts.FastAdd("EditImageId", tbEditImageId1.Text);
            opts.FastAdd("PageId", tbPageId.Text);


            DelayedSave(++changeId);
        }

        private void DelayedSave(ulong id)
        {
            Task.Run(async () =>
            {
                await Task.Delay(500);
                if (changeId == id)
                {
                    config.Export("config.ini");
                }
            });
        }
    }
}
