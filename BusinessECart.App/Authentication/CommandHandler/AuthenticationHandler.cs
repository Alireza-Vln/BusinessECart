using BusinessECart.Contracts.Interfaces;
using BusinessECart.Service.Authentications.Authentication.Contracts;
using BusinessECart.Service.Authentications.Authentication.Contracts.Dto;

namespace BusinessECart.App.Authentication.CommandHandler;

public class AuthenticationHandler(
    IAuthenticationService authenticationService,
    IUnitOfWork unitOfWork)
    : IAuthenticationHandler
{
    public async Task Handle(AuthenticationCommand command)
    {
        await unitOfWork.Begin();
        try
        {
            var userRegister = await authenticationService.Register(new RegisterRequestDto
            {
                UserName = command.UserName,
                PhoneNumber = command.PhoneNumber,
                Email = command.Email,
                Password = command.Password,
                PasswordAgain = command.PasswordAgain
            });

            await authenticationService.AddToRole(userRegister, command.Role);

            await unitOfWork.Commit();
        }
        catch
        {
            await unitOfWork.Rollback();
            throw;
        }
    }
}