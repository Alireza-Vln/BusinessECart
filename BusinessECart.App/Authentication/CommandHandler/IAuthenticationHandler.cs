using BusinessECart.Contracts.Interfaces;

namespace BusinessECart.App.Authentication.CommandHandler;

public interface IAuthenticationHandler : IScope
{
    Task Handle(AuthenticationCommand command);
}