using Microsoft.AspNetCore.Mvc;
using Apollo.Api.Interfaces;
namespace Apollo.Api.Controllers;

[ApiController]
[Route("api/keyrotate")]
public class KeyRotateApiController : ControllerBase
{
    private readonly IEncryptionService _encryptionService;
    public KeyRotateApiController(IEncryptionService encryptionService)
    {
        _encryptionService = encryptionService;
    }

    [HttpPost]
    [Route("rotate-key")]
    public async Task<IActionResult> RotateKey()
    {
        var response = await _encryptionService.RotateKey();
        return Ok(response);
    }

}
