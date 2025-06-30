using BusinessECart.Contracts.Interfaces;

namespace BusinessECart.Service.Authentications.UserToken;

public interface IUserTokenService : IScope
{
    string? GetUserId();
    List<string> GetUserRoles();
}