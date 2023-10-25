using Domain.Models;
using Domain.Services;
using DSharpPlus.EventArgs;

namespace Bot.Events
{
    public class EventosGerais
    {
        private readonly GuildService _guildService;

        public EventosGerais(GuildService guildService)
        {
            _guildService = guildService;


            StartUpBot.Client.GuildMemberAdded += NovosUsuarios;
            StartUpBot.Client.MessageCreated += NovaMenssagem;
            StartUpBot.Client.GuildAvailable += AdicionarGuilda;
        }

        private async Task NovaMenssagem(DiscordClient s, MessageCreateEventArgs e)
        {
            Console.WriteLine("Menssagem de: " + e.Author.Username);
        }


        private async Task NovosUsuarios(DiscordClient s, GuildMemberAddEventArgs e)
        {
            if (!e.Member.IsBot)
            {
                try{
                var dmChannel = await e.Member.CreateDmChannelAsync();

                await dmChannel.SendMessageAsync($"bem vindo a {e.Guild.Name}" +
                 "para verificar seu score e estar no rank"+
                 "do servidor digite o comando /register");
                }

                catch(Exception ex)
                {

                }
            }
        }

        private async Task AdicionarGuilda(DiscordClient s, GuildCreateEventArgs e)
        {
            _guildService.AddGuildToDatabase(e.Guild.Id, 
                e.Guild.Name, 
                e.Guild.OwnerId);
        }
    }
}