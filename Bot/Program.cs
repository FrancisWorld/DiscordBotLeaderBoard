using Bot;
using Bot.DI;
using Bot.Events;
using Domain.Services;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


ServiceCollection services = new();

services.AddDbContext<AppDbContext>();


//Configuração de Injeção de dependencia
BootStrap.Configure(services);

StartUpBot bot = new();
bot.ConfigureClient(services);

new EventosGerais(services.BuildServiceProvider()
    .GetService<GuildService>());

await bot.RunBot();