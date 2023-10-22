using Domain.Interfaces;
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
            var a = _client.V2.Stats.GetBrV2Async(x => x.Name = nickName).GetAwaiter().GetResult();

            if(a.IsSuccess)
                return true;

            return false;
        }

    }
}