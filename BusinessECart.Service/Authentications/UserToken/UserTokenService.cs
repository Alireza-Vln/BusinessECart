using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BusinessECart.Service.Authentications.UserToken;

public class UserTokenService : IUserTokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserTokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public List<string> GetUserRoles()
    {
        return _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList() 
               ?? new List<string>();
    }
}