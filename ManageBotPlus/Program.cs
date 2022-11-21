using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace ManageBotPlus
{
    public class Program : IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private DiscordSocketClient? _client;
        private InteractionService? _interactionService;
        private const ulong TEST_GUILD_ID = 890279450376294453;
        public Program()
        {
            this._serviceProvider = CreateServiceProvider();
        }

        public static void Main() =>
            new Program().RunAsync().GetAwaiter().GetResult();

        public static IServiceProvider CreateServiceProvider()
        {
            var config = new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.None
            };

            var interConfig = new InteractionServiceConfig()
            {
                LogLevel = LogSeverity.Info
            };

            var collection = new ServiceCollection()
                .AddSingleton(config)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton(interConfig)
                .AddSingleton<InteractionService>();
            return collection.BuildServiceProvider();
        }

        public async Task RunAsync()
        {
            this._client = this._serviceProvider.GetRequiredService<DiscordSocketClient>();
            this._interactionService = this._serviceProvider.GetRequiredService<InteractionService>();

            this._client.Log += async message =>
            {
                await Task.CompletedTask;
                Console.WriteLine(message);
            };

            this._client.InteractionCreated += this.CheckForManageBot;
            this._interactionService.SlashCommandExecuted += SlashCommandExecutedHandler.SlashCommandExecuted;

            this._client.Ready += this.SetupCommandsAsync;
            await this._client.LoginAsync(TokenType.Bot, (await JsonUtil.GetJsonAsync<TokenModel>("token.json")).Token);
            await this._client.SetGameAsync("Hello World!");
            await this._client.StartAsync();


            await Task.Delay(Timeout.Infinite);
        }

        public async Task CheckForManageBot(SocketInteraction interaction)
        {
            IGuild guild = this._client.GetGuild((ulong)interaction.GuildId);
            if (interaction is SocketSlashCommand command)
            {
                AllowedGuildsModel guildIds = await JsonUtil.GetJsonAsync<AllowedGuildsModel>("allowedGuilds.json");
                if ((await guild.GetUserAsync(890273033204420678)) == null && !guildIds.AllowedGuilds.Contains((ulong)interaction.GuildId))
                {
                    await interaction.RespondAsync("It appears, that ManageBot is not on this server. To use this bot, you must [invite ManageBot](https://discord.com/api/oauth2/authorize?client_id=890273033204420678&permissions=1374859684086&scope=bot%20applications.commands). " +
                        "If you think this is a mistake, please [join the support server](https://discord.gg/5mYSubhkrg) or contact a server moderator.", ephemeral: true);
                    return;
                }
            }

            var ctx = new SocketInteractionContext(this._client, interaction);

            await this._interactionService.ExecuteCommandAsync(ctx, this._serviceProvider);
        }

        private async Task SetupCommandsAsync()
        {
            await this._interactionService.AddModuleAsync<NSFWCommands>(this._serviceProvider);
#if DEBUG
            await this._interactionService.RegisterCommandsToGuildAsync(TEST_GUILD_ID, true);
#else
            await this._interactionService?.RegisterCommandsGloballyAsync(true);
#endif
        }

        public void Dispose()
        {
            this._client!.LogoutAsync();
            this._client!.Dispose();
            Environment.Exit(0);
            GC.SuppressFinalize(this);
        }

    }
}