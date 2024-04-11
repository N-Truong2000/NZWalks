using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Services
{
    public interface ITokenService
    {
      string  CraeteJwtToken(IdentityUser user, List<string> roles);
    }
}
