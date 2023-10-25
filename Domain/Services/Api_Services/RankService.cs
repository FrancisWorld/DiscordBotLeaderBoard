using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Api_Services
{
    public class RankService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Guild> _guildRepository;

        public RankService(IRepository<User> userRepository, IRepository<Guild> guildRepository)
        {
            _userRepository = userRepository;
            _guildRepository = guildRepository;
        }

        public List<User> GetRankByGuildId(ulong guildId)
        {
            var users = _userRepository.GetAllById(guildId);

            if (!users.Any())
            {
                return Enumerable.Empty<User>().ToList();
            }


            var rankUsers = users
                .Where(x => x.IsRanked).ToList();

            return rankUsers.OrderBy(
                x => x.RankPoints).ToList();
        }


        public void ConfigureRank(List<ulong> blockedUsers, RankingTypes rankingType, ulong guildId)
        {
            var guild = _guildRepository.GetById(guildId);

            foreach (var userId in blockedUsers)
            {
                var user = _userRepository.GetById(userId);
                user.IsRanked = false;

                _userRepository.Update(user);
            }

            guild.RankingTypes = rankingType;

            _guildRepository.Update(guild);
        }
    }
}