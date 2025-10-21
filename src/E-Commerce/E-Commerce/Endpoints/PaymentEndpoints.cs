using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Dtos;

namespace Api.Endpoints;

public static class PaymentEndpoints
{
    public static void MapPaymentEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/payments")
                       .WithTags("PaymentManagement")
                       .RequireAuthorization();

        group.MapGet("/", async ([FromServices] IPaymentService service) =>
        {
            var payments = await service.GetAllAsync();
            return Results.Ok(payments);
        })
        .WithName("GetAllPayments");

        group.MapGet("/{id:long}", async (long id, [FromServices] IPaymentService service) =>
        {
            var payment = await service.GetByIdAsync(id);
            return payment is not null ? Results.Ok(payment) : Results.NotFound();
        })
        .WithName("GetPaymentById");

        group.MapPost("/", async ([FromBody] PaymentCreateDto dto, [FromServices] IPaymentService service) =>
        {
            var id = await service.AddPaymentAsync(dto);
            return Results.Created($"/api/payments/{id}", id);
        })
        .WithName("CreatePayment");

        group.MapPut("/{id:long}", async (long id, [FromBody] PaymentUpdateDto dto, [FromServices] IPaymentService service) =>
        {
            await service.UpdateAsync(dto, id);
            return Results.NoContent();
        })
        .WithName("UpdatePayment");

        group.MapDelete("/{id:long}", async (long id, [FromServices] IPaymentService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeletePayment");

        group.MapGet("/user/{userId:long}", async (long userId, [FromServices] IPaymentService service) =>
        {
            var payments = await service.GetByUserIdAsync(userId);
            return Results.Ok(payments);
        })
        .WithName("GetPaymentsByUserId");

        group.MapGet("/order/{orderId:long}", async (long orderId, [FromServices] IPaymentService service) =>
        {
            var payment = await service.GetByOrderIdAsync(orderId);
            return payment is not null ? Results.Ok(payment) : Results.NotFound();
        })
        .WithName("GetPaymentByOrderId");

        group.MapGet("/status/{status}", async (string status, [FromServices] IPaymentService service) =>
        {
            var payments = await service.GetByStatusAsync(status);
            return Results.Ok(payments);
        })
        .WithName("GetPaymentsByStatus");

        group.MapGet("/range", async ([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromServices] IPaymentService service) =>
        {
            var payments = await service.GetByDateRangeAsync(startDate, endDate);
            return Results.Ok(payments);
        })
        .WithName("GetPaymentsByDateRange");

        group.MapGet("/user/{userId:long}/total", async (long userId, [FromServices] IPaymentService service) =>
        {
            var total = await service.GetTotalPaidByUserAsync(userId);
            return Results.Ok(total);
        })
        .WithName("GetTotalPaidByUser");

        group.MapGet("/total", async ([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromServices] IPaymentService service) =>
        {
            var total = await service.GetTotalPaidInPeriodAsync(startDate, endDate);
            return Results.Ok(total);
        })
        .WithName("GetTotalPaidInPeriod");

        group.MapGet("/transaction/{transactionId}", async (string transactionId, [FromServices] IPaymentService service) =>
        {
            var payment = await service.GetByTransactionIdAsync(transactionId);
            return payment is not null ? Results.Ok(payment) : Results.NotFound();
        })
        .WithName("GetPaymentByTransactionId");

        group.MapPut("/{paymentId:long}/status", async (long paymentId, [FromQuery] string newStatus, [FromServices] IPaymentService service) =>
        {
            await service.UpdateStatusAsync(paymentId, newStatus);
            return Results.NoContent();
        })
        .WithName("UpdatePaymentStatus");

        group.MapGet("/order/{orderId:long}/completed", async (long orderId, [FromServices] IPaymentService service) =>
        {
            var completed = await service.IsPaymentCompletedAsync(orderId);
            return Results.Ok(completed);
        })
        .WithName("IsPaymentCompleted");
    }
}
