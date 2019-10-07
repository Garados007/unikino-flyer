using System;
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
            await Client.SwitchImage(home, "/im/1526578133_1343_00_111.jpg", "Dieser Text wurde durch ein Programm automatisch gesetzt");
        }
    }
}
