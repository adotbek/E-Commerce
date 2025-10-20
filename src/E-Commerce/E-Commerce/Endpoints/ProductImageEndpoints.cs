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

        group.MapGet("/", async (IProductImageService service) =>
        {
            var images = await service.GetAllAsync();
            return Results.Ok(images);
        })
        .WithName("GetAllProductImages");

        group.MapGet("/{id:long}", async (long id, IProductImageService service) =>
        {
            var image = await service.GetByIdAsync(id);
            return image is not null ? Results.Ok(image) : Results.NotFound();
        })
        .WithName("GetProductImageById");

        group.MapPost("/", async ([FromBody] ProductImageDto dto, IProductImageService service) =>
        {
            var id = await service.AddProductImageAsync(dto);
            return Results.Created($"/api/product-images/{id}", id);
        })
        .WithName("CreateProductImage");

        group.MapPut("/{id:long}", async (long id, [FromBody] ProductImageDto dto, IProductImageService service) =>
        {
            await service.UpdateAsync(dto, id);
            return Results.NoContent();
        })
        .WithName("UpdateProductImage");

        group.MapDelete("/{id:long}", async (long id, IProductImageService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteProductImage");

        group.MapGet("/product/{productId:long}", async (long productId, IProductImageService service) =>
        {
            var images = await service.GetByProductIdAsync(productId);
            return Results.Ok(images);
        })
        .WithName("GetImagesByProductId");

        group.MapGet("/product/{productId:long}/main", async (long productId, IProductImageService service) =>
        {
            var mainImage = await service.GetMainImageByProductIdAsync(productId);
            return mainImage is not null ? Results.Ok(mainImage) : Results.NotFound();
        })
        .WithName("GetMainImageByProductId");

        group.MapPut("/product/{productId:long}/set-main/{imageId:long}", async (long productId, long imageId, IProductImageService service) =>
        {
            await service.SetMainImageAsync(imageId, productId);
            return Results.NoContent();
        })
        .WithName("SetMainImageForProduct");

        group.MapDelete("/{id:long}/soft", async (long id, IProductImageService service) =>
        {
            await service.SoftDeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("SoftDeleteProductImage");
    }
}
