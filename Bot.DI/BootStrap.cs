using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories;
using Domain.Services;
using Infrastructure.Data.Context;
using Infrastructure.FortniteApi;
using Domain.Interfaces;
using Infrastructure.Data.Cache;

namespace Bot.DI;

public class BootStrap
{
    #pragma warning disable
    public static void Configure(ServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped(typeof(UserService));
        services.AddScoped(typeof(GuildService));
        services.AddScoped(typeof(ApiConsumer));
        services.AddTransient(typeof(IRepository<>), typeof(GenericRepositorie<>));
        services.AddTransient(typeof(IApiConsumer), typeof(ApiConsumer));
        services.AddTransient(typeof(IUserCache), typeof(UserCache));

        //services.AddTransient<IRepository<User>, UserRepository>();




        var _services = services.BuildServiceProvider();

        var dbCntext = _services.GetService<AppDbContext>();
        dbCntext.Database.EnsureCreated();

    }
}