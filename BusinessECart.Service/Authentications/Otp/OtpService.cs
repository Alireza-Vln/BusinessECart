using Microsoft.Extensions.Caching.Memory;

namespace BusinessECart.Service.Authentications.Otp;

public class OtpService(IMemoryCache cache, IOtpSender otpSender) : IOtpService
{
    public async Task SendOtpAsync(string phoneNumber)
    {
        var otp = new Random().Next(100000, 999999).ToString();
        cache.Set($"otp_{phoneNumber}", otp, TimeSpan.FromMinutes(5));
        await otpSender.SendOtpAsync(phoneNumber, otp);
    }

    public bool VerifyOtp(string phoneNumber, string otp)
    {
        return cache.TryGetValue($"otp_{phoneNumber}", out string cachedOtp) && cachedOtp == otp;
    }
}