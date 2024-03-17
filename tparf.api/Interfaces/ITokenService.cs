using System.Security.Claims;
using tparf.dto.Auth;

namespace tparf.api.Interfaces
{
    public interface ITokenService
    {
        TokenResponse GetToken(IEnumerable<Claim> claim);
        string GetRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
