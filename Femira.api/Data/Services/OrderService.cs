using Femira.api.Data.Entities;
using Femira.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Femira.api.Data.Services
{
    public class OrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResult> PlaceOrderAsync(PlacedOrderDto dto, int userId)
        {
            if(dto.Items.Length == 0)
                return ApiResult.Fail("Order Must Contain Items");

            var productIds = dto.Items.Select(i => i.Product_Id).ToHashSet();

            var products = await _context.Products
                .Where(p => productIds.Contains(p.Product_Id))
                .ToDictionaryAsync(p=> p.Product_Id);

            if(products.Count == dto.Items.Length)
            {
                return ApiResult.Fail("Some Product is not avaible");
            }

            var orderItems = dto.Items
                 .Select(i => new OrderItem
                 {
                     Product_Id = i.Product_Id,
                     Quantity = i.Quantity,
                     P_ImageUrl = products[i.Product_Id].P_ImageUrl,
                     P_Name = products[i.Product_Id].P_Name,
                     P_Price = products[i.Product_Id].P_Price,
                     Unit = products[i.Product_Id].unit,
                 }).ToArray();

            var now = DateTime.UtcNow;
            var order = new Order
            {
                Order_Date = now,
                User_Id = userId,
                User_Address_Id = dto.User_Address_Id,
                Address = dto.Address,
                AddressName = dto.AddressName,
                TotalItems = dto.Items.Length,
                Total_Amount = orderItems.Sum(oi => oi.Quantity * oi.P_Price),
                OrderItems = orderItems
            };
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return ApiResult.Success();
            }
            catch(Exception ex)
            {
                return ApiResult.Fail(ex.Message);

            }
        }

        public async Task<AddressDto[]> GetUserOrdersAsync(int userId, int startIndex, int pageSize) =>
            await _context.UserAddresses
                .AsNoTracking()
                .Where(a => a.User_Id == userId)
                .OrderByDescending(a => a.Address_Id)
                .Skip(startIndex).Take(pageSize)
                .Select(a=> new AddressDto
                {
                    Id = a.Address_Id,
                    Address = a.Address,
                    IsDefault = a.IsDefault,
                    name = a.Name
                })
                .ToArrayAsync();
        
        public async Task<ApiResult<OrderItemDto[]>> GetUserOrderItemsAsync(int orderId, int userId)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(o=> o.OrderItems)
                .FirstOrDefaultAsync(o => o.Order_Id == orderId);
            if (order == null)
                return ApiResult<OrderItemDto[]>.Fail("Order Not Found");

            if (order.User_Id != userId)
                return ApiResult<OrderItemDto[]>.Fail("Order Not Found");


            var items = order.OrderItems
                .Select(oi => new OrderItemDto
                    {
                        OrderItem_Id = oi.OrderItem_Id,
                        Product_Id = oi.Product_Id,
                        P_ImageUrl = oi.P_ImageUrl,
                        P_Name = oi.P_Name,
                        P_Price = oi.P_Price,
                        Quantity = oi.Quantity,
                        Unit = oi.Unit
                    })
                 .ToArray();

            return ApiResult<OrderItemDto[]>.Success(items);
        }
    }


    
}
