using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ForAuth;
using Shared.DataTransferObjects.ForShow;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<IdentityResult> ResetPassword(ResetPasswordDto resetPasswordDto);
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<bool> ValidateEmail(string email);
        Task<bool> SendRestoreLinkToEmail(ForgotPasswordDto forgotPasswordDto);
        Task<string> CreateToken();
        Task<UserDto> GetAuthUser(string id);

        Task<bool> IsEmailConfirmed(UserForAuthenticationDto userForAuthentication);
        Task<bool> IsUserLockOut(UserForAuthenticationDto userForAuthentication);
        Task SetLockoutEndDateAsync(string email);
        Task<IdentityResult> EmailConfirmation(string email, string token);


    }
}