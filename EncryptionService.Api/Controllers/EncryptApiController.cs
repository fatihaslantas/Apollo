using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection;
using EncryptionService.Api.Models;
namespace EncryptionService.Api.Controllers;

[ApiController]
[Route("encrypt")]
public class EncryptApiController : ControllerBase
{
    private readonly IDataProtector _protector;
    public EncryptApiController(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector("Apollo.v1");
    }

    [HttpPost]
    public async Task<IActionResult> Post(CryptoRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Text))
            throw new ArgumentNullException(nameof(request.Text), "Text string can not be null or empty");

        var response = new CryptoResponse();

        response.Data = _protector.Protect(request.Text);

        return Ok(response);
    }

}
