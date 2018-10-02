using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;

namespace Example.Modules
{
    [Name("Events")]
    public class EventModule : InteractiveBase
    {
        [Command("giveaway", RunMode = RunMode.Async)]
        [Summary("Starts a giveaway.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Giveaway(int minutes, [Remainder] string message)
        {
            var msg = await ReplyAsync($"A giveaway has started, and will run for another {minutes} minutes!\n" +
                                       $"React with the :confetti_ball: emoji to participate!\n" +
                                       $"More info: {message}");
            msg.AddReactionAsync(new Emoji("🎊"));
            for (int i = 1; i <= minutes; i++)
            {
                await Task.Delay(6000);
                await msg.ModifyAsync(x => x.Content = $"A giveaway has started, and will run for another {minutes-i} minutes!\n" +
                                                       $"React with the :confetti_ball: emoji to participate!\n" +
                                                       $"More info: {message}");
            }

            await msg.RemoveReactionAsync(new Emoji("🎊"),msg.Author);
            var users = msg.GetReactionUsersAsync("🎊").Result;
            if (users.Count < 1)
            {
                await ReplyAsync("No users participated :(\nEnding giveaway with no winners.");
                return;
            };
            Random rnd = new Random();
            var winner = users.ToList()[rnd.Next(0, users.Count-1)];
            await ReplyAsync($"Winner is: {winner.Mention}");
        }
    }
}