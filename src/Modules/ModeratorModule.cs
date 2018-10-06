using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Example.Modules
{
    [Name("Moderator")]
    [RequireContext(ContextType.Guild)]
    public class ModeratorModule : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [Summary("Kick the specified user.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick([Remainder]SocketGuildUser user)
        {
            await ReplyAsync($"cya {user.Mention} :wave:");
            await user.KickAsync();
        }

        [Command("forcenick"), Priority(0)]
        [Summary("Change another user's nickname to the specified text")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task FNick(SocketGuildUser user, [Remainder]string name)
        {
            await user.ModifyAsync(x => x.Nickname = name);
            await ReplyAsync($"{user.Mention} I changed your name to **{name}**");
        }


        [Command("purge")]
        [Summary("Purge specified amount of messages. (0-100)")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task purge(int amountToPurge)
        {
            var atp = Math.Abs(amountToPurge);
            if (atp > 100)
            {
                await ReplyAsync("Attempted to purge too many messages. Amount must be between 0-100.");
                return;
            }
            var messages = await Context.Channel.GetMessagesAsync(atp +1).Flatten();
            messages = messages.Where(x => x.IsPinned != true);
            await Context.Channel.DeleteMessagesAsync(messages);
        }
    }
}
