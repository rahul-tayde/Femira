using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Femira.Shared.Dtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }

        [JsonIgnore]
        [Required, Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }

    }
}
