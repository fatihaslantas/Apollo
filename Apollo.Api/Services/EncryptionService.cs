using Apollo.Api.Interfaces;
namespace Apollo.Api.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly HttpClient _client;
        public EncryptionService(HttpClient client)
        {
            _client = client;
        }

        public void Encrypt(string text)
        {

        }

        public void Decrypt(string text)
        {

        }
    }
}