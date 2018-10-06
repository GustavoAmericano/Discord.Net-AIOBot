using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Example.Modules
{
    [Name("Fun stuff")]
    public class ExampleModule : ModuleBase<SocketCommandContext>
    {
        [RequireContext(ContextType.Guild)]
        public class Set : ModuleBase
        {
            [Command("sha256")]
            [Summary("Converts plaintext to Sha-256")]
            public async Task sha256([Remainder] string input)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                    // Convert byte array to a string   
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }

                    await ReplyAsync(builder.ToString());
                }
            }

            [Command("Geek")]
            [Summary("Tells a stupid geek joke..")]
            public async Task geek()
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] raw = wc.DownloadData("https://geek-jokes.sameerkumar.website/api");

                string webData = System.Text.Encoding.UTF8.GetString(raw);
                await ReplyAsync(webData);
            }

            [Command("blb")]
            [Summary("Tells a stupid BLB joke..")]
            public async Task blb()
            {
                await ReplyAsync("http://belikebill.azurewebsites.net/billgen-API.php?default=1");

                //http://numbersapi.com/100
            }

            [Command("rnf")]
            [Summary("Random number fact! (0-100)")]
            public async Task blb(int number = -1)
            {
                Math.Abs(number);
                Random rnd = new Random();
                if (number == -1) number = rnd.Next(0, 100);
                if (number > 100 || number == 0)
                {
                    await ReplyAsync("Invalid number. Must be between 1 and 100.");
                    return;
                }
                
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] raw = wc.DownloadData("http://numbersapi.com/" + number);

                string webData = System.Text.Encoding.UTF8.GetString(raw);
                await ReplyAsync(webData);

                //http://numbersapi.com/100
            }


        }
    }
}
