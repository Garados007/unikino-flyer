using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.IO;
using MaxLib.Data.HtmlDom;

namespace UnikinoFlyer.Uploader
{
    static class Client
    {
        static readonly Dictionary<string, string> cookie = new Dictionary<string, string>();

        private static async Task<T> Run<T>(Func<WebClient, Task<T>> work)
        {
            if (work == null) throw new ArgumentNullException(nameof(work));
            using (var client = new WebClient())
            {
                client.BaseAddress = Config.GetRoot("BaseUrl")?.GetString();
                client.Headers.Set(HttpRequestHeader.Cookie,
                    string.Join("; ", cookie.Select(p => $"{System.Web.HttpUtility.UrlEncode(p.Key)}={System.Web.HttpUtility.UrlEncode(p.Value)}")));
                var result = await work(client);
                var values = client.ResponseHeaders.GetValues("Set-Cookie");
                if (values != null)
                    foreach (var header in values)
                    {
                        var p = header.Split(new[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (p.Length == 0) continue;
                        var kp = p[0].Split('=', 2);
                        if (kp.Length != 2) continue;
                        cookie[System.Web.HttpUtility.UrlDecode(kp[0])] = System.Web.HttpUtility.UrlDecode(kp[1]);
                    }
                return result;
            }
        }

        private static async Task Run(Func<WebClient, Task> work)
        {
            if (work == null) throw new ArgumentNullException(nameof(work));
            await Run(async client => { await work(client); return 1; });
        }

        private static string PrepairUpload(WebClient client, Dictionary<string, object> upload)
        {
            client.Headers.Set(HttpRequestHeader.ContentType, "multipart/form-data; boundary=---------------------------235981322718164");

            var sb = new StringBuilder();
            foreach (var p in upload)
            {
                sb.AppendLine("---------------------------235981322718164");
                if (p.Value is string)
                {
                    sb.AppendLine("Content-Disposition: form-data; name=\"" + p.Key + "\"");
                    sb.AppendLine();
                    sb.AppendLine(p.Value.ToString());
                }
                else if (p.Value is FileInfo file)
                {
                    sb.AppendLine($"Content-Disposition: form-data; name=\"{p.Key}\"; filename=\"{file.Name}\"");
                    var mime = MaxLib.Net.Webserver.MimeTypes.ApplicationOctetStream;
                    switch (file.Extension.ToLower())
                    {
                        case ".jpg":
                        case ".jpeg":
                            mime = MaxLib.Net.Webserver.MimeTypes.ImageJpeg;
                            break;
                        case ".png":
                        case ".pneg":
                            mime = MaxLib.Net.Webserver.MimeTypes.ImageJpeg;
                            break;
                        case ".gif":
                            mime = MaxLib.Net.Webserver.MimeTypes.ImageGif;
                            break;
                    }
                    sb.AppendLine($"Content-Type: {mime}");
                    sb.AppendLine();
                    sb.AppendLine(File.ReadAllText(file.FullName));
                }
            }
            sb.AppendLine("---------------------------235981322718164--");
            return sb.ToString();
        }

        private static Dictionary<string, object> HiddenValues(string code, Dictionary<string, object> dict = null)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (dict == null) dict = new Dictionary<string, object>();

            var dom = HtmlDomParser.ParseHtml(code);
            foreach (var input in dom.Elements.GetElementsByName("input"))
            {
                var valid = false;
                foreach (var attr in input.GetAttribute("type"))
                    if (attr.Value == "hidden")
                    {
                        valid = true;
                        break;
                    }
                if (!valid) continue;
                string name = null, value = null;
                foreach (var attr in input.GetAttribute("name"))
                    name = attr.Value;
                foreach (var attr in input.GetAttribute("value"))
                    value = attr.Value;
                if (name != null && value != null)
                    dict[name] = value;
            }
            return dict;
        }

        private static Dictionary<string, object> NextPageValues(Dictionary<string, object> prev, Dictionary<string, object> next)
        {
            if (prev == null) throw new ArgumentNullException(nameof(prev));
            if (next == null) throw new ArgumentNullException(nameof(next));

            foreach (var p in prev)
                if (!next.ContainsKey(p.Key))
                    next.Add(p.Key, p.Value);

            foreach (var p in cookie)
                if (!next.ContainsKey(p.Key))
                    next.Add(p.Key, p.Value);

            return next;
        }

        public static Task<Dictionary<string, object>> Login()
        {
            return Run(async client =>
            {
                Console.WriteLine("Login to " + Config.GetRoot("BaseUrl"));

                var res1 = await client.DownloadStringTaskAsync("test.php?id=2201326&submit=Anmelden");
                var ind = res1.IndexOf("name=\"PHPSESSID\"");
                ind = res1.IndexOf("value=\"", ind) + "value=\"".Length;
                var ind2 = res1.IndexOf('"', ind);
                var id = cookie["PHPSESSID"] = res1.Substring(ind, ind2 - ind);

                var result = Encoding.UTF8.GetString(await client.UploadValuesTaskAsync("test.php?id=2201326", new System.Collections.Specialized.NameValueCollection
                {
                    { "formularname", "login" },
                    { "login_email", Config.GetRoot("User")?.GetString() },
                    { "login_password", Config.GetRoot("PW")?.GetString() },
                    { "submit", "Anmelden" }
                }));
                if (result.Contains(Config.GetRoot("Login Check")?.GetString()))
                    return HiddenValues(result);
                else return null;
            });
        }

        public static Task<string> UploadImage(FileInfo image, string comment)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            if (comment == null) throw new ArgumentNullException(nameof(comment));
            return Run(async client =>
            {
                Console.WriteLine("Upload image " + image.Name);
                var result = await client.UploadStringTaskAsync("test.php?id=2201326", PrepairUpload(client, new Dictionary<string, object>
                {
                    { "PHPSESSID", cookie["PHPSESSID"] },
                    { "id", "2201326" },
                    { "pageOfImageDB", "0" },
                    { "ownerOfImageDB", "3" },
                    { "numberOfImagePage", "0" },
                    { "imageDBstatus", "rightCol" },
                    { "imageDBactiveElement[0]", "anderes Bild auswählen" },
                    { "uf0", image },
                    { "uf0text", comment },
                    { "uploadImage", "hinzufügen" },
                }));

                var ind = result.LastIndexOf("Dieses Bild wurde hochgeladen und zu den eigenen Bildern hinzugefügt");
                if (ind == -1) return null;

                ind = result.IndexOf("/im/", ind);
                var ind2 = result.IndexOf('"', ind);
                return result.Substring(ind, ind2 - ind);
            });
        }

