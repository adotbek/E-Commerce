using Application.Dtos;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class WishlistItemEndpoints
{
    public static void MapWishlistItemEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/wishlist-items")
                       .WithTags("WishlistManagement")
                       .RequireAuthorization();

        group.MapGet("/", async (IWishlistItemService service) =>
        {
            var items = await service.GetAllAsync();
            return Results.Ok(items);
        })
        .WithName("GetAllWishlistItems");

        group.MapGet("/{id:long}", async (long id, IWishlistItemService service) =>
        {
            var item = await service.GetByIdAsync(id);
            return item is not null ? Results.Ok(item) : Results.NotFound();
        })
        .WithName("GetWishlistItemById");

        group.MapPost("/", async ([FromBody] WishlistItemGetDto dto, IWishlistItemService service) =>
        {
            var id = await service.AddWishlistItemAsync(dto);
            return Results.Created($"/api/wishlist-items/{id}", id);
        })
        .WithName("CreateWishlistItem");

        group.MapPut("/{id:long}", async (long id, [FromBody] WishlistItemGetDto dto, IWishlistItemService service) =>
        {
            await service.UpdateAsync(id, dto);
            return Results.NoContent();
        })
        .WithName("UpdateWishlistItem");

        group.MapDelete("/{id:long}", async (long id, IWishlistItemService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteWishlistItem");

        group.MapGet("/user/{userId:long}", async (long userId, IWishlistItemService service) =>
        {
            var items = await service.GetByUserIdAsync(userId);
            return Results.Ok(items);
        })
        .WithName("GetWishlistItemsByUserId");

        group.MapGet("/wishlist/{wishlistId:long}", async (long wishlistId, IWishlistItemService service) =>
        {
            var items = await service.GetByWishlistIdAsync(wishlistId);
            return Results.Ok(items);
        })
        .WithName("GetWishlistItemsByWishlistId");

        group.MapGet("/exists", async ([FromQuery] long wishlistId, [FromQuery] long productId, IWishlistItemService service) =>
        {
            var exists = await service.ExistsAsync(wishlistId, productId);
            return Results.Ok(exists);
        })
        .WithName("CheckWishlistItemExists");

        group.MapGet("/count/{wishlistId:long}", async (long wishlistId, IWishlistItemService service) =>
        {
            var count = await service.GetCountByWishlistIdAsync(wishlistId);
            return Results.Ok(count);
        })
        .WithName("GetWishlistItemCount");

        group.MapDelete("/clear/{wishlistId:long}", async (long wishlistId, IWishlistItemService service) =>
        {
            await service.ClearWishlistAsync(wishlistId);
            return Results.NoContent();
        })
        .WithName("ClearWishlist");
    }
}
