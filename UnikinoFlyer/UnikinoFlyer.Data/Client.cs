using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.IO;
using MaxLib.Data.HtmlDom;

namespace UnikinoFlyer.Data
{
    public static class Client
    {
        static readonly Dictionary<string, string> cookie = new Dictionary<string, string>();
        static Encoding ServerEncoding = Encoding.UTF8;

        private static async Task<T> Run<T>(Func<WebClient, Task<T>> work)
        {
            if (work == null) throw new ArgumentNullException(nameof(work));
            using (var client = new WebClient())
            {
                client.BaseAddress = Config.GetRoot("BaseUrl")?.GetString();
                client.Headers.Set(HttpRequestHeader.Cookie,
                    string.Join("; ", cookie.Select(p => $"{System.Web.HttpUtility.UrlEncode(p.Key)}={System.Web.HttpUtility.UrlEncode(p.Value)}")));
                client.Headers.Set(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                client.Headers.Set(HttpRequestHeader.AcceptLanguage, "de,en-US;q=0.7,en;q=0.3");
                client.Headers.Set(HttpRequestHeader.Referer, "https://wcms.itz.uni-halle.de/test.php");
                client.Headers.Set("Upgrade-Insecure-Requests", "1");
                client.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:69.0) Gecko/20100101 Firefox/69.0");
                client.Encoding = ServerEncoding;
                var result = await work(client);
                var values = client.ResponseHeaders.GetValues("Set-Cookie");
                if (values != null)
                    foreach (var header in values)
                    {
                        var p = header.Split(new[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (p.Length == 0) continue;
                        var kp = p[0].Split(new[] { '=' }, 2);
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

        private static string PrepairUpload(WebClient client, Dictionary<string, object> upload, bool fragment = true)
        {
            if (fragment)
            {
                client.Headers.Set(HttpRequestHeader.ContentType, "multipart/form-data; boundary=---------------------------235981322718164");

                var sb = new StringBuilder();
                foreach (var p in upload)
                {
                    if (p.Value is null)
                        continue;
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
            else
            {
                client.Headers.Set(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");

                var sb = new StringBuilder();
                foreach (var p in upload)
                {
                    if (p.Value is null) continue;
                    if (sb.Length != 0) sb.Append('&');
                    sb.Append(System.Web.HttpUtility.UrlEncode(p.Key));
                    sb.Append('=');
                    if (p.Value is string)
                        sb.Append(System.Web.HttpUtility.UrlEncode(p.Value.ToString()));
                    else if (p.Value is FileInfo file)
                        sb.Append(System.Web.HttpUtility.UrlEncode(File.ReadAllText(file.FullName)));
                }
                return sb.ToString();
            }
        }

        static readonly string[] ignoreInputNames = new[] { "submit", "image", "button" };
        private static Dictionary<string, object> HiddenValues(string code, Dictionary<string, object> dict = null)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (dict == null) dict = new Dictionary<string, object>();

            var dom = HtmlDomParser.ParseHtml(code);
            var elem = dom.DeepSearch()
                .Select(e =>
                {
                    var name = e.GetAttribute("name")
                        .Select(a => a.Value)
                        .FirstOrDefault();
                    if (name == null) return null;
                    var value = e.GetAttribute("value")
                        .Select(a => a.Value)
                        .FirstOrDefault();
                    if (e.ElementName == "input")
                    {
                        var type = e.GetAttribute("type")
                            .Select(a => a.Value)
                            .FirstOrDefault();
                        if (type == null || ignoreInputNames.Contains(type))
                            value = null;
                    }
                    if (e.ElementName == "select")
                    {
                        var first = e.Elements
                            .SelectMany(s => s.GetAttribute("value"))
                            .Select(a => a.Value)
                            .FirstOrDefault();
                        var selected = e.Elements
                            .Where(s => s.GetAttribute("selected").Length > 0)
                            .SelectMany(s => s.GetAttribute("value"))
                            .Select(a => a.Value)
                            .FirstOrDefault();
                        value = selected ?? first ?? value;
                    }
                    if (e.ElementName == "img")
                    {
                        var src = e.GetAttribute("src").Select(a => a.Value).FirstOrDefault();
                        if (src != null)
                        {
                            var ind = src.LastIndexOf('/');
                            src = src.Substring(ind + 1);
                        }
                        value = value ?? src;
                    }
                    return new Tuple<string, string>(name, value);
                })
                .Where(e => e != null);
            foreach (var e in elem)
                dict[e.Item1] = e.Item2;
            return dict;
        }

        private static Dictionary<string, object> NextPageValues(Dictionary<string, object> prev, Dictionary<string, object> next)
        {
            if (prev == null) throw new ArgumentNullException(nameof(prev));
            if (next == null) throw new ArgumentNullException(nameof(next));

            var result = new Dictionary<string, object>(prev);

            foreach (var n in next)
                result[n.Key] = n.Value;

            result = result.Where(p => !(p.Value is null)).ToDictionary(p => p.Key, p => p.Value);

            return result;

            //foreach (var p in prev)
            //    if (!next.ContainsKey(p.Key))
            //        result.Add(p.Key, p.Value);

            //foreach (var p in cookie)
            //    if (!next.ContainsKey(p.Key))
            //        next.Add(p.Key, p.Value);

            //next["id"] = "2201326";

            //return next;
        }

        private static System.Collections.Specialized.NameValueCollection MapCollection(Dictionary<string, object> dict)
        {
            var nv = new System.Collections.Specialized.NameValueCollection();
            foreach (var p in dict)
            {
                if (p.Value is null) continue;
                if (p.Value is string)
                    nv.Add(p.Key, p.Value.ToString());
                if (p.Value is FileInfo file)
                    nv.Add(p.Key, System.Web.HttpUtility.UrlEncode(File.ReadAllText(file.FullName)));
            }
            return nv;
        }

        public static Task<Dictionary<string, object>> Login()
        {
            return Run(async client =>
            {
                Console.WriteLine("Login to " + Config.GetRoot("BaseUrl"));

                var result = await client.DownloadStringTaskAsync("test.php?id=2201326&submit=Anmelden");
                var ind = result.IndexOf("name=\"PHPSESSID\"");
                ind = result.IndexOf("value=\"", ind) + "value=\"".Length;
                var ind2 = result.IndexOf('"', ind);
                var id = cookie["PHPSESSID"] = result.Substring(ind, ind2 - ind);

                ind = result.IndexOf("http-equiv=\"content-type\"");
                if (ind > 0)
                {
                    ind = result.IndexOf("charset", ind);
                    ind = result.IndexOf('=', ind) + 1;
                    ind2 = result.IndexOf('"', ind);
                    var charset = result.Substring(ind, ind2 - ind);
                    ServerEncoding = Encoding.GetEncoding(charset);
                }

                result = ServerEncoding.GetString(await client.UploadValuesTaskAsync("test.php?id=2201326", new System.Collections.Specialized.NameValueCollection
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

        public static Task<string> CallPage(Dictionary<string, object> page)
        {
            return Run(async client =>
            {
                return await client.UploadStringTaskAsync("test.php?id=2201326", PrepairUpload(client, page));
            });
        }

        private static async Task<Dictionary<string, object>> DoValueRequest(WebClient client, Dictionary<string, object> pageBase, Dictionary<string, object> navigation)
        {
            var result = await DoValueRequestRaw(client, pageBase, navigation);
            return result != null ? HiddenValues(result) : null;
        }

        private static async Task<string> DoValueRequestRaw(WebClient client, Dictionary<string, object> pageBase, Dictionary<string, object> navigation)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (pageBase == null) throw new ArgumentNullException(nameof(pageBase));
            if (navigation == null) throw new ArgumentNullException(nameof(navigation));
            //var result = 
            //    ServerEncoding.GetString(
            //        await client.UploadValuesTaskAsync(
            //            "test.php?id=2201326",
            //            MapCollection(
            //                NextPageValues(
            //                    pageBase,
            //                    navigation
            //                )
            //            )
            //        )
            //    );
            var result = await client.UploadStringTaskAsync(
                "test.php?id=2201326",
                PrepairUpload(
                    client,
                    NextPageValues(
                        pageBase,
                        navigation
                    ),
                    false
                )
            );
            if (!result.Contains(Config.GetRoot("Login Check")?.GetString() ?? ""))
            {
                Console.WriteLine("## LOGIN SESSION LOST ##");
                return null;
            }
            return result;
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
                var conf1 = await DoValueRequest(client, home, new Dictionary<string, object>
                {
                    { $"editsingleelement{Config.GetRoot("EditImageId")?.GetString()}", "Bild bearbeiten" }
                });

                Console.WriteLine("  Click on open image db");
                var conf2 = await DoValueRequest(client, conf1, new Dictionary<string, object>
                {
                    { $"openImageDb[{Config.GetRoot("EditImageId")?.GetString()}]", "Bilddatenbank öffnen" }
                });

                while (!conf2.ContainsKey($"insertimage[{imageKey}]") && conf2.ContainsKey("rightShiftImages"))
                {
                    Console.WriteLine("  Search on next page");
                    conf2 = await DoValueRequest(client, conf2, new Dictionary<string, object>
                    {
                        { $"rightShiftImages.x", "9" },
                        { $"rightShiftImages.y", "5" }
                    });
                }

                if (!conf2.ContainsKey($"insertimage[{imageKey}]"))
                {
                    Console.WriteLine("  Image not found");
                    return false;
                }

                Console.WriteLine("  Select image");
                var conf3 = await DoValueRequest(client, conf2, new Dictionary<string, object>
                {
                    { $"insertimage[{imageKey}].x", "9" },
                    { $"insertimage[{imageKey}].y", "7" }
                });

                Console.WriteLine("  Set text and upload");
                var conf4 = await DoValueRequest(client, conf3, new Dictionary<string, object>
                {
                    { $"text{Config.GetRoot("EditImageId")?.GetString()}[de]", comment },
                    { $"BU[save][{Config.GetRoot("EditImageId")?.GetString()}].x", "13" },
                    { $"BU[save][{Config.GetRoot("EditImageId")?.GetString()}].y", "11" }
                });

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

        public static Task Publish(Dictionary<string, object> home)
        {
            if (home == null) throw new ArgumentNullException(nameof(home));
            return Run(async client =>
            {
                Console.WriteLine("Publish current changes");

                Console.WriteLine("  Click on publish pages");
                var conf1 = await DoValueRequest(client, home, new Dictionary<string, object>
                {
                    { "publishPage.x", "13" },
                    { "publishPage.y", "14" }
                });

                Console.Write("  Publish Page");
                var ids = conf1.Select(p => p.Key)
                    .Where(k => k.StartsWith("publishThesePages"))
                    .Select(k => k.Substring("publishThesePages".Length))
                    .ToDictionary(p => "publishThesePages" + p, p => (object)p);
                ids.Add("publishPage.x", "12");
                ids.Add("publishPage.y", "17");
                var conf2 = await DoValueRequest(client, conf1, ids);
            });
        }

        public static Task<Tuple<int,Dictionary<string, object>>> NavToImgLib(Dictionary<string, object> home)
        {
            if (home == null) throw new ArgumentNullException(nameof(home));
            return Run(async client =>
            {
                Console.WriteLine("Navigate to image library");

                var result = await DoValueRequestRaw(client, home, new Dictionary<string, object>
                {
                    { "openImageDb[0][rightCol]", "Bilddatenbank öffnen" }
                });
                var conf1 = HiddenValues(result);
                var dom = HtmlDomParser.ParseHtml(result);
                var maxPage = dom
                    .DeepSearch()
                    .TagName("select")
                    .Attribute("name", "numberOfImagePage")
                    .SelectMany(e => e.Elements)
                    .TagName("option")
                    .SelectMany(e => e.GetAttribute("value"))
                    .Select(e => int.TryParse(e.Value, out int v) ? new Tuple<int>(v) : null)
                    .Where(t => t != null)
                    .Select(t => t.Item1)
                    .Max() + 1;

                return new Tuple<int, Dictionary<string, object>>(maxPage, conf1);
            });
        }

        public static Task<string[]> FetchImgSrc(int page, Dictionary<string, object> imgLib)
        {
            if (page < 0) throw new ArgumentOutOfRangeException(nameof(page));
            if (imgLib == null) throw new ArgumentNullException(nameof(imgLib));
            return Run(async client =>
            {
                Console.WriteLine("Fetch image library page " + page.ToString());
                var result = await DoValueRequestRaw(client, imgLib, new Dictionary<string, object>
                {
                    { "numberOfImagePage", page.ToString() },
                    { "aktualiseren.x", "32" },
                    { "aktualiseren.y", "16" }
                });
                var imgList = HtmlDomParser.ParseHtml(result)
                    .DeepSearch()
                    .TagName("img")
                    .Class("bild111")
                    .Select(e => e.Parent)
                    .SelectMany(e => e.GetAttribute("href"))
                    .Select(a => a.Value);

                return imgList.ToArray();
            });
        }
    }
}
