using Domain.Interfaces;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.AuthConfigs
{
    public class KeyManager : IKeyManager
    {
        private const string fakeKey = "XhaveSecret4kkkK";
        private const string fakeIv = "21354235349540I0";

        public string GenerateAndEncrypt(ulong guildId)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(fakeKey);
                    aesAlg.IV = Encoding.UTF8.GetBytes(fakeIv);

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (BinaryWriter bw = new BinaryWriter(csEncrypt))
                            {
                                bw.Write(guildId);
                            }
                        }

                        byte[] encryptedBytes = msEncrypt.ToArray();
                        return BitConverter.ToString(encryptedBytes).Replace("-", ""); // Converte para uma string hexadecimal
                    }
                }
            }

            catch (Exception ex) { return string.Empty; }
        }


        public ulong Decrypt(string encryptedKey)
        {
            byte[] encryptedBytes = new byte[encryptedKey.Length / 2];
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                encryptedBytes[i] = byte.Parse(encryptedKey
                    .Substring(2 * i, 2), 
                    NumberStyles.HexNumber);
            }

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(fakeKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(fakeIv);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (BinaryReader br = new BinaryReader(csDecrypt))
                {
                    return br.ReadUInt64();
                }
            }
        }

    }
}
