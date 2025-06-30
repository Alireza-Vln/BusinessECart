using BusinessECart.Contracts.Interfaces;
using BusinessECart.Service.Authentications.Authentication.Contracts.Dto;
using Microsoft.AspNetCore.Identity;

namespace BusinessECart.Service.Authentications.Authentication.Contracts;

public interface IAuthenticationService : IScope
{
    Task<IdentityUser> Register(RegisterRequestDto dto);
    Task<string?> LoginAsync(LoginRequestDto dto);
   // Task ForgetPassword(ForgotPasswordRequestDto request);
    Task AddToRole(IdentityUser userRegister, string roleName);
    Task<string?> ChangePassword(ChangePasswordDto dto);
}