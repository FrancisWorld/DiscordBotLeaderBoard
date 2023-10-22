using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserCache
    {
        public List<User> VefiryCache(ulong guildId);

        public void SetCache(ulong guildId, List<User> users);
    }
}
