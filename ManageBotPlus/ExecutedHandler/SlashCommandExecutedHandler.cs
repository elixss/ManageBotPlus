using Discord;
using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBotPlus
{
    internal static class SlashCommandExecutedHandler
    {
        public static async Task SlashCommandExecuted(SlashCommandInfo info, IInteractionContext context, IResult result)
        {
            if (!result.IsSuccess)
            {
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        await context.Interaction.RespondAsync($"Unmet command condition: {result.ErrorReason}", ephemeral: true);
                        break;
                    case InteractionCommandError.Unsuccessful:
                        await context.Interaction.RespondAsync($"An error occurred: {result.ErrorReason}", ephemeral: true);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
