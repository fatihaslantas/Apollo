using Apollo.Api.Models;
namespace Apollo.Api.Interfaces
{
    public interface IEncryptionService
    {
        Task<EncryptResponse> Decrypt(string text);
        Task<EncryptResponse> Encrypt(string text);
    }
}