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

        group.MapGet("/", async ([FromServices] ICategoryService service) =>
        {
            var categories = await service.GetAllAsync();
            return Results.Ok(categories);
        })
        .WithName("GetAllCategories");

        group.MapGet("/{id:long}", async (long id, [FromServices] ICategoryService service) =>
        {
            var category = await service.GetByIdAsync(id);
            return category is not null ? Results.Ok(category) : Results.NotFound();
        })
        .WithName("GetCategoryById");
    }
}
