using Domain.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {

        #pragma warning disable CS8618

        public AppDbContext() : base () {}



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guild>()
                .ToTable("guild").HasKey(e => e.Id);

            modelBuilder.Entity<User>()
                .ToTable("user").HasKey(e => e.Id);


            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 385869185198063626,
                    DiscordNickName = "Cleiton Furst",
                    GuildId = 1038639386646032415,
                    Kills = 640,
                    WinRate = 12.2,
                    IsRanked = true,
                    GameNickName = "Jhpo",
                    ThumbUrl = "https://cdn.discordapp.com/embed/avatars/0.png?size=1024"
                },

                new User()
                {
                    Id = 926292122842312795,
                    DiscordNickName = "camargooo",
                    GuildId = 1038639386646032415,
                    Kills = 99,
                    WinRate = 0.5,
                    IsRanked = true,
                    GameNickName = "Faker171",
                    ThumbUrl = "https://cdn.discordapp.com/embed/avatars/0.png?size=1024"
                },

                new User()
                {
                    Id = 1134185785617293402,
                    DiscordNickName = "yuru44",
                    GuildId = 1038639386646032415,
                    Kills = 93,
                    WinRate = 10,
                    IsRanked = true,
                    GameNickName = "Oyurii",
                    ThumbUrl = "https://cdn.discordapp.com/embed/avatars/0.png?size=1024"
                },

                new User()
                {
                    Id = 732999507331252246,
                    DiscordNickName = "VicManzas",
                    GuildId = 1038639386646032415,
                    Kills = 117,
                    WinRate = 45.8,
                    IsRanked = true,
                    GameNickName = "manzass",
                    ThumbUrl = "https://cdn.discordapp.com/embed/avatars/0.png?size=1024"
                }
            );
        }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Bot.db");
            }
        }

        public DbSet<User> Users {get; set;}
        public DbSet<Guild> Guilds {get; set;}
    }
}