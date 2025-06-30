using BusinessECart.Contracts.Interfaces;

namespace BusinessECart.Service.Authentications.Otp;

public interface IOtpSender :IScope
{
    Task SendOtpAsync(string phoneNumber, string otpCode);
}
