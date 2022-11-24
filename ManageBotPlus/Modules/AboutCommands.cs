using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBotPlus
{
    [Group("about", "See stuff about ManageBot Plus")]
    public class AboutCommands : InteractionModuleBase, IManageBotModule
    {
        private readonly IServiceProvider _services;
        private readonly InteractionService _interactionService;
        private readonly DiscordSocketClient _client;

        public AboutCommands(IServiceProvider services)
        {
            this._services = services;
            this._client = services.GetRequiredService<DiscordSocketClient>();
            this._interactionService = services.GetRequiredService<InteractionService>();
        }

        [SlashCommand("source", "View my source code on GitHub!")]
        public async Task OpenSourceCommandAsync()
        {
            IEmote emote = Emote.Parse("<:github:935591565122998362>");
            var builder = new ComponentBuilder()
                .WithButton(label: "Click here", emote: emote, url: "https://github.com/elixss/ManageBotPlus", style: ButtonStyle.Link);
            await RespondAsync("Click to view my source code.", components: builder.Build());
        }

        [SlashCommand("me", "View information about me!")]
        public async Task AboutMeAsync()
        {
            string botInfo = $$"""
                Guilds: {{this._client.Guilds.Count}}
                Commands: {{this._interactionService.SlashCommands.Count}}
                Developer: elixss#9999
                Gateway latency: {{this._client.Latency}}ms
                """;

            var builder = new EmbedBuilder()
            {
                Title = "About ManageBot Plus",
                Description = $"Last update: <t:{Config.LastUpdate}:R>",
                Color = Config.Color
            }
            .AddField("See information about me!", botInfo, false);
            var onlineSince = DateTime.UtcNow - Config.StartTime;


            await RespondAsync(embed: builder.Build());
        }

        [SlashCommand("support-server", "Get a link to join the support server!")]
        public async Task SupportServerAsync()
        {
            var builder = new ComponentBuilder()
                .WithButton(label: "Click here", url: Config.SupportInvite, style: ButtonStyle.Link);
            await RespondAsync("Click to join the support server", components: builder.Build());
        }
    }
}
