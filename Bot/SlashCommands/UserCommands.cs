using DSharpPlus.Entities;
using Domain.Services;
using Domain.Models;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Interactivity;
using System.Runtime.CompilerServices;
using Domain.Services.Api_Services;
using Domain;
using Bot.Utils;

namespace Bot.SlashCommands
{
    internal sealed class UserCommands : ApplicationCommandModule
    {

        private const string siteUrl = "https://leaderboard-nine-amber.vercel.app/";
        private readonly UserService _userServices;
        private readonly AuthenticationServices _authenticationServices;

        public UserCommands(UserService userServices, AuthenticationServices authenticationServices)
        {
            _userServices = userServices;
            _authenticationServices = authenticationServices;
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
            var errorMessage = EmbedFormatter.SimpleEmbedFormatter("Ranking vazio!", "Nenhum usuário participa do LeaderBoard");

            if (rank.Count > 0)
            {

                var embed = EmbedFormatter.
                CustomEmbedFormatter("Ranking do Servidor", siteUrl, rank);

                await ctx.CreateResponseAsync(embed);
            }

            else await ctx.CreateResponseAsync(errorMessage, true);


            /*if(pages.Any())
                await ctx.Client.GetInteractivity()
                    .SendPaginatedResponseAsync(
                    ctx.Interaction, 
                    false ,ctx.User, 
                    pages.AsEnumerable());

            else
                await ctx.CreateResponseAsync(
                    InteractionResponseType.ChannelMessageWithSource, 
                    new DiscordInteractionResponseBuilder()
                    .WithContent("Não há nenhum"));*/

        }


        [SlashCommand("config", "Configurar o bot no painel online")]
        public async Task ConfigBot(InteractionContext ctx,
            [Option("rankconfig", "Define a base de pontuação do LeaderBoard")]
            RankingTypes rankingType)
        {
            if (ctx.Member.Permissions.HasPermission(Permissions.Administrator))
            {
                var response = _userServices.ConfigureRank(rankingType, ctx.Guild.Id);

                var embed = EmbedFormatter.SimpleEmbedFormatter(response);

                await ctx.CreateResponseAsync(embed, true);
            }

            else
                await ctx.CreateResponseAsync(
                    InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                    .WithContent("Você não tem autorização para usar esse comando"));
        }
    }
}