using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Femira.api.Data.Entities
{
    public class OrderItem
    {
        [Key]
        public long OrderItem_Id { get; set; }

        public int Order_Id { get; set; }
        public virtual Order Order { get; set; }

        public int Product_Id { get; set; }

        public string P_Name { get; set; }

        public decimal P_Price { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Unit { get; set; }

        public string P_ImageUrl { get; set; }
    }
}
