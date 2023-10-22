using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;


namespace Domain.Services
{
    public class UserService
    {
        private readonly IRepository<User> _repository;
        private readonly IApiConsumer _api;
        private readonly IUserCache _cache;

        public UserService(IRepository<User> repository, IApiConsumer api, IUserCache cache)
        {
            _repository = repository;
            _api = api;
            _cache = cache;
        }


        public List<User> GetRankByGuildId(ulong guildId)
        {
            var cacheUsers = _cache.VefiryCache(guildId);

            if (!cacheUsers.Any())
            {

                var users = _repository.GetAll();

                if (!users.Any())
                {
                    return Enumerable.Empty<User>().ToList();
                }

                
                var rankUsers = users
                    .Where(x => x.GuildId == guildId)
                    .Where(x => x.IsRanked);

                if (rankUsers.Any())
                {
                    _cache.SetCache(guildId, rankUsers.ToList());

                    return rankUsers.OrderBy(x => x.RankPoints).ToList();
                }
            }

            else
            {
                return cacheUsers.OrderBy(x => x.RankPoints).ToList();
            }

            return Enumerable.Empty<User>().ToList();

        }



        public string TryAddNewUser(ulong id, string gameNickName,
            string discordNickName, string thumbUrl, ulong guildId)
        {
            try
            {
                //Verifica conta de usuário na API do jogo
                if (!_api.CheckAccountExists(gameNickName))
                    return "Seu nick não corresponde a nenhuma" +
                        " conta no jogo, verifique novamente!";

                else
                {
                    //Verifica se conta já está salva no banco
                    var userInDb = _repository.GetById(id);

                    if (userInDb is not null)
                        return "Você já está registrado!";

                    #region Logica rankeamento

                    //verificar config da guilda

                    #endregion

                    var user = new User
                        (id,
                        gameNickName,
                        discordNickName,
                        thumbUrl,
                        true);

                    user.GuildId = guildId;

                    _repository.Save(user);

                    
                }


                return "Salvo com sucesso";
            }

            catch(PersistenceFailureException ex)
            {
                return ex.Message;
            }
        }

    }
}