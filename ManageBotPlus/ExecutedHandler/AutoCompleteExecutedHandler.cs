using Discord;
using Discord.Commands;
using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBotPlus.ExecutedHandler
{
    internal static class AutoCompleteExecutedHandler
    {
        public static async Task AutoCompleteExecuted(AutocompleteCommandInfo info, IInteractionContext context, Discord.Commands.IResult result)
        {
            if (result.IsSuccess)
            {
                switch (result.Error)
                {
                    case CommandError.UnmetPrecondition:
                        IEnumerable<AutocompleteResult> results = new[] { new AutocompleteResult($"Failed to load due to: {result.ErrorReason}", "error") };
                        info.
                        break;
                    default:
                        break;

                }
            }
        }
    }
}
