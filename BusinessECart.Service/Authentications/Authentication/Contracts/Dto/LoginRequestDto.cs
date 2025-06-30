namespace BusinessECart.Service.Authentications.Authentication.Contracts.Dto;

public class LoginRequestDto
{
    public string UserIdentifier  { get; set; }
    public string Password { get; set; }
}