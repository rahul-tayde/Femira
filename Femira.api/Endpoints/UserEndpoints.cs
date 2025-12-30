using Femira.api.Data.Services;
using Femira.Shared;
using Femira.Shared.Dtos;
using System.Security.Claims;

namespace Femira.api.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var userGroup = app.MapGroup("/api/user")
                .RequireAuthorization()
                .WithTags("User");

            userGroup.MapPost("/adsresses", async (AddressDto dto, UserService service, ClaimsPrincipal principal) =>
            {
                return Results.Ok(await service.SaveAddressAsync(dto, principal.GetUserId()));
            })
            .Produces<ApiResult>()
            .WithName("Save-Address");

            userGroup.MapGet("/adsresses", async (UserService service, ClaimsPrincipal principal) =>
            {
                return Results.Ok(await service.GetAddressesAsync(principal.GetUserId()));
            })
            .Produces<AddressDto>()
            .WithName("Get-Addresses");

            userGroup.MapPost("/Change-Password", async (ChangePasswordDto dto, UserService service, ClaimsPrincipal principal) =>
            {
                return Results.Ok(await service.ChangePasswordAsync(dto, principal.GetUserId()));
            })
            .Produces<ApiResult>()
            .WithName("Change-Password");

            return app;
        }
    }
}