using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using UnikinoFlyer.Data;

namespace UnikinoFlyer.Uploader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Dictionary<string, object> home;
            try { home = await Client.Login(); }
            catch (System.Net.WebException ex)
            {
                if (ex.Response is System.Net.HttpWebResponse response)
                {
                    Console.WriteLine($"Bad status code: {response.StatusCode} ({(int)response.StatusCode})");
                }
                else
                {
                    Console.WriteLine($"Connection error: {ex}");
                }
                return;
            }
            if (home == null)
            {
                Console.Error.WriteLine("Login failed");
                return;
            }
            //var page = await Client.CallPage(home);

            //await Client.UploadImage(new System.IO.FileInfo(@"C:\Users\Max\Desktop\road-sign-361514_960_720.png"), "test image 2");
            await Client.SwitchImage(home, "1507797022_1343_0.jpg", "Auf jeden Fall könnt ihr euch schon auf die FZB freuen (bot)");
            await Client.Publish(home);
        }
    }
}
