using Discord;
using Discord.Interactions;
using System.Net.Http.Headers;

namespace ManageBotPlus
{
    [RequireNsfw]
    [Group("nsfw", "Various commands that are NSFW related.")]
    public class NSFWCommands : InteractionModuleBase, IManageBotModule
    {
        private readonly IServiceProvider _service;
        public NSFWCommands(IServiceProvider serviceProvider)
        {
            this._service = serviceProvider;
        }

        private static async Task<EmbedBuilder> GetNsfwEmbed(string category)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "015445535454455354D6");
            HttpResponseMessage result = await client.GetAsync($"https://nekobot.xyz/api/image?type={category.ToString().ToLower()}");

            using Stream stream = await result.Content.ReadAsStreamAsync();
            NsfwResponse response = await JsonUtil.GetJsonAsync<NsfwResponse>(stream);

            var builder = new EmbedBuilder()
            {
                Description = $"Content from category: {category}",
                Color = Config.color,
                ImageUrl = response.Message
            };
            return builder;
        }

        [SlashCommand("show", "This command displays NSFW content.")]
        public async Task NsfwAsync(
            [Summary("category", "Choose the category to see the content from"), Autocomplete(typeof(NsfwAutoCompleteHandler))] string category,
            [Summary("hidden", "Privacy is respected. Whether this is hidden, or not."), Choice("Yes", "true"), Choice("No", "false")] string? hidden = null
        )
        {
            await DeferAsync(ephemeral: bool.Parse(hidden ?? "false"));

            await ModifyOriginalResponseAsync(message => message.Embed = GetNsfwEmbed(category).GetAwaiter().GetResult().Build());
        }

        [SlashCommand("dm", "Sends you NSFW content into your DMs.")]
        public async Task NsfwDmAsync(
            [Summary("category", "Choose the category to see the content from"),
            Autocomplete(typeof(NsfwAutoCompleteHandler))] string category
        )
        {
            await DeferAsync(ephemeral: true);
            try
            {
                await Context.User.SendMessageAsync(embed: (await GetNsfwEmbed(category)).Build());
                await ModifyOriginalResponseAsync(message => message.Content = "Check your DMs!");
            }
            catch (Discord.Net.HttpException ex)
            {
                if (ex.DiscordCode == DiscordErrorCode.CannotSendMessageToUser)
                {
                    await ModifyOriginalResponseAsync(async message =>
                    {
                        message.Content = "Couldn't DM you. I send it here instead:";
                        message.Embed = (await GetNsfwEmbed(category)).Build();
                    });
                    return;
                }
                await ModifyOriginalResponseAsync(message => message.Content = $"Something went frong: {ex.Message}");
            }
        }
    }
}
