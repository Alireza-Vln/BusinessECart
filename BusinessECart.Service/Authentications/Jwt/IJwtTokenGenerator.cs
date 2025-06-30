using BusinessECart.Contracts.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BusinessECart.Service.Authentications.Jwt;

public interface IJwtTokenGenerator :IScope
{
     string GenerateToken(IdentityUser user);

}