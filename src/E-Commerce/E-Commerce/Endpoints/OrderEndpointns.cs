using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Dtos;
using Domain.Enums;

namespace Api.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/orders")
                       .WithTags("OrderManagement")
                       .RequireAuthorization();

        group.MapGet("/{id:long}", async (long id, [FromServices] IOrderService service) =>
        {
            var order = await service.GetByIdAsync(id);
            return order is not null ? Results.Ok(order) : Results.NotFound();
        })
        .WithName("GetOrderById");

        group.MapPost("/", async ([FromBody] OrderCreateDto dto, [FromServices] IOrderService service) =>
        {
            var id = await service.AddOrderAsync(dto);
            return Results.Created($"/api/orders/{id}", id);
        })
        .WithName("CreateOrder");

        group.MapDelete("/{id:long}", async (long id, [FromServices] IOrderService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteOrder");

        group.MapGet("/user/{userId:long}", async (long userId, [FromServices] IOrderService service) =>
        {
            var orders = await service.GetByUserIdAsync(userId);
            return Results.Ok(orders);
        })
        .WithName("GetOrdersByUserId");

        group.MapPut("/{id:long}/status", async (long id, [FromQuery] OrderStatus status, [FromServices] IOrderService service) =>
        {
            await service.UpdateStatusAsync(id, status);
            return Results.NoContent();
        })
        .WithName("UpdateOrderStatus");

        group.MapGet("/status/{status}", async (OrderStatus status, [FromServices] IOrderService service) =>
        {
            var orders = await service.GetByStatusAsync(status);
            return Results.Ok(orders);
        })
        .WithName("GetOrdersByStatus");

        group.MapGet("/{orderId:long}/total", async (long orderId, [FromServices] IOrderService service) =>
        {
            var total = await service.CalculateTotalAmountAsync(orderId);
            return Results.Ok(total);
        })
        .WithName("GetOrderTotalAmount");

        group.MapGet("/recent", async ([FromQuery] int count, [FromServices] IOrderService service) =>
        {
            var orders = await service.GetRecentOrdersAsync(count);
            return Results.Ok(orders);
        })
        .WithName("GetRecentOrders");

        group.MapGet("/{orderId:long}/exists", async (long orderId, [FromServices] IOrderService service) =>
        {
            var exists = await service.ExistsAsync(orderId);
            return Results.Ok(exists);
        })
        .WithName("CheckOrderExists");

        group.MapGet("/pending", async ([FromServices] IOrderService service) =>
        {
            var pending = await service.GetPendingOrdersAsync();
            return Results.Ok(pending);
        })
        .WithName("GetPendingOrders");

        group.MapGet("/range", async ([FromQuery] DateTime from, [FromQuery] DateTime to, [FromServices] IOrderService service) =>
        {
            var orders = await service.GetByDateRangeAsync(from, to);
            return Results.Ok(orders);
        })
        .WithName("GetOrdersByDateRange");

        group.MapGet("/count", async ([FromServices] IOrderService service) =>
        {
            var total = await service.GetTotalOrdersCountAsync();
            return Results.Ok(total);
        })
        .WithName("GetTotalOrdersCount");
    }
}
