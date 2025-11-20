using System.Security.Cryptography;
using System.Text;

namespace LenelConfigService.Services
{
    public class AesEncryptService : IEncryptService
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public AesEncryptService(IConfiguration config)
        {
            var key = config.GetValue<string>("Encryption:Key");
            var iv = config.GetValue<string>("Encryption:IV");

            if (key is null || iv is null)
                throw new InvalidOperationException("Encryption key or IV missing from configuration.");

            _key = Convert.FromBase64String(key);
            _iv = Convert.FromBase64String(iv);
        }

        public byte[] Encrypt(string plaintext)
        {
            if (string.IsNullOrEmpty(plaintext))
                return Array.Empty<byte>();

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plaintext);

            return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        public string Decrypt(byte[] cipherBytes)
        {
            if (cipherBytes == null || cipherBytes.Length == 0)
                return string.Empty;

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}