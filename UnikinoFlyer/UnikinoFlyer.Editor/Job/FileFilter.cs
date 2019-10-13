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
    public partial class FileFilter : DualControlBase
    {
        enum Mode
        {
            Unknown,
            Visible,
            Hidden
        }

        OptionsLoader config;
        string folder;
        string tnFolder;
        bool close = false;

        Dictionary<Mode, HashSet<string>> imageList;
        Mode filter = Mode.Unknown;

        public FileFilter()
        {
            InitializeComponent();
            this.Load += FileFilter_Load;
            imageList = new Dictionary<Mode, HashSet<string>>
            {
                { Mode.Unknown, new HashSet<string>() },
                { Mode.Visible, new HashSet<string>() },
                { Mode.Hidden, new HashSet<string>() }
            };
        }

        private void FileFilter_Load(object sender, EventArgs e)
        {
            config = new OptionsLoader("config.ini", false);
            folder = config[0].Options.GetString("ImageMirror", "");
            if (!Directory.Exists(folder))
            {
                DoSwitchMainControl(new InvalidFolderHint(folder, new FileFilter()));
                return;
            }
            tnFolder = $"{folder}\\thumbnails";
            if (!Directory.Exists(tnFolder))
                Directory.CreateDirectory(tnFolder);

            Task.Run(DoBackgroundTask);

            var group = config["VisibleFiles"] ?? new OptionsGroup();
            foreach (var file in Directory.EnumerateFiles(folder))
            {
                if (!SupportedExtension.Contains(Path.GetExtension(file).ToLower()))
                    continue;

                var name = Path.GetFileName(file);
                var node = group.Options.FindName(name);
                if (node == null)
                    imageList[Mode.Unknown].Add(name);
                else imageList[node.GetBool() ? Mode.Visible : Mode.Hidden].Add(name);
            }

            ShowFilter();
        }

        private void ShowFilter()
        {
            showUnsorted.Enabled = filter != Mode.Unknown;
            showVisible.Enabled = filter != Mode.Visible;
            showHidden.Enabled = filter != Mode.Hidden;
            doVisible.Enabled = filter != Mode.Visible;
            doHide.Enabled = filter != Mode.Hidden;

            listView.BeginUpdate();
            listView.Items.Clear();
            var list = imageList[filter];
            foreach (var name in list)
            {
                listView.Items.Add(name, name, name);
            }
            listView.EndUpdate();
        }

        static readonly string[] SupportedExtension =
            new[] { ".jpg", ".jpeg", ".png", ".pneg", ".gif", ".bmp" };
        private void DoBackgroundTask()
        {
            foreach (var file in Directory.EnumerateFiles(folder, "*.*", SearchOption.TopDirectoryOnly))
            {
                if (close) return;

                if (!SupportedExtension.Contains(Path.GetExtension(file).ToLower()))
                    continue;

                var target = $"{tnFolder}\\{Path.GetFileNameWithoutExtension(file)}.png";

                if (!File.Exists(target))
                {
                    var original = Image.FromFile(file);
                    var copy = new Bitmap(64, 64);
                    var g = Graphics.FromImage(copy);
                    g.Clear(Color.Transparent);
                    var v = Math.Min(64f / original.Width, 64f / original.Height);
                    var nw = v * original.Width; var nh = v * original.Height;
                    var tr = new RectangleF((64 - nw) * .5f, (64 - nh) * .5f, nw, nh);
                    var sr = new RectangleF(new Point(), original.Size);
                    g.DrawImage(original, tr, sr, GraphicsUnit.Pixel);
                    copy.Save(target, System.Drawing.Imaging.ImageFormat.Png);
                }

                if (close) return;

                var img = Image.FromFile(target);
                Invoke(() => images.Images.Add(Path.GetFileName(file), img));
            }
        }

        public override bool CloseControl()
        {
            close = true;
            return base.CloseControl();
        }

        private void showUnsorted_Click(object sender, EventArgs e)
        {
            filter = Mode.Unknown;
            ShowFilter();
        }

        private void showVisible_Click(object sender, EventArgs e)
        {
            filter = Mode.Visible;
            ShowFilter();
        }

        private void showHidden_Click(object sender, EventArgs e)
        {
            filter = Mode.Hidden;
            ShowFilter();
        }

        private void doVisible_Click(object sender, EventArgs e)
        {
            SetVisible(true);
        }

        private void SetVisible(bool visible)
        {
            var group = config["VisibleFiles"] ?? config.Add("VisibleFiles");
            foreach (ListViewItem item in listView.SelectedItems)
            {
                imageList[filter].Remove(item.Text);
                imageList[visible ? Mode.Visible : Mode.Hidden].Add(item.Text);
                group.Options.SetValue(item.Text, visible);
            }
            config.Export("config.ini");
            ShowFilter();
        }

        private void doHide_Click(object sender, EventArgs e)
        {
            SetVisible(false);
        }

        private void listView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            var tt = new PictureToolTip();
        }
    }

    class PictureToolTip : ToolTip
    {
        public PictureToolTip()
        {
            this.OwnerDraw = true;
            this.Popup += PictureToolTip_Popup;
            this.Draw += PictureToolTip_Draw;
        }

        private void PictureToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PictureToolTip_Popup(object sender, PopupEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
