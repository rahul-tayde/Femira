using System.ComponentModel.DataAnnotations;

namespace Femira.api.Data.Entities
{
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }

        [Required, MaxLength(100)]
        public string P_Name { get; set; }

        [Required]
        public string P_Description { get; set; }

        [Required]
        public string P_ImageUrl { get; set; }

        [Required]
        public decimal P_Price { get; set; }

        [Required, MaxLength(20)]
        public string unit { get; set; }
    } 
}
