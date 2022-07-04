using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string ClientUrl { get; set; }
    }
}