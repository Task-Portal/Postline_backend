using System;

namespace Shared.DataTransferObjects.ForUpdate
{
    public class UserForUpdateDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }

        public bool IsTwoFactorAuthorizationEnabled { get; set; }
    }
}