using Femira.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Femira.Shared.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Username  { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
