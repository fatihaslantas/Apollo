using Apollo.Api.Interfaces;
using Apollo.Api.Models;
using Newtonsoft.Json;
using System.Text;

namespace Apollo.Api.Services;

public class EncryptionService : IEncryptionService
{
    private readonly HttpClient _client;
    public EncryptionService(HttpClient client)
    {
        _client = client;
    }

    public async Task<EncryptResponse> Encrypt(string text)
    {
        var response = new EncryptResponse();

        var request = new CryptoRequest(text);
        var jsonContent = JsonConvert.SerializeObject(request);
        var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var apiResponse = await _client.PostAsync("/encrypt", contentString);
        if (!apiResponse.IsSuccessStatusCode)
        {
            response.ErrorCode = "5001";
            response.Message = "Unhandled exception";
            return response;
        }
        
        string responseBody = await apiResponse.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<EncryptApiResponse>(responseBody);

        return response;
    }

    public async Task<EncryptResponse> Decrypt(string text)
    {
        var response = new EncryptResponse();

        var request = new CryptoRequest(text);
        var jsonContent = JsonConvert.SerializeObject(request);
        var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var apiResponse = await _client.PostAsync("/decrypt", contentString);
        if (!apiResponse.IsSuccessStatusCode)
        {
            response.ErrorCode = "5001";
            response.Message = "Unhandled exception";
            return response;
        }
        string responseBody = await apiResponse.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<EncryptApiResponse>(responseBody);

        return response;
    }
}
