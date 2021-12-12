using Microsoft.AspNetCore.Mvc;
using EncryptionService.Api.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
namespace EncryptionService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class KeyRotateApiController : ControllerBase
{
    private readonly IKeyManager _keyManager;
    public KeyRotateApiController(IKeyManager keyManager)
    {
        _keyManager = keyManager;
    }

    [HttpPost]
    [Route("rotate-key")]
    public async Task<IActionResult> RotateKey()
    {
        _keyManager.RevokeAllKeys(DateTimeOffset.Now);

        _keyManager.CreateNewKey(DateTimeOffset.Now, DateTimeOffset.UtcNow.AddDays(90));

        return Ok();
    }

}
