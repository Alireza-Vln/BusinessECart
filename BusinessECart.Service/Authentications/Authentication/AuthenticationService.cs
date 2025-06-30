using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessECart.Service.Authentications.Authentication.Contracts;
using BusinessECart.Service.Authentications.Authentication.Contracts.Dto;
using BusinessECart.Service.Authentications.Authentication.Contracts.Exceptions;
using BusinessECart.Service.Authentications.Otp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessECart.Service.Authentications.Authentication;

public class AuthenticationService(
    UserManager<IdentityUser> userManager,
    IMemoryCache cache,
    IOtpService otpService,
    IConfiguration config)
    : IAuthenticationService
{
    public async Task<IdentityUser> Register(RegisterRequestDto dto)
    {
        var existingPhoneUser = await userManager.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);


        if (existingPhoneUser != null)
        {
            throw new DuplicatePhoneNumberException();
        }

        if (dto.Password != dto.PasswordAgain)
        {
            throw new PasswordsAreNotTheSameException();
        }

        var existingUserName = await userManager.Users
            .FirstOrDefaultAsync(u => u.UserName == dto.UserName);
        if (existingUserName != null)
        {
            throw new DuplicateUserNameException();
        }

        var user = new IdentityUser
        {
            UserName = dto.UserName,
            PhoneNumber = dto.PhoneNumber
        };

        await userManager.CreateAsync(user, dto.Password);

        return user;
    }

    public async Task<string?> LoginAsync(LoginRequestDto dto)
    {
        var user = await userManager.Users
            .FirstOrDefaultAsync(u => u.Email == dto.UserIdentifier
                                      || u.PhoneNumber == dto.UserIdentifier);

        if (user == null || !await userManager.CheckPasswordAsync(user, dto.Password))
            return null;

        return await ReturnToken(user);
    }


    public async Task AddToRole(IdentityUser userRegister, string roleName)
    {
        await userManager.AddToRoleAsync(userRegister, roleName);
    }

    public async Task<string?> ChangePassword(ChangePasswordDto dto)
    {
        var user = await userManager.Users
            .FirstOrDefaultAsync(u => u.Email == dto.UserIdentifier
                                      || u.PhoneNumber == dto.UserIdentifier);
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        if (dto.NewPassword != dto.NewPasswordAgain)
        {
            throw new PasswordsAreNotTheSameException();
        }

        var result = await userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                if (error != null)
                {
                    throw new CurrentPasswordIsIncorrectException();
                }
            }
        }

        return await ReturnToken(user);
    }


    private async Task<string?> ReturnToken(IdentityUser user)
    {
        var roles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(config["Jwt:ExpiresInMinutes"]!)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // public async Task ForgetPassword(ForgotPasswordRequestDto request)
    // {
    //     var user = await userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
    //     if (user == null)
    //         throw new Exception();
    //     var otp = new Random().Next(100000, 999999).ToString();
    //     cache.Set($"otp_{request.PhoneNumber}", otp, TimeSpan.FromMinutes(5));
    //     await otpService.SendOtpAsync(request.PhoneNumber);
    // }
}