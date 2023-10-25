using DSharpPlus.Entities;
using Domain.Services;
using Domain.Models;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Interactivity;

namespace Bot.SlashCommands
{
    internal sealed class UserCommands : ApplicationCommandModule
    {
        private readonly UserService _userServices;

        public UserCommands(UserService userServices)
        {
            _userServices = userServices;
        }



        [SlashCommand("register", "forneça seu nick para participar do rank do servidor")]
        public async Task RegisterNewUser(InteractionContext ctx, 
            [Option("nick", "Seu nick")] string gameGickName)
        {

            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            var result = _userServices.TryAddNewUser(ctx.User.Id,
                gameGickName, ctx.User.Username,
                ctx.User.AvatarUrl,
                ctx.Guild.Id);


            await ctx.EditResponseAsync(
                new DiscordWebhookBuilder().
                WithContent(result));


        }



        [SlashCommand("rank", "Rank dos melhores jogadores do servidor")]
        public async Task RankUsers(InteractionContext ctx)
        {
            var rank = _userServices.GetRankByGuildId(ctx.Guild.Id);
            List<Page> pages = new List<Page>();

            foreach (var item in rank)
            {
                pages.Add(new Page(item));
            }
            

            if(pages.Any())
                await ctx.Client.GetInteractivity()
                    .SendPaginatedResponseAsync(
                    ctx.Interaction, 
                    false ,ctx.User, 
                    pages.AsEnumerable());

            else
                await ctx.CreateResponseAsync(
                    InteractionResponseType.ChannelMessageWithSource, 
                    new DiscordInteractionResponseBuilder()
                    .WithContent("Success!"));

        }
    }
}