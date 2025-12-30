using Femira.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Femira.api.Data.Entities
{
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }
        
        public int User_Id { get; set; }

        public virtual User User { get; set; }

        [Required]
        public DateTime Order_Date { get; set; }

        public decimal Total_Amount { get; set; }

        [MaxLength(100)]
        public string? remarks { get; set; }

        public string status { get; set; } = nameof(OrderStatus.Placed);

        public int User_Address_Id { get; set; }
        public string Address { get; set; }

        public string AddressName { get; set; }

        public int TotalItems { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
