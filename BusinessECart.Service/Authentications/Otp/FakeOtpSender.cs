namespace BusinessECart.Service.Authentications.Otp;


public class FakeOtpSender : IOtpSender
{
    public Task SendOtpAsync(string phoneNumber, string otpCode)
    {
        Console.WriteLine($"Send OTP {otpCode} to {phoneNumber}");
        return Task.CompletedTask;
    }
}