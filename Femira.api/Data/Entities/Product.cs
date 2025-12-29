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
        public string P_Category { get; set; }

        [Required, MaxLength(20)]
        public string unit { get; set; }

        public static Product[] GetSeedData()
        {
            const string ImageUrl = "https://raw.githubusercontent.com/rahul-tayde/Femira-product-img/main/";

            Product[] products = [
                new () { Product_Id = 1, P_Name = "Brightening Face Wash",P_Description = "Brightening face-wash for oily skin", P_ImageUrl = $"{ImageUrl}brighteningfacewash.png",unit = "each", P_Price = 199.00m, P_Category = "Facewash"},
                new () { Product_Id = 2, P_Name = "Kojic Acid Serum",P_Description = "Kojic Acid Serum for Dark Spot Remove", P_ImageUrl = $"{ImageUrl}KojicAcid-Serum.png",unit = "each", P_Price = 399.00m, P_Category = "Serum"}
                ];

            return products;
        }
    } 
}
