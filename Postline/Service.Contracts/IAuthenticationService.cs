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
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<bool> ValidateEmail(string email);
        Task<bool> ValidateUserName(string userName);
        Task<string> CreateToken();
        Task<UserDto> GetAuthUser(string id);
        
        
    }
}