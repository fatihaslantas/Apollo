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
    public string Post(CryptoRequest request)
    {
        return _protector.Unprotect(request.Text);
    }

}
