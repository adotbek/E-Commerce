using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Dtos;

namespace Api.Endpoints;

public static class FlashSaleEndpoints
{
    public static void MapFlashSaleEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/flashsales")
                       .WithTags("FlashSaleManagement")
                       .RequireAuthorization();

        group.MapGet("/", async (IFlashSaleService service) =>
        {
            var sales = await service.GetAllAsync();
            return Results.Ok(sales);
        })
        .WithName("GetAllFlashSales");

        group.MapGet("/{id:long}", async (long id, IFlashSaleService service) =>
        {
            var sale = await service.GetByIdAsync(id);
            return sale is not null ? Results.Ok(sale) : Results.NotFound();
        })
        .WithName("GetFlashSaleById");

        group.MapPost("/", async ([FromBody] FlashSaleCreateDto dto, IFlashSaleService service) =>
        {
            var id = await service.AddFlashSaleAsync(dto);
            return Results.Created($"/api/flashsales/{id}", id);
        })
        .WithName("CreateFlashSale");

        group.MapPut("/{id:long}", async (long id, [FromBody] FlashSaleUpdateDto dto, IFlashSaleService service) =>
        {
            await service.UpdateAsync(id, dto);
            return Results.NoContent();
        })
        .WithName("UpdateFlashSale");

        group.MapDelete("/{id:long}", async (long id, IFlashSaleService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteFlashSale");

        group.MapGet("/active", async ([FromQuery] DateTime? at, IFlashSaleService service) =>
        {
            var active = await service.GetActiveAsync(at);
            return Results.Ok(active);
        })
        .WithName("GetActiveFlashSales");

        group.MapGet("/active/product/{productId:long}", async (long productId, IFlashSaleService service) =>
        {
            var sale = await service.GetActiveByProductIdAsync(productId);
            return sale is not null ? Results.Ok(sale) : Results.NotFound();
        })
        .WithName("GetActiveFlashSaleByProductId");

        group.MapGet("/{flashSaleId:long}/is-active", async (long flashSaleId, [FromQuery] DateTime? now, IFlashSaleService service) =>
        {
            var isActive = await service.IsActiveAsync(flashSaleId, now);
            return Results.Ok(isActive);
        })
        .WithName("IsFlashSaleActive");

        group.MapDelete("/expired", async ([FromQuery] DateTime? now, IFlashSaleService service) =>
        {
            var count = await service.RemoveExpiredAsync(now);
            return Results.Ok(new { RemovedCount = count });
        })
        .WithName("RemoveExpiredFlashSales");
    }
}
