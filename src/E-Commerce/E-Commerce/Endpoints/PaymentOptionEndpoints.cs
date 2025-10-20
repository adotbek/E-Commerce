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

        group.MapGet("/", async (IPaymentOptionService service) =>
        {
            var options = await service.GetAllAsync();
            return Results.Ok(options);
        })
        .WithName("GetAllPaymentOptions");

        group.MapGet("/{id:long}", async (long id, IPaymentOptionService service) =>
        {
            var option = await service.GetByIdAsync(id);
            return option is not null ? Results.Ok(option) : Results.NotFound();
        })
        .WithName("GetPaymentOptionById");

        group.MapPost("/", async ([FromBody] PaymentOptionCreateDto dto, IPaymentOptionService service) =>
        {
            var id = await service.AddPaymentOptionAsync(dto);
            return Results.Created($"/api/payment-options/{id}", id);
        })
        .WithName("CreatePaymentOption");

        group.MapPut("/{id:long}", async (long id, [FromBody] PaymentOptionUpdateDto dto, IPaymentOptionService service) =>
        {
            await service.UpdateAsync(dto, id);
            return Results.NoContent();
        })
        .WithName("UpdatePaymentOption");

        group.MapDelete("/{id:long}", async (long id, IPaymentOptionService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeletePaymentOption");

        group.MapGet("/user/{userId:long}", async (long userId, IPaymentOptionService service) =>
        {
            var options = await service.GetByUserIdAsync(userId);
            return Results.Ok(options);
        })
        .WithName("GetPaymentOptionsByUserId");

        group.MapGet("/user/{userId:long}/active", async (long userId, IPaymentOptionService service) =>
        {
            var options = await service.GetActiveByUserIdAsync(userId);
            return Results.Ok(options);
        })
        .WithName("GetActivePaymentOptionsByUserId");

        group.MapGet("/user/{userId:long}/default", async (long userId, IPaymentOptionService service) =>
        {
            var option = await service.GetDefaultByUserIdAsync(userId);
            return option is not null ? Results.Ok(option) : Results.NotFound();
        })
        .WithName("GetDefaultPaymentOptionByUserId");

        group.MapPut("/user/{userId:long}/set-default/{paymentOptionId:long}", async (long userId, long paymentOptionId, IPaymentOptionService service) =>
        {
            await service.SetDefaultAsync(userId, paymentOptionId);
            return Results.NoContent();
        })
        .WithName("SetDefaultPaymentOption");

        group.MapGet("/{paymentOptionId:long}/belongs-to/{userId:long}", async (long paymentOptionId, long userId, IPaymentOptionService service) =>
        {
            var result = await service.BelongsToUserAsync(paymentOptionId, userId);
            return Results.Ok(result);
        })
        .WithName("CheckPaymentOptionOwnership");

        group.MapGet("/exists", async ([FromQuery] string cardNumber, [FromQuery] long userId, IPaymentOptionService service) =>
        {
            var exists = await service.ExistsByCardNumberAsync(cardNumber, userId);
            return Results.Ok(exists);
        })
        .WithName("ExistsPaymentOptionByCardNumber");

        group.MapGet("/{id:long}/expired", async (long id, IPaymentOptionService service) =>
        {
            var expired = await service.IsExpiredAsync(id);
            return Results.Ok(expired);
        })
        .WithName("IsPaymentOptionExpired");

        group.MapGet("/{id:long}/masked-number", async (long id, IPaymentOptionService service) =>
        {
            var masked = await service.GetMaskedCardNumberAsync(id);
            return masked is not null ? Results.Ok(masked) : Results.NotFound();
        })
        .WithName("GetMaskedCardNumber");

        group.MapPut("/{id:long}/toggle-active", async (long id, [FromQuery] bool isActive, IPaymentOptionService service) =>
        {
            await service.ToggleActiveAsync(id, isActive);
            return Results.NoContent();
        })
        .WithName("TogglePaymentOptionActive");

        group.MapPost("/{id:long}/generate-token", async (long id, IPaymentOptionService service) =>
        {
            var token = await service.GeneratePaymentTokenAsync(id);
            return Results.Ok(token);
        })
        .WithName("GeneratePaymentToken");
    }
}
