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
        }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=C:\\Bot\\bot.db");
            }
        }

        public DbSet<User> Users {get; set;}
        public DbSet<Guild> Guilds {get; set;}
    }
}