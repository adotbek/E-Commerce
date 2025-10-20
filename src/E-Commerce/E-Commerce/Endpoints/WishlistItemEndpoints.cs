using Application.Dtos;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class WishlistEndpoints
{
    public static void MapWishlistEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/wishlists")
                       .WithTags("WishlistManagement")
                       .RequireAuthorization();

        group.MapGet("/", async (IWishlistService service) =>
        {
            var wishlists = await service.GetAllAsync();
            return Results.Ok(wishlists);
        })
        .WithName("GetAllWishlists");

        group.MapGet("/{id:long}", async (long id, IWishlistService service) =>
        {
            var wishlist = await service.GetByIdAsync(id);
            return wishlist is not null ? Results.Ok(wishlist) : Results.NotFound();
        })
        .WithName("GetWishlistById");

        group.MapPost("/", async ([FromBody] WishlistCreateDto dto, IWishlistService service) =>
        {
            var id = await service.AddWishlistAsync(dto);
            return Results.Created($"/api/wishlists/{id}", id);
        })
        .WithName("CreateWishlist");

        group.MapPut("/{id:long}", async (long id, [FromBody] WishlistCreateDto dto, IWishlistService service) =>
        {
            await service.UpdateAsync(dto, id);
            return Results.NoContent();
        })
        .WithName("UpdateWishlist");

        group.MapDelete("/{id:long}", async (long id, IWishlistService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteWishlist");

        group.MapGet("/user/{userId:long}", async (long userId, IWishlistService service) =>
        {
            var wishlist = await service.GetByUserIdAsync(userId);
            return wishlist is not null ? Results.Ok(wishlist) : Results.NotFound();
        })
        .WithName("GetWishlistByUserId");

        group.MapGet("/exists/user/{userId:long}", async (long userId, IWishlistService service) =>
        {
            var exists = await service.ExistsByUserIdAsync(userId);
            return Results.Ok(exists);
        })
        .WithName("CheckWishlistExistsByUserId");

        group.MapGet("/{wishlistId:long}/item-count", async (long wishlistId, IWishlistService service) =>
        {
            var count = await service.GetItemCountAsync(wishlistId);
            return Results.Ok(count);
        })
        .WithName("GetWishlistItemCount");

        group.MapDelete("/{wishlistId:long}/clear", async (long wishlistId, IWishlistService service) =>
        {
            await service.ClearAsync(wishlistId);
            return Results.NoContent();
        })
        .WithName("ClearWishlist");
    }
}
