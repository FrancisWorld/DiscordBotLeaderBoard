using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Data.Cache
{
    public class UserCache : IUserCache
    {
        private readonly IMemoryCache _cache;

        public UserCache(IMemoryCache cache)
        {
            _cache = cache;
        }


        public List<User> VefiryCache(ulong guildId)
        {
            List<User> result = new();

            if(_cache.TryGetValue(guildId, out result))
            {
                return result;
            }

            return result;
        }


        public void SetCache(ulong guildId, List<User> users)
        {
            _cache.Set(guildId, users, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
            });
        }
    }
}