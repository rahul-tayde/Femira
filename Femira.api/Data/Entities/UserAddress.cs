using System.ComponentModel.DataAnnotations;

namespace Femira.api.Data.Entities
{
    public class UserAddress
    {
        [Key]
        public int Address_Id { get; set; }
        public int User_Id { get; set; }
        public virtual User User { get; set; }

        [Required, MaxLength(250)]
        public string Address { get; set; }
        
        [Required, MaxLength(20)]
        public string Name { get; set; }

        public bool IsDefault { get; set; }
    }
}
