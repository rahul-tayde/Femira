using System.ComponentModel.DataAnnotations;

namespace Femira.Shared.Dtos
{
    public class OrderItemSaveDto
    {
        [Required]
        public int Product_Id { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
