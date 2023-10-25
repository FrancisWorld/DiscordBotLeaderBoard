using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories;
using Domain.Services;
using Infrastructure.Data.Context;
using Infrastructure.FortniteApi;
using Domain.Interfaces;
using Infrastructure.Data.Cache;
using Domain.Models;
using Domain.Services.Api_Services;

namespace Bot.DI;

public class BootStrap
{
    public static AppDbContext GlobalServices;

    #pragma warning disable
    public static void ConfigureBotClientDI(ServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddTransient(typeof(UserService));
        services.AddTransient(typeof(GuildService));
        services.AddTransient(typeof(ApiConsumer));
        services.AddTransient(typeof(IRepository<>), typeof(GenericRepositorie<>));
        services.AddTransient(typeof(IRepository<User>), typeof(UserRepository));
        services.AddTransient(typeof(IApiConsumer), typeof(ApiConsumer));
        services.AddSingleton(typeof(IUserCache), typeof(UserCache));

        //services.AddTransient<IRepository<User>, UserRepository>();

        var _services = services.BuildServiceProvider();

        var dbCntext = _services.GetService<AppDbContext>();
        dbCntext.Database.EnsureCreated();

    }

    public static void ConfigureApiDI(IServiceCollection services)
    {
        services.AddTransient<RankService>();
        services.AddScoped<AppDbContext>();
        services.AddTransient(typeof(IRepository<>), typeof(GenericRepositorie<>));
        services.AddTransient<ConfigBotService>();
        services.AddTransient(typeof(IRepository<User>), typeof(UserRepository));
    }
}