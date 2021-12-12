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
        return Ok();
    }

}
