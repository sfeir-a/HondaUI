namespace LenelConfigService.Services
{
    public interface IEncryptService
    {
        byte[] Encrypt(string plaintext);
        string Decrypt(byte[] cipherBytes);
    }
}