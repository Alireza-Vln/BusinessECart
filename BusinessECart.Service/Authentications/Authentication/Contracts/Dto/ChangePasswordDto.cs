namespace BusinessECart.Service.Authentications.Authentication.Contracts.Dto;

public class ChangePasswordDto
{
    public string UserIdentifier  { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string NewPasswordAgain { get; set; }
    
}