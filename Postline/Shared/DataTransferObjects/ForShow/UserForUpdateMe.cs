namespace Shared.DataTransferObjects.ForShow
{
    public class UserForUpdateMe
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsTwoFactorAuthorizationEnabled { get; set; }
    }
}