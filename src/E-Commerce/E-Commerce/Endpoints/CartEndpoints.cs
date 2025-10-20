using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Dtos;

namespace Api.Endpoints;

public static class CartEndpoints
{
    public static void MapCartEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/carts")
                       .WithTags("CartManagement")
                       .RequireAuthorization();

        group.MapPost("/", async ([FromBody] CartCreateDto dto, ICartService service) =>
        {
            var id = await service.AddCartAsync(dto);
            return Results.Created($"/api/carts/{id}", id);
        })
        .WithName("CreateCart");

        group.MapGet("/{id:long}", async (long id, ICartService service) =>
        {
            var cart = await service.GetByIdAsync(id);
            return cart is not null ? Results.Ok(cart) : Results.NotFound();
        })
        .WithName("GetCartById");

        group.MapGet("/user/{userId:long}", async (long userId, ICartService service) =>
        {
            var cart = await service.GetByUserIdAsync(userId);
            return cart is not null ? Results.Ok(cart) : Results.NotFound();
        })
        .WithName("GetCartByUserId");

        group.MapPut("/user/{userId:long}", async (long userId, [FromBody] CartUpdateDto dto, ICartService service) =>
        {
            await service.UpdateAsync(userId, dto);
            return Results.NoContent();
        })
        .WithName("UpdateCartByUserId");

        group.MapDelete("/{id:long}", async (long id, ICartService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteCart");

        group.MapGet("/user/{userId:long}/exists", async (long userId, ICartService service) =>
        {
            var exists = await service.ExistsByUserIdAsync(userId);
            return Results.Ok(exists);
        })
        .WithName("CheckCartExistsByUserId");

        group.MapGet("/{cartId:long}/total", async (long cartId, ICartService service) =>
        {
            var total = await service.CalculateTotalPriceAsync(cartId);
            return Results.Ok(total);
        })
        .WithName("CalculateCartTotalPrice");

        group.MapDelete("/{cartId:long}/clear", async (long cartId, ICartService service) =>
        {
            await service.ClearCartAsync(cartId);
            return Results.NoContent();
        })
        .WithName("ClearCart");
    }
}
