using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Endpoints;

public static class AdminEndpoints 
{
    public static void MapAdminEndpoints (this WebApplication app)
    {
        var group = app.MapGroup("/api/admins")
            .WithTags("AdminManagement")
            .RequireAuthorization();

        group.MapGet("/{id:long}", async (long id, ICategoryService service) =>
        {
            var category = await service.GetByIdAsync(id);
            return category is not null ? Results.Ok(category) : Results.NotFound();
        })
        .WithName("GetCategoryById");

        group.MapPost("/", async ([FromBody] CategoryCreateDto dto, ICategoryService service) =>
        {
            var id = await service.AddCategoryAsync(dto);
            return Results.Created($"/api/categories/{id}", id);
        })
        .WithName("CreateCategory");

        group.MapPut("/{id:long}", async (long id, [FromBody] CategoryUpdateDto dto, ICategoryService service) =>
        {
            await service.UpdateAsync(id, dto);
            return Results.NoContent();
        })
        .WithName("UpdateCategory");

        group.MapDelete("/{id:long}", async (long id, ICategoryService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteCategory");

        group.MapPost("/apply", async ([FromQuery] string code, [FromQuery] decimal totalPrice, ICouponService service) =>
        {
            var discountedPrice = await service.ApplyCouponAsync(code, totalPrice);
            return Results.Ok(discountedPrice);
        })
           .WithName("ApplyCoupon");
    }
}
