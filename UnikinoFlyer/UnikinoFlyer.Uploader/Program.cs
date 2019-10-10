﻿using System;
using System.Threading.Tasks;

namespace UnikinoFlyer.Uploader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var home = await Client.Login();
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
