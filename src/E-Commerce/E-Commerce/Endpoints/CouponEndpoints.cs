using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Dtos;

namespace Api.Endpoints;

public static class CouponEndpoints
{
    public static void MapCouponEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/coupons")
                       .WithTags("CouponManagement")
                       .RequireAuthorization();

        group.MapGet("/by-code/{code}", async (string code, [FromServices] ICouponService service) =>
        {
            var coupon = await service.GetByCodeAsync(code);
            return coupon is not null ? Results.Ok(coupon) : Results.NotFound();
        })
        .WithName("GetCouponByCode");

        group.MapGet("/validate/{code}", async (string code, [FromServices] ICouponService service) =>
        {
            var isValid = await service.ValidateCouponAsync(code);
            return Results.Ok(isValid);
        })
        .WithName("ValidateCoupon");

        group.MapPost("/apply", async ([FromQuery] string code, [FromQuery] decimal totalPrice, [FromServices] ICouponService service) =>
        {
            var discountedPrice = await service.ApplyCouponAsync(code, totalPrice);
            return Results.Ok(discountedPrice);
        })
        .WithName("ApplyCoupon");
    }
}
