using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class GuildService
    {
        private readonly IRepository<Guild> _repository;

        public GuildService(IRepository<Guild> repository)
        {
            _repository = repository;
        }



        public void AddGuildToDatabase(ulong guildId, string guildName, ulong ownerId)
        {
            if (_repository.GetById(guildId) is null)
            {

                var newGuild = new Guild(guildId, guildName, ownerId);

                newGuild.RankingTypes = RankingTypes.Kills;

                _repository.Save(newGuild);
            }
        }
    }
}