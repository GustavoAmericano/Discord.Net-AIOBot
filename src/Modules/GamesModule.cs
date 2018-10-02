using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Example.Modules
{
    [Name("Games")]
    public class GamesModule : ModuleBase<SocketCommandContext>
    {
        //[Command("kick")]
        //[Summary("Kick the specified user.")]
        //[RequireUserPermission(GuildPermission.KickMembers)]
        //public async Task Kick([Remainder]SocketGuildUser user)
        //{
        //    await ReplyAsync($"cya {user.Mention} :wave:");
        //    await user.KickAsync();
        //}

        [Command("roll"), Alias("dice")]
        [Summary("Rolls a dice and prints the output")]
        public async Task RollDice(int sides = 6)
        {
            await ReplyAsync($"Rolling a {sides} sided dice.. :game_die: ");
            Random rnd = new Random();
            int output = rnd.Next(1, sides);
            await ReplyAsync(output.ToString());
        }
    }
}