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

        group.MapPost("/", async ([FromServices] ICartService service,HttpContext context) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value ?? throw new Exception();
            var id = await service.AddCartAsync(new CartCreateDto() { UserId = long.Parse(userId) });
            return Results.Created($"/api/carts/{id}", id);
        })
        .WithName("CreateCart");

        group.MapGet("/{id:long}", async (long id, [FromServices] ICartService service) =>
        {
            var cart = await service.GetByIdAsync(id);
            return cart is not null ? Results.Ok(cart) : Results.NotFound();
        })
        .WithName("GetCartById");

        group.MapGet("/user/{userId:long}", async ([FromServices] ICartService service,HttpContext context) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value ?? throw new Exception();
            var cart = await service.GetByUserIdAsync(long.Parse(userId));
            return cart is not null ? Results.Ok(cart) : Results.NotFound();
        })
        .WithName("GetCartByUserId");

        group.MapPut("/user/{userId:long}", async ([FromBody] CartUpdateDto dto, [FromServices] ICartService service,HttpContext context) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value ?? throw new Exception();
            await service.UpdateAsync(long.Parse(userId), dto);
            return Results.NoContent();
        })
        .WithName("UpdateCartByUserId");

        group.MapDelete("/{id:long}", async (long id, [FromServices] ICartService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteCart");

        group.MapGet("/user/{userId:long}/exists", async ( [FromServices] ICartService service,HttpContext context) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value ?? throw new Exception();
            var exists = await service.ExistsByUserIdAsync(long.Parse(userId));
            return Results.Ok(exists);
        })
        .WithName("CheckCartExistsByUserId");

        group.MapGet("/{cartId:long}/total", async (long cartId, [FromServices] ICartService service) =>
        {
            var total = await service.CalculateTotalPriceAsync(cartId);
            return Results.Ok(total);
        })
        .WithName("CalculateCartTotalPrice");

        group.MapDelete("/{cartId:long}/clear", async (long cartId, [FromServices] ICartService service) =>
        {
            await service.ClearCartAsync(cartId);
            return Results.NoContent();
        })
        .WithName("ClearCart");
    }
}
