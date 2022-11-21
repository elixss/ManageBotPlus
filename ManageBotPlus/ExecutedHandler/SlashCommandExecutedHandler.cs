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
                        await context.Interaction.RespondAsync($"Requirements to execute the command are not met: {result.ErrorReason}", ephemeral: true);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
