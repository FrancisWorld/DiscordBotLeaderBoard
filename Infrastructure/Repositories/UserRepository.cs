using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepositorie<User>
    {
        public UserRepository(AppDbContext context) :base(context) { }



        public override IEnumerable<User> GetAllById(ulong id)
        {
            var userList = _context.Set<User>().
                Where(g => g.GuildId == id).ToList();

            return userList.Any() ? userList : Enumerable.Empty<User>();
        }

    }
}
