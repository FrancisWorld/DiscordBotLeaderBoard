using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepositorie<User>
    {
        public UserRepository(AppDbContext context) :base(context) { }


        /*public List<User> GetAllByGuildId(ulong guildId)
        {
            var userList = _context.Set<User>().
                Where(g => g.GuildId == guildId).ToList();

            return userList.Any() ? userList : new List<User>();
        }*/
    }
}
