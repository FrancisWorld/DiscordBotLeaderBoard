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



        public void AddGuildToDatabase(ulong guildId, string nameId, ulong ownerId)
        {
            //_repository.Save(new Guild);
        }
    }
}