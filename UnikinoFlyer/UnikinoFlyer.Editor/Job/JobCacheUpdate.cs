using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;
using MaxLib.Data.IniFiles;
using System.IO;
using UnikinoFlyer.Data;

namespace UnikinoFlyer.Editor.Job
{
    public partial class JobCacheUpdate : DualControlBase
    {
        OptionsLoader config;
        string cacheDir;

        TimeSpan delaySpan = new TimeSpan(0, 0, 5);

        int currentPage = 0;
        int pageCount = 0;
        int currentImg = 0;
        int maxImgCount = 0;
        string lastImgPath = null;
        object updLock = new object();

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

            Task.Run(BackgroundRun);

            //var newDelay = delaySpan + delaySpan;
            //if (newDelay >= new TimeSpan(1, 30, 0))
            //    newDelay = new TimeSpan(1, 30, 0);
            //DoSwitchMainControl(new NoConnection(
            //    new JobCacheUpdate(newDelay), delaySpan));
        }

        private async Task BackgroundRun()
        {
            Invoke(() =>
            {
                status.Text = "Login to server ...";
                progress.Style = ProgressBarStyle.Marquee;
            });
            Config.SetOptionsRoot(config["Uploader"]);
            Dictionary<string, object> home;
            try { home = await Client.Login(); }
            catch (Exception e) { home = null; }
            if (home == null)
            {
                Invoke(() =>
                {
                    var newDelay = delaySpan + delaySpan;
                    if (newDelay >= new TimeSpan(1, 30, 0))
                        newDelay = new TimeSpan(1, 30, 0);
                    DoSwitchMainControl(new NoConnection(
                        new JobCacheUpdate(newDelay), delaySpan));
                });
                return;
            }
            Invoke(() =>
            {
                status.Text = "Request images ...";
                progress.Style = ProgressBarStyle.Marquee;
            });
            var t1 = await Client.NavToImgLib(home);
            currentPage = 1;
            pageCount = t1.Item1;
            Invoke(() => timer1.Enabled = true);
            var block = new BufferBlock<string>();
            var act = new ActionBlock<string>(async url =>
            {
                using (var client = new System.Net.WebClient())
                {
                    if (url.StartsWith("/"))
                        url = url.Substring(1);
                    var urlBase = config["Uploader"]?.Options.GetString("BaseUrl", "") ?? "";
                    var fileBase = config[0].Options.GetString("ImageMirror", "");
                    var turl = $"{urlBase}{url}";
                    var ind = url.IndexOf('?');
                    if (ind > 0)
                        url = url.Remove(ind);
                    ind = url.LastIndexOf('/');
                    url = url.Substring(ind + 1);
                    var tfile = $"{fileBase}\\{url}";
                    await client.DownloadFileTaskAsync(turl, tfile);

                    lock (updLock)
                    {
                        currentImg++;
                        lastImgPath = tfile;
                        //if (currentImg == maxImgCount && currentPage == pageCount)
                        //    block.Complete();
                    }
                }
            });
            block.LinkTo(act, new DataflowLinkOptions { PropagateCompletion = true });
            var lib = t1.Item2;
            for (int i = 0; i<t1.Item1; ++i)
            {
                currentPage = i + 1;
                var imgs = await Client.FetchImgSrc(i, lib);
                maxImgCount += imgs.Length;
                foreach (var img in imgs)
                    block.Post(img);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            status.Text = $"Download {currentImg} / {maxImgCount} - " +
                $"Page {currentPage} / {pageCount}";
            progress.Style = ProgressBarStyle.Continuous;
            int max = maxImgCount * pageCount / currentPage;
            progress.Maximum = max;
            progress.Value = currentImg;
            picture.ImageLocation = lastImgPath;
        }
    }
}
