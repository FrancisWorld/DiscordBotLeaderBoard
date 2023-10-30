using Domain.Interfaces;
using Domain.Models;
using Fortnite_API;

namespace Infrastructure.FortniteApi
{
    public class ApiConsumer : IApiConsumer
    {

        public string? API_KEY;
        private readonly FortniteApiClient _client;

        public ApiConsumer()
        {
            API_KEY =
                Environment.GetEnvironmentVariable("FORTNITE_API_KEY");

            _client = new FortniteApiClient(API_KEY);
        }


        public bool CheckAccountExists(string nickName)
        {
            var account = _client.V2.Stats.GetBrV2Async
                (x => x.Name = nickName).GetAwaiter().GetResult();

            if(account.IsSuccess)
                return true;

            return false;
        }

        public double GetKillStats(string nickName)
        {
            var account = _client.V2.Stats.GetBrV2Async
                (x => x.Name = nickName).GetAwaiter ().GetResult();

            if(account.IsSuccess &&
                account.Data.Stats.All.Overall != null)
            {
                var kills = account.Data.Stats.All.Overall.Kills;

                return kills;
            }

            return 0;
        }


        public double GetWinRate(string nickName)
        {
            var account = _client.V2.Stats.GetBrV2Async
                (x => x.Name = nickName).GetAwaiter().GetResult();

            if (account.IsSuccess &&
                account.Data.Stats.All.Overall != null)
            {
                var winRate = account.Data.Stats.All.Overall.WinRate;

                return winRate;
            }

            return 0;
        }

    }
}