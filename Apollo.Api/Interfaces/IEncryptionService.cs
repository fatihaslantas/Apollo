
namespace Apollo.Api.Interfaces
{
    public interface IEncryptionService
    {
        void Decrypt(string text);
        void Encrypt(string text);
    }
}