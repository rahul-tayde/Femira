using Femira.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Femira.api.Data.Services
{
    public class ProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context) 
        {
            _context = context;
        }

        public async Task<ProductDto[]> GetProductsAsync() =>
            await _context.Products
            .AsNoTracking()
            .Select(p => new ProductDto
            {
                Product_Id = p.Product_Id,
                P_Name = p.P_Name,
                P_Category = p.P_Category,
                P_Description = p.P_Description,
                P_ImageUrl = p.P_ImageUrl,
                P_Price = p.P_Price,
                unit = p.unit
            }).ToArrayAsync();
    } 
}
