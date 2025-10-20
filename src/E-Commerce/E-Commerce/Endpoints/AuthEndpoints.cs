using Application.Dtos;
using Application.Interfaces;
using Application.Services;

namespace E_Commerce.Endpoints;

public static class AuthEndpoints
{

    public record SendCodeRequest(string Email);

    public static void MapAuthEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/auth")
            .AllowAnonymous()
            .WithTags("AuthenticationManagement");


        userGroup.MapPost("/send-code",
        async (SendCodeRequest request, IAuthService _service) =>
        {
            if (string.IsNullOrEmpty(request.Email))
                return Results.BadRequest("Email is required");

            await _service.EailCodeSender(request.Email);
            return Results.Ok(new { success = true, data = "Confirmation code sent" });
        })
        .WithName("SendCode");

        userGroup.MapPost("/confirm-code",
        async (ConfirmCodeRequestDto request, IAuthService _service) =>
        {
            var res = await _service.ConfirmCode(request.Code, request.Email);
            return Results.Ok(res);
        })
        .WithName("ConfirmCode");

        userGroup.MapPost("/register",
        async (UserCreateDto user, IAuthService _service) =>
        {
            return Results.Ok(await _service.SignUpUserAsync(user));
        })
        .AllowAnonymous()
        .WithName("SignUp");

        userGroup.MapPost("/login",
        async (UserLoginDto user, IAuthService _service) =>
        {
            var result = await _service.LoginUserAsync(user);
            return Results.Ok(result);
        })
        .WithName("Login");

        userGroup.MapPost("/google-register",
        async (GoogleAuthDto dto, IAuthService _service) =>
        {
            var userId = await _service.GoogleRegisterAsync(dto);
            return Results.Ok(new { UserId = userId });
        })
        .WithName("GoogleRegister");

        userGroup.MapPost("/google-login",
        async (GoogleAuthDto dto, IAuthService _service) =>
        {
            var response = await _service.GoogleLoginAsync(dto);
            return Results.Ok(response);
        })
        .WithName("GoogleLogin");

        userGroup.MapPut("/refresh-token",
        async (RefreshRequestDto refresh, IAuthService _service) =>
        {
            return Results.Ok(await _service.RefreshTokenAsync(refresh));
        })
        .WithName("RefreshToken");

        userGroup.MapDelete("/log-out",
        async (string refreshToken, IAuthService _service) =>
        {
            await _service.LogOut(refreshToken);
            return Results.Ok();
        })
        .WithName("LogOut");

        userGroup.MapPost("/forgot-password",
        async (string email, string newPassword, string confirmCode, IAuthService _service) =>
        {
            await _service.ForgotPassword(email, newPassword, confirmCode);
        })
        .WithName("ForgotPassword");

    }
}
