using Microsoft.AspNetCore.Mvc;
using Apollo.Api.Interfaces;
using Apollo.Api.Models;
namespace Apollo.Api.Controllers;

[ApiController]
[Route("api/decrypt")]
public class DecryptApiController : ControllerBase
{
    private readonly IEncryptionService _encryptionService;
    public DecryptApiController(IEncryptionService encryptionService)
    {
        _encryptionService = encryptionService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CryptoRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Text))
            return BadRequest(new EncryptResponse() { ErrorCode = ErrorCodes.ArgumentNull, Message = ErrorCodes.ArgumentNull });

        var response = await _encryptionService.Decrypt(request.Text);
        
        return Ok(response);
    }

}
