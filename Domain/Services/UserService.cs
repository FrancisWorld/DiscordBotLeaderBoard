using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;


namespace Domain.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IApiConsumer _api;
        private readonly IUserCache _cache;
        private readonly IRepository<Guild> _guildRepository;

        public UserService(IRepository<User> repository, 
            IApiConsumer api, 
            IUserCache cache, 
            IRepository<Guild> guildRepository)
        {
            _userRepository = repository;
            _api = api;
            _cache = cache;
            _guildRepository = guildRepository;
        }


        public List<string> GetRankByGuildId(ulong guildId)
        {
            var cacheUsers = _cache.VefiryCache(guildId);
            var guild = _guildRepository.GetById(guildId);


            if (!cacheUsers.Any())
            {

                var users = _userRepository.GetAllById(guildId);

                if (!users.Any())
                {
                    return Enumerable.Empty<string>().ToList();
                }


                var rankUsers = users
                    .Where(x => x.IsRanked).ToList();

                _cache.SetCache(guildId, rankUsers);

                return AllToString(rankUsers.OrderBy(
                    x => x.RankPoints)
                    .ToList(), guild.RankingTypes);

            }

            else
            {
                return AllToString(cacheUsers.OrderBy(
                    x => x.RankPoints)
                    .ToList(), guild.RankingTypes);
            }

        }



        public string TryAddNewUser(ulong id, string gameNickName,
            string discordNickName, string thumbUrl, ulong guildId)
        {
            try
            {

                int rankPoints = 0;
                
                //Verifica conta de usuário na API do jogo
                if (!_api.CheckAccountExists(gameNickName))
                    return "Seu nick não corresponde a nenhuma" +
                        " conta no jogo, verifique novamente!";

                else
                {
                    //Verifica se conta já está salva no banco
                    var userInDb = _userRepository.GetById(id);

                    if (userInDb is not null)
                        return "Você já está registrado!";

                    var guild = _guildRepository.GetById(guildId);

                    #region Logica de rankeamento

                    if (guild.RankingTypes == RankingTypes.ByKills)
                        rankPoints = _api.GetKillStats(gameNickName);


                    #endregion

                    var user = new User
                        (id,
                        gameNickName,
                        discordNickName,
                        thumbUrl,
                        true);

                    user.GuildId = guildId;
                    user.RankPoints = rankPoints;

                    _userRepository.Save(user);

                    
                }


                return "Salvo com sucesso";
            }

            catch(PersistenceFailureException ex)
            {
                return ex.Message;
            }
        }




        private List<string> AllToString(List<User> userList, RankingTypes rankingType)
        {
            string result = "";
            List<string> resultList = new List<string>();
            int index = 1;
            string rankingName;

            if (rankingType == RankingTypes.ByKills)
                rankingName = "Kills";

            else if (rankingType == RankingTypes.ByWinRate)
                rankingName = "Win Rate";

            else
                rankingName = "Xp";


            foreach (var item in userList)
            {
                result += $"{index} - {item.DiscordNickName} | {rankingName} : *{item.RankPoints}*\n";

                if (index % 10 == 0 || index == userList.Count)
                {
                    resultList.Add(result);
                    result = "";
                }

                index++;
            }

            return resultList;
        }


    }
}