using Application.Dtos;
using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class ReviewEndpoints
{
    public static void MapReviewEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/reviews")
                       .WithTags("ReviewManagement")
                       .RequireAuthorization();

        group.MapGet("/{id:long}", async (long id, [FromServices] IReviewService service) =>
        {
            var review = await service.GetByIdAsync(id);
            return review is not null ? Results.Ok(review) : Results.NotFound();
        })
        .WithName("GetReviewById");

        group.MapPost("/", async ([FromBody] ReviewDto dto, [FromServices] IReviewService service) =>
        {
            var id = await service.AddReviewAsync(dto);
            return Results.Created($"/api/reviews/{id}", id);
        })
        .WithName("CreateReview");

        group.MapPut("/{id:long}", async (long id, [FromBody] ReviewDto dto, [FromServices] IReviewService service) =>
        {
            await service.UpdateAsync(dto, id);
            return Results.NoContent();
        })
        .WithName("UpdateReview");

        group.MapDelete("/{id:long}", async (long id, [FromServices] IReviewService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteReview");

        group.MapGet("/product/{productId:long}", async (long productId, [FromServices] IReviewService service) =>
        {
            var reviews = await service.GetByProductIdAsync(productId);
            return Results.Ok(reviews);
        })
        .WithName("GetReviewsByProductId");

        group.MapGet("/user/{userId:long}", async (long userId, [FromServices] IReviewService service) =>
        {
            var reviews = await service.GetByUserIdAsync(userId);
            return Results.Ok(reviews);
        })
        .WithName("GetReviewsByUserId");

        group.MapGet("/{productId:long}/average-rating", async (long productId, [FromServices] IReviewService service) =>
        {
            var avg = await service.GetAverageRatingByProductIdAsync(productId);
            return Results.Ok(avg);
        })
        .WithName("GetAverageRating");

        group.MapGet("/{productId:long}/count", async (long productId, [FromServices] IReviewService service) =>
        {
            var count = await service.GetReviewCountByProductIdAsync(productId);
            return Results.Ok(count);
        })
        .WithName("GetReviewCount");

        group.MapGet("/exists", async ([FromQuery] long userId, [FromQuery] long productId, [FromServices] IReviewService service) =>
        {
            var exists = await service.ExistsAsync(userId, productId);
            return Results.Ok(exists);
        })
        .WithName("CheckReviewExists");
    }
}
