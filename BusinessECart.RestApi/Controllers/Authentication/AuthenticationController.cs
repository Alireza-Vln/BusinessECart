using BusinessECart.App.Authentication.CommandHandler;
using BusinessECart.Service.Authentications.Authentication.Contracts;
using BusinessECart.Service.Authentications.Authentication.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BusinessECart.RestApi.Controllers.Authentication;

[ApiController]
[Route("api/v1/authentications")]
public class AuthenticationController : ControllerBase
{
    [HttpPost("register")]
    public async Task Register(
        [FromBody] AuthenticationCommand command,
        [FromServices] IAuthenticationHandler handler)
    {
        await handler.Handle(command);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto request,
        [FromServices] IAuthenticationService authenticationService)
    {
        var token = await authenticationService.LoginAsync(request);
        if (token == null)
            return Unauthorized();

        return Ok(new AuthResponseDto { Token = token });
    }

    [HttpGet]
    public IActionResult GetSecret()
    {
        return Ok("You are authorized!");
    }

    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangePassword(
        [FromBody] ChangePasswordDto dto,
        [FromServices] IAuthenticationService authenticationService)
    {
        var token = await authenticationService.ChangePassword(dto);
        if (token == null)
            return Unauthorized();

        return Ok(new AuthResponseDto { Token = token });
    }
    // [HttpPost("forgot-password")]
    // public async Task<IActionResult> ForgotPassword(
    //     [FromBody] ForgotPasswordRequestDto request,
    //     [FromServices]IAuthenticationService authenticationService)
    //
    // {
    //     await authenticationService.ForgetPassword(request);
    //     return Ok("کد تأیید ارسال شد");
    // }
}