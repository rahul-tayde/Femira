using System.ComponentModel.DataAnnotations;

namespace Femira.api.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string full_name { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [Required, MaxLength(15)]
        public string Mobile_Number { get; set; }

        [Required]
        public string Password_Hash { get; set; }

        public ICollection<UserAddress> UserAddresses { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
