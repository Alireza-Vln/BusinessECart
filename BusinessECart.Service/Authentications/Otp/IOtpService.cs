using BusinessECart.Contracts.Interfaces;

namespace BusinessECart.Service.Authentications.Otp;

public interface IOtpService : IScope
{
    Task SendOtpAsync(string phoneNumber);
}