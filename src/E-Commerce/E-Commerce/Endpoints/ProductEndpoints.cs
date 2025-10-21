using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.DTOs;

namespace Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/products")
                       .WithTags("ProductManagement")
                       .RequireAuthorization();

        group.MapGet("/", async ([FromServices] IProductService service) =>
        {
            var products = await service.GetAllAsync();
            return Results.Ok(products);
        })
        .WithName("GetAllProducts");

        group.MapGet("/{id:long}", async (long id, [FromServices] IProductService service) =>
        {
            var product = await service.GetByIdAsync(id);
            return product is not null ? Results.Ok(product) : Results.NotFound();
        })
        .WithName("GetProductById");

        group.MapPost("/", async ([FromBody] ProductDto dto, [FromQuery] long categoryId, [FromServices] IProductService service) =>
        {
            var id = await service.AddProductAsync(dto, categoryId);
            return Results.Created($"/api/products/{id}", id);
        })
        .WithName("CreateProduct");

        group.MapPut("/{id:long}", async (long id, [FromBody] ProductDto dto, [FromQuery] long categoryId, [FromServices] IProductService service) =>
        {
            await service.UpdateAsync(dto, categoryId, id);
            return Results.NoContent();
        })
        .WithName("UpdateProduct");

        group.MapDelete("/{id:long}", async (long id, [FromServices] IProductService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteProduct");

        group.MapGet("/category/{categoryId:long}", async (long categoryId, [FromServices] IProductService service) =>
        {
            var products = await service.GetByCategoryIdAsync(categoryId);
            return Results.Ok(products);
        })
        .WithName("GetProductsByCategoryId");

        group.MapGet("/featured", async ([FromServices] IProductService service) =>
        {
            var products = await service.GetFeaturedAsync();
            return Results.Ok(products);
        })
        .WithName("GetFeaturedProducts");

        group.MapGet("/new-arrivals", async ([FromServices] IProductService service) =>
        {
            var products = await service.GetNewArrivalsAsync();
            return Results.Ok(products);
        })
        .WithName("GetNewArrivals");

        group.MapGet("/search", async ([FromQuery] string keyword, [FromServices] IProductService service) =>
        {
            var products = await service.SearchAsync(keyword);
            return Results.Ok(products);
        })
        .WithName("SearchProducts");

        group.MapGet("/{id:long}/exists", async (long id, [FromServices] IProductService service) =>
        {
            var exists = await service.ExistsAsync(id);
            return Results.Ok(exists);
        })
        .WithName("CheckProductExists");

        group.MapPut("/{id:long}/update-stock", async (long id, [FromQuery] int quantity, [FromServices] IProductService service) =>
        {
            await service.UpdateStockAsync(id, quantity);
            return Results.NoContent();
        })
        .WithName("UpdateProductStock");

        group.MapGet("/out-of-stock", async ([FromServices] IProductService service) =>
        {
            var products = await service.GetOutOfStockAsync();
            return Results.Ok(products);
        })
        .WithName("GetOutOfStockProducts");

        group.MapGet("/{id:long}/discount-price", async (long id, [FromServices] IProductService service) =>
        {
            var discountPrice = await service.GetDiscountPriceAsync(id);
            return discountPrice is not null ? Results.Ok(discountPrice) : Results.NotFound();
        })
        .WithName("GetDiscountPrice");
    }
}
