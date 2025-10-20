using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Dtos;

namespace Api.Endpoints;

public static class BannerEndpoints
{
    public static void MapBannerEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/banners")
                       .WithTags("BannerManagement")
                       .RequireAuthorization();

        group.MapGet("/", async (IBannerService service) =>
        {
            var banners = await service.GetAllAsync();
            return Results.Ok(banners);
        })
        .WithName("GetAllBanners");

        group.MapGet("/{id:long}", async (long id, IBannerService service) =>
        {
            var banner = await service.GetByIdAsync(id);
            return banner is not null ? Results.Ok(banner) : Results.NotFound();
        })
        .WithName("GetBannerById");

        group.MapPost("/", async ([FromBody] BannerCreateDto dto, IBannerService service) =>
        {
            var id = await service.AddBannerAsync(dto);
            return Results.Created($"/api/banners/{id}", id);
        })
        .WithName("CreateBanner");

        group.MapPut("/{id:long}", async (long id, [FromBody] BannerUpdateDto dto, IBannerService service) =>
        {
            await service.UpdateAsync(id, dto);
            return Results.NoContent();
        })
        .WithName("UpdateBanner");

        group.MapDelete("/{id:long}", async (long id, IBannerService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteBanner");

        group.MapGet("/active", async (IBannerService service) =>
        {
            var activeBanners = await service.GetActiveAsync();
            return Results.Ok(activeBanners);
        })
        .WithName("GetActiveBanners");

        group.MapPatch("/{id:long}/toggle", async (long id, IBannerService service) =>
        {
            await service.ToggleActiveAsync(id);
            return Results.Ok();
        })
        .WithName("ToggleBannerActive");
    }
}
