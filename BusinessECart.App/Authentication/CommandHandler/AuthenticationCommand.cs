namespace BusinessECart.App.Authentication.CommandHandler;

public class AuthenticationCommand
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string PasswordAgain { get; set; }

    public required string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public required string Role { get; set; }
}