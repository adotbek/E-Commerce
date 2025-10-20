using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Dtos;

namespace Api.Endpoints;

public static class OrderItemEndpoints
{
    public static void MapOrderItemEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/order-items")
                       .WithTags("OrderItemManagement")
                       .RequireAuthorization();

        group.MapGet("/", async (IOrderItemService service) =>
        {
            var items = await service.GetAllAsync();
            return Results.Ok(items);
        })
        .WithName("GetAllOrderItems");

        group.MapGet("/{id:long}", async (long id, IOrderItemService service) =>
        {
            var item = await service.GetByIdAsync(id);
            return item is not null ? Results.Ok(item) : Results.NotFound();
        })
        .WithName("GetOrderItemById");

        group.MapPost("/", async ([FromBody] OrderItemCreateDto dto, IOrderItemService service) =>
        {
            var id = await service.AddOrderAsync(dto);
            return Results.Created($"/api/order-items/{id}", id);
        })
        .WithName("CreateOrderItem");

        group.MapPut("/{id:long}", async (long id, [FromBody] OrderItemUpdateDto dto, IOrderItemService service) =>
        {
            await service.UpdateAsync(dto, id);
            return Results.NoContent();
        })
        .WithName("UpdateOrderItem");

        group.MapDelete("/{id:long}", async (long id, IOrderItemService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteOrderItem");

        group.MapGet("/order/{orderId:long}", async (long orderId, IOrderItemService service) =>
        {
            var items = await service.GetByOrderIdAsync(orderId);
            return Results.Ok(items);
        })
        .WithName("GetOrderItemsByOrderId");

        group.MapGet("/order/{orderId:long}/total", async (long orderId, IOrderItemService service) =>
        {
            var total = await service.CalculateTotalAsync(orderId);
            return Results.Ok(total);
        })
        .WithName("GetOrderItemsTotal");

        group.MapGet("/{orderItemId:long}/exists", async (long orderItemId, IOrderItemService service) =>
        {
            var exists = await service.ExistsAsync(orderItemId);
            return Results.Ok(exists);
        })
        .WithName("CheckOrderItemExists");

        group.MapGet("/order/{orderId:long}/quantity", async (long orderId, IOrderItemService service) =>
        {
            var quantity = await service.GetTotalQuantityAsync(orderId);
            return Results.Ok(quantity);
        })
        .WithName("GetOrderItemsTotalQuantity");

        group.MapPost("/order/{orderId:long}/product/{productId:long}", async (long orderId, long productId, [FromQuery] int quantity, IOrderItemService service) =>
        {
            await service.AddOrUpdateItemAsync(orderId, productId, quantity);
            return Results.Ok();
        })
        .WithName("AddOrUpdateOrderItem");

        group.MapDelete("/order/{orderId:long}/delete", async (long orderId, IOrderItemService service) =>
        {
            await service.DeleteByOrderIdAsync(orderId);
            return Results.NoContent();
        })
        .WithName("DeleteOrderItemsByOrderId");

        group.MapGet("/order/{orderId:long}/product/{productId:long}/exists", async (long orderId, long productId, IOrderItemService service) =>
        {
            var exists = await service.ExistsInOrderAsync(orderId, productId);
            return Results.Ok(exists);
        })
        .WithName("CheckProductExistsInOrder");
    }
}
