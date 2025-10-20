using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Dtos;

namespace Api.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/categories")
                       .WithTags("CategoryManagement")
                       .RequireAuthorization();

        group.MapGet("/", async (ICategoryService service) =>
        {
            var categories = await service.GetAllAsync();
            return Results.Ok(categories);
        })
        .WithName("GetAllCategories");

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
    }
}
