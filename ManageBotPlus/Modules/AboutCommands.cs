using Discord;
using Discord.Interactions;
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
        public AboutCommands(IServiceProvider services)
        {
            this._services = services;
        }

        [SlashCommand("source", "View source code on GitHub!")]
        public async Task OpenSourceCommandAsync()
        {
            IEmote emote = Emote.Parse("<:github:935591565122998362>");
            var builder = new ComponentBuilder()
                .WithButton(label: "Click here", emote: emote, url: "https://github.com/elixss/ManageBotPlus", style: ButtonStyle.Link);
            await RespondAsync("Click to view my source code.", components: builder.Build());
        }

    }
}
