namespace BusinessECart.Service.Authentications.Authentication.Contracts.Dto;

public class RegisterRequestDto
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string PasswordAgain { get; set; }
    public required string PhoneNumber { get; set; } 
    public string? Email { get; set; }



}