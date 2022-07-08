using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.ForAuth
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password name is required")]
        public string Password { get; set; }
        public string ClientURL { get; set; }
    }
}