using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femira.Shared.Dtos
{
    public class ProductDto
    {
        public int Product_Id { get; set; }

        public string P_Name { get; set; }

        public string P_Description { get; set; }

        public string P_ImageUrl { get; set; }

        public decimal P_Price { get; set; }

        public string P_Category { get; set; }

        public string unit { get; set; }
    }

    public class AddressDto
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string name { get; set; }

        public bool IsDefault {  get; set; }
    }
 
}
