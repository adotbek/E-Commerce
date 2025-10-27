using Application.Dtos;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Endpoints;

public static class AdminEndpoints
{
    public static void MapAdminEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/admins")
            .WithTags("AdminManagement")
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,SuperAdmin" });

        group.MapPost("/categories", async ([FromBody] CategoryCreateDto dto, ICategoryService service) =>
        {
            var id = await service.AddCategoryAsync(dto);
            return Results.Created($"/api/categories/{id}", id);
        })
        .WithName("CreateCategory");

        group.MapPut("/categories/{id:long}", async (long id, [FromBody] CategoryUpdateDto dto, ICategoryService service) =>
        {
            await service.UpdateAsync(id, dto);
            return Results.NoContent();
        })
        .WithName("UpdateCategory");

        group.MapDelete("/categories/{id:long}", async (long id, ICategoryService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteCategory");

        group.MapPost("/coupons", async ([FromBody] CouponCreateDto dto, ICouponService service) =>
        {
            var id = await service.AddCouponAsync(dto);
            return Results.Created($"/api/admins/coupons/{id}", id);
        })
        .WithName("CreateCoupon");

        group.MapPut("/coupons/{id:long}", async (long id, [FromBody] CouponUpdateDto dto, ICouponService service) =>
        {
            await service.UpdateAsync(id, dto);
            return Results.NoContent();
        })
        .WithName("UpdateCoupon");

        group.MapDelete("/coupons/{id:long}", async (long id, ICouponService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteCoupon");

        group.MapGet("/coupons/active", async (ICouponService service) =>
        {
            var activeCoupons = await service.GetActiveCouponsAsync();
            return Results.Ok(activeCoupons);
        })
        .WithName("GetActiveCoupons");

        group.MapGet("/coupons", async (ICouponService service) =>
        {
            var coupons = await service.GetAllAsync();
            return Results.Ok(coupons);
        })
        .WithName("GetAllCoupons");

        group.MapGet("/coupons/{id:long}", async (long id, ICouponService service) =>
        {
            var coupon = await service.GetByIdAsync(id);
            return coupon is not null ? Results.Ok(coupon) : Results.NotFound();
        })
        .WithName("GetCouponById");

        group.MapGet("/orders", async (IOrderService service) =>
        {
            var orders = await service.GetAllAsync();
            return Results.Ok(orders);
        })
        .WithName("GetAllOrders");

        group.MapGet("/payment-options", async (IPaymentOptionService service) =>
        {
            var options = await service.GetAllAsync();
            return Results.Ok(options);
        })
        .WithName("GetAllPaymentOptions");

        group.MapPost("/product-images", async ([FromBody] ProductImageDto dto, IProductImageService service) =>
        {
            var id = await service.AddProductImageAsync(dto);
            return Results.Created($"/api/product-images/{id}", id);
        })
        .WithName("CreateProductImage");

        group.MapPut("/product-images/{id:long}", async (long id, [FromBody] ProductImageDto dto, IProductImageService service) =>
        {
            await service.UpdateAsync(dto, id);
            return Results.NoContent();
        })
        .WithName("UpdateProductImage");

        group.MapDelete("/product-images/{id:long}", async (long id, IProductImageService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteProductImage");

        group.MapPut("/product/{productId:long}/set-main/{imageId:long}", async (long productId, long imageId, IProductImageService service) =>
        {
            await service.SetMainImageAsync(imageId, productId);
            return Results.NoContent();
        })
        .WithName("SetMainImageForProduct");

        group.MapDelete("/product-images/{id:long}/soft", async (long id, IProductImageService service) =>
        {
            await service.SoftDeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("SoftDeleteProductImage");

        group.MapGet("/reviews", async (IReviewService service) =>
        {
            var reviews = await service.GetAllAsync();
            return Results.Ok(reviews);
        })
        .WithName("GetAllReviews");

        group.MapGet("/reviews/recent", async ([FromQuery] int count, IReviewService service) =>
        {
            var reviews = await service.GetRecentReviewsAsync(count);
            return Results.Ok(reviews);
        })
        .WithName("GetRecentReviews");

        group.MapDelete("/users/{id:long}", async (HttpContext http, long id, IUserService userService) =>
        {
            var userRole = http.User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole is null)
                return Results.Unauthorized();

            await userService.DeleteUserByIdAsync(id, userRole);
            return Results.NoContent();
        })
        .WithName("DeleteUserById");

        group.MapPut("/users/{id:long}/role", async (long id, [FromBody] string newRole, IUserService userService) =>
        {
            await userService.UpdateUserRoleAsync(id, newRole);
            return Results.NoContent();
        })
        .WithName("UpdateUserRole");
    }
}
