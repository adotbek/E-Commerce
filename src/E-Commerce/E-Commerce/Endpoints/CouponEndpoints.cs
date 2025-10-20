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

        group.MapGet("/", async (ICouponService service) =>
        {
            var coupons = await service.GetAllAsync();
            return Results.Ok(coupons);
        })
        .WithName("GetAllCoupons");

        group.MapGet("/{id:long}", async (long id, ICouponService service) =>
        {
            var coupon = await service.GetByIdAsync(id);
            return coupon is not null ? Results.Ok(coupon) : Results.NotFound();
        })
        .WithName("GetCouponById");

        group.MapGet("/by-code/{code}", async (string code, ICouponService service) =>
        {
            var coupon = await service.GetByCodeAsync(code);
            return coupon is not null ? Results.Ok(coupon) : Results.NotFound();
        })
        .WithName("GetCouponByCode");

        group.MapGet("/active", async (ICouponService service) =>
        {
            var activeCoupons = await service.GetActiveCouponsAsync();
            return Results.Ok(activeCoupons);
        })
        .WithName("GetActiveCoupons");

        group.MapPost("/", async ([FromBody] CouponCreateDto dto, ICouponService service) =>
        {
            var id = await service.AddCouponAsync(dto);
            return Results.Created($"/api/coupons/{id}", id);
        })
        .WithName("CreateCoupon");

        group.MapPut("/{id:long}", async (long id, [FromBody] CouponUpdateDto dto, ICouponService service) =>
        {
            await service.UpdateAsync(id, dto);
            return Results.NoContent();
        })
        .WithName("UpdateCoupon");

        group.MapDelete("/{id:long}", async (long id, ICouponService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteCoupon");

        group.MapGet("/validate/{code}", async (string code, ICouponService service) =>
        {
            var isValid = await service.ValidateCouponAsync(code);
            return Results.Ok(isValid);
        })
        .WithName("ValidateCoupon");

        group.MapPost("/apply", async ([FromQuery] string code, [FromQuery] decimal totalPrice, ICouponService service) =>
        {
            var discountedPrice = await service.ApplyCouponAsync(code, totalPrice);
            return Results.Ok(discountedPrice);
        })
        .WithName("ApplyCoupon");
    }
}
