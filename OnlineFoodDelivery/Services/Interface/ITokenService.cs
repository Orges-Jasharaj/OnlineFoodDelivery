using OnlineFoodDelivery.Data.Models;
using OnlineFoodDelivery.Dtos.Responses;
using System.Security.Claims;

namespace OnlineFoodDelivery.Services.Interface
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, List<string> roles);
        RefreshTokenDto GenerateRrefreshToken();

        ClaimsPrincipal GetClaimsPrincipal(string token);
    }
}
