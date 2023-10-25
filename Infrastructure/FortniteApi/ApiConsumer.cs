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
                Environment.
                GetEnvironmentVariable
                ("FORTNITE_API_KEY",
                EnvironmentVariableTarget.User);

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

        public int GetKillStats(string nickName)
        {
            var account = _client.V2.Stats.GetBrV2Async
                (x => x.Name = nickName).GetAwaiter ().GetResult();

            if(account.IsSuccess)
            {
                var kills = account.Data.Stats.All.Ltm.Kills;

                return (int)kills;
            }

            return 0;
        }

    }
}