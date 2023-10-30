using Domain.DTOs;
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



        public List<RankedUsersDTO> GetRankByGuildId(ulong guildId)
        {
            var users = _userRepository.GetAllById(guildId);
            var guild = _guildRepository.GetById(guildId);

            if (!users.Any())
            {
                return Enumerable.
                    Empty<RankedUsersDTO>().ToList();
            }


            var rankUsers = users
                .Where(x => x.IsRanked).ToList();

            if (guild != null &&
                guild.RankingTypes == RankingTypes.Kills)
            {
                rankUsers = rankUsers.OrderByDescending(
                    x => x.Kills)
                    .ToList();

                List<RankedUsersDTO> rankResult = new();

                foreach (var item in rankUsers)
                {
                    rankResult.Add(new RankedUsersDTO()
                    {
                        UserName = item.DiscordNickName,
                        RankPoints = item.Kills,
                        ThumbUrl = item.ThumbUrl
                    });
                }

                return rankResult;

            }

            else if(guild != null)
            {
                rankUsers = rankUsers.OrderByDescending(
                    x => x.WinRate)
                    .ToList();

                List<RankedUsersDTO> rankResult = new();

                foreach (var item in rankUsers)
                {
                    rankResult.Add(new RankedUsersDTO()
                    {
                        UserName = item.DiscordNickName,
                        RankPoints = item.WinRate,
                        ThumbUrl = item.ThumbUrl
                    });
                }

                return rankResult;
            }

            return Enumerable.Empty<RankedUsersDTO>().ToList();
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