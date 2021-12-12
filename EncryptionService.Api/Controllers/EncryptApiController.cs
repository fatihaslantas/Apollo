using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection;
using EncryptionService.Api.Models;
namespace EncryptionService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EncryptApiController : ControllerBase
{
    private readonly IDataProtector _protector;
    public EncryptApiController(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector("Apollo.v1");
    }

    [HttpPost]
    public string Post(EncryptRequest request)
    {
        return _protector.Protect(request.Text);
    }

}
