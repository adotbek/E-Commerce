using Application.Dtos;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class AddressEndpoints
{
    public static void MapAddressEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/addresses")
                       .WithTags("AddressManagement")
                       .RequireAuthorization();

        group.MapGet("/{id:long}", async (long id, [FromServices] IAddressService service) =>
        {
            var address = await service.GetByIdAsync(id);
            return address is not null ? Results.Ok(address) : Results.NotFound();
        })
        .WithName("GetAddressById");

        group.MapGet("/user/{userId:long}", async (long userId, [FromServices] IAddressService service) =>
        {
            var addresses = await service.GetByUserIdAsync(userId);
            return Results.Ok(addresses);
        })
        .WithName("GetAddressesByUserId");

        group.MapPost("/", async ([FromBody] AddressCreateDto dto, [FromServices] IAddressService service) =>
        {
            var id = await service.AddAddressAsync(dto);
            return Results.Created($"/api/addresses/{id}", id);
        })
        .WithName("CreateAddress");

        group.MapPut("/{id:long}", async (long id, [FromBody] AddressUpdateDto dto, [FromServices] IAddressService service) =>
        {
            await service.UpdateAsync(id, dto);
            return Results.NoContent();
        })
        .WithName("UpdateAddress");

        group.MapDelete("/{id:long}", async (long id, [FromServices] IAddressService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteAddress");

        group.MapPost("/set-default/{userId:long}/{addressId:long}", async (long userId, long addressId, [FromServices] IAddressService service) =>
        {
            await service.SetDefaultAddressAsync(userId, addressId);
            return Results.Ok();
        })
        .WithName("SetDefaultAddress");

        group.MapGet("/default/{userId:long}", async (long userId, [FromServices] IAddressService service) =>
        {
            var defaultAddress = await service.GetDefaultAddressAsync(userId);
            return defaultAddress is not null ? Results.Ok(defaultAddress) : Results.NotFound();
        })
        .WithName("GetDefaultAddress");

        group.MapGet("/exists/{id:long}/{userId:long}", async (long id, long userId, [FromServices] IAddressService service) =>
        {
            var exists = await service.ExistsAsync(id, userId);
            return Results.Ok(exists);
        })
        .WithName("CheckAddressExists");
    }
}
