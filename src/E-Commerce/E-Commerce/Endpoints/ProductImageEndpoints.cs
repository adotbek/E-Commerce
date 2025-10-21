using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.DTOs;

namespace Api.Endpoints;

public static class ProductImageEndpoints
{
    public static void MapProductImageEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/product-images")
                       .WithTags("ProductImageManagement")
                       .RequireAuthorization();

        group.MapGet("/", async ([FromServices] IProductImageService service) =>
        {
            var images = await service.GetAllAsync();
            return Results.Ok(images);
        })
        .WithName("GetAllProductImages");

        group.MapGet("/{id:long}", async (long id, [FromServices] IProductImageService service) =>
        {
            var image = await service.GetByIdAsync(id);
            return image is not null ? Results.Ok(image) : Results.NotFound();
        })
        .WithName("GetProductImageById");

        group.MapGet("/product/{productId:long}", async (long productId, [FromServices] IProductImageService service) =>
        {
            var images = await service.GetByProductIdAsync(productId);
            return Results.Ok(images);
        })
        .WithName("GetImagesByProductId");

        group.MapGet("/product/{productId:long}/main", async (long productId, [FromServices] IProductImageService service) =>
        {
            var mainImage = await service.GetMainImageByProductIdAsync(productId);
            return mainImage is not null ? Results.Ok(mainImage) : Results.NotFound();
        })
        .WithName("GetMainImageByProductId");
    }
}
