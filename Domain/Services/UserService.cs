using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using System;


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

                if (guild.RankingTypes == RankingTypes.Kills)
                    return AllToString(rankUsers.OrderByDescending(
                        x => x.Kills)
                        .ToList(), guild.RankingTypes);

                else
                    return AllToString(rankUsers.OrderByDescending(
                        x => x.WinRate)
                        .ToList(), guild.RankingTypes);

            }

            else
            {
                if (guild.RankingTypes == RankingTypes.Kills)
                    return AllToString(cacheUsers.OrderByDescending(
                        x => x.Kills)
                        .ToList(), guild.RankingTypes);

                else
                    return AllToString(cacheUsers.OrderByDescending(
                        x => x.WinRate)
                        .ToList(), guild.RankingTypes);
            }

        }



        public string TryAddNewUser(ulong id, string gameNickName,
            string discordNickName, string thumbUrl, ulong guildId)
        {
            try
            {

                double rankPoints = 0;
                
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

                    if (guild.RankingTypes == RankingTypes.Kills)
                        rankPoints = _api.GetKillStats(gameNickName);

                    else if (guild.RankingTypes == RankingTypes.WinRate)
                        rankPoints = _api.GetWinRate(gameNickName);

                    #endregion

                    var user = new User
                        (id,
                        gameNickName,
                        discordNickName,
                        thumbUrl,
                        true);

                    user.GuildId = guildId;

                    switch (guild.RankingTypes)
                    {
                        case RankingTypes.Kills:
                            user.Kills = (int)rankPoints;
                            break;
                        case RankingTypes.WinRate:
                            user.WinRate = rankPoints;
                            break;
                        default:
                            user.WinRate = 0;
                            user.Kills = 0;
                            break;
                    }

                    _userRepository.Save(user);

                    
                }


                return "Salvo com sucesso";
            }

            catch(PersistenceFailureException ex)
            {
                return ex.Message;
            }
        }


        public List<string> ConfigureRank(RankingTypes rankingType, ulong guildId)
        {
            try
            {
                var guild = _guildRepository.GetById(guildId);

                guild.RankingTypes = rankingType;

                _guildRepository.Update(guild);

                var listResult = new List<string>();
                listResult.Add($"As configurações em {guild.Name} foram atualizadas!");
                listResult.Add("Sucesso!");


                return listResult;
            }

            catch(Exception ex)
            {
                var list = new List<string>();
                list.Add("Não foi possível atualizar as configurações!");
                list.Add("Erro");


                return list;
            }
        }



        /// <summary>
        /// Transforma todas as informações em uma lista de string para ser exibido pelo bot
        /// </summary>
        /// <param name="userList"></param>
        /// <param name="rankingType"></param>
        /// <returns></returns>
        private List<string> AllToString(List<User> userList, RankingTypes rankingType)
        {
            string result = "";
            List<string> resultList = new List<string>();
            int index = 1;
            string rankingName;

            switch (rankingType)
            {
                case RankingTypes.Kills:
                    rankingName = "Kills";
                    break;
                case RankingTypes.WinRate:
                    rankingName = "Win Rate";
                    break;
                default:
                    rankingName = "Xp";
                    break;
            }


            if (rankingType == RankingTypes.Kills)
                foreach (var item in userList)
                {
                    result += $"{index} - {item.DiscordNickName} | {rankingName} : *{item.Kills}*\n";


                    resultList.Add(result);
                    result = "";


                    index++;
                }


            else
                foreach (var item in userList)
                {
                    result += $"{index} - {item.DiscordNickName} | {rankingName} : *{item.WinRate}%*\n";

                    //if (index % 10 == 0 || index == userList.Count)

                    resultList.Add(result);
                    result = "";

                    index++;
                }

            return resultList;
        }


    }
}