using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Dtos;

namespace Api.Endpoints;

public static class PaymentOptionEndpoints
{
    public static void MapPaymentOptionEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/payment-options")
                       .WithTags("PaymentOptionManagement")
                       .RequireAuthorization();

        group.MapGet("/{id:long}", async (long id, [FromServices] IPaymentOptionService service) =>
        {
            var option = await service.GetByIdAsync(id);
            return option is not null ? Results.Ok(option) : Results.NotFound();
        })
        .WithName("GetPaymentOptionById");

        group.MapGet("/user/{userId:long}", async (long userId, [FromServices] IPaymentOptionService service) =>
        {
            var options = await service.GetByUserIdAsync(userId);
            return Results.Ok(options);
        })
        .WithName("GetPaymentOptionsByUserId");

        group.MapGet("/user/{userId:long}/active", async (long userId, [FromServices] IPaymentOptionService service) =>
        {
            var options = await service.GetActiveByUserIdAsync(userId);
            return Results.Ok(options);
        })
        .WithName("GetActivePaymentOptionsByUserId");

        group.MapGet("/user/{userId:long}/default", async (long userId, [FromServices] IPaymentOptionService service) =>
        {
            var option = await service.GetDefaultByUserIdAsync(userId);
            return option is not null ? Results.Ok(option) : Results.NotFound();
        })
        .WithName("GetDefaultPaymentOptionByUserId");

        group.MapPut("/user/{userId:long}/set-default/{paymentOptionId:long}", async (long userId, long paymentOptionId, [FromServices] IPaymentOptionService service) =>
        {
            await service.SetDefaultAsync(userId, paymentOptionId);
            return Results.NoContent();
        })
        .WithName("SetDefaultPaymentOption");

        group.MapGet("/{paymentOptionId:long}/belongs-to/{userId:long}", async (long paymentOptionId, long userId, [FromServices] IPaymentOptionService service) =>
        {
            var result = await service.BelongsToUserAsync(paymentOptionId, userId);
            return Results.Ok(result);
        })
        .WithName("CheckPaymentOptionOwnership");

        group.MapGet("/exists", async ([FromQuery] string cardNumber, [FromQuery] long userId, [FromServices] IPaymentOptionService service) =>
        {
            var exists = await service.ExistsByCardNumberAsync(cardNumber, userId);
            return Results.Ok(exists);
        })
        .WithName("ExistsPaymentOptionByCardNumber");

        group.MapGet("/{id:long}/expired", async (long id, [FromServices] IPaymentOptionService service) =>
        {
            var expired = await service.IsExpiredAsync(id);
            return Results.Ok(expired);
        })
        .WithName("IsPaymentOptionExpired");

        group.MapGet("/{id:long}/masked-number", async (long id, [FromServices] IPaymentOptionService service) =>
        {
            var masked = await service.GetMaskedCardNumberAsync(id);
            return masked is not null ? Results.Ok(masked) : Results.NotFound();
        })
        .WithName("GetMaskedCardNumber");

        group.MapPut("/{id:long}/toggle-active", async (long id, [FromQuery] bool isActive, [FromServices] IPaymentOptionService service) =>
        {
            await service.ToggleActiveAsync(id, isActive);
            return Results.NoContent();
        })
        .WithName("TogglePaymentOptionActive");

        group.MapPost("/{id:long}/generate-token", async (long id, [FromServices] IPaymentOptionService service) =>
        {
            var token = await service.GeneratePaymentTokenAsync(id);
            return Results.Ok(token);
        })
        .WithName("GeneratePaymentToken");
    }
}
