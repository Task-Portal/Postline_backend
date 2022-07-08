using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.ForAuth
{
    public class TwoFactorDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Provider { get; set; }
        [Required]
        public string Token { get; set; }
    }
}