        public static Task<bool> SwitchImage(Dictionary<string, object> home, string imageKey, string comment)
        {
            if (home == null) throw new ArgumentNullException(nameof(home));
            if (imageKey == null) throw new ArgumentNullException(nameof(imageKey));
            if (comment == null) throw new ArgumentNullException(nameof(comment));
            return Run(async client =>
            {
                Console.WriteLine("Switch image box to " + imageKey);

                Console.WriteLine("  Click on Image Edit");
                var result = await client.UploadStringTaskAsync("test.php?id=2201326", PrepairUpload(client, NextPageValues(home, new Dictionary<string, object>
                {
                    { $"editsingleelement{Config.GetRoot("EditImageId")?.GetString()}", "Bild bearbeiten" }
                })));
                var conf1 = HiddenValues(result);

                Console.WriteLine("  Click on open image db");
                result = await client.UploadStringTaskAsync("test.php?id=2201326", PrepairUpload(client, NextPageValues(conf1, new Dictionary<string, object>
                {
                    { $"openImageDb[{Config.GetRoot("EditImageId")?.GetString()}]", "Bilddatenbank öffnen" }
                })));
                var conf2 = HiddenValues(result);

                while (!result.Contains(imageKey) && result.Contains("rightShiftImages"))
                {
                    Console.WriteLine("  Search on next page");
                    result = await client.UploadStringTaskAsync("test.php?id=2201326", PrepairUpload(client, NextPageValues(conf2, new Dictionary<string, object>
                    {
                        { $"rightShiftImages.x", "9" },
                        { $"rightShiftImages.y", "5" }
                    })));
                    conf2 = HiddenValues(result);
                }

                if (!result.Contains(imageKey))
                {
                    Console.WriteLine("  Image not found");
                    return false;
                }

                Console.WriteLine("  Select image");
                result = await client.UploadStringTaskAsync("test.php?id=2201326", PrepairUpload(client, NextPageValues(conf2, new Dictionary<string, object>
                {
                    { $"insertimage[{Config.GetRoot("EditImageId")?.GetString()}].x", "9" },
                    { $"insertimage[{Config.GetRoot("EditImageId")?.GetString()}].y", "7" }
                })));
                var conf3 = HiddenValues(result);

                Console.WriteLine("  Set text and upload");
                result = await client.UploadStringTaskAsync("test.php?id=2201326", PrepairUpload(client, NextPageValues(conf3, new Dictionary<string, object>
                {
                    { $"text{Config.GetRoot("EditImageId")?.GetString()}[de]", comment },
                    { $"BU[save][{Config.GetRoot("EditImageId")?.GetString()}].x", "13" },
                    { $"BU[save][{Config.GetRoot("EditImageId")?.GetString()}].y", "11" }
                })));
                var conf4 = HiddenValues(result);

                return true;
                //var result = await client.UploadStringTaskAsync("test.php?id=2201326", PrepairUpload(client, new Dictionary<string, object>
                //{
                //    { "PHPSESSID", cookie["PHPSESSID"] },
                //    { "id", "2201326" },
                //    { "BU[insert][2201327][3045744][below]", "NULL" },
                //    { "rightColImages", "1443783486_1343_0.jpg" },
                //    { "rightColImageRekursiv", "1" },
                //    { "BU[insert][2201328][3130168][below]", "NULL" },
                //    { "BU[insert][2201329][NULL][below]", "NULL" },
                //    { "BU[insert][2201330][2206423][below]", "NULL" },
                //    { "editsingleelement3242227", "Bild bearbeiten" },
                //    { "BU[insert][2201326][2460401][below]", "NULL" },
                //    { "preandeditview", "1" },
                //    { "lastmod", "preandeditview" }
                //}));
            });
        }
    }
}
