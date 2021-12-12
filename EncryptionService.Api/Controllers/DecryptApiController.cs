using Microsoft.AspNetCore.Mvc;
using EncryptionService.Api.Models;
using Microsoft.AspNetCore.DataProtection;

namespace EncryptionService.Api.Controllers;

[ApiController]
[Route("decrypt")]
public class DecryptApiController : ControllerBase
{
    private readonly IDataProtector _protector;
    public DecryptApiController(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector("Apollo.v1");
    }

    [HttpPost]
    public async Task<IActionResult> Post(CryptoRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Text))
            throw new ArgumentNullException(nameof(request.Text), "Text string can not be null or empty");

        var response = new CryptoResponse();

        response.Data = _protector.Unprotect(request.Text);
        return Ok(response);
    }

}
