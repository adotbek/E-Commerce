using Application.Dtos;
using Application.DTOs.FlashSaleItems;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class FlashSaleItemEndpoints
{
    public static void MapFlashSaleItemEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/flashsale-items")
                       .WithTags("FlashSaleItemManagement")
                       .RequireAuthorization();

        group.MapGet("/", async (IFlashSaleItemService service) =>
        {
            var items = await service.GetAllAsync();
            return Results.Ok(items);
        })
        .WithName("GetAllFlashSaleItems");

        group.MapGet("/{id:long}", async (long id, IFlashSaleItemService service) =>
        {
            var item = await service.GetByIdAsync(id);
            return item is not null ? Results.Ok(item) : Results.NotFound();
        })
        .WithName("GetFlashSaleItemById");

        group.MapPost("/", async ([FromBody] FlashSaleItemCreateDto dto, IFlashSaleItemService service) =>
        {
            var id = await service.AddFlashSaleItemService(dto);
            return Results.Created($"/api/flashsale-items/{id}", id);
        })
        .WithName("CreateFlashSaleItem");

        group.MapPut("/{id:long}", async (long id, [FromBody] FlashSaleItemUpdateDto dto, IFlashSaleItemService service) =>
        {
            await service.UpdateAsync(dto, id);
            return Results.NoContent();
        })
        .WithName("UpdateFlashSaleItem");

        group.MapDelete("/{id:long}", async (long id, IFlashSaleItemService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteFlashSaleItem");

        group.MapGet("/flashsale/{flashSaleId:long}", async (long flashSaleId, IFlashSaleItemService service) =>
        {
            var items = await service.GetByFlashSaleIdAsync(flashSaleId);
            return Results.Ok(items);
        })
        .WithName("GetFlashSaleItemsByFlashSaleId");

        group.MapGet("/product/{productId:long}", async (long productId, IFlashSaleItemService service) =>
        {
            var item = await service.GetByProductIdAsync(productId);
            return item is not null ? Results.Ok(item) : Results.NotFound();
        })
        .WithName("GetFlashSaleItemByProductId");

        group.MapGet("/active", async ([FromQuery] DateTime? now, IFlashSaleItemService service) =>
        {
            var date = now ?? DateTime.UtcNow;
            var items = await service.GetActiveItemsAsync(date);
            return Results.Ok(items);
        })
        .WithName("GetActiveFlashSaleItems");

        group.MapGet("/calculate-price", async ([FromQuery] long productId, [FromQuery] long flashSaleId, IFlashSaleItemService service) =>
        {
            var price = await service.CalculateDiscountedPriceAsync(productId, flashSaleId);
            return Results.Ok(price);
        })
        .WithName("CalculateDiscountedPrice");

        group.MapGet("/exists", async ([FromQuery] long productId, [FromQuery] long flashSaleId, IFlashSaleItemService service) =>
        {
            var exists = await service.ExistsAsync(productId, flashSaleId);
            return Results.Ok(exists);
        })
        .WithName("CheckFlashSaleItemExists");

        group.MapDelete("/expired", async ([FromQuery] DateTime? now, IFlashSaleItemService service) =>
        {
            var date = now ?? DateTime.UtcNow;
            var removed = await service.RemoveExpiredItemsAsync(date);
            return Results.Ok(new { RemovedCount = removed });
        })
        .WithName("RemoveExpiredFlashSaleItems");
    }
}
