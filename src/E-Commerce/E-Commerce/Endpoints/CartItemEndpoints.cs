using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Dtos;

namespace Api.Endpoints;

public static class CartItemEndpoints
{
    public static void MapCartItemEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/cart-items")
                       .WithTags("CartItemManagement")
                       .RequireAuthorization();

        group.MapPost("/", async ([FromBody] CartItemCreateDto dto, ICartItemService service) =>
        {
            var id = await service.AddCartItemAsync(dto);
            return Results.Created($"/api/cart-items/{id}", id);
        })
        .WithName("CreateCartItem");

        group.MapGet("/{id:long}", async (long id, ICartItemService service) =>
        {
            var item = await service.GetByIdAsync(id);
            return item is not null ? Results.Ok(item) : Results.NotFound();
        })
        .WithName("GetCartItemById");

        group.MapGet("/cart/{cartId:long}", async (long cartId, ICartItemService service) =>
        {
            var items = await service.GetByCartIdAsync(cartId);
            return Results.Ok(items);
        })
        .WithName("GetCartItemsByCartId");

        group.MapPut("/{id:long}", async (long id, [FromBody] CartItemUpdateDto dto, ICartItemService service) =>
        {
            await service.UpdateAsync(id, dto);
            return Results.NoContent();
        })
        .WithName("UpdateCartItem");

        group.MapDelete("/{id:long}", async (long id, ICartItemService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteCartItem");

        group.MapPatch("/{id:long}/increment", async (long id, [FromQuery] int amount, ICartItemService service) =>
        {
            await service.IncrementQuantityAsync(id, amount);
            return Results.NoContent();
        })
        .WithName("IncrementCartItemQuantity");

        group.MapPatch("/{id:long}/decrement", async (long id, [FromQuery] int amount, ICartItemService service) =>
        {
            await service.DecrementQuantityAsync(id, amount);
            return Results.NoContent();
        })
        .WithName("DecrementCartItemQuantity");

        group.MapDelete("/cart/{cartId:long}/clear", async (long cartId, ICartItemService service) =>
        {
            await service.ClearCartAsync(cartId);
            return Results.NoContent();
        })
        .WithName("ClearCartItems");

        group.MapGet("/cart/{cartId:long}/total", async (long cartId, ICartItemService service) =>
        {
            var total = await service.GetTotalPriceAsync(cartId);
            return Results.Ok(total);
        })
        .WithName("GetCartItemsTotalPrice");
    }
}
