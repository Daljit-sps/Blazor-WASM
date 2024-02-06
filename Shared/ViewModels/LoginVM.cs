using System.ComponentModel.DataAnnotations;

namespace Shared.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

    }
}
