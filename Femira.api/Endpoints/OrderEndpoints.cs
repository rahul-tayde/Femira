using Femira.api.Data.Services;
using Femira.Shared;
using Femira.Shared.Dtos;
using System.Security.Claims;

namespace Femira.api.Endpoints
{
    public static class OrderEndpoints
    {
        public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
        {
            var orderGroup = app.MapGroup("/api/orders")
                .RequireAuthorization()
                .WithTags("Orders");

            orderGroup.MapPost("/place-order", async (PlacedOrderDto dto, OrderService service, ClaimsPrincipal principal) =>
            {
                return Results.Ok(await service.PlaceOrderAsync(dto, principal.GetUserId()));

            })
             .Produces<ApiResult>()
             .WithName("Place-Order");

            orderGroup.MapGet("/user/{userId: int}", async (int userId, int startIndex, int pageSize, OrderService service, ClaimsPrincipal principal) =>
            {
                if (userId != principal.GetUserId())
                    return Results.Unauthorized();

                return Results.Ok(await service.GetUserOrdersAsync(principal.GetUserId(),startIndex, pageSize));

            })
             .Produces<OrderDto[]>()
             .WithName("Get-User-Order");


            orderGroup.MapGet("/user/{userId: int}/orders/{orderId: int}/items", async (int userId,int orderId, OrderService service, ClaimsPrincipal principal) =>
            {
                if (userId != principal.GetUserId())
                    return Results.Unauthorized();

                return Results.Ok(await service.GetUserOrderItemsAsync(principal.GetUserId(),orderId));

            })
             .Produces<OrderItemDto[]>()
             .WithName("Get-User-Order-Items");

            return app;
        }
    }
}