using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace ManageBotPlus
{
    public class NsfwAutoCompleteHandler : AutocompleteHandler
    {
        private readonly IServiceProvider _services;
        private readonly DiscordSocketClient _client;
        public NsfwAutoCompleteHandler(IServiceProvider services)
        {
            this._services = services;
            this._client = services.GetRequiredService<DiscordSocketClient>();
        }

        public override async Task<AutocompletionResult> GenerateSuggestionsAsync(
            IInteractionContext context,
            IAutocompleteInteraction autocompleteInteraction,
            IParameterInfo parameter,
            IServiceProvider services
        )
        {
            string[] categories = { "Hentai", "Ass", "Pgif", "Thigh", "Hass", "Hboobs", "Boobs", "Pussy", "Paizuri", "Lewdneko", "Hyuri",
                "Hthigh", "Hmidriff", "Anal", "Feet", "Blowjob", "Gonewild", "Hkitsune", "Tentacle", "4K", "Kanna",
                "Hentai_Anal", "Yaoi"
            };
            List<AutocompleteResult> autocompletions = new();


            if (context.Channel is ITextChannel channel && channel.IsNsfw)
            {
                foreach (string category in categories)
                {
                    autocompletions.Add(new AutocompleteResult(category, category));
                }
                return AutocompletionResult.FromSuccess(autocompletions.Take(25));
            }
            autocompletions.Add(new AutocompleteResult("This only works in NSFW channels.", "nsfw_only"));
            return AutocompletionResult.FromSuccess(autocompletions.Take(25));
        }
    }
}
