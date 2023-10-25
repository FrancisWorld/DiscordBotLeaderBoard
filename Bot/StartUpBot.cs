using Bot.Events;
using Bot.SlashCommands;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.DependencyInjection;

namespace Bot
{

    public class StartUpBot
    {        
        public static DiscordClient Client;

        public StartUpBot() { }


        public void ConfigureClient(ServiceCollection services)
        {

            var client = new DiscordClient(new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                AutoReconnect = true,
                Token = Environment.GetEnvironmentVariable("BOT_TEST"),
                TokenType = TokenType.Bot
            });


            // Configuração para comandos de texto

            /*var cmd = Client.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" },
                Services = services.BuildServiceProvider()
            });*/

            // Configuração para comandos de barra

            var slash = client.UseSlashCommands(new SlashCommandsConfiguration
            {
                Services = services.BuildServiceProvider()
            });

            //Registro de comandos de barra
            slash.RegisterCommands<UserCommands>();

            client.UseInteractivity(new InteractivityConfiguration
            {
                PaginationBehaviour = PaginationBehaviour.WrapAround,
                PaginationDeletion = PaginationDeletion.KeepEmojis,
                PaginationEmojis = new PaginationEmojis(),
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromMinutes(1),
            });

            Client = client;

            //cmd.RegisterCommands<TextCommand>();
            
        }



        public async Task RunBot()
        {
            await Client.ConnectAsync();
            Console.WriteLine("ON");
            await Task.Delay(-1);
        }
    }
    
}