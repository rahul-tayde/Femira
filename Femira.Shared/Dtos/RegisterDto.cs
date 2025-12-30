using System.ComponentModel.DataAnnotations;

namespace Femira.Shared.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        public string Password  { get; set; }

    }
}
