using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;


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

        private static double GetTotalSize()
        {
            double amount = 0;
            Assembly.GetExecutingAssembly()
                .GetFiles()
                .ToList()
                .ForEach(element => amount += element.Length / 1000);
            return amount;
        }


        [SlashCommand("me", "View information about me!")]
        public async Task AboutMeAsync()
        {
            DateTime start = DateTime.Now;
            string generalStats = $"""
                <:discord_logo:993244803896709130> | Servers: {this._client.Guilds.Count}
                <:dotnetbot:1045809815940759652> | Platform Version: .NET v7.00 / C# 11
                <:dev:892145685384798288> | Developer: {Config.developer}
                <:discordnet:1045814870748172358> | Discord.Net version: {Assembly.GetAssembly(typeof(Embed))?.GetName().Version}
                <:blurpleSlashCommands:892470245057499146> | Commands: {this._interactionService.SlashCommands.Count}
                🗓 | Last update: <t:{Config.lastUpdate}:R>
                """;

            string storageUtilization = $"""
                🗃 | Extensions: {Config.moduleCount}
                📁 | File size (not compiled): {GetTotalSize():#.00} KB
                🔢 | CPU threads: {Environment.ProcessorCount}
                📈 | Memory usage: {(double)(Process.GetCurrentProcess().WorkingSet64 / 1e6):#.00} MB
                """;


            DateTime end = DateTime.Now;
            TimeSpan executionTime = end - start;

            string discordStats = $"""
                <:good_ping:890642508923682886> | Gateway latency: {this._client.Latency} ms
                ⏱️ | Code execution time: {executionTime.Milliseconds} ms
                """;

            var builder = new EmbedBuilder()
            {
                Title = "About ManageBot Plus",
                Description = $"See some information about ManageBot Plus!",
                Color = Config.color
            }
            .AddField("General", generalStats, false)
            .AddField("Performance", discordStats, false)
            .AddField("Storage", storageUtilization, false);

            await RespondAsync(embed: builder.Build());
        }

        [SlashCommand("support-server", "Get a link to join the support server!")]
        public async Task SupportServerAsync()
        {
            var builder = new ComponentBuilder()
                .WithButton(label: "Click here", url: Config.supportInvite, style: ButtonStyle.Link);
            await RespondAsync("Click to join the support server", components: builder.Build());
        }
    }
}
