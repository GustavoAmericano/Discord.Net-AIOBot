using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Audio.Streams;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Addons.Interactive;

namespace Example.Modules
{
    [Name("Games")]
    public class GamesModule : InteractiveBase
    {

        [Command("roll"), Alias("dice", "r")]
        [Summary("Rolls a dice and prints the output.")]
        public async Task RollDice(string dices = "1", string sides = "6")
        {
            if (!int.TryParse(dices, out int j))
            {
                await ReplyAsync($"Invalid input at DICES. \nInput must be between 1 and 100");
                return;
            }

            if (j > 100)
            {
                await ReplyAsync($"Invalid input at DICES. \nInput must be between 1 and 100");
                return;
            }

            if (!int.TryParse(sides, out int i))
            {
                await ReplyAsync($"Invalid input at SIDES. \nInput must be between 1 and {int.MaxValue}");
                return;
            }

            Math.Abs(i);
            await ReplyAsync($"Rolling {dices}x {sides} sided dice.. :game_die: ");
            Random rnd = new Random();
            List<int> rolls = new List<int>();
            for (int k = 0; k < j; k++)
            {
                rolls.Add(rnd.Next(1, i));
            }

            string rollsString = "";
            for (var k = 0; k < rolls.Count; k++)
            {
                rollsString += rolls[k] + ",";
                if (10 % i == 0) rollsString += "\n";
                if (rollsString.Length >= 2000)
                {
                    rollsString = rollsString.Remove(2000);
                    await ReplyAsync(rollsString);
                    rollsString = "";
                }
                
            }
            await ReplyAsync($"Rolls are: {rollsString}");
        }

        [Command("hilo", RunMode = RunMode.Async)]
        [Summary("Starts a game of higher or lower")]

        public async Task hilo(int min = 0, int max = 10)
        {
            int oldNum, newNum, score = 0;
            TimeSpan ts;
            Random rnd = new Random();
            bool hasFailed = false;

            newNum = rnd.Next(min, max);
            var message = await ReplyAsync($"A game of hi-lo has started! ({min}-{max})\n" +
                                       "Reply with 'hi' for higher, and 'lo' for lower. Type 'Cancel' to end the game.\n" +
                                       $"number is: {newNum}");
            while (!hasFailed)
            {
                var response = await NextMessageAsync(timeout: TimeSpan.FromSeconds(15));
                if (response != null)
                {
                    if ((response.Content.ToLower() == "hi" || response.Content == "lo"))
                    {
                        oldNum = newNum;
                        newNum = rnd.Next(min, max);

                        switch (response.Content.ToLower())
                        {
                            case "hi":
                                if (oldNum > newNum) hasFailed = true;
                                break;
                            case "lo":
                                if (oldNum < newNum) hasFailed = true;
                                break;
                        }

                        if (hasFailed)
                        {
                            await ReplyAsync($"You chose {response} on {oldNum}, next number is {newNum}. You lose!\n" +
                                             $"Final score: {score}");
                            return;
                        }
                        else
                        {
                            await message.ModifyAsync(msg =>
                                msg.Content = $"You chose {response} on {oldNum}, next number is {newNum}. You win!\n" +
                                              $" Score: {++score}\n\n" +
                                              $"Reply with 'hi' for higher, and 'lo' for lower. Type 'Cancel' to end the game.\n" +
                                              $"number is: {newNum}");
                        }
                        
                    }
                }
                else
                {
                    await ReplyAsync("You did not respond in time. Game has ended.");
                    return;
                }
                
            }

        }
    }
}