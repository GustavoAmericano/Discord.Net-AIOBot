using System.Security.Cryptography;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Example.Modules
{
    [Name("St00f")]
    public class ExampleModule : ModuleBase<SocketCommandContext>
    {
        [RequireContext(ContextType.Guild)]
        public class Set : ModuleBase
        {
            [Command("sha256")]
            [Summary("Converts plaintext to Sha-256")]
            public async Task sha256(string input)
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
        }
    }
}
