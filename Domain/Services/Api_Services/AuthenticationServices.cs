using Domain.Interfaces;

namespace Domain.Services.Api_Services
{
    public class AuthenticationServices
    {
        private readonly IKeyManager _keyManager;

        public AuthenticationServices(IKeyManager keyManager)
        {
            _keyManager = keyManager;
        }


        public string GenerateKey(ulong guildId)
        {
            var resultKey = _keyManager.GenerateAndEncrypt(guildId);

            return resultKey;
        }


        public ulong ReadCheckKey(string key)
        {
            var result = _keyManager.Decrypt(key);

            return result;
        }
    }
}
