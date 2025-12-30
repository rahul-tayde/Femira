using Femira.api.Data.Services;
using Femira.Shared.Dtos;

namespace Femira.api.Endpoints
{
    public static class ProductEndpoints
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/products", async (ProductService service)
                => Results.Ok(await service.GetProductsAsync())
            ).Produces<ProductDto[]>()
            .WithName("Products");
            return app;
        }
    }
}