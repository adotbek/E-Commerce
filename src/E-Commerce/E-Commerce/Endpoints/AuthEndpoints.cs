using Application.Dtos;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class AuthEndpoints
{
    public record SendCodeRequest(string Email);

    public static void MapAuthEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/auth")
            .AllowAnonymous()
            .WithTags("AuthenticationManagement");


        userGroup.MapPost("/send-code",
        async ([FromBody] SendCodeRequest request, [FromServices] IAuthService _service) =>
        {
            if (string.IsNullOrEmpty(request.Email))
                return Results.BadRequest("Email is required");

            await _service.EmailCodeSender(request.Email);
            return Results.Ok(new { success = true, data = "Confirmation code sent" });
        })
        .WithName("SendCode");

        userGroup.MapPost("/confirm-code",
        async ([FromBody] ConfirmCodeRequestDto request, [FromServices] IAuthService _service) =>
        {
            var res = await _service.ConfirmCode(request.Code, request.Email);
            return Results.Ok(res);
        })
        .WithName("ConfirmCode");

        userGroup.MapPost("/register",
        async ([FromBody] UserCreateDto user, [FromServices] IAuthService _service) =>
        {
            return Results.Ok(await _service.SignUpUserAsync(user));
        })
        .WithName("SignUp");

        userGroup.MapPost("/login",
        async ([FromBody] UserLoginDto user, [FromServices] IAuthService _service) =>
        {
            var result = await _service.LoginUserAsync(user);
            return Results.Ok(result);
        })
        .WithName("Login");

        userGroup.MapPost("/google-register",
        async ([FromBody] GoogleAuthDto dto, [FromServices] IAuthService _service) =>
        {
            var userId = await _service.GoogleRegisterAsync(dto);
            return Results.Ok(new { UserId = userId });
        })
        .WithName("GoogleRegister");

        userGroup.MapPost("/google-login",
        async ([FromBody] GoogleAuthDto dto, [FromServices] IAuthService _service) =>
        {
            var response = await _service.GoogleLoginAsync(dto);
            return Results.Ok(response);
        })
        .WithName("GoogleLogin");

        userGroup.MapPut("/refresh-token",
        async ([FromBody] RefreshRequestDto refresh, [FromServices] IAuthService _service) =>
        {
            return Results.Ok(await _service.RefreshTokenAsync(refresh));
        })
        .WithName("RefreshToken");

        userGroup.MapDelete("/log-out",
        async ([FromQuery] string refreshToken, [FromServices] IAuthService _service) =>
        {
            await _service.LogOut(refreshToken);
            return Results.Ok();
        })
        .WithName("LogOut");

        userGroup.MapPost("/forgot-password",
        async ([FromQuery] string email, [FromQuery] string newPassword, [FromQuery] string confirmCode, [FromServices] IAuthService _service) =>
        {
            await _service.ForgotPassword(email, newPassword, confirmCode);
            return Results.Ok();
        })
        .WithName("ForgotPassword");
    }
}
