using System.ComponentModel.DataAnnotations;

namespace Femira.Shared.Dtos
{
    public class OrderDto
    {
        public int Order_Id { get; set; }

        [Required]
        public DateTime Order_Date { get; set; }

        public decimal Total_Amount { get; set; }

        [MaxLength(100)]
        public string? remarks { get; set; }

        public string status { get; set; }

        public string Address { get; set; }

        public string AddressName { get; set; }

        public int TotalItems { get; set; }
    }
}
