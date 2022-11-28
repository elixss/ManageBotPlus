using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ManageBotPlus
{
    public class OtherCommands : InteractionModuleBase, IManageBotModule
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly InteractionService _interactionService;
        private readonly DiscordSocketClient _socketClient;

        public OtherCommands(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._interactionService = serviceProvider.GetRequiredService<InteractionService>();
            this._socketClient = serviceProvider.GetRequiredService<DiscordSocketClient>();
        }

        [SlashCommand("help", "Get help with ManageBot Plus!")]
        public async Task HelpAsync()
        {
            var builder = new EmbedBuilder()
            {
                Title = "ManageBot Plus Help",
                Description = "See ManageBot Plus' commands below.",
                Color = Config.color
            };

            foreach (var group in this._interactionService.Modules)
            {
                if (group.IsSlashGroup)
                {
                    string groupName = group.Name.Replace("Commands", string.Empty);
                    string title = $"{groupName} command-group";
                    string description = string.Empty;

                    foreach (SlashCommandInfo command in group.SlashCommands)
                    {
                        description += $"`/{groupName.ToLower()} {command.Name}` - {command.Description}\n";
                    }
                    builder.AddField(title, description, false);
                }

                if (group.Name == nameof(OtherCommands))
                {
                    string groupName = "Other commands";
                    string description = string.Empty;

                    foreach (SlashCommandInfo command in group.SlashCommands)
                    {
                        description += $"`/{command.Name.ToLower()}` - {command.Description}\n";
                    }
                    builder.AddField(groupName, description, false);
                }
            }
            await RespondAsync(embed: builder.Build());
        }

        [SlashCommand("invite", "Add me to your server!")]
        public async Task InviteAsync()
        {
            var builder = new ComponentBuilder()
                .WithButton(label: "Click here", style: ButtonStyle.Link, url: Config.invite);
            await RespondAsync("Click to invite me to your server!", components: builder.Build());
        }
    }
}
