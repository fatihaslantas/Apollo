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
        try
        {
            var apiResponse = await _client.PostAsync("/encrypt", contentString);
            apiResponse.EnsureSuccessStatusCode();
            string responseBody = await apiResponse.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<EncryptApiResponse>(responseBody);

            if (data == null)
            {
                response.ErrorCode = "5002";
                response.Message = "Unable to get encrypted data from api.";
            }

            response.Data = data.Data;
            return response;

        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            response.ErrorCode = "4040";
            response.Message = "Encryption service api not found.";
        }
        catch (HttpRequestException ex)
        {
            response.ErrorCode = "5004";
            response.Message = ex.Message;
        }
        return response;
    }

    public async Task<EncryptResponse> Decrypt(string text)
    {
        var response = new EncryptResponse();

        var request = new CryptoRequest(text);
        var jsonContent = JsonConvert.SerializeObject(request);
        var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        try
        {
            var apiResponse = await _client.PostAsync("/decrypt", contentString);
            apiResponse.EnsureSuccessStatusCode();
            string responseBody = await apiResponse.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<EncryptApiResponse>(responseBody);

            if (data == null)
            {
                response.ErrorCode = "5003";
                response.Message = "Unable to get decrypted data from api.";
            }

            response.Data = data.Data;
            return response;

        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            response.ErrorCode = "4040";
            response.Message = "Encryption service api not found.";
        }
        catch (HttpRequestException ex)
        {
            response.ErrorCode = "5004";
            response.Message = ex.Message;
        }
        return response;
    }
}
