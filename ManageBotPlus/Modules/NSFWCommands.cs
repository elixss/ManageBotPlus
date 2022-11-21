using Discord;
using Discord.Interactions;
using System.Net.Http.Headers;

namespace ManageBotPlus
{
    public class NSFWCommands : InteractionModuleBase
    {
        private readonly IServiceProvider _service;
        public NSFWCommands(IServiceProvider serviceProvider)
        {
            this._service = serviceProvider;
        }

        [RequireNsfw]
        [SlashCommand("nsfw", "This command displays NSFW content from given categories.")]
        public async Task NsfwAsync(
            [Summary("category", "Choose the category to see the content from"), Autocomplete(typeof(NsfwAutoCompleteHandler))] string category,
            [Summary("hidden", "Privacy is respected. Whether this is hidden, or not."), Choice("Yes", "true"), Choice("No", "false")] string? hidden = null
        )
        {
            await DeferAsync(ephemeral: bool.Parse(hidden ?? "false"));

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "015445535454455354D6");
            HttpResponseMessage result = await client.GetAsync($"https://nekobot.xyz/api/image?type={category.ToString().ToLower()}");

            using Stream stream = await result.Content.ReadAsStreamAsync();
            NsfwResponse response = await JsonUtil.GetJsonAsync<NsfwResponse>(stream);

            var builder = new EmbedBuilder()
            {
                Description = $"Content from category: {category}",
                Color = 0xac45ec,
                ImageUrl = response.Message
            };
            await ModifyOriginalResponseAsync(message => message.Embed = builder.Build());
        }
    }
}
