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
                response.ErrorCode = ErrorCodes.EncryptedDataNull;
                response.Message = ErrorCodes.EncryptedDataNull;
            }

            response.Data = data.Data;
            return response;

        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            response.ErrorCode = (int)ErrorCodes.ServiceNotFound;
            response.Message = ErrorCodes.ServiceNotFound;
        }
        catch (HttpRequestException ex)
        {
            response.ErrorCode = ErrorCodes.GenericError;
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
                response.ErrorCode = ErrorCodes.DecryptedDataNull;
                response.Message = ErrorCodes.DecryptedDataNull;
            }

            response.Data = data.Data;
            return response;

        }
        catch (HttpRequestException ex)
        {
            response.ErrorCode = ErrorCodes.GenericError;
            response.Message = ex.Message;
        }
        return response;
    }

    public async Task<EncryptResponse> RotateKey()
    {
        var response = new EncryptResponse();
        try
        {
            var apiResponse = await _client.PostAsync("/keyrotate/rotate-key", null);
            apiResponse.EnsureSuccessStatusCode();
            response.Message = "Rotation is successful";
        }
        catch (HttpRequestException ex)
        {
            response.ErrorCode = ErrorCodes.GenericError;
            response.Message = ex.Message;
        }
        return response;
    }

}
