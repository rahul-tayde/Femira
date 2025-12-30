namespace Femira.Shared.Dtos
{
    public class OrderItemDto
    {
        public long OrderItem_Id { get; set; }

        public int Product_Id { get; set; }

        public string P_Name { get; set; }

        public decimal P_Price { get; set; }

        public int Quantity { get; set; }

        public string Unit { get; set; }

        public string P_ImageUrl { get; set; }
    }
}
