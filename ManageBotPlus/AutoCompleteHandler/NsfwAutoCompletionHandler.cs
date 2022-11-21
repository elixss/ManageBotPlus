using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace ManageBotPlus
{
    public class NsfwAutoCompleteHandler : AutocompleteHandler
    {
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

            if (context.Channel is ITextChannel channel)
            {
                if (!channel.IsNsfw)
                {
                    autocompletions.Add(new AutocompleteResult("This only works in NSFW channels.", "nsfw_only"));
                    return AutocompletionResult.FromSuccess(autocompletions);
                }
                else if (channel.IsNsfw)
                {
                    foreach (string category in categories)
                    {
                        autocompletions.Add(new AutocompleteResult(category, category));
                    }
                    return AutocompletionResult.FromSuccess(autocompletions.Take(25));
                }
            }
            autocompletions.Add(new AutocompleteResult("Something went massively wrong.", "ERROR"));
            return AutocompletionResult.FromSuccess(autocompletions);

        }
    }
}
