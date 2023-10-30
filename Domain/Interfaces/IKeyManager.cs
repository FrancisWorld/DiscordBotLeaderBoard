using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IKeyManager
    {
        public string GenerateAndEncrypt(ulong guildId);

        public ulong Decrypt(string encryptedKey);
    }
}
