using Femira.api.Data.Services;
using Femira.Shared.Dtos;

namespace Femira.api.Endpoints
{
    public static class AuthEndpoints
    {
        public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var authGroup = app.MapGroup("/api/auth").WithTags("Auth");

            authGroup.MapPost("/register", async (RegisterDto dto, AuthService service) =>
            {
                return Results.Ok(await service.RegisterAsync(dto));
            })
            .Produces<ApiResult>()
            .WithName("Register");

            authGroup.MapPost("/login", async (LoginDto dto, AuthService service) =>
            {
                return Results.Ok(await service.LoginAsync(dto));
            })
            .Produces<ApiResult<LoggedInUser>>()
            .WithName("Register");

            return app;
        }
    }
}






